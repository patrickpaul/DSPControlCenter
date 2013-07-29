namespace SA_Resources
{
    partial class FilterForm3
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
            System.Windows.Forms.DataVisualization.Charting.CustomLabel customLabel1 = new System.Windows.Forms.DataVisualization.Charting.CustomLabel();
            System.Windows.Forms.DataVisualization.Charting.CustomLabel customLabel2 = new System.Windows.Forms.DataVisualization.Charting.CustomLabel();
            System.Windows.Forms.DataVisualization.Charting.CustomLabel customLabel3 = new System.Windows.Forms.DataVisualization.Charting.CustomLabel();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series7 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series8 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FilterForm3));
            this.filterChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dropFilter0 = new System.Windows.Forms.ComboBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblFilterSelector0 = new System.Windows.Forms.Label();
            this.lblFilterSelector1 = new System.Windows.Forms.Label();
            this.lblFilterSelector2 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.tooltipFilterSelector = new System.Windows.Forms.ToolTip(this.components);
            this.panel7 = new System.Windows.Forms.Panel();
            this.lblSlope0 = new System.Windows.Forms.Label();
            this.lblQ0 = new System.Windows.Forms.Label();
            this.lblGain0 = new System.Windows.Forms.Label();
            this.lblFreq0 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dropSlope0 = new System.Windows.Forms.ComboBox();
            this.lblSlope1 = new System.Windows.Forms.Label();
            this.lblQ1 = new System.Windows.Forms.Label();
            this.lblGain1 = new System.Windows.Forms.Label();
            this.lblFreq1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dropFilter1 = new System.Windows.Forms.ComboBox();
            this.dropSlope1 = new System.Windows.Forms.ComboBox();
            this.lblSlope2 = new System.Windows.Forms.Label();
            this.lblQ2 = new System.Windows.Forms.Label();
            this.lblGain2 = new System.Windows.Forms.Label();
            this.lblFreq2 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.dropFilter2 = new System.Windows.Forms.ComboBox();
            this.dropSlope2 = new System.Windows.Forms.ComboBox();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.txtFreq0 = new System.Windows.Forms.TextBox();
            this.txtGain0 = new System.Windows.Forms.TextBox();
            this.txtQval0 = new System.Windows.Forms.TextBox();
            this.txtFreq1 = new System.Windows.Forms.TextBox();
            this.txtFreq2 = new System.Windows.Forms.TextBox();
            this.txtQval1 = new System.Windows.Forms.TextBox();
            this.txtGain1 = new System.Windows.Forms.TextBox();
            this.txtQval2 = new System.Windows.Forms.TextBox();
            this.txtGain2 = new System.Windows.Forms.TextBox();
            this.lblAction = new System.Windows.Forms.Label();
            this.dropAction = new System.Windows.Forms.ComboBox();
            this.btnGo = new SA_Resources.PictureButton();
            this.btnCancel = new SA_Resources.PictureButton();
            this.btnSave = new SA_Resources.PictureButton();
            this.chkBypass2 = new Controls.PictureCheckbox();
            this.chkBypass1 = new Controls.PictureCheckbox();
            this.chkBypass0 = new Controls.PictureCheckbox();
            ((System.ComponentModel.ISupportInitialize)(this.filterChart)).BeginInit();
            this.SuspendLayout();
            // 
            // filterChart
            // 
            this.filterChart.BackColor = System.Drawing.Color.Black;
            this.filterChart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.filterChart.BackImageWrapMode = System.Windows.Forms.DataVisualization.Charting.ChartImageWrapMode.Unscaled;
            customLabel1.FromPosition = 1.8D;
            customLabel1.Text = "100Hz";
            customLabel1.ToPosition = 2.2D;
            customLabel2.FromPosition = 2.8D;
            customLabel2.Text = "1kHz";
            customLabel2.ToPosition = 3.2D;
            customLabel3.FromPosition = 3.8D;
            customLabel3.Text = "10kHz";
            customLabel3.ToPosition = 4.2D;
            chartArea1.AxisX.CustomLabels.Add(customLabel1);
            chartArea1.AxisX.CustomLabels.Add(customLabel2);
            chartArea1.AxisX.CustomLabels.Add(customLabel3);
            chartArea1.AxisX.IsLogarithmic = true;
            chartArea1.AxisX.IsStartedFromZero = false;
            chartArea1.AxisX.LabelStyle.ForeColor = System.Drawing.Color.Gainsboro;
            chartArea1.AxisX.LabelStyle.Interval = 0D;
            chartArea1.AxisX.LabelStyle.IntervalOffset = 0D;
            chartArea1.AxisX.MajorGrid.Interval = 0D;
            chartArea1.AxisX.MajorGrid.IntervalOffset = 0D;
            chartArea1.AxisX.MajorTickMark.LineColor = System.Drawing.Color.Gainsboro;
            chartArea1.AxisX.Maximum = 20000D;
            chartArea1.AxisX.Minimum = 10D;
            chartArea1.AxisX.MinorGrid.Enabled = true;
            chartArea1.AxisX.MinorGrid.Interval = 1D;
            chartArea1.AxisX.MinorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dash;
            chartArea1.AxisY.Interval = 10D;
            chartArea1.AxisY.IntervalOffset = 5D;
            chartArea1.AxisY.LabelStyle.ForeColor = System.Drawing.Color.Gainsboro;
            chartArea1.AxisY.MajorGrid.Interval = 10D;
            chartArea1.AxisY.MajorGrid.IntervalOffset = 5D;
            chartArea1.AxisY.MajorGrid.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Auto;
            chartArea1.AxisY.MajorGrid.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea1.AxisY.MajorTickMark.Interval = 5D;
            chartArea1.AxisY.MajorTickMark.LineColor = System.Drawing.Color.Gainsboro;
            chartArea1.AxisY.Maximum = 25D;
            chartArea1.AxisY.Minimum = -25D;
            chartArea1.AxisY.MinorGrid.Enabled = true;
            chartArea1.AxisY.MinorGrid.Interval = 5D;
            chartArea1.BackColor = System.Drawing.Color.White;
            chartArea1.Name = "ChartArea1";
            this.filterChart.ChartAreas.Add(chartArea1);
            this.filterChart.Location = new System.Drawing.Point(-29, -1);
            this.filterChart.Name = "filterChart";
            series1.BorderColor = System.Drawing.Color.LightCoral;
            series1.BorderWidth = 2;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series1.Color = System.Drawing.Color.LightCoral;
            series1.IsVisibleInLegend = false;
            series1.MarkerBorderWidth = 5;
            series1.Name = "Filter #1";
            series2.BorderColor = System.Drawing.Color.Blue;
            series2.BorderWidth = 2;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series2.Color = System.Drawing.Color.Blue;
            series2.Name = "Filter #2";
            series3.BorderColor = System.Drawing.Color.Purple;
            series3.BorderWidth = 2;
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series3.Color = System.Drawing.Color.Purple;
            series3.Name = "Filter #3";
            series4.BorderWidth = 2;
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series4.Enabled = false;
            series4.Name = "Filter #4";
            series5.BorderWidth = 2;
            series5.ChartArea = "ChartArea1";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series5.Enabled = false;
            series5.Name = "Filter #5";
            series6.BorderWidth = 2;
            series6.ChartArea = "ChartArea1";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series6.Enabled = false;
            series6.Name = "Filter #6";
            series7.BorderWidth = 4;
            series7.ChartArea = "ChartArea1";
            series7.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series7.Color = System.Drawing.Color.Gold;
            series7.Name = "Master Output";
            series8.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.NotSet;
            series8.BorderWidth = 3;
            series8.ChartArea = "ChartArea1";
            series8.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series8.Color = System.Drawing.Color.Transparent;
            series8.MarkerBorderColor = System.Drawing.Color.Red;
            series8.MarkerColor = System.Drawing.Color.Yellow;
            series8.MarkerSize = 6;
            series8.Name = "Markers";
            this.filterChart.Series.Add(series1);
            this.filterChart.Series.Add(series2);
            this.filterChart.Series.Add(series3);
            this.filterChart.Series.Add(series4);
            this.filterChart.Series.Add(series5);
            this.filterChart.Series.Add(series6);
            this.filterChart.Series.Add(series7);
            this.filterChart.Series.Add(series8);
            this.filterChart.Size = new System.Drawing.Size(888, 300);
            this.filterChart.TabIndex = 0;
            this.filterChart.MouseDown += new System.Windows.Forms.MouseEventHandler(this.chart1_MouseDown);
            this.filterChart.MouseMove += new System.Windows.Forms.MouseEventHandler(this.chart1_MouseMove);
            this.filterChart.MouseUp += new System.Windows.Forms.MouseEventHandler(this.chart1_MouseUp);
            // 
            // dropFilter0
            // 
            this.dropFilter0.BackColor = System.Drawing.SystemColors.Control;
            this.dropFilter0.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.dropFilter0.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dropFilter0.ForeColor = System.Drawing.Color.Black;
            this.dropFilter0.FormattingEnabled = true;
            this.dropFilter0.Items.AddRange(new object[] {
            "Not Used",
            "Low Pass",
            "High Pass",
            "Low Shelf",
            "High Shelf",
            "Peak (PEQ)",
            "Notch"});
            this.dropFilter0.Location = new System.Drawing.Point(52, 325);
            this.dropFilter0.Name = "dropFilter0";
            this.dropFilter0.Size = new System.Drawing.Size(121, 21);
            this.dropFilter0.TabIndex = 3;
            this.dropFilter0.SelectedIndexChanged += new System.EventHandler(this.dropFilter_SelectedIndexChanged);
            // 
            // timer1
            // 
            this.timer1.Interval = 1;
            // 
            // lblFilterSelector0
            // 
            this.lblFilterSelector0.BackColor = System.Drawing.Color.Chocolate;
            this.lblFilterSelector0.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblFilterSelector0.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilterSelector0.Location = new System.Drawing.Point(98, 274);
            this.lblFilterSelector0.Name = "lblFilterSelector0";
            this.lblFilterSelector0.Size = new System.Drawing.Size(20, 23);
            this.lblFilterSelector0.TabIndex = 67;
            this.lblFilterSelector0.Text = "1";
            this.lblFilterSelector0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.tooltipFilterSelector.SetToolTip(this.lblFilterSelector0, "PEQ Filter");
            this.lblFilterSelector0.Click += new System.EventHandler(this.lblFilterSelector_Click);
            this.lblFilterSelector0.Paint += new System.Windows.Forms.PaintEventHandler(this.lblFilterSelector_Paint);
            // 
            // lblFilterSelector1
            // 
            this.lblFilterSelector1.BackColor = System.Drawing.Color.Chartreuse;
            this.lblFilterSelector1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblFilterSelector1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilterSelector1.Location = new System.Drawing.Point(124, 274);
            this.lblFilterSelector1.Name = "lblFilterSelector1";
            this.lblFilterSelector1.Size = new System.Drawing.Size(20, 23);
            this.lblFilterSelector1.TabIndex = 68;
            this.lblFilterSelector1.Text = "2";
            this.lblFilterSelector1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.tooltipFilterSelector.SetToolTip(this.lblFilterSelector1, "Not Used");
            this.lblFilterSelector1.Click += new System.EventHandler(this.lblFilterSelector_Click);
            this.lblFilterSelector1.Paint += new System.Windows.Forms.PaintEventHandler(this.lblFilterSelector_Paint);
            // 
            // lblFilterSelector2
            // 
            this.lblFilterSelector2.BackColor = System.Drawing.Color.DarkMagenta;
            this.lblFilterSelector2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblFilterSelector2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilterSelector2.Location = new System.Drawing.Point(150, 274);
            this.lblFilterSelector2.Name = "lblFilterSelector2";
            this.lblFilterSelector2.Size = new System.Drawing.Size(20, 23);
            this.lblFilterSelector2.TabIndex = 69;
            this.lblFilterSelector2.Text = "3";
            this.lblFilterSelector2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblFilterSelector2.Click += new System.EventHandler(this.lblFilterSelector_Click);
            this.lblFilterSelector2.Paint += new System.Windows.Forms.PaintEventHandler(this.lblFilterSelector_Paint);
            // 
            // lblStatus
            // 
            this.lblStatus.BackColor = System.Drawing.Color.Black;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.White;
            this.lblStatus.Location = new System.Drawing.Point(611, 271);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(128, 23);
            this.lblStatus.TabIndex = 73;
            this.lblStatus.Text = "0Hz, 0dB";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.Black;
            this.panel7.Location = new System.Drawing.Point(-1, 299);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(860, 17);
            this.panel7.TabIndex = 74;
            // 
            // lblSlope0
            // 
            this.lblSlope0.AutoSize = true;
            this.lblSlope0.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSlope0.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblSlope0.Location = new System.Drawing.Point(157, 355);
            this.lblSlope0.Name = "lblSlope0";
            this.lblSlope0.Size = new System.Drawing.Size(39, 13);
            this.lblSlope0.TabIndex = 79;
            this.lblSlope0.Text = "Slope";
            this.lblSlope0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblQ0
            // 
            this.lblQ0.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQ0.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblQ0.Location = new System.Drawing.Point(184, 355);
            this.lblQ0.Name = "lblQ0";
            this.lblQ0.Size = new System.Drawing.Size(64, 13);
            this.lblQ0.TabIndex = 67;
            this.lblQ0.Text = "Q";
            this.lblQ0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblGain0
            // 
            this.lblGain0.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGain0.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblGain0.Location = new System.Drawing.Point(109, 355);
            this.lblGain0.Name = "lblGain0";
            this.lblGain0.Size = new System.Drawing.Size(58, 13);
            this.lblGain0.TabIndex = 66;
            this.lblGain0.Text = "Gain";
            this.lblGain0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblFreq0
            // 
            this.lblFreq0.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFreq0.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblFreq0.Location = new System.Drawing.Point(22, 355);
            this.lblFreq0.Name = "lblFreq0";
            this.lblFreq0.Size = new System.Drawing.Size(71, 13);
            this.lblFreq0.TabIndex = 65;
            this.lblFreq0.Text = "Center Freq";
            this.lblFreq0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Gainsboro;
            this.label1.Location = new System.Drawing.Point(12, 328);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 60;
            this.label1.Text = "Type:";
            // 
            // dropSlope0
            // 
            this.dropSlope0.FormattingEnabled = true;
            this.dropSlope0.Items.AddRange(new object[] {
            "6dB/Octave",
            "12dB/Octave"});
            this.dropSlope0.Location = new System.Drawing.Point(131, 375);
            this.dropSlope0.Name = "dropSlope0";
            this.dropSlope0.Size = new System.Drawing.Size(90, 21);
            this.dropSlope0.TabIndex = 78;
            this.dropSlope0.SelectedIndexChanged += new System.EventHandler(this.dropSlope_SelectedIndexChanged);
            // 
            // lblSlope1
            // 
            this.lblSlope1.AutoSize = true;
            this.lblSlope1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSlope1.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblSlope1.Location = new System.Drawing.Point(442, 358);
            this.lblSlope1.Name = "lblSlope1";
            this.lblSlope1.Size = new System.Drawing.Size(39, 13);
            this.lblSlope1.TabIndex = 81;
            this.lblSlope1.Text = "Slope";
            this.lblSlope1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblQ1
            // 
            this.lblQ1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQ1.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblQ1.Location = new System.Drawing.Point(465, 358);
            this.lblQ1.Name = "lblQ1";
            this.lblQ1.Size = new System.Drawing.Size(64, 13);
            this.lblQ1.TabIndex = 67;
            this.lblQ1.Text = "Q";
            this.lblQ1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblGain1
            // 
            this.lblGain1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGain1.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblGain1.Location = new System.Drawing.Point(390, 358);
            this.lblGain1.Name = "lblGain1";
            this.lblGain1.Size = new System.Drawing.Size(58, 13);
            this.lblGain1.TabIndex = 66;
            this.lblGain1.Text = "Gain";
            this.lblGain1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblFreq1
            // 
            this.lblFreq1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFreq1.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblFreq1.Location = new System.Drawing.Point(303, 358);
            this.lblFreq1.Name = "lblFreq1";
            this.lblFreq1.Size = new System.Drawing.Size(71, 13);
            this.lblFreq1.TabIndex = 65;
            this.lblFreq1.Text = "Center Freq";
            this.lblFreq1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.Color.Gainsboro;
            this.label5.Location = new System.Drawing.Point(293, 331);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 60;
            this.label5.Text = "Type:";
            // 
            // dropFilter1
            // 
            this.dropFilter1.BackColor = System.Drawing.SystemColors.Control;
            this.dropFilter1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.dropFilter1.ForeColor = System.Drawing.Color.Black;
            this.dropFilter1.FormattingEnabled = true;
            this.dropFilter1.Items.AddRange(new object[] {
            "Not Used",
            "Low Pass",
            "High Pass",
            "Low Shelf",
            "High Shelf",
            "Peak (PEQ)",
            "Notch"});
            this.dropFilter1.Location = new System.Drawing.Point(333, 328);
            this.dropFilter1.Name = "dropFilter1";
            this.dropFilter1.Size = new System.Drawing.Size(121, 21);
            this.dropFilter1.TabIndex = 3;
            this.dropFilter1.SelectedIndexChanged += new System.EventHandler(this.dropFilter_SelectedIndexChanged);
            // 
            // dropSlope1
            // 
            this.dropSlope1.FormattingEnabled = true;
            this.dropSlope1.Items.AddRange(new object[] {
            "6dB/Octave",
            "12dB/Octave"});
            this.dropSlope1.Location = new System.Drawing.Point(416, 375);
            this.dropSlope1.Name = "dropSlope1";
            this.dropSlope1.Size = new System.Drawing.Size(90, 21);
            this.dropSlope1.TabIndex = 80;
            this.dropSlope1.SelectedIndexChanged += new System.EventHandler(this.dropSlope_SelectedIndexChanged);
            // 
            // lblSlope2
            // 
            this.lblSlope2.AutoSize = true;
            this.lblSlope2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSlope2.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblSlope2.Location = new System.Drawing.Point(738, 358);
            this.lblSlope2.Name = "lblSlope2";
            this.lblSlope2.Size = new System.Drawing.Size(39, 13);
            this.lblSlope2.TabIndex = 81;
            this.lblSlope2.Text = "Slope";
            this.lblSlope2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblQ2
            // 
            this.lblQ2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQ2.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblQ2.Location = new System.Drawing.Point(765, 357);
            this.lblQ2.Name = "lblQ2";
            this.lblQ2.Size = new System.Drawing.Size(64, 13);
            this.lblQ2.TabIndex = 67;
            this.lblQ2.Text = "Q";
            this.lblQ2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblGain2
            // 
            this.lblGain2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGain2.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblGain2.Location = new System.Drawing.Point(690, 357);
            this.lblGain2.Name = "lblGain2";
            this.lblGain2.Size = new System.Drawing.Size(58, 13);
            this.lblGain2.TabIndex = 66;
            this.lblGain2.Text = "Gain";
            this.lblGain2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblFreq2
            // 
            this.lblFreq2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFreq2.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblFreq2.Location = new System.Drawing.Point(603, 357);
            this.lblFreq2.Name = "lblFreq2";
            this.lblFreq2.Size = new System.Drawing.Size(71, 13);
            this.lblFreq2.TabIndex = 65;
            this.lblFreq2.Text = "Center Freq";
            this.lblFreq2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold);
            this.label9.ForeColor = System.Drawing.Color.Gainsboro;
            this.label9.Location = new System.Drawing.Point(593, 330);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 13);
            this.label9.TabIndex = 60;
            this.label9.Text = "Type:";
            // 
            // dropFilter2
            // 
            this.dropFilter2.BackColor = System.Drawing.SystemColors.Control;
            this.dropFilter2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.dropFilter2.ForeColor = System.Drawing.Color.Black;
            this.dropFilter2.FormattingEnabled = true;
            this.dropFilter2.Items.AddRange(new object[] {
            "Not Used",
            "Low Pass",
            "High Pass",
            "Low Shelf",
            "High Shelf",
            "Peak (PEQ)",
            "Notch"});
            this.dropFilter2.Location = new System.Drawing.Point(633, 327);
            this.dropFilter2.Name = "dropFilter2";
            this.dropFilter2.Size = new System.Drawing.Size(121, 21);
            this.dropFilter2.TabIndex = 3;
            this.dropFilter2.SelectedIndexChanged += new System.EventHandler(this.dropFilter_SelectedIndexChanged);
            // 
            // dropSlope2
            // 
            this.dropSlope2.FormattingEnabled = true;
            this.dropSlope2.Items.AddRange(new object[] {
            "6dB/Octave",
            "12dB/Octave"});
            this.dropSlope2.Location = new System.Drawing.Point(712, 375);
            this.dropSlope2.Name = "dropSlope2";
            this.dropSlope2.Size = new System.Drawing.Size(90, 21);
            this.dropSlope2.TabIndex = 80;
            this.dropSlope2.SelectedIndexChanged += new System.EventHandler(this.dropSlope_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Black;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.label2.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label2.Location = new System.Drawing.Point(17, 277);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 17);
            this.label2.TabIndex = 80;
            this.label2.Text = "Filter Select:";
            // 
            // txtFreq0
            // 
            this.txtFreq0.Location = new System.Drawing.Point(29, 375);
            this.txtFreq0.MaxLength = 5;
            this.txtFreq0.Name = "txtFreq0";
            this.txtFreq0.Size = new System.Drawing.Size(56, 20);
            this.txtFreq0.TabIndex = 86;
            this.txtFreq0.Text = "100";
            this.txtFreq0.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Event_Textbox_KeyPress);
            this.txtFreq0.Leave += new System.EventHandler(this.Event_Textbox_Leave);
            this.txtFreq0.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Event_Textbox_MouseUp);
            // 
            // txtGain0
            // 
            this.txtGain0.Location = new System.Drawing.Point(114, 375);
            this.txtGain0.MaxLength = 6;
            this.txtGain0.Name = "txtGain0";
            this.txtGain0.Size = new System.Drawing.Size(56, 20);
            this.txtGain0.TabIndex = 87;
            this.txtGain0.Text = "0";
            this.txtGain0.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Event_Textbox_KeyPress);
            this.txtGain0.Leave += new System.EventHandler(this.Event_Textbox_Leave);
            this.txtGain0.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Event_Textbox_MouseUp);
            // 
            // txtQval0
            // 
            this.txtQval0.Location = new System.Drawing.Point(192, 375);
            this.txtQval0.MaxLength = 5;
            this.txtQval0.Name = "txtQval0";
            this.txtQval0.Size = new System.Drawing.Size(56, 20);
            this.txtQval0.TabIndex = 88;
            this.txtQval0.Text = "0.707";
            this.txtQval0.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Event_Textbox_KeyPress);
            // 
            // txtFreq1
            // 
            this.txtFreq1.Location = new System.Drawing.Point(310, 375);
            this.txtFreq1.MaxLength = 5;
            this.txtFreq1.Name = "txtFreq1";
            this.txtFreq1.Size = new System.Drawing.Size(56, 20);
            this.txtFreq1.TabIndex = 89;
            this.txtFreq1.Text = "1000";
            // 
            // txtFreq2
            // 
            this.txtFreq2.Location = new System.Drawing.Point(614, 375);
            this.txtFreq2.MaxLength = 5;
            this.txtFreq2.Name = "txtFreq2";
            this.txtFreq2.Size = new System.Drawing.Size(56, 20);
            this.txtFreq2.TabIndex = 90;
            this.txtFreq2.Text = "10000";
            // 
            // txtQval1
            // 
            this.txtQval1.Location = new System.Drawing.Point(473, 375);
            this.txtQval1.MaxLength = 5;
            this.txtQval1.Name = "txtQval1";
            this.txtQval1.Size = new System.Drawing.Size(56, 20);
            this.txtQval1.TabIndex = 92;
            this.txtQval1.Text = "0.707";
            // 
            // txtGain1
            // 
            this.txtGain1.Location = new System.Drawing.Point(395, 375);
            this.txtGain1.MaxLength = 6;
            this.txtGain1.Name = "txtGain1";
            this.txtGain1.Size = new System.Drawing.Size(56, 20);
            this.txtGain1.TabIndex = 91;
            this.txtGain1.Text = "0.0";
            // 
            // txtQval2
            // 
            this.txtQval2.Location = new System.Drawing.Point(773, 375);
            this.txtQval2.MaxLength = 5;
            this.txtQval2.Name = "txtQval2";
            this.txtQval2.Size = new System.Drawing.Size(56, 20);
            this.txtQval2.TabIndex = 94;
            this.txtQval2.Text = "0.707";
            // 
            // txtGain2
            // 
            this.txtGain2.Location = new System.Drawing.Point(693, 375);
            this.txtGain2.MaxLength = 6;
            this.txtGain2.Name = "txtGain2";
            this.txtGain2.Size = new System.Drawing.Size(56, 20);
            this.txtGain2.TabIndex = 93;
            this.txtGain2.Text = "0.0";
            // 
            // lblAction
            // 
            this.lblAction.AutoSize = true;
            this.lblAction.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAction.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblAction.Location = new System.Drawing.Point(13, 443);
            this.lblAction.Name = "lblAction";
            this.lblAction.Size = new System.Drawing.Size(44, 13);
            this.lblAction.TabIndex = 97;
            this.lblAction.Text = "Action:";
            // 
            // dropAction
            // 
            this.dropAction.FormattingEnabled = true;
            this.dropAction.Items.AddRange(new object[] {
            "Copy configuration to...",
            "Copy configuration from...",
            "Reset to Defaults"});
            this.dropAction.Location = new System.Drawing.Point(63, 440);
            this.dropAction.Name = "dropAction";
            this.dropAction.Size = new System.Drawing.Size(133, 21);
            this.dropAction.TabIndex = 98;
            // 
            // btnGo
            // 
            this.btnGo.AutoResize = true;
            this.btnGo.BackgroundImage = global::SA_Resources.GlobalResources.ui_btn_go;
            this.btnGo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGo.Location = new System.Drawing.Point(204, 440);
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
            this.btnGo.TabIndex = 99;
            this.btnGo.ToolTipText = "";
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AutoResize = true;
            this.btnCancel.BackgroundImage = global::SA_Resources.GlobalResources.ui_btn_cancel;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.Location = new System.Drawing.Point(429, 443);
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
            this.btnCancel.TabIndex = 96;
            this.btnCancel.ToolTipText = "";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.AutoResize = true;
            this.btnSave.BackgroundImage = global::SA_Resources.GlobalResources.ui_btn_save;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Location = new System.Drawing.Point(373, 443);
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
            this.btnSave.TabIndex = 95;
            this.btnSave.ToolTipText = "";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // chkBypass2
            // 
            this.chkBypass2.CheckedImage = global::SA_Resources.GlobalResources.ui_btn_bypass_red;
            this.chkBypass2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkBypass2.Location = new System.Drawing.Point(773, 327);
            this.chkBypass2.Name = "chkBypass2";
            this.chkBypass2.Size = new System.Drawing.Size(61, 23);
            this.chkBypass2.TabIndex = 83;
            this.chkBypass2.UncheckedImage = global::SA_Resources.GlobalResources.ui_btn_bypass;
            this.chkBypass2.UseVisualStyleBackColor = true;
            this.chkBypass2.CheckedChanged += new System.EventHandler(this.chkBypass_CheckedChanged);
            // 
            // chkBypass1
            // 
            this.chkBypass1.CheckedImage = global::SA_Resources.GlobalResources.ui_btn_bypass_red;
            this.chkBypass1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkBypass1.Location = new System.Drawing.Point(473, 327);
            this.chkBypass1.Name = "chkBypass1";
            this.chkBypass1.Size = new System.Drawing.Size(61, 23);
            this.chkBypass1.TabIndex = 82;
            this.chkBypass1.UncheckedImage = global::SA_Resources.GlobalResources.ui_btn_bypass;
            this.chkBypass1.UseVisualStyleBackColor = true;
            this.chkBypass1.CheckedChanged += new System.EventHandler(this.chkBypass_CheckedChanged);
            // 
            // chkBypass0
            // 
            this.chkBypass0.CheckedImage = global::SA_Resources.GlobalResources.ui_btn_bypass_red;
            this.chkBypass0.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkBypass0.Location = new System.Drawing.Point(192, 324);
            this.chkBypass0.Name = "chkBypass0";
            this.chkBypass0.Size = new System.Drawing.Size(61, 23);
            this.chkBypass0.TabIndex = 80;
            this.chkBypass0.UncheckedImage = global::SA_Resources.GlobalResources.ui_btn_bypass;
            this.chkBypass0.UseVisualStyleBackColor = true;
            this.chkBypass0.CheckedChanged += new System.EventHandler(this.chkBypass_CheckedChanged);
            // 
            // FilterForm3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(82)))), ((int)(((byte)(82)))));
            this.ClientSize = new System.Drawing.Size(853, 481);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.dropAction);
            this.Controls.Add(this.lblAction);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtQval2);
            this.Controls.Add(this.txtGain2);
            this.Controls.Add(this.txtQval1);
            this.Controls.Add(this.txtGain1);
            this.Controls.Add(this.txtFreq2);
            this.Controls.Add(this.txtFreq1);
            this.Controls.Add(this.txtQval0);
            this.Controls.Add(this.txtGain0);
            this.Controls.Add(this.txtFreq0);
            this.Controls.Add(this.chkBypass2);
            this.Controls.Add(this.chkBypass1);
            this.Controls.Add(this.lblSlope1);
            this.Controls.Add(this.lblSlope2);
            this.Controls.Add(this.chkBypass0);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblQ1);
            this.Controls.Add(this.lblGain1);
            this.Controls.Add(this.lblSlope0);
            this.Controls.Add(this.lblFreq1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dropFilter1);
            this.Controls.Add(this.lblQ2);
            this.Controls.Add(this.dropSlope1);
            this.Controls.Add(this.lblGain2);
            this.Controls.Add(this.lblFreq2);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.dropFilter2);
            this.Controls.Add(this.dropSlope2);
            this.Controls.Add(this.lblQ0);
            this.Controls.Add(this.lblGain0);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblFreq0);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblFilterSelector2);
            this.Controls.Add(this.dropFilter0);
            this.Controls.Add(this.dropSlope0);
            this.Controls.Add(this.lblFilterSelector1);
            this.Controls.Add(this.lblFilterSelector0);
            this.Controls.Add(this.filterChart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FilterForm3";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Filter Designer - CH 1";
            this.Load += new System.EventHandler(this.FilterForm3_Load);
            ((System.ComponentModel.ISupportInitialize)(this.filterChart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart filterChart;
        private System.Windows.Forms.ComboBox dropFilter0;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblFilterSelector0;
        private System.Windows.Forms.Label lblFilterSelector1;
        private System.Windows.Forms.Label lblFilterSelector2;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ToolTip tooltipFilterSelector;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblQ0;
        private System.Windows.Forms.Label lblGain0;
        private System.Windows.Forms.Label lblFreq0;
        private System.Windows.Forms.Label lblQ1;
        private System.Windows.Forms.Label lblGain1;
        private System.Windows.Forms.Label lblFreq1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox dropFilter1;
        private System.Windows.Forms.Label lblQ2;
        private System.Windows.Forms.Label lblGain2;
        private System.Windows.Forms.Label lblFreq2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox dropFilter2;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.ComboBox dropSlope0;
        private System.Windows.Forms.Label lblSlope0;
        private System.Windows.Forms.Label lblSlope1;
        private System.Windows.Forms.ComboBox dropSlope1;
        private System.Windows.Forms.Label lblSlope2;
        private System.Windows.Forms.ComboBox dropSlope2;
        private System.Windows.Forms.Label label2;
        private Controls.PictureCheckbox chkBypass0;
        private Controls.PictureCheckbox chkBypass1;
        private Controls.PictureCheckbox chkBypass2;
        private System.Windows.Forms.TextBox txtFreq0;
        private System.Windows.Forms.TextBox txtGain0;
        private System.Windows.Forms.TextBox txtQval0;
        private System.Windows.Forms.TextBox txtFreq1;
        private System.Windows.Forms.TextBox txtFreq2;
        private System.Windows.Forms.TextBox txtQval1;
        private System.Windows.Forms.TextBox txtGain1;
        private System.Windows.Forms.TextBox txtQval2;
        private System.Windows.Forms.TextBox txtGain2;
        private PictureButton btnCancel;
        private PictureButton btnSave;
        private System.Windows.Forms.Label lblAction;
        private System.Windows.Forms.ComboBox dropAction;
        private PictureButton btnGo;
    }
}

