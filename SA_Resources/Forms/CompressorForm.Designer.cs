namespace SA_Resources
{
    partial class CompressorForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint1 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(-60D, -60D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint2 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(10D, 10D);
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint3 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(-60D, -60D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint4 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(-20D, -20D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint5 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(10D, -20D);
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint6 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(-60D, -60D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint7 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(-20D, -20D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint8 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(10D, -20D);
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint9 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(-60D, -60D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint10 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint11 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint12 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint13 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint14 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CompressorForm));
            this.dynChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.nudCompRatio = new System.Windows.Forms.NumericUpDown();
            this.lblRatio = new System.Windows.Forms.Label();
            this.lblRatioSuffix = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.nudCompThreshold = new System.Windows.Forms.NumericUpDown();
            this.DialCompAttack = new System.Windows.Forms.PictureBox();
            this.TextCompAttack = new System.Windows.Forms.TextBox();
            this.label36 = new System.Windows.Forms.Label();
            this.TextCompRelease = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.DialCompRelease = new System.Windows.Forms.PictureBox();
            this.dropAction = new System.Windows.Forms.ComboBox();
            this.lblAction = new System.Windows.Forms.Label();
            this.pbMeter = new System.Windows.Forms.PictureBox();
            this.signalTimer = new System.Windows.Forms.Timer(this.components);
            this.btnGo = new SA_Resources.PictureButton();
            this.chkSoftKnee = new SA_Resources.PictureCheckbox();
            this.chkBypass = new SA_Resources.PictureCheckbox();
            this.btnCancel = new SA_Resources.PictureButton();
            this.btnSave = new SA_Resources.PictureButton();
            ((System.ComponentModel.ISupportInitialize)(this.dynChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCompRatio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCompThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DialCompAttack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DialCompRelease)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMeter)).BeginInit();
            this.SuspendLayout();
            // 
            // dynChart
            // 
            this.dynChart.BackColor = System.Drawing.Color.Black;
            this.dynChart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.dynChart.BackImageWrapMode = System.Windows.Forms.DataVisualization.Charting.ChartImageWrapMode.Unscaled;
            chartArea1.AxisX.Interval = 5D;
            chartArea1.AxisX.LabelStyle.ForeColor = System.Drawing.Color.Gainsboro;
            chartArea1.AxisX.LabelStyle.Interval = 10D;
            chartArea1.AxisX.LabelStyle.IntervalOffset = 0D;
            chartArea1.AxisX.LineColor = System.Drawing.Color.SkyBlue;
            chartArea1.AxisX.MajorGrid.Interval = 5D;
            chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.SkyBlue;
            chartArea1.AxisX.MajorTickMark.Interval = 10D;
            chartArea1.AxisX.MajorTickMark.LineColor = System.Drawing.Color.Gainsboro;
            chartArea1.AxisX.Maximum = 10D;
            chartArea1.AxisX.Minimum = -60D;
            chartArea1.AxisX.MinorGrid.Interval = 1D;
            chartArea1.AxisX.MinorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea1.AxisX.Title = "Input";
            chartArea1.AxisX.TitleFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            chartArea1.AxisX.TitleForeColor = System.Drawing.Color.WhiteSmoke;
            chartArea1.AxisY.Interval = 10D;
            chartArea1.AxisY.IsStartedFromZero = false;
            chartArea1.AxisY.LabelStyle.ForeColor = System.Drawing.Color.Gainsboro;
            chartArea1.AxisY.LineColor = System.Drawing.Color.SkyBlue;
            chartArea1.AxisY.MajorGrid.Interval = 5D;
            chartArea1.AxisY.MajorGrid.IntervalOffset = 5D;
            chartArea1.AxisY.MajorGrid.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Auto;
            chartArea1.AxisY.MajorGrid.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.SkyBlue;
            chartArea1.AxisY.MajorTickMark.Interval = 5D;
            chartArea1.AxisY.MajorTickMark.LineColor = System.Drawing.Color.Gainsboro;
            chartArea1.AxisY.Maximum = 10D;
            chartArea1.AxisY.Minimum = -60D;
            chartArea1.AxisY.MinorGrid.Interval = 5D;
            chartArea1.AxisY.Title = "Output";
            chartArea1.AxisY.TitleFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            chartArea1.AxisY.TitleForeColor = System.Drawing.Color.WhiteSmoke;
            chartArea1.BackColor = System.Drawing.Color.White;
            chartArea1.BackImageWrapMode = System.Windows.Forms.DataVisualization.Charting.ChartImageWrapMode.TileFlipX;
            chartArea1.Name = "ChartArea1";
            this.dynChart.ChartAreas.Add(chartArea1);
            this.dynChart.Location = new System.Drawing.Point(-2, 0);
            this.dynChart.Name = "dynChart";
            series1.BorderColor = System.Drawing.Color.Tomato;
            series1.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            series1.BorderWidth = 2;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series1.Color = System.Drawing.Color.LightCoral;
            series1.IsVisibleInLegend = false;
            series1.MarkerBorderWidth = 5;
            series1.Name = "Fixed Line";
            series1.Points.Add(dataPoint1);
            series1.Points.Add(dataPoint2);
            series2.BorderWidth = 2;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series2.Color = System.Drawing.Color.Yellow;
            series2.MarkerSize = 0;
            series2.Name = "Markers";
            dataPoint3.MarkerSize = 0;
            dataPoint4.MarkerBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            dataPoint4.MarkerColor = System.Drawing.Color.Yellow;
            dataPoint4.MarkerSize = 12;
            dataPoint4.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            dataPoint5.MarkerBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            dataPoint5.MarkerColor = System.Drawing.Color.Yellow;
            dataPoint5.MarkerSize = 0;
            dataPoint5.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series2.Points.Add(dataPoint3);
            series2.Points.Add(dataPoint4);
            series2.Points.Add(dataPoint5);
            series3.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.DashDot;
            series3.BorderWidth = 2;
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series3.Color = System.Drawing.Color.Yellow;
            series3.Name = "Response Line";
            series3.Points.Add(dataPoint6);
            series3.Points.Add(dataPoint7);
            series3.Points.Add(dataPoint8);
            series4.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.DashDot;
            series4.BorderWidth = 2;
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series4.Color = System.Drawing.Color.Yellow;
            series4.Name = "Curved";
            series4.Points.Add(dataPoint9);
            series4.Points.Add(dataPoint10);
            series4.Points.Add(dataPoint11);
            series4.Points.Add(dataPoint12);
            series4.Points.Add(dataPoint13);
            series4.Points.Add(dataPoint14);
            this.dynChart.Series.Add(series1);
            this.dynChart.Series.Add(series2);
            this.dynChart.Series.Add(series3);
            this.dynChart.Series.Add(series4);
            this.dynChart.Size = new System.Drawing.Size(306, 306);
            this.dynChart.TabIndex = 6;
            this.dynChart.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dynChart_MouseDown);
            this.dynChart.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dynChart_MouseMove);
            this.dynChart.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dynChart_MouseUp);
            // 
            // nudCompRatio
            // 
            this.nudCompRatio.DecimalPlaces = 1;
            this.nudCompRatio.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudCompRatio.Location = new System.Drawing.Point(74, 343);
            this.nudCompRatio.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudCompRatio.Name = "nudCompRatio";
            this.nudCompRatio.Size = new System.Drawing.Size(53, 20);
            this.nudCompRatio.TabIndex = 7;
            this.nudCompRatio.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudCompRatio.ValueChanged += new System.EventHandler(this.nudCompRatio_ValueChanged);
            // 
            // lblRatio
            // 
            this.lblRatio.AutoSize = true;
            this.lblRatio.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.lblRatio.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblRatio.Location = new System.Drawing.Point(12, 347);
            this.lblRatio.Name = "lblRatio";
            this.lblRatio.Size = new System.Drawing.Size(34, 13);
            this.lblRatio.TabIndex = 8;
            this.lblRatio.Text = "Ratio";
            // 
            // lblRatioSuffix
            // 
            this.lblRatioSuffix.AutoSize = true;
            this.lblRatioSuffix.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.lblRatioSuffix.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblRatioSuffix.Location = new System.Drawing.Point(129, 345);
            this.lblRatioSuffix.Name = "lblRatioSuffix";
            this.lblRatioSuffix.Size = new System.Drawing.Size(16, 13);
            this.lblRatioSuffix.TabIndex = 9;
            this.lblRatioSuffix.Text = ":1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.label2.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label2.Location = new System.Drawing.Point(127, 321);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "dB";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.label3.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label3.Location = new System.Drawing.Point(12, 321);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Threshold";
            // 
            // nudCompThreshold
            // 
            this.nudCompThreshold.DecimalPlaces = 1;
            this.nudCompThreshold.Location = new System.Drawing.Point(74, 317);
            this.nudCompThreshold.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudCompThreshold.Minimum = new decimal(new int[] {
            60,
            0,
            0,
            -2147483648});
            this.nudCompThreshold.Name = "nudCompThreshold";
            this.nudCompThreshold.Size = new System.Drawing.Size(53, 20);
            this.nudCompThreshold.TabIndex = 10;
            this.nudCompThreshold.Value = new decimal(new int[] {
            20,
            0,
            0,
            -2147483648});
            this.nudCompThreshold.ValueChanged += new System.EventHandler(this.nudCompThreshold_ValueChanged);
            // 
            // DialCompAttack
            // 
            this.DialCompAttack.BackgroundImage = global::SA_Resources.GlobalResources.knob_blue_bg;
            this.DialCompAttack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DialCompAttack.Image = global::SA_Resources.GlobalResources.knob_blue_line;
            this.DialCompAttack.InitialImage = null;
            this.DialCompAttack.Location = new System.Drawing.Point(69, 424);
            this.DialCompAttack.Name = "DialCompAttack";
            this.DialCompAttack.Size = new System.Drawing.Size(40, 40);
            this.DialCompAttack.TabIndex = 17;
            this.DialCompAttack.TabStop = false;
            // 
            // TextCompAttack
            // 
            this.TextCompAttack.Cursor = System.Windows.Forms.Cursors.Hand;
            this.TextCompAttack.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextCompAttack.Location = new System.Drawing.Point(64, 399);
            this.TextCompAttack.MaxLength = 10;
            this.TextCompAttack.Name = "TextCompAttack";
            this.TextCompAttack.Size = new System.Drawing.Size(50, 22);
            this.TextCompAttack.TabIndex = 24;
            this.TextCompAttack.Text = "10.0ms";
            this.TextCompAttack.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label36.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label36.Location = new System.Drawing.Point(70, 383);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(39, 13);
            this.label36.TabIndex = 23;
            this.label36.Text = "Attack";
            // 
            // TextCompRelease
            // 
            this.TextCompRelease.Cursor = System.Windows.Forms.Cursors.Hand;
            this.TextCompRelease.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextCompRelease.Location = new System.Drawing.Point(156, 400);
            this.TextCompRelease.MaxLength = 10;
            this.TextCompRelease.Name = "TextCompRelease";
            this.TextCompRelease.Size = new System.Drawing.Size(50, 22);
            this.TextCompRelease.TabIndex = 27;
            this.TextCompRelease.Text = "10.0ms";
            this.TextCompRelease.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label4.Location = new System.Drawing.Point(159, 383);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 26;
            this.label4.Text = "Release";
            // 
            // DialCompRelease
            // 
            this.DialCompRelease.BackgroundImage = global::SA_Resources.GlobalResources.knob_orange_bg;
            this.DialCompRelease.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DialCompRelease.Image = global::SA_Resources.GlobalResources.knob_orange_line;
            this.DialCompRelease.InitialImage = null;
            this.DialCompRelease.Location = new System.Drawing.Point(161, 425);
            this.DialCompRelease.Name = "DialCompRelease";
            this.DialCompRelease.Size = new System.Drawing.Size(40, 40);
            this.DialCompRelease.TabIndex = 25;
            this.DialCompRelease.TabStop = false;
            // 
            // dropAction
            // 
            this.dropAction.FormattingEnabled = true;
            this.dropAction.Items.AddRange(new object[] {
            "Copy configuration to...",
            "Reset to Defaults"});
            this.dropAction.Location = new System.Drawing.Point(58, 516);
            this.dropAction.Name = "dropAction";
            this.dropAction.Size = new System.Drawing.Size(133, 21);
            this.dropAction.TabIndex = 104;
            // 
            // lblAction
            // 
            this.lblAction.AutoSize = true;
            this.lblAction.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAction.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblAction.Location = new System.Drawing.Point(8, 519);
            this.lblAction.Name = "lblAction";
            this.lblAction.Size = new System.Drawing.Size(44, 13);
            this.lblAction.TabIndex = 103;
            this.lblAction.Text = "Action:";
            // 
            // pbMeter
            // 
            this.pbMeter.BackgroundImage = global::SA_Resources.GlobalResources.ui_meter_base;
            this.pbMeter.Location = new System.Drawing.Point(261, 312);
            this.pbMeter.Name = "pbMeter";
            this.pbMeter.Size = new System.Drawing.Size(43, 225);
            this.pbMeter.TabIndex = 106;
            this.pbMeter.TabStop = false;
            this.pbMeter.Paint += new System.Windows.Forms.PaintEventHandler(this.pbMeter_Paint);
            // 
            // signalTimer
            // 
            this.signalTimer.Tick += new System.EventHandler(this.signalTimer_Tick);
            // 
            // btnGo
            // 
            this.btnGo.AutoResize = true;
            this.btnGo.BackgroundImage = global::SA_Resources.GlobalResources.ui_btn_go;
            this.btnGo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGo.Location = new System.Drawing.Point(199, 516);
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
            this.btnGo.TabIndex = 105;
            this.btnGo.ToolTipText = "";
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // chkSoftKnee
            // 
            this.chkSoftKnee.AutoSize = true;
            this.chkSoftKnee.CheckedImage = null;
            this.chkSoftKnee.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkSoftKnee.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.chkSoftKnee.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.chkSoftKnee.Location = new System.Drawing.Point(166, 353);
            this.chkSoftKnee.Name = "chkSoftKnee";
            this.chkSoftKnee.Size = new System.Drawing.Size(78, 17);
            this.chkSoftKnee.TabIndex = 32;
            this.chkSoftKnee.Text = " Soft Knee";
            this.chkSoftKnee.UncheckedImage = null;
            this.chkSoftKnee.UseVisualStyleBackColor = true;
            this.chkSoftKnee.CheckedChanged += new System.EventHandler(this.chkSoftKnee_CheckedChanged);
            // 
            // chkBypass
            // 
            this.chkBypass.Checked = true;
            this.chkBypass.CheckedImage = global::SA_Resources.GlobalResources.ui_btn_bypass_red;
            this.chkBypass.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBypass.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkBypass.Location = new System.Drawing.Point(172, 315);
            this.chkBypass.Name = "chkBypass";
            this.chkBypass.Size = new System.Drawing.Size(61, 23);
            this.chkBypass.TabIndex = 30;
            this.chkBypass.UncheckedImage = global::SA_Resources.GlobalResources.ui_btn_bypass;
            this.chkBypass.UseVisualStyleBackColor = true;
            this.chkBypass.CheckedChanged += new System.EventHandler(this.chkBypass_CheckedChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.AutoResize = true;
            this.btnCancel.BackgroundImage = global::SA_Resources.GlobalResources.ui_btn_cancel;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Location = new System.Drawing.Point(136, 478);
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
            this.btnCancel.TabIndex = 29;
            this.btnCancel.ToolTipText = "";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.AutoResize = true;
            this.btnSave.BackgroundImage = global::SA_Resources.GlobalResources.ui_btn_save;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Location = new System.Drawing.Point(80, 478);
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
            this.btnSave.TabIndex = 28;
            this.btnSave.ToolTipText = "";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // CompressorForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(82)))), ((int)(((byte)(82)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(306, 546);
            this.Controls.Add(this.pbMeter);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.dropAction);
            this.Controls.Add(this.lblAction);
            this.Controls.Add(this.chkSoftKnee);
            this.Controls.Add(this.chkBypass);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.TextCompRelease);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.DialCompRelease);
            this.Controls.Add(this.TextCompAttack);
            this.Controls.Add(this.label36);
            this.Controls.Add(this.DialCompAttack);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.nudCompThreshold);
            this.Controls.Add(this.lblRatioSuffix);
            this.Controls.Add(this.lblRatio);
            this.Controls.Add(this.nudCompRatio);
            this.Controls.Add(this.dynChart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "CompressorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Compressor - CH1";
            ((System.ComponentModel.ISupportInitialize)(this.dynChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCompRatio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCompThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DialCompAttack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DialCompRelease)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMeter)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart dynChart;
        private System.Windows.Forms.NumericUpDown nudCompRatio;
        private System.Windows.Forms.Label lblRatio;
        private System.Windows.Forms.Label lblRatioSuffix;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudCompThreshold;
        private System.Windows.Forms.PictureBox DialCompAttack;
        private System.Windows.Forms.TextBox TextCompAttack;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.TextBox TextCompRelease;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox DialCompRelease;
        private PictureButton btnSave;
        private PictureButton btnCancel;
        private SA_Resources.PictureCheckbox chkBypass;
        private SA_Resources.PictureCheckbox chkSoftKnee;
        private PictureButton btnGo;
        private System.Windows.Forms.ComboBox dropAction;
        private System.Windows.Forms.Label lblAction;
        private System.Windows.Forms.PictureBox pbMeter;
        private System.Windows.Forms.Timer signalTimer;
    }
}