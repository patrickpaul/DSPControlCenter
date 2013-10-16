using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SA_Resources.Forms
{
    partial class MainForm_Template
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm_Template));
            this.Queue_Thread = new System.ComponentModel.BackgroundWorker();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.openProgramDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveProgramDialog = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mbtnOpenConfiguration = new System.Windows.Forms.ToolStripMenuItem();
            this.openConfigurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveConfigurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitApplicationToolStripMenuItem = new System.Windows.Forms.ToolStripSeparator();
            this.resetToDefaultToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deviceToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToDeviceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToDeviceToolStripMenuItem = new System.Windows.Forms.ToolStripSeparator();
            this.readFromDeviceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.viewHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutDSPControlCenterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.pictureConnectionStatus = new System.Windows.Forms.PictureBox();
            this.btnConnectToDevice = new System.Windows.Forms.Button();
            this.dropProgramSelection = new System.Windows.Forms.ComboBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.HeartbeatTimer = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureConnectionStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // Queue_Thread
            // 
            this.Queue_Thread.WorkerSupportsCancellation = true;
            this.Queue_Thread.DoWork += new System.ComponentModel.DoWorkEventHandler(this.Queue_Thread_DoWork);
            this.Queue_Thread.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.Queue_Thread_RunWorkerCompleted);
            // 
            // openProgramDialog
            // 
            this.openProgramDialog.Filter = "Configuration Files (*.scfg)|*.scfg";
            this.openProgramDialog.Title = "Open Configuration File";
            // 
            // saveProgramDialog
            // 
            this.saveProgramDialog.Filter = "Configuration Files (*.scfg)|*.scfg";
            this.saveProgramDialog.Title = "Save Configuration File";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mbtnOpenConfiguration,
            this.deviceToolStripMenuItem1,
            this.editToolStripMenuItem,
            this.helpToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuStrip1.Size = new System.Drawing.Size(906, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mbtnOpenConfiguration
            // 
            this.mbtnOpenConfiguration.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openConfigurationToolStripMenuItem,
            this.saveConfigurationToolStripMenuItem,
            this.exitApplicationToolStripMenuItem,
            this.resetToDefaultToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.mbtnOpenConfiguration.ForeColor = System.Drawing.Color.Gainsboro;
            this.mbtnOpenConfiguration.Name = "mbtnOpenConfiguration";
            this.mbtnOpenConfiguration.Size = new System.Drawing.Size(37, 20);
            this.mbtnOpenConfiguration.Text = "File";
            // 
            // openConfigurationToolStripMenuItem
            // 
            this.openConfigurationToolStripMenuItem.Name = "openConfigurationToolStripMenuItem";
            this.openConfigurationToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openConfigurationToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.openConfigurationToolStripMenuItem.Text = "Open Configuration";
            this.openConfigurationToolStripMenuItem.Click += new System.EventHandler(this.ReadSCFG_Event);
            // 
            // saveConfigurationToolStripMenuItem
            // 
            this.saveConfigurationToolStripMenuItem.Name = "saveConfigurationToolStripMenuItem";
            this.saveConfigurationToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveConfigurationToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.saveConfigurationToolStripMenuItem.Text = "Save Configuration";
            this.saveConfigurationToolStripMenuItem.Click += new System.EventHandler(this.WriteSCFG_Event);
            // 
            // exitApplicationToolStripMenuItem
            // 
            this.exitApplicationToolStripMenuItem.Name = "exitApplicationToolStripMenuItem";
            this.exitApplicationToolStripMenuItem.Size = new System.Drawing.Size(220, 6);
            // 
            // resetToDefaultToolStripMenuItem
            // 
            this.resetToDefaultToolStripMenuItem.Name = "resetToDefaultToolStripMenuItem";
            this.resetToDefaultToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.resetToDefaultToolStripMenuItem.Text = "Restore Default Settings";
            this.resetToDefaultToolStripMenuItem.Click += new System.EventHandler(this.ResetInterface_Event);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(220, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.Close_Event);
            // 
            // deviceToolStripMenuItem1
            // 
            this.deviceToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectToDeviceToolStripMenuItem,
            this.saveToDeviceToolStripMenuItem,
            this.readFromDeviceToolStripMenuItem});
            this.deviceToolStripMenuItem1.ForeColor = System.Drawing.Color.Gainsboro;
            this.deviceToolStripMenuItem1.Name = "deviceToolStripMenuItem1";
            this.deviceToolStripMenuItem1.Size = new System.Drawing.Size(54, 20);
            this.deviceToolStripMenuItem1.Text = "Device";
            // 
            // connectToDeviceToolStripMenuItem
            // 
            this.connectToDeviceToolStripMenuItem.Name = "connectToDeviceToolStripMenuItem";
            this.connectToDeviceToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.connectToDeviceToolStripMenuItem.Text = "Open Device Manager";
            this.connectToDeviceToolStripMenuItem.Click += new System.EventHandler(this.Connect_Event);
            // 
            // saveToDeviceToolStripMenuItem
            // 
            this.saveToDeviceToolStripMenuItem.Name = "saveToDeviceToolStripMenuItem";
            this.saveToDeviceToolStripMenuItem.Size = new System.Drawing.Size(211, 6);
            this.saveToDeviceToolStripMenuItem.Click += new System.EventHandler(this.WriteDevice_Event);
            // 
            // readFromDeviceToolStripMenuItem
            // 
            this.readFromDeviceToolStripMenuItem.Enabled = false;
            this.readFromDeviceToolStripMenuItem.Name = "readFromDeviceToolStripMenuItem";
            this.readFromDeviceToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.readFromDeviceToolStripMenuItem.Text = "Restore to Factory Settings";
            this.readFromDeviceToolStripMenuItem.Click += new System.EventHandler(this.FactoryReset_Event);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Enabled = false;
            this.editToolStripMenuItem.ForeColor = System.Drawing.Color.Gainsboro;
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Visible = false;
            // 
            // helpToolStripMenuItem1
            // 
            this.helpToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewHelpToolStripMenuItem,
            this.toolStripMenuItem2,
            this.aboutDSPControlCenterToolStripMenuItem});
            this.helpToolStripMenuItem1.ForeColor = System.Drawing.Color.Gainsboro;
            this.helpToolStripMenuItem1.Name = "helpToolStripMenuItem1";
            this.helpToolStripMenuItem1.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem1.Text = "Help";
            // 
            // viewHelpToolStripMenuItem
            // 
            this.viewHelpToolStripMenuItem.Name = "viewHelpToolStripMenuItem";
            this.viewHelpToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.viewHelpToolStripMenuItem.Text = "View Help";
            this.viewHelpToolStripMenuItem.Visible = false;
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(209, 6);
            this.toolStripMenuItem2.Visible = false;
            // 
            // aboutDSPControlCenterToolStripMenuItem
            // 
            this.aboutDSPControlCenterToolStripMenuItem.BackColor = System.Drawing.SystemColors.Control;
            this.aboutDSPControlCenterToolStripMenuItem.Name = "aboutDSPControlCenterToolStripMenuItem";
            this.aboutDSPControlCenterToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.aboutDSPControlCenterToolStripMenuItem.Text = "About DSP Control Center";
            this.aboutDSPControlCenterToolStripMenuItem.Click += new System.EventHandler(this.About_Event);
            // 
            // pictureConnectionStatus
            // 
            this.pictureConnectionStatus.Location = new System.Drawing.Point(350, 24);
            this.pictureConnectionStatus.Name = "pictureConnectionStatus";
            this.pictureConnectionStatus.Size = new System.Drawing.Size(146, 37);
            this.pictureConnectionStatus.TabIndex = 85;
            this.pictureConnectionStatus.TabStop = false;
            // 
            // btnConnectToDevice
            // 
            this.btnConnectToDevice.BackColor = System.Drawing.Color.Transparent;
            this.btnConnectToDevice.Location = new System.Drawing.Point(502, 29);
            this.btnConnectToDevice.Name = "btnConnectToDevice";
            this.btnConnectToDevice.Size = new System.Drawing.Size(84, 23);
            this.btnConnectToDevice.TabIndex = 84;
            this.btnConnectToDevice.Text = "Connect";
            this.btnConnectToDevice.UseVisualStyleBackColor = false;
            // 
            // dropProgramSelection
            // 
            this.dropProgramSelection.FormattingEnabled = true;
            this.dropProgramSelection.Items.AddRange(new object[] {
            "Program 1 (Default)",
            "Program 2",
            "Program 3"});
            this.dropProgramSelection.Location = new System.Drawing.Point(763, 29);
            this.dropProgramSelection.Name = "dropProgramSelection";
            this.dropProgramSelection.Size = new System.Drawing.Size(131, 21);
            this.dropProgramSelection.TabIndex = 83;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(598, 24);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(155, 37);
            this.pictureBox2.TabIndex = 82;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Location = new System.Drawing.Point(0, 24);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(906, 37);
            this.pictureBox1.TabIndex = 86;
            this.pictureBox1.TabStop = false;
            // 
            // HeartbeatTimer
            // 
            this.HeartbeatTimer.Interval = 10000;
            // 
            // MainForm_Template
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(82)))), ((int)(((byte)(82)))));
            this.ClientSize = new System.Drawing.Size(906, 394);
            this.Controls.Add(this.pictureConnectionStatus);
            this.Controls.Add(this.btnConnectToDevice);
            this.Controls.Add(this.dropProgramSelection);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.Name = "MainForm_Template";
            this.Text = "MainForm_Template";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_Template_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Template_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureConnectionStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private BackgroundWorker Queue_Thread;
        protected ToolTip toolTip1;
        private OpenFileDialog openProgramDialog;
        private SaveFileDialog saveProgramDialog;
        private ToolStripMenuItem mbtnOpenConfiguration;
        private ToolStripMenuItem openConfigurationToolStripMenuItem;
        private ToolStripMenuItem saveConfigurationToolStripMenuItem;
        private ToolStripSeparator exitApplicationToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem deviceToolStripMenuItem1;
        private ToolStripMenuItem connectToDeviceToolStripMenuItem;
        private ToolStripMenuItem readFromDeviceToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem1;
        private ToolStripMenuItem viewHelpToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem2;
        private ToolStripMenuItem aboutDSPControlCenterToolStripMenuItem;
        protected MenuStrip menuStrip1;
        protected System.IO.Ports.SerialPort serialPort1;
        protected PictureBox pictureConnectionStatus;
        protected Button btnConnectToDevice;
        protected ComboBox dropProgramSelection;
        protected PictureBox pictureBox2;
        private PictureBox pictureBox1;
        private ToolStripMenuItem resetToDefaultToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripSeparator saveToDeviceToolStripMenuItem;
        private Timer HeartbeatTimer;
    }
}