using System;
using System.ComponentModel;
using System.Drawing;
using System.Media;
using System.Windows.Forms;
using SA_Resources.DSP;
using SA_Resources.DSP.Primitives;
using SA_Resources.SAForms;

namespace SA_Resources
{
    public partial class OutputConfiguration : Form
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

        private bool form_loaded = false;

        private MainForm_Template PARENT_FORM;
        private int CH_NUMBER;

        private double read_gain_value;

        private DSP_Primitive_Output Active_Primitive;


        public OutputConfiguration(MainForm_Template _parentForm, DSP_Primitive_Output in_primitive)
        {
            InitializeComponent();

            try
            {
                PARENT_FORM = _parentForm;
                CH_NUMBER = in_primitive.Channel + 1;


                this.Text = "CH " + CH_NUMBER.ToString("N0") + " - Output Configuration";

                Active_Primitive = in_primitive;

                txtDisplayName.Text = Active_Primitive.OutputName;

                if (_parentForm.LIVE_MODE && _parentForm._PIC_Conn.isOpen && PARENT_FORM.FIRMWARE_VERSION > 2.5)
                {
                    signalTimer.Enabled = true;
                    gainMeterOut.Visible = true;
                    panelRS232.Visible = true;
                }
                else
                {
                    gainMeterOut.Visible = false;
                    panelRS232.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[Exception in OutputConfiguration]: " + ex.Message);
            }
        }

        private void OutputConfiguration_Load(object sender, EventArgs e)
        {
            txtDisplayName.SelectAll();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveRoutine();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            CancelRoutine();
        }

        private void SaveRoutine()
        {
            this.DialogResult = DialogResult.OK;
            this.Close();

        }

        private void CancelRoutine()
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();

        }

        private void txtDisplayName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                e.Handled = true;

                SaveRoutine();
                return;
            }

            string allowedCharacterSet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()_+=.:'/\\- \b\n";
            if (allowedCharacterSet.Contains(e.KeyChar.ToString()))
            {
                // Good!
                return;
            }
            else
            {
                // Skip the car
                SystemSounds.Beep.Play(); 
                e.Handled = true;
            }

        }

        private void OutputConfiguration_FormClosing(object sender, FormClosingEventArgs e)
        {
            Active_Primitive.OutputName = txtDisplayName.Text;
        }

        private void signalTimer_Tick(object sender, EventArgs e)
        {

            if (!PARENT_FORM._PIC_Conn.isOpen || !PARENT_FORM.LIVE_MODE)
            {
                signalTimer.Enabled = false;
                return;
            }

            UInt32 read_value;
            double converted_value;
            double offset = (20 - 20 + 3.8) + 10 * Math.Log10(2) + 20 * Math.Log10(16);
            UInt32 read_address = 0x00000000;


            try
            {
                read_address = PARENT_FORM.DSP_METER_MANAGER.LookupMeter(DSP_Primitive_Types.Output, Active_Primitive.Channel, 0).Address;
            }
            catch (Exception ex)
            {
                Console.WriteLine("[Exception in OutputConfiguration.signalTimer_Tick]: " + ex.Message);
            }

            read_value = PARENT_FORM._PIC_Conn.Read_Live_DSP_Value(read_address);

            converted_value = DSP_Math.MN_to_double_signed(read_value, 1, 31);

            if (converted_value > (0.000001 * 0.000001))
            {
                read_gain_value = offset + 10 * Math.Log10(converted_value);

            }
            else
            {
                read_gain_value = -100;
            }

            gainMeterOut.DB = read_gain_value;
        }

        private void UpdateRS232Stats()
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;

            worker.DoWork += UpdateRS232Stats_DoWork;
            worker.RunWorkerAsync();
        }

        private void UpdateRS232Stats_DoWork(object sender, DoWorkEventArgs doWorkEventArgs)
        {
            bool mute_status = PARENT_FORM._PIC_Conn.ReadRS232Mute(CH_NUMBER);

            if (mute_status == true)
            {

                this.SetMuteLabel("Muted");
                this.SetMuteImage(GlobalResources.ui_btn_blue_unmute);
            }
            else
            {
                this.SetMuteLabel("Unmuted");
                this.SetMuteImage(GlobalResources.ui_btn_blue_mute);
            }

            double cur_volume = PARENT_FORM._PIC_Conn.ReadRS232Vol(CH_NUMBER);

            this.SetVolumeLabel(cur_volume.ToString() + "%");
        }


        #region BackgroundWorker Helpers for UpdateRS232Stats

        delegate void SetMuteImageCallback(Image newImage);

        private void SetMuteImage(Image newImage)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (txtRS232Vol.InvokeRequired)
            {
                SetMuteImageCallback d = new SetMuteImageCallback(SetMuteImage);
                this.Invoke(d, new object[] { newImage });
            }
            else
            {
                this.pbtnMute.Image = newImage;
            }
        }
        
        
        delegate void SetVolumeLabelCallback(string text);

        private void SetVolumeLabel(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (txtRS232Vol.InvokeRequired)
            {
                SetVolumeLabelCallback d = new SetVolumeLabelCallback(SetVolumeLabel);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.txtRS232Vol.Text = text;
            }
        }

        delegate void SetMuteLabelCallback(string text);

        private void SetMuteLabel(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (txtRS232Vol.InvokeRequired)
            {
                SetMuteLabelCallback d = new SetMuteLabelCallback(SetMuteLabel);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.txtRS232Mute.Text = text;
            }
        }

        #endregion


        private void pbtnMute_Click(object sender, EventArgs e)
        {
            
            
            PARENT_FORM._PIC_Conn.SetRS232Mute(CH_NUMBER, 2);

            UpdateRS232Stats();
        }

        private void pbtnReset_Click(object sender, EventArgs e)
        {
            PARENT_FORM._PIC_Conn.ResetRS232Volume(CH_NUMBER);

            UpdateRS232Stats();
        }

        private void OutputConfiguration_Shown(object sender, EventArgs e)
        {
            if(PARENT_FORM.LIVE_MODE)
            {
                UpdateRS232Stats();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (PARENT_FORM.LIVE_MODE)
            {
                UpdateRS232Stats();
            }
        }

    }
}
