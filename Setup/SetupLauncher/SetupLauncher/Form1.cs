using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Ionic.Zip;
using Ionic.Zlib;
namespace SetupLauncher
{
    public partial class Form1 : Form
    {
        private bool created = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (!created)
            {
                string DirectoryPath = @"C:\SOURCE\DSPControlCenter\Setup\Setup\Express\SingleImage\DiskImages\DISK1";
                using (ZipFile zip = new ZipFile())
                {
                    zip.AddDirectory(DirectoryPath, System.IO.Path.GetFileName(DirectoryPath));
                    zip.Comment = "This will be embedded into a self-extracting console-based exe";
                    SelfExtractorSaveOptions options = new SelfExtractorSaveOptions();
                    options.Flavor = SelfExtractorFlavor.ConsoleApplication;
                    options.Quiet = true;
                    options.DefaultExtractDirectory = "%TEMP%\\DSPCC";
                    options.PostExtractCommandLine = "%TEMP%\\DSPCC\\DISK1\\setup.exe";
                    options.RemoveUnpackedFilesAfterExecute = true;
                    options.FileVersion = new Version(2, 1, 0);
                    options.Description = "DSP Control Center Installer Package";
                    options.Copyright = "© 2015 Stewart Audio, Inc.";
                    options.IconFile = @"C:\SOURCE\DSPControlCenter\DSP Control Center v4.ico";
                    options.ProductName = "DSP Control Center";
                    options.ProductVersion = "3.5.1";
                    options.ExtractExistingFile = ExtractExistingFileAction.OverwriteSilently;
                    zip.SaveSelfExtractor(txtFilename.Text + " v" + txtVersion.Text + ".exe", options);

                    button1.Text = "Open Folder";

                    created = true;
                }
            } else
            {
                string myPath = @"C:\SOURCE\DSPControlCenter\Setup\SetupLauncher\SetupLauncher\bin\Release";
                System.Diagnostics.Process prc = new System.Diagnostics.Process();
                prc.StartInfo.FileName = myPath;
                prc.Start();
            }
        }
    }
}
