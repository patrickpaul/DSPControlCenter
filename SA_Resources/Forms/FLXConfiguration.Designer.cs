using SA_Resources.SAControls;

namespace SA_Resources
{
    partial class FLXConfigurationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FLXConfigurationForm));
            this.btnSave = new System.Windows.Forms.PictureBox();
            this.grpSleepTimer = new System.Windows.Forms.GroupBox();
            this.chkSleepEnable = new SA_Resources.SAControls.PictureCheckbox();
            this.nudSleepSeconds = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.grpRVC = new System.Windows.Forms.GroupBox();
            this.txtRVCMax = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtRVCMIn = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtRVCValue = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pbtnRefresh = new SA_Resources.SAControls.PictureButton();
            this.pbtnCalibrateUpper = new SA_Resources.SAControls.PictureButton();
            this.pbtnCalibrateLower = new SA_Resources.SAControls.PictureButton();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).BeginInit();
            this.grpSleepTimer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSleepSeconds)).BeginInit();
            this.grpRVC.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.Location = new System.Drawing.Point(99, 245);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(49, 23);
            this.btnSave.TabIndex = 6;
            this.btnSave.TabStop = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // grpSleepTimer
            // 
            this.grpSleepTimer.Controls.Add(this.chkSleepEnable);
            this.grpSleepTimer.Controls.Add(this.nudSleepSeconds);
            this.grpSleepTimer.Controls.Add(this.label2);
            this.grpSleepTimer.Controls.Add(this.label1);
            this.grpSleepTimer.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.grpSleepTimer.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpSleepTimer.Location = new System.Drawing.Point(12, 12);
            this.grpSleepTimer.Name = "grpSleepTimer";
            this.grpSleepTimer.Size = new System.Drawing.Size(222, 91);
            this.grpSleepTimer.TabIndex = 27;
            this.grpSleepTimer.TabStop = false;
            this.grpSleepTimer.Text = "Sleep Mode";
            // 
            // chkSleepEnable
            // 
            this.chkSleepEnable.CheckedImage = null;
            this.chkSleepEnable.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkSleepEnable.Location = new System.Drawing.Point(190, 23);
            this.chkSleepEnable.Name = "chkSleepEnable";
            this.chkSleepEnable.Size = new System.Drawing.Size(16, 16);
            this.chkSleepEnable.TabIndex = 16;
            this.chkSleepEnable.UncheckedImage = null;
            this.chkSleepEnable.UseVisualStyleBackColor = true;
            // 
            // nudSleepSeconds
            // 
            this.nudSleepSeconds.Location = new System.Drawing.Point(152, 53);
            this.nudSleepSeconds.Maximum = new decimal(new int[] {
            3600,
            0,
            0,
            0});
            this.nudSleepSeconds.Minimum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.nudSleepSeconds.Name = "nudSleepSeconds";
            this.nudSleepSeconds.Size = new System.Drawing.Size(54, 22);
            this.nudSleepSeconds.TabIndex = 15;
            this.nudSleepSeconds.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.label2.ForeColor = System.Drawing.Color.Gainsboro;
            this.label2.Location = new System.Drawing.Point(9, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Seconds Before Sleep:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Gainsboro;
            this.label1.Location = new System.Drawing.Point(9, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Sleep Mode Enabled:";
            // 
            // grpRVC
            // 
            this.grpRVC.Controls.Add(this.pbtnCalibrateLower);
            this.grpRVC.Controls.Add(this.pbtnRefresh);
            this.grpRVC.Controls.Add(this.pbtnCalibrateUpper);
            this.grpRVC.Controls.Add(this.txtRVCMax);
            this.grpRVC.Controls.Add(this.label5);
            this.grpRVC.Controls.Add(this.txtRVCMIn);
            this.grpRVC.Controls.Add(this.label3);
            this.grpRVC.Controls.Add(this.txtRVCValue);
            this.grpRVC.Controls.Add(this.label4);
            this.grpRVC.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.grpRVC.ForeColor = System.Drawing.Color.Gainsboro;
            this.grpRVC.Location = new System.Drawing.Point(12, 117);
            this.grpRVC.Name = "grpRVC";
            this.grpRVC.Size = new System.Drawing.Size(222, 117);
            this.grpRVC.TabIndex = 28;
            this.grpRVC.TabStop = false;
            this.grpRVC.Text = "Remote Volume Control";
            // 
            // txtRVCMax
            // 
            this.txtRVCMax.Location = new System.Drawing.Point(108, 80);
            this.txtRVCMax.Name = "txtRVCMax";
            this.txtRVCMax.ReadOnly = true;
            this.txtRVCMax.Size = new System.Drawing.Size(38, 22);
            this.txtRVCMax.TabIndex = 36;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Gainsboro;
            this.label5.Location = new System.Drawing.Point(9, 85);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 13);
            this.label5.TabIndex = 35;
            this.label5.Text = "Maximum Value:";
            // 
            // txtRVCMIn
            // 
            this.txtRVCMIn.Location = new System.Drawing.Point(108, 50);
            this.txtRVCMIn.Name = "txtRVCMIn";
            this.txtRVCMIn.ReadOnly = true;
            this.txtRVCMIn.Size = new System.Drawing.Size(38, 22);
            this.txtRVCMIn.TabIndex = 33;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Gainsboro;
            this.label3.Location = new System.Drawing.Point(9, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 32;
            this.label3.Text = "Minimum Value:";
            // 
            // txtRVCValue
            // 
            this.txtRVCValue.Location = new System.Drawing.Point(78, 21);
            this.txtRVCValue.Name = "txtRVCValue";
            this.txtRVCValue.Size = new System.Drawing.Size(68, 22);
            this.txtRVCValue.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Gainsboro;
            this.label4.Location = new System.Drawing.Point(9, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Current:";
            // 
            // pbtnRefresh
            // 
            this.pbtnRefresh.AutoResize = true;
            this.pbtnRefresh.BackgroundImage = global::SA_Resources.GlobalResources.ui_btn_refresh;
            this.pbtnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbtnRefresh.Location = new System.Drawing.Point(157, 20);
            this.pbtnRefresh.Name = "pbtnRefresh";
            this.pbtnRefresh.OverImage = null;
            this.pbtnRefresh.Overlay1Image = null;
            this.pbtnRefresh.Overlay1Visible = false;
            this.pbtnRefresh.Overlay2Image = null;
            this.pbtnRefresh.Overlay2Visible = false;
            this.pbtnRefresh.Overlay3Image = null;
            this.pbtnRefresh.Overlay3Visible = false;
            this.pbtnRefresh.PressedImage = null;
            this.pbtnRefresh.Size = new System.Drawing.Size(49, 23);
            this.pbtnRefresh.TabIndex = 29;
            this.pbtnRefresh.ToolTipText = "";
            this.pbtnRefresh.Click += new System.EventHandler(this.pbtnRefresh_Click);
            // 
            // pbtnCalibrateUpper
            // 
            this.pbtnCalibrateUpper.AutoResize = true;
            this.pbtnCalibrateUpper.BackgroundImage = global::SA_Resources.GlobalResources.ui_btn_calibrate;
            this.pbtnCalibrateUpper.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbtnCalibrateUpper.Location = new System.Drawing.Point(157, 80);
            this.pbtnCalibrateUpper.Name = "pbtnCalibrateUpper";
            this.pbtnCalibrateUpper.OverImage = null;
            this.pbtnCalibrateUpper.Overlay1Image = null;
            this.pbtnCalibrateUpper.Overlay1Visible = false;
            this.pbtnCalibrateUpper.Overlay2Image = null;
            this.pbtnCalibrateUpper.Overlay2Visible = false;
            this.pbtnCalibrateUpper.Overlay3Image = null;
            this.pbtnCalibrateUpper.Overlay3Visible = false;
            this.pbtnCalibrateUpper.PressedImage = null;
            this.pbtnCalibrateUpper.Size = new System.Drawing.Size(49, 23);
            this.pbtnCalibrateUpper.TabIndex = 30;
            this.pbtnCalibrateUpper.ToolTipText = "";
            this.pbtnCalibrateUpper.Click += new System.EventHandler(this.pbtnCalibrateUpper_Click);
            // 
            // pbtnCalibrateLower
            // 
            this.pbtnCalibrateLower.AutoResize = true;
            this.pbtnCalibrateLower.BackgroundImage = global::SA_Resources.GlobalResources.ui_btn_calibrate;
            this.pbtnCalibrateLower.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbtnCalibrateLower.Location = new System.Drawing.Point(157, 49);
            this.pbtnCalibrateLower.Name = "pbtnCalibrateLower";
            this.pbtnCalibrateLower.OverImage = null;
            this.pbtnCalibrateLower.Overlay1Image = null;
            this.pbtnCalibrateLower.Overlay1Visible = false;
            this.pbtnCalibrateLower.Overlay2Image = null;
            this.pbtnCalibrateLower.Overlay2Visible = false;
            this.pbtnCalibrateLower.Overlay3Image = null;
            this.pbtnCalibrateLower.Overlay3Visible = false;
            this.pbtnCalibrateLower.PressedImage = null;
            this.pbtnCalibrateLower.Size = new System.Drawing.Size(49, 23);
            this.pbtnCalibrateLower.TabIndex = 31;
            this.pbtnCalibrateLower.ToolTipText = "";
            this.pbtnCalibrateLower.Click += new System.EventHandler(this.pbtnCalibrateLower_Click);
            // 
            // FLXConfigurationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(82)))), ((int)(((byte)(82)))));
            this.ClientSize = new System.Drawing.Size(246, 277);
            this.Controls.Add(this.grpRVC);
            this.Controls.Add(this.grpSleepTimer);
            this.Controls.Add(this.btnSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FLXConfigurationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Device Configuration";
            this.Load += new System.EventHandler(this.FLXConfigurationForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).EndInit();
            this.grpSleepTimer.ResumeLayout(false);
            this.grpSleepTimer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSleepSeconds)).EndInit();
            this.grpRVC.ResumeLayout(false);
            this.grpRVC.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox btnSave;
        private System.Windows.Forms.GroupBox grpSleepTimer;
        private System.Windows.Forms.NumericUpDown nudSleepSeconds;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grpRVC;
        private System.Windows.Forms.TextBox txtRVCValue;
        private System.Windows.Forms.Label label4;
        private PictureCheckbox chkSleepEnable;
        private System.Windows.Forms.TextBox txtRVCMax;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtRVCMIn;
        private System.Windows.Forms.Label label3;
        private PictureButton pbtnRefresh;
        private PictureButton pbtnCalibrateUpper;
        private PictureButton pbtnCalibrateLower;
    }
}