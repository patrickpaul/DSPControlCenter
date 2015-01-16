namespace SA_Resources.SAControls
{
    partial class SAGainFader
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SAGainFader));
            this.lblGain = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.sliderPB = new System.Windows.Forms.PictureBox();
            this.chkMuted = new SA_Resources.SAControls.PictureCheckbox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sliderPB)).BeginInit();
            this.SuspendLayout();
            // 
            // lblGain
            // 
            this.lblGain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.lblGain.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.lblGain.ForeColor = System.Drawing.Color.White;
            this.lblGain.Location = new System.Drawing.Point(4, 316);
            this.lblGain.Name = "lblGain";
            this.lblGain.Size = new System.Drawing.Size(59, 20);
            this.lblGain.TabIndex = 4;
            this.lblGain.Text = "0.0dB";
            this.lblGain.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel1.Controls.Add(this.sliderPB);
            this.panel1.Location = new System.Drawing.Point(0, 31);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(73, 302);
            this.panel1.TabIndex = 3;
            // 
            // sliderPB
            // 
            this.sliderPB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.sliderPB.Image = global::SA_GFXLib.SA_GFXLib_Resources.ui_fader_slider;
            this.sliderPB.Location = new System.Drawing.Point(18, 93);
            this.sliderPB.Name = "sliderPB";
            this.sliderPB.Size = new System.Drawing.Size(20, 44);
            this.sliderPB.TabIndex = 2;
            this.sliderPB.TabStop = false;
            this.sliderPB.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pic_MouseDown);
            this.sliderPB.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pic_MouseMove);
            this.sliderPB.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pic_MouseUp);
            // 
            // chkMuted
            // 
            this.chkMuted.CheckedImage = global::SA_GFXLib.SA_GFXLib_Resources.ui_btn_mute_red;
            this.chkMuted.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkMuted.Location = new System.Drawing.Point(3, 5);
            this.chkMuted.Name = "chkMuted";
            this.chkMuted.Size = new System.Drawing.Size(61, 23);
            this.chkMuted.TabIndex = 33;
            this.chkMuted.UncheckedImage = global::SA_GFXLib.SA_GFXLib_Resources.ui_btn_mute_grey;
            this.chkMuted.UseVisualStyleBackColor = true;
            this.chkMuted.CheckedChanged += new System.EventHandler(this.chkMuted_CheckedChanged);
            // 
            // SAGainFader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(82)))), ((int)(((byte)(82)))));
            this.Controls.Add(this.chkMuted);
            this.Controls.Add(this.lblGain);
            this.Controls.Add(this.panel1);
            this.Name = "SAGainFader";
            this.Size = new System.Drawing.Size(73, 340);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.sliderPB)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblGain;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox sliderPB;
        private PictureCheckbox chkMuted;
    }
}
