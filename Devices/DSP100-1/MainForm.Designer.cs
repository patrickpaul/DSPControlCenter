using SA_Resources;
using SA_Resources.SAControls;

namespace DSP100_1_Analog
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
            this.pictureBox37 = new System.Windows.Forms.PictureBox();
            this.btnGain03 = new SA_Resources.SAControls.PictureButton();
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
            ((System.ComponentModel.ISupportInitialize)(this.pictureConnectionStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPresetSelection)).BeginInit();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox37)).BeginInit();
            this.pnlCH2PreMixer.SuspendLayout();
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
            // serialPort
            // 
            this.serialPort.BaudRate = 19200;
            // 
            // dropProgramSelection
            // 
            this.dropProgramSelection.Items.AddRange(new object[] {
            "Preset 0",
            "Preset 0"});
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
            // pictureBox37
            // 
            this.pictureBox37.Image = global::SA_GFXLib.SA_GFXLib_Resources.ui_logo;
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
            this.btnGain03.BackgroundImage = global::SA_GFXLib.SA_GFXLib_Resources.ui_btn_gain;
            this.btnGain03.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGain03.Location = new System.Drawing.Point(270, 0);
            this.btnGain03.Name = "btnGain03";
            this.btnGain03.OverImage = null;
            this.btnGain03.Overlay1Image = global::SA_GFXLib.SA_GFXLib_Resources.ui_icon_overlay_bypassed;
            this.btnGain03.Overlay1Visible = false;
            this.btnGain03.Overlay2Image = global::SA_GFXLib.SA_GFXLib_Resources.ui_icon_overlay_mute;
            this.btnGain03.Overlay2Visible = false;
            this.btnGain03.Overlay3Image = null;
            this.btnGain03.Overlay3Visible = false;
            this.btnGain03.PressedImage = global::SA_GFXLib.SA_GFXLib_Resources.ui_btn_gain_over;
            this.btnGain03.Size = new System.Drawing.Size(39, 39);
            this.btnGain03.TabIndex = 31;
            this.btnGain03.ToolTipText = "";
            // 
            // btnCompressor01
            // 
            this.btnCompressor01.AutoResize = false;
            this.btnCompressor01.BackgroundImage = global::SA_GFXLib.SA_GFXLib_Resources.ui_btn_limiter;
            this.btnCompressor01.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCompressor01.Location = new System.Drawing.Point(190, 0);
            this.btnCompressor01.Name = "btnCompressor01";
            this.btnCompressor01.OverImage = null;
            this.btnCompressor01.Overlay1Image = global::SA_GFXLib.SA_GFXLib_Resources.ui_icon_overlay_bypassed;
            this.btnCompressor01.Overlay1Visible = true;
            this.btnCompressor01.Overlay2Image = global::SA_GFXLib.SA_GFXLib_Resources.ui_icon_overlay_mute;
            this.btnCompressor01.Overlay2Visible = false;
            this.btnCompressor01.Overlay3Image = null;
            this.btnCompressor01.Overlay3Visible = false;
            this.btnCompressor01.PressedImage = global::SA_GFXLib.SA_GFXLib_Resources.ui_btn_limiter_over;
            this.btnCompressor01.Size = new System.Drawing.Size(39, 39);
            this.btnCompressor01.TabIndex = 29;
            this.btnCompressor01.ToolTipText = "";
            // 
            // btnGain02
            // 
            this.btnGain02.AutoResize = false;
            this.btnGain02.BackgroundImage = global::SA_GFXLib.SA_GFXLib_Resources.ui_btn_gain;
            this.btnGain02.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGain02.Location = new System.Drawing.Point(30, 0);
            this.btnGain02.Name = "btnGain02";
            this.btnGain02.OverImage = null;
            this.btnGain02.Overlay1Image = global::SA_GFXLib.SA_GFXLib_Resources.ui_icon_overlay_bypassed;
            this.btnGain02.Overlay1Visible = false;
            this.btnGain02.Overlay2Image = global::SA_GFXLib.SA_GFXLib_Resources.ui_icon_overlay_mute;
            this.btnGain02.Overlay2Visible = false;
            this.btnGain02.Overlay3Image = null;
            this.btnGain02.Overlay3Visible = false;
            this.btnGain02.PressedImage = global::SA_GFXLib.SA_GFXLib_Resources.ui_btn_gain_over;
            this.btnGain02.Size = new System.Drawing.Size(39, 39);
            this.btnGain02.TabIndex = 28;
            this.btnGain02.ToolTipText = "";
            // 
            // btnCH0PostFilters
            // 
            this.btnCH0PostFilters.AutoResize = false;
            this.btnCH0PostFilters.BackgroundImage = global::SA_GFXLib.SA_GFXLib_Resources.ui_btn_filters;
            this.btnCH0PostFilters.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCH0PostFilters.Location = new System.Drawing.Point(110, 0);
            this.btnCH0PostFilters.Name = "btnCH0PostFilters";
            this.btnCH0PostFilters.OverImage = null;
            this.btnCH0PostFilters.Overlay1Image = global::SA_GFXLib.SA_GFXLib_Resources.ui_icon_overlay_bypassed;
            this.btnCH0PostFilters.Overlay1Visible = false;
            this.btnCH0PostFilters.Overlay2Image = global::SA_GFXLib.SA_GFXLib_Resources.ui_icon_overlay_mute;
            this.btnCH0PostFilters.Overlay2Visible = false;
            this.btnCH0PostFilters.Overlay3Image = null;
            this.btnCH0PostFilters.Overlay3Visible = false;
            this.btnCH0PostFilters.PressedImage = global::SA_GFXLib.SA_GFXLib_Resources.ui_btn_filters_over;
            this.btnCH0PostFilters.Size = new System.Drawing.Size(39, 39);
            this.btnCH0PostFilters.TabIndex = 27;
            this.btnCH0PostFilters.ToolTipText = "";
            // 
            // btnGain30
            // 
            this.btnGain30.AutoResize = false;
            this.btnGain30.BackgroundImage = global::SA_GFXLib.SA_GFXLib_Resources.ui_btn_gain;
            this.btnGain30.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGain30.Location = new System.Drawing.Point(30, 0);
            this.btnGain30.Name = "btnGain30";
            this.btnGain30.OverImage = null;
            this.btnGain30.Overlay1Image = global::SA_GFXLib.SA_GFXLib_Resources.ui_icon_overlay_bypassed;
            this.btnGain30.Overlay1Visible = false;
            this.btnGain30.Overlay2Image = global::SA_GFXLib.SA_GFXLib_Resources.ui_icon_overlay_mute;
            this.btnGain30.Overlay2Visible = false;
            this.btnGain30.Overlay3Image = null;
            this.btnGain30.Overlay3Visible = false;
            this.btnGain30.PressedImage = global::SA_GFXLib.SA_GFXLib_Resources.ui_btn_gain_over;
            this.btnGain30.Size = new System.Drawing.Size(39, 39);
            this.btnGain30.TabIndex = 36;
            this.btnGain30.ToolTipText = "";
            // 
            // btnCH3PreFilters
            // 
            this.btnCH3PreFilters.AutoResize = false;
            this.btnCH3PreFilters.BackgroundImage = global::SA_GFXLib.SA_GFXLib_Resources.ui_btn_filters;
            this.btnCH3PreFilters.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCH3PreFilters.Location = new System.Drawing.Point(90, 0);
            this.btnCH3PreFilters.Name = "btnCH3PreFilters";
            this.btnCH3PreFilters.OverImage = null;
            this.btnCH3PreFilters.Overlay1Image = global::SA_GFXLib.SA_GFXLib_Resources.ui_icon_overlay_bypassed;
            this.btnCH3PreFilters.Overlay1Visible = false;
            this.btnCH3PreFilters.Overlay2Image = global::SA_GFXLib.SA_GFXLib_Resources.ui_icon_overlay_mute;
            this.btnCH3PreFilters.Overlay2Visible = false;
            this.btnCH3PreFilters.Overlay3Image = null;
            this.btnCH3PreFilters.Overlay3Visible = false;
            this.btnCH3PreFilters.PressedImage = global::SA_GFXLib.SA_GFXLib_Resources.ui_btn_filters_over;
            this.btnCH3PreFilters.Size = new System.Drawing.Size(39, 39);
            this.btnCH3PreFilters.TabIndex = 35;
            this.btnCH3PreFilters.ToolTipText = "";
            // 
            // btnCompressor30
            // 
            this.btnCompressor30.AutoResize = false;
            this.btnCompressor30.BackgroundImage = global::SA_GFXLib.SA_GFXLib_Resources.ui_btn_compressor;
            this.btnCompressor30.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCompressor30.Location = new System.Drawing.Point(152, 0);
            this.btnCompressor30.Name = "btnCompressor30";
            this.btnCompressor30.OverImage = null;
            this.btnCompressor30.Overlay1Image = global::SA_GFXLib.SA_GFXLib_Resources.ui_icon_overlay_bypassed;
            this.btnCompressor30.Overlay1Visible = true;
            this.btnCompressor30.Overlay2Image = global::SA_GFXLib.SA_GFXLib_Resources.ui_icon_overlay_mute;
            this.btnCompressor30.Overlay2Visible = false;
            this.btnCompressor30.Overlay3Image = null;
            this.btnCompressor30.Overlay3Visible = false;
            this.btnCompressor30.PressedImage = global::SA_GFXLib.SA_GFXLib_Resources.ui_btn_compressor_over;
            this.btnCompressor30.Size = new System.Drawing.Size(39, 39);
            this.btnCompressor30.TabIndex = 34;
            this.btnCompressor30.ToolTipText = "";
            // 
            // btnGain31
            // 
            this.btnGain31.AutoResize = false;
            this.btnGain31.BackgroundImage = global::SA_GFXLib.SA_GFXLib_Resources.ui_btn_gain;
            this.btnGain31.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGain31.Location = new System.Drawing.Point(212, 0);
            this.btnGain31.Name = "btnGain31";
            this.btnGain31.OverImage = null;
            this.btnGain31.Overlay1Image = global::SA_GFXLib.SA_GFXLib_Resources.ui_icon_overlay_bypassed;
            this.btnGain31.Overlay1Visible = false;
            this.btnGain31.Overlay2Image = global::SA_GFXLib.SA_GFXLib_Resources.ui_icon_overlay_mute;
            this.btnGain31.Overlay2Visible = false;
            this.btnGain31.Overlay3Image = null;
            this.btnGain31.Overlay3Visible = false;
            this.btnGain31.PressedImage = global::SA_GFXLib.SA_GFXLib_Resources.ui_btn_gain_over;
            this.btnGain31.Size = new System.Drawing.Size(39, 39);
            this.btnGain31.TabIndex = 33;
            this.btnGain31.ToolTipText = "";
            // 
            // btnGain20
            // 
            this.btnGain20.AutoResize = false;
            this.btnGain20.BackgroundImage = global::SA_GFXLib.SA_GFXLib_Resources.ui_btn_gain;
            this.btnGain20.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGain20.Location = new System.Drawing.Point(30, 0);
            this.btnGain20.Name = "btnGain20";
            this.btnGain20.OverImage = null;
            this.btnGain20.Overlay1Image = global::SA_GFXLib.SA_GFXLib_Resources.ui_icon_overlay_bypassed;
            this.btnGain20.Overlay1Visible = false;
            this.btnGain20.Overlay2Image = global::SA_GFXLib.SA_GFXLib_Resources.ui_icon_overlay_mute;
            this.btnGain20.Overlay2Visible = false;
            this.btnGain20.Overlay3Image = null;
            this.btnGain20.Overlay3Visible = false;
            this.btnGain20.PressedImage = global::SA_GFXLib.SA_GFXLib_Resources.ui_btn_gain_over;
            this.btnGain20.Size = new System.Drawing.Size(39, 39);
            this.btnGain20.TabIndex = 36;
            this.btnGain20.ToolTipText = "";
            // 
            // btnCH2PreFilters
            // 
            this.btnCH2PreFilters.AutoResize = false;
            this.btnCH2PreFilters.BackgroundImage = global::SA_GFXLib.SA_GFXLib_Resources.ui_btn_filters;
            this.btnCH2PreFilters.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCH2PreFilters.Location = new System.Drawing.Point(90, 0);
            this.btnCH2PreFilters.Name = "btnCH2PreFilters";
            this.btnCH2PreFilters.OverImage = null;
            this.btnCH2PreFilters.Overlay1Image = global::SA_GFXLib.SA_GFXLib_Resources.ui_icon_overlay_bypassed;
            this.btnCH2PreFilters.Overlay1Visible = false;
            this.btnCH2PreFilters.Overlay2Image = global::SA_GFXLib.SA_GFXLib_Resources.ui_icon_overlay_mute;
            this.btnCH2PreFilters.Overlay2Visible = false;
            this.btnCH2PreFilters.Overlay3Image = null;
            this.btnCH2PreFilters.Overlay3Visible = false;
            this.btnCH2PreFilters.PressedImage = global::SA_GFXLib.SA_GFXLib_Resources.ui_btn_filters_over;
            this.btnCH2PreFilters.Size = new System.Drawing.Size(39, 39);
            this.btnCH2PreFilters.TabIndex = 35;
            this.btnCH2PreFilters.ToolTipText = "";
            // 
            // btnCompressor20
            // 
            this.btnCompressor20.AutoResize = false;
            this.btnCompressor20.BackgroundImage = global::SA_GFXLib.SA_GFXLib_Resources.ui_btn_compressor;
            this.btnCompressor20.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCompressor20.Location = new System.Drawing.Point(152, 0);
            this.btnCompressor20.Name = "btnCompressor20";
            this.btnCompressor20.OverImage = null;
            this.btnCompressor20.Overlay1Image = global::SA_GFXLib.SA_GFXLib_Resources.ui_icon_overlay_bypassed;
            this.btnCompressor20.Overlay1Visible = true;
            this.btnCompressor20.Overlay2Image = global::SA_GFXLib.SA_GFXLib_Resources.ui_icon_overlay_mute;
            this.btnCompressor20.Overlay2Visible = false;
            this.btnCompressor20.Overlay3Image = null;
            this.btnCompressor20.Overlay3Visible = false;
            this.btnCompressor20.PressedImage = global::SA_GFXLib.SA_GFXLib_Resources.ui_btn_compressor_over;
            this.btnCompressor20.Size = new System.Drawing.Size(39, 39);
            this.btnCompressor20.TabIndex = 34;
            this.btnCompressor20.ToolTipText = "";
            // 
            // btnGain21
            // 
            this.btnGain21.AutoResize = false;
            this.btnGain21.BackgroundImage = global::SA_GFXLib.SA_GFXLib_Resources.ui_btn_gain;
            this.btnGain21.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGain21.Location = new System.Drawing.Point(212, 0);
            this.btnGain21.Name = "btnGain21";
            this.btnGain21.OverImage = null;
            this.btnGain21.Overlay1Image = global::SA_GFXLib.SA_GFXLib_Resources.ui_icon_overlay_bypassed;
            this.btnGain21.Overlay1Visible = false;
            this.btnGain21.Overlay2Image = global::SA_GFXLib.SA_GFXLib_Resources.ui_icon_overlay_mute;
            this.btnGain21.Overlay2Visible = false;
            this.btnGain21.Overlay3Image = null;
            this.btnGain21.Overlay3Visible = false;
            this.btnGain21.PressedImage = global::SA_GFXLib.SA_GFXLib_Resources.ui_btn_gain_over;
            this.btnGain21.Size = new System.Drawing.Size(39, 39);
            this.btnGain21.TabIndex = 33;
            this.btnGain21.ToolTipText = "";
            // 
            // btnGain00
            // 
            this.btnGain00.AutoResize = false;
            this.btnGain00.BackgroundImage = global::SA_GFXLib.SA_GFXLib_Resources.ui_btn_gain;
            this.btnGain00.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGain00.Location = new System.Drawing.Point(30, 0);
            this.btnGain00.Name = "btnGain00";
            this.btnGain00.OverImage = null;
            this.btnGain00.Overlay1Image = global::SA_GFXLib.SA_GFXLib_Resources.ui_icon_overlay_bypassed;
            this.btnGain00.Overlay1Visible = false;
            this.btnGain00.Overlay2Image = global::SA_GFXLib.SA_GFXLib_Resources.ui_icon_overlay_mute;
            this.btnGain00.Overlay2Visible = false;
            this.btnGain00.Overlay3Image = null;
            this.btnGain00.Overlay3Visible = false;
            this.btnGain00.PressedImage = global::SA_GFXLib.SA_GFXLib_Resources.ui_btn_gain_over;
            this.btnGain00.Size = new System.Drawing.Size(39, 39);
            this.btnGain00.TabIndex = 35;
            this.btnGain00.ToolTipText = "";
            // 
            // btnCH0PreFilters
            // 
            this.btnCH0PreFilters.AutoResize = false;
            this.btnCH0PreFilters.BackgroundImage = global::SA_GFXLib.SA_GFXLib_Resources.ui_btn_filters;
            this.btnCH0PreFilters.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCH0PreFilters.Location = new System.Drawing.Point(90, 0);
            this.btnCH0PreFilters.Name = "btnCH0PreFilters";
            this.btnCH0PreFilters.OverImage = null;
            this.btnCH0PreFilters.Overlay1Image = global::SA_GFXLib.SA_GFXLib_Resources.ui_icon_overlay_bypassed;
            this.btnCH0PreFilters.Overlay1Visible = false;
            this.btnCH0PreFilters.Overlay2Image = global::SA_GFXLib.SA_GFXLib_Resources.ui_icon_overlay_mute;
            this.btnCH0PreFilters.Overlay2Visible = false;
            this.btnCH0PreFilters.Overlay3Image = null;
            this.btnCH0PreFilters.Overlay3Visible = false;
            this.btnCH0PreFilters.PressedImage = global::SA_GFXLib.SA_GFXLib_Resources.ui_btn_filters_over;
            this.btnCH0PreFilters.Size = new System.Drawing.Size(39, 39);
            this.btnCH0PreFilters.TabIndex = 34;
            this.btnCH0PreFilters.ToolTipText = "Filters";
            // 
            // btnCompressor00
            // 
            this.btnCompressor00.AutoResize = false;
            this.btnCompressor00.BackgroundImage = global::SA_GFXLib.SA_GFXLib_Resources.ui_btn_compressor;
            this.btnCompressor00.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCompressor00.Location = new System.Drawing.Point(152, 0);
            this.btnCompressor00.Name = "btnCompressor00";
            this.btnCompressor00.OverImage = null;
            this.btnCompressor00.Overlay1Image = global::SA_GFXLib.SA_GFXLib_Resources.ui_icon_overlay_bypassed;
            this.btnCompressor00.Overlay1Visible = false;
            this.btnCompressor00.Overlay2Image = global::SA_GFXLib.SA_GFXLib_Resources.ui_icon_overlay_mute;
            this.btnCompressor00.Overlay2Visible = false;
            this.btnCompressor00.Overlay3Image = null;
            this.btnCompressor00.Overlay3Visible = false;
            this.btnCompressor00.PressedImage = global::SA_GFXLib.SA_GFXLib_Resources.ui_btn_compressor_over;
            this.btnCompressor00.Size = new System.Drawing.Size(39, 39);
            this.btnCompressor00.TabIndex = 33;
            this.btnCompressor00.ToolTipText = "Compressor";
            // 
            // btnGain01
            // 
            this.btnGain01.AutoResize = false;
            this.btnGain01.BackgroundImage = global::SA_GFXLib.SA_GFXLib_Resources.ui_btn_gain;
            this.btnGain01.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGain01.Location = new System.Drawing.Point(212, 0);
            this.btnGain01.Name = "btnGain01";
            this.btnGain01.OverImage = null;
            this.btnGain01.Overlay1Image = global::SA_GFXLib.SA_GFXLib_Resources.ui_icon_overlay_bypassed;
            this.btnGain01.Overlay1Visible = false;
            this.btnGain01.Overlay2Image = global::SA_GFXLib.SA_GFXLib_Resources.ui_icon_overlay_mute;
            this.btnGain01.Overlay2Visible = false;
            this.btnGain01.Overlay3Image = null;
            this.btnGain01.Overlay3Visible = false;
            this.btnGain01.PressedImage = global::SA_GFXLib.SA_GFXLib_Resources.ui_btn_gain_over;
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
            this.pnlCH2PreMixer.BackgroundImage = global::SA_GFXLib.SA_GFXLib_Resources.ui_bg_horizontal_line;
            this.pnlCH2PreMixer.Controls.Add(this.btnGain10);
            this.pnlCH2PreMixer.Controls.Add(this.btnCH1PreFilters);
            this.pnlCH2PreMixer.Controls.Add(this.btnCompressor10);
            this.pnlCH2PreMixer.Controls.Add(this.btnGain11);
            this.pnlCH2PreMixer.Location = new System.Drawing.Point(119, 139);
            this.pnlCH2PreMixer.Name = "pnlCH2PreMixer";
            this.pnlCH2PreMixer.Size = new System.Drawing.Size(281, 39);
            this.pnlCH2PreMixer.TabIndex = 78;
            // 
            // btnGain10
            // 
            this.btnGain10.AutoResize = false;
            this.btnGain10.BackgroundImage = global::SA_GFXLib.SA_GFXLib_Resources.ui_btn_gain;
            this.btnGain10.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGain10.Location = new System.Drawing.Point(30, 0);
            this.btnGain10.Name = "btnGain10";
            this.btnGain10.OverImage = null;
            this.btnGain10.Overlay1Image = global::SA_GFXLib.SA_GFXLib_Resources.ui_icon_overlay_bypassed;
            this.btnGain10.Overlay1Visible = false;
            this.btnGain10.Overlay2Image = global::SA_GFXLib.SA_GFXLib_Resources.ui_icon_overlay_mute;
            this.btnGain10.Overlay2Visible = false;
            this.btnGain10.Overlay3Image = null;
            this.btnGain10.Overlay3Visible = false;
            this.btnGain10.PressedImage = global::SA_GFXLib.SA_GFXLib_Resources.ui_btn_gain_over;
            this.btnGain10.Size = new System.Drawing.Size(39, 39);
            this.btnGain10.TabIndex = 36;
            this.btnGain10.ToolTipText = "";
            // 
            // btnCH1PreFilters
            // 
            this.btnCH1PreFilters.AutoResize = false;
            this.btnCH1PreFilters.BackgroundImage = global::SA_GFXLib.SA_GFXLib_Resources.ui_btn_filters;
            this.btnCH1PreFilters.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCH1PreFilters.Location = new System.Drawing.Point(90, 0);
            this.btnCH1PreFilters.Name = "btnCH1PreFilters";
            this.btnCH1PreFilters.OverImage = null;
            this.btnCH1PreFilters.Overlay1Image = global::SA_GFXLib.SA_GFXLib_Resources.ui_icon_overlay_bypassed;
            this.btnCH1PreFilters.Overlay1Visible = false;
            this.btnCH1PreFilters.Overlay2Image = global::SA_GFXLib.SA_GFXLib_Resources.ui_icon_overlay_mute;
            this.btnCH1PreFilters.Overlay2Visible = false;
            this.btnCH1PreFilters.Overlay3Image = null;
            this.btnCH1PreFilters.Overlay3Visible = false;
            this.btnCH1PreFilters.PressedImage = global::SA_GFXLib.SA_GFXLib_Resources.ui_btn_filters_over;
            this.btnCH1PreFilters.Size = new System.Drawing.Size(39, 39);
            this.btnCH1PreFilters.TabIndex = 35;
            this.btnCH1PreFilters.ToolTipText = "";
            // 
            // btnCompressor10
            // 
            this.btnCompressor10.AutoResize = false;
            this.btnCompressor10.BackgroundImage = global::SA_GFXLib.SA_GFXLib_Resources.ui_btn_compressor;
            this.btnCompressor10.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCompressor10.Location = new System.Drawing.Point(152, 0);
            this.btnCompressor10.Name = "btnCompressor10";
            this.btnCompressor10.OverImage = null;
            this.btnCompressor10.Overlay1Image = global::SA_GFXLib.SA_GFXLib_Resources.ui_icon_overlay_bypassed;
            this.btnCompressor10.Overlay1Visible = true;
            this.btnCompressor10.Overlay2Image = global::SA_GFXLib.SA_GFXLib_Resources.ui_icon_overlay_mute;
            this.btnCompressor10.Overlay2Visible = false;
            this.btnCompressor10.Overlay3Image = null;
            this.btnCompressor10.Overlay3Visible = false;
            this.btnCompressor10.PressedImage = global::SA_GFXLib.SA_GFXLib_Resources.ui_btn_compressor_over;
            this.btnCompressor10.Size = new System.Drawing.Size(39, 39);
            this.btnCompressor10.TabIndex = 34;
            this.btnCompressor10.ToolTipText = "";
            // 
            // btnGain11
            // 
            this.btnGain11.AutoResize = false;
            this.btnGain11.BackgroundImage = global::SA_GFXLib.SA_GFXLib_Resources.ui_btn_gain;
            this.btnGain11.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGain11.Location = new System.Drawing.Point(212, 0);
            this.btnGain11.Name = "btnGain11";
            this.btnGain11.OverImage = null;
            this.btnGain11.Overlay1Image = global::SA_GFXLib.SA_GFXLib_Resources.ui_icon_overlay_bypassed;
            this.btnGain11.Overlay1Visible = false;
            this.btnGain11.Overlay2Image = global::SA_GFXLib.SA_GFXLib_Resources.ui_icon_overlay_mute;
            this.btnGain11.Overlay2Visible = false;
            this.btnGain11.Overlay3Image = null;
            this.btnGain11.Overlay3Visible = false;
            this.btnGain11.PressedImage = global::SA_GFXLib.SA_GFXLib_Resources.ui_btn_gain_over;
            this.btnGain11.Size = new System.Drawing.Size(39, 39);
            this.btnGain11.TabIndex = 33;
            this.btnGain11.ToolTipText = "";
            // 
            // btnMatrixMixer
            // 
            this.btnMatrixMixer.AutoResize = true;
            this.btnMatrixMixer.BackgroundImage = global::SA_GFXLib.SA_GFXLib_Resources.ui_icon_matrix;
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
            this.pnlCH1PostMixer.BackgroundImage = global::SA_GFXLib.SA_GFXLib_Resources.ui_bg_horizontal_line;
            this.pnlCH1PostMixer.Controls.Add(this.btnGain02);
            this.pnlCH1PostMixer.Controls.Add(this.btnCH0PostFilters);
            this.pnlCH1PostMixer.Controls.Add(this.btnCompressor01);
            this.pnlCH1PostMixer.Controls.Add(this.btnGain03);
            this.pnlCH1PostMixer.Location = new System.Drawing.Point(454, 91);
            this.pnlCH1PostMixer.Name = "pnlCH1PostMixer";
            this.pnlCH1PostMixer.Size = new System.Drawing.Size(335, 39);
            this.pnlCH1PostMixer.TabIndex = 69;
            // 
            // pnlCH4PreMixer
            // 
            this.pnlCH4PreMixer.BackgroundImage = global::SA_GFXLib.SA_GFXLib_Resources.ui_bg_horizontal_line;
            this.pnlCH4PreMixer.Controls.Add(this.btnGain30);
            this.pnlCH4PreMixer.Controls.Add(this.btnCH3PreFilters);
            this.pnlCH4PreMixer.Controls.Add(this.btnCompressor30);
            this.pnlCH4PreMixer.Controls.Add(this.btnGain31);
            this.pnlCH4PreMixer.Location = new System.Drawing.Point(119, 235);
            this.pnlCH4PreMixer.Name = "pnlCH4PreMixer";
            this.pnlCH4PreMixer.Size = new System.Drawing.Size(281, 39);
            this.pnlCH4PreMixer.TabIndex = 68;
            // 
            // pnlCH3PreMixer
            // 
            this.pnlCH3PreMixer.BackgroundImage = global::SA_GFXLib.SA_GFXLib_Resources.ui_bg_horizontal_line;
            this.pnlCH3PreMixer.Controls.Add(this.btnGain20);
            this.pnlCH3PreMixer.Controls.Add(this.btnCH2PreFilters);
            this.pnlCH3PreMixer.Controls.Add(this.btnCompressor20);
            this.pnlCH3PreMixer.Controls.Add(this.btnGain21);
            this.pnlCH3PreMixer.Location = new System.Drawing.Point(119, 187);
            this.pnlCH3PreMixer.Name = "pnlCH3PreMixer";
            this.pnlCH3PreMixer.Size = new System.Drawing.Size(281, 39);
            this.pnlCH3PreMixer.TabIndex = 67;
            // 
            // pnlCH1PreMixer
            // 
            this.pnlCH1PreMixer.BackgroundImage = global::SA_GFXLib.SA_GFXLib_Resources.ui_bg_horizontal_line;
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
            this.lblCH4Input.Text = "RCA Input 2";
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
            this.lblCH3Input.Text = "RCA Input 1";
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
            this.lblCH2Input.Text = "Bal Input 2";
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
            this.lblCH1Input.Text = "Bal Input 1";
            this.lblCH1Input.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // picDuckerLine
            // 
            this.picDuckerLine.BackgroundImage = global::SA_GFXLib.SA_GFXLib_Resources.ui_bg_vertical_line;
            this.picDuckerLine.Location = new System.Drawing.Point(197, 109);
            this.picDuckerLine.Name = "picDuckerLine";
            this.picDuckerLine.Size = new System.Drawing.Size(4, 179);
            this.picDuckerLine.TabIndex = 88;
            this.picDuckerLine.TabStop = false;
            // 
            // pbtnDucker
            // 
            this.pbtnDucker.AutoResize = false;
            this.pbtnDucker.BackgroundImage = global::SA_GFXLib.SA_GFXLib_Resources.ui_btn_duck;
            this.pbtnDucker.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbtnDucker.Location = new System.Drawing.Point(180, 288);
            this.pbtnDucker.Name = "pbtnDucker";
            this.pbtnDucker.OverImage = null;
            this.pbtnDucker.Overlay1Image = global::SA_GFXLib.SA_GFXLib_Resources.ui_icon_overlay_bypassed;
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
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(82)))), ((int)(((byte)(82)))));
            this.ClientSize = new System.Drawing.Size(906, 404);
            this.Controls.Add(this.pbtnDucker);
            this.Controls.Add(this.picDuckerLine);
            this.Controls.Add(this.pnlCH2PreMixer);
            this.Controls.Add(this.btnMatrixMixer);
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
            this.Text = "DSP100-1";
            this.Controls.SetChildIndex(this.pbPresetSelection, 0);
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
            this.Controls.SetChildIndex(this.btnMatrixMixer, 0);
            this.Controls.SetChildIndex(this.pnlCH2PreMixer, 0);
            this.Controls.SetChildIndex(this.picDuckerLine, 0);
            this.Controls.SetChildIndex(this.pbtnDucker, 0);
            ((System.ComponentModel.ISupportInitialize)(this.pictureConnectionStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPresetSelection)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox37)).EndInit();
            this.pnlCH2PreMixer.ResumeLayout(false);
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
        private PictureButton btnCompressor01;
        private PictureButton btnGain02;
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
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}