using SA_Resources.SAControls;

namespace SA_Resources.SAForms
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
            this.saGainFader1 = new SA_Resources.SAControls.SAGainFader();
            this.gainMeter = new SA_Resources.SAControls.SignalMeter();
            this.btnCancel = new SA_Resources.SAControls.PictureButton();
            this.btnSave = new SA_Resources.SAControls.PictureButton();
            ((System.ComponentModel.ISupportInitialize)(this.gainMeter)).BeginInit();
            this.SuspendLayout();
            // 
            // saGainFader1
            // 
            this.saGainFader1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(82)))), ((int)(((byte)(82)))));
            this.saGainFader1.Gain = 0D;
            this.saGainFader1.Location = new System.Drawing.Point(25, 5);
            this.saGainFader1.Mode = 0;
            this.saGainFader1.Muted = false;
            this.saGainFader1.Name = "saGainFader1";
            this.saGainFader1.Size = new System.Drawing.Size(73, 340);
            this.saGainFader1.TabIndex = 35;
            this.saGainFader1.OnChange += new SA_Resources.SAControls.FaderEventHandler(this.saGainFader1_OnChange);
            // 
            // gainMeter
            // 
            this.gainMeter.BackgroundImage = global::SA_GFXLib.SA_GFXLib_Resources.bg_meter_large;
            this.gainMeter.DB = -35D;
            this.gainMeter.Location = new System.Drawing.Point(116, 69);
            this.gainMeter.Name = "gainMeter";
            this.gainMeter.Size = new System.Drawing.Size(43, 225);
            this.gainMeter.TabIndex = 34;
            this.gainMeter.TabStop = false;
            // 
            // btnCancel
            // 
            this.btnCancel.AutoResize = true;
            this.btnCancel.BackgroundImage = global::SA_GFXLib.SA_GFXLib_Resources.ui_btn_cancel;
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
            this.btnSave.BackgroundImage = global::SA_GFXLib.SA_GFXLib_Resources.ui_btn_save;
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
            // GainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(82)))), ((int)(((byte)(82)))));
            this.ClientSize = new System.Drawing.Size(171, 380);
            this.Controls.Add(this.saGainFader1);
            this.Controls.Add(this.gainMeter);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CH 1 - Gain";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GainForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.gainMeter)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private PictureButton btnCancel;
        private PictureButton btnSave;
        private SignalMeter gainMeter;
        private SAGainFader saGainFader1;
    }
}