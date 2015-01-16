using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SA_Resources.SAForms
{
    public partial class RestoreSettingsForm : Form
    {

        private MainForm_Template PARENT_FORM;

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | 0x200;
                return myCp;
            }
        }
        
        
        public RestoreSettingsForm(MainForm_Template _parentForm)
        {
            InitializeComponent();

            PARENT_FORM = _parentForm;

            
        }

        private void SwitchProgramForm_Shown(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            progressBar1.Visible = true;


            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.ProgressChanged += ProgressChanged;

            //worker.DoWork += PARENT_FORM.DeviceConn.RestoreFactorySettings;
             
            worker.RunWorkerCompleted += WorkComplete;

            worker.RunWorkerAsync();
        }

        private void WorkComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar1.Value = 100;

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
            progressBar1.Value = e.ProgressPercentage;
            
            if (e.UserState != null)
            {
                this.SetStatusLabel(e.UserState.ToString());
            }
        }

        private void closeTimer_Tick(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
