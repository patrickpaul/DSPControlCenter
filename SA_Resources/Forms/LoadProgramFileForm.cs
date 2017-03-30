using System;
using System.ComponentModel;
using System.Windows.Forms;
using SA_Resources.DeviceManagement;

namespace SA_Resources.SAForms
{
    public partial class LoadProgramFileForm : Form
    {
        private MainForm_Template PARENT_FORM;

        private string SCFG_FILE;
        //SCFG_Manager.Read(this.openProgramDialog.FileName, this);
        //        this.UpdateTooltips();
        public LoadProgramFileForm(string inputFile, MainForm_Template _parent)
        {
            InitializeComponent();

            PARENT_FORM = _parent;

            SCFG_FILE = inputFile;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            btnCancel.Enabled = false;
            btnOK.Enabled = false;
            if (radioDisconnect.Checked)
            {
                PARENT_FORM.DeviceConn.Close();
                PARENT_FORM.EndLiveMode();

                SCFG_Manager.Read(SCFG_FILE, PARENT_FORM);
                PARENT_FORM.UpdateTooltips();

                closeTimer.Enabled = true;
            }
            else
            {
                this.progressBar1.Visible = true;
                SCFG_Manager.Read(SCFG_FILE, PARENT_FORM);
                PARENT_FORM.UpdateTooltips();

                BackgroundWorker worker = new BackgroundWorker();
                worker.WorkerReportsProgress = true;
                worker.ProgressChanged += ProgressChanged;

                worker.DoWork += PARENT_FORM.WriteDevice;

                worker.RunWorkerCompleted += WorkComplete;

                worker.RunWorkerAsync();

            }
        }

        private void WorkComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar1.Value = 100;

            int program_index = PARENT_FORM.DeviceConn.GetCurrentProgram();

            PARENT_FORM.ChangeProgram_AfterRead(program_index);

            PARENT_FORM.BeginLiveMode();
            closeTimer.Enabled = true;

        }

        delegate void SetStatusLabelCallback(string text);

        private void SetStatusLabel(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (statusStrip1.InvokeRequired)
            {
                SetStatusLabelCallback d = new SetStatusLabelCallback(SetStatusLabel);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.statusLabel.Text = text;
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

        private void closeTimer_Tick(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
