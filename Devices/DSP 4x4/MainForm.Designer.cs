using SA_Resources;
using SA_Resources.SAControls;

namespace DSP_4x4
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deviceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnGain33 = new SA_Resources.SAControls.PictureButton();
            this.btnCH4Delay = new SA_Resources.SAControls.PictureButton();
            this.btnCompressor31 = new SA_Resources.SAControls.PictureButton();
            this.btnGain32 = new SA_Resources.SAControls.PictureButton();
            this.btnCH3PostFilters = new SA_Resources.SAControls.PictureButton();
            this.btnGain23 = new SA_Resources.SAControls.PictureButton();
            this.btnCH3Delay = new SA_Resources.SAControls.PictureButton();
            this.btnCompressor21 = new SA_Resources.SAControls.PictureButton();
            this.btnGain22 = new SA_Resources.SAControls.PictureButton();
            this.btnCH2PostFilters = new SA_Resources.SAControls.PictureButton();
            this.btnGain13 = new SA_Resources.SAControls.PictureButton();
            this.btnCH2Delay = new SA_Resources.SAControls.PictureButton();
            this.btnCompressor11 = new SA_Resources.SAControls.PictureButton();
            this.btnGain12 = new SA_Resources.SAControls.PictureButton();
            this.btnCH1PostFilters = new SA_Resources.SAControls.PictureButton();
            this.pictureBox37 = new System.Windows.Forms.PictureBox();
            this.btnGain03 = new SA_Resources.SAControls.PictureButton();
            this.btnCH1Delay = new SA_Resources.SAControls.PictureButton();
            this.btnCompressor01 = new SA_Resources.SAControls.PictureButton();
            this.btnGain02 = new SA_Resources.SAControls.PictureButton();
            this.btnCH0PostFilters = new SA_Resources.SAControls.PictureButton();
            this.btnGain30 = new SA_Resources.SAControls.PictureButton();
            this.btnCH3PreFilters = new SA_Resources.SAControls.PictureButton();
            this.btnCompressor30 = new SA_Resources.SAControls.PictureButton();
            this.btnGain31 = new SA_Resources.SAControls.PictureButton();
            this.btnGain20 = new SA_Resources.SAControls.PictureButton();
            this.btnCH2PreFilters = new SA_Resources.SAControls.PictureButton();
            this.btnCompressor20 = new SA_Resources.SAControls.PictureButton();
            this.btnGain21 = new SA_Resources.SAControls.PictureButton();
            this.btnGain00 = new SA_Resources.SAControls.PictureButton();
            this.btnCH0PreFilters = new SA_Resources.SAControls.PictureButton();
            this.btnCompressor00 = new SA_Resources.SAControls.PictureButton();
            this.btnGain01 = new SA_Resources.SAControls.PictureButton();
            this.saveProgramDialog = new System.Windows.Forms.SaveFileDialog();
            this.openProgramDialog = new System.Windows.Forms.OpenFileDialog();
            this.pnlCH2PreMixer = new System.Windows.Forms.Panel();
            this.btnGain10 = new SA_Resources.SAControls.PictureButton();
            this.btnCH1PreFilters = new SA_Resources.SAControls.PictureButton();
            this.btnCompressor10 = new SA_Resources.SAControls.PictureButton();
            this.btnGain11 = new SA_Resources.SAControls.PictureButton();
            this.btnMatrixMixer = new SA_Resources.SAControls.PictureButton();
            this.lblCH4Output = new System.Windows.Forms.Label();
            this.lblCH3Output = new System.Windows.Forms.Label();
            this.lblCH2Output = new System.Windows.Forms.Label();
            this.pnlCH4PostMixer = new System.Windows.Forms.Panel();
            this.pnlCH3PostMixer = new System.Windows.Forms.Panel();
            this.pnlCH2PostMixer = new System.Windows.Forms.Panel();
            this.lblCH1Output = new System.Windows.Forms.Label();
            this.pnlCH1PostMixer = new System.Windows.Forms.Panel();
            this.pnlCH4PreMixer = new System.Windows.Forms.Panel();
            this.pnlCH3PreMixer = new System.Windows.Forms.Panel();
            this.pnlCH1PreMixer = new System.Windows.Forms.Panel();
            this.lblCH4Input = new System.Windows.Forms.Label();
            this.lblCH3Input = new System.Windows.Forms.Label();
            this.lblCH2Input = new System.Windows.Forms.Label();
            this.lblCH1Input = new System.Windows.Forms.Label();
            this.picDuckerLine = new System.Windows.Forms.PictureBox();
            this.pbtnDucker = new SA_Resources.SAControls.PictureButton();
            this.btnSavetoFile = new System.Windows.Forms.Button();
            this.btnStreamRead = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureConnectionStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox37)).BeginInit();
            this.pnlCH2PreMixer.SuspendLayout();
            this.pnlCH4PostMixer.SuspendLayout();
            this.pnlCH3PostMixer.SuspendLayout();
            this.pnlCH2PostMixer.SuspendLayout();
            this.pnlCH1PostMixer.SuspendLayout();
            this.pnlCH4PreMixer.SuspendLayout();
            this.pnlCH3PreMixer.SuspendLayout();
            this.pnlCH1PreMixer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDuckerLine)).BeginInit();
            this.SuspendLayout();
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 5000;
            this.toolTip1.InitialDelay = 10;
            this.toolTip1.ReshowDelay = 50;
            this.toolTip1.ShowAlways = true;
            // 
            // serialPort1
            // 
            this.serialPort1.BaudRate = 19200;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 382);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(906, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // tsStatusLabel
            // 
            this.tsStatusLabel.BackColor = System.Drawing.Color.Transparent;
            this.tsStatusLabel.Name = "tsStatusLabel";
            this.tsStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.fileToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(38, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // deviceToolStripMenuItem
            // 
            this.deviceToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.deviceToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.deviceToolStripMenuItem.Name = "deviceToolStripMenuItem";
            this.deviceToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            this.deviceToolStripMenuItem.Text = "Device";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.settingsToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.helpToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // btnGain33
            // 
            this.btnGain33.AutoResize = false;
            this.btnGain33.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnGain33.BackgroundImage")));
            this.btnGain33.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGain33.Location = new System.Drawing.Point(270, 0);
            this.btnGain33.Name = "btnGain33";
            this.btnGain33.OverImage = null;
            this.btnGain33.Overlay1Image = ((System.Drawing.Image)(resources.GetObject("btnGain33.Overlay1Image")));
            this.btnGain33.Overlay1Visible = false;
            this.btnGain33.Overlay2Image = ((System.Drawing.Image)(resources.GetObject("btnGain33.Overlay2Image")));
            this.btnGain33.Overlay2Visible = false;
            this.btnGain33.Overlay3Image = null;
            this.btnGain33.Overlay3Visible = false;
            this.btnGain33.PressedImage = ((System.Drawing.Image)(resources.GetObject("btnGain33.PressedImage")));
            this.btnGain33.Size = new System.Drawing.Size(39, 39);
            this.btnGain33.TabIndex = 31;
            this.btnGain33.ToolTipText = "";
            // 
            // btnCH4Delay
            // 
            this.btnCH4Delay.AutoResize = false;
            this.btnCH4Delay.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCH4Delay.BackgroundImage")));
            this.btnCH4Delay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCH4Delay.Location = new System.Drawing.Point(210, 0);
            this.btnCH4Delay.Name = "btnCH4Delay";
            this.btnCH4Delay.OverImage = null;
            this.btnCH4Delay.Overlay1Image = ((System.Drawing.Image)(resources.GetObject("btnCH4Delay.Overlay1Image")));
            this.btnCH4Delay.Overlay1Visible = false;
            this.btnCH4Delay.Overlay2Image = ((System.Drawing.Image)(resources.GetObject("btnCH4Delay.Overlay2Image")));
            this.btnCH4Delay.Overlay2Visible = false;
            this.btnCH4Delay.Overlay3Image = null;
            this.btnCH4Delay.Overlay3Visible = false;
            this.btnCH4Delay.PressedImage = ((System.Drawing.Image)(resources.GetObject("btnCH4Delay.PressedImage")));
            this.btnCH4Delay.Size = new System.Drawing.Size(39, 39);
            this.btnCH4Delay.TabIndex = 30;
            this.btnCH4Delay.ToolTipText = "";
            // 
            // btnCompressor31
            // 
            this.btnCompressor31.AutoResize = false;
            this.btnCompressor31.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCompressor31.BackgroundImage")));
            this.btnCompressor31.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCompressor31.Location = new System.Drawing.Point(150, 0);
            this.btnCompressor31.Name = "btnCompressor31";
            this.btnCompressor31.OverImage = null;
            this.btnCompressor31.Overlay1Image = ((System.Drawing.Image)(resources.GetObject("btnCompressor31.Overlay1Image")));
            this.btnCompressor31.Overlay1Visible = true;
            this.btnCompressor31.Overlay2Image = ((System.Drawing.Image)(resources.GetObject("btnCompressor31.Overlay2Image")));
            this.btnCompressor31.Overlay2Visible = false;
            this.btnCompressor31.Overlay3Image = null;
            this.btnCompressor31.Overlay3Visible = false;
            this.btnCompressor31.PressedImage = ((System.Drawing.Image)(resources.GetObject("btnCompressor31.PressedImage")));
            this.btnCompressor31.Size = new System.Drawing.Size(39, 39);
            this.btnCompressor31.TabIndex = 29;
            this.btnCompressor31.ToolTipText = "";
            // 
            // btnGain32
            // 
            this.btnGain32.AutoResize = false;
            this.btnGain32.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnGain32.BackgroundImage")));
            this.btnGain32.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGain32.Location = new System.Drawing.Point(30, 0);
            this.btnGain32.Name = "btnGain32";
            this.btnGain32.OverImage = null;
            this.btnGain32.Overlay1Image = ((System.Drawing.Image)(resources.GetObject("btnGain32.Overlay1Image")));
            this.btnGain32.Overlay1Visible = false;
            this.btnGain32.Overlay2Image = ((System.Drawing.Image)(resources.GetObject("btnGain32.Overlay2Image")));
            this.btnGain32.Overlay2Visible = false;
            this.btnGain32.Overlay3Image = null;
            this.btnGain32.Overlay3Visible = false;
            this.btnGain32.PressedImage = ((System.Drawing.Image)(resources.GetObject("btnGain32.PressedImage")));
            this.btnGain32.Size = new System.Drawing.Size(39, 39);
            this.btnGain32.TabIndex = 28;
            this.btnGain32.ToolTipText = "";
            // 
            // btnCH3PostFilters
            // 
            this.btnCH3PostFilters.AutoResize = false;
            this.btnCH3PostFilters.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCH3PostFilters.BackgroundImage")));
            this.btnCH3PostFilters.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCH3PostFilters.Location = new System.Drawing.Point(90, 0);
            this.btnCH3PostFilters.Name = "btnCH3PostFilters";
            this.btnCH3PostFilters.OverImage = null;
            this.btnCH3PostFilters.Overlay1Image = ((System.Drawing.Image)(resources.GetObject("btnCH3PostFilters.Overlay1Image")));
            this.btnCH3PostFilters.Overlay1Visible = false;
            this.btnCH3PostFilters.Overlay2Image = ((System.Drawing.Image)(resources.GetObject("btnCH3PostFilters.Overlay2Image")));
            this.btnCH3PostFilters.Overlay2Visible = false;
            this.btnCH3PostFilters.Overlay3Image = null;
            this.btnCH3PostFilters.Overlay3Visible = false;
            this.btnCH3PostFilters.PressedImage = ((System.Drawing.Image)(resources.GetObject("btnCH3PostFilters.PressedImage")));
            this.btnCH3PostFilters.Size = new System.Drawing.Size(39, 39);
            this.btnCH3PostFilters.TabIndex = 27;
            this.btnCH3PostFilters.ToolTipText = "";
            // 
            // btnGain23
            // 
            this.btnGain23.AutoResize = false;
            this.btnGain23.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnGain23.BackgroundImage")));
            this.btnGain23.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGain23.Location = new System.Drawing.Point(270, 0);
            this.btnGain23.Name = "btnGain23";
            this.btnGain23.OverImage = null;
            this.btnGain23.Overlay1Image = ((System.Drawing.Image)(resources.GetObject("btnGain23.Overlay1Image")));
            this.btnGain23.Overlay1Visible = false;
            this.btnGain23.Overlay2Image = ((System.Drawing.Image)(resources.GetObject("btnGain23.Overlay2Image")));
            this.btnGain23.Overlay2Visible = false;
            this.btnGain23.Overlay3Image = null;
            this.btnGain23.Overlay3Visible = false;
            this.btnGain23.PressedImage = ((System.Drawing.Image)(resources.GetObject("btnGain23.PressedImage")));
            this.btnGain23.Size = new System.Drawing.Size(39, 39);
            this.btnGain23.TabIndex = 31;
            this.btnGain23.ToolTipText = "";
            // 
            // btnCH3Delay
            // 
            this.btnCH3Delay.AutoResize = false;
            this.btnCH3Delay.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCH3Delay.BackgroundImage")));
            this.btnCH3Delay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCH3Delay.Location = new System.Drawing.Point(210, 0);
            this.btnCH3Delay.Name = "btnCH3Delay";
            this.btnCH3Delay.OverImage = null;
            this.btnCH3Delay.Overlay1Image = ((System.Drawing.Image)(resources.GetObject("btnCH3Delay.Overlay1Image")));
            this.btnCH3Delay.Overlay1Visible = false;
            this.btnCH3Delay.Overlay2Image = ((System.Drawing.Image)(resources.GetObject("btnCH3Delay.Overlay2Image")));
            this.btnCH3Delay.Overlay2Visible = false;
            this.btnCH3Delay.Overlay3Image = null;
            this.btnCH3Delay.Overlay3Visible = false;
            this.btnCH3Delay.PressedImage = ((System.Drawing.Image)(resources.GetObject("btnCH3Delay.PressedImage")));
            this.btnCH3Delay.Size = new System.Drawing.Size(39, 39);
            this.btnCH3Delay.TabIndex = 30;
            this.btnCH3Delay.ToolTipText = "";
            // 
            // btnCompressor21
            // 
            this.btnCompressor21.AutoResize = false;
            this.btnCompressor21.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCompressor21.BackgroundImage")));
            this.btnCompressor21.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCompressor21.Location = new System.Drawing.Point(150, 0);
            this.btnCompressor21.Name = "btnCompressor21";
            this.btnCompressor21.OverImage = null;
            this.btnCompressor21.Overlay1Image = ((System.Drawing.Image)(resources.GetObject("btnCompressor21.Overlay1Image")));
            this.btnCompressor21.Overlay1Visible = true;
            this.btnCompressor21.Overlay2Image = ((System.Drawing.Image)(resources.GetObject("btnCompressor21.Overlay2Image")));
            this.btnCompressor21.Overlay2Visible = false;
            this.btnCompressor21.Overlay3Image = null;
            this.btnCompressor21.Overlay3Visible = false;
            this.btnCompressor21.PressedImage = ((System.Drawing.Image)(resources.GetObject("btnCompressor21.PressedImage")));
            this.btnCompressor21.Size = new System.Drawing.Size(39, 39);
            this.btnCompressor21.TabIndex = 29;
            this.btnCompressor21.ToolTipText = "";
            // 
            // btnGain22
            // 
            this.btnGain22.AutoResize = false;
            this.btnGain22.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnGain22.BackgroundImage")));
            this.btnGain22.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGain22.Location = new System.Drawing.Point(30, 0);
            this.btnGain22.Name = "btnGain22";
            this.btnGain22.OverImage = null;
            this.btnGain22.Overlay1Image = ((System.Drawing.Image)(resources.GetObject("btnGain22.Overlay1Image")));
            this.btnGain22.Overlay1Visible = false;
            this.btnGain22.Overlay2Image = ((System.Drawing.Image)(resources.GetObject("btnGain22.Overlay2Image")));
            this.btnGain22.Overlay2Visible = false;
            this.btnGain22.Overlay3Image = null;
            this.btnGain22.Overlay3Visible = false;
            this.btnGain22.PressedImage = ((System.Drawing.Image)(resources.GetObject("btnGain22.PressedImage")));
            this.btnGain22.Size = new System.Drawing.Size(39, 39);
            this.btnGain22.TabIndex = 28;
            this.btnGain22.ToolTipText = "";
            // 
            // btnCH2PostFilters
            // 
            this.btnCH2PostFilters.AutoResize = false;
            this.btnCH2PostFilters.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCH2PostFilters.BackgroundImage")));
            this.btnCH2PostFilters.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCH2PostFilters.Location = new System.Drawing.Point(90, 0);
            this.btnCH2PostFilters.Name = "btnCH2PostFilters";
            this.btnCH2PostFilters.OverImage = null;
            this.btnCH2PostFilters.Overlay1Image = ((System.Drawing.Image)(resources.GetObject("btnCH2PostFilters.Overlay1Image")));
            this.btnCH2PostFilters.Overlay1Visible = false;
            this.btnCH2PostFilters.Overlay2Image = ((System.Drawing.Image)(resources.GetObject("btnCH2PostFilters.Overlay2Image")));
            this.btnCH2PostFilters.Overlay2Visible = false;
            this.btnCH2PostFilters.Overlay3Image = null;
            this.btnCH2PostFilters.Overlay3Visible = false;
            this.btnCH2PostFilters.PressedImage = ((System.Drawing.Image)(resources.GetObject("btnCH2PostFilters.PressedImage")));
            this.btnCH2PostFilters.Size = new System.Drawing.Size(39, 39);
            this.btnCH2PostFilters.TabIndex = 27;
            this.btnCH2PostFilters.ToolTipText = "";
            // 
            // btnGain13
            // 
            this.btnGain13.AutoResize = false;
            this.btnGain13.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnGain13.BackgroundImage")));
            this.btnGain13.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGain13.Location = new System.Drawing.Point(270, 0);
            this.btnGain13.Name = "btnGain13";
            this.btnGain13.OverImage = null;
            this.btnGain13.Overlay1Image = ((System.Drawing.Image)(resources.GetObject("btnGain13.Overlay1Image")));
            this.btnGain13.Overlay1Visible = false;
            this.btnGain13.Overlay2Image = ((System.Drawing.Image)(resources.GetObject("btnGain13.Overlay2Image")));
            this.btnGain13.Overlay2Visible = false;
            this.btnGain13.Overlay3Image = null;
            this.btnGain13.Overlay3Visible = false;
            this.btnGain13.PressedImage = ((System.Drawing.Image)(resources.GetObject("btnGain13.PressedImage")));
            this.btnGain13.Size = new System.Drawing.Size(39, 39);
            this.btnGain13.TabIndex = 31;
            this.btnGain13.ToolTipText = "";
            // 
            // btnCH2Delay
            // 
            this.btnCH2Delay.AutoResize = false;
            this.btnCH2Delay.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCH2Delay.BackgroundImage")));
            this.btnCH2Delay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCH2Delay.Location = new System.Drawing.Point(210, 0);
            this.btnCH2Delay.Name = "btnCH2Delay";
            this.btnCH2Delay.OverImage = null;
            this.btnCH2Delay.Overlay1Image = ((System.Drawing.Image)(resources.GetObject("btnCH2Delay.Overlay1Image")));
            this.btnCH2Delay.Overlay1Visible = false;
            this.btnCH2Delay.Overlay2Image = ((System.Drawing.Image)(resources.GetObject("btnCH2Delay.Overlay2Image")));
            this.btnCH2Delay.Overlay2Visible = false;
            this.btnCH2Delay.Overlay3Image = null;
            this.btnCH2Delay.Overlay3Visible = false;
            this.btnCH2Delay.PressedImage = ((System.Drawing.Image)(resources.GetObject("btnCH2Delay.PressedImage")));
            this.btnCH2Delay.Size = new System.Drawing.Size(39, 39);
            this.btnCH2Delay.TabIndex = 30;
            this.btnCH2Delay.ToolTipText = "";
            // 
            // btnCompressor11
            // 
            this.btnCompressor11.AutoResize = false;
            this.btnCompressor11.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCompressor11.BackgroundImage")));
            this.btnCompressor11.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCompressor11.Location = new System.Drawing.Point(150, 0);
            this.btnCompressor11.Name = "btnCompressor11";
            this.btnCompressor11.OverImage = null;
            this.btnCompressor11.Overlay1Image = ((System.Drawing.Image)(resources.GetObject("btnCompressor11.Overlay1Image")));
            this.btnCompressor11.Overlay1Visible = true;
            this.btnCompressor11.Overlay2Image = ((System.Drawing.Image)(resources.GetObject("btnCompressor11.Overlay2Image")));
            this.btnCompressor11.Overlay2Visible = false;
            this.btnCompressor11.Overlay3Image = null;
            this.btnCompressor11.Overlay3Visible = false;
            this.btnCompressor11.PressedImage = ((System.Drawing.Image)(resources.GetObject("btnCompressor11.PressedImage")));
            this.btnCompressor11.Size = new System.Drawing.Size(39, 39);
            this.btnCompressor11.TabIndex = 29;
            this.btnCompressor11.ToolTipText = "";
            // 
            // btnGain12
            // 
            this.btnGain12.AutoResize = false;
            this.btnGain12.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnGain12.BackgroundImage")));
            this.btnGain12.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGain12.Location = new System.Drawing.Point(30, 0);
            this.btnGain12.Name = "btnGain12";
            this.btnGain12.OverImage = null;
            this.btnGain12.Overlay1Image = ((System.Drawing.Image)(resources.GetObject("btnGain12.Overlay1Image")));
            this.btnGain12.Overlay1Visible = false;
            this.btnGain12.Overlay2Image = ((System.Drawing.Image)(resources.GetObject("btnGain12.Overlay2Image")));
            this.btnGain12.Overlay2Visible = false;
            this.btnGain12.Overlay3Image = null;
            this.btnGain12.Overlay3Visible = false;
            this.btnGain12.PressedImage = ((System.Drawing.Image)(resources.GetObject("btnGain12.PressedImage")));
            this.btnGain12.Size = new System.Drawing.Size(39, 39);
            this.btnGain12.TabIndex = 28;
            this.btnGain12.ToolTipText = "";
            // 
            // btnCH1PostFilters
            // 
            this.btnCH1PostFilters.AutoResize = false;
            this.btnCH1PostFilters.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCH1PostFilters.BackgroundImage")));
            this.btnCH1PostFilters.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCH1PostFilters.Location = new System.Drawing.Point(90, 0);
            this.btnCH1PostFilters.Name = "btnCH1PostFilters";
            this.btnCH1PostFilters.OverImage = null;
            this.btnCH1PostFilters.Overlay1Image = ((System.Drawing.Image)(resources.GetObject("btnCH1PostFilters.Overlay1Image")));
            this.btnCH1PostFilters.Overlay1Visible = false;
            this.btnCH1PostFilters.Overlay2Image = ((System.Drawing.Image)(resources.GetObject("btnCH1PostFilters.Overlay2Image")));
            this.btnCH1PostFilters.Overlay2Visible = false;
            this.btnCH1PostFilters.Overlay3Image = null;
            this.btnCH1PostFilters.Overlay3Visible = false;
            this.btnCH1PostFilters.PressedImage = ((System.Drawing.Image)(resources.GetObject("btnCH1PostFilters.PressedImage")));
            this.btnCH1PostFilters.Size = new System.Drawing.Size(39, 39);
            this.btnCH1PostFilters.TabIndex = 27;
            this.btnCH1PostFilters.ToolTipText = "";
            // 
            // pictureBox37
            // 
            this.pictureBox37.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox37.Image")));
            this.pictureBox37.Location = new System.Drawing.Point(644, 292);
            this.pictureBox37.Name = "pictureBox37";
            this.pictureBox37.Size = new System.Drawing.Size(248, 63);
            this.pictureBox37.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox37.TabIndex = 24;
            this.pictureBox37.TabStop = false;
            // 
            // btnGain03
            // 
            this.btnGain03.AutoResize = false;
            this.btnGain03.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnGain03.BackgroundImage")));
            this.btnGain03.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGain03.Location = new System.Drawing.Point(270, 0);
            this.btnGain03.Name = "btnGain03";
            this.btnGain03.OverImage = null;
            this.btnGain03.Overlay1Image = ((System.Drawing.Image)(resources.GetObject("btnGain03.Overlay1Image")));
            this.btnGain03.Overlay1Visible = false;
            this.btnGain03.Overlay2Image = ((System.Drawing.Image)(resources.GetObject("btnGain03.Overlay2Image")));
            this.btnGain03.Overlay2Visible = false;
            this.btnGain03.Overlay3Image = null;
            this.btnGain03.Overlay3Visible = false;
            this.btnGain03.PressedImage = ((System.Drawing.Image)(resources.GetObject("btnGain03.PressedImage")));
            this.btnGain03.Size = new System.Drawing.Size(39, 39);
            this.btnGain03.TabIndex = 31;
            this.btnGain03.ToolTipText = "";
            // 
            // btnCH1Delay
            // 
            this.btnCH1Delay.AutoResize = false;
            this.btnCH1Delay.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCH1Delay.BackgroundImage")));
            this.btnCH1Delay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCH1Delay.Location = new System.Drawing.Point(210, 0);
            this.btnCH1Delay.Name = "btnCH1Delay";
            this.btnCH1Delay.OverImage = null;
            this.btnCH1Delay.Overlay1Image = ((System.Drawing.Image)(resources.GetObject("btnCH1Delay.Overlay1Image")));
            this.btnCH1Delay.Overlay1Visible = false;
            this.btnCH1Delay.Overlay2Image = ((System.Drawing.Image)(resources.GetObject("btnCH1Delay.Overlay2Image")));
            this.btnCH1Delay.Overlay2Visible = false;
            this.btnCH1Delay.Overlay3Image = null;
            this.btnCH1Delay.Overlay3Visible = false;
            this.btnCH1Delay.PressedImage = ((System.Drawing.Image)(resources.GetObject("btnCH1Delay.PressedImage")));
            this.btnCH1Delay.Size = new System.Drawing.Size(39, 39);
            this.btnCH1Delay.TabIndex = 30;
            this.btnCH1Delay.ToolTipText = "";
            // 
            // btnCompressor01
            // 
            this.btnCompressor01.AutoResize = false;
            this.btnCompressor01.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCompressor01.BackgroundImage")));
            this.btnCompressor01.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCompressor01.Location = new System.Drawing.Point(150, 0);
            this.btnCompressor01.Name = "btnCompressor01";
            this.btnCompressor01.OverImage = null;
            this.btnCompressor01.Overlay1Image = ((System.Drawing.Image)(resources.GetObject("btnCompressor01.Overlay1Image")));
            this.btnCompressor01.Overlay1Visible = true;
            this.btnCompressor01.Overlay2Image = ((System.Drawing.Image)(resources.GetObject("btnCompressor01.Overlay2Image")));
            this.btnCompressor01.Overlay2Visible = false;
            this.btnCompressor01.Overlay3Image = null;
            this.btnCompressor01.Overlay3Visible = false;
            this.btnCompressor01.PressedImage = ((System.Drawing.Image)(resources.GetObject("btnCompressor01.PressedImage")));
            this.btnCompressor01.Size = new System.Drawing.Size(39, 39);
            this.btnCompressor01.TabIndex = 29;
            this.btnCompressor01.ToolTipText = "";
            // 
            // btnGain02
            // 
            this.btnGain02.AutoResize = false;
            this.btnGain02.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnGain02.BackgroundImage")));
            this.btnGain02.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGain02.Location = new System.Drawing.Point(30, 0);
            this.btnGain02.Name = "btnGain02";
            this.btnGain02.OverImage = null;
            this.btnGain02.Overlay1Image = ((System.Drawing.Image)(resources.GetObject("btnGain02.Overlay1Image")));
            this.btnGain02.Overlay1Visible = false;
            this.btnGain02.Overlay2Image = ((System.Drawing.Image)(resources.GetObject("btnGain02.Overlay2Image")));
            this.btnGain02.Overlay2Visible = false;
            this.btnGain02.Overlay3Image = null;
            this.btnGain02.Overlay3Visible = false;
            this.btnGain02.PressedImage = ((System.Drawing.Image)(resources.GetObject("btnGain02.PressedImage")));
            this.btnGain02.Size = new System.Drawing.Size(39, 39);
            this.btnGain02.TabIndex = 28;
            this.btnGain02.ToolTipText = "";
            // 
            // btnCH0PostFilters
            // 
            this.btnCH0PostFilters.AutoResize = false;
            this.btnCH0PostFilters.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCH0PostFilters.BackgroundImage")));
            this.btnCH0PostFilters.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCH0PostFilters.Location = new System.Drawing.Point(90, 0);
            this.btnCH0PostFilters.Name = "btnCH0PostFilters";
            this.btnCH0PostFilters.OverImage = null;
            this.btnCH0PostFilters.Overlay1Image = ((System.Drawing.Image)(resources.GetObject("btnCH0PostFilters.Overlay1Image")));
            this.btnCH0PostFilters.Overlay1Visible = false;
            this.btnCH0PostFilters.Overlay2Image = ((System.Drawing.Image)(resources.GetObject("btnCH0PostFilters.Overlay2Image")));
            this.btnCH0PostFilters.Overlay2Visible = false;
            this.btnCH0PostFilters.Overlay3Image = null;
            this.btnCH0PostFilters.Overlay3Visible = false;
            this.btnCH0PostFilters.PressedImage = ((System.Drawing.Image)(resources.GetObject("btnCH0PostFilters.PressedImage")));
            this.btnCH0PostFilters.Size = new System.Drawing.Size(39, 39);
            this.btnCH0PostFilters.TabIndex = 27;
            this.btnCH0PostFilters.ToolTipText = "";
            // 
            // btnGain30
            // 
            this.btnGain30.AutoResize = false;
            this.btnGain30.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnGain30.BackgroundImage")));
            this.btnGain30.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGain30.Location = new System.Drawing.Point(30, 0);
            this.btnGain30.Name = "btnGain30";
            this.btnGain30.OverImage = null;
            this.btnGain30.Overlay1Image = ((System.Drawing.Image)(resources.GetObject("btnGain30.Overlay1Image")));
            this.btnGain30.Overlay1Visible = false;
            this.btnGain30.Overlay2Image = ((System.Drawing.Image)(resources.GetObject("btnGain30.Overlay2Image")));
            this.btnGain30.Overlay2Visible = false;
            this.btnGain30.Overlay3Image = null;
            this.btnGain30.Overlay3Visible = false;
            this.btnGain30.PressedImage = ((System.Drawing.Image)(resources.GetObject("btnGain30.PressedImage")));
            this.btnGain30.Size = new System.Drawing.Size(39, 39);
            this.btnGain30.TabIndex = 36;
            this.btnGain30.ToolTipText = "";
            // 
            // btnCH3PreFilters
            // 
            this.btnCH3PreFilters.AutoResize = false;
            this.btnCH3PreFilters.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCH3PreFilters.BackgroundImage")));
            this.btnCH3PreFilters.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCH3PreFilters.Location = new System.Drawing.Point(90, 0);
            this.btnCH3PreFilters.Name = "btnCH3PreFilters";
            this.btnCH3PreFilters.OverImage = null;
            this.btnCH3PreFilters.Overlay1Image = ((System.Drawing.Image)(resources.GetObject("btnCH3PreFilters.Overlay1Image")));
            this.btnCH3PreFilters.Overlay1Visible = false;
            this.btnCH3PreFilters.Overlay2Image = ((System.Drawing.Image)(resources.GetObject("btnCH3PreFilters.Overlay2Image")));
            this.btnCH3PreFilters.Overlay2Visible = false;
            this.btnCH3PreFilters.Overlay3Image = null;
            this.btnCH3PreFilters.Overlay3Visible = false;
            this.btnCH3PreFilters.PressedImage = ((System.Drawing.Image)(resources.GetObject("btnCH3PreFilters.PressedImage")));
            this.btnCH3PreFilters.Size = new System.Drawing.Size(39, 39);
            this.btnCH3PreFilters.TabIndex = 35;
            this.btnCH3PreFilters.ToolTipText = "";
            // 
            // btnCompressor30
            // 
            this.btnCompressor30.AutoResize = false;
            this.btnCompressor30.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCompressor30.BackgroundImage")));
            this.btnCompressor30.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCompressor30.Location = new System.Drawing.Point(152, 0);
            this.btnCompressor30.Name = "btnCompressor30";
            this.btnCompressor30.OverImage = null;
            this.btnCompressor30.Overlay1Image = ((System.Drawing.Image)(resources.GetObject("btnCompressor30.Overlay1Image")));
            this.btnCompressor30.Overlay1Visible = true;
            this.btnCompressor30.Overlay2Image = ((System.Drawing.Image)(resources.GetObject("btnCompressor30.Overlay2Image")));
            this.btnCompressor30.Overlay2Visible = false;
            this.btnCompressor30.Overlay3Image = null;
            this.btnCompressor30.Overlay3Visible = false;
            this.btnCompressor30.PressedImage = ((System.Drawing.Image)(resources.GetObject("btnCompressor30.PressedImage")));
            this.btnCompressor30.Size = new System.Drawing.Size(39, 39);
            this.btnCompressor30.TabIndex = 34;
            this.btnCompressor30.ToolTipText = "";
            // 
            // btnGain31
            // 
            this.btnGain31.AutoResize = false;
            this.btnGain31.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnGain31.BackgroundImage")));
            this.btnGain31.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGain31.Location = new System.Drawing.Point(212, 0);
            this.btnGain31.Name = "btnGain31";
            this.btnGain31.OverImage = null;
            this.btnGain31.Overlay1Image = ((System.Drawing.Image)(resources.GetObject("btnGain31.Overlay1Image")));
            this.btnGain31.Overlay1Visible = false;
            this.btnGain31.Overlay2Image = ((System.Drawing.Image)(resources.GetObject("btnGain31.Overlay2Image")));
            this.btnGain31.Overlay2Visible = false;
            this.btnGain31.Overlay3Image = null;
            this.btnGain31.Overlay3Visible = false;
            this.btnGain31.PressedImage = ((System.Drawing.Image)(resources.GetObject("btnGain31.PressedImage")));
            this.btnGain31.Size = new System.Drawing.Size(39, 39);
            this.btnGain31.TabIndex = 33;
            this.btnGain31.ToolTipText = "";
            // 
            // btnGain20
            // 
            this.btnGain20.AutoResize = false;
            this.btnGain20.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnGain20.BackgroundImage")));
            this.btnGain20.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGain20.Location = new System.Drawing.Point(30, 0);
            this.btnGain20.Name = "btnGain20";
            this.btnGain20.OverImage = null;
            this.btnGain20.Overlay1Image = ((System.Drawing.Image)(resources.GetObject("btnGain20.Overlay1Image")));
            this.btnGain20.Overlay1Visible = false;
            this.btnGain20.Overlay2Image = ((System.Drawing.Image)(resources.GetObject("btnGain20.Overlay2Image")));
            this.btnGain20.Overlay2Visible = false;
            this.btnGain20.Overlay3Image = null;
            this.btnGain20.Overlay3Visible = false;
            this.btnGain20.PressedImage = ((System.Drawing.Image)(resources.GetObject("btnGain20.PressedImage")));
            this.btnGain20.Size = new System.Drawing.Size(39, 39);
            this.btnGain20.TabIndex = 36;
            this.btnGain20.ToolTipText = "";
            // 
            // btnCH2PreFilters
            // 
            this.btnCH2PreFilters.AutoResize = false;
            this.btnCH2PreFilters.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCH2PreFilters.BackgroundImage")));
            this.btnCH2PreFilters.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCH2PreFilters.Location = new System.Drawing.Point(90, 0);
            this.btnCH2PreFilters.Name = "btnCH2PreFilters";
            this.btnCH2PreFilters.OverImage = null;
            this.btnCH2PreFilters.Overlay1Image = ((System.Drawing.Image)(resources.GetObject("btnCH2PreFilters.Overlay1Image")));
            this.btnCH2PreFilters.Overlay1Visible = false;
            this.btnCH2PreFilters.Overlay2Image = ((System.Drawing.Image)(resources.GetObject("btnCH2PreFilters.Overlay2Image")));
            this.btnCH2PreFilters.Overlay2Visible = false;
            this.btnCH2PreFilters.Overlay3Image = null;
            this.btnCH2PreFilters.Overlay3Visible = false;
            this.btnCH2PreFilters.PressedImage = ((System.Drawing.Image)(resources.GetObject("btnCH2PreFilters.PressedImage")));
            this.btnCH2PreFilters.Size = new System.Drawing.Size(39, 39);
            this.btnCH2PreFilters.TabIndex = 35;
            this.btnCH2PreFilters.ToolTipText = "";
            // 
            // btnCompressor20
            // 
            this.btnCompressor20.AutoResize = false;
            this.btnCompressor20.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCompressor20.BackgroundImage")));
            this.btnCompressor20.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCompressor20.Location = new System.Drawing.Point(152, 0);
            this.btnCompressor20.Name = "btnCompressor20";
            this.btnCompressor20.OverImage = null;
            this.btnCompressor20.Overlay1Image = ((System.Drawing.Image)(resources.GetObject("btnCompressor20.Overlay1Image")));
            this.btnCompressor20.Overlay1Visible = true;
            this.btnCompressor20.Overlay2Image = ((System.Drawing.Image)(resources.GetObject("btnCompressor20.Overlay2Image")));
            this.btnCompressor20.Overlay2Visible = false;
            this.btnCompressor20.Overlay3Image = null;
            this.btnCompressor20.Overlay3Visible = false;
            this.btnCompressor20.PressedImage = ((System.Drawing.Image)(resources.GetObject("btnCompressor20.PressedImage")));
            this.btnCompressor20.Size = new System.Drawing.Size(39, 39);
            this.btnCompressor20.TabIndex = 34;
            this.btnCompressor20.ToolTipText = "";
            // 
            // btnGain21
            // 
            this.btnGain21.AutoResize = false;
            this.btnGain21.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnGain21.BackgroundImage")));
            this.btnGain21.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGain21.Location = new System.Drawing.Point(212, 0);
            this.btnGain21.Name = "btnGain21";
            this.btnGain21.OverImage = null;
            this.btnGain21.Overlay1Image = ((System.Drawing.Image)(resources.GetObject("btnGain21.Overlay1Image")));
            this.btnGain21.Overlay1Visible = false;
            this.btnGain21.Overlay2Image = ((System.Drawing.Image)(resources.GetObject("btnGain21.Overlay2Image")));
            this.btnGain21.Overlay2Visible = false;
            this.btnGain21.Overlay3Image = null;
            this.btnGain21.Overlay3Visible = false;
            this.btnGain21.PressedImage = ((System.Drawing.Image)(resources.GetObject("btnGain21.PressedImage")));
            this.btnGain21.Size = new System.Drawing.Size(39, 39);
            this.btnGain21.TabIndex = 33;
            this.btnGain21.ToolTipText = "";
            // 
            // btnGain00
            // 
            this.btnGain00.AutoResize = false;
            this.btnGain00.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnGain00.BackgroundImage")));
            this.btnGain00.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGain00.Location = new System.Drawing.Point(30, 0);
            this.btnGain00.Name = "btnGain00";
            this.btnGain00.OverImage = null;
            this.btnGain00.Overlay1Image = ((System.Drawing.Image)(resources.GetObject("btnGain00.Overlay1Image")));
            this.btnGain00.Overlay1Visible = false;
            this.btnGain00.Overlay2Image = ((System.Drawing.Image)(resources.GetObject("btnGain00.Overlay2Image")));
            this.btnGain00.Overlay2Visible = false;
            this.btnGain00.Overlay3Image = null;
            this.btnGain00.Overlay3Visible = false;
            this.btnGain00.PressedImage = ((System.Drawing.Image)(resources.GetObject("btnGain00.PressedImage")));
            this.btnGain00.Size = new System.Drawing.Size(39, 39);
            this.btnGain00.TabIndex = 35;
            this.btnGain00.ToolTipText = "";
            // 
            // btnCH0PreFilters
            // 
            this.btnCH0PreFilters.AutoResize = false;
            this.btnCH0PreFilters.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCH0PreFilters.BackgroundImage")));
            this.btnCH0PreFilters.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCH0PreFilters.Location = new System.Drawing.Point(90, 0);
            this.btnCH0PreFilters.Name = "btnCH0PreFilters";
            this.btnCH0PreFilters.OverImage = null;
            this.btnCH0PreFilters.Overlay1Image = ((System.Drawing.Image)(resources.GetObject("btnCH0PreFilters.Overlay1Image")));
            this.btnCH0PreFilters.Overlay1Visible = false;
            this.btnCH0PreFilters.Overlay2Image = ((System.Drawing.Image)(resources.GetObject("btnCH0PreFilters.Overlay2Image")));
            this.btnCH0PreFilters.Overlay2Visible = false;
            this.btnCH0PreFilters.Overlay3Image = null;
            this.btnCH0PreFilters.Overlay3Visible = false;
            this.btnCH0PreFilters.PressedImage = ((System.Drawing.Image)(resources.GetObject("btnCH0PreFilters.PressedImage")));
            this.btnCH0PreFilters.Size = new System.Drawing.Size(39, 39);
            this.btnCH0PreFilters.TabIndex = 34;
            this.btnCH0PreFilters.ToolTipText = "Filters";
            // 
            // btnCompressor00
            // 
            this.btnCompressor00.AutoResize = false;
            this.btnCompressor00.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCompressor00.BackgroundImage")));
            this.btnCompressor00.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCompressor00.Location = new System.Drawing.Point(152, 0);
            this.btnCompressor00.Name = "btnCompressor00";
            this.btnCompressor00.OverImage = null;
            this.btnCompressor00.Overlay1Image = ((System.Drawing.Image)(resources.GetObject("btnCompressor00.Overlay1Image")));
            this.btnCompressor00.Overlay1Visible = false;
            this.btnCompressor00.Overlay2Image = ((System.Drawing.Image)(resources.GetObject("btnCompressor00.Overlay2Image")));
            this.btnCompressor00.Overlay2Visible = false;
            this.btnCompressor00.Overlay3Image = null;
            this.btnCompressor00.Overlay3Visible = false;
            this.btnCompressor00.PressedImage = ((System.Drawing.Image)(resources.GetObject("btnCompressor00.PressedImage")));
            this.btnCompressor00.Size = new System.Drawing.Size(39, 39);
            this.btnCompressor00.TabIndex = 33;
            this.btnCompressor00.ToolTipText = "Compressor";
            // 
            // btnGain01
            // 
            this.btnGain01.AutoResize = false;
            this.btnGain01.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnGain01.BackgroundImage")));
            this.btnGain01.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGain01.Location = new System.Drawing.Point(212, 0);
            this.btnGain01.Name = "btnGain01";
            this.btnGain01.OverImage = null;
            this.btnGain01.Overlay1Image = ((System.Drawing.Image)(resources.GetObject("btnGain01.Overlay1Image")));
            this.btnGain01.Overlay1Visible = false;
            this.btnGain01.Overlay2Image = ((System.Drawing.Image)(resources.GetObject("btnGain01.Overlay2Image")));
            this.btnGain01.Overlay2Visible = false;
            this.btnGain01.Overlay3Image = null;
            this.btnGain01.Overlay3Visible = false;
            this.btnGain01.PressedImage = ((System.Drawing.Image)(resources.GetObject("btnGain01.PressedImage")));
            this.btnGain01.Size = new System.Drawing.Size(39, 39);
            this.btnGain01.TabIndex = 32;
            this.btnGain01.ToolTipText = "";
            // 
            // saveProgramDialog
            // 
            this.saveProgramDialog.Filter = "Configuration Files (*.scfg)|*.scfg";
            this.saveProgramDialog.Title = "Save Configuration File";
            // 
            // openProgramDialog
            // 
            this.openProgramDialog.Filter = "Configuration Files (*.scfg)|*.scfg";
            this.openProgramDialog.Title = "Open Configuration File";
            // 
            // pnlCH2PreMixer
            // 
            this.pnlCH2PreMixer.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlCH2PreMixer.BackgroundImage")));
            this.pnlCH2PreMixer.Controls.Add(this.btnGain10);
            this.pnlCH2PreMixer.Controls.Add(this.btnCH1PreFilters);
            this.pnlCH2PreMixer.Controls.Add(this.btnCompressor10);
            this.pnlCH2PreMixer.Controls.Add(this.btnGain11);
            this.pnlCH2PreMixer.Location = new System.Drawing.Point(119, 140);
            this.pnlCH2PreMixer.Name = "pnlCH2PreMixer";
            this.pnlCH2PreMixer.Size = new System.Drawing.Size(281, 39);
            this.pnlCH2PreMixer.TabIndex = 78;
            // 
            // btnGain10
            // 
            this.btnGain10.AutoResize = false;
            this.btnGain10.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnGain10.BackgroundImage")));
            this.btnGain10.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGain10.Location = new System.Drawing.Point(30, 0);
            this.btnGain10.Name = "btnGain10";
            this.btnGain10.OverImage = null;
            this.btnGain10.Overlay1Image = ((System.Drawing.Image)(resources.GetObject("btnGain10.Overlay1Image")));
            this.btnGain10.Overlay1Visible = false;
            this.btnGain10.Overlay2Image = ((System.Drawing.Image)(resources.GetObject("btnGain10.Overlay2Image")));
            this.btnGain10.Overlay2Visible = false;
            this.btnGain10.Overlay3Image = null;
            this.btnGain10.Overlay3Visible = false;
            this.btnGain10.PressedImage = ((System.Drawing.Image)(resources.GetObject("btnGain10.PressedImage")));
            this.btnGain10.Size = new System.Drawing.Size(39, 39);
            this.btnGain10.TabIndex = 36;
            this.btnGain10.ToolTipText = "";
            // 
            // btnCH1PreFilters
            // 
            this.btnCH1PreFilters.AutoResize = false;
            this.btnCH1PreFilters.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCH1PreFilters.BackgroundImage")));
            this.btnCH1PreFilters.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCH1PreFilters.Location = new System.Drawing.Point(90, 0);
            this.btnCH1PreFilters.Name = "btnCH1PreFilters";
            this.btnCH1PreFilters.OverImage = null;
            this.btnCH1PreFilters.Overlay1Image = ((System.Drawing.Image)(resources.GetObject("btnCH1PreFilters.Overlay1Image")));
            this.btnCH1PreFilters.Overlay1Visible = false;
            this.btnCH1PreFilters.Overlay2Image = ((System.Drawing.Image)(resources.GetObject("btnCH1PreFilters.Overlay2Image")));
            this.btnCH1PreFilters.Overlay2Visible = false;
            this.btnCH1PreFilters.Overlay3Image = null;
            this.btnCH1PreFilters.Overlay3Visible = false;
            this.btnCH1PreFilters.PressedImage = ((System.Drawing.Image)(resources.GetObject("btnCH1PreFilters.PressedImage")));
            this.btnCH1PreFilters.Size = new System.Drawing.Size(39, 39);
            this.btnCH1PreFilters.TabIndex = 35;
            this.btnCH1PreFilters.ToolTipText = "";
            // 
            // btnCompressor10
            // 
            this.btnCompressor10.AutoResize = false;
            this.btnCompressor10.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCompressor10.BackgroundImage")));
            this.btnCompressor10.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCompressor10.Location = new System.Drawing.Point(152, 0);
            this.btnCompressor10.Name = "btnCompressor10";
            this.btnCompressor10.OverImage = null;
            this.btnCompressor10.Overlay1Image = ((System.Drawing.Image)(resources.GetObject("btnCompressor10.Overlay1Image")));
            this.btnCompressor10.Overlay1Visible = true;
            this.btnCompressor10.Overlay2Image = ((System.Drawing.Image)(resources.GetObject("btnCompressor10.Overlay2Image")));
            this.btnCompressor10.Overlay2Visible = false;
            this.btnCompressor10.Overlay3Image = null;
            this.btnCompressor10.Overlay3Visible = false;
            this.btnCompressor10.PressedImage = ((System.Drawing.Image)(resources.GetObject("btnCompressor10.PressedImage")));
            this.btnCompressor10.Size = new System.Drawing.Size(39, 39);
            this.btnCompressor10.TabIndex = 34;
            this.btnCompressor10.ToolTipText = "";
            // 
            // btnGain11
            // 
            this.btnGain11.AutoResize = false;
            this.btnGain11.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnGain11.BackgroundImage")));
            this.btnGain11.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGain11.Location = new System.Drawing.Point(212, 0);
            this.btnGain11.Name = "btnGain11";
            this.btnGain11.OverImage = null;
            this.btnGain11.Overlay1Image = ((System.Drawing.Image)(resources.GetObject("btnGain11.Overlay1Image")));
            this.btnGain11.Overlay1Visible = false;
            this.btnGain11.Overlay2Image = ((System.Drawing.Image)(resources.GetObject("btnGain11.Overlay2Image")));
            this.btnGain11.Overlay2Visible = false;
            this.btnGain11.Overlay3Image = null;
            this.btnGain11.Overlay3Visible = false;
            this.btnGain11.PressedImage = ((System.Drawing.Image)(resources.GetObject("btnGain11.PressedImage")));
            this.btnGain11.Size = new System.Drawing.Size(39, 39);
            this.btnGain11.TabIndex = 33;
            this.btnGain11.ToolTipText = "";
            // 
            // btnMatrixMixer
            // 
            this.btnMatrixMixer.AutoResize = true;
            this.btnMatrixMixer.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnMatrixMixer.BackgroundImage")));
            this.btnMatrixMixer.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMatrixMixer.Location = new System.Drawing.Point(400, 91);
            this.btnMatrixMixer.Name = "btnMatrixMixer";
            this.btnMatrixMixer.OverImage = null;
            this.btnMatrixMixer.Overlay1Image = null;
            this.btnMatrixMixer.Overlay1Visible = false;
            this.btnMatrixMixer.Overlay2Image = null;
            this.btnMatrixMixer.Overlay2Visible = false;
            this.btnMatrixMixer.Overlay3Image = null;
            this.btnMatrixMixer.Overlay3Visible = false;
            this.btnMatrixMixer.PressedImage = null;
            this.btnMatrixMixer.Size = new System.Drawing.Size(54, 184);
            this.btnMatrixMixer.TabIndex = 77;
            this.btnMatrixMixer.ToolTipText = "";
            // 
            // lblCH4Output
            // 
            this.lblCH4Output.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lblCH4Output.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblCH4Output.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCH4Output.ForeColor = System.Drawing.Color.White;
            this.lblCH4Output.Location = new System.Drawing.Point(783, 244);
            this.lblCH4Output.Name = "lblCH4Output";
            this.lblCH4Output.Size = new System.Drawing.Size(111, 23);
            this.lblCH4Output.TabIndex = 73;
            this.lblCH4Output.Text = "Output #4";
            this.lblCH4Output.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCH3Output
            // 
            this.lblCH3Output.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lblCH3Output.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblCH3Output.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCH3Output.ForeColor = System.Drawing.Color.White;
            this.lblCH3Output.Location = new System.Drawing.Point(783, 196);
            this.lblCH3Output.Name = "lblCH3Output";
            this.lblCH3Output.Size = new System.Drawing.Size(111, 23);
            this.lblCH3Output.TabIndex = 72;
            this.lblCH3Output.Text = "Output #3";
            this.lblCH3Output.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCH2Output
            // 
            this.lblCH2Output.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lblCH2Output.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblCH2Output.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCH2Output.ForeColor = System.Drawing.Color.White;
            this.lblCH2Output.Location = new System.Drawing.Point(783, 148);
            this.lblCH2Output.Name = "lblCH2Output";
            this.lblCH2Output.Size = new System.Drawing.Size(111, 23);
            this.lblCH2Output.TabIndex = 71;
            this.lblCH2Output.Text = "Output #2";
            this.lblCH2Output.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlCH4PostMixer
            // 
            this.pnlCH4PostMixer.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlCH4PostMixer.BackgroundImage")));
            this.pnlCH4PostMixer.Controls.Add(this.btnGain32);
            this.pnlCH4PostMixer.Controls.Add(this.btnCH3PostFilters);
            this.pnlCH4PostMixer.Controls.Add(this.btnCompressor31);
            this.pnlCH4PostMixer.Controls.Add(this.btnCH4Delay);
            this.pnlCH4PostMixer.Controls.Add(this.btnGain33);
            this.pnlCH4PostMixer.Location = new System.Drawing.Point(454, 236);
            this.pnlCH4PostMixer.Name = "pnlCH4PostMixer";
            this.pnlCH4PostMixer.Size = new System.Drawing.Size(335, 39);
            this.pnlCH4PostMixer.TabIndex = 76;
            // 
            // pnlCH3PostMixer
            // 
            this.pnlCH3PostMixer.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlCH3PostMixer.BackgroundImage")));
            this.pnlCH3PostMixer.Controls.Add(this.btnGain22);
            this.pnlCH3PostMixer.Controls.Add(this.btnCH2PostFilters);
            this.pnlCH3PostMixer.Controls.Add(this.btnCompressor21);
            this.pnlCH3PostMixer.Controls.Add(this.btnCH3Delay);
            this.pnlCH3PostMixer.Controls.Add(this.btnGain23);
            this.pnlCH3PostMixer.Location = new System.Drawing.Point(454, 188);
            this.pnlCH3PostMixer.Name = "pnlCH3PostMixer";
            this.pnlCH3PostMixer.Size = new System.Drawing.Size(335, 39);
            this.pnlCH3PostMixer.TabIndex = 75;
            // 
            // pnlCH2PostMixer
            // 
            this.pnlCH2PostMixer.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlCH2PostMixer.BackgroundImage")));
            this.pnlCH2PostMixer.Controls.Add(this.btnGain12);
            this.pnlCH2PostMixer.Controls.Add(this.btnCH1PostFilters);
            this.pnlCH2PostMixer.Controls.Add(this.btnCompressor11);
            this.pnlCH2PostMixer.Controls.Add(this.btnCH2Delay);
            this.pnlCH2PostMixer.Controls.Add(this.btnGain13);
            this.pnlCH2PostMixer.Location = new System.Drawing.Point(454, 139);
            this.pnlCH2PostMixer.Name = "pnlCH2PostMixer";
            this.pnlCH2PostMixer.Size = new System.Drawing.Size(335, 39);
            this.pnlCH2PostMixer.TabIndex = 74;
            // 
            // lblCH1Output
            // 
            this.lblCH1Output.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lblCH1Output.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblCH1Output.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCH1Output.ForeColor = System.Drawing.Color.White;
            this.lblCH1Output.Location = new System.Drawing.Point(783, 100);
            this.lblCH1Output.Name = "lblCH1Output";
            this.lblCH1Output.Size = new System.Drawing.Size(111, 23);
            this.lblCH1Output.TabIndex = 70;
            this.lblCH1Output.Text = "Output #1";
            this.lblCH1Output.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlCH1PostMixer
            // 
            this.pnlCH1PostMixer.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlCH1PostMixer.BackgroundImage")));
            this.pnlCH1PostMixer.Controls.Add(this.btnGain02);
            this.pnlCH1PostMixer.Controls.Add(this.btnCH0PostFilters);
            this.pnlCH1PostMixer.Controls.Add(this.btnCompressor01);
            this.pnlCH1PostMixer.Controls.Add(this.btnCH1Delay);
            this.pnlCH1PostMixer.Controls.Add(this.btnGain03);
            this.pnlCH1PostMixer.Location = new System.Drawing.Point(454, 91);
            this.pnlCH1PostMixer.Name = "pnlCH1PostMixer";
            this.pnlCH1PostMixer.Size = new System.Drawing.Size(335, 39);
            this.pnlCH1PostMixer.TabIndex = 69;
            // 
            // pnlCH4PreMixer
            // 
            this.pnlCH4PreMixer.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlCH4PreMixer.BackgroundImage")));
            this.pnlCH4PreMixer.Controls.Add(this.btnGain30);
            this.pnlCH4PreMixer.Controls.Add(this.btnCH3PreFilters);
            this.pnlCH4PreMixer.Controls.Add(this.btnCompressor30);
            this.pnlCH4PreMixer.Controls.Add(this.btnGain31);
            this.pnlCH4PreMixer.Location = new System.Drawing.Point(119, 236);
            this.pnlCH4PreMixer.Name = "pnlCH4PreMixer";
            this.pnlCH4PreMixer.Size = new System.Drawing.Size(281, 39);
            this.pnlCH4PreMixer.TabIndex = 68;
            // 
            // pnlCH3PreMixer
            // 
            this.pnlCH3PreMixer.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlCH3PreMixer.BackgroundImage")));
            this.pnlCH3PreMixer.Controls.Add(this.btnGain20);
            this.pnlCH3PreMixer.Controls.Add(this.btnCH2PreFilters);
            this.pnlCH3PreMixer.Controls.Add(this.btnCompressor20);
            this.pnlCH3PreMixer.Controls.Add(this.btnGain21);
            this.pnlCH3PreMixer.Location = new System.Drawing.Point(119, 188);
            this.pnlCH3PreMixer.Name = "pnlCH3PreMixer";
            this.pnlCH3PreMixer.Size = new System.Drawing.Size(281, 39);
            this.pnlCH3PreMixer.TabIndex = 67;
            // 
            // pnlCH1PreMixer
            // 
            this.pnlCH1PreMixer.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlCH1PreMixer.BackgroundImage")));
            this.pnlCH1PreMixer.Controls.Add(this.btnGain00);
            this.pnlCH1PreMixer.Controls.Add(this.btnCH0PreFilters);
            this.pnlCH1PreMixer.Controls.Add(this.btnCompressor00);
            this.pnlCH1PreMixer.Controls.Add(this.btnGain01);
            this.pnlCH1PreMixer.Location = new System.Drawing.Point(119, 91);
            this.pnlCH1PreMixer.Name = "pnlCH1PreMixer";
            this.pnlCH1PreMixer.Size = new System.Drawing.Size(281, 39);
            this.pnlCH1PreMixer.TabIndex = 66;
            // 
            // lblCH4Input
            // 
            this.lblCH4Input.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lblCH4Input.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblCH4Input.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCH4Input.ForeColor = System.Drawing.Color.White;
            this.lblCH4Input.Location = new System.Drawing.Point(12, 244);
            this.lblCH4Input.Name = "lblCH4Input";
            this.lblCH4Input.Size = new System.Drawing.Size(107, 23);
            this.lblCH4Input.TabIndex = 65;
            this.lblCH4Input.Text = "Local Input #4";
            this.lblCH4Input.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCH3Input
            // 
            this.lblCH3Input.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lblCH3Input.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblCH3Input.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCH3Input.ForeColor = System.Drawing.Color.White;
            this.lblCH3Input.Location = new System.Drawing.Point(12, 196);
            this.lblCH3Input.Name = "lblCH3Input";
            this.lblCH3Input.Size = new System.Drawing.Size(107, 23);
            this.lblCH3Input.TabIndex = 63;
            this.lblCH3Input.Text = "Local Input #3";
            this.lblCH3Input.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCH2Input
            // 
            this.lblCH2Input.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lblCH2Input.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblCH2Input.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCH2Input.ForeColor = System.Drawing.Color.White;
            this.lblCH2Input.Location = new System.Drawing.Point(12, 148);
            this.lblCH2Input.Name = "lblCH2Input";
            this.lblCH2Input.Size = new System.Drawing.Size(107, 23);
            this.lblCH2Input.TabIndex = 62;
            this.lblCH2Input.Text = "Local Input #2";
            this.lblCH2Input.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCH1Input
            // 
            this.lblCH1Input.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.lblCH1Input.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblCH1Input.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCH1Input.ForeColor = System.Drawing.Color.White;
            this.lblCH1Input.Location = new System.Drawing.Point(12, 100);
            this.lblCH1Input.Name = "lblCH1Input";
            this.lblCH1Input.Size = new System.Drawing.Size(107, 23);
            this.lblCH1Input.TabIndex = 64;
            this.lblCH1Input.Text = "Local Input #1";
            this.lblCH1Input.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // picDuckerLine
            // 
            this.picDuckerLine.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picDuckerLine.BackgroundImage")));
            this.picDuckerLine.Image = ((System.Drawing.Image)(resources.GetObject("picDuckerLine.Image")));
            this.picDuckerLine.Location = new System.Drawing.Point(194, 109);
            this.picDuckerLine.Name = "picDuckerLine";
            this.picDuckerLine.Size = new System.Drawing.Size(8, 179);
            this.picDuckerLine.TabIndex = 88;
            this.picDuckerLine.TabStop = false;
            // 
            // pbtnDucker
            // 
            this.pbtnDucker.AutoResize = false;
            this.pbtnDucker.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pbtnDucker.BackgroundImage")));
            this.pbtnDucker.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbtnDucker.Location = new System.Drawing.Point(178, 288);
            this.pbtnDucker.Name = "pbtnDucker";
            this.pbtnDucker.OverImage = null;
            this.pbtnDucker.Overlay1Image = ((System.Drawing.Image)(resources.GetObject("pbtnDucker.Overlay1Image")));
            this.pbtnDucker.Overlay1Visible = false;
            this.pbtnDucker.Overlay2Image = null;
            this.pbtnDucker.Overlay2Visible = false;
            this.pbtnDucker.Overlay3Image = null;
            this.pbtnDucker.Overlay3Visible = false;
            this.pbtnDucker.PressedImage = null;
            this.pbtnDucker.Size = new System.Drawing.Size(39, 39);
            this.pbtnDucker.TabIndex = 89;
            this.pbtnDucker.ToolTipText = "";
            // 
            // btnSavetoFile
            // 
            this.btnSavetoFile.Location = new System.Drawing.Point(15, 347);
            this.btnSavetoFile.Name = "btnSavetoFile";
            this.btnSavetoFile.Size = new System.Drawing.Size(75, 23);
            this.btnSavetoFile.TabIndex = 91;
            this.btnSavetoFile.Text = "Save";
            this.btnSavetoFile.UseVisualStyleBackColor = true;
            this.btnSavetoFile.Click += new System.EventHandler(this.btnSavetoFile_Click);
            // 
            // btnStreamRead
            // 
            this.btnStreamRead.Location = new System.Drawing.Point(400, 332);
            this.btnStreamRead.Name = "btnStreamRead";
            this.btnStreamRead.Size = new System.Drawing.Size(101, 23);
            this.btnStreamRead.TabIndex = 92;
            this.btnStreamRead.Text = "Read Program";
            this.btnStreamRead.UseVisualStyleBackColor = true;
            this.btnStreamRead.Visible = false;
            this.btnStreamRead.Click += new System.EventHandler(this.btnStreamRead_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(82)))), ((int)(((byte)(82)))));
            this.ClientSize = new System.Drawing.Size(906, 404);
            this.Controls.Add(this.btnStreamRead);
            this.Controls.Add(this.btnSavetoFile);
            this.Controls.Add(this.pbtnDucker);
            this.Controls.Add(this.picDuckerLine);
            this.Controls.Add(this.pnlCH2PreMixer);
            this.Controls.Add(this.btnMatrixMixer);
            this.Controls.Add(this.lblCH4Output);
            this.Controls.Add(this.lblCH3Output);
            this.Controls.Add(this.lblCH2Output);
            this.Controls.Add(this.pnlCH4PostMixer);
            this.Controls.Add(this.pnlCH3PostMixer);
            this.Controls.Add(this.pnlCH2PostMixer);
            this.Controls.Add(this.lblCH1Output);
            this.Controls.Add(this.pnlCH1PostMixer);
            this.Controls.Add(this.pnlCH4PreMixer);
            this.Controls.Add(this.pnlCH3PreMixer);
            this.Controls.Add(this.pnlCH1PreMixer);
            this.Controls.Add(this.lblCH4Input);
            this.Controls.Add(this.lblCH3Input);
            this.Controls.Add(this.lblCH2Input);
            this.Controls.Add(this.lblCH1Input);
            this.Controls.Add(this.pictureBox37);
            this.Controls.Add(this.statusStrip1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "DSP 4x4";
            this.Controls.SetChildIndex(this.pictureBox2, 0);
            this.Controls.SetChildIndex(this.dropProgramSelection, 0);
            this.Controls.SetChildIndex(this.btnConnectToDevice, 0);
            this.Controls.SetChildIndex(this.pictureConnectionStatus, 0);
            this.Controls.SetChildIndex(this.statusStrip1, 0);
            this.Controls.SetChildIndex(this.pictureBox37, 0);
            this.Controls.SetChildIndex(this.lblCH1Input, 0);
            this.Controls.SetChildIndex(this.lblCH2Input, 0);
            this.Controls.SetChildIndex(this.lblCH3Input, 0);
            this.Controls.SetChildIndex(this.lblCH4Input, 0);
            this.Controls.SetChildIndex(this.pnlCH1PreMixer, 0);
            this.Controls.SetChildIndex(this.pnlCH3PreMixer, 0);
            this.Controls.SetChildIndex(this.pnlCH4PreMixer, 0);
            this.Controls.SetChildIndex(this.pnlCH1PostMixer, 0);
            this.Controls.SetChildIndex(this.lblCH1Output, 0);
            this.Controls.SetChildIndex(this.pnlCH2PostMixer, 0);
            this.Controls.SetChildIndex(this.pnlCH3PostMixer, 0);
            this.Controls.SetChildIndex(this.pnlCH4PostMixer, 0);
            this.Controls.SetChildIndex(this.lblCH2Output, 0);
            this.Controls.SetChildIndex(this.lblCH3Output, 0);
            this.Controls.SetChildIndex(this.lblCH4Output, 0);
            this.Controls.SetChildIndex(this.btnMatrixMixer, 0);
            this.Controls.SetChildIndex(this.pnlCH2PreMixer, 0);
            this.Controls.SetChildIndex(this.picDuckerLine, 0);
            this.Controls.SetChildIndex(this.pbtnDucker, 0);
            this.Controls.SetChildIndex(this.btnSavetoFile, 0);
            this.Controls.SetChildIndex(this.btnStreamRead, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pictureConnectionStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox37)).EndInit();
            this.pnlCH2PreMixer.ResumeLayout(false);
            this.pnlCH4PostMixer.ResumeLayout(false);
            this.pnlCH3PostMixer.ResumeLayout(false);
            this.pnlCH2PostMixer.ResumeLayout(false);
            this.pnlCH1PostMixer.ResumeLayout(false);
            this.pnlCH4PreMixer.ResumeLayout(false);
            this.pnlCH3PreMixer.ResumeLayout(false);
            this.pnlCH1PreMixer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picDuckerLine)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox37;
        private System.Windows.Forms.ToolStripMenuItem deviceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private PictureButton btnCH0PostFilters;
        private PictureButton btnGain03;
        private PictureButton btnCH1Delay;
        private PictureButton btnCompressor01;
        private PictureButton btnGain02;
        private PictureButton btnGain13;
        private PictureButton btnCH2Delay;
        private PictureButton btnCompressor11;
        private PictureButton btnGain12;
        private PictureButton btnCH1PostFilters;
        private PictureButton btnGain23;
        private PictureButton btnCH3Delay;
        private PictureButton btnCompressor21;
        private PictureButton btnGain22;
        private PictureButton btnCH2PostFilters;
        private PictureButton btnGain33;
        private PictureButton btnCH4Delay;
        private PictureButton btnCompressor31;
        private PictureButton btnGain32;
        private PictureButton btnCH3PostFilters;
        private PictureButton btnCompressor00;
        private PictureButton btnGain01;
        private PictureButton btnGain21;
        private PictureButton btnGain31;
        private PictureButton btnGain00;
        private PictureButton btnCH0PreFilters;
        private PictureButton btnGain20;
        private PictureButton btnCH2PreFilters;
        private PictureButton btnCompressor20;
        private PictureButton btnGain30;
        private PictureButton btnCH3PreFilters;
        private PictureButton btnCompressor30;
        private System.Windows.Forms.SaveFileDialog saveProgramDialog;
        private System.Windows.Forms.OpenFileDialog openProgramDialog;
        private System.Windows.Forms.ToolStripStatusLabel tsStatusLabel;
        private System.Windows.Forms.Panel pnlCH2PreMixer;
        private PictureButton btnGain10;
        private PictureButton btnCH1PreFilters;
        private PictureButton btnCompressor10;
        private PictureButton btnGain11;
        protected PictureButton btnMatrixMixer;
        private System.Windows.Forms.Label lblCH4Output;
        private System.Windows.Forms.Label lblCH3Output;
        private System.Windows.Forms.Label lblCH2Output;
        private System.Windows.Forms.Panel pnlCH4PostMixer;
        private System.Windows.Forms.Panel pnlCH3PostMixer;
        private System.Windows.Forms.Panel pnlCH2PostMixer;
        private System.Windows.Forms.Label lblCH1Output;
        private System.Windows.Forms.Panel pnlCH1PostMixer;
        private System.Windows.Forms.Panel pnlCH4PreMixer;
        private System.Windows.Forms.Panel pnlCH3PreMixer;
        private System.Windows.Forms.Panel pnlCH1PreMixer;
        private System.Windows.Forms.Label lblCH4Input;
        private System.Windows.Forms.Label lblCH3Input;
        private System.Windows.Forms.Label lblCH2Input;
        private System.Windows.Forms.Label lblCH1Input;
        private System.Windows.Forms.PictureBox picDuckerLine;
        private PictureButton pbtnDucker;
        private System.Windows.Forms.Button btnSavetoFile;
        private System.Windows.Forms.Button btnStreamRead;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}