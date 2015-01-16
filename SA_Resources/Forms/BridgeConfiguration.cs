using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Windows.Forms;
using SA_Resources;
using SA_Resources.DeviceManagement;
using SA_Resources.DSP;
using SA_Resources.SAForms;
using SA_Resources.DSP.Primitives;

namespace SA_Resources.SAForms
{
    public partial class BridgeConfigurationForm : Form
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



        public BridgeConfigurationForm(MainForm_Template _parentForm)
        {
            InitializeComponent();

            PARENT_FORM = _parentForm;

            this.Text = "Bridge Configuration";

            
        }

        private void BridgeConfigurationForm_Load(object sender, EventArgs e)
        {
            dropBridgeMode.SelectedIndex = PARENT_FORM.AmplifierMode;

            form_loaded = true;
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

        private void dropBridgeMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!form_loaded)
            {
                return;
            }

            switch (dropBridgeMode.SelectedIndex)
            {
                case 0 :
                    PARENT_FORM.AmplifierBridgeMode = BridgeMode.FourChannel;
                    break;

                case 1 :
                    PARENT_FORM.AmplifierBridgeMode = BridgeMode.TwoChannel;
                    break;

                case 2:
                    PARENT_FORM.AmplifierBridgeMode = BridgeMode.TwoOneChannel;
                    break;
            }

            PARENT_FORM.AmplifierMode = dropBridgeMode.SelectedIndex;
            PARENT_FORM.DeviceConn.SetAmplifierMode(dropBridgeMode.SelectedIndex);
            PARENT_FORM.DeviceConn.SoftReboot();
        }
    }
}
