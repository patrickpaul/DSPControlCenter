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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GainForm));
            this.lblGain = new System.Windows.Forms.Label();
            this.btnCancel = new SA_Resources.PictureButton();
            this.btnSave = new SA_Resources.PictureButton();
            this.chkMuted = new SA_Resources.PictureCheckbox();
            this.sliderPB = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.sliderPB)).BeginInit();
            this.panel1.SuspendLayout();
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
            // btnCancel
            // 
            this.btnCancel.AutoResize = true;
            this.btnCancel.BackgroundImage = global::SA_Resources.GlobalResources.ui_btn_cancel;
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
            this.btnSave.BackgroundImage = global::SA_Resources.GlobalResources.ui_btn_save;
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
            // chkMuted
            // 
            this.chkMuted.CheckedImage = global::SA_Resources.GlobalResources.ui_mute_red;
            this.chkMuted.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkMuted.Location = new System.Drawing.Point(26, 8);
            this.chkMuted.Name = "chkMuted";
            this.chkMuted.Size = new System.Drawing.Size(61, 23);
            this.chkMuted.TabIndex = 32;
            this.chkMuted.UncheckedImage = global::SA_Resources.GlobalResources.ui_mute_grey;
            this.chkMuted.UseVisualStyleBackColor = true;
            this.chkMuted.CheckedChanged += new System.EventHandler(this.chkMuted_CheckedChanged);
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
            this.panel1.BackgroundImage = global::SA_Resources.GlobalResources.ui_fader_bg_12toinfinite;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel1.Controls.Add(this.sliderPB);
            this.panel1.Location = new System.Drawing.Point(24, 37);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(73, 302);
            this.panel1.TabIndex = 1;
            // 
            // GainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(82)))), ((int)(((byte)(82)))));
            this.ClientSize = new System.Drawing.Size(121, 380);
            this.ControlBox = false;
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
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblGain;
        private PictureButton btnCancel;
        private PictureButton btnSave;
        private SA_Resources.PictureCheckbox chkMuted;
        private System.Windows.Forms.PictureBox sliderPB;
        private System.Windows.Forms.Panel panel1;
    }
}