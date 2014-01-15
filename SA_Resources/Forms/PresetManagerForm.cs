using System;
using System.Windows.Forms;

namespace SA_Resources.Forms
{

    public partial class PresetManager : Form
    {

        private MainForm_Template PARENT_FORM;
        private bool form_loaded = false;

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | 0x200;
                return myCp;
            }
        }

        public PresetManager(MainForm_Template _parent)
        {
            InitializeComponent();

            dropProgramSelection.Items.Clear();
            dropProgramSelection.Text = "";

            for(int i = 0; i < _parent.NumPresets(); i++)
            {
                dropProgramSelection.Items.Add(_parent._presetNames[i]);
            }

            dropProgramSelection.SelectedIndex = 0;
            dropProgramSelection.Invalidate();

            PARENT_FORM = _parent;
            form_loaded = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dropProgramSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(!form_loaded)
            {
                return;
            }

            
            txtPresetName.Text = PARENT_FORM._presetNames[dropProgramSelection.SelectedIndex];
        }

        private void dropProgramSelection_SelectedValueChanged(object sender, EventArgs e)
        {
            
        }

    }
}
