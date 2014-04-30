using SA_Resources.SAControls;

namespace SA_Resources.SAForms
{
    partial class DuckerForm4x4
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DuckerForm4x4));
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.nudDuckThreshold = new System.Windows.Forms.NumericUpDown();
            this.label36 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.signalTimer = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.nudDuckDepth = new System.Windows.Forms.NumericUpDown();
            this.TextDuckRelease = new System.Windows.Forms.TextBox();
            this.DialDuckRelease = new System.Windows.Forms.PictureBox();
            this.TextDuckAttack = new System.Windows.Forms.TextBox();
            this.DialDuckAttack = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.DialDuckHold = new System.Windows.Forms.PictureBox();
            this.TextDuckHold = new System.Windows.Forms.TextBox();
            this.dropPriorityChannel = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.lblDuckInput0 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblDuckInput1 = new System.Windows.Forms.Label();
            this.lblDuckInput2 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.chkBypass2 = new SA_Resources.SAControls.PictureCheckbox();
            this.chkBypass1 = new SA_Resources.SAControls.PictureCheckbox();
            this.chkBypass0 = new SA_Resources.SAControls.PictureCheckbox();
            this.meter1 = new SA_Resources.SAControls.SignalMeter_Small();
            this.btnSave = new SA_Resources.SAControls.PictureButton();
            this.btnCancel = new SA_Resources.SAControls.PictureButton();
            this.chkBypass = new SA_Resources.SAControls.PictureCheckbox();
            ((System.ComponentModel.ISupportInitialize)(this.nudDuckThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDuckDepth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DialDuckRelease)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DialDuckAttack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DialDuckHold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.meter1)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.label2.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label2.Location = new System.Drawing.Point(277, 65);
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
            this.label3.Location = new System.Drawing.Point(162, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Threshold";
            // 
            // nudDuckThreshold
            // 
            this.nudDuckThreshold.DecimalPlaces = 1;
            this.nudDuckThreshold.Location = new System.Drawing.Point(224, 61);
            this.nudDuckThreshold.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudDuckThreshold.Minimum = new decimal(new int[] {
            60,
            0,
            0,
            -2147483648});
            this.nudDuckThreshold.Name = "nudDuckThreshold";
            this.nudDuckThreshold.Size = new System.Drawing.Size(53, 20);
            this.nudDuckThreshold.TabIndex = 10;
            this.nudDuckThreshold.Value = new decimal(new int[] {
            20,
            0,
            0,
            -2147483648});
            this.nudDuckThreshold.ValueChanged += new System.EventHandler(this.nudDuckThreshold_ValueChanged);
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label36.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label36.Location = new System.Drawing.Point(168, 135);
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
            this.label4.Location = new System.Drawing.Point(236, 135);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 26;
            this.label4.Text = "Release";
            // 
            // signalTimer
            // 
            this.signalTimer.Interval = 70;
            this.signalTimer.Tick += new System.EventHandler(this.signalTimer_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.label1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label1.Location = new System.Drawing.Point(277, 100);
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
            this.label5.Location = new System.Drawing.Point(162, 100);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 13);
            this.label5.TabIndex = 113;
            this.label5.Text = "Depth";
            // 
            // nudDuckDepth
            // 
            this.nudDuckDepth.DecimalPlaces = 1;
            this.nudDuckDepth.Location = new System.Drawing.Point(224, 96);
            this.nudDuckDepth.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudDuckDepth.Minimum = new decimal(new int[] {
            60,
            0,
            0,
            -2147483648});
            this.nudDuckDepth.Name = "nudDuckDepth";
            this.nudDuckDepth.Size = new System.Drawing.Size(53, 20);
            this.nudDuckDepth.TabIndex = 112;
            this.nudDuckDepth.Value = new decimal(new int[] {
            20,
            0,
            0,
            -2147483648});
            this.nudDuckDepth.ValueChanged += new System.EventHandler(this.nudDuckDepth_ValueChanged);
            // 
            // TextDuckRelease
            // 
            this.TextDuckRelease.Cursor = System.Windows.Forms.Cursors.Hand;
            this.TextDuckRelease.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextDuckRelease.Location = new System.Drawing.Point(234, 151);
            this.TextDuckRelease.MaxLength = 10;
            this.TextDuckRelease.Name = "TextDuckRelease";
            this.TextDuckRelease.Size = new System.Drawing.Size(50, 22);
            this.TextDuckRelease.TabIndex = 27;
            this.TextDuckRelease.Text = "10.0ms";
            this.TextDuckRelease.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // DialDuckRelease
            // 
            this.DialDuckRelease.BackgroundImage = global::SA_Resources.GlobalResources.knob_orange_bg;
            this.DialDuckRelease.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DialDuckRelease.Image = global::SA_Resources.GlobalResources.knob_orange_line;
            this.DialDuckRelease.InitialImage = null;
            this.DialDuckRelease.Location = new System.Drawing.Point(239, 176);
            this.DialDuckRelease.Name = "DialDuckRelease";
            this.DialDuckRelease.Size = new System.Drawing.Size(40, 40);
            this.DialDuckRelease.TabIndex = 25;
            this.DialDuckRelease.TabStop = false;
            // 
            // TextDuckAttack
            // 
            this.TextDuckAttack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.TextDuckAttack.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextDuckAttack.Location = new System.Drawing.Point(162, 151);
            this.TextDuckAttack.MaxLength = 10;
            this.TextDuckAttack.Name = "TextDuckAttack";
            this.TextDuckAttack.Size = new System.Drawing.Size(50, 22);
            this.TextDuckAttack.TabIndex = 24;
            this.TextDuckAttack.Text = "10.0ms";
            this.TextDuckAttack.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // DialDuckAttack
            // 
            this.DialDuckAttack.BackgroundImage = global::SA_Resources.GlobalResources.knob_blue_bg;
            this.DialDuckAttack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DialDuckAttack.Image = global::SA_Resources.GlobalResources.knob_blue_line;
            this.DialDuckAttack.InitialImage = null;
            this.DialDuckAttack.Location = new System.Drawing.Point(167, 176);
            this.DialDuckAttack.Name = "DialDuckAttack";
            this.DialDuckAttack.Size = new System.Drawing.Size(40, 40);
            this.DialDuckAttack.TabIndex = 17;
            this.DialDuckAttack.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label6.Location = new System.Drawing.Point(303, 135);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 13);
            this.label6.TabIndex = 116;
            this.label6.Text = "Hold Time";
            // 
            // DialDuckHold
            // 
            this.DialDuckHold.BackgroundImage = global::SA_Resources.GlobalResources.knob_green_bg;
            this.DialDuckHold.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DialDuckHold.Image = global::SA_Resources.GlobalResources.knob_green_line;
            this.DialDuckHold.InitialImage = null;
            this.DialDuckHold.Location = new System.Drawing.Point(311, 176);
            this.DialDuckHold.Name = "DialDuckHold";
            this.DialDuckHold.Size = new System.Drawing.Size(40, 40);
            this.DialDuckHold.TabIndex = 115;
            this.DialDuckHold.TabStop = false;
            // 
            // TextDuckHold
            // 
            this.TextDuckHold.Cursor = System.Windows.Forms.Cursors.Hand;
            this.TextDuckHold.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextDuckHold.Location = new System.Drawing.Point(306, 151);
            this.TextDuckHold.MaxLength = 10;
            this.TextDuckHold.Name = "TextDuckHold";
            this.TextDuckHold.Size = new System.Drawing.Size(50, 22);
            this.TextDuckHold.TabIndex = 117;
            this.TextDuckHold.Text = "10.0ms";
            this.TextDuckHold.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // dropPriorityChannel
            // 
            this.dropPriorityChannel.FormattingEnabled = true;
            this.dropPriorityChannel.Location = new System.Drawing.Point(113, 16);
            this.dropPriorityChannel.Name = "dropPriorityChannel";
            this.dropPriorityChannel.Size = new System.Drawing.Size(133, 21);
            this.dropPriorityChannel.TabIndex = 119;
            this.dropPriorityChannel.SelectedIndexChanged += new System.EventHandler(this.dropPriorityChannel_SelectedIndexChanged);
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
            // lblDuckInput0
            // 
            this.lblDuckInput0.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.lblDuckInput0.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblDuckInput0.Location = new System.Drawing.Point(2, 96);
            this.lblDuckInput0.Name = "lblDuckInput0";
            this.lblDuckInput0.Size = new System.Drawing.Size(109, 20);
            this.lblDuckInput0.TabIndex = 127;
            this.lblDuckInput0.Text = "Local Input #1";
            this.lblDuckInput0.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.label9.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label9.Location = new System.Drawing.Point(12, 67);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(109, 13);
            this.label9.TabIndex = 128;
            this.label9.Text = "Duck the following:";
            // 
            // lblDuckInput1
            // 
            this.lblDuckInput1.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.lblDuckInput1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblDuckInput1.Location = new System.Drawing.Point(2, 129);
            this.lblDuckInput1.Name = "lblDuckInput1";
            this.lblDuckInput1.Size = new System.Drawing.Size(109, 20);
            this.lblDuckInput1.TabIndex = 130;
            this.lblDuckInput1.Text = "Local Input #1";
            this.lblDuckInput1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDuckInput2
            // 
            this.lblDuckInput2.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.lblDuckInput2.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblDuckInput2.Location = new System.Drawing.Point(2, 161);
            this.lblDuckInput2.Name = "lblDuckInput2";
            this.lblDuckInput2.Size = new System.Drawing.Size(109, 20);
            this.lblDuckInput2.TabIndex = 132;
            this.lblDuckInput2.Text = "Local Input #1";
            this.lblDuckInput2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.label13.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label13.Location = new System.Drawing.Point(370, 228);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(43, 13);
            this.label13.TabIndex = 135;
            this.label13.Text = "Priority";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.label14.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label14.Location = new System.Drawing.Point(366, 241);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(50, 13);
            this.label14.TabIndex = 136;
            this.label14.Text = "Channel";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkBypass2
            // 
            this.chkBypass2.CheckedImage = null;
            this.chkBypass2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkBypass2.Location = new System.Drawing.Point(117, 164);
            this.chkBypass2.Name = "chkBypass2";
            this.chkBypass2.Size = new System.Drawing.Size(16, 16);
            this.chkBypass2.TabIndex = 131;
            this.chkBypass2.UncheckedImage = null;
            this.chkBypass2.UseVisualStyleBackColor = true;
            this.chkBypass2.CheckedChanged += new System.EventHandler(this.chkChannelBypass_CheckedChanged);
            // 
            // chkBypass1
            // 
            this.chkBypass1.CheckedImage = null;
            this.chkBypass1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkBypass1.Location = new System.Drawing.Point(117, 131);
            this.chkBypass1.Name = "chkBypass1";
            this.chkBypass1.Size = new System.Drawing.Size(16, 16);
            this.chkBypass1.TabIndex = 129;
            this.chkBypass1.UncheckedImage = null;
            this.chkBypass1.UseVisualStyleBackColor = true;
            this.chkBypass1.CheckedChanged += new System.EventHandler(this.chkChannelBypass_CheckedChanged);
            // 
            // chkBypass0
            // 
            this.chkBypass0.CheckedImage = null;
            this.chkBypass0.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkBypass0.Location = new System.Drawing.Point(117, 98);
            this.chkBypass0.Name = "chkBypass0";
            this.chkBypass0.Size = new System.Drawing.Size(16, 16);
            this.chkBypass0.TabIndex = 126;
            this.chkBypass0.UncheckedImage = null;
            this.chkBypass0.UseVisualStyleBackColor = true;
            this.chkBypass0.CheckedChanged += new System.EventHandler(this.chkChannelBypass_CheckedChanged);
            // 
            // meter1
            // 
            this.meter1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("meter1.BackgroundImage")));
            this.meter1.DB = -35D;
            this.meter1.Location = new System.Drawing.Point(376, 67);
            this.meter1.Name = "meter1";
            this.meter1.Size = new System.Drawing.Size(30, 157);
            this.meter1.TabIndex = 122;
            this.meter1.TabStop = false;
            // 
            // btnSave
            // 
            this.btnSave.AutoResize = true;
            this.btnSave.BackgroundImage = global::SA_Resources.GlobalResources.ui_btn_save;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Location = new System.Drawing.Point(160, 244);
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
            // btnCancel
            // 
            this.btnCancel.AutoResize = true;
            this.btnCancel.BackgroundImage = global::SA_Resources.GlobalResources.ui_btn_cancel;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Location = new System.Drawing.Point(216, 244);
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
            // chkBypass
            // 
            this.chkBypass.Checked = true;
            this.chkBypass.CheckedImage = global::SA_Resources.GlobalResources.ui_btn_bypass_red;
            this.chkBypass.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBypass.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkBypass.Location = new System.Drawing.Point(300, 14);
            this.chkBypass.Name = "chkBypass";
            this.chkBypass.Size = new System.Drawing.Size(61, 23);
            this.chkBypass.TabIndex = 30;
            this.chkBypass.UncheckedImage = global::SA_Resources.GlobalResources.ui_btn_bypass;
            this.chkBypass.UseVisualStyleBackColor = true;
            this.chkBypass.CheckedChanged += new System.EventHandler(this.chkBypass_CheckedChanged);
            // 
            // DuckerForm4x4
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(82)))), ((int)(((byte)(82)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(424, 289);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.lblDuckInput2);
            this.Controls.Add(this.chkBypass2);
            this.Controls.Add(this.lblDuckInput1);
            this.Controls.Add(this.chkBypass1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.lblDuckInput0);
            this.Controls.Add(this.chkBypass0);
            this.Controls.Add(this.meter1);
            this.Controls.Add(this.dropPriorityChannel);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.DialDuckHold);
            this.Controls.Add(this.TextDuckHold);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.nudDuckDepth);
            this.Controls.Add(this.label36);
            this.Controls.Add(this.DialDuckAttack);
            this.Controls.Add(this.TextDuckAttack);
            this.Controls.Add(this.DialDuckRelease);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.TextDuckRelease);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.chkBypass);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.nudDuckThreshold);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "DuckerForm4x4";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Ducker Configuration";
            this.Load += new System.EventHandler(this.DuckerForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudDuckThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDuckDepth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DialDuckRelease)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DialDuckAttack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DialDuckHold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.meter1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudDuckThreshold;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label4;
        private PictureButton btnSave;
        private PictureButton btnCancel;
        private PictureCheckbox chkBypass;
        private System.Windows.Forms.Timer signalTimer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nudDuckDepth;
        private System.Windows.Forms.TextBox TextDuckRelease;
        private System.Windows.Forms.PictureBox DialDuckRelease;
        private System.Windows.Forms.TextBox TextDuckAttack;
        private System.Windows.Forms.PictureBox DialDuckAttack;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox DialDuckHold;
        private System.Windows.Forms.TextBox TextDuckHold;
        private System.Windows.Forms.ComboBox dropPriorityChannel;
        private System.Windows.Forms.Label label7;
        private SignalMeter_Small meter1;
        private PictureCheckbox chkBypass0;
        private System.Windows.Forms.Label lblDuckInput0;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblDuckInput1;
        private PictureCheckbox chkBypass1;
        private System.Windows.Forms.Label lblDuckInput2;
        private PictureCheckbox chkBypass2;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
    }
}