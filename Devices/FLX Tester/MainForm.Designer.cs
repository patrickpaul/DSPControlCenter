namespace FLX_Tester
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.textLog = new System.Windows.Forms.TextBox();
            this.queueTimer = new System.Windows.Forms.Timer(this.components);
            this.btnClear = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.resetConfigurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.deviceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.programToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.readToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.commPortToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.contentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.indexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openProgramDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveProgramDialog = new System.Windows.Forms.SaveFileDialog();
            this.tabMain = new System.Windows.Forms.TabPage();
            this.label13 = new System.Windows.Forms.Label();
            this.dropAmpMode = new System.Windows.Forms.ComboBox();
            this.grpSine = new System.Windows.Forms.GroupBox();
            this.TextPinkGain = new System.Windows.Forms.TextBox();
            this.DialPinkGain = new System.Windows.Forms.PictureBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.TextSineGain = new System.Windows.Forms.TextBox();
            this.DialSineGain = new System.Windows.Forms.PictureBox();
            this.TextSineFreq = new System.Windows.Forms.TextBox();
            this.DialSineFreq = new System.Windows.Forms.PictureBox();
            this.grpGain = new System.Windows.Forms.GroupBox();
            this.lblCH1 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.DialGain1 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.TextGain1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.DialGain2 = new System.Windows.Forms.PictureBox();
            this.TextGain2 = new System.Windows.Forms.TextBox();
            this.TextGain4 = new System.Windows.Forms.TextBox();
            this.DialGain3 = new System.Windows.Forms.PictureBox();
            this.DialGain4 = new System.Windows.Forms.PictureBox();
            this.TextGain3 = new System.Windows.Forms.TextBox();
            this.grpOutputRouter = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.OUTROUTER_1_4 = new System.Windows.Forms.RadioButton();
            this.OUTROUTER_1_3 = new System.Windows.Forms.RadioButton();
            this.OUTROUTER_1_2 = new System.Windows.Forms.RadioButton();
            this.OUTROUTER_1_1 = new System.Windows.Forms.RadioButton();
            this.panel6 = new System.Windows.Forms.Panel();
            this.OUTROUTER_4_4 = new System.Windows.Forms.RadioButton();
            this.OUTROUTER_4_3 = new System.Windows.Forms.RadioButton();
            this.OUTROUTER_4_2 = new System.Windows.Forms.RadioButton();
            this.OUTROUTER_4_1 = new System.Windows.Forms.RadioButton();
            this.panel7 = new System.Windows.Forms.Panel();
            this.OUTROUTER_2_4 = new System.Windows.Forms.RadioButton();
            this.OUTROUTER_2_3 = new System.Windows.Forms.RadioButton();
            this.OUTROUTER_2_2 = new System.Windows.Forms.RadioButton();
            this.OUTROUTER_2_1 = new System.Windows.Forms.RadioButton();
            this.panel8 = new System.Windows.Forms.Panel();
            this.OUTROUTER_3_4 = new System.Windows.Forms.RadioButton();
            this.OUTROUTER_3_3 = new System.Windows.Forms.RadioButton();
            this.OUTROUTER_3_2 = new System.Windows.Forms.RadioButton();
            this.OUTROUTER_3_1 = new System.Windows.Forms.RadioButton();
            this.grpInputRouter = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label54 = new System.Windows.Forms.Label();
            this.label53 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.INROUTER_1_6 = new System.Windows.Forms.RadioButton();
            this.INROUTER_1_5 = new System.Windows.Forms.RadioButton();
            this.INROUTER_1_4 = new System.Windows.Forms.RadioButton();
            this.INROUTER_1_3 = new System.Windows.Forms.RadioButton();
            this.INROUTER_1_2 = new System.Windows.Forms.RadioButton();
            this.INROUTER_1_1 = new System.Windows.Forms.RadioButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.INROUTER_4_6 = new System.Windows.Forms.RadioButton();
            this.INROUTER_4_5 = new System.Windows.Forms.RadioButton();
            this.INROUTER_4_4 = new System.Windows.Forms.RadioButton();
            this.INROUTER_4_3 = new System.Windows.Forms.RadioButton();
            this.INROUTER_4_2 = new System.Windows.Forms.RadioButton();
            this.INROUTER_4_1 = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.INROUTER_2_6 = new System.Windows.Forms.RadioButton();
            this.INROUTER_2_5 = new System.Windows.Forms.RadioButton();
            this.INROUTER_2_4 = new System.Windows.Forms.RadioButton();
            this.INROUTER_2_3 = new System.Windows.Forms.RadioButton();
            this.INROUTER_2_2 = new System.Windows.Forms.RadioButton();
            this.INROUTER_2_1 = new System.Windows.Forms.RadioButton();
            this.panel4 = new System.Windows.Forms.Panel();
            this.INROUTER_3_6 = new System.Windows.Forms.RadioButton();
            this.INROUTER_3_5 = new System.Windows.Forms.RadioButton();
            this.INROUTER_3_4 = new System.Windows.Forms.RadioButton();
            this.INROUTER_3_3 = new System.Windows.Forms.RadioButton();
            this.INROUTER_3_2 = new System.Windows.Forms.RadioButton();
            this.INROUTER_3_1 = new System.Windows.Forms.RadioButton();
            this.grpComm = new System.Windows.Forms.GroupBox();
            this.btnReboot = new System.Windows.Forms.Button();
            this.chkDebug = new System.Windows.Forms.CheckBox();
            this.btnRead = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.statusBar.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.grpSine.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DialPinkGain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DialSineGain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DialSineFreq)).BeginInit();
            this.grpGain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DialGain1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DialGain2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DialGain3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DialGain4)).BeginInit();
            this.grpOutputRouter.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel8.SuspendLayout();
            this.grpInputRouter.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.grpComm.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // serialPort1
            // 
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // statusBar
            // 
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel});
            this.statusBar.Location = new System.Drawing.Point(0, 544);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(624, 22);
            this.statusBar.TabIndex = 6;
            this.statusBar.Text = "Press Start";
            // 
            // StatusLabel
            // 
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(61, 17);
            this.StatusLabel.Text = "Press Start";
            // 
            // textLog
            // 
            this.textLog.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textLog.Location = new System.Drawing.Point(5, 438);
            this.textLog.Multiline = true;
            this.textLog.Name = "textLog";
            this.textLog.ReadOnly = true;
            this.textLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textLog.Size = new System.Drawing.Size(616, 103);
            this.textLog.TabIndex = 7;
            this.textLog.TabStop = false;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(546, 409);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 100;
            this.btnClear.Text = "Clear Log";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 416);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(521, 10);
            this.progressBar1.TabIndex = 12;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.MenuBar;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem1,
            this.deviceToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(624, 24);
            this.menuStrip1.TabIndex = 15;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem1
            // 
            this.fileToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.toolStripSeparator2,
            this.resetConfigurationToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem1});
            this.fileToolStripMenuItem1.Name = "fileToolStripMenuItem1";
            this.fileToolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem1.Text = "&File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripMenuItem.Image")));
            this.openToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.openToolStripMenuItem.Text = "&Open Program";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripMenuItem.Image")));
            this.saveToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.saveToolStripMenuItem.Text = "&Save Program";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(192, 6);
            // 
            // resetConfigurationToolStripMenuItem
            // 
            this.resetConfigurationToolStripMenuItem.Name = "resetConfigurationToolStripMenuItem";
            this.resetConfigurationToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
            this.resetConfigurationToolStripMenuItem.Text = "Reset Interface";
            this.resetConfigurationToolStripMenuItem.Click += new System.EventHandler(this.resetConfigurationToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(192, 6);
            // 
            // exitToolStripMenuItem1
            // 
            this.exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
            this.exitToolStripMenuItem1.ShortcutKeyDisplayString = "Alt+F4";
            this.exitToolStripMenuItem1.Size = new System.Drawing.Size(195, 22);
            this.exitToolStripMenuItem1.Text = "E&xit";
            this.exitToolStripMenuItem1.Click += new System.EventHandler(this.exitToolStripMenuItem1_Click);
            // 
            // deviceToolStripMenuItem
            // 
            this.deviceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.programToolStripMenuItem,
            this.readToolStripMenuItem});
            this.deviceToolStripMenuItem.Name = "deviceToolStripMenuItem";
            this.deviceToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.deviceToolStripMenuItem.Text = "&Device";
            // 
            // programToolStripMenuItem
            // 
            this.programToolStripMenuItem.Enabled = false;
            this.programToolStripMenuItem.Name = "programToolStripMenuItem";
            this.programToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.programToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.programToolStripMenuItem.Text = "&Program Device";
            // 
            // readToolStripMenuItem
            // 
            this.readToolStripMenuItem.Enabled = false;
            this.readToolStripMenuItem.Name = "readToolStripMenuItem";
            this.readToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.readToolStripMenuItem.Size = new System.Drawing.Size(199, 22);
            this.readToolStripMenuItem.Text = "&Read Device";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.commPortToolStripMenuItem,
            this.debugToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.toolsToolStripMenuItem.Text = "&Settings";
            // 
            // commPortToolStripMenuItem
            // 
            this.commPortToolStripMenuItem.Name = "commPortToolStripMenuItem";
            this.commPortToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.commPortToolStripMenuItem.Text = "&Comm Port";
            // 
            // debugToolStripMenuItem
            // 
            this.debugToolStripMenuItem.CheckOnClick = true;
            this.debugToolStripMenuItem.Name = "debugToolStripMenuItem";
            this.debugToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.debugToolStripMenuItem.Text = "&Debug Mode";
            this.debugToolStripMenuItem.Click += new System.EventHandler(this.debugToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem1
            // 
            this.helpToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contentsToolStripMenuItem,
            this.indexToolStripMenuItem,
            this.searchToolStripMenuItem,
            this.toolStripSeparator6,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem1.Name = "helpToolStripMenuItem1";
            this.helpToolStripMenuItem1.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem1.Text = "&Help";
            // 
            // contentsToolStripMenuItem
            // 
            this.contentsToolStripMenuItem.Name = "contentsToolStripMenuItem";
            this.contentsToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.contentsToolStripMenuItem.Text = "&Contents";
            // 
            // indexToolStripMenuItem
            // 
            this.indexToolStripMenuItem.Name = "indexToolStripMenuItem";
            this.indexToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.indexToolStripMenuItem.Text = "&Index";
            // 
            // searchToolStripMenuItem
            // 
            this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
            this.searchToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.searchToolStripMenuItem.Text = "&Search";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(119, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.aboutToolStripMenuItem.Text = "&About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // openProgramDialog
            // 
            this.openProgramDialog.FileName = "openFileDialog1";
            // 
            // saveProgramDialog
            // 
            this.saveProgramDialog.DefaultExt = "prog";
            this.saveProgramDialog.Filter = "Program files (*.prog)|*.prog|All files (*.*)|*.*";
            this.saveProgramDialog.RestoreDirectory = true;
            // 
            // tabMain
            // 
            this.tabMain.BackColor = System.Drawing.SystemColors.Control;
            this.tabMain.Controls.Add(this.label13);
            this.tabMain.Controls.Add(this.dropAmpMode);
            this.tabMain.Controls.Add(this.grpSine);
            this.tabMain.Controls.Add(this.grpGain);
            this.tabMain.Controls.Add(this.grpOutputRouter);
            this.tabMain.Controls.Add(this.grpInputRouter);
            this.tabMain.Controls.Add(this.grpComm);
            this.tabMain.Location = new System.Drawing.Point(4, 22);
            this.tabMain.Name = "tabMain";
            this.tabMain.Padding = new System.Windows.Forms.Padding(3);
            this.tabMain.Size = new System.Drawing.Size(607, 350);
            this.tabMain.TabIndex = 0;
            this.tabMain.Text = "Main Tab";
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(483, 49);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(81, 16);
            this.label13.TabIndex = 52;
            this.label13.Text = "Amp Mode";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dropAmpMode
            // 
            this.dropAmpMode.FormattingEnabled = true;
            this.dropAmpMode.Items.AddRange(new object[] {
            "4 Channel",
            "2 Channel",
            "2.1 Channel"});
            this.dropAmpMode.Location = new System.Drawing.Point(486, 68);
            this.dropAmpMode.Name = "dropAmpMode";
            this.dropAmpMode.Size = new System.Drawing.Size(104, 21);
            this.dropAmpMode.TabIndex = 51;
            // 
            // grpSine
            // 
            this.grpSine.Controls.Add(this.TextPinkGain);
            this.grpSine.Controls.Add(this.DialPinkGain);
            this.grpSine.Controls.Add(this.label12);
            this.grpSine.Controls.Add(this.label11);
            this.grpSine.Controls.Add(this.label10);
            this.grpSine.Controls.Add(this.TextSineGain);
            this.grpSine.Controls.Add(this.DialSineGain);
            this.grpSine.Controls.Add(this.TextSineFreq);
            this.grpSine.Controls.Add(this.DialSineFreq);
            this.grpSine.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpSine.Location = new System.Drawing.Point(212, 169);
            this.grpSine.Name = "grpSine";
            this.grpSine.Size = new System.Drawing.Size(217, 178);
            this.grpSine.TabIndex = 50;
            this.grpSine.TabStop = false;
            this.grpSine.Text = "Generators";
            // 
            // TextPinkGain
            // 
            this.TextPinkGain.Cursor = System.Windows.Forms.Cursors.Hand;
            this.TextPinkGain.Enabled = false;
            this.TextPinkGain.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextPinkGain.Location = new System.Drawing.Point(139, 53);
            this.TextPinkGain.Name = "TextPinkGain";
            this.TextPinkGain.Size = new System.Drawing.Size(50, 22);
            this.TextPinkGain.TabIndex = 50;
            this.TextPinkGain.Text = "10dB";
            this.TextPinkGain.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // DialPinkGain
            // 
            this.DialPinkGain.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DialPinkGain.Enabled = false;
            this.DialPinkGain.Location = new System.Drawing.Point(145, 76);
            this.DialPinkGain.Name = "DialPinkGain";
            this.DialPinkGain.Size = new System.Drawing.Size(40, 40);
            this.DialPinkGain.TabIndex = 49;
            this.DialPinkGain.TabStop = false;
            // 
            // label12
            // 
            this.label12.Enabled = false;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(134, 29);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(64, 13);
            this.label12.TabIndex = 48;
            this.label12.Text = "Pink Gain";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.Enabled = false;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(73, 29);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(58, 13);
            this.label11.TabIndex = 47;
            this.label11.Text = "Sine Gain";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(7, 29);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(56, 13);
            this.label10.TabIndex = 46;
            this.label10.Text = "Sine Freq";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TextSineGain
            // 
            this.TextSineGain.Cursor = System.Windows.Forms.Cursors.Hand;
            this.TextSineGain.Enabled = false;
            this.TextSineGain.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextSineGain.Location = new System.Drawing.Point(75, 53);
            this.TextSineGain.Name = "TextSineGain";
            this.TextSineGain.Size = new System.Drawing.Size(50, 22);
            this.TextSineGain.TabIndex = 38;
            this.TextSineGain.Text = "10dB";
            this.TextSineGain.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // DialSineGain
            // 
            this.DialSineGain.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DialSineGain.Enabled = false;
            this.DialSineGain.Location = new System.Drawing.Point(81, 76);
            this.DialSineGain.Name = "DialSineGain";
            this.DialSineGain.Size = new System.Drawing.Size(40, 40);
            this.DialSineGain.TabIndex = 37;
            this.DialSineGain.TabStop = false;
            // 
            // TextSineFreq
            // 
            this.TextSineFreq.Cursor = System.Windows.Forms.Cursors.Hand;
            this.TextSineFreq.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextSineFreq.Location = new System.Drawing.Point(10, 53);
            this.TextSineFreq.Name = "TextSineFreq";
            this.TextSineFreq.Size = new System.Drawing.Size(50, 22);
            this.TextSineFreq.TabIndex = 36;
            this.TextSineFreq.Text = "1.00kHz";
            this.TextSineFreq.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // DialSineFreq
            // 
            this.DialSineFreq.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DialSineFreq.Location = new System.Drawing.Point(16, 76);
            this.DialSineFreq.Name = "DialSineFreq";
            this.DialSineFreq.Size = new System.Drawing.Size(40, 40);
            this.DialSineFreq.TabIndex = 35;
            this.DialSineFreq.TabStop = false;
            // 
            // grpGain
            // 
            this.grpGain.Controls.Add(this.lblCH1);
            this.grpGain.Controls.Add(this.label9);
            this.grpGain.Controls.Add(this.DialGain1);
            this.grpGain.Controls.Add(this.label4);
            this.grpGain.Controls.Add(this.TextGain1);
            this.grpGain.Controls.Add(this.label3);
            this.grpGain.Controls.Add(this.DialGain2);
            this.grpGain.Controls.Add(this.TextGain2);
            this.grpGain.Controls.Add(this.TextGain4);
            this.grpGain.Controls.Add(this.DialGain3);
            this.grpGain.Controls.Add(this.DialGain4);
            this.grpGain.Controls.Add(this.TextGain3);
            this.grpGain.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpGain.Location = new System.Drawing.Point(193, 31);
            this.grpGain.Name = "grpGain";
            this.grpGain.Size = new System.Drawing.Size(268, 115);
            this.grpGain.TabIndex = 49;
            this.grpGain.TabStop = false;
            this.grpGain.Text = "Gain";
            // 
            // lblCH1
            // 
            this.lblCH1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCH1.Location = new System.Drawing.Point(16, 18);
            this.lblCH1.Name = "lblCH1";
            this.lblCH1.Size = new System.Drawing.Size(40, 13);
            this.lblCH1.TabIndex = 45;
            this.lblCH1.Text = "CH 1";
            this.lblCH1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(215, 18);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(40, 13);
            this.label9.TabIndex = 48;
            this.label9.Text = "CH 4";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DialGain1
            // 
            this.DialGain1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DialGain1.Location = new System.Drawing.Point(16, 64);
            this.DialGain1.Name = "DialGain1";
            this.DialGain1.Size = new System.Drawing.Size(40, 40);
            this.DialGain1.TabIndex = 37;
            this.DialGain1.TabStop = false;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(149, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 13);
            this.label4.TabIndex = 47;
            this.label4.Text = "CH 3";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TextGain1
            // 
            this.TextGain1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.TextGain1.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextGain1.Location = new System.Drawing.Point(10, 41);
            this.TextGain1.Name = "TextGain1";
            this.TextGain1.Size = new System.Drawing.Size(50, 22);
            this.TextGain1.TabIndex = 38;
            this.TextGain1.Text = "0dB";
            this.TextGain1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(82, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 46;
            this.label3.Text = "CH 2";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DialGain2
            // 
            this.DialGain2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DialGain2.Location = new System.Drawing.Point(82, 64);
            this.DialGain2.Name = "DialGain2";
            this.DialGain2.Size = new System.Drawing.Size(40, 40);
            this.DialGain2.TabIndex = 39;
            this.DialGain2.TabStop = false;
            // 
            // TextGain2
            // 
            this.TextGain2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.TextGain2.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextGain2.Location = new System.Drawing.Point(76, 41);
            this.TextGain2.Name = "TextGain2";
            this.TextGain2.Size = new System.Drawing.Size(50, 22);
            this.TextGain2.TabIndex = 40;
            this.TextGain2.Text = "0dB";
            this.TextGain2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TextGain4
            // 
            this.TextGain4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.TextGain4.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextGain4.Location = new System.Drawing.Point(209, 41);
            this.TextGain4.Name = "TextGain4";
            this.TextGain4.Size = new System.Drawing.Size(50, 22);
            this.TextGain4.TabIndex = 44;
            this.TextGain4.Text = "0dB";
            this.TextGain4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // DialGain3
            // 
            this.DialGain3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DialGain3.Location = new System.Drawing.Point(149, 64);
            this.DialGain3.Name = "DialGain3";
            this.DialGain3.Size = new System.Drawing.Size(40, 40);
            this.DialGain3.TabIndex = 41;
            this.DialGain3.TabStop = false;
            // 
            // DialGain4
            // 
            this.DialGain4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DialGain4.Location = new System.Drawing.Point(215, 64);
            this.DialGain4.Name = "DialGain4";
            this.DialGain4.Size = new System.Drawing.Size(40, 40);
            this.DialGain4.TabIndex = 43;
            this.DialGain4.TabStop = false;
            // 
            // TextGain3
            // 
            this.TextGain3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.TextGain3.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextGain3.Location = new System.Drawing.Point(143, 41);
            this.TextGain3.Name = "TextGain3";
            this.TextGain3.Size = new System.Drawing.Size(50, 22);
            this.TextGain3.TabIndex = 42;
            this.TextGain3.Text = "0dB";
            this.TextGain3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // grpOutputRouter
            // 
            this.grpOutputRouter.Controls.Add(this.label2);
            this.grpOutputRouter.Controls.Add(this.label5);
            this.grpOutputRouter.Controls.Add(this.label6);
            this.grpOutputRouter.Controls.Add(this.label7);
            this.grpOutputRouter.Controls.Add(this.label8);
            this.grpOutputRouter.Controls.Add(this.panel5);
            this.grpOutputRouter.Controls.Add(this.panel6);
            this.grpOutputRouter.Controls.Add(this.panel7);
            this.grpOutputRouter.Controls.Add(this.panel8);
            this.grpOutputRouter.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpOutputRouter.Location = new System.Drawing.Point(435, 180);
            this.grpOutputRouter.Name = "grpOutputRouter";
            this.grpOutputRouter.Size = new System.Drawing.Size(166, 167);
            this.grpOutputRouter.TabIndex = 31;
            this.grpOutputRouter.TabStop = false;
            this.grpOutputRouter.Text = "Output Router";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(31, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 13);
            this.label2.TabIndex = 36;
            this.label2.Text = "1       2        3       4";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(4, 112);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(22, 13);
            this.label5.TabIndex = 33;
            this.label5.Text = "4";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(4, 90);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(22, 13);
            this.label6.TabIndex = 32;
            this.label6.Text = "3";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(4, 68);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(22, 13);
            this.label7.TabIndex = 31;
            this.label7.Text = "2";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(7, 46);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(19, 13);
            this.label8.TabIndex = 30;
            this.label8.Text = "1";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.OUTROUTER_1_4);
            this.panel5.Controls.Add(this.OUTROUTER_1_3);
            this.panel5.Controls.Add(this.OUTROUTER_1_2);
            this.panel5.Controls.Add(this.OUTROUTER_1_1);
            this.panel5.Location = new System.Drawing.Point(23, 37);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(30, 103);
            this.panel5.TabIndex = 26;
            // 
            // OUTROUTER_1_4
            // 
            this.OUTROUTER_1_4.AutoSize = true;
            this.OUTROUTER_1_4.Location = new System.Drawing.Point(8, 75);
            this.OUTROUTER_1_4.Name = "OUTROUTER_1_4";
            this.OUTROUTER_1_4.Size = new System.Drawing.Size(14, 13);
            this.OUTROUTER_1_4.TabIndex = 3;
            this.OUTROUTER_1_4.UseVisualStyleBackColor = true;
            // 
            // OUTROUTER_1_3
            // 
            this.OUTROUTER_1_3.AutoSize = true;
            this.OUTROUTER_1_3.Location = new System.Drawing.Point(8, 53);
            this.OUTROUTER_1_3.Name = "OUTROUTER_1_3";
            this.OUTROUTER_1_3.Size = new System.Drawing.Size(14, 13);
            this.OUTROUTER_1_3.TabIndex = 2;
            this.OUTROUTER_1_3.UseVisualStyleBackColor = true;
            // 
            // OUTROUTER_1_2
            // 
            this.OUTROUTER_1_2.AutoSize = true;
            this.OUTROUTER_1_2.Location = new System.Drawing.Point(8, 31);
            this.OUTROUTER_1_2.Name = "OUTROUTER_1_2";
            this.OUTROUTER_1_2.Size = new System.Drawing.Size(14, 13);
            this.OUTROUTER_1_2.TabIndex = 1;
            this.OUTROUTER_1_2.UseVisualStyleBackColor = true;
            // 
            // OUTROUTER_1_1
            // 
            this.OUTROUTER_1_1.AutoSize = true;
            this.OUTROUTER_1_1.Checked = true;
            this.OUTROUTER_1_1.Location = new System.Drawing.Point(8, 9);
            this.OUTROUTER_1_1.Name = "OUTROUTER_1_1";
            this.OUTROUTER_1_1.Size = new System.Drawing.Size(14, 13);
            this.OUTROUTER_1_1.TabIndex = 0;
            this.OUTROUTER_1_1.TabStop = true;
            this.OUTROUTER_1_1.UseVisualStyleBackColor = true;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.OUTROUTER_4_4);
            this.panel6.Controls.Add(this.OUTROUTER_4_3);
            this.panel6.Controls.Add(this.OUTROUTER_4_2);
            this.panel6.Controls.Add(this.OUTROUTER_4_1);
            this.panel6.Location = new System.Drawing.Point(107, 37);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(30, 103);
            this.panel6.TabIndex = 29;
            // 
            // OUTROUTER_4_4
            // 
            this.OUTROUTER_4_4.AutoSize = true;
            this.OUTROUTER_4_4.Checked = true;
            this.OUTROUTER_4_4.Location = new System.Drawing.Point(8, 75);
            this.OUTROUTER_4_4.Name = "OUTROUTER_4_4";
            this.OUTROUTER_4_4.Size = new System.Drawing.Size(14, 13);
            this.OUTROUTER_4_4.TabIndex = 3;
            this.OUTROUTER_4_4.TabStop = true;
            this.OUTROUTER_4_4.UseVisualStyleBackColor = true;
            // 
            // OUTROUTER_4_3
            // 
            this.OUTROUTER_4_3.AutoSize = true;
            this.OUTROUTER_4_3.Location = new System.Drawing.Point(8, 53);
            this.OUTROUTER_4_3.Name = "OUTROUTER_4_3";
            this.OUTROUTER_4_3.Size = new System.Drawing.Size(14, 13);
            this.OUTROUTER_4_3.TabIndex = 2;
            this.OUTROUTER_4_3.UseVisualStyleBackColor = true;
            // 
            // OUTROUTER_4_2
            // 
            this.OUTROUTER_4_2.AutoSize = true;
            this.OUTROUTER_4_2.Location = new System.Drawing.Point(8, 31);
            this.OUTROUTER_4_2.Name = "OUTROUTER_4_2";
            this.OUTROUTER_4_2.Size = new System.Drawing.Size(14, 13);
            this.OUTROUTER_4_2.TabIndex = 1;
            this.OUTROUTER_4_2.UseVisualStyleBackColor = true;
            // 
            // OUTROUTER_4_1
            // 
            this.OUTROUTER_4_1.AutoSize = true;
            this.OUTROUTER_4_1.Location = new System.Drawing.Point(8, 9);
            this.OUTROUTER_4_1.Name = "OUTROUTER_4_1";
            this.OUTROUTER_4_1.Size = new System.Drawing.Size(14, 13);
            this.OUTROUTER_4_1.TabIndex = 0;
            this.OUTROUTER_4_1.UseVisualStyleBackColor = true;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.OUTROUTER_2_4);
            this.panel7.Controls.Add(this.OUTROUTER_2_3);
            this.panel7.Controls.Add(this.OUTROUTER_2_2);
            this.panel7.Controls.Add(this.OUTROUTER_2_1);
            this.panel7.Location = new System.Drawing.Point(51, 37);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(30, 103);
            this.panel7.TabIndex = 27;
            // 
            // OUTROUTER_2_4
            // 
            this.OUTROUTER_2_4.AutoSize = true;
            this.OUTROUTER_2_4.Location = new System.Drawing.Point(8, 75);
            this.OUTROUTER_2_4.Name = "OUTROUTER_2_4";
            this.OUTROUTER_2_4.Size = new System.Drawing.Size(14, 13);
            this.OUTROUTER_2_4.TabIndex = 3;
            this.OUTROUTER_2_4.UseVisualStyleBackColor = true;
            // 
            // OUTROUTER_2_3
            // 
            this.OUTROUTER_2_3.AutoSize = true;
            this.OUTROUTER_2_3.Location = new System.Drawing.Point(8, 53);
            this.OUTROUTER_2_3.Name = "OUTROUTER_2_3";
            this.OUTROUTER_2_3.Size = new System.Drawing.Size(14, 13);
            this.OUTROUTER_2_3.TabIndex = 2;
            this.OUTROUTER_2_3.UseVisualStyleBackColor = true;
            // 
            // OUTROUTER_2_2
            // 
            this.OUTROUTER_2_2.AutoSize = true;
            this.OUTROUTER_2_2.Checked = true;
            this.OUTROUTER_2_2.Location = new System.Drawing.Point(8, 31);
            this.OUTROUTER_2_2.Name = "OUTROUTER_2_2";
            this.OUTROUTER_2_2.Size = new System.Drawing.Size(14, 13);
            this.OUTROUTER_2_2.TabIndex = 1;
            this.OUTROUTER_2_2.TabStop = true;
            this.OUTROUTER_2_2.UseVisualStyleBackColor = true;
            // 
            // OUTROUTER_2_1
            // 
            this.OUTROUTER_2_1.AutoSize = true;
            this.OUTROUTER_2_1.Location = new System.Drawing.Point(8, 9);
            this.OUTROUTER_2_1.Name = "OUTROUTER_2_1";
            this.OUTROUTER_2_1.Size = new System.Drawing.Size(14, 13);
            this.OUTROUTER_2_1.TabIndex = 0;
            this.OUTROUTER_2_1.UseVisualStyleBackColor = true;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.OUTROUTER_3_4);
            this.panel8.Controls.Add(this.OUTROUTER_3_3);
            this.panel8.Controls.Add(this.OUTROUTER_3_2);
            this.panel8.Controls.Add(this.OUTROUTER_3_1);
            this.panel8.Location = new System.Drawing.Point(79, 37);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(30, 103);
            this.panel8.TabIndex = 28;
            // 
            // OUTROUTER_3_4
            // 
            this.OUTROUTER_3_4.AutoSize = true;
            this.OUTROUTER_3_4.Location = new System.Drawing.Point(8, 75);
            this.OUTROUTER_3_4.Name = "OUTROUTER_3_4";
            this.OUTROUTER_3_4.Size = new System.Drawing.Size(14, 13);
            this.OUTROUTER_3_4.TabIndex = 3;
            this.OUTROUTER_3_4.UseVisualStyleBackColor = true;
            // 
            // OUTROUTER_3_3
            // 
            this.OUTROUTER_3_3.AutoSize = true;
            this.OUTROUTER_3_3.Checked = true;
            this.OUTROUTER_3_3.Location = new System.Drawing.Point(8, 53);
            this.OUTROUTER_3_3.Name = "OUTROUTER_3_3";
            this.OUTROUTER_3_3.Size = new System.Drawing.Size(14, 13);
            this.OUTROUTER_3_3.TabIndex = 2;
            this.OUTROUTER_3_3.TabStop = true;
            this.OUTROUTER_3_3.UseVisualStyleBackColor = true;
            // 
            // OUTROUTER_3_2
            // 
            this.OUTROUTER_3_2.AutoSize = true;
            this.OUTROUTER_3_2.Location = new System.Drawing.Point(8, 31);
            this.OUTROUTER_3_2.Name = "OUTROUTER_3_2";
            this.OUTROUTER_3_2.Size = new System.Drawing.Size(14, 13);
            this.OUTROUTER_3_2.TabIndex = 1;
            this.OUTROUTER_3_2.UseVisualStyleBackColor = true;
            // 
            // OUTROUTER_3_1
            // 
            this.OUTROUTER_3_1.AutoSize = true;
            this.OUTROUTER_3_1.Location = new System.Drawing.Point(8, 9);
            this.OUTROUTER_3_1.Name = "OUTROUTER_3_1";
            this.OUTROUTER_3_1.Size = new System.Drawing.Size(14, 13);
            this.OUTROUTER_3_1.TabIndex = 0;
            this.OUTROUTER_3_1.UseVisualStyleBackColor = true;
            // 
            // grpInputRouter
            // 
            this.grpInputRouter.Controls.Add(this.label1);
            this.grpInputRouter.Controls.Add(this.label54);
            this.grpInputRouter.Controls.Add(this.label53);
            this.grpInputRouter.Controls.Add(this.label37);
            this.grpInputRouter.Controls.Add(this.label38);
            this.grpInputRouter.Controls.Add(this.label36);
            this.grpInputRouter.Controls.Add(this.label35);
            this.grpInputRouter.Controls.Add(this.panel1);
            this.grpInputRouter.Controls.Add(this.panel3);
            this.grpInputRouter.Controls.Add(this.panel2);
            this.grpInputRouter.Controls.Add(this.panel4);
            this.grpInputRouter.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpInputRouter.Location = new System.Drawing.Point(7, 158);
            this.grpInputRouter.Name = "grpInputRouter";
            this.grpInputRouter.Size = new System.Drawing.Size(199, 189);
            this.grpInputRouter.TabIndex = 30;
            this.grpInputRouter.TabStop = false;
            this.grpInputRouter.Text = "Input Router";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(77, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 13);
            this.label1.TabIndex = 36;
            this.label1.Text = "1       2        3       4";
            // 
            // label54
            // 
            this.label54.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label54.Location = new System.Drawing.Point(2, 158);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(70, 13);
            this.label54.TabIndex = 35;
            this.label54.Text = "Pink Noise";
            this.label54.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label53
            // 
            this.label53.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label53.Location = new System.Drawing.Point(2, 136);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(70, 13);
            this.label53.TabIndex = 34;
            this.label53.Text = "Sine Wave";
            this.label53.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label37
            // 
            this.label37.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label37.Location = new System.Drawing.Point(2, 114);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(70, 13);
            this.label37.TabIndex = 33;
            this.label37.Text = "Input 4";
            this.label37.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label38
            // 
            this.label38.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label38.Location = new System.Drawing.Point(2, 92);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(70, 13);
            this.label38.TabIndex = 32;
            this.label38.Text = "Input 3";
            this.label38.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label36
            // 
            this.label36.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label36.Location = new System.Drawing.Point(2, 70);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(70, 13);
            this.label36.TabIndex = 31;
            this.label36.Text = "Input 2";
            this.label36.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label35
            // 
            this.label35.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label35.Location = new System.Drawing.Point(5, 48);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(67, 13);
            this.label35.TabIndex = 30;
            this.label35.Text = "Input 1";
            this.label35.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.INROUTER_1_6);
            this.panel1.Controls.Add(this.INROUTER_1_5);
            this.panel1.Controls.Add(this.INROUTER_1_4);
            this.panel1.Controls.Add(this.INROUTER_1_3);
            this.panel1.Controls.Add(this.INROUTER_1_2);
            this.panel1.Controls.Add(this.INROUTER_1_1);
            this.panel1.Location = new System.Drawing.Point(69, 39);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(30, 141);
            this.panel1.TabIndex = 26;
            // 
            // INROUTER_1_6
            // 
            this.INROUTER_1_6.AutoSize = true;
            this.INROUTER_1_6.Location = new System.Drawing.Point(8, 119);
            this.INROUTER_1_6.Name = "INROUTER_1_6";
            this.INROUTER_1_6.Size = new System.Drawing.Size(14, 13);
            this.INROUTER_1_6.TabIndex = 5;
            this.INROUTER_1_6.UseVisualStyleBackColor = true;
            // 
            // INROUTER_1_5
            // 
            this.INROUTER_1_5.AutoSize = true;
            this.INROUTER_1_5.Location = new System.Drawing.Point(8, 97);
            this.INROUTER_1_5.Name = "INROUTER_1_5";
            this.INROUTER_1_5.Size = new System.Drawing.Size(14, 13);
            this.INROUTER_1_5.TabIndex = 4;
            this.INROUTER_1_5.UseVisualStyleBackColor = true;
            // 
            // INROUTER_1_4
            // 
            this.INROUTER_1_4.AutoSize = true;
            this.INROUTER_1_4.Location = new System.Drawing.Point(8, 75);
            this.INROUTER_1_4.Name = "INROUTER_1_4";
            this.INROUTER_1_4.Size = new System.Drawing.Size(14, 13);
            this.INROUTER_1_4.TabIndex = 3;
            this.INROUTER_1_4.UseVisualStyleBackColor = true;
            // 
            // INROUTER_1_3
            // 
            this.INROUTER_1_3.AutoSize = true;
            this.INROUTER_1_3.Location = new System.Drawing.Point(8, 53);
            this.INROUTER_1_3.Name = "INROUTER_1_3";
            this.INROUTER_1_3.Size = new System.Drawing.Size(14, 13);
            this.INROUTER_1_3.TabIndex = 2;
            this.INROUTER_1_3.UseVisualStyleBackColor = true;
            // 
            // INROUTER_1_2
            // 
            this.INROUTER_1_2.AutoSize = true;
            this.INROUTER_1_2.Location = new System.Drawing.Point(8, 31);
            this.INROUTER_1_2.Name = "INROUTER_1_2";
            this.INROUTER_1_2.Size = new System.Drawing.Size(14, 13);
            this.INROUTER_1_2.TabIndex = 1;
            this.INROUTER_1_2.UseVisualStyleBackColor = true;
            // 
            // INROUTER_1_1
            // 
            this.INROUTER_1_1.AutoSize = true;
            this.INROUTER_1_1.Checked = true;
            this.INROUTER_1_1.Location = new System.Drawing.Point(8, 9);
            this.INROUTER_1_1.Name = "INROUTER_1_1";
            this.INROUTER_1_1.Size = new System.Drawing.Size(14, 13);
            this.INROUTER_1_1.TabIndex = 0;
            this.INROUTER_1_1.TabStop = true;
            this.INROUTER_1_1.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.INROUTER_4_6);
            this.panel3.Controls.Add(this.INROUTER_4_5);
            this.panel3.Controls.Add(this.INROUTER_4_4);
            this.panel3.Controls.Add(this.INROUTER_4_3);
            this.panel3.Controls.Add(this.INROUTER_4_2);
            this.panel3.Controls.Add(this.INROUTER_4_1);
            this.panel3.Location = new System.Drawing.Point(153, 39);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(30, 141);
            this.panel3.TabIndex = 29;
            // 
            // INROUTER_4_6
            // 
            this.INROUTER_4_6.AutoSize = true;
            this.INROUTER_4_6.Location = new System.Drawing.Point(8, 119);
            this.INROUTER_4_6.Name = "INROUTER_4_6";
            this.INROUTER_4_6.Size = new System.Drawing.Size(14, 13);
            this.INROUTER_4_6.TabIndex = 5;
            this.INROUTER_4_6.UseVisualStyleBackColor = true;
            // 
            // INROUTER_4_5
            // 
            this.INROUTER_4_5.AutoSize = true;
            this.INROUTER_4_5.Location = new System.Drawing.Point(8, 97);
            this.INROUTER_4_5.Name = "INROUTER_4_5";
            this.INROUTER_4_5.Size = new System.Drawing.Size(14, 13);
            this.INROUTER_4_5.TabIndex = 4;
            this.INROUTER_4_5.UseVisualStyleBackColor = true;
            // 
            // INROUTER_4_4
            // 
            this.INROUTER_4_4.AutoSize = true;
            this.INROUTER_4_4.Checked = true;
            this.INROUTER_4_4.Location = new System.Drawing.Point(8, 75);
            this.INROUTER_4_4.Name = "INROUTER_4_4";
            this.INROUTER_4_4.Size = new System.Drawing.Size(14, 13);
            this.INROUTER_4_4.TabIndex = 3;
            this.INROUTER_4_4.TabStop = true;
            this.INROUTER_4_4.UseVisualStyleBackColor = true;
            // 
            // INROUTER_4_3
            // 
            this.INROUTER_4_3.AutoSize = true;
            this.INROUTER_4_3.Location = new System.Drawing.Point(8, 53);
            this.INROUTER_4_3.Name = "INROUTER_4_3";
            this.INROUTER_4_3.Size = new System.Drawing.Size(14, 13);
            this.INROUTER_4_3.TabIndex = 2;
            this.INROUTER_4_3.UseVisualStyleBackColor = true;
            // 
            // INROUTER_4_2
            // 
            this.INROUTER_4_2.AutoSize = true;
            this.INROUTER_4_2.Location = new System.Drawing.Point(8, 31);
            this.INROUTER_4_2.Name = "INROUTER_4_2";
            this.INROUTER_4_2.Size = new System.Drawing.Size(14, 13);
            this.INROUTER_4_2.TabIndex = 1;
            this.INROUTER_4_2.UseVisualStyleBackColor = true;
            // 
            // INROUTER_4_1
            // 
            this.INROUTER_4_1.AutoSize = true;
            this.INROUTER_4_1.Location = new System.Drawing.Point(8, 9);
            this.INROUTER_4_1.Name = "INROUTER_4_1";
            this.INROUTER_4_1.Size = new System.Drawing.Size(14, 13);
            this.INROUTER_4_1.TabIndex = 0;
            this.INROUTER_4_1.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.INROUTER_2_6);
            this.panel2.Controls.Add(this.INROUTER_2_5);
            this.panel2.Controls.Add(this.INROUTER_2_4);
            this.panel2.Controls.Add(this.INROUTER_2_3);
            this.panel2.Controls.Add(this.INROUTER_2_2);
            this.panel2.Controls.Add(this.INROUTER_2_1);
            this.panel2.Location = new System.Drawing.Point(97, 39);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(30, 141);
            this.panel2.TabIndex = 27;
            // 
            // INROUTER_2_6
            // 
            this.INROUTER_2_6.AutoSize = true;
            this.INROUTER_2_6.Location = new System.Drawing.Point(8, 119);
            this.INROUTER_2_6.Name = "INROUTER_2_6";
            this.INROUTER_2_6.Size = new System.Drawing.Size(14, 13);
            this.INROUTER_2_6.TabIndex = 5;
            this.INROUTER_2_6.UseVisualStyleBackColor = true;
            // 
            // INROUTER_2_5
            // 
            this.INROUTER_2_5.AutoSize = true;
            this.INROUTER_2_5.Location = new System.Drawing.Point(8, 97);
            this.INROUTER_2_5.Name = "INROUTER_2_5";
            this.INROUTER_2_5.Size = new System.Drawing.Size(14, 13);
            this.INROUTER_2_5.TabIndex = 4;
            this.INROUTER_2_5.UseVisualStyleBackColor = true;
            // 
            // INROUTER_2_4
            // 
            this.INROUTER_2_4.AutoSize = true;
            this.INROUTER_2_4.Location = new System.Drawing.Point(8, 75);
            this.INROUTER_2_4.Name = "INROUTER_2_4";
            this.INROUTER_2_4.Size = new System.Drawing.Size(14, 13);
            this.INROUTER_2_4.TabIndex = 3;
            this.INROUTER_2_4.UseVisualStyleBackColor = true;
            // 
            // INROUTER_2_3
            // 
            this.INROUTER_2_3.AutoSize = true;
            this.INROUTER_2_3.Location = new System.Drawing.Point(8, 53);
            this.INROUTER_2_3.Name = "INROUTER_2_3";
            this.INROUTER_2_3.Size = new System.Drawing.Size(14, 13);
            this.INROUTER_2_3.TabIndex = 2;
            this.INROUTER_2_3.UseVisualStyleBackColor = true;
            // 
            // INROUTER_2_2
            // 
            this.INROUTER_2_2.AutoSize = true;
            this.INROUTER_2_2.Checked = true;
            this.INROUTER_2_2.Location = new System.Drawing.Point(8, 31);
            this.INROUTER_2_2.Name = "INROUTER_2_2";
            this.INROUTER_2_2.Size = new System.Drawing.Size(14, 13);
            this.INROUTER_2_2.TabIndex = 1;
            this.INROUTER_2_2.TabStop = true;
            this.INROUTER_2_2.UseVisualStyleBackColor = true;
            // 
            // INROUTER_2_1
            // 
            this.INROUTER_2_1.AutoSize = true;
            this.INROUTER_2_1.Location = new System.Drawing.Point(8, 9);
            this.INROUTER_2_1.Name = "INROUTER_2_1";
            this.INROUTER_2_1.Size = new System.Drawing.Size(14, 13);
            this.INROUTER_2_1.TabIndex = 0;
            this.INROUTER_2_1.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.INROUTER_3_6);
            this.panel4.Controls.Add(this.INROUTER_3_5);
            this.panel4.Controls.Add(this.INROUTER_3_4);
            this.panel4.Controls.Add(this.INROUTER_3_3);
            this.panel4.Controls.Add(this.INROUTER_3_2);
            this.panel4.Controls.Add(this.INROUTER_3_1);
            this.panel4.Location = new System.Drawing.Point(125, 39);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(30, 141);
            this.panel4.TabIndex = 28;
            // 
            // INROUTER_3_6
            // 
            this.INROUTER_3_6.AutoSize = true;
            this.INROUTER_3_6.Location = new System.Drawing.Point(8, 119);
            this.INROUTER_3_6.Name = "INROUTER_3_6";
            this.INROUTER_3_6.Size = new System.Drawing.Size(14, 13);
            this.INROUTER_3_6.TabIndex = 5;
            this.INROUTER_3_6.UseVisualStyleBackColor = true;
            // 
            // INROUTER_3_5
            // 
            this.INROUTER_3_5.AutoSize = true;
            this.INROUTER_3_5.Location = new System.Drawing.Point(8, 97);
            this.INROUTER_3_5.Name = "INROUTER_3_5";
            this.INROUTER_3_5.Size = new System.Drawing.Size(14, 13);
            this.INROUTER_3_5.TabIndex = 4;
            this.INROUTER_3_5.UseVisualStyleBackColor = true;
            // 
            // INROUTER_3_4
            // 
            this.INROUTER_3_4.AutoSize = true;
            this.INROUTER_3_4.Location = new System.Drawing.Point(8, 75);
            this.INROUTER_3_4.Name = "INROUTER_3_4";
            this.INROUTER_3_4.Size = new System.Drawing.Size(14, 13);
            this.INROUTER_3_4.TabIndex = 3;
            this.INROUTER_3_4.UseVisualStyleBackColor = true;
            // 
            // INROUTER_3_3
            // 
            this.INROUTER_3_3.AutoSize = true;
            this.INROUTER_3_3.Checked = true;
            this.INROUTER_3_3.Location = new System.Drawing.Point(8, 53);
            this.INROUTER_3_3.Name = "INROUTER_3_3";
            this.INROUTER_3_3.Size = new System.Drawing.Size(14, 13);
            this.INROUTER_3_3.TabIndex = 2;
            this.INROUTER_3_3.TabStop = true;
            this.INROUTER_3_3.UseVisualStyleBackColor = true;
            // 
            // INROUTER_3_2
            // 
            this.INROUTER_3_2.AutoSize = true;
            this.INROUTER_3_2.Location = new System.Drawing.Point(8, 31);
            this.INROUTER_3_2.Name = "INROUTER_3_2";
            this.INROUTER_3_2.Size = new System.Drawing.Size(14, 13);
            this.INROUTER_3_2.TabIndex = 1;
            this.INROUTER_3_2.UseVisualStyleBackColor = true;
            // 
            // INROUTER_3_1
            // 
            this.INROUTER_3_1.AutoSize = true;
            this.INROUTER_3_1.Location = new System.Drawing.Point(8, 9);
            this.INROUTER_3_1.Name = "INROUTER_3_1";
            this.INROUTER_3_1.Size = new System.Drawing.Size(14, 13);
            this.INROUTER_3_1.TabIndex = 0;
            this.INROUTER_3_1.UseVisualStyleBackColor = true;
            // 
            // grpComm
            // 
            this.grpComm.Controls.Add(this.btnReboot);
            this.grpComm.Controls.Add(this.chkDebug);
            this.grpComm.Controls.Add(this.btnRead);
            this.grpComm.Controls.Add(this.btnSave);
            this.grpComm.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpComm.Location = new System.Drawing.Point(7, 10);
            this.grpComm.Name = "grpComm";
            this.grpComm.Size = new System.Drawing.Size(162, 146);
            this.grpComm.TabIndex = 15;
            this.grpComm.TabStop = false;
            this.grpComm.Text = "Communications";
            // 
            // btnReboot
            // 
            this.btnReboot.Location = new System.Drawing.Point(34, 92);
            this.btnReboot.Name = "btnReboot";
            this.btnReboot.Size = new System.Drawing.Size(87, 21);
            this.btnReboot.TabIndex = 23;
            this.btnReboot.Text = "Reboot Board";
            this.btnReboot.UseVisualStyleBackColor = true;
            this.btnReboot.Visible = false;
            this.btnReboot.Click += new System.EventHandler(this.btnReboot_Click);
            // 
            // chkDebug
            // 
            this.chkDebug.AutoSize = true;
            this.chkDebug.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDebug.Location = new System.Drawing.Point(34, 119);
            this.chkDebug.Name = "chkDebug";
            this.chkDebug.Size = new System.Drawing.Size(94, 17);
            this.chkDebug.TabIndex = 20;
            this.chkDebug.Text = "Debug Mode";
            this.chkDebug.UseVisualStyleBackColor = true;
            // 
            // btnRead
            // 
            this.btnRead.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRead.Location = new System.Drawing.Point(34, 56);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(87, 23);
            this.btnRead.TabIndex = 11;
            this.btnRead.Text = "Read Device";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(34, 21);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(87, 23);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "Save to Device";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabMain);
            this.tabControl1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(6, 27);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(615, 376);
            this.tabControl1.TabIndex = 14;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 566);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.textLog);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "FLX Control Center - Evaluation Build";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabMain.ResumeLayout(false);
            this.grpSine.ResumeLayout(false);
            this.grpSine.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DialPinkGain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DialSineGain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DialSineFreq)).EndInit();
            this.grpGain.ResumeLayout(false);
            this.grpGain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DialGain1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DialGain2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DialGain3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DialGain4)).EndInit();
            this.grpOutputRouter.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.grpInputRouter.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.grpComm.ResumeLayout(false);
            this.grpComm.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel;
        private System.Windows.Forms.TextBox textLog;
        private System.Windows.Forms.Timer queueTimer;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem deviceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem programToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem readToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem commPortToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem debugToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem contentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem indexToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openProgramDialog;
        private System.Windows.Forms.SaveFileDialog saveProgramDialog;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem resetConfigurationToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.TabPage tabMain;
        private System.Windows.Forms.Button btnReboot;
        private System.Windows.Forms.CheckBox chkDebug;
        private System.Windows.Forms.GroupBox grpComm;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton INROUTER_1_6;
        private System.Windows.Forms.RadioButton INROUTER_1_5;
        private System.Windows.Forms.RadioButton INROUTER_1_4;
        private System.Windows.Forms.RadioButton INROUTER_1_3;
        private System.Windows.Forms.RadioButton INROUTER_1_2;
        private System.Windows.Forms.RadioButton INROUTER_1_1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton INROUTER_4_6;
        private System.Windows.Forms.RadioButton INROUTER_4_5;
        private System.Windows.Forms.RadioButton INROUTER_4_4;
        private System.Windows.Forms.RadioButton INROUTER_4_3;
        private System.Windows.Forms.RadioButton INROUTER_4_2;
        private System.Windows.Forms.RadioButton INROUTER_4_1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.RadioButton INROUTER_3_6;
        private System.Windows.Forms.RadioButton INROUTER_3_5;
        private System.Windows.Forms.RadioButton INROUTER_3_4;
        private System.Windows.Forms.RadioButton INROUTER_3_3;
        private System.Windows.Forms.RadioButton INROUTER_3_2;
        private System.Windows.Forms.RadioButton INROUTER_3_1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton INROUTER_2_6;
        private System.Windows.Forms.RadioButton INROUTER_2_5;
        private System.Windows.Forms.RadioButton INROUTER_2_4;
        private System.Windows.Forms.RadioButton INROUTER_2_3;
        private System.Windows.Forms.RadioButton INROUTER_2_2;
        private System.Windows.Forms.RadioButton INROUTER_2_1;
        private System.Windows.Forms.GroupBox grpInputRouter;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.GroupBox grpOutputRouter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.RadioButton OUTROUTER_1_4;
        private System.Windows.Forms.RadioButton OUTROUTER_1_3;
        private System.Windows.Forms.RadioButton OUTROUTER_1_2;
        private System.Windows.Forms.RadioButton OUTROUTER_1_1;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.RadioButton OUTROUTER_4_4;
        private System.Windows.Forms.RadioButton OUTROUTER_4_3;
        private System.Windows.Forms.RadioButton OUTROUTER_4_2;
        private System.Windows.Forms.RadioButton OUTROUTER_4_1;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.RadioButton OUTROUTER_2_4;
        private System.Windows.Forms.RadioButton OUTROUTER_2_3;
        private System.Windows.Forms.RadioButton OUTROUTER_2_2;
        private System.Windows.Forms.RadioButton OUTROUTER_2_1;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.RadioButton OUTROUTER_3_4;
        private System.Windows.Forms.RadioButton OUTROUTER_3_3;
        private System.Windows.Forms.RadioButton OUTROUTER_3_2;
        private System.Windows.Forms.RadioButton OUTROUTER_3_1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TextSineFreq;
        private System.Windows.Forms.PictureBox DialSineFreq;
        private System.Windows.Forms.GroupBox grpSine;
        private System.Windows.Forms.TextBox TextSineGain;
        private System.Windows.Forms.PictureBox DialSineGain;
        private System.Windows.Forms.GroupBox grpGain;
        private System.Windows.Forms.Label lblCH1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.PictureBox DialGain1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TextGain1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox DialGain2;
        private System.Windows.Forms.TextBox TextGain2;
        private System.Windows.Forms.TextBox TextGain4;
        private System.Windows.Forms.PictureBox DialGain3;
        private System.Windows.Forms.PictureBox DialGain4;
        private System.Windows.Forms.TextBox TextGain3;
        private System.Windows.Forms.TextBox TextPinkGain;
        private System.Windows.Forms.PictureBox DialPinkGain;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox dropAmpMode;
    }
}

