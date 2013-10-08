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
            this.Queue_Thread = new System.ComponentModel.BackgroundWorker();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.openProgramDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveProgramDialog = new System.Windows.Forms.SaveFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mbtnOpenConfiguration = new System.Windows.Forms.ToolStripMenuItem();
            this.openConfigurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveConfigurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitApplicationToolStripMenuItem = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deviceToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToDeviceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToDeviceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.readFromDeviceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.viewHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutDSPControlCenterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
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
            this.connectToDeviceToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.connectToDeviceToolStripMenuItem.Text = "Connect to Device";
            this.connectToDeviceToolStripMenuItem.Visible = false;
            this.connectToDeviceToolStripMenuItem.Click += new System.EventHandler(this.Connect_Event);
            // 
            // saveToDeviceToolStripMenuItem
            // 
            this.saveToDeviceToolStripMenuItem.Name = "saveToDeviceToolStripMenuItem";
            this.saveToDeviceToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.saveToDeviceToolStripMenuItem.Text = "Save to Device";
            this.saveToDeviceToolStripMenuItem.Click += new System.EventHandler(this.WriteDevice_Event);
            // 
            // readFromDeviceToolStripMenuItem
            // 
            this.readFromDeviceToolStripMenuItem.Name = "readFromDeviceToolStripMenuItem";
            this.readFromDeviceToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.readFromDeviceToolStripMenuItem.Text = "Read From Device";
            this.readFromDeviceToolStripMenuItem.Click += new System.EventHandler(this.ReadDevice_Event);
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
            // MainForm_Template
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(82)))), ((int)(((byte)(82)))));
            this.ClientSize = new System.Drawing.Size(906, 394);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.Name = "MainForm_Template";
            this.Text = "MainForm_Template";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_Template_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
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
        private ToolStripMenuItem saveToDeviceToolStripMenuItem;
        private ToolStripMenuItem readFromDeviceToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem1;
        private ToolStripMenuItem viewHelpToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem2;
        private ToolStripMenuItem aboutDSPControlCenterToolStripMenuItem;
        protected MenuStrip menuStrip1;
    }
}