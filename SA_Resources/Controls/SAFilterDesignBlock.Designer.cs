namespace SA_Resources.SAControls
{
    partial class SAFilterDesignBlock
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
            this.txtQval = new System.Windows.Forms.TextBox();
            this.txtGain = new System.Windows.Forms.TextBox();
            this.txtFreq = new System.Windows.Forms.TextBox();
            this.lblSlope = new System.Windows.Forms.Label();
            this.lblQ = new System.Windows.Forms.Label();
            this.lblGain = new System.Windows.Forms.Label();
            this.lblFreq = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dropFilter = new System.Windows.Forms.ComboBox();
            this.dropSlope = new System.Windows.Forms.ComboBox();
            this.chkBypass = new SA_Resources.SAControls.PictureCheckbox();
            this.SuspendLayout();
            // 
            // txtQval
            // 
            this.txtQval.Location = new System.Drawing.Point(181, 54);
            this.txtQval.MaxLength = 5;
            this.txtQval.Name = "txtQval";
            this.txtQval.Size = new System.Drawing.Size(56, 20);
            this.txtQval.TabIndex = 5;
            this.txtQval.Text = "0.707";
            this.txtQval.Enter += new System.EventHandler(this.Event_Textbox_Enter);
            this.txtQval.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Event_Textbox_KeyPress);
            this.txtQval.Leave += new System.EventHandler(this.Event_Textbox_Leave);
            this.txtQval.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Event_Textbox_MouseUp);
            // 
            // txtGain
            // 
            this.txtGain.Location = new System.Drawing.Point(103, 54);
            this.txtGain.MaxLength = 6;
            this.txtGain.Name = "txtGain";
            this.txtGain.Size = new System.Drawing.Size(56, 20);
            this.txtGain.TabIndex = 3;
            this.txtGain.Text = "0.0";
            this.txtGain.Enter += new System.EventHandler(this.Event_Textbox_Enter);
            this.txtGain.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Event_Textbox_KeyPress);
            this.txtGain.Leave += new System.EventHandler(this.Event_Textbox_Leave);
            this.txtGain.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Event_Textbox_MouseUp);
            // 
            // txtFreq
            // 
            this.txtFreq.Location = new System.Drawing.Point(18, 54);
            this.txtFreq.MaxLength = 5;
            this.txtFreq.Name = "txtFreq";
            this.txtFreq.Size = new System.Drawing.Size(56, 20);
            this.txtFreq.TabIndex = 2;
            this.txtFreq.Text = "100";
            this.txtFreq.Enter += new System.EventHandler(this.Event_Textbox_Enter);
            this.txtFreq.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Event_Textbox_KeyPress);
            this.txtFreq.Leave += new System.EventHandler(this.Event_Textbox_Leave);
            this.txtFreq.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Event_Textbox_MouseUp);
            // 
            // lblSlope
            // 
            this.lblSlope.AutoSize = true;
            this.lblSlope.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSlope.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblSlope.Location = new System.Drawing.Point(146, 34);
            this.lblSlope.Name = "lblSlope";
            this.lblSlope.Size = new System.Drawing.Size(39, 13);
            this.lblSlope.TabIndex = 9;
            this.lblSlope.Text = "Slope";
            this.lblSlope.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblQ
            // 
            this.lblQ.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQ.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblQ.Location = new System.Drawing.Point(173, 34);
            this.lblQ.Name = "lblQ";
            this.lblQ.Size = new System.Drawing.Size(64, 13);
            this.lblQ.TabIndex = 10;
            this.lblQ.Text = "Q";
            this.lblQ.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblGain
            // 
            this.lblGain.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGain.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblGain.Location = new System.Drawing.Point(98, 34);
            this.lblGain.Name = "lblGain";
            this.lblGain.Size = new System.Drawing.Size(58, 13);
            this.lblGain.TabIndex = 8;
            this.lblGain.Text = "Gain";
            this.lblGain.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblFreq
            // 
            this.lblFreq.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFreq.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblFreq.Location = new System.Drawing.Point(11, 34);
            this.lblFreq.Name = "lblFreq";
            this.lblFreq.Size = new System.Drawing.Size(71, 13);
            this.lblFreq.TabIndex = 7;
            this.lblFreq.Text = "Center Freq";
            this.lblFreq.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Gainsboro;
            this.label1.Location = new System.Drawing.Point(1, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Type:";
            // 
            // dropFilter
            // 
            this.dropFilter.BackColor = System.Drawing.SystemColors.Control;
            this.dropFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.dropFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dropFilter.ForeColor = System.Drawing.Color.Black;
            this.dropFilter.FormattingEnabled = true;
            this.dropFilter.Items.AddRange(new object[] {
            "Not Used",
            "Low Pass",
            "High Pass",
            "Low Shelf",
            "High Shelf",
            "Peak (PEQ)",
            "Notch"});
            this.dropFilter.Location = new System.Drawing.Point(41, 4);
            this.dropFilter.Name = "dropFilter";
            this.dropFilter.Size = new System.Drawing.Size(121, 21);
            this.dropFilter.TabIndex = 0;
            this.dropFilter.SelectedIndexChanged += new System.EventHandler(this.dropFilter_SelectedIndexChanged);
            this.dropFilter.Enter += new System.EventHandler(this.dropFilter_Enter);
            // 
            // dropSlope
            // 
            this.dropSlope.FormattingEnabled = true;
            this.dropSlope.Items.AddRange(new object[] {
            "6dB/Octave",
            "12dB/Octave"});
            this.dropSlope.Location = new System.Drawing.Point(120, 54);
            this.dropSlope.Name = "dropSlope";
            this.dropSlope.Size = new System.Drawing.Size(90, 21);
            this.dropSlope.TabIndex = 4;
            this.dropSlope.SelectedIndexChanged += new System.EventHandler(this.dropSlope_SelectedIndexChanged);
            // 
            // chkBypass
            // 
            this.chkBypass.CheckedImage = global::SA_GFXLib.SA_GFXLib_Resources.ui_btn_bypass_red;
            this.chkBypass.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkBypass.Location = new System.Drawing.Point(181, 3);
            this.chkBypass.Name = "chkBypass";
            this.chkBypass.Size = new System.Drawing.Size(61, 23);
            this.chkBypass.TabIndex = 1;
            this.chkBypass.TabStop = false;
            this.chkBypass.UncheckedImage = global::SA_GFXLib.SA_GFXLib_Resources.ui_btn_bypass_grey;
            this.chkBypass.UseVisualStyleBackColor = true;
            this.chkBypass.CheckedChanged += new System.EventHandler(this.chkBypass_CheckedChanged);
            // 
            // SAFilterDesignBlock
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(82)))), ((int)(((byte)(82)))));
            this.Controls.Add(this.txtQval);
            this.Controls.Add(this.txtGain);
            this.Controls.Add(this.txtFreq);
            this.Controls.Add(this.chkBypass);
            this.Controls.Add(this.lblSlope);
            this.Controls.Add(this.lblQ);
            this.Controls.Add(this.lblGain);
            this.Controls.Add(this.lblFreq);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dropFilter);
            this.Controls.Add(this.dropSlope);
            this.Name = "SAFilterDesignBlock";
            this.Size = new System.Drawing.Size(243, 80);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtQval;
        private System.Windows.Forms.TextBox txtGain;
        private System.Windows.Forms.TextBox txtFreq;
        private PictureCheckbox chkBypass;
        private System.Windows.Forms.Label lblSlope;
        private System.Windows.Forms.Label lblQ;
        private System.Windows.Forms.Label lblGain;
        private System.Windows.Forms.Label lblFreq;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox dropFilter;
        private System.Windows.Forms.ComboBox dropSlope;
    }
}
