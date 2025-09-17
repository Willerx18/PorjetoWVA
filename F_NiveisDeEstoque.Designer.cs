
namespace Atlas_projeto
{
    partial class F_NiveisDeEstoque
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint1 = new System.Windows.Forms.DataVisualization.Charting.DataPoint(0D, 0D);
            this.cb_QualEstoque = new System.Windows.Forms.ComboBox();
            this.cb_Filtrar = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.G_Grafico_Geral = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.lb_Setor = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.G_Grafico_Geral)).BeginInit();
            this.SuspendLayout();
            // 
            // cb_QualEstoque
            // 
            this.cb_QualEstoque.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_QualEstoque.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_QualEstoque.FormattingEnabled = true;
            this.cb_QualEstoque.Location = new System.Drawing.Point(733, 40);
            this.cb_QualEstoque.Name = "cb_QualEstoque";
            this.cb_QualEstoque.Size = new System.Drawing.Size(121, 21);
            this.cb_QualEstoque.TabIndex = 2;
            this.cb_QualEstoque.SelectedIndexChanged += new System.EventHandler(this.cb_QualEstoque_SelectedIndexChanged);
            // 
            // cb_Filtrar
            // 
            this.cb_Filtrar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_Filtrar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Filtrar.FormattingEnabled = true;
            this.cb_Filtrar.Location = new System.Drawing.Point(297, 40);
            this.cb_Filtrar.Name = "cb_Filtrar";
            this.cb_Filtrar.Size = new System.Drawing.Size(121, 21);
            this.cb_Filtrar.TabIndex = 3;
            this.cb_Filtrar.SelectedIndexChanged += new System.EventHandler(this.cb_Filtrar_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(764, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "ESTOQUE";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(331, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "FILTRAR";
            // 
            // G_Grafico_Geral
            // 
            this.G_Grafico_Geral.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.AxisX.Crossing = -1.7976931348623157E+308D;
            chartArea1.AxisX.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Years;
            chartArea1.AxisX.IsLabelAutoFit = false;
            chartArea1.AxisX.LabelAutoFitStyle = ((System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles)((((System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.DecreaseFont | System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.StaggeredLabels) 
            | System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.LabelsAngleStep90) 
            | System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.WordWrap)));
            chartArea1.AxisX.MaximumAutoSize = 100F;
            chartArea1.AxisX.ScaleView.SmallScrollMinSizeType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea1.AxisX.ScaleView.SmallScrollSizeType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea1.AxisX.Title = "PEÇAS";
            chartArea1.AxisX.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.AxisX2.Crossing = -1.7976931348623157E+308D;
            chartArea1.AxisX2.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Days;
            chartArea1.AxisX2.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Days;
            chartArea1.AxisX2.LabelAutoFitStyle = ((System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles)(((((System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.IncreaseFont | System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.DecreaseFont) 
            | System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.StaggeredLabels) 
            | System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.LabelsAngleStep90) 
            | System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.WordWrap)));
            chartArea1.AxisX2.ScaleView.Zoomable = false;
            chartArea1.AxisX2.TextOrientation = System.Windows.Forms.DataVisualization.Charting.TextOrientation.Rotated90;
            chartArea1.AxisY.Title = "CAPACIDADE";
            chartArea1.AxisY.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            chartArea1.Name = "ChartArea1";
            this.G_Grafico_Geral.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.G_Grafico_Geral.Legends.Add(legend1);
            this.G_Grafico_Geral.Location = new System.Drawing.Point(-3, 85);
            this.G_Grafico_Geral.Name = "G_Grafico_Geral";
            this.G_Grafico_Geral.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Excel;
            series1.ChartArea = "ChartArea1";
            series1.IsValueShownAsLabel = true;
            series1.LabelAngle = -90;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            dataPoint1.LabelAngle = 90;
            series1.Points.Add(dataPoint1);
            series1.SmartLabelStyle.Enabled = false;
            this.G_Grafico_Geral.Series.Add(series1);
            this.G_Grafico_Geral.Size = new System.Drawing.Size(1149, 572);
            this.G_Grafico_Geral.TabIndex = 11;
            this.G_Grafico_Geral.Text = "chart1";
            // 
            // lb_Setor
            // 
            this.lb_Setor.AutoSize = true;
            this.lb_Setor.Location = new System.Drawing.Point(1173, 9);
            this.lb_Setor.Name = "lb_Setor";
            this.lb_Setor.Size = new System.Drawing.Size(35, 13);
            this.lb_Setor.TabIndex = 12;
            this.lb_Setor.Text = "label1";
            // 
            // F_NiveisDeEstoque
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1146, 669);
            this.Controls.Add(this.lb_Setor);
            this.Controls.Add(this.G_Grafico_Geral);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cb_Filtrar);
            this.Controls.Add(this.cb_QualEstoque);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "F_NiveisDeEstoque";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Niveis De Estoque";
            this.Load += new System.EventHandler(this.F_NiveisDeEstoque_Load);
            ((System.ComponentModel.ISupportInitialize)(this.G_Grafico_Geral)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox cb_QualEstoque;
        private System.Windows.Forms.ComboBox cb_Filtrar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataVisualization.Charting.Chart G_Grafico_Geral;
        private System.Windows.Forms.Label lb_Setor;
    }
}