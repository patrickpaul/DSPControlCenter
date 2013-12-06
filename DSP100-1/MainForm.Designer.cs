using SA_Resources;

namespace DSP_100_1
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deviceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCH4PostGain = new SA_Resources.PictureButton();
            this.btnCH4Delay = new SA_Resources.PictureButton();
            this.btnCH4Limiter = new SA_Resources.PictureButton();
            this.btnCH4PostTrim = new SA_Resources.PictureButton();
            this.btnCH4PostFilters = new SA_Resources.PictureButton();
            this.btnCH3PostGain = new SA_Resources.PictureButton();
            this.btnCH3Delay = new SA_Resources.PictureButton();
            this.btnCH3Limiter = new SA_Resources.PictureButton();
            this.btnCH3PostTrim = new SA_Resources.PictureButton();
            this.btnCH3PostFilters = new SA_Resources.PictureButton();
            this.btnCH2PostGain = new SA_Resources.PictureButton();
            this.btnCH2Delay = new SA_Resources.PictureButton();
            this.btnCH2Limiter = new SA_Resources.PictureButton();
            this.btnCH2PostTrim = new SA_Resources.PictureButton();
            this.btnCH2PostFilters = new SA_Resources.PictureButton();
            this.pictureBox37 = new System.Windows.Forms.PictureBox();
            this.btnCH1PostGain = new SA_Resources.PictureButton();
            this.btnCH1Delay = new SA_Resources.PictureButton();
            this.btnCH1Limiter = new SA_Resources.PictureButton();
            this.btnCH1PostTrim = new SA_Resources.PictureButton();
            this.btnCH1PostFilters = new SA_Resources.PictureButton();
            this.btnCH4PreGain = new SA_Resources.PictureButton();
            this.btnCH4PreFilters = new SA_Resources.PictureButton();
            this.btnCH4Compressor = new SA_Resources.PictureButton();
            this.btnCH4PreGain2 = new SA_Resources.PictureButton();
            this.btnCH3PreGain = new SA_Resources.PictureButton();
            this.btnCH3PreFilters = new SA_Resources.PictureButton();
            this.btnCH3Compressor = new SA_Resources.PictureButton();
            this.btnCH3PreGain2 = new SA_Resources.PictureButton();
            this.btnCH1PreGain = new SA_Resources.PictureButton();
            this.btnCH1PreFilters = new SA_Resources.PictureButton();
            this.btnCH1Compressor = new SA_Resources.PictureButton();
            this.btnCH1PreGain2 = new SA_Resources.PictureButton();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.saveProgramDialog = new System.Windows.Forms.SaveFileDialog();
            this.openProgramDialog = new System.Windows.Forms.OpenFileDialog();
            this.pnlCH2PreMixer = new System.Windows.Forms.Panel();
            this.btnCH2PreGain = new SA_Resources.PictureButton();
            this.btnCH2PreFilters = new SA_Resources.PictureButton();
            this.btnCH2Compressor = new SA_Resources.PictureButton();
            this.btnCH2PreGain2 = new SA_Resources.PictureButton();
            this.btnMatrixMixer = new SA_Resources.PictureButton();
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
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureConnectionStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox37)).BeginInit();
            this.pnlCH2PreMixer.SuspendLayout();
            this.pnlCH4PostMixer.SuspendLayout();
            this.pnlCH3PostMixer.SuspendLayout();
            this.pnlCH2PostMixer.SuspendLayout();
            this.pnlCH1PostMixer.SuspendLayout();
            this.pnlCH4PreMixer.SuspendLayout();
            this.pnlCH3PreMixer.SuspendLayout();
            this.pnlCH1PreMixer.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 5000;
            this.toolTip1.InitialDelay = 10;
            this.toolTip1.ReshowDelay = 50;
            this.toolTip1.ShowAlways = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 382);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(906, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
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
            // btnCH4PostGain
            // 
            this.btnCH4PostGain.AutoResize = false;
            this.btnCH4PostGain.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCH4PostGain.BackgroundImage")));
            this.btnCH4PostGain.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCH4PostGain.Location = new System.Drawing.Point(270, 0);
            this.btnCH4PostGain.Name = "btnCH4PostGain";
            this.btnCH4PostGain.OverImage = null;
            this.btnCH4PostGain.Overlay1Image = ((System.Drawing.Image)(resources.GetObject("btnCH4PostGain.Overlay1Image")));
            this.btnCH4PostGain.Overlay1Visible = false;
            this.btnCH4PostGain.Overlay2Image = ((System.Drawing.Image)(resources.GetObject("btnCH4PostGain.Overlay2Image")));
            this.btnCH4PostGain.Overlay2Visible = false;
            this.btnCH4PostGain.Overlay3Image = null;
            this.btnCH4PostGain.Overlay3Visible = false;
            this.btnCH4PostGain.PressedImage = ((System.Drawing.Image)(resources.GetObject("btnCH4PostGain.PressedImage")));
            this.btnCH4PostGain.Size = new System.Drawing.Size(39, 39);
            this.btnCH4PostGain.TabIndex = 31;
            this.btnCH4PostGain.ToolTipText = "";
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
            // btnCH4Limiter
            // 
            this.btnCH4Limiter.AutoResize = false;
            this.btnCH4Limiter.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCH4Limiter.BackgroundImage")));
            this.btnCH4Limiter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCH4Limiter.Location = new System.Drawing.Point(150, 0);
            this.btnCH4Limiter.Name = "btnCH4Limiter";
            this.btnCH4Limiter.OverImage = null;
            this.btnCH4Limiter.Overlay1Image = ((System.Drawing.Image)(resources.GetObject("btnCH4Limiter.Overlay1Image")));
            this.btnCH4Limiter.Overlay1Visible = true;
            this.btnCH4Limiter.Overlay2Image = ((System.Drawing.Image)(resources.GetObject("btnCH4Limiter.Overlay2Image")));
            this.btnCH4Limiter.Overlay2Visible = false;
            this.btnCH4Limiter.Overlay3Image = null;
            this.btnCH4Limiter.Overlay3Visible = false;
            this.btnCH4Limiter.PressedImage = ((System.Drawing.Image)(resources.GetObject("btnCH4Limiter.PressedImage")));
            this.btnCH4Limiter.Size = new System.Drawing.Size(39, 39);
            this.btnCH4Limiter.TabIndex = 29;
            this.btnCH4Limiter.ToolTipText = "";
            // 
            // btnCH4PostTrim
            // 
            this.btnCH4PostTrim.AutoResize = false;
            this.btnCH4PostTrim.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCH4PostTrim.BackgroundImage")));
            this.btnCH4PostTrim.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCH4PostTrim.Location = new System.Drawing.Point(30, 0);
            this.btnCH4PostTrim.Name = "btnCH4PostTrim";
            this.btnCH4PostTrim.OverImage = null;
            this.btnCH4PostTrim.Overlay1Image = ((System.Drawing.Image)(resources.GetObject("btnCH4PostTrim.Overlay1Image")));
            this.btnCH4PostTrim.Overlay1Visible = false;
            this.btnCH4PostTrim.Overlay2Image = ((System.Drawing.Image)(resources.GetObject("btnCH4PostTrim.Overlay2Image")));
            this.btnCH4PostTrim.Overlay2Visible = false;
            this.btnCH4PostTrim.Overlay3Image = null;
            this.btnCH4PostTrim.Overlay3Visible = false;
            this.btnCH4PostTrim.PressedImage = ((System.Drawing.Image)(resources.GetObject("btnCH4PostTrim.PressedImage")));
            this.btnCH4PostTrim.Size = new System.Drawing.Size(39, 39);
            this.btnCH4PostTrim.TabIndex = 28;
            this.btnCH4PostTrim.ToolTipText = "";
            // 
            // btnCH4PostFilters
            // 
            this.btnCH4PostFilters.AutoResize = false;
            this.btnCH4PostFilters.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCH4PostFilters.BackgroundImage")));
            this.btnCH4PostFilters.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCH4PostFilters.Location = new System.Drawing.Point(90, 0);
            this.btnCH4PostFilters.Name = "btnCH4PostFilters";
            this.btnCH4PostFilters.OverImage = null;
            this.btnCH4PostFilters.Overlay1Image = ((System.Drawing.Image)(resources.GetObject("btnCH4PostFilters.Overlay1Image")));
            this.btnCH4PostFilters.Overlay1Visible = false;
            this.btnCH4PostFilters.Overlay2Image = ((System.Drawing.Image)(resources.GetObject("btnCH4PostFilters.Overlay2Image")));
            this.btnCH4PostFilters.Overlay2Visible = false;
            this.btnCH4PostFilters.Overlay3Image = null;
            this.btnCH4PostFilters.Overlay3Visible = false;
            this.btnCH4PostFilters.PressedImage = ((System.Drawing.Image)(resources.GetObject("btnCH4PostFilters.PressedImage")));
            this.btnCH4PostFilters.Size = new System.Drawing.Size(39, 39);
            this.btnCH4PostFilters.TabIndex = 27;
            this.btnCH4PostFilters.ToolTipText = "";
            // 
            // btnCH3PostGain
            // 
            this.btnCH3PostGain.AutoResize = false;
            this.btnCH3PostGain.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCH3PostGain.BackgroundImage")));
            this.btnCH3PostGain.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCH3PostGain.Location = new System.Drawing.Point(270, 0);
            this.btnCH3PostGain.Name = "btnCH3PostGain";
            this.btnCH3PostGain.OverImage = null;
            this.btnCH3PostGain.Overlay1Image = ((System.Drawing.Image)(resources.GetObject("btnCH3PostGain.Overlay1Image")));
            this.btnCH3PostGain.Overlay1Visible = false;
            this.btnCH3PostGain.Overlay2Image = ((System.Drawing.Image)(resources.GetObject("btnCH3PostGain.Overlay2Image")));
            this.btnCH3PostGain.Overlay2Visible = false;
            this.btnCH3PostGain.Overlay3Image = null;
            this.btnCH3PostGain.Overlay3Visible = false;
            this.btnCH3PostGain.PressedImage = ((System.Drawing.Image)(resources.GetObject("btnCH3PostGain.PressedImage")));
            this.btnCH3PostGain.Size = new System.Drawing.Size(39, 39);
            this.btnCH3PostGain.TabIndex = 31;
            this.btnCH3PostGain.ToolTipText = "";
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
            // btnCH3Limiter
            // 
            this.btnCH3Limiter.AutoResize = false;
            this.btnCH3Limiter.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCH3Limiter.BackgroundImage")));
            this.btnCH3Limiter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCH3Limiter.Location = new System.Drawing.Point(150, 0);
            this.btnCH3Limiter.Name = "btnCH3Limiter";
            this.btnCH3Limiter.OverImage = null;
            this.btnCH3Limiter.Overlay1Image = ((System.Drawing.Image)(resources.GetObject("btnCH3Limiter.Overlay1Image")));
            this.btnCH3Limiter.Overlay1Visible = true;
            this.btnCH3Limiter.Overlay2Image = ((System.Drawing.Image)(resources.GetObject("btnCH3Limiter.Overlay2Image")));
            this.btnCH3Limiter.Overlay2Visible = false;
            this.btnCH3Limiter.Overlay3Image = null;
            this.btnCH3Limiter.Overlay3Visible = false;
            this.btnCH3Limiter.PressedImage = ((System.Drawing.Image)(resources.GetObject("btnCH3Limiter.PressedImage")));
            this.btnCH3Limiter.Size = new System.Drawing.Size(39, 39);
            this.btnCH3Limiter.TabIndex = 29;
            this.btnCH3Limiter.ToolTipText = "";
            // 
            // btnCH3PostTrim
            // 
            this.btnCH3PostTrim.AutoResize = false;
            this.btnCH3PostTrim.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCH3PostTrim.BackgroundImage")));
            this.btnCH3PostTrim.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCH3PostTrim.Location = new System.Drawing.Point(30, 0);
            this.btnCH3PostTrim.Name = "btnCH3PostTrim";
            this.btnCH3PostTrim.OverImage = null;
            this.btnCH3PostTrim.Overlay1Image = ((System.Drawing.Image)(resources.GetObject("btnCH3PostTrim.Overlay1Image")));
            this.btnCH3PostTrim.Overlay1Visible = false;
            this.btnCH3PostTrim.Overlay2Image = ((System.Drawing.Image)(resources.GetObject("btnCH3PostTrim.Overlay2Image")));
            this.btnCH3PostTrim.Overlay2Visible = false;
            this.btnCH3PostTrim.Overlay3Image = null;
            this.btnCH3PostTrim.Overlay3Visible = false;
            this.btnCH3PostTrim.PressedImage = ((System.Drawing.Image)(resources.GetObject("btnCH3PostTrim.PressedImage")));
            this.btnCH3PostTrim.Size = new System.Drawing.Size(39, 39);
            this.btnCH3PostTrim.TabIndex = 28;
            this.btnCH3PostTrim.ToolTipText = "";
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
            // btnCH2PostGain
            // 
            this.btnCH2PostGain.AutoResize = false;
            this.btnCH2PostGain.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCH2PostGain.BackgroundImage")));
            this.btnCH2PostGain.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCH2PostGain.Location = new System.Drawing.Point(270, 0);
            this.btnCH2PostGain.Name = "btnCH2PostGain";
            this.btnCH2PostGain.OverImage = null;
            this.btnCH2PostGain.Overlay1Image = ((System.Drawing.Image)(resources.GetObject("btnCH2PostGain.Overlay1Image")));
            this.btnCH2PostGain.Overlay1Visible = false;
            this.btnCH2PostGain.Overlay2Image = ((System.Drawing.Image)(resources.GetObject("btnCH2PostGain.Overlay2Image")));
            this.btnCH2PostGain.Overlay2Visible = false;
            this.btnCH2PostGain.Overlay3Image = null;
            this.btnCH2PostGain.Overlay3Visible = false;
            this.btnCH2PostGain.PressedImage = ((System.Drawing.Image)(resources.GetObject("btnCH2PostGain.PressedImage")));
            this.btnCH2PostGain.Size = new System.Drawing.Size(39, 39);
            this.btnCH2PostGain.TabIndex = 31;
            this.btnCH2PostGain.ToolTipText = "";
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
            // btnCH2Limiter
            // 
            this.btnCH2Limiter.AutoResize = false;
            this.btnCH2Limiter.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCH2Limiter.BackgroundImage")));
            this.btnCH2Limiter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCH2Limiter.Location = new System.Drawing.Point(150, 0);
            this.btnCH2Limiter.Name = "btnCH2Limiter";
            this.btnCH2Limiter.OverImage = null;
            this.btnCH2Limiter.Overlay1Image = ((System.Drawing.Image)(resources.GetObject("btnCH2Limiter.Overlay1Image")));
            this.btnCH2Limiter.Overlay1Visible = true;
            this.btnCH2Limiter.Overlay2Image = ((System.Drawing.Image)(resources.GetObject("btnCH2Limiter.Overlay2Image")));
            this.btnCH2Limiter.Overlay2Visible = false;
            this.btnCH2Limiter.Overlay3Image = null;
            this.btnCH2Limiter.Overlay3Visible = false;
            this.btnCH2Limiter.PressedImage = ((System.Drawing.Image)(resources.GetObject("btnCH2Limiter.PressedImage")));
            this.btnCH2Limiter.Size = new System.Drawing.Size(39, 39);
            this.btnCH2Limiter.TabIndex = 29;
            this.btnCH2Limiter.ToolTipText = "";
            // 
            // btnCH2PostTrim
            // 
            this.btnCH2PostTrim.AutoResize = false;
            this.btnCH2PostTrim.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCH2PostTrim.BackgroundImage")));
            this.btnCH2PostTrim.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCH2PostTrim.Location = new System.Drawing.Point(30, 0);
            this.btnCH2PostTrim.Name = "btnCH2PostTrim";
            this.btnCH2PostTrim.OverImage = null;
            this.btnCH2PostTrim.Overlay1Image = ((System.Drawing.Image)(resources.GetObject("btnCH2PostTrim.Overlay1Image")));
            this.btnCH2PostTrim.Overlay1Visible = false;
            this.btnCH2PostTrim.Overlay2Image = ((System.Drawing.Image)(resources.GetObject("btnCH2PostTrim.Overlay2Image")));
            this.btnCH2PostTrim.Overlay2Visible = false;
            this.btnCH2PostTrim.Overlay3Image = null;
            this.btnCH2PostTrim.Overlay3Visible = false;
            this.btnCH2PostTrim.PressedImage = ((System.Drawing.Image)(resources.GetObject("btnCH2PostTrim.PressedImage")));
            this.btnCH2PostTrim.Size = new System.Drawing.Size(39, 39);
            this.btnCH2PostTrim.TabIndex = 28;
            this.btnCH2PostTrim.ToolTipText = "";
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
            // btnCH1PostGain
            // 
            this.btnCH1PostGain.AutoResize = false;
            this.btnCH1PostGain.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCH1PostGain.BackgroundImage")));
            this.btnCH1PostGain.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCH1PostGain.Location = new System.Drawing.Point(270, 0);
            this.btnCH1PostGain.Name = "btnCH1PostGain";
            this.btnCH1PostGain.OverImage = null;
            this.btnCH1PostGain.Overlay1Image = ((System.Drawing.Image)(resources.GetObject("btnCH1PostGain.Overlay1Image")));
            this.btnCH1PostGain.Overlay1Visible = false;
            this.btnCH1PostGain.Overlay2Image = ((System.Drawing.Image)(resources.GetObject("btnCH1PostGain.Overlay2Image")));
            this.btnCH1PostGain.Overlay2Visible = false;
            this.btnCH1PostGain.Overlay3Image = null;
            this.btnCH1PostGain.Overlay3Visible = false;
            this.btnCH1PostGain.PressedImage = ((System.Drawing.Image)(resources.GetObject("btnCH1PostGain.PressedImage")));
            this.btnCH1PostGain.Size = new System.Drawing.Size(39, 39);
            this.btnCH1PostGain.TabIndex = 31;
            this.btnCH1PostGain.ToolTipText = "";
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
            // btnCH1Limiter
            // 
            this.btnCH1Limiter.AutoResize = false;
            this.btnCH1Limiter.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCH1Limiter.BackgroundImage")));
            this.btnCH1Limiter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCH1Limiter.Location = new System.Drawing.Point(150, 0);
            this.btnCH1Limiter.Name = "btnCH1Limiter";
            this.btnCH1Limiter.OverImage = null;
            this.btnCH1Limiter.Overlay1Image = ((System.Drawing.Image)(resources.GetObject("btnCH1Limiter.Overlay1Image")));
            this.btnCH1Limiter.Overlay1Visible = true;
            this.btnCH1Limiter.Overlay2Image = ((System.Drawing.Image)(resources.GetObject("btnCH1Limiter.Overlay2Image")));
            this.btnCH1Limiter.Overlay2Visible = false;
            this.btnCH1Limiter.Overlay3Image = null;
            this.btnCH1Limiter.Overlay3Visible = false;
            this.btnCH1Limiter.PressedImage = ((System.Drawing.Image)(resources.GetObject("btnCH1Limiter.PressedImage")));
            this.btnCH1Limiter.Size = new System.Drawing.Size(39, 39);
            this.btnCH1Limiter.TabIndex = 29;
            this.btnCH1Limiter.ToolTipText = "";
            // 
            // btnCH1PostTrim
            // 
            this.btnCH1PostTrim.AutoResize = false;
            this.btnCH1PostTrim.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCH1PostTrim.BackgroundImage")));
            this.btnCH1PostTrim.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCH1PostTrim.Location = new System.Drawing.Point(30, 0);
            this.btnCH1PostTrim.Name = "btnCH1PostTrim";
            this.btnCH1PostTrim.OverImage = null;
            this.btnCH1PostTrim.Overlay1Image = ((System.Drawing.Image)(resources.GetObject("btnCH1PostTrim.Overlay1Image")));
            this.btnCH1PostTrim.Overlay1Visible = false;
            this.btnCH1PostTrim.Overlay2Image = ((System.Drawing.Image)(resources.GetObject("btnCH1PostTrim.Overlay2Image")));
            this.btnCH1PostTrim.Overlay2Visible = false;
            this.btnCH1PostTrim.Overlay3Image = null;
            this.btnCH1PostTrim.Overlay3Visible = false;
            this.btnCH1PostTrim.PressedImage = ((System.Drawing.Image)(resources.GetObject("btnCH1PostTrim.PressedImage")));
            this.btnCH1PostTrim.Size = new System.Drawing.Size(39, 39);
            this.btnCH1PostTrim.TabIndex = 28;
            this.btnCH1PostTrim.ToolTipText = "";
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
            // btnCH4PreGain
            // 
            this.btnCH4PreGain.AutoResize = false;
            this.btnCH4PreGain.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCH4PreGain.BackgroundImage")));
            this.btnCH4PreGain.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCH4PreGain.Location = new System.Drawing.Point(30, 0);
            this.btnCH4PreGain.Name = "btnCH4PreGain";
            this.btnCH4PreGain.OverImage = null;
            this.btnCH4PreGain.Overlay1Image = ((System.Drawing.Image)(resources.GetObject("btnCH4PreGain.Overlay1Image")));
            this.btnCH4PreGain.Overlay1Visible = false;
            this.btnCH4PreGain.Overlay2Image = ((System.Drawing.Image)(resources.GetObject("btnCH4PreGain.Overlay2Image")));
            this.btnCH4PreGain.Overlay2Visible = false;
            this.btnCH4PreGain.Overlay3Image = null;
            this.btnCH4PreGain.Overlay3Visible = false;
            this.btnCH4PreGain.PressedImage = ((System.Drawing.Image)(resources.GetObject("btnCH4PreGain.PressedImage")));
            this.btnCH4PreGain.Size = new System.Drawing.Size(39, 39);
            this.btnCH4PreGain.TabIndex = 36;
            this.btnCH4PreGain.ToolTipText = "";
            // 
            // btnCH4PreFilters
            // 
            this.btnCH4PreFilters.AutoResize = false;
            this.btnCH4PreFilters.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCH4PreFilters.BackgroundImage")));
            this.btnCH4PreFilters.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCH4PreFilters.Location = new System.Drawing.Point(90, 0);
            this.btnCH4PreFilters.Name = "btnCH4PreFilters";
            this.btnCH4PreFilters.OverImage = null;
            this.btnCH4PreFilters.Overlay1Image = ((System.Drawing.Image)(resources.GetObject("btnCH4PreFilters.Overlay1Image")));
            this.btnCH4PreFilters.Overlay1Visible = false;
            this.btnCH4PreFilters.Overlay2Image = ((System.Drawing.Image)(resources.GetObject("btnCH4PreFilters.Overlay2Image")));
            this.btnCH4PreFilters.Overlay2Visible = false;
            this.btnCH4PreFilters.Overlay3Image = null;
            this.btnCH4PreFilters.Overlay3Visible = false;
            this.btnCH4PreFilters.PressedImage = ((System.Drawing.Image)(resources.GetObject("btnCH4PreFilters.PressedImage")));
            this.btnCH4PreFilters.Size = new System.Drawing.Size(39, 39);
            this.btnCH4PreFilters.TabIndex = 35;
            this.btnCH4PreFilters.ToolTipText = "";
            // 
            // btnCH4Compressor
            // 
            this.btnCH4Compressor.AutoResize = false;
            this.btnCH4Compressor.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCH4Compressor.BackgroundImage")));
            this.btnCH4Compressor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCH4Compressor.Location = new System.Drawing.Point(152, 0);
            this.btnCH4Compressor.Name = "btnCH4Compressor";
            this.btnCH4Compressor.OverImage = null;
            this.btnCH4Compressor.Overlay1Image = ((System.Drawing.Image)(resources.GetObject("btnCH4Compressor.Overlay1Image")));
            this.btnCH4Compressor.Overlay1Visible = true;
            this.btnCH4Compressor.Overlay2Image = ((System.Drawing.Image)(resources.GetObject("btnCH4Compressor.Overlay2Image")));
            this.btnCH4Compressor.Overlay2Visible = false;
            this.btnCH4Compressor.Overlay3Image = null;
            this.btnCH4Compressor.Overlay3Visible = false;
            this.btnCH4Compressor.PressedImage = ((System.Drawing.Image)(resources.GetObject("btnCH4Compressor.PressedImage")));
            this.btnCH4Compressor.Size = new System.Drawing.Size(39, 39);
            this.btnCH4Compressor.TabIndex = 34;
            this.btnCH4Compressor.ToolTipText = "";
            // 
            // btnCH4PreGain2
            // 
            this.btnCH4PreGain2.AutoResize = false;
            this.btnCH4PreGain2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCH4PreGain2.BackgroundImage")));
            this.btnCH4PreGain2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCH4PreGain2.Location = new System.Drawing.Point(212, 0);
            this.btnCH4PreGain2.Name = "btnCH4PreGain2";
            this.btnCH4PreGain2.OverImage = null;
            this.btnCH4PreGain2.Overlay1Image = ((System.Drawing.Image)(resources.GetObject("btnCH4PreGain2.Overlay1Image")));
            this.btnCH4PreGain2.Overlay1Visible = false;
            this.btnCH4PreGain2.Overlay2Image = ((System.Drawing.Image)(resources.GetObject("btnCH4PreGain2.Overlay2Image")));
            this.btnCH4PreGain2.Overlay2Visible = false;
            this.btnCH4PreGain2.Overlay3Image = null;
            this.btnCH4PreGain2.Overlay3Visible = false;
            this.btnCH4PreGain2.PressedImage = ((System.Drawing.Image)(resources.GetObject("btnCH4PreGain2.PressedImage")));
            this.btnCH4PreGain2.Size = new System.Drawing.Size(39, 39);
            this.btnCH4PreGain2.TabIndex = 33;
            this.btnCH4PreGain2.ToolTipText = "";
            // 
            // btnCH3PreGain
            // 
            this.btnCH3PreGain.AutoResize = false;
            this.btnCH3PreGain.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCH3PreGain.BackgroundImage")));
            this.btnCH3PreGain.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCH3PreGain.Location = new System.Drawing.Point(30, 0);
            this.btnCH3PreGain.Name = "btnCH3PreGain";
            this.btnCH3PreGain.OverImage = null;
            this.btnCH3PreGain.Overlay1Image = ((System.Drawing.Image)(resources.GetObject("btnCH3PreGain.Overlay1Image")));
            this.btnCH3PreGain.Overlay1Visible = false;
            this.btnCH3PreGain.Overlay2Image = ((System.Drawing.Image)(resources.GetObject("btnCH3PreGain.Overlay2Image")));
            this.btnCH3PreGain.Overlay2Visible = false;
            this.btnCH3PreGain.Overlay3Image = null;
            this.btnCH3PreGain.Overlay3Visible = false;
            this.btnCH3PreGain.PressedImage = ((System.Drawing.Image)(resources.GetObject("btnCH3PreGain.PressedImage")));
            this.btnCH3PreGain.Size = new System.Drawing.Size(39, 39);
            this.btnCH3PreGain.TabIndex = 36;
            this.btnCH3PreGain.ToolTipText = "";
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
            // btnCH3Compressor
            // 
            this.btnCH3Compressor.AutoResize = false;
            this.btnCH3Compressor.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCH3Compressor.BackgroundImage")));
            this.btnCH3Compressor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCH3Compressor.Location = new System.Drawing.Point(152, 0);
            this.btnCH3Compressor.Name = "btnCH3Compressor";
            this.btnCH3Compressor.OverImage = null;
            this.btnCH3Compressor.Overlay1Image = ((System.Drawing.Image)(resources.GetObject("btnCH3Compressor.Overlay1Image")));
            this.btnCH3Compressor.Overlay1Visible = true;
            this.btnCH3Compressor.Overlay2Image = ((System.Drawing.Image)(resources.GetObject("btnCH3Compressor.Overlay2Image")));
            this.btnCH3Compressor.Overlay2Visible = false;
            this.btnCH3Compressor.Overlay3Image = null;
            this.btnCH3Compressor.Overlay3Visible = false;
            this.btnCH3Compressor.PressedImage = ((System.Drawing.Image)(resources.GetObject("btnCH3Compressor.PressedImage")));
            this.btnCH3Compressor.Size = new System.Drawing.Size(39, 39);
            this.btnCH3Compressor.TabIndex = 34;
            this.btnCH3Compressor.ToolTipText = "";
            // 
            // btnCH3PreGain2
            // 
            this.btnCH3PreGain2.AutoResize = false;
            this.btnCH3PreGain2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCH3PreGain2.BackgroundImage")));
            this.btnCH3PreGain2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCH3PreGain2.Location = new System.Drawing.Point(212, 0);
            this.btnCH3PreGain2.Name = "btnCH3PreGain2";
            this.btnCH3PreGain2.OverImage = null;
            this.btnCH3PreGain2.Overlay1Image = ((System.Drawing.Image)(resources.GetObject("btnCH3PreGain2.Overlay1Image")));
            this.btnCH3PreGain2.Overlay1Visible = false;
            this.btnCH3PreGain2.Overlay2Image = ((System.Drawing.Image)(resources.GetObject("btnCH3PreGain2.Overlay2Image")));
            this.btnCH3PreGain2.Overlay2Visible = false;
            this.btnCH3PreGain2.Overlay3Image = null;
            this.btnCH3PreGain2.Overlay3Visible = false;
            this.btnCH3PreGain2.PressedImage = ((System.Drawing.Image)(resources.GetObject("btnCH3PreGain2.PressedImage")));
            this.btnCH3PreGain2.Size = new System.Drawing.Size(39, 39);
            this.btnCH3PreGain2.TabIndex = 33;
            this.btnCH3PreGain2.ToolTipText = "";
            // 
            // btnCH1PreGain
            // 
            this.btnCH1PreGain.AutoResize = false;
            this.btnCH1PreGain.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCH1PreGain.BackgroundImage")));
            this.btnCH1PreGain.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCH1PreGain.Location = new System.Drawing.Point(30, 0);
            this.btnCH1PreGain.Name = "btnCH1PreGain";
            this.btnCH1PreGain.OverImage = null;
            this.btnCH1PreGain.Overlay1Image = ((System.Drawing.Image)(resources.GetObject("btnCH1PreGain.Overlay1Image")));
            this.btnCH1PreGain.Overlay1Visible = false;
            this.btnCH1PreGain.Overlay2Image = ((System.Drawing.Image)(resources.GetObject("btnCH1PreGain.Overlay2Image")));
            this.btnCH1PreGain.Overlay2Visible = false;
            this.btnCH1PreGain.Overlay3Image = null;
            this.btnCH1PreGain.Overlay3Visible = false;
            this.btnCH1PreGain.PressedImage = ((System.Drawing.Image)(resources.GetObject("btnCH1PreGain.PressedImage")));
            this.btnCH1PreGain.Size = new System.Drawing.Size(39, 39);
            this.btnCH1PreGain.TabIndex = 35;
            this.btnCH1PreGain.ToolTipText = "";
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
            this.btnCH1PreFilters.TabIndex = 34;
            this.btnCH1PreFilters.ToolTipText = "Filters";
            // 
            // btnCH1Compressor
            // 
            this.btnCH1Compressor.AutoResize = false;
            this.btnCH1Compressor.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCH1Compressor.BackgroundImage")));
            this.btnCH1Compressor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCH1Compressor.Location = new System.Drawing.Point(152, 0);
            this.btnCH1Compressor.Name = "btnCH1Compressor";
            this.btnCH1Compressor.OverImage = null;
            this.btnCH1Compressor.Overlay1Image = ((System.Drawing.Image)(resources.GetObject("btnCH1Compressor.Overlay1Image")));
            this.btnCH1Compressor.Overlay1Visible = true;
            this.btnCH1Compressor.Overlay2Image = ((System.Drawing.Image)(resources.GetObject("btnCH1Compressor.Overlay2Image")));
            this.btnCH1Compressor.Overlay2Visible = false;
            this.btnCH1Compressor.Overlay3Image = null;
            this.btnCH1Compressor.Overlay3Visible = false;
            this.btnCH1Compressor.PressedImage = ((System.Drawing.Image)(resources.GetObject("btnCH1Compressor.PressedImage")));
            this.btnCH1Compressor.Size = new System.Drawing.Size(39, 39);
            this.btnCH1Compressor.TabIndex = 33;
            this.btnCH1Compressor.ToolTipText = "Compressor";
            // 
            // btnCH1PreGain2
            // 
            this.btnCH1PreGain2.AutoResize = false;
            this.btnCH1PreGain2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCH1PreGain2.BackgroundImage")));
            this.btnCH1PreGain2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCH1PreGain2.Location = new System.Drawing.Point(212, 0);
            this.btnCH1PreGain2.Name = "btnCH1PreGain2";
            this.btnCH1PreGain2.OverImage = null;
            this.btnCH1PreGain2.Overlay1Image = ((System.Drawing.Image)(resources.GetObject("btnCH1PreGain2.Overlay1Image")));
            this.btnCH1PreGain2.Overlay1Visible = false;
            this.btnCH1PreGain2.Overlay2Image = ((System.Drawing.Image)(resources.GetObject("btnCH1PreGain2.Overlay2Image")));
            this.btnCH1PreGain2.Overlay2Visible = false;
            this.btnCH1PreGain2.Overlay3Image = null;
            this.btnCH1PreGain2.Overlay3Visible = false;
            this.btnCH1PreGain2.PressedImage = ((System.Drawing.Image)(resources.GetObject("btnCH1PreGain2.PressedImage")));
            this.btnCH1PreGain2.Size = new System.Drawing.Size(39, 39);
            this.btnCH1PreGain2.TabIndex = 32;
            this.btnCH1PreGain2.ToolTipText = "";
            // 
            // timer1
            // 
            this.timer1.Interval = 75;
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
            this.pnlCH2PreMixer.Controls.Add(this.btnCH2PreGain);
            this.pnlCH2PreMixer.Controls.Add(this.btnCH2PreFilters);
            this.pnlCH2PreMixer.Controls.Add(this.btnCH2Compressor);
            this.pnlCH2PreMixer.Controls.Add(this.btnCH2PreGain2);
            this.pnlCH2PreMixer.Location = new System.Drawing.Point(119, 140);
            this.pnlCH2PreMixer.Name = "pnlCH2PreMixer";
            this.pnlCH2PreMixer.Size = new System.Drawing.Size(281, 39);
            this.pnlCH2PreMixer.TabIndex = 78;
            // 
            // btnCH2PreGain
            // 
            this.btnCH2PreGain.AutoResize = false;
            this.btnCH2PreGain.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCH2PreGain.BackgroundImage")));
            this.btnCH2PreGain.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCH2PreGain.Location = new System.Drawing.Point(30, 0);
            this.btnCH2PreGain.Name = "btnCH2PreGain";
            this.btnCH2PreGain.OverImage = null;
            this.btnCH2PreGain.Overlay1Image = ((System.Drawing.Image)(resources.GetObject("btnCH2PreGain.Overlay1Image")));
            this.btnCH2PreGain.Overlay1Visible = false;
            this.btnCH2PreGain.Overlay2Image = ((System.Drawing.Image)(resources.GetObject("btnCH2PreGain.Overlay2Image")));
            this.btnCH2PreGain.Overlay2Visible = false;
            this.btnCH2PreGain.Overlay3Image = null;
            this.btnCH2PreGain.Overlay3Visible = false;
            this.btnCH2PreGain.PressedImage = ((System.Drawing.Image)(resources.GetObject("btnCH2PreGain.PressedImage")));
            this.btnCH2PreGain.Size = new System.Drawing.Size(39, 39);
            this.btnCH2PreGain.TabIndex = 36;
            this.btnCH2PreGain.ToolTipText = "";
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
            // btnCH2Compressor
            // 
            this.btnCH2Compressor.AutoResize = false;
            this.btnCH2Compressor.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCH2Compressor.BackgroundImage")));
            this.btnCH2Compressor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCH2Compressor.Location = new System.Drawing.Point(152, 0);
            this.btnCH2Compressor.Name = "btnCH2Compressor";
            this.btnCH2Compressor.OverImage = null;
            this.btnCH2Compressor.Overlay1Image = ((System.Drawing.Image)(resources.GetObject("btnCH2Compressor.Overlay1Image")));
            this.btnCH2Compressor.Overlay1Visible = true;
            this.btnCH2Compressor.Overlay2Image = ((System.Drawing.Image)(resources.GetObject("btnCH2Compressor.Overlay2Image")));
            this.btnCH2Compressor.Overlay2Visible = false;
            this.btnCH2Compressor.Overlay3Image = null;
            this.btnCH2Compressor.Overlay3Visible = false;
            this.btnCH2Compressor.PressedImage = ((System.Drawing.Image)(resources.GetObject("btnCH2Compressor.PressedImage")));
            this.btnCH2Compressor.Size = new System.Drawing.Size(39, 39);
            this.btnCH2Compressor.TabIndex = 34;
            this.btnCH2Compressor.ToolTipText = "";
            // 
            // btnCH2PreGain2
            // 
            this.btnCH2PreGain2.AutoResize = false;
            this.btnCH2PreGain2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCH2PreGain2.BackgroundImage")));
            this.btnCH2PreGain2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCH2PreGain2.Location = new System.Drawing.Point(212, 0);
            this.btnCH2PreGain2.Name = "btnCH2PreGain2";
            this.btnCH2PreGain2.OverImage = null;
            this.btnCH2PreGain2.Overlay1Image = ((System.Drawing.Image)(resources.GetObject("btnCH2PreGain2.Overlay1Image")));
            this.btnCH2PreGain2.Overlay1Visible = false;
            this.btnCH2PreGain2.Overlay2Image = ((System.Drawing.Image)(resources.GetObject("btnCH2PreGain2.Overlay2Image")));
            this.btnCH2PreGain2.Overlay2Visible = false;
            this.btnCH2PreGain2.Overlay3Image = null;
            this.btnCH2PreGain2.Overlay3Visible = false;
            this.btnCH2PreGain2.PressedImage = ((System.Drawing.Image)(resources.GetObject("btnCH2PreGain2.PressedImage")));
            this.btnCH2PreGain2.Size = new System.Drawing.Size(39, 39);
            this.btnCH2PreGain2.TabIndex = 33;
            this.btnCH2PreGain2.ToolTipText = "";
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
            this.lblCH4Output.BackColor = System.Drawing.Color.Teal;
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
            this.lblCH3Output.BackColor = System.Drawing.Color.Teal;
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
            this.lblCH2Output.BackColor = System.Drawing.Color.Teal;
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
            this.pnlCH4PostMixer.Controls.Add(this.btnCH4PostTrim);
            this.pnlCH4PostMixer.Controls.Add(this.btnCH4PostFilters);
            this.pnlCH4PostMixer.Controls.Add(this.btnCH4Limiter);
            this.pnlCH4PostMixer.Controls.Add(this.btnCH4Delay);
            this.pnlCH4PostMixer.Controls.Add(this.btnCH4PostGain);
            this.pnlCH4PostMixer.Location = new System.Drawing.Point(454, 236);
            this.pnlCH4PostMixer.Name = "pnlCH4PostMixer";
            this.pnlCH4PostMixer.Size = new System.Drawing.Size(335, 39);
            this.pnlCH4PostMixer.TabIndex = 76;
            // 
            // pnlCH3PostMixer
            // 
            this.pnlCH3PostMixer.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlCH3PostMixer.BackgroundImage")));
            this.pnlCH3PostMixer.Controls.Add(this.btnCH3PostTrim);
            this.pnlCH3PostMixer.Controls.Add(this.btnCH3PostFilters);
            this.pnlCH3PostMixer.Controls.Add(this.btnCH3Limiter);
            this.pnlCH3PostMixer.Controls.Add(this.btnCH3Delay);
            this.pnlCH3PostMixer.Controls.Add(this.btnCH3PostGain);
            this.pnlCH3PostMixer.Location = new System.Drawing.Point(454, 188);
            this.pnlCH3PostMixer.Name = "pnlCH3PostMixer";
            this.pnlCH3PostMixer.Size = new System.Drawing.Size(335, 39);
            this.pnlCH3PostMixer.TabIndex = 75;
            // 
            // pnlCH2PostMixer
            // 
            this.pnlCH2PostMixer.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlCH2PostMixer.BackgroundImage")));
            this.pnlCH2PostMixer.Controls.Add(this.btnCH2PostTrim);
            this.pnlCH2PostMixer.Controls.Add(this.btnCH2PostFilters);
            this.pnlCH2PostMixer.Controls.Add(this.btnCH2Limiter);
            this.pnlCH2PostMixer.Controls.Add(this.btnCH2Delay);
            this.pnlCH2PostMixer.Controls.Add(this.btnCH2PostGain);
            this.pnlCH2PostMixer.Location = new System.Drawing.Point(454, 139);
            this.pnlCH2PostMixer.Name = "pnlCH2PostMixer";
            this.pnlCH2PostMixer.Size = new System.Drawing.Size(335, 39);
            this.pnlCH2PostMixer.TabIndex = 74;
            // 
            // lblCH1Output
            // 
            this.lblCH1Output.BackColor = System.Drawing.Color.Teal;
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
            this.pnlCH1PostMixer.Controls.Add(this.btnCH1PostTrim);
            this.pnlCH1PostMixer.Controls.Add(this.btnCH1PostFilters);
            this.pnlCH1PostMixer.Controls.Add(this.btnCH1Limiter);
            this.pnlCH1PostMixer.Controls.Add(this.btnCH1Delay);
            this.pnlCH1PostMixer.Controls.Add(this.btnCH1PostGain);
            this.pnlCH1PostMixer.Location = new System.Drawing.Point(454, 91);
            this.pnlCH1PostMixer.Name = "pnlCH1PostMixer";
            this.pnlCH1PostMixer.Size = new System.Drawing.Size(335, 39);
            this.pnlCH1PostMixer.TabIndex = 69;
            // 
            // pnlCH4PreMixer
            // 
            this.pnlCH4PreMixer.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlCH4PreMixer.BackgroundImage")));
            this.pnlCH4PreMixer.Controls.Add(this.btnCH4PreGain);
            this.pnlCH4PreMixer.Controls.Add(this.btnCH4PreFilters);
            this.pnlCH4PreMixer.Controls.Add(this.btnCH4Compressor);
            this.pnlCH4PreMixer.Controls.Add(this.btnCH4PreGain2);
            this.pnlCH4PreMixer.Location = new System.Drawing.Point(119, 236);
            this.pnlCH4PreMixer.Name = "pnlCH4PreMixer";
            this.pnlCH4PreMixer.Size = new System.Drawing.Size(281, 39);
            this.pnlCH4PreMixer.TabIndex = 68;
            // 
            // pnlCH3PreMixer
            // 
            this.pnlCH3PreMixer.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlCH3PreMixer.BackgroundImage")));
            this.pnlCH3PreMixer.Controls.Add(this.btnCH3PreGain);
            this.pnlCH3PreMixer.Controls.Add(this.btnCH3PreFilters);
            this.pnlCH3PreMixer.Controls.Add(this.btnCH3Compressor);
            this.pnlCH3PreMixer.Controls.Add(this.btnCH3PreGain2);
            this.pnlCH3PreMixer.Location = new System.Drawing.Point(119, 188);
            this.pnlCH3PreMixer.Name = "pnlCH3PreMixer";
            this.pnlCH3PreMixer.Size = new System.Drawing.Size(281, 39);
            this.pnlCH3PreMixer.TabIndex = 67;
            // 
            // pnlCH1PreMixer
            // 
            this.pnlCH1PreMixer.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlCH1PreMixer.BackgroundImage")));
            this.pnlCH1PreMixer.Controls.Add(this.btnCH1PreGain);
            this.pnlCH1PreMixer.Controls.Add(this.btnCH1PreFilters);
            this.pnlCH1PreMixer.Controls.Add(this.btnCH1Compressor);
            this.pnlCH1PreMixer.Controls.Add(this.btnCH1PreGain2);
            this.pnlCH1PreMixer.Location = new System.Drawing.Point(119, 91);
            this.pnlCH1PreMixer.Name = "pnlCH1PreMixer";
            this.pnlCH1PreMixer.Size = new System.Drawing.Size(281, 39);
            this.pnlCH1PreMixer.TabIndex = 66;
            // 
            // lblCH4Input
            // 
            this.lblCH4Input.BackColor = System.Drawing.Color.Teal;
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
            this.lblCH3Input.BackColor = System.Drawing.Color.Teal;
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
            this.lblCH2Input.BackColor = System.Drawing.Color.Teal;
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
            this.lblCH1Input.BackColor = System.Drawing.Color.Teal;
            this.lblCH1Input.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblCH1Input.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCH1Input.ForeColor = System.Drawing.Color.White;
            this.lblCH1Input.Location = new System.Drawing.Point(12, 100);
            this.lblCH1Input.Name = "lblCH1Input";
            this.lblCH1Input.Size = new System.Drawing.Size(107, 23);
            this.lblCH1Input.TabIndex = 64;
            this.lblCH1Input.Text = "Input #1";
            this.lblCH1Input.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer2
            // 
            this.timer2.Interval = 500;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(82)))), ((int)(((byte)(82)))));
            this.ClientSize = new System.Drawing.Size(906, 404);
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
            this.Text = "DSP 100-1";
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
            ((System.ComponentModel.ISupportInitialize)(this.pictureConnectionStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox37)).EndInit();
            this.pnlCH2PreMixer.ResumeLayout(false);
            this.pnlCH4PostMixer.ResumeLayout(false);
            this.pnlCH3PostMixer.ResumeLayout(false);
            this.pnlCH2PostMixer.ResumeLayout(false);
            this.pnlCH1PostMixer.ResumeLayout(false);
            this.pnlCH4PreMixer.ResumeLayout(false);
            this.pnlCH3PreMixer.ResumeLayout(false);
            this.pnlCH1PreMixer.ResumeLayout(false);
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
        private PictureButton btnCH1PostFilters;
        private PictureButton btnCH1PostGain;
        private PictureButton btnCH1Delay;
        private PictureButton btnCH1Limiter;
        private PictureButton btnCH1PostTrim;
        private PictureButton btnCH2PostGain;
        private PictureButton btnCH2Delay;
        private PictureButton btnCH2Limiter;
        private PictureButton btnCH2PostTrim;
        private PictureButton btnCH2PostFilters;
        private PictureButton btnCH3PostGain;
        private PictureButton btnCH3Delay;
        private PictureButton btnCH3Limiter;
        private PictureButton btnCH3PostTrim;
        private PictureButton btnCH3PostFilters;
        private PictureButton btnCH4PostGain;
        private PictureButton btnCH4Delay;
        private PictureButton btnCH4Limiter;
        private PictureButton btnCH4PostTrim;
        private PictureButton btnCH4PostFilters;
        private PictureButton btnCH1Compressor;
        private PictureButton btnCH1PreGain2;
        private PictureButton btnCH3PreGain2;
        private PictureButton btnCH4PreGain2;
        private PictureButton btnCH1PreGain;
        private PictureButton btnCH1PreFilters;
        private PictureButton btnCH3PreGain;
        private PictureButton btnCH3PreFilters;
        private PictureButton btnCH3Compressor;
        private PictureButton btnCH4PreGain;
        private PictureButton btnCH4PreFilters;
        private PictureButton btnCH4Compressor;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.SaveFileDialog saveProgramDialog;
        private System.Windows.Forms.OpenFileDialog openProgramDialog;
        private System.Windows.Forms.ToolStripStatusLabel tsStatusLabel;
        private System.Windows.Forms.Panel pnlCH2PreMixer;
        private PictureButton btnCH2PreGain;
        private PictureButton btnCH2PreFilters;
        private PictureButton btnCH2Compressor;
        private PictureButton btnCH2PreGain2;
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
        private System.Windows.Forms.Timer timer2;
    }
}