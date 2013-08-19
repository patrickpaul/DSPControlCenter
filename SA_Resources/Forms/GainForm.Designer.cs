namespace SA_Resources
{
    partial class GainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GainForm));
            this.lblGain = new System.Windows.Forms.Label();
            this.sliderPB = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.signalTimer = new System.Windows.Forms.Timer(this.components);
            this.chkMuted = new SA_Resources.PictureCheckbox();
            this.btnCancel = new SA_Resources.PictureButton();
            this.btnSave = new SA_Resources.PictureButton();
            this.gainMeter = new SA_Resources.SignalMeter();
            ((System.ComponentModel.ISupportInitialize)(this.sliderPB)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gainMeter)).BeginInit();
            this.SuspendLayout();
            // 
            // lblGain
            // 
            this.lblGain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.lblGain.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.lblGain.ForeColor = System.Drawing.Color.White;
            this.lblGain.Location = new System.Drawing.Point(28, 319);
            this.lblGain.Name = "lblGain";
            this.lblGain.Size = new System.Drawing.Size(59, 20);
            this.lblGain.TabIndex = 2;
            this.lblGain.Text = "0.0dB";
            this.lblGain.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // sliderPB
            // 
            this.sliderPB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.sliderPB.Image = ((System.Drawing.Image)(resources.GetObject("sliderPB.Image")));
            this.sliderPB.Location = new System.Drawing.Point(18, 236);
            this.sliderPB.Name = "sliderPB";
            this.sliderPB.Size = new System.Drawing.Size(20, 44);
            this.sliderPB.TabIndex = 2;
            this.sliderPB.TabStop = false;
            this.sliderPB.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pic_MouseDown);
            this.sliderPB.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pic_MouseMove);
            this.sliderPB.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pic_MouseUp);
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel1.Controls.Add(this.sliderPB);
            this.panel1.Location = new System.Drawing.Point(24, 37);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(73, 302);
            this.panel1.TabIndex = 1;
            // 
            // signalTimer
            // 
            this.signalTimer.Tick += new System.EventHandler(this.signalTimer_Tick);
            // 
            // chkMuted
            // 
            this.chkMuted.CheckedImage = ((System.Drawing.Image)(resources.GetObject("chkMuted.CheckedImage")));
            this.chkMuted.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkMuted.Location = new System.Drawing.Point(26, 8);
            this.chkMuted.Name = "chkMuted";
            this.chkMuted.Size = new System.Drawing.Size(61, 23);
            this.chkMuted.TabIndex = 32;
            this.chkMuted.UncheckedImage = ((System.Drawing.Image)(resources.GetObject("chkMuted.UncheckedImage")));
            this.chkMuted.UseVisualStyleBackColor = true;
            this.chkMuted.CheckedChanged += new System.EventHandler(this.chkMuted_CheckedChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.AutoResize = true;
            this.btnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.BackgroundImage")));
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Location = new System.Drawing.Point(64, 351);
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
            this.btnCancel.TabIndex = 31;
            this.btnCancel.ToolTipText = "";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.AutoResize = true;
            this.btnSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSave.BackgroundImage")));
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Location = new System.Drawing.Point(8, 351);
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
            this.btnSave.TabIndex = 30;
            this.btnSave.ToolTipText = "";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // gainMeter
            // 
            this.gainMeter.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("gainMeter.BackgroundImage")));
            this.gainMeter.DB = -35D;
            this.gainMeter.Location = new System.Drawing.Point(116, 69);
            this.gainMeter.Name = "gainMeter";
            this.gainMeter.Size = new System.Drawing.Size(43, 225);
            this.gainMeter.TabIndex = 34;
            this.gainMeter.TabStop = false;
            // 
            // GainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(82)))), ((int)(((byte)(82)))));
            this.ClientSize = new System.Drawing.Size(179, 380);
            this.ControlBox = false;
            this.Controls.Add(this.gainMeter);
            this.Controls.Add(this.chkMuted);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblGain);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GainForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CH 1 - Gain";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GainForm_FormClosing);
            this.Load += new System.EventHandler(this.GainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.sliderPB)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gainMeter)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblGain;
        private PictureButton btnCancel;
        private PictureButton btnSave;
        private SA_Resources.PictureCheckbox chkMuted;
        private System.Windows.Forms.PictureBox sliderPB;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Timer signalTimer;
        private SignalMeter gainMeter;
    }
}