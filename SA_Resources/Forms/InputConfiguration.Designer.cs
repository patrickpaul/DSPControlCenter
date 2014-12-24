using SA_Resources.SAControls;

namespace SA_Resources
{
    partial class InputConfiguration
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InputConfiguration));
            this.label1 = new System.Windows.Forms.Label();
            this.txtDisplayName = new System.Windows.Forms.TextBox();
            this.lblPhantomPower = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.PictureBox();
            this.btnSave = new System.Windows.Forms.PictureBox();
            this.lblInputType = new System.Windows.Forms.Label();
            this.dropInputType = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.lblNoProperties = new System.Windows.Forms.Label();
            this.gainMeter = new SA_Resources.SAControls.SignalMeter_Small();
            this.chkPhantomPower = new SA_Resources.SAControls.PictureCheckbox();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gainMeter)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.label1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Display Name:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtDisplayName
            // 
            this.txtDisplayName.Location = new System.Drawing.Point(113, 13);
            this.txtDisplayName.MaxLength = 17;
            this.txtDisplayName.Name = "txtDisplayName";
            this.txtDisplayName.Size = new System.Drawing.Size(133, 20);
            this.txtDisplayName.TabIndex = 1;
            this.txtDisplayName.Text = "Local Input #1";
            this.txtDisplayName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDisplayName_KeyPress);
            // 
            // lblPhantomPower
            // 
            this.lblPhantomPower.AutoSize = true;
            this.lblPhantomPower.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.lblPhantomPower.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblPhantomPower.Location = new System.Drawing.Point(13, 89);
            this.lblPhantomPower.Name = "lblPhantomPower";
            this.lblPhantomPower.Size = new System.Drawing.Size(91, 13);
            this.lblPhantomPower.TabIndex = 2;
            this.lblPhantomPower.Text = "Phantom Power:";
            this.lblPhantomPower.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnCancel
            // 
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.Location = new System.Drawing.Point(137, 146);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(49, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.TabStop = false;
            this.btnCancel.Visible = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.Location = new System.Drawing.Point(113, 145);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(49, 23);
            this.btnSave.TabIndex = 6;
            this.btnSave.TabStop = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblInputType
            // 
            this.lblInputType.AutoSize = true;
            this.lblInputType.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.lblInputType.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblInputType.Location = new System.Drawing.Point(12, 55);
            this.lblInputType.Name = "lblInputType";
            this.lblInputType.Size = new System.Drawing.Size(64, 13);
            this.lblInputType.TabIndex = 11;
            this.lblInputType.Text = "Input Type:";
            this.lblInputType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dropInputType
            // 
            this.dropInputType.FormattingEnabled = true;
            this.dropInputType.Items.AddRange(new object[] {
            "Line Level +0dB",
            "Microphone +6dB",
            "Microphone +20dB"});
            this.dropInputType.Location = new System.Drawing.Point(113, 51);
            this.dropInputType.Name = "dropInputType";
            this.dropInputType.Size = new System.Drawing.Size(133, 21);
            this.dropInputType.TabIndex = 12;
            this.dropInputType.SelectedIndexChanged += new System.EventHandler(this.dropInputType_SelectedIndexChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(147, 81);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(39, 20);
            this.textBox1.TabIndex = 14;
            this.textBox1.Visible = false;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(204, 81);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(42, 20);
            this.textBox2.TabIndex = 15;
            this.textBox2.Visible = false;
            // 
            // lblNoProperties
            // 
            this.lblNoProperties.AutoSize = true;
            this.lblNoProperties.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.lblNoProperties.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblNoProperties.Location = new System.Drawing.Point(47, 55);
            this.lblNoProperties.Name = "lblNoProperties";
            this.lblNoProperties.Size = new System.Drawing.Size(167, 13);
            this.lblNoProperties.TabIndex = 16;
            this.lblNoProperties.Text = "There are no properties to edit.";
            this.lblNoProperties.Visible = false;
            // 
            // gainMeter
            // 
            this.gainMeter.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("gainMeter.BackgroundImage")));
            this.gainMeter.DB = -35D;
            this.gainMeter.Location = new System.Drawing.Point(264, 16);
            this.gainMeter.Name = "gainMeter";
            this.gainMeter.Size = new System.Drawing.Size(30, 157);
            this.gainMeter.TabIndex = 13;
            this.gainMeter.TabStop = false;
            // 
            // chkPhantomPower
            // 
            this.chkPhantomPower.CheckedImage = null;
            this.chkPhantomPower.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkPhantomPower.Location = new System.Drawing.Point(114, 89);
            this.chkPhantomPower.Name = "chkPhantomPower";
            this.chkPhantomPower.Size = new System.Drawing.Size(16, 16);
            this.chkPhantomPower.TabIndex = 3;
            this.chkPhantomPower.UncheckedImage = null;
            this.chkPhantomPower.UseVisualStyleBackColor = true;
            this.chkPhantomPower.CheckedChanged += new System.EventHandler(this.chkPhantomPower_CheckedChanged);
            // 
            // InputConfiguration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(82)))), ((int)(((byte)(82)))));
            this.ClientSize = new System.Drawing.Size(303, 180);
            this.Controls.Add(this.lblNoProperties);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.gainMeter);
            this.Controls.Add(this.dropInputType);
            this.Controls.Add(this.lblInputType);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.chkPhantomPower);
            this.Controls.Add(this.lblPhantomPower);
            this.Controls.Add(this.txtDisplayName);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "InputConfiguration";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CH 1 - Configure Input";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.InputConfiguration_FormClosing);
            this.Load += new System.EventHandler(this.InputConfiguration_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gainMeter)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtDisplayName;
        private System.Windows.Forms.Label lblPhantomPower;
        private PictureCheckbox chkPhantomPower;
        private System.Windows.Forms.PictureBox btnCancel;
        private System.Windows.Forms.PictureBox btnSave;
        private System.Windows.Forms.Label lblInputType;
        private System.Windows.Forms.ComboBox dropInputType;
        private SignalMeter_Small gainMeter;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label lblNoProperties;
    }
}