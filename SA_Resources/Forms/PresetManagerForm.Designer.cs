namespace SA_Resources.SAForms
{
    partial class PresetManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PresetManager));
            this.dropProgramSelection = new System.Windows.Forms.ComboBox();
            this.lblProductTitle = new System.Windows.Forms.Label();
            this.btnRecall = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPresetName = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnRenameSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // dropProgramSelection
            // 
            this.dropProgramSelection.FormattingEnabled = true;
            this.dropProgramSelection.Location = new System.Drawing.Point(94, 16);
            this.dropProgramSelection.Name = "dropProgramSelection";
            this.dropProgramSelection.Size = new System.Drawing.Size(165, 21);
            this.dropProgramSelection.TabIndex = 84;
            this.dropProgramSelection.Text = "com";
            this.dropProgramSelection.SelectedIndexChanged += new System.EventHandler(this.dropProgramSelection_SelectedIndexChanged);
            this.dropProgramSelection.SelectedValueChanged += new System.EventHandler(this.dropProgramSelection_SelectedValueChanged);
            // 
            // lblProductTitle
            // 
            this.lblProductTitle.AutoSize = true;
            this.lblProductTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProductTitle.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblProductTitle.Location = new System.Drawing.Point(12, 18);
            this.lblProductTitle.Name = "lblProductTitle";
            this.lblProductTitle.Size = new System.Drawing.Size(76, 15);
            this.lblProductTitle.TabIndex = 85;
            this.lblProductTitle.Text = "Select Preset:";
            // 
            // btnRecall
            // 
            this.btnRecall.Location = new System.Drawing.Point(266, 16);
            this.btnRecall.Name = "btnRecall";
            this.btnRecall.Size = new System.Drawing.Size(61, 21);
            this.btnRecall.TabIndex = 86;
            this.btnRecall.Text = "Recall";
            this.btnRecall.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label1.Location = new System.Drawing.Point(35, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 15);
            this.label1.TabIndex = 87;
            this.label1.Text = "Rename:";
            // 
            // txtPresetName
            // 
            this.txtPresetName.Location = new System.Drawing.Point(95, 56);
            this.txtPresetName.Name = "txtPresetName";
            this.txtPresetName.Size = new System.Drawing.Size(165, 20);
            this.txtPresetName.TabIndex = 88;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(91, 109);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(168, 23);
            this.button1.TabIndex = 93;
            this.button1.Text = "Copy Items Between Presets";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // btnRenameSave
            // 
            this.btnRenameSave.Location = new System.Drawing.Point(266, 55);
            this.btnRenameSave.Name = "btnRenameSave";
            this.btnRenameSave.Size = new System.Drawing.Size(61, 21);
            this.btnRenameSave.TabIndex = 94;
            this.btnRenameSave.Text = "Save";
            this.btnRenameSave.UseVisualStyleBackColor = true;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(140, 145);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(61, 21);
            this.btnClose.TabIndex = 95;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // PresetManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(82)))), ((int)(((byte)(82)))));
            this.ClientSize = new System.Drawing.Size(340, 180);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnRenameSave);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtPresetName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnRecall);
            this.Controls.Add(this.lblProductTitle);
            this.Controls.Add(this.dropProgramSelection);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PresetManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Preset Manager";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.ComboBox dropProgramSelection;
        private System.Windows.Forms.Label lblProductTitle;
        private System.Windows.Forms.Button btnRecall;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPresetName;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnRenameSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Timer timer1;

    }
}