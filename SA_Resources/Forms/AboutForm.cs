using System;
using System.Windows.Forms;

using System.Reflection;
using System.Diagnostics;

namespace SA_Resources.SAForms
{
    public partial class AboutForm : Form
    {
        public AboutForm(String formTitle, String longProductName, Version currentVersion, String versionSuffix)
        {
            InitializeComponent();

            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fileVersionInfo.ProductVersion.Substring(0,5);


            this.Text = formTitle;
            lblProductTitle.Text = longProductName;

            lblVersion.Text = "Version " + version + " " + versionSuffix;

            //pictureBox1.Image = Images.Company_Logo;
        }

        private void BtnAboutOk_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
