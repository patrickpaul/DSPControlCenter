using SA_Resources.SAControls;

namespace SA_Resources.SAForms
{
    partial class FilterDesignerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FilterDesignerForm));
            this.filterDesignBlock5 = new SA_Resources.SAControls.SAFilterDesignBlock();
            this.filterDesignBlock4 = new SA_Resources.SAControls.SAFilterDesignBlock();
            this.filterDesignBlock3 = new SA_Resources.SAControls.SAFilterDesignBlock();
            this.filterDesignBlock2 = new SA_Resources.SAControls.SAFilterDesignBlock();
            this.filterDesignBlock1 = new SA_Resources.SAControls.SAFilterDesignBlock();
            this.filterDesignBlock0 = new SA_Resources.SAControls.SAFilterDesignBlock();
            this.filterDesigner = new SA_Resources.SAControls.FilterDesigner();
            this.btnSave = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).BeginInit();
            this.SuspendLayout();
            // 
            // filterDesignBlock5
            // 
            this.filterDesignBlock5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(82)))), ((int)(((byte)(82)))));
            this.filterDesignBlock5.CenterFrequency = 0D;
            this.filterDesignBlock5.Gain = 0D;
            this.filterDesignBlock5.Location = new System.Drawing.Point(610, 415);
            this.filterDesignBlock5.Name = "filterDesignBlock5";
            this.filterDesignBlock5.QValue = 0D;
            this.filterDesignBlock5.Size = new System.Drawing.Size(243, 80);
            this.filterDesignBlock5.TabIndex = 6;
            this.filterDesignBlock5.Visible = false;
            this.filterDesignBlock5.OnChange += new SA_Resources.SAControls.FilterDesignerEventHandler(this.filterDesignBlock_OnChange);
            // 
            // filterDesignBlock4
            // 
            this.filterDesignBlock4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(82)))), ((int)(((byte)(82)))));
            this.filterDesignBlock4.CenterFrequency = 0D;
            this.filterDesignBlock4.Gain = 0D;
            this.filterDesignBlock4.Location = new System.Drawing.Point(315, 415);
            this.filterDesignBlock4.Name = "filterDesignBlock4";
            this.filterDesignBlock4.QValue = 0D;
            this.filterDesignBlock4.Size = new System.Drawing.Size(243, 80);
            this.filterDesignBlock4.TabIndex = 5;
            this.filterDesignBlock4.Visible = false;
            this.filterDesignBlock4.OnChange += new SA_Resources.SAControls.FilterDesignerEventHandler(this.filterDesignBlock_OnChange);
            // 
            // filterDesignBlock3
            // 
            this.filterDesignBlock3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(82)))), ((int)(((byte)(82)))));
            this.filterDesignBlock3.CenterFrequency = 0D;
            this.filterDesignBlock3.Gain = 0D;
            this.filterDesignBlock3.Location = new System.Drawing.Point(20, 415);
            this.filterDesignBlock3.Name = "filterDesignBlock3";
            this.filterDesignBlock3.QValue = 0D;
            this.filterDesignBlock3.Size = new System.Drawing.Size(243, 80);
            this.filterDesignBlock3.TabIndex = 4;
            this.filterDesignBlock3.Visible = false;
            this.filterDesignBlock3.OnChange += new SA_Resources.SAControls.FilterDesignerEventHandler(this.filterDesignBlock_OnChange);
            // 
            // filterDesignBlock2
            // 
            this.filterDesignBlock2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(82)))), ((int)(((byte)(82)))));
            this.filterDesignBlock2.CenterFrequency = 0D;
            this.filterDesignBlock2.Gain = 0D;
            this.filterDesignBlock2.Location = new System.Drawing.Point(610, 307);
            this.filterDesignBlock2.Name = "filterDesignBlock2";
            this.filterDesignBlock2.QValue = 0D;
            this.filterDesignBlock2.Size = new System.Drawing.Size(243, 80);
            this.filterDesignBlock2.TabIndex = 3;
            this.filterDesignBlock2.Visible = false;
            this.filterDesignBlock2.OnChange += new SA_Resources.SAControls.FilterDesignerEventHandler(this.filterDesignBlock_OnChange);
            // 
            // filterDesignBlock1
            // 
            this.filterDesignBlock1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(82)))), ((int)(((byte)(82)))));
            this.filterDesignBlock1.CenterFrequency = 0D;
            this.filterDesignBlock1.Gain = 0D;
            this.filterDesignBlock1.Location = new System.Drawing.Point(315, 307);
            this.filterDesignBlock1.Name = "filterDesignBlock1";
            this.filterDesignBlock1.QValue = 0D;
            this.filterDesignBlock1.Size = new System.Drawing.Size(243, 80);
            this.filterDesignBlock1.TabIndex = 2;
            this.filterDesignBlock1.Visible = false;
            this.filterDesignBlock1.OnChange += new SA_Resources.SAControls.FilterDesignerEventHandler(this.filterDesignBlock_OnChange);
            // 
            // filterDesignBlock0
            // 
            this.filterDesignBlock0.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(82)))), ((int)(((byte)(82)))));
            this.filterDesignBlock0.CenterFrequency = 0D;
            this.filterDesignBlock0.Gain = 0D;
            this.filterDesignBlock0.Location = new System.Drawing.Point(20, 307);
            this.filterDesignBlock0.Name = "filterDesignBlock0";
            this.filterDesignBlock0.QValue = 0D;
            this.filterDesignBlock0.Size = new System.Drawing.Size(243, 80);
            this.filterDesignBlock0.TabIndex = 1;
            this.filterDesignBlock0.Visible = false;
            this.filterDesignBlock0.OnChange += new SA_Resources.SAControls.FilterDesignerEventHandler(this.filterDesignBlock_OnChange);
            this.filterDesignBlock0.Click += new System.EventHandler(this.filterDesignBlock0_Click);
            // 
            // filterDesigner
            // 
            this.filterDesigner.Location = new System.Drawing.Point(0, 0);
            this.filterDesigner.Name = "filterDesigner";
            this.filterDesigner.Size = new System.Drawing.Size(888, 300);
            this.filterDesigner.TabIndex = 0;
            this.filterDesigner.OnDragBegin += new System.EventHandler(this.filterDesigner_OnDragBegin);
            this.filterDesigner.OnDragEnd += new System.EventHandler(this.filterDesigner_OnDragEnd);
            // 
            // btnSave
            // 
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.Location = new System.Drawing.Point(412, 514);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(49, 23);
            this.btnSave.TabIndex = 8;
            this.btnSave.TabStop = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // FilterDesignerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(82)))), ((int)(((byte)(82)))));
            this.ClientSize = new System.Drawing.Size(872, 549);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.filterDesignBlock5);
            this.Controls.Add(this.filterDesignBlock4);
            this.Controls.Add(this.filterDesignBlock3);
            this.Controls.Add(this.filterDesignBlock2);
            this.Controls.Add(this.filterDesignBlock1);
            this.Controls.Add(this.filterDesignBlock0);
            this.Controls.Add(this.filterDesigner);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FilterDesignerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FilterDesignerForm";
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FilterDesigner filterDesigner;
        private SAFilterDesignBlock filterDesignBlock3;
        private SAFilterDesignBlock filterDesignBlock4;
        private SAFilterDesignBlock filterDesignBlock5;
        private SAFilterDesignBlock filterDesignBlock0;
        private SAFilterDesignBlock filterDesignBlock1;
        private SAFilterDesignBlock filterDesignBlock2;
        private System.Windows.Forms.PictureBox btnSave;



    }
}