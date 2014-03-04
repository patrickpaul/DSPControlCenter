using SA_Resources.SAControls;

namespace SA_Resources.SAForms
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
            this.meter4 = new SignalMeter_Small();
            this.meter3 = new SignalMeter_Small();
            this.meter2 = new SignalMeter_Small();
            this.meter1 = new SignalMeter_Small();
            this.btnSave = new PictureButton();
            this.btnCancel = new PictureButton();
            this.chkBypass = new PictureCheckbox();
            ((System.ComponentModel.ISupportInitialize)(this.nudDuckThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDuckDepth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DialDuckRelease)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DialDuckAttack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DialDuckHold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.meter4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.meter3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.meter2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.meter1)).BeginInit();
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
            // nudDuckThreshold
            // 
            this.nudDuckThreshold.DecimalPlaces = 1;
            this.nudDuckThreshold.Location = new System.Drawing.Point(74, 59);
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
            this.label36.Location = new System.Drawing.Point(18, 133);
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
            this.label4.Location = new System.Drawing.Point(86, 133);
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
            // nudDuckDepth
            // 
            this.nudDuckDepth.DecimalPlaces = 1;
            this.nudDuckDepth.Location = new System.Drawing.Point(74, 94);
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
            this.TextDuckRelease.Location = new System.Drawing.Point(84, 149);
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
            this.DialDuckRelease.Location = new System.Drawing.Point(89, 174);
            this.DialDuckRelease.Name = "DialDuckRelease";
            this.DialDuckRelease.Size = new System.Drawing.Size(40, 40);
            this.DialDuckRelease.TabIndex = 25;
            this.DialDuckRelease.TabStop = false;
            // 
            // TextDuckAttack
            // 
            this.TextDuckAttack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.TextDuckAttack.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextDuckAttack.Location = new System.Drawing.Point(12, 149);
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
            this.DialDuckAttack.Location = new System.Drawing.Point(17, 174);
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
            this.label6.Location = new System.Drawing.Point(153, 133);
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
            this.DialDuckHold.Location = new System.Drawing.Point(161, 174);
            this.DialDuckHold.Name = "DialDuckHold";
            this.DialDuckHold.Size = new System.Drawing.Size(40, 40);
            this.DialDuckHold.TabIndex = 115;
            this.DialDuckHold.TabStop = false;
            // 
            // TextDuckHold
            // 
            this.TextDuckHold.Cursor = System.Windows.Forms.Cursors.Hand;
            this.TextDuckHold.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextDuckHold.Location = new System.Drawing.Point(156, 149);
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
            // meter4
            // 
            this.meter4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("meter4.BackgroundImage")));
            this.meter4.DB = -35D;
            this.meter4.Location = new System.Drawing.Point(363, 57);
            this.meter4.Name = "meter4";
            this.meter4.Size = new System.Drawing.Size(30, 157);
            this.meter4.TabIndex = 125;
            this.meter4.TabStop = false;
            // 
            // meter3
            // 
            this.meter3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("meter3.BackgroundImage")));
            this.meter3.DB = -35D;
            this.meter3.Location = new System.Drawing.Point(319, 57);
            this.meter3.Name = "meter3";
            this.meter3.Size = new System.Drawing.Size(30, 157);
            this.meter3.TabIndex = 124;
            this.meter3.TabStop = false;
            // 
            // meter2
            // 
            this.meter2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("meter2.BackgroundImage")));
            this.meter2.DB = -35D;
            this.meter2.Location = new System.Drawing.Point(275, 57);
            this.meter2.Name = "meter2";
            this.meter2.Size = new System.Drawing.Size(30, 157);
            this.meter2.TabIndex = 123;
            this.meter2.TabStop = false;
            // 
            // meter1
            // 
            this.meter1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("meter1.BackgroundImage")));
            this.meter1.DB = -35D;
            this.meter1.Location = new System.Drawing.Point(231, 57);
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
            this.btnSave.Location = new System.Drawing.Point(156, 241);
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
            this.btnCancel.Location = new System.Drawing.Point(212, 241);
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
            this.chkBypass.Location = new System.Drawing.Point(344, 16);
            this.chkBypass.Name = "chkBypass";
            this.chkBypass.Size = new System.Drawing.Size(61, 23);
            this.chkBypass.TabIndex = 30;
            this.chkBypass.UncheckedImage = global::SA_Resources.GlobalResources.ui_btn_bypass;
            this.chkBypass.UseVisualStyleBackColor = true;
            this.chkBypass.CheckedChanged += new System.EventHandler(this.chkBypass_CheckedChanged);
            // 
            // DuckerForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(82)))), ((int)(((byte)(82)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(417, 277);
            this.Controls.Add(this.meter4);
            this.Controls.Add(this.meter3);
            this.Controls.Add(this.meter2);
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
            this.Name = "DuckerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Ducker Configuration";
            ((System.ComponentModel.ISupportInitialize)(this.nudDuckThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDuckDepth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DialDuckRelease)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DialDuckAttack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DialDuckHold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.meter4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.meter3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.meter2)).EndInit();
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
        private SignalMeter_Small meter4;
        private SignalMeter_Small meter3;
        private SignalMeter_Small meter2;
        private SignalMeter_Small meter1;
    }
}