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
            this.DeviceListBox = new System.Windows.Forms.ListBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.btnLoadDemo = new System.Windows.Forms.Button();
            this.btnScanDevices = new System.Windows.Forms.Button();
            this.lblScanStatus = new System.Windows.Forms.Label();
            this.lblDevice = new System.Windows.Forms.Label();
            this.listDevices = new System.Windows.Forms.ComboBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // DeviceListBox
            // 
            this.DeviceListBox.FormattingEnabled = true;
            this.DeviceListBox.Location = new System.Drawing.Point(49, 169);
            this.DeviceListBox.Name = "DeviceListBox";
            this.DeviceListBox.Size = new System.Drawing.Size(255, 95);
            this.DeviceListBox.TabIndex = 0;
            this.DeviceListBox.DoubleClick += new System.EventHandler(this.DeviceListBox_DoubleClick);
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(104, 103);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(145, 37);
            this.lblStatus.TabIndex = 7;
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label1.Location = new System.Drawing.Point(122, 150);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Available Devices";
            // 
            // btnLoadDemo
            // 
            this.btnLoadDemo.Location = new System.Drawing.Point(112, 269);
            this.btnLoadDemo.Name = "btnLoadDemo";
            this.btnLoadDemo.Size = new System.Drawing.Size(127, 23);
            this.btnLoadDemo.TabIndex = 18;
            this.btnLoadDemo.Text = "Emulation Mode";
            this.btnLoadDemo.UseVisualStyleBackColor = true;
            this.btnLoadDemo.Click += new System.EventHandler(this.btnLoadDemo_Click);
            // 
            // btnScanDevices
            // 
            this.btnScanDevices.Location = new System.Drawing.Point(117, 19);
            this.btnScanDevices.Name = "btnScanDevices";
            this.btnScanDevices.Size = new System.Drawing.Size(119, 23);
            this.btnScanDevices.TabIndex = 19;
            this.btnScanDevices.Text = "Scan For Devices";
            this.btnScanDevices.UseVisualStyleBackColor = true;
            this.btnScanDevices.Click += new System.EventHandler(this.btnScanDevices_Click);
            // 
            // lblScanStatus
            // 
            this.lblScanStatus.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblScanStatus.Location = new System.Drawing.Point(97, 49);
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
            this.lblDevice.Location = new System.Drawing.Point(26, 82);
            this.lblDevice.Name = "lblDevice";
            this.lblDevice.Size = new System.Drawing.Size(43, 13);
            this.lblDevice.TabIndex = 23;
            this.lblDevice.Text = "Device:";
            // 
            // listDevices
            // 
            this.listDevices.Enabled = false;
            this.listDevices.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listDevices.FormattingEnabled = true;
            this.listDevices.Location = new System.Drawing.Point(70, 79);
            this.listDevices.Name = "listDevices";
            this.listDevices.Size = new System.Drawing.Size(193, 21);
            this.listDevices.TabIndex = 22;
            // 
            // btnConnect
            // 
            this.btnConnect.Enabled = false;
            this.btnConnect.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConnect.ForeColor = System.Drawing.Color.Black;
            this.btnConnect.Location = new System.Drawing.Point(269, 77);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(72, 23);
            this.btnConnect.TabIndex = 21;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // StartupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(82)))), ((int)(((byte)(82)))));
            this.ClientSize = new System.Drawing.Size(353, 303);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblDevice);
            this.Controls.Add(this.listDevices);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.lblScanStatus);
            this.Controls.Add(this.btnScanDevices);
            this.Controls.Add(this.btnLoadDemo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DeviceListBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StartupForm";
            this.Text = "Stewart Audio Control Center";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox DeviceListBox;
        private System.Windows.Forms.Label lblStatus;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnLoadDemo;
        private System.Windows.Forms.Button btnScanDevices;
        private System.Windows.Forms.Label lblScanStatus;
        private System.Windows.Forms.Label lblDevice;
        private System.Windows.Forms.ComboBox listDevices;
        private System.Windows.Forms.Button btnConnect;

    }
}

