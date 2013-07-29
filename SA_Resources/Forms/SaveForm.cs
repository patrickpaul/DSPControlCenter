using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;

namespace SA_Resources
{
    public partial class SaveForm : Form
    {
        private List<DSP_Setting> settings;
            
        delegate void AddTextCallback(string text);

        int delay_ms = 30;

        byte command_RTS = 0x01;
        byte command_SaveEEPROM = 0x02;
        byte command_SetPhantomPower = 0x09;
        byte command_RebootDevice = 0x07;

        byte command_DisableTimers = 0x05;
        byte command_EnableTimers = 0x06;

        bool debug_mode = true;

        private PIC_Bridge _PIC_Conn;

        bool disable_comms = true;

        private int _amp_mode = 1;

        private InputConfig[] input_configs = new InputConfig[4];
        private OutputConfig[] output_configs = new OutputConfig[4];

        public SaveForm(List<DSP_Setting> in_settings, InputConfig[] in_inputs, OutputConfig[] in_outputs, PIC_Bridge PIC_Conn, bool param_disable_comms)
        {
            InitializeComponent();

            settings = in_settings;
            _PIC_Conn = PIC_Conn;

            input_configs = in_inputs;
            output_configs = in_outputs;
            //_amp_mode = amp_mode;

            disable_comms = param_disable_comms;
            //debug_mode = param_debug_mode;
        }

        private void SaveForm_Load(object sender, EventArgs e)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.ProgressChanged += ProgressChanged;
            worker.DoWork += SaveToDevice;
            worker.RunWorkerCompleted += WorkComplete;

            worker.RunWorkerAsync(settings);
        }

        private void SaveToDevice(object sender, DoWorkEventArgs doWorkEventArgs)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            //Code

            bool send_ackd = false;
            int last_command_result = 0;

            bool rts_status;

            BackgroundWorker worker = sender as BackgroundWorker;

            List<DSP_Setting> received_settings_list = doWorkEventArgs.Argument as List<DSP_Setting>;

            double total = received_settings_list.Count;
            double count = 0;

            AddTextToLog("Preparing device...");
            //AddDebugTextToLog("Clearing out serial data buffer... ");

            _PIC_Conn.FlushBuffer();
            //AddDebugTextToLog("Done." + System.Environment.NewLine);


            AddTextToLog("Temporarily disabling RVC and Sleep Timers... ");

            if (_PIC_Conn.sendAckdCommand(command_DisableTimers))
            {
                //AddTextToLog("Done." + System.Environment.NewLine);

            }
            else
            {
                //AddTextToLog("[ERROR] Unable to disable timers. Save may fail." + System.Environment.NewLine);
            }


            AddTextToLog("Saving values to device... ");

            //goto InputStuff;
            foreach (DSP_Setting single_setting in received_settings_list)
            {

                count++;

                if (single_setting.Index > 271 && single_setting.Index < 300)
                {
                    AddDebugTextToLog("SKIPPING " + count + System.Environment.NewLine);
                    continue;
                }

                AddDebugTextToLog("Sending " + single_setting.Name + " [" + single_setting.Value.ToString("X8") + "] ...");
                rts_status = _PIC_Conn.sendAckdCommand(command_RTS);
                if (rts_status == true)
                {
                    //Thread.Sleep(delay_ms);
                    send_ackd = _PIC_Conn.SetDSPValue(single_setting.Index, single_setting.Value);

                    if (!send_ackd)
                    {
                        //AddDebugTextToLog("FAILED. Exiting." + System.Environment.NewLine);
                        return;
                    }
                    else
                    {
                        AddDebugTextToLog("SENT. Verifying...");

                        if (_PIC_Conn.verifyLastCommand())
                        {
                            //AddDebugTextToLog("SUCCESS" + System.Environment.NewLine);
                        }
                        else
                        {
                            //AddDebugTextToLog("FAILED - " + last_command_result + System.Environment.NewLine);
                        }

                    }
                }
                else
                {
                    //AddDebugTextToLog("No RTS. FAILED." + System.Environment.NewLine);
                    return;
                }


                

                worker.ReportProgress((int)((count / total) * 80.0));

            }

            //AddTextToLog("Done." + System.Environment.NewLine);
;

            AddTextToLog("Setting phantom power modes... ");

            bool last_response = false;

            // TODO - change this to use num phantom
            for(int i = 0; i < 4; i++)
            {
                last_response = _PIC_Conn.sendAckdData(command_SetPhantomPower, (byte)i, 100, Convert.ToByte(input_configs[i].PhantomPower));
            }

            if (last_response)
            {
                //AddTextToLog("Done." + System.Environment.NewLine);

            }
            else
            {
                //AddTextToLog("[ERROR] Unable to set phantom power" + System.Environment.NewLine);
            }

            worker.ReportProgress(82);

            InputStuff:
            AddTextToLog("Saving Input Configuration... "+ System.Environment.NewLine);

            // TODO change this to use a channel count
            int retry_count = 0;
            int max_retries = 5;
            
            
            for (int i = 0; i < 4; i++)
            {
                //AddTextToLog("CH " + (i+1) + "... ");
                for(int j = 0; j < max_retries; j++)
                {
                    if(_PIC_Conn.SendChannelName(i+1,input_configs[i].Name))
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

            worker.ReportProgress(84);
            AddTextToLog("Saving Output Configuration... " + System.Environment.NewLine);


            for (int i = 0; i < 4; i++)
            {
                //AddTextToLog("CH " + (i + 1) + "... ");
                for (int j = 0; j < max_retries; j++)
                {
                    if (_PIC_Conn.SendChannelName(i + 1, output_configs[i].Name,true))
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

            worker.ReportProgress(88);

            
            AddTextToLog("Saving to EEPROM. This may take a moment... ");

            if (_PIC_Conn.sendAckdCommand(command_SaveEEPROM, 6000))
            {
                //AddTextToLog("Done." + System.Environment.NewLine);

            }
            else
            {
                //AddTextToLog("[ERROR] Unable to save to EEPROM" + System.Environment.NewLine);
            }

            worker.ReportProgress(98); 

            //AddTextToLog("Re-enabling RVC and Sleep Timers... ");

            if (_PIC_Conn.sendAckdCommand(command_EnableTimers))
            {
                //AddTextToLog("Done." + System.Environment.NewLine);

            }
            else
            {
                //AddTextToLog("[ERROR] Unable to re-enable timers. This can be fixed by rebooting the amplifier." + System.Environment.NewLine);
            }

            AddTextToLog("Rebooting amplifier... ");

            if (_PIC_Conn.sendAckdCommand(command_RebootDevice,5000))
            {
                //AddTextToLog("Done." + System.Environment.NewLine);

            }
            else
            {
                //AddTextToLog("[ERROR] Unable to reboot device." + System.Environment.NewLine);
            }

            AddTextToLog("Done!");
            stopWatch.Stop();
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;

            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Console.WriteLine(elapsedTime, "RunTime"); 

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
