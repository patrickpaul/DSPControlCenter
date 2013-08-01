namespace SA_Resources
{
    partial class StartupForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartupForm));
            this.lblStatus = new System.Windows.Forms.Label();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.btnLaunch = new System.Windows.Forms.Button();
            this.btnRefreshDevices = new System.Windows.Forms.Button();
            this.lblScanStatus = new System.Windows.Forms.Label();
            this.lblDevice = new System.Windows.Forms.Label();
            this.listDevices = new System.Windows.Forms.ComboBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.DeviceListBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(175, 71);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(145, 37);
            this.lblStatus.TabIndex = 7;
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.label1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label1.Location = new System.Drawing.Point(11, 128);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 15);
            this.label1.TabIndex = 17;
            this.label1.Text = "Device Type:";
            // 
            // btnLaunch
            // 
            this.btnLaunch.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLaunch.Location = new System.Drawing.Point(217, 124);
            this.btnLaunch.Name = "btnLaunch";
            this.btnLaunch.Size = new System.Drawing.Size(63, 23);
            this.btnLaunch.TabIndex = 18;
            this.btnLaunch.Text = "Launch";
            this.btnLaunch.UseVisualStyleBackColor = true;
            this.btnLaunch.Click += new System.EventHandler(this.btnLaunch_Click);
            // 
            // btnRefreshDevices
            // 
            this.btnRefreshDevices.Location = new System.Drawing.Point(59, 71);
            this.btnRefreshDevices.Name = "btnRefreshDevices";
            this.btnRefreshDevices.Size = new System.Drawing.Size(66, 23);
            this.btnRefreshDevices.TabIndex = 19;
            this.btnRefreshDevices.Text = "Refresh";
            this.btnRefreshDevices.UseVisualStyleBackColor = true;
            this.btnRefreshDevices.Click += new System.EventHandler(this.btnScanDevices_Click);
            // 
            // lblScanStatus
            // 
            this.lblScanStatus.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblScanStatus.Location = new System.Drawing.Point(97, 14);
            this.lblScanStatus.Name = "lblScanStatus";
            this.lblScanStatus.Size = new System.Drawing.Size(159, 23);
            this.lblScanStatus.TabIndex = 20;
            this.lblScanStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDevice
            // 
            this.lblDevice.AutoSize = true;
            this.lblDevice.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDevice.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblDevice.Location = new System.Drawing.Point(5, 47);
            this.lblDevice.Name = "lblDevice";
            this.lblDevice.Size = new System.Drawing.Size(48, 13);
            this.lblDevice.TabIndex = 23;
            this.lblDevice.Text = "Devices:";
            // 
            // listDevices
            // 
            this.listDevices.Enabled = false;
            this.listDevices.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listDevices.FormattingEnabled = true;
            this.listDevices.Location = new System.Drawing.Point(59, 44);
            this.listDevices.Name = "listDevices";
            this.listDevices.Size = new System.Drawing.Size(183, 21);
            this.listDevices.TabIndex = 22;
            // 
            // btnConnect
            // 
            this.btnConnect.Enabled = false;
            this.btnConnect.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConnect.ForeColor = System.Drawing.Color.Black;
            this.btnConnect.Location = new System.Drawing.Point(248, 42);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(72, 23);
            this.btnConnect.TabIndex = 21;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // DeviceListBox
            // 
            this.DeviceListBox.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeviceListBox.FormattingEnabled = true;
            this.DeviceListBox.Location = new System.Drawing.Point(90, 124);
            this.DeviceListBox.Name = "DeviceListBox";
            this.DeviceListBox.Size = new System.Drawing.Size(121, 23);
            this.DeviceListBox.TabIndex = 24;
            // 
            // StartupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(82)))), ((int)(((byte)(82)))));
            this.ClientSize = new System.Drawing.Size(326, 164);
            this.Controls.Add(this.btnRefreshDevices);
            this.Controls.Add(this.DeviceListBox);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblDevice);
            this.Controls.Add(this.listDevices);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.lblScanStatus);
            this.Controls.Add(this.btnLaunch);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StartupForm";
            this.Text = "DSP Control Center";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.StartupForm_Load);
            this.Shown += new System.EventHandler(this.StartupForm_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblStatus;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnLaunch;
        private System.Windows.Forms.Button btnRefreshDevices;
        private System.Windows.Forms.Label lblScanStatus;
        private System.Windows.Forms.Label lblDevice;
        private System.Windows.Forms.ComboBox listDevices;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.ComboBox DeviceListBox;

    }
}

