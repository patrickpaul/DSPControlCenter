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
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
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
                options.Copyright = "© 2013 Stewart Audio, Inc.";
                options.IconFile = @"C:\SOURCE\DSPControlCenter\DSP Control Center v4.ico";
                options.ProductName = "DSP Control Center";
                options.ProductVersion = "2.1.0";
                options.ExtractExistingFile = ExtractExistingFileAction.OverwriteSilently;
                zip.SaveSelfExtractor("Install DSP Control Center.exe", options);
            }
        }
    }
}
