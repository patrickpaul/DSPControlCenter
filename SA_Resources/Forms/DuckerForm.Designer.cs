namespace SA_Resources
{
    partial class DuckerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DuckerForm));
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.nudCompThreshold = new System.Windows.Forms.NumericUpDown();
            this.label36 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.signalTimer = new System.Windows.Forms.Timer(this.components);
            this.gainMeterOut = new SA_Resources.SignalMeter_Small();
            this.gainMeterIn = new SA_Resources.SignalMeter_Small();
            this.chkBypass = new SA_Resources.PictureCheckbox();
            this.btnCancel = new SA_Resources.PictureButton();
            this.btnSave = new SA_Resources.PictureButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.TextCompRelease = new System.Windows.Forms.TextBox();
            this.DialCompRelease = new System.Windows.Forms.PictureBox();
            this.TextCompAttack = new System.Windows.Forms.TextBox();
            this.DialCompAttack = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.dropInputType = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.signalMeter_Small1 = new SA_Resources.SignalMeter_Small();
            this.signalMeter_Small2 = new SA_Resources.SignalMeter_Small();
            this.signalMeter_Small3 = new SA_Resources.SignalMeter_Small();
            this.signalMeter_Small4 = new SA_Resources.SignalMeter_Small();
            this.signalMeter_Small5 = new SA_Resources.SignalMeter_Small();
            this.signalMeter_Small6 = new SA_Resources.SignalMeter_Small();
            ((System.ComponentModel.ISupportInitialize)(this.nudCompThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gainMeterOut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gainMeterIn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DialCompRelease)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DialCompAttack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.signalMeter_Small1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.signalMeter_Small2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.signalMeter_Small3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.signalMeter_Small4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.signalMeter_Small5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.signalMeter_Small6)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.label2.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label2.Location = new System.Drawing.Point(127, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "dB";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.label3.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label3.Location = new System.Drawing.Point(12, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Threshold";
            // 
            // nudCompThreshold
            // 
            this.nudCompThreshold.DecimalPlaces = 1;
            this.nudCompThreshold.Location = new System.Drawing.Point(74, 59);
            this.nudCompThreshold.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudCompThreshold.Minimum = new decimal(new int[] {
            60,
            0,
            0,
            -2147483648});
            this.nudCompThreshold.Name = "nudCompThreshold";
            this.nudCompThreshold.Size = new System.Drawing.Size(53, 20);
            this.nudCompThreshold.TabIndex = 10;
            this.nudCompThreshold.Value = new decimal(new int[] {
            20,
            0,
            0,
            -2147483648});
            this.nudCompThreshold.ValueChanged += new System.EventHandler(this.nudCompThreshold_ValueChanged);
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label36.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label36.Location = new System.Drawing.Point(289, 47);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(39, 13);
            this.label36.TabIndex = 23;
            this.label36.Text = "Attack";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label4.Location = new System.Drawing.Point(357, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 26;
            this.label4.Text = "Release";
            // 
            // signalTimer
            // 
            this.signalTimer.Tick += new System.EventHandler(this.signalTimer_Tick);
            // 
            // gainMeterOut
            // 
            this.gainMeterOut.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("gainMeterOut.BackgroundImage")));
            this.gainMeterOut.DB = -35D;
            this.gainMeterOut.Location = new System.Drawing.Point(64, 153);
            this.gainMeterOut.Name = "gainMeterOut";
            this.gainMeterOut.Size = new System.Drawing.Size(30, 157);
            this.gainMeterOut.TabIndex = 111;
            this.gainMeterOut.TabStop = false;
            // 
            // gainMeterIn
            // 
            this.gainMeterIn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("gainMeterIn.BackgroundImage")));
            this.gainMeterIn.DB = -35D;
            this.gainMeterIn.Location = new System.Drawing.Point(20, 153);
            this.gainMeterIn.Name = "gainMeterIn";
            this.gainMeterIn.Size = new System.Drawing.Size(30, 157);
            this.gainMeterIn.TabIndex = 110;
            this.gainMeterIn.TabStop = false;
            // 
            // chkBypass
            // 
            this.chkBypass.Checked = true;
            this.chkBypass.CheckedImage = global::SA_Resources.GlobalResources.ui_btn_bypass_red;
            this.chkBypass.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBypass.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkBypass.Location = new System.Drawing.Point(344, 16);
            this.chkBypass.Name = "chkBypass";
            this.chkBypass.Size = new System.Drawing.Size(61, 23);
            this.chkBypass.TabIndex = 30;
            this.chkBypass.UncheckedImage = global::SA_Resources.GlobalResources.ui_btn_bypass;
            this.chkBypass.UseVisualStyleBackColor = true;
            this.chkBypass.CheckedChanged += new System.EventHandler(this.chkBypass_CheckedChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.AutoResize = true;
            this.btnCancel.BackgroundImage = global::SA_Resources.GlobalResources.ui_btn_cancel;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Location = new System.Drawing.Point(212, 340);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.OverImage = null;
            this.btnCancel.Overlay1Image = null;
            this.btnCancel.Overlay1Visible = false;
            this.btnCancel.Overlay2Image = null;
            this.btnCancel.Overlay2Visible = false;
            this.btnCancel.Overlay3Image = null;
            this.btnCancel.Overlay3Visible = false;
            this.btnCancel.PressedImage = null;
            this.btnCancel.Size = new System.Drawing.Size(49, 23);
            this.btnCancel.TabIndex = 29;
            this.btnCancel.ToolTipText = "";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.AutoResize = true;
            this.btnSave.BackgroundImage = global::SA_Resources.GlobalResources.ui_btn_save;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Location = new System.Drawing.Point(156, 340);
            this.btnSave.Name = "btnSave";
            this.btnSave.OverImage = null;
            this.btnSave.Overlay1Image = null;
            this.btnSave.Overlay1Visible = false;
            this.btnSave.Overlay2Image = null;
            this.btnSave.Overlay2Visible = false;
            this.btnSave.Overlay3Image = null;
            this.btnSave.Overlay3Visible = false;
            this.btnSave.PressedImage = null;
            this.btnSave.Size = new System.Drawing.Size(49, 23);
            this.btnSave.TabIndex = 28;
            this.btnSave.ToolTipText = "";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.label1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label1.Location = new System.Drawing.Point(127, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 13);
            this.label1.TabIndex = 114;
            this.label1.Text = "dB";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.label5.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label5.Location = new System.Drawing.Point(12, 98);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 13);
            this.label5.TabIndex = 113;
            this.label5.Text = "Depth";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.DecimalPlaces = 1;
            this.numericUpDown1.Location = new System.Drawing.Point(74, 94);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            60,
            0,
            0,
            -2147483648});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(53, 20);
            this.numericUpDown1.TabIndex = 112;
            this.numericUpDown1.Value = new decimal(new int[] {
            20,
            0,
            0,
            -2147483648});
            // 
            // TextCompRelease
            // 
            this.TextCompRelease.Cursor = System.Windows.Forms.Cursors.Hand;
            this.TextCompRelease.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextCompRelease.Location = new System.Drawing.Point(355, 63);
            this.TextCompRelease.MaxLength = 10;
            this.TextCompRelease.Name = "TextCompRelease";
            this.TextCompRelease.Size = new System.Drawing.Size(50, 22);
            this.TextCompRelease.TabIndex = 27;
            this.TextCompRelease.Text = "10.0ms";
            this.TextCompRelease.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // DialCompRelease
            // 
            this.DialCompRelease.BackgroundImage = global::SA_Resources.GlobalResources.knob_orange_bg;
            this.DialCompRelease.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DialCompRelease.Image = global::SA_Resources.GlobalResources.knob_orange_line;
            this.DialCompRelease.InitialImage = null;
            this.DialCompRelease.Location = new System.Drawing.Point(360, 88);
            this.DialCompRelease.Name = "DialCompRelease";
            this.DialCompRelease.Size = new System.Drawing.Size(40, 40);
            this.DialCompRelease.TabIndex = 25;
            this.DialCompRelease.TabStop = false;
            // 
            // TextCompAttack
            // 
            this.TextCompAttack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.TextCompAttack.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextCompAttack.Location = new System.Drawing.Point(283, 63);
            this.TextCompAttack.MaxLength = 10;
            this.TextCompAttack.Name = "TextCompAttack";
            this.TextCompAttack.Size = new System.Drawing.Size(50, 22);
            this.TextCompAttack.TabIndex = 24;
            this.TextCompAttack.Text = "10.0ms";
            this.TextCompAttack.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // DialCompAttack
            // 
            this.DialCompAttack.BackgroundImage = global::SA_Resources.GlobalResources.knob_blue_bg;
            this.DialCompAttack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DialCompAttack.Image = global::SA_Resources.GlobalResources.knob_blue_line;
            this.DialCompAttack.InitialImage = null;
            this.DialCompAttack.Location = new System.Drawing.Point(288, 88);
            this.DialCompAttack.Name = "DialCompAttack";
            this.DialCompAttack.Size = new System.Drawing.Size(40, 40);
            this.DialCompAttack.TabIndex = 17;
            this.DialCompAttack.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label6.Location = new System.Drawing.Point(207, 47);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 13);
            this.label6.TabIndex = 116;
            this.label6.Text = "Hold Time";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::SA_Resources.GlobalResources.knob_green_bg;
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = global::SA_Resources.GlobalResources.knob_green_line;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(215, 88);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(40, 40);
            this.pictureBox1.TabIndex = 115;
            this.pictureBox1.TabStop = false;
            // 
            // textBox1
            // 
            this.textBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.textBox1.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(210, 63);
            this.textBox1.MaxLength = 10;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(50, 22);
            this.textBox1.TabIndex = 117;
            this.textBox1.Text = "10.0ms";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // dropInputType
            // 
            this.dropInputType.FormattingEnabled = true;
            this.dropInputType.Items.AddRange(new object[] {
            "Line Level +0dB",
            "Microphone +20dB",
            "Microphone +40dB"});
            this.dropInputType.Location = new System.Drawing.Point(113, 16);
            this.dropInputType.Name = "dropInputType";
            this.dropInputType.Size = new System.Drawing.Size(133, 21);
            this.dropInputType.TabIndex = 119;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.label7.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label7.Location = new System.Drawing.Point(12, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(92, 13);
            this.label7.TabIndex = 118;
            this.label7.Text = "Priority Channel:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // signalMeter_Small1
            // 
            this.signalMeter_Small1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("signalMeter_Small1.BackgroundImage")));
            this.signalMeter_Small1.DB = -35D;
            this.signalMeter_Small1.Location = new System.Drawing.Point(152, 153);
            this.signalMeter_Small1.Name = "signalMeter_Small1";
            this.signalMeter_Small1.Size = new System.Drawing.Size(30, 157);
            this.signalMeter_Small1.TabIndex = 121;
            this.signalMeter_Small1.TabStop = false;
            // 
            // signalMeter_Small2
            // 
            this.signalMeter_Small2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("signalMeter_Small2.BackgroundImage")));
            this.signalMeter_Small2.DB = -35D;
            this.signalMeter_Small2.Location = new System.Drawing.Point(108, 153);
            this.signalMeter_Small2.Name = "signalMeter_Small2";
            this.signalMeter_Small2.Size = new System.Drawing.Size(30, 157);
            this.signalMeter_Small2.TabIndex = 120;
            this.signalMeter_Small2.TabStop = false;
            // 
            // signalMeter_Small3
            // 
            this.signalMeter_Small3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("signalMeter_Small3.BackgroundImage")));
            this.signalMeter_Small3.DB = -35D;
            this.signalMeter_Small3.Location = new System.Drawing.Point(366, 153);
            this.signalMeter_Small3.Name = "signalMeter_Small3";
            this.signalMeter_Small3.Size = new System.Drawing.Size(30, 157);
            this.signalMeter_Small3.TabIndex = 125;
            this.signalMeter_Small3.TabStop = false;
            // 
            // signalMeter_Small4
            // 
            this.signalMeter_Small4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("signalMeter_Small4.BackgroundImage")));
            this.signalMeter_Small4.DB = -35D;
            this.signalMeter_Small4.Location = new System.Drawing.Point(322, 153);
            this.signalMeter_Small4.Name = "signalMeter_Small4";
            this.signalMeter_Small4.Size = new System.Drawing.Size(30, 157);
            this.signalMeter_Small4.TabIndex = 124;
            this.signalMeter_Small4.TabStop = false;
            // 
            // signalMeter_Small5
            // 
            this.signalMeter_Small5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("signalMeter_Small5.BackgroundImage")));
            this.signalMeter_Small5.DB = -35D;
            this.signalMeter_Small5.Location = new System.Drawing.Point(278, 153);
            this.signalMeter_Small5.Name = "signalMeter_Small5";
            this.signalMeter_Small5.Size = new System.Drawing.Size(30, 157);
            this.signalMeter_Small5.TabIndex = 123;
            this.signalMeter_Small5.TabStop = false;
            // 
            // signalMeter_Small6
            // 
            this.signalMeter_Small6.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("signalMeter_Small6.BackgroundImage")));
            this.signalMeter_Small6.DB = -35D;
            this.signalMeter_Small6.Location = new System.Drawing.Point(234, 153);
            this.signalMeter_Small6.Name = "signalMeter_Small6";
            this.signalMeter_Small6.Size = new System.Drawing.Size(30, 157);
            this.signalMeter_Small6.TabIndex = 122;
            this.signalMeter_Small6.TabStop = false;
            // 
            // DuckerForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(82)))), ((int)(((byte)(82)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(417, 372);
            this.Controls.Add(this.signalMeter_Small3);
            this.Controls.Add(this.signalMeter_Small4);
            this.Controls.Add(this.signalMeter_Small5);
            this.Controls.Add(this.signalMeter_Small6);
            this.Controls.Add(this.signalMeter_Small1);
            this.Controls.Add(this.signalMeter_Small2);
            this.Controls.Add(this.dropInputType);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.label36);
            this.Controls.Add(this.DialCompAttack);
            this.Controls.Add(this.gainMeterOut);
            this.Controls.Add(this.TextCompAttack);
            this.Controls.Add(this.gainMeterIn);
            this.Controls.Add(this.DialCompRelease);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.TextCompRelease);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.chkBypass);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.nudCompThreshold);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "DuckerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Ducker Configuration";
            ((System.ComponentModel.ISupportInitialize)(this.nudCompThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gainMeterOut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gainMeterIn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DialCompRelease)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DialCompAttack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.signalMeter_Small1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.signalMeter_Small2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.signalMeter_Small3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.signalMeter_Small4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.signalMeter_Small5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.signalMeter_Small6)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudCompThreshold;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label4;
        private PictureButton btnSave;
        private PictureButton btnCancel;
        private SA_Resources.PictureCheckbox chkBypass;
        private System.Windows.Forms.Timer signalTimer;
        private SignalMeter_Small gainMeterIn;
        private SignalMeter_Small gainMeterOut;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.TextBox TextCompRelease;
        private System.Windows.Forms.PictureBox DialCompRelease;
        private System.Windows.Forms.TextBox TextCompAttack;
        private System.Windows.Forms.PictureBox DialCompAttack;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox dropInputType;
        private System.Windows.Forms.Label label7;
        private SignalMeter_Small signalMeter_Small1;
        private SignalMeter_Small signalMeter_Small2;
        private SignalMeter_Small signalMeter_Small3;
        private SignalMeter_Small signalMeter_Small4;
        private SignalMeter_Small signalMeter_Small5;
        private SignalMeter_Small signalMeter_Small6;
    }
}