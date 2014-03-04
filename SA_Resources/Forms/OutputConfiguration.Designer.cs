using SA_Resources.SAControls;

namespace SA_Resources
{
    partial class OutputConfiguration
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OutputConfiguration));
            this.label1 = new System.Windows.Forms.Label();
            this.txtDisplayName = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.PictureBox();
            this.btnSave = new System.Windows.Forms.PictureBox();
            this.txtRS232Vol = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pbtnReset = new System.Windows.Forms.PictureBox();
            this.signalTimer = new System.Windows.Forms.Timer(this.components);
            this.pbtnMute = new System.Windows.Forms.PictureBox();
            this.txtRS232Mute = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.panelRS232 = new System.Windows.Forms.Panel();
            this.gainMeterOut = new SignalMeter_Small();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbtnReset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbtnMute)).BeginInit();
            this.panelRS232.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gainMeterOut)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.label1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label1.Location = new System.Drawing.Point(13, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Display Name:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtDisplayName
            // 
            this.txtDisplayName.Location = new System.Drawing.Point(116, 13);
            this.txtDisplayName.MaxLength = 17;
            this.txtDisplayName.Name = "txtDisplayName";
            this.txtDisplayName.Size = new System.Drawing.Size(133, 20);
            this.txtDisplayName.TabIndex = 1;
            this.txtDisplayName.Text = "Output #1";
            this.txtDisplayName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDisplayName_KeyPress);
            // 
            // btnCancel
            // 
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.Location = new System.Drawing.Point(137, 142);
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
            this.btnSave.Location = new System.Drawing.Point(79, 142);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(49, 23);
            this.btnSave.TabIndex = 6;
            this.btnSave.TabStop = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtRS232Vol
            // 
            this.txtRS232Vol.Location = new System.Drawing.Point(105, 37);
            this.txtRS232Vol.MaxLength = 17;
            this.txtRS232Vol.Name = "txtRS232Vol";
            this.txtRS232Vol.ReadOnly = true;
            this.txtRS232Vol.Size = new System.Drawing.Size(43, 20);
            this.txtRS232Vol.TabIndex = 9;
            this.txtRS232Vol.Text = "-";
            this.txtRS232Vol.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.label2.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label2.Location = new System.Drawing.Point(4, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "RS232 Level:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pbtnReset
            // 
            this.pbtnReset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbtnReset.Image = global::SA_Resources.GlobalResources.ui_btn_reset;
            this.pbtnReset.Location = new System.Drawing.Point(186, 35);
            this.pbtnReset.Name = "pbtnReset";
            this.pbtnReset.Size = new System.Drawing.Size(49, 23);
            this.pbtnReset.TabIndex = 11;
            this.pbtnReset.TabStop = false;
            this.pbtnReset.Click += new System.EventHandler(this.pbtnReset_Click);
            // 
            // signalTimer
            // 
            this.signalTimer.Tick += new System.EventHandler(this.signalTimer_Tick);
            // 
            // pbtnMute
            // 
            this.pbtnMute.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbtnMute.Image = global::SA_Resources.GlobalResources.ui_btn_blue_mute;
            this.pbtnMute.Location = new System.Drawing.Point(183, 6);
            this.pbtnMute.Name = "pbtnMute";
            this.pbtnMute.Size = new System.Drawing.Size(55, 23);
            this.pbtnMute.TabIndex = 14;
            this.pbtnMute.TabStop = false;
            this.pbtnMute.Click += new System.EventHandler(this.pbtnMute_Click);
            // 
            // txtRS232Mute
            // 
            this.txtRS232Mute.Location = new System.Drawing.Point(105, 8);
            this.txtRS232Mute.MaxLength = 17;
            this.txtRS232Mute.Name = "txtRS232Mute";
            this.txtRS232Mute.ReadOnly = true;
            this.txtRS232Mute.Size = new System.Drawing.Size(60, 20);
            this.txtRS232Mute.TabIndex = 13;
            this.txtRS232Mute.Text = "-";
            this.txtRS232Mute.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.label3.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label3.Location = new System.Drawing.Point(4, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "RS232 Mute:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 64);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 15;
            this.button1.Text = "Refresh";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panelRS232
            // 
            this.panelRS232.Controls.Add(this.label3);
            this.panelRS232.Controls.Add(this.button1);
            this.panelRS232.Controls.Add(this.label2);
            this.panelRS232.Controls.Add(this.pbtnMute);
            this.panelRS232.Controls.Add(this.txtRS232Vol);
            this.panelRS232.Controls.Add(this.txtRS232Mute);
            this.panelRS232.Controls.Add(this.pbtnReset);
            this.panelRS232.Location = new System.Drawing.Point(10, 39);
            this.panelRS232.Name = "panelRS232";
            this.panelRS232.Size = new System.Drawing.Size(241, 100);
            this.panelRS232.TabIndex = 16;
            // 
            // gainMeterOut
            // 
            this.gainMeterOut.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("gainMeterOut.BackgroundImage")));
            this.gainMeterOut.DB = -35D;
            this.gainMeterOut.Location = new System.Drawing.Point(255, 12);
            this.gainMeterOut.Name = "gainMeterOut";
            this.gainMeterOut.Size = new System.Drawing.Size(30, 157);
            this.gainMeterOut.TabIndex = 10;
            this.gainMeterOut.TabStop = false;
            // 
            // OutputConfiguration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(82)))), ((int)(((byte)(82)))));
            this.ClientSize = new System.Drawing.Size(293, 178);
            this.Controls.Add(this.panelRS232);
            this.Controls.Add(this.gainMeterOut);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtDisplayName);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OutputConfiguration";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CH 1 - Configure Output";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OutputConfiguration_FormClosing);
            this.Load += new System.EventHandler(this.OutputConfiguration_Load);
            this.Shown += new System.EventHandler(this.OutputConfiguration_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbtnReset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbtnMute)).EndInit();
            this.panelRS232.ResumeLayout(false);
            this.panelRS232.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gainMeterOut)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtDisplayName;
        private System.Windows.Forms.PictureBox btnCancel;
        private System.Windows.Forms.PictureBox btnSave;
        public System.Windows.Forms.TextBox txtRS232Vol;
        private System.Windows.Forms.Label label2;
        private SignalMeter_Small gainMeterOut;
        private System.Windows.Forms.PictureBox pbtnReset;
        private System.Windows.Forms.Timer signalTimer;
        private System.Windows.Forms.PictureBox pbtnMute;
        public System.Windows.Forms.TextBox txtRS232Mute;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panelRS232;
    }
}