using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;
using SA_Resources;

namespace DSP_4x4
{
    public partial class ReadForm : Form
    {

        delegate void AddTextCallback(string text);


        byte command_RTS = 0x01;

        byte command_DisableTimers = 0x05;
        byte command_EnableTimers = 0x06;

        bool debug_mode = true;

        bool demo_mode = true;

        private PIC_Bridge _PIC_Conn;
        private MainForm _parentForm;



        public ReadForm(MainForm parentForm, PIC_Bridge PIC_Conn)
        {
            InitializeComponent();
             
            _parentForm = parentForm;
            _PIC_Conn = PIC_Conn;
        }

        private void ReadForm_Load(object sender, EventArgs e)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.ProgressChanged += ProgressChanged;
            worker.DoWork += ReadFromDevice;
            worker.RunWorkerCompleted += WorkComplete;

            worker.RunWorkerAsync();
        }

        private void ReadFromDevice(object sender, DoWorkEventArgs doWorkEventArgs)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            bool rts_status;

            BackgroundWorker worker = sender as BackgroundWorker;

            //TODO CHANGE ME
            double total = (_parentForm._settings[0].Count-210) * _parentForm.NUM_PROGRAMS;

            double current = 0;
            AddTextToLog("Putting device into read mode... ");

            _PIC_Conn.FlushBuffer();
            

            /*
            AddTextToLog("Temporarily disabling RVC and Sleep Timers... ");

            if (_PIC_Conn.sendAckdCommand(command_DisableTimers))
            {
                AddTextToLog("Done." + System.Environment.NewLine);

            }
            else
            {
                AddTextToLog("[ERROR] Unable to disable timers. Save may fail." + System.Environment.NewLine);
            }
            */
            for(int cur_program = 0; cur_program < _parentForm.NUM_PROGRAMS; cur_program++)
            {
                
                byte switch_command = 0x00;

                if (cur_program == 0)
                {
                    switch_command = 0x30;
                }
                else if(cur_program == 1)
                {
                    switch_command = 0x31;
                } else {
                    switch_command = 0x32;
                }

                AddTextToLog("Switching to program " + (cur_program+1) + System.Environment.NewLine);

                if (_parentForm._PIC_Conn.sendAckdCommand(switch_command, 3000))
                {
                    AddTextToLog("Done." + System.Environment.NewLine);

                }
                else
                {
                    AddDebugTextToLog("[ERROR] Unable to switch program" + System.Environment.NewLine);
                    return;
                }
                
                AddTextToLog("Reading values from device... " + System.Environment.NewLine);

            // Input names

            for(int x = 0; x < 4; x++)
            {
                // TODO - make this zero-based
                _parentForm.PROGRAMS[cur_program].inputs[x].Name = _PIC_Conn.ReadChannelName(x + 1); // Must increment by 1
                //AddDebugTextToLog("Read " + _parentForm.PROGRAMS[cur_program].inputs[x].Name + System.Environment.NewLine);
                Thread.Sleep(100);
            }

            // Output Names

            for (int x = 0; x < 4; x++)
            {
                // TODO - make this zero-based
                _parentForm.PROGRAMS[cur_program].outputs[x].Name = _PIC_Conn.ReadChannelName(x + 1, true); // Must increment by 1
                //AddDebugTextToLog("Read " + _parentForm.PROGRAMS[cur_program].outputs[x].Name + System.Environment.NewLine);
                Thread.Sleep(100);
            } 
            
            for (int x = 0; x < 4; x++)
            {
                _parentForm.PROGRAMS[cur_program].inputs[x].PhantomPower = _PIC_Conn.ReadPhantomPower(x);
                //AddDebugTextToLog("Phantom Power on CH" + (x + 1) + " " + _parentForm.PROGRAMS[cur_program].inputs[x].PhantomPower.ToString() + System.Environment.NewLine);
                Thread.Sleep(10);
            }

            UInt32 read_value = 0x00000000;
            int count = 0;

            foreach (DSP_Setting single_setting in _parentForm._settings[cur_program])
            {

                
                if ((single_setting.Index > 39 && single_setting.Index < 220) || (single_setting.Index > 271 && single_setting.Index < 300))
                {
                    AddDebugTextToLog("SKIPPING " + single_setting.Name + System.Environment.NewLine);
                    count++;
                    continue;
                }

                AddDebugTextToLog("Requesting " + single_setting.Name + System.Environment.NewLine);


                //AddDebugTextToLog("Requesting " + single_setting.Name + " ... ");
                rts_status = _PIC_Conn.sendAckdCommand(command_RTS);
                if (rts_status == true)
                {
                    //Thread.Sleep(delay_ms);
                    read_value = _PIC_Conn.Read_DSP_Value(single_setting.Index);
                    _parentForm._settings[cur_program][count].Value = read_value;
                    _parentForm._cached_settings[cur_program][count].Value = read_value;
 
                    
                }
                else
                {
                    //ddDebugTextToLog("No RTS. FAILED." + System.Environment.NewLine);
                    return;
                }


                count++;
                current++;
                worker.ReportProgress(Math.Min(100,(int)((current / total) * 100.0)));
            }

            
  
                
                

            }
            
            AddTextToLog("Done." + System.Environment.NewLine);

            worker.ReportProgress(92);

            /*
            AddTextToLog("Re-enabling RVC and Sleep Timers... ");

            if (_PIC_Conn.sendAckdCommand(command_EnableTimers))
            {
                AddTextToLog("Done." + System.Environment.NewLine);

            }
            else
            {
                AddTextToLog("[ERROR] Unable to re-enable timers. This can be fixed by rebooting the amplifier." + System.Environment.NewLine);
            }

            */

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

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

    }
}
