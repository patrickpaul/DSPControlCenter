using System;
using System.Windows.Forms;

namespace SA_Resources
{
    public partial class AboutForm : Form
    {
        public AboutForm(String formTitle, String longProductName, Version currentVersion, String versionSuffix)
        {
            InitializeComponent();

            this.Text = formTitle;
            lblProductTitle.Text = longProductName;

            lblVersion.Text = "Version " + currentVersion.Major + "." + currentVersion.Minor + " " + versionSuffix;

            //pictureBox1.Image = Images.Company_Logo;
        }

        private void BtnAboutOk_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
