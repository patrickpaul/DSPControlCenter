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
    public partial class SwitchProgramForm : Form
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


        private MainForm_Template PARENT_FORM;
        private int NEW_PROGRAM_INDEX;
        public SwitchProgramForm(MainForm_Template _parentForm, int new_program)
        {
            InitializeComponent();

            PARENT_FORM = _parentForm;
            NEW_PROGRAM_INDEX = new_program;

            
        }

        private void SwitchProgramForm_Shown(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;

            if (PARENT_FORM.DeviceConn.getRTS())
            {
                if (PARENT_FORM.DeviceConn.SwitchActiveProgram(NEW_PROGRAM_INDEX))
                {
                    this.DialogResult = DialogResult.OK;


                }
                else
                {
                    this.DialogResult = DialogResult.No;
                }
            }
            else
            {
                this.DialogResult = DialogResult.Abort;
            }

            this.Close();
        }
    }
}
