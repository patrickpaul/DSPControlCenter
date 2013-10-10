namespace SA_Resources
{
    partial class DelayForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DelayForm));
            this.btnCancel = new System.Windows.Forms.PictureBox();
            this.btnSave = new System.Windows.Forms.PictureBox();
            this.TextDelayMS = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.DialDelayMS = new System.Windows.Forms.PictureBox();
            this.TextDelayFT = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.DialDelayFT = new System.Windows.Forms.PictureBox();
            this.TextDelayM = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.DialDelayM = new System.Windows.Forms.PictureBox();
            this.dropAction = new System.Windows.Forms.ComboBox();
            this.lblAction = new System.Windows.Forms.Label();
            this.btnGo = new SA_Resources.PictureButton();
            this.chkBypass = new SA_Resources.PictureCheckbox();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DialDelayMS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DialDelayFT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DialDelayM)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.Location = new System.Drawing.Point(135, 125);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(49, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.TabStop = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.Location = new System.Drawing.Point(77, 125);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(49, 23);
            this.btnSave.TabIndex = 6;
            this.btnSave.TabStop = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.btnSave.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnSave_Click);
            // 
            // TextDelayMS
            // 
            this.TextDelayMS.Cursor = System.Windows.Forms.Cursors.Hand;
            this.TextDelayMS.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextDelayMS.Location = new System.Drawing.Point(21, 27);
            this.TextDelayMS.MaxLength = 10;
            this.TextDelayMS.Name = "TextDelayMS";
            this.TextDelayMS.Size = new System.Drawing.Size(50, 22);
            this.TextDelayMS.TabIndex = 34;
            this.TextDelayMS.Text = "10.0ms";
            this.TextDelayMS.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label4.Location = new System.Drawing.Point(20, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 33;
            this.label4.Text = "Time (ms)";
            // 
            // DialDelayMS
            // 
            this.DialDelayMS.BackgroundImage = global::SA_Resources.GlobalResources.knob_red_bg;
            this.DialDelayMS.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DialDelayMS.Image = global::SA_Resources.GlobalResources.knob_red_line;
            this.DialDelayMS.InitialImage = null;
            this.DialDelayMS.Location = new System.Drawing.Point(26, 52);
            this.DialDelayMS.Name = "DialDelayMS";
            this.DialDelayMS.Size = new System.Drawing.Size(40, 40);
            this.DialDelayMS.TabIndex = 32;
            this.DialDelayMS.TabStop = false;
            // 
            // TextDelayFT
            // 
            this.TextDelayFT.Cursor = System.Windows.Forms.Cursors.Hand;
            this.TextDelayFT.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextDelayFT.Location = new System.Drawing.Point(101, 27);
            this.TextDelayFT.MaxLength = 10;
            this.TextDelayFT.Name = "TextDelayFT";
            this.TextDelayFT.Size = new System.Drawing.Size(50, 22);
            this.TextDelayFT.TabIndex = 37;
            this.TextDelayFT.Text = "11.2ft";
            this.TextDelayFT.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label2.Location = new System.Drawing.Point(92, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 36;
            this.label2.Text = "Distance (ft)";
            // 
            // DialDelayFT
            // 
            this.DialDelayFT.BackgroundImage = global::SA_Resources.GlobalResources.knob_blue_bg;
            this.DialDelayFT.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DialDelayFT.Image = global::SA_Resources.GlobalResources.knob_blue_line;
            this.DialDelayFT.InitialImage = null;
            this.DialDelayFT.Location = new System.Drawing.Point(106, 52);
            this.DialDelayFT.Name = "DialDelayFT";
            this.DialDelayFT.Size = new System.Drawing.Size(40, 40);
            this.DialDelayFT.TabIndex = 35;
            this.DialDelayFT.TabStop = false;
            // 
            // TextDelayM
            // 
            this.TextDelayM.Cursor = System.Windows.Forms.Cursors.Hand;
            this.TextDelayM.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextDelayM.Location = new System.Drawing.Point(181, 27);
            this.TextDelayM.MaxLength = 10;
            this.TextDelayM.Name = "TextDelayM";
            this.TextDelayM.Size = new System.Drawing.Size(50, 22);
            this.TextDelayM.TabIndex = 40;
            this.TextDelayM.Text = "3.4m";
            this.TextDelayM.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label3.Location = new System.Drawing.Point(172, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 39;
            this.label3.Text = "Distance (m)";
            // 
            // DialDelayM
            // 
            this.DialDelayM.BackgroundImage = global::SA_Resources.GlobalResources.knob_green_bg;
            this.DialDelayM.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DialDelayM.Image = global::SA_Resources.GlobalResources.knob_green_line;
            this.DialDelayM.InitialImage = null;
            this.DialDelayM.Location = new System.Drawing.Point(186, 52);
            this.DialDelayM.Name = "DialDelayM";
            this.DialDelayM.Size = new System.Drawing.Size(40, 40);
            this.DialDelayM.TabIndex = 38;
            this.DialDelayM.TabStop = false;
            // 
            // dropAction
            // 
            this.dropAction.FormattingEnabled = true;
            this.dropAction.Items.AddRange(new object[] {
            "Copy configuration to...",
            "Reset to Defaults"});
            this.dropAction.Location = new System.Drawing.Point(60, 208);
            this.dropAction.Name = "dropAction";
            this.dropAction.Size = new System.Drawing.Size(133, 21);
            this.dropAction.TabIndex = 101;
            this.dropAction.Visible = false;
            // 
            // lblAction
            // 
            this.lblAction.AutoSize = true;
            this.lblAction.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAction.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblAction.Location = new System.Drawing.Point(10, 211);
            this.lblAction.Name = "lblAction";
            this.lblAction.Size = new System.Drawing.Size(44, 13);
            this.lblAction.TabIndex = 100;
            this.lblAction.Text = "Action:";
            this.lblAction.Visible = false;
            // 
            // btnGo
            // 
            this.btnGo.AutoResize = true;
            this.btnGo.BackgroundImage = global::SA_Resources.GlobalResources.ui_btn_go;
            this.btnGo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGo.Location = new System.Drawing.Point(201, 208);
            this.btnGo.Name = "btnGo";
            this.btnGo.OverImage = null;
            this.btnGo.Overlay1Image = null;
            this.btnGo.Overlay1Visible = false;
            this.btnGo.Overlay2Image = null;
            this.btnGo.Overlay2Visible = false;
            this.btnGo.Overlay3Image = null;
            this.btnGo.Overlay3Visible = false;
            this.btnGo.PressedImage = null;
            this.btnGo.Size = new System.Drawing.Size(49, 23);
            this.btnGo.TabIndex = 102;
            this.btnGo.ToolTipText = "";
            this.btnGo.Visible = false;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // chkBypass
            // 
            this.chkBypass.CheckedImage = global::SA_Resources.GlobalResources.ui_btn_bypass_red;
            this.chkBypass.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkBypass.Location = new System.Drawing.Point(184, 12);
            this.chkBypass.Name = "chkBypass";
            this.chkBypass.Size = new System.Drawing.Size(61, 23);
            this.chkBypass.TabIndex = 31;
            this.chkBypass.UncheckedImage = global::SA_Resources.GlobalResources.ui_btn_bypass;
            this.chkBypass.UseVisualStyleBackColor = true;
            this.chkBypass.Visible = false;
            this.chkBypass.CheckedChanged += new System.EventHandler(this.chkBypass_CheckedChanged);
            // 
            // DelayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(82)))), ((int)(((byte)(82)))));
            this.ClientSize = new System.Drawing.Size(260, 161);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.dropAction);
            this.Controls.Add(this.lblAction);
            this.Controls.Add(this.TextDelayM);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.DialDelayM);
            this.Controls.Add(this.TextDelayFT);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.DialDelayFT);
            this.Controls.Add(this.TextDelayMS);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.DialDelayMS);
            this.Controls.Add(this.chkBypass);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "DelayForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CH 1 - Delay";
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DialDelayMS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DialDelayFT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DialDelayM)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox btnCancel;
        private System.Windows.Forms.PictureBox btnSave;
        private SA_Resources.PictureCheckbox chkBypass;
        private System.Windows.Forms.TextBox TextDelayMS;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox DialDelayMS;
        private System.Windows.Forms.TextBox TextDelayFT;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox DialDelayFT;
        private System.Windows.Forms.TextBox TextDelayM;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox DialDelayM;
        private PictureButton btnGo;
        private System.Windows.Forms.ComboBox dropAction;
        private System.Windows.Forms.Label lblAction;
    }
}