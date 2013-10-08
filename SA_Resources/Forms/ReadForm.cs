using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;
using SA_Resources;
using SA_Resources.Forms;

namespace SA_Resources
{
    public partial class ReadForm : Form
    {
        private MainForm_Template PARENT_FORM;

        public ReadForm(MainForm_Template _parentForm)
        {
            InitializeComponent();
             
            PARENT_FORM = _parentForm;
        }

        private void ReadForm_Load(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            progressBar1.Visible = true;


            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.ProgressChanged += ProgressChanged;

            worker.DoWork += PARENT_FORM.ReadDevice;
            worker.RunWorkerCompleted += WorkComplete;

            worker.RunWorkerAsync();
        }


        private void WorkComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar1.Value = 100;

            PARENT_FORM.LoadSettingsToProgramConfig();
            PARENT_FORM.UpdateTooltips();

            PARENT_FORM.BeginLiveMode();

            closeTimer.Enabled = true;

        }

        delegate void SetStatusLabelCallback(string text);

        private void SetStatusLabel(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (lblStatus.InvokeRequired)
            {
                SetStatusLabelCallback d = new SetStatusLabelCallback(SetStatusLabel);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.lblStatus.Text = text;
            }
        }

        private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage > 0)
            {
                progressBar1.Value = e.ProgressPercentage;
            }
            else
            {
                if (e.UserState != null)
                {
                    this.SetStatusLabel(e.UserState.ToString());
                }
            }
        }


        private void closeTimer_Tick_1(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
