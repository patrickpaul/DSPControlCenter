namespace SA_Resources
{
    partial class DeviceManagerForm
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
            this.grpConnect = new System.Windows.Forms.GroupBox();
            this.lblFirmware = new System.Windows.Forms.Label();
            this.lblDevice = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.listDevices = new System.Windows.Forms.ComboBox();
            this.btnRefreshDevices = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.button1 = new System.Windows.Forms.Button();
            this.deviceThumbnail = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.radioPush = new System.Windows.Forms.RadioButton();
            this.radioPull = new System.Windows.Forms.RadioButton();
            this.btnOK = new System.Windows.Forms.Button();
            this.grpPushPull = new System.Windows.Forms.GroupBox();
            this.chkDebug = new System.Windows.Forms.CheckBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.closeTimer = new System.Windows.Forms.Timer(this.components);
            this.chkProgram1 = new System.Windows.Forms.CheckBox();
            this.grpConnect.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deviceThumbnail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.grpPushPull.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpConnect
            // 
            this.grpConnect.Controls.Add(this.lblFirmware);
            this.grpConnect.Controls.Add(this.lblDevice);
            this.grpConnect.Controls.Add(this.btnConnect);
            this.grpConnect.Controls.Add(this.listDevices);
            this.grpConnect.Controls.Add(this.btnRefreshDevices);
            this.grpConnect.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.grpConnect.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpConnect.Location = new System.Drawing.Point(11, 12);
            this.grpConnect.Name = "grpConnect";
            this.grpConnect.Size = new System.Drawing.Size(357, 86);
            this.grpConnect.TabIndex = 26;
            this.grpConnect.TabStop = false;
            this.grpConnect.Text = "Connect to a Device";
            // 
            // lblFirmware
            // 
            this.lblFirmware.Location = new System.Drawing.Point(193, 54);
            this.lblFirmware.Name = "lblFirmware";
            this.lblFirmware.Size = new System.Drawing.Size(77, 18);
            this.lblFirmware.TabIndex = 24;
            // 
            // lblDevice
            // 
            this.lblDevice.AutoSize = true;
            this.lblDevice.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDevice.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblDevice.Location = new System.Drawing.Point(8, 24);
            this.lblDevice.Name = "lblDevice";
            this.lblDevice.Size = new System.Drawing.Size(48, 13);
            this.lblDevice.TabIndex = 23;
            this.lblDevice.Text = "Devices:";
            // 
            // btnConnect
            // 
            this.btnConnect.Enabled = false;
            this.btnConnect.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConnect.ForeColor = System.Drawing.Color.Black;
            this.btnConnect.Location = new System.Drawing.Point(276, 19);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(72, 23);
            this.btnConnect.TabIndex = 21;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // listDevices
            // 
            this.listDevices.Enabled = false;
            this.listDevices.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listDevices.FormattingEnabled = true;
            this.listDevices.Location = new System.Drawing.Point(62, 20);
            this.listDevices.Name = "listDevices";
            this.listDevices.Size = new System.Drawing.Size(208, 21);
            this.listDevices.TabIndex = 22;
            this.listDevices.SelectedIndexChanged += new System.EventHandler(this.listDevices_SelectedIndexChanged);
            // 
            // btnRefreshDevices
            // 
            this.btnRefreshDevices.ForeColor = System.Drawing.SystemColors.WindowText;
            this.btnRefreshDevices.Location = new System.Drawing.Point(61, 49);
            this.btnRefreshDevices.Name = "btnRefreshDevices";
            this.btnRefreshDevices.Size = new System.Drawing.Size(92, 23);
            this.btnRefreshDevices.TabIndex = 19;
            this.btnRefreshDevices.Text = "Refresh List";
            this.btnRefreshDevices.UseVisualStyleBackColor = true;
            this.btnRefreshDevices.Click += new System.EventHandler(this.btnRefreshDevices_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 278);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(379, 22);
            this.statusStrip1.TabIndex = 27;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            this.statusLabel.BackColor = System.Drawing.SystemColors.Control;
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(308, 304);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(175, 23);
            this.button1.TabIndex = 29;
            this.button1.Text = "Load Device Image";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // deviceThumbnail
            // 
            this.deviceThumbnail.Location = new System.Drawing.Point(308, 333);
            this.deviceThumbnail.Name = "deviceThumbnail";
            this.deviceThumbnail.Size = new System.Drawing.Size(175, 100);
            this.deviceThumbnail.TabIndex = 28;
            this.deviceThumbnail.TabStop = false;
            this.deviceThumbnail.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::SA_Resources.GlobalResources.UI_thumbnail;
            this.pictureBox1.Location = new System.Drawing.Point(12, 333);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(175, 100);
            this.pictureBox1.TabIndex = 30;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // radioPush
            // 
            this.radioPush.AutoSize = true;
            this.radioPush.Checked = true;
            this.radioPush.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.radioPush.ForeColor = System.Drawing.Color.Gainsboro;
            this.radioPush.Location = new System.Drawing.Point(18, 26);
            this.radioPush.Name = "radioPush";
            this.radioPush.Size = new System.Drawing.Size(323, 23);
            this.radioPush.TabIndex = 31;
            this.radioPush.TabStop = true;
            this.radioPush.Text = "Copy information from Control Center to Device";
            this.radioPush.UseVisualStyleBackColor = true;
            // 
            // radioPull
            // 
            this.radioPull.AutoSize = true;
            this.radioPull.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.radioPull.ForeColor = System.Drawing.Color.Gainsboro;
            this.radioPull.Location = new System.Drawing.Point(18, 50);
            this.radioPull.Name = "radioPull";
            this.radioPull.Size = new System.Drawing.Size(323, 23);
            this.radioPull.TabIndex = 32;
            this.radioPull.Text = "Copy information from Device to Control Center";
            this.radioPull.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.ForeColor = System.Drawing.Color.Black;
            this.btnOK.Location = new System.Drawing.Point(130, 89);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 33;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // grpPushPull
            // 
            this.grpPushPull.Controls.Add(this.chkProgram1);
            this.grpPushPull.Controls.Add(this.chkDebug);
            this.grpPushPull.Controls.Add(this.radioPush);
            this.grpPushPull.Controls.Add(this.btnOK);
            this.grpPushPull.Controls.Add(this.radioPull);
            this.grpPushPull.Enabled = false;
            this.grpPushPull.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.grpPushPull.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpPushPull.Location = new System.Drawing.Point(11, 119);
            this.grpPushPull.Name = "grpPushPull";
            this.grpPushPull.Size = new System.Drawing.Size(357, 125);
            this.grpPushPull.TabIndex = 34;
            this.grpPushPull.TabStop = false;
            this.grpPushPull.Text = "Synchronize";
            // 
            // chkDebug
            // 
            this.chkDebug.AutoSize = true;
            this.chkDebug.Location = new System.Drawing.Point(247, 79);
            this.chkDebug.Name = "chkDebug";
            this.chkDebug.Size = new System.Drawing.Size(74, 17);
            this.chkDebug.TabIndex = 34;
            this.chkDebug.Text = "Skip Sync";
            this.chkDebug.UseVisualStyleBackColor = true;
            // 
            // progressBar1
            // 
            this.progressBar1.Enabled = false;
            this.progressBar1.Location = new System.Drawing.Point(11, 250);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(357, 15);
            this.progressBar1.TabIndex = 34;
            // 
            // closeTimer
            // 
            this.closeTimer.Interval = 1000;
            this.closeTimer.Tick += new System.EventHandler(this.closeTimer_Tick);
            // 
            // chkProgram1
            // 
            this.chkProgram1.AutoSize = true;
            this.chkProgram1.Location = new System.Drawing.Point(247, 102);
            this.chkProgram1.Name = "chkProgram1";
            this.chkProgram1.Size = new System.Drawing.Size(105, 17);
            this.chkProgram1.TabIndex = 35;
            this.chkProgram1.Text = "Only Program 1";
            this.chkProgram1.UseVisualStyleBackColor = true;
            // 
            // DeviceManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(82)))), ((int)(((byte)(82)))));
            this.ClientSize = new System.Drawing.Size(379, 300);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.grpPushPull);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.deviceThumbnail);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.grpConnect);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DeviceManagerForm";
            this.Text = "Device Manager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DeviceManagerForm_FormClosing);
            this.Shown += new System.EventHandler(this.DeviceManagerForm_Shown);
            this.grpConnect.ResumeLayout(false);
            this.grpConnect.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.deviceThumbnail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.grpPushPull.ResumeLayout(false);
            this.grpPushPull.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpConnect;
        private System.Windows.Forms.Label lblDevice;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.ComboBox listDevices;
        private System.Windows.Forms.Button btnRefreshDevices;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.PictureBox deviceThumbnail;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.RadioButton radioPush;
        private System.Windows.Forms.RadioButton radioPull;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.GroupBox grpPushPull;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Timer closeTimer;
        private System.Windows.Forms.CheckBox chkDebug;
        private System.Windows.Forms.Label lblFirmware;
        private System.Windows.Forms.CheckBox chkProgram1;
    }
}