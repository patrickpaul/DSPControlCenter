﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SA_Resources.SAForms
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
            this.saveConfigurationAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitApplicationToolStripMenuItem = new System.Windows.Forms.ToolStripSeparator();
            this.resetToDefaultToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deviceToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.presetManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.connectToDeviceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.readFromDeviceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.viewHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutDSPControlCenterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serialPort = new System.IO.Ports.SerialPort(this.components);
            this.pictureConnectionStatus = new System.Windows.Forms.PictureBox();
            this.btnConnectToDevice = new System.Windows.Forms.Button();
            this.dropProgramSelection = new System.Windows.Forms.ComboBox();
            this.pbPresetSelection = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.HeartbeatTimer = new System.Windows.Forms.Timer(this.components);
            this.menuBlockCopy = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuItem_Cut = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_Copy = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_Paste = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.chkDebugLiveMode = new System.Windows.Forms.CheckBox();
            this.btnDebugShowMeters = new System.Windows.Forms.Button();
            this.pbtnSettings = new SA_Resources.SAControls.PictureButton();
            this.pbtn_Meters = new SA_Resources.SAControls.PictureButton();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureConnectionStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPresetSelection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.menuBlockCopy.SuspendLayout();
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
            this.saveConfigurationAsToolStripMenuItem,
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
            this.openConfigurationToolStripMenuItem.Size = new System.Drawing.Size(263, 22);
            this.openConfigurationToolStripMenuItem.Text = "Open Configuration";
            this.openConfigurationToolStripMenuItem.Click += new System.EventHandler(this.ReadSCFG_Event);
            // 
            // saveConfigurationToolStripMenuItem
            // 
            this.saveConfigurationToolStripMenuItem.Name = "saveConfigurationToolStripMenuItem";
            this.saveConfigurationToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveConfigurationToolStripMenuItem.Size = new System.Drawing.Size(263, 22);
            this.saveConfigurationToolStripMenuItem.Text = "Save Configuration";
            this.saveConfigurationToolStripMenuItem.Click += new System.EventHandler(this.WriteSCFG_Event);
            // 
            // saveConfigurationAsToolStripMenuItem
            // 
            this.saveConfigurationAsToolStripMenuItem.Name = "saveConfigurationAsToolStripMenuItem";
            this.saveConfigurationAsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.S)));
            this.saveConfigurationAsToolStripMenuItem.Size = new System.Drawing.Size(263, 22);
            this.saveConfigurationAsToolStripMenuItem.Text = "Save Configuration As...";
            this.saveConfigurationAsToolStripMenuItem.Visible = false;
            this.saveConfigurationAsToolStripMenuItem.Click += new System.EventHandler(this.WriteSCFG_As_Event);
            // 
            // exitApplicationToolStripMenuItem
            // 
            this.exitApplicationToolStripMenuItem.Name = "exitApplicationToolStripMenuItem";
            this.exitApplicationToolStripMenuItem.Size = new System.Drawing.Size(260, 6);
            // 
            // resetToDefaultToolStripMenuItem
            // 
            this.resetToDefaultToolStripMenuItem.Name = "resetToDefaultToolStripMenuItem";
            this.resetToDefaultToolStripMenuItem.Size = new System.Drawing.Size(263, 22);
            this.resetToDefaultToolStripMenuItem.Text = "Restore Default Settings";
            this.resetToDefaultToolStripMenuItem.Click += new System.EventHandler(this.ResetInterface_Event);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(260, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(263, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.Close_Event);
            // 
            // deviceToolStripMenuItem1
            // 
            this.deviceToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.presetManagerToolStripMenuItem,
            this.toolStripMenuItem3,
            this.connectToDeviceToolStripMenuItem,
            this.readFromDeviceToolStripMenuItem});
            this.deviceToolStripMenuItem1.ForeColor = System.Drawing.Color.Gainsboro;
            this.deviceToolStripMenuItem1.Name = "deviceToolStripMenuItem1";
            this.deviceToolStripMenuItem1.Size = new System.Drawing.Size(48, 20);
            this.deviceToolStripMenuItem1.Text = "Tools";
            // 
            // presetManagerToolStripMenuItem
            // 
            this.presetManagerToolStripMenuItem.Name = "presetManagerToolStripMenuItem";
            this.presetManagerToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.presetManagerToolStripMenuItem.Text = "Preset Manager";
            this.presetManagerToolStripMenuItem.Visible = false;
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(211, 6);
            this.toolStripMenuItem3.Visible = false;
            // 
            // connectToDeviceToolStripMenuItem
            // 
            this.connectToDeviceToolStripMenuItem.Name = "connectToDeviceToolStripMenuItem";
            this.connectToDeviceToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.connectToDeviceToolStripMenuItem.Text = "Open Device Manager";
            this.connectToDeviceToolStripMenuItem.Click += new System.EventHandler(this.Connect_Event);
            // 
            // readFromDeviceToolStripMenuItem
            // 
            this.readFromDeviceToolStripMenuItem.Enabled = false;
            this.readFromDeviceToolStripMenuItem.Name = "readFromDeviceToolStripMenuItem";
            this.readFromDeviceToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.readFromDeviceToolStripMenuItem.Text = "Restore to Factory Settings";
            this.readFromDeviceToolStripMenuItem.Visible = false;
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
            this.viewHelpToolStripMenuItem.Text = "View Users Manual";
            this.viewHelpToolStripMenuItem.Click += new System.EventHandler(this.viewHelpToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(209, 6);
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
            this.pictureConnectionStatus.Location = new System.Drawing.Point(331, 24);
            this.pictureConnectionStatus.Name = "pictureConnectionStatus";
            this.pictureConnectionStatus.Size = new System.Drawing.Size(146, 37);
            this.pictureConnectionStatus.TabIndex = 85;
            this.pictureConnectionStatus.TabStop = false;
            // 
            // btnConnectToDevice
            // 
            this.btnConnectToDevice.BackColor = System.Drawing.Color.Transparent;
            this.btnConnectToDevice.Location = new System.Drawing.Point(492, 30);
            this.btnConnectToDevice.Name = "btnConnectToDevice";
            this.btnConnectToDevice.Size = new System.Drawing.Size(84, 23);
            this.btnConnectToDevice.TabIndex = 84;
            this.btnConnectToDevice.Text = "Connect";
            this.btnConnectToDevice.UseVisualStyleBackColor = false;
            // 
            // dropProgramSelection
            // 
            this.dropProgramSelection.Items.AddRange(new object[] {
            "Preset 1",
            "Preset 2",
            "Preset 3",
            "Preset 4",
            "Preset 5",
            "Preset 6",
            "Preset 7",
            "Preset 8",
            "Preset 9",
            "Preset 10",
            "---",
            "Manage Presets"});
            this.dropProgramSelection.Location = new System.Drawing.Point(729, 32);
            this.dropProgramSelection.Name = "dropProgramSelection";
            this.dropProgramSelection.Size = new System.Drawing.Size(165, 21);
            this.dropProgramSelection.TabIndex = 86;
            // 
            // pbPresetSelection
            // 
            this.pbPresetSelection.Image = ((System.Drawing.Image)(resources.GetObject("pbPresetSelection.Image")));
            this.pbPresetSelection.Location = new System.Drawing.Point(568, 24);
            this.pbPresetSelection.Name = "pbPresetSelection";
            this.pbPresetSelection.Size = new System.Drawing.Size(155, 37);
            this.pbPresetSelection.TabIndex = 82;
            this.pbPresetSelection.TabStop = false;
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
            // menuBlockCopy
            // 
            this.menuBlockCopy.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem_Cut,
            this.menuItem_Copy,
            this.menuItem_Paste,
            this.toolStripSeparator1,
            this.toolStripMenuItem6});
            this.menuBlockCopy.Name = "contextMenuStrip1";
            this.menuBlockCopy.Size = new System.Drawing.Size(179, 98);
            // 
            // menuItem_Cut
            // 
            this.menuItem_Cut.Image = ((System.Drawing.Image)(resources.GetObject("menuItem_Cut.Image")));
            this.menuItem_Cut.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuItem_Cut.Name = "menuItem_Cut";
            this.menuItem_Cut.Size = new System.Drawing.Size(178, 22);
            this.menuItem_Cut.Text = "Cu&t";
            this.menuItem_Cut.Visible = false;
            // 
            // menuItem_Copy
            // 
            this.menuItem_Copy.Image = ((System.Drawing.Image)(resources.GetObject("menuItem_Copy.Image")));
            this.menuItem_Copy.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuItem_Copy.Name = "menuItem_Copy";
            this.menuItem_Copy.Size = new System.Drawing.Size(178, 22);
            this.menuItem_Copy.Text = "&Copy";
            // 
            // menuItem_Paste
            // 
            this.menuItem_Paste.Image = ((System.Drawing.Image)(resources.GetObject("menuItem_Paste.Image")));
            this.menuItem_Paste.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.menuItem_Paste.Name = "menuItem_Paste";
            this.menuItem_Paste.Size = new System.Drawing.Size(178, 22);
            this.menuItem_Paste.Text = "&Paste";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(175, 6);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem6.Image")));
            this.toolStripMenuItem6.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(178, 22);
            this.toolStripMenuItem6.Text = "Clear Configuration";
            // 
            // chkDebugLiveMode
            // 
            this.chkDebugLiveMode.AutoSize = true;
            this.chkDebugLiveMode.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.chkDebugLiveMode.ForeColor = System.Drawing.Color.Gainsboro;
            this.chkDebugLiveMode.Location = new System.Drawing.Point(157, 349);
            this.chkDebugLiveMode.Name = "chkDebugLiveMode";
            this.chkDebugLiveMode.Size = new System.Drawing.Size(116, 17);
            this.chkDebugLiveMode.TabIndex = 102;
            this.chkDebugLiveMode.Text = "Debug Live Mode";
            this.chkDebugLiveMode.UseVisualStyleBackColor = true;
            this.chkDebugLiveMode.Visible = false;
            this.chkDebugLiveMode.CheckedChanged += new System.EventHandler(this.chkDebugLiveMode_CheckedChanged);
            // 
            // btnDebugShowMeters
            // 
            this.btnDebugShowMeters.Location = new System.Drawing.Point(290, 343);
            this.btnDebugShowMeters.Name = "btnDebugShowMeters";
            this.btnDebugShowMeters.Size = new System.Drawing.Size(90, 23);
            this.btnDebugShowMeters.TabIndex = 103;
            this.btnDebugShowMeters.Text = "Show Meters";
            this.btnDebugShowMeters.UseVisualStyleBackColor = true;
            this.btnDebugShowMeters.Visible = false;
            this.btnDebugShowMeters.Click += new System.EventHandler(this.btnDebugShowMeters_Click);
            // 
            // pbtnSettings
            // 
            this.pbtnSettings.AutoResize = true;
            this.pbtnSettings.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pbtnSettings.BackgroundImage")));
            this.pbtnSettings.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbtnSettings.Location = new System.Drawing.Point(74, 344);
            this.pbtnSettings.Name = "pbtnSettings";
            this.pbtnSettings.OverImage = null;
            this.pbtnSettings.Overlay1Image = null;
            this.pbtnSettings.Overlay1Visible = false;
            this.pbtnSettings.Overlay2Image = null;
            this.pbtnSettings.Overlay2Visible = false;
            this.pbtnSettings.Overlay3Image = null;
            this.pbtnSettings.Overlay3Visible = false;
            this.pbtnSettings.PressedImage = null;
            this.pbtnSettings.Size = new System.Drawing.Size(63, 23);
            this.pbtnSettings.TabIndex = 101;
            this.pbtnSettings.ToolTipText = "";
            this.pbtnSettings.Visible = false;
            this.pbtnSettings.Click += new System.EventHandler(this.pbtnSettings_Click);
            // 
            // pbtn_Meters
            // 
            this.pbtn_Meters.AutoResize = true;
            this.pbtn_Meters.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pbtn_Meters.BackgroundImage")));
            this.pbtn_Meters.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbtn_Meters.Location = new System.Drawing.Point(13, 344);
            this.pbtn_Meters.Name = "pbtn_Meters";
            this.pbtn_Meters.OverImage = null;
            this.pbtn_Meters.Overlay1Image = null;
            this.pbtn_Meters.Overlay1Visible = false;
            this.pbtn_Meters.Overlay2Image = null;
            this.pbtn_Meters.Overlay2Visible = false;
            this.pbtn_Meters.Overlay3Image = null;
            this.pbtn_Meters.Overlay3Visible = false;
            this.pbtn_Meters.PressedImage = null;
            this.pbtn_Meters.Size = new System.Drawing.Size(55, 23);
            this.pbtn_Meters.TabIndex = 87;
            this.pbtn_Meters.ToolTipText = "";
            this.pbtn_Meters.Visible = false;
            this.pbtn_Meters.Click += new System.EventHandler(this.pbtn_Meters_Click);
            // 
            // MainForm_Template
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(82)))), ((int)(((byte)(82)))));
            this.ClientSize = new System.Drawing.Size(906, 394);
            this.Controls.Add(this.btnDebugShowMeters);
            this.Controls.Add(this.chkDebugLiveMode);
            this.Controls.Add(this.pbtnSettings);
            this.Controls.Add(this.pbtn_Meters);
            this.Controls.Add(this.pictureConnectionStatus);
            this.Controls.Add(this.btnConnectToDevice);
            this.Controls.Add(this.dropProgramSelection);
            this.Controls.Add(this.pbPresetSelection);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm_Template";
            this.Text = "MainForm_Template";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_Template_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Template_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureConnectionStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPresetSelection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.menuBlockCopy.ResumeLayout(false);
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
        protected System.IO.Ports.SerialPort serialPort;
        protected PictureBox pictureConnectionStatus;
        protected Button btnConnectToDevice;
        protected ComboBox dropProgramSelection;
        protected PictureBox pbPresetSelection;
        private PictureBox pictureBox1;
        private ToolStripMenuItem resetToDefaultToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem1;
        private Timer HeartbeatTimer;
        private ContextMenuStrip menuBlockCopy;
        private ToolStripMenuItem menuItem_Cut;
        private ToolStripMenuItem menuItem_Copy;
        private ToolStripMenuItem menuItem_Paste;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem toolStripMenuItem6;
        private ToolStripMenuItem presetManagerToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem3;
        private SAControls.PictureButton pbtn_Meters;
        private SAControls.PictureButton pbtnSettings;
        private ToolStripMenuItem saveConfigurationAsToolStripMenuItem;
        private CheckBox chkDebugLiveMode;
        private Button btnDebugShowMeters;
    }
}