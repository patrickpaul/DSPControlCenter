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
        private byte SWITCH_COMMAND;
        public SwitchProgramForm(MainForm_Template _parentForm, byte switchCommand)
        {
            InitializeComponent();

            PARENT_FORM = _parentForm;
            SWITCH_COMMAND = switchCommand;

            
        }

        private void SwitchProgramForm_Shown(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;

            if (PARENT_FORM._PIC_Conn.getRTS())
            {
                if (PARENT_FORM._PIC_Conn.sendAckdCommand(SWITCH_COMMAND, 3000))
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
