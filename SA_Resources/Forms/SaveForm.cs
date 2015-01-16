using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;

namespace SA_Resources.SAForms
{
    public partial class SaveForm : Form
    {

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | 0x200;
                return myCp;
            }
        } 


        delegate void AddTextCallback(string text);

        byte command_RTS = 0x01;
        //byte command_SaveEEPROM = 0x02;
        byte command_SetPhantomPower = 0x09;
        byte command_RebootDevice = 0x07;

        //byte command_DisableTimers = 0x05;
        //byte command_EnableTimers = 0x06;

        byte command_SwitchPreset1_Operation = 0x10; 
        
        byte command_SwitchPreset1 = 0x20;
        byte command_SwitchPreset2 = 0x21;
        byte command_SwitchPreset3 = 0x22;

        bool debug_mode = true;

        bool disable_comms = true;

        private MainForm_Template PARENT_FORM;

        public SaveForm(MainForm_Template _parentForm, bool param_disable_comms)
        {
            InitializeComponent();

            PARENT_FORM = _parentForm;

            disable_comms = param_disable_comms;
        }

        private void SaveForm_Load(object sender, EventArgs e)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.ProgressChanged += ProgressChanged;
            worker.DoWork += SaveToDevice;
            worker.RunWorkerCompleted += WorkComplete;

            worker.RunWorkerAsync(PARENT_FORM._settings);
        }

        private void SaveToDevice(object sender, DoWorkEventArgs doWorkEventArgs)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            //Code

            bool send_ackd = false;

            bool rts_status;

            BackgroundWorker worker = sender as BackgroundWorker;

            //List<DSP_Setting> received_settings_list = doWorkEventArgs.Argument as List<DSP_Setting>;

            //double total = received_settings_list.Count;

            double count = 0;

            AddTextToLog("Preparing device...");
            AddDebugTextToLog("Clearing out serial data buffer... ");

            PARENT_FORM._PIC_Conn.FlushBuffer();
            AddDebugTextToLog("Done." + System.Environment.NewLine);

            /*
            AddTextToLog("Temporarily disabling RVC and Sleep Timers... ");

            if (PARENT_FORM._PIC_Conn.sendAckdCommand(command_DisableTimers))
            {
                AddTextToLog("Done." + System.Environment.NewLine);

            }
            else
            {
                AddTextToLog("[ERROR] Unable to disable timers. Save may fail." + System.Environment.NewLine);
            }
            */

            AddTextToLog("Switching back to Program 1... ");

            if (PARENT_FORM._PIC_Conn.sendAckdCommand(command_SwitchPreset1))
            {
                AddTextToLog("Done." + System.Environment.NewLine);

            }
            else
            {
                AddTextToLog("[ERROR] Unable to switch to program 1" + System.Environment.NewLine);
            }

            AddTextToLog("Saving values to device... \n");

            int index_counter = 0;
            for (int program_index = 0; program_index < PARENT_FORM.NUM_PROGRAMS; program_index++)
            {
                AddTextToLog("Saving configuration for Program " + (program_index+1) + "\n");
                
                index_counter = 0;
                foreach (DSP_Setting single_setting in PARENT_FORM._settings[program_index])
                {
                    
                    count++;

                    if (single_setting.Value == PARENT_FORM._cached_settings[program_index][index_counter].Value)
                    {
                        AddDebugTextToLog("UNCHANGED " + count + System.Environment.NewLine);
                        index_counter++;
                        continue;
                    }

                    index_counter++;

                    if (single_setting.Index > 271 && single_setting.Index < 300)
                    //if (single_setting.Index > 30)
                    {
                        AddDebugTextToLog("SKIPPING " + count + System.Environment.NewLine);
                        continue;
                    }

                    AddDebugTextToLog("Sending " + single_setting.Name + " [" + single_setting.Value.ToString("X8") + "] ...");

                    rts_status = PARENT_FORM._PIC_Conn.sendAckdCommand(command_RTS);

                    if (rts_status == true)
                    {
                        send_ackd = PARENT_FORM._PIC_Conn.SetDSPValue(single_setting.Index, single_setting.Value);

                        if (!send_ackd)
                        {
                            AddDebugTextToLog("FAILED. Exiting." + System.Environment.NewLine);
                            return;
                        }
                        else
                        {
                            AddDebugTextToLog("SENT. Verifying...");

                            if (PARENT_FORM._PIC_Conn.verifyLastCommand())
                            {
                                AddDebugTextToLog("SUCCESS" + System.Environment.NewLine);
                            }
                            else
                            {
                                AddDebugTextToLog("FAILED" + System.Environment.NewLine);
                            }

                        }
                    }
                    else
                    {
                        AddDebugTextToLog("No RTS. FAILED." + System.Environment.NewLine);
                        return;
                    }

                    worker.ReportProgress((int)((count / (PARENT_FORM._settings[0].Count*3)) * 90.0));

                }
   
                //AddTextToLog("Setting phantom power modes... ");

                bool last_response = false;

                // TODO - change this to use num phantom
                for(int i = 0; i < 4; i++)
                {
                    last_response = PARENT_FORM._PIC_Conn.sendAckdData(command_SetPhantomPower, (byte)i, 100, Convert.ToByte(PARENT_FORM.PROGRAMS[program_index].inputs[i].PhantomPower));
                }

                if (last_response)
                {
                    //AddTextToLog("Done." + System.Environment.NewLine);

                }
                else
                {
                    //AddTextToLog("[ERROR] Unable to set phantom power" + System.Environment.NewLine);
                }

                //worker.ReportProgress(82);

                //AddTextToLog("Saving Input Configuration... "+ System.Environment.NewLine);

                // TODO change this to use a channel count
                int max_retries = 5;
            
            
                for (int i = 0; i < 4; i++)
                {
                    //AddTextToLog("CH " + (i+1) + "... ");
                    for(int j = 0; j < max_retries; j++)
                    {
                        if(PARENT_FORM._PIC_Conn.SendChannelName(i+1,PARENT_FORM.PROGRAMS[program_index].inputs[i].Name))
                        {
                            //AddTextToLog("Completed after " + (j+1) + " attempts." + System.Environment.NewLine);
                            break;
                        }

                        if(j == (max_retries-1))
                        {
                            //AddTextToLog("Failed after " + (j + 1) + " attempts." + System.Environment.NewLine);
                            break;
                        }
                    }
                }

                //worker.ReportProgress(84);
                //AddTextToLog("Saving Output Configuration... " + System.Environment.NewLine);


                for (int i = 0; i < 4; i++)
                {
                    //AddTextToLog("CH " + (i + 1) + "... ");
                    for (int j = 0; j < max_retries; j++)
                    {
                    
                        if (PARENT_FORM._PIC_Conn.SendChannelName(i + 1, PARENT_FORM.PROGRAMS[program_index].outputs[i].Name,true))
                        {
                            //AddTextToLog("Completed after " + (j + 1) + " attempts." + System.Environment.NewLine);
                            break;
                        }

                        if (j == (max_retries - 1))
                        {
                            //AddTextToLog("Failed after " + (j + 1) + " attempts." + System.Environment.NewLine);
                            break;
                        }
                    }
                }

                AddTextToLog("Saving and switching to next Program... ");

                byte switch_command = 0x00;

                if (program_index == 0)
                {
                    switch_command = command_SwitchPreset2;
                }
                else
                {
                    switch_command = command_SwitchPreset3;
                }

                if (PARENT_FORM._PIC_Conn.sendAckdCommand(switch_command, 8000))
                {
                    AddDebugTextToLog("Done." + System.Environment.NewLine);

                }
                else
                {
                    AddDebugTextToLog("[ERROR] Unable to save Program to EEPROM" + System.Environment.NewLine);
                }

            }

            AddTextToLog("Switching back to Program 1... ");

            if (PARENT_FORM._PIC_Conn.sendAckdCommand(command_SwitchPreset1_Operation))
            {
                AddTextToLog("Done." + System.Environment.NewLine);

            }
            else
            {
                AddTextToLog("[ERROR] Unable to switch to program 1" + System.Environment.NewLine);
            }

            worker.ReportProgress(95);

            AddTextToLog("Rebooting amplifier... ");

            if (PARENT_FORM._PIC_Conn.sendAckdCommand(command_RebootDevice, 5000))
            {
                AddTextToLog("Done." + System.Environment.NewLine);

            }
            else
            {
                AddTextToLog("[ERROR] Unable to reboot device." + System.Environment.NewLine);
            }

            worker.ReportProgress(100);

            AddTextToLog("Done!");
            stopWatch.Stop();
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;

            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Debug.WriteLine(elapsedTime, "RunTime"); 

            worker.ReportProgress(100);
        }

        

        private void AddTextToLog(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.lblStatus.InvokeRequired)
            {
                AddTextCallback d = new AddTextCallback(AddTextToLog);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.lblStatus.Text = text;
            }
        }

        private void AddDebugTextToLog(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.textLog.InvokeRequired)
            {
                AddTextCallback d = new AddTextCallback(AddDebugTextToLog);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                if (this.debug_mode)
                {
                    this.textLog.AppendText(text);
                }
            }
        }

        private void WorkComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar1.Value = 100;
            timer1.Enabled = true;

        }
        private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            //lblPercentage.Text = e.ProgressPercentage.ToString() + "%";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
