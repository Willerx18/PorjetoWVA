
namespace Atlas_projeto
{
    partial class F_GraficosReforma
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
            this.cb_DefeitoEspecifico = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cb_FamiliaPeça = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cb_FamiliaDefeitoProcurado = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cb_PeçaProcurada = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lb_fim = new System.Windows.Forms.Label();
            this.lb_inicio = new System.Windows.Forms.Label();
            this.Dta_fim = new System.Windows.Forms.DateTimePicker();
            this.cb_TipoRelatorio = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lb_Data = new System.Windows.Forms.Label();
            this.gb_TipoDeBusqueda = new System.Windows.Forms.GroupBox();
            this.rb_Semana = new System.Windows.Forms.RadioButton();
            this.rb_Ano = new System.Windows.Forms.RadioButton();
            this.rb_Mes = new System.Windows.Forms.RadioButton();
            this.rb_Dia = new System.Windows.Forms.RadioButton();
            this.gb_Turno = new System.Windows.Forms.GroupBox();
            this.rb_3 = new System.Windows.Forms.RadioButton();
            this.rb_2 = new System.Windows.Forms.RadioButton();
            this.rb_1 = new System.Windows.Forms.RadioButton();
            this.Dtp_data = new System.Windows.Forms.DateTimePicker();
            this.G_Grafico_Geral = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tb_CIRAtual = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dataSet1 = new System.Data.DataSet();
            this.dataTable1 = new System.Data.DataTable();
            this.gb_TipoDeBusqueda.SuspendLayout();
            this.gb_Turno.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.G_Grafico_Geral)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).BeginInit();
            this.SuspendLayout();
            // 
            // cb_DefeitoEspecifico
            // 
            this.cb_DefeitoEspecifico.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_DefeitoEspecifico.Enabled = false;
            this.cb_DefeitoEspecifico.FormattingEnabled = true;
            this.cb_DefeitoEspecifico.Location = new System.Drawing.Point(698, 739);
            this.cb_DefeitoEspecifico.Margin = new System.Windows.Forms.Padding(4);
            this.cb_DefeitoEspecifico.Name = "cb_DefeitoEspecifico";
            this.cb_DefeitoEspecifico.Size = new System.Drawing.Size(173, 24);
            this.cb_DefeitoEspecifico.TabIndex = 262;
            this.cb_DefeitoEspecifico.SelectedIndexChanged += new System.EventHandler(this.cb_DefeitoEspecifico_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(712, 715);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(145, 20);
            this.label7.TabIndex = 261;
            this.label7.Text = "Defeito Procurado";
            // 
            // cb_FamiliaPeça
            // 
            this.cb_FamiliaPeça.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_FamiliaPeça.FormattingEnabled = true;
            this.cb_FamiliaPeça.Location = new System.Drawing.Point(471, 684);
            this.cb_FamiliaPeça.Margin = new System.Windows.Forms.Padding(4);
            this.cb_FamiliaPeça.Name = "cb_FamiliaPeça";
            this.cb_FamiliaPeça.Size = new System.Drawing.Size(173, 24);
            this.cb_FamiliaPeça.TabIndex = 260;
            this.cb_FamiliaPeça.SelectedIndexChanged += new System.EventHandler(this.cb_FamiliaPeça_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(470, 657);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(175, 20);
            this.label6.TabIndex = 259;
            this.label6.Text = "Peça Geral Procurada";
            // 
            // cb_FamiliaDefeitoProcurado
            // 
            this.cb_FamiliaDefeitoProcurado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_FamiliaDefeitoProcurado.FormattingEnabled = true;
            this.cb_FamiliaDefeitoProcurado.Location = new System.Drawing.Point(698, 687);
            this.cb_FamiliaDefeitoProcurado.Margin = new System.Windows.Forms.Padding(4);
            this.cb_FamiliaDefeitoProcurado.Name = "cb_FamiliaDefeitoProcurado";
            this.cb_FamiliaDefeitoProcurado.Size = new System.Drawing.Size(173, 24);
            this.cb_FamiliaDefeitoProcurado.TabIndex = 258;
            this.cb_FamiliaDefeitoProcurado.SelectedIndexChanged += new System.EventHandler(this.cb_FamiliaDefeitoProcurado_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(685, 663);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(196, 20);
            this.label4.TabIndex = 257;
            this.label4.Text = " Defeito Geral Procurado";
            // 
            // cb_PeçaProcurada
            // 
            this.cb_PeçaProcurada.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_PeçaProcurada.Enabled = false;
            this.cb_PeçaProcurada.FormattingEnabled = true;
            this.cb_PeçaProcurada.Location = new System.Drawing.Point(471, 735);
            this.cb_PeçaProcurada.Margin = new System.Windows.Forms.Padding(4);
            this.cb_PeçaProcurada.Name = "cb_PeçaProcurada";
            this.cb_PeçaProcurada.Size = new System.Drawing.Size(173, 24);
            this.cb_PeçaProcurada.TabIndex = 256;
            this.cb_PeçaProcurada.SelectedIndexChanged += new System.EventHandler(this.cb_PeçaProcurada_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(493, 711);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(129, 20);
            this.label2.TabIndex = 255;
            this.label2.Text = "Peça Procurada";
            // 
            // lb_fim
            // 
            this.lb_fim.AutoSize = true;
            this.lb_fim.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_fim.Location = new System.Drawing.Point(1386, 721);
            this.lb_fim.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lb_fim.Name = "lb_fim";
            this.lb_fim.Size = new System.Drawing.Size(37, 20);
            this.lb_fim.TabIndex = 254;
            this.lb_fim.Text = "Fim";
            this.lb_fim.Visible = false;
            // 
            // lb_inicio
            // 
            this.lb_inicio.AutoSize = true;
            this.lb_inicio.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_inicio.Location = new System.Drawing.Point(1375, 690);
            this.lb_inicio.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lb_inicio.Name = "lb_inicio";
            this.lb_inicio.Size = new System.Drawing.Size(48, 20);
            this.lb_inicio.TabIndex = 253;
            this.lb_inicio.Text = "Inicio";
            this.lb_inicio.Visible = false;
            // 
            // Dta_fim
            // 
            this.Dta_fim.CustomFormat = "dd/MM/yyyy ";
            this.Dta_fim.Enabled = false;
            this.Dta_fim.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.Dta_fim.Location = new System.Drawing.Point(1429, 719);
            this.Dta_fim.Margin = new System.Windows.Forms.Padding(4);
            this.Dta_fim.Name = "Dta_fim";
            this.Dta_fim.Size = new System.Drawing.Size(124, 22);
            this.Dta_fim.TabIndex = 252;
            this.Dta_fim.Visible = false;
            // 
            // cb_TipoRelatorio
            // 
            this.cb_TipoRelatorio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_TipoRelatorio.FormattingEnabled = true;
            this.cb_TipoRelatorio.Location = new System.Drawing.Point(263, 711);
            this.cb_TipoRelatorio.Margin = new System.Windows.Forms.Padding(4);
            this.cb_TipoRelatorio.Name = "cb_TipoRelatorio";
            this.cb_TipoRelatorio.Size = new System.Drawing.Size(173, 24);
            this.cb_TipoRelatorio.TabIndex = 251;
            this.cb_TipoRelatorio.SelectedIndexChanged += new System.EventHandler(this.cb_TipoRelatorio_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(281, 684);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(136, 20);
            this.label3.TabIndex = 250;
            this.label3.Text = "Tipo de Relatorio";
            // 
            // lb_Data
            // 
            this.lb_Data.AutoSize = true;
            this.lb_Data.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Data.Location = new System.Drawing.Point(1424, 663);
            this.lb_Data.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lb_Data.Name = "lb_Data";
            this.lb_Data.Size = new System.Drawing.Size(135, 20);
            this.lb_Data.TabIndex = 249;
            this.lb_Data.Text = "Escolha Semana";
            // 
            // gb_TipoDeBusqueda
            // 
            this.gb_TipoDeBusqueda.Controls.Add(this.rb_Semana);
            this.gb_TipoDeBusqueda.Controls.Add(this.rb_Ano);
            this.gb_TipoDeBusqueda.Controls.Add(this.rb_Mes);
            this.gb_TipoDeBusqueda.Controls.Add(this.rb_Dia);
            this.gb_TipoDeBusqueda.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gb_TipoDeBusqueda.Location = new System.Drawing.Point(1139, 664);
            this.gb_TipoDeBusqueda.Margin = new System.Windows.Forms.Padding(4);
            this.gb_TipoDeBusqueda.Name = "gb_TipoDeBusqueda";
            this.gb_TipoDeBusqueda.Padding = new System.Windows.Forms.Padding(4);
            this.gb_TipoDeBusqueda.Size = new System.Drawing.Size(208, 95);
            this.gb_TipoDeBusqueda.TabIndex = 248;
            this.gb_TipoDeBusqueda.TabStop = false;
            this.gb_TipoDeBusqueda.Text = "Tipo de Visualização";
            // 
            // rb_Semana
            // 
            this.rb_Semana.AutoSize = true;
            this.rb_Semana.Location = new System.Drawing.Point(21, 61);
            this.rb_Semana.Margin = new System.Windows.Forms.Padding(4);
            this.rb_Semana.Name = "rb_Semana";
            this.rb_Semana.Size = new System.Drawing.Size(91, 24);
            this.rb_Semana.TabIndex = 227;
            this.rb_Semana.Text = "Semana";
            this.rb_Semana.UseVisualStyleBackColor = true;
            this.rb_Semana.CheckedChanged += new System.EventHandler(this.rb_Semana_CheckedChanged);
            // 
            // rb_Ano
            // 
            this.rb_Ano.AutoSize = true;
            this.rb_Ano.Location = new System.Drawing.Point(130, 61);
            this.rb_Ano.Margin = new System.Windows.Forms.Padding(4);
            this.rb_Ano.Name = "rb_Ano";
            this.rb_Ano.Size = new System.Drawing.Size(59, 24);
            this.rb_Ano.TabIndex = 214;
            this.rb_Ano.Text = "Ano";
            this.rb_Ano.UseVisualStyleBackColor = true;
            this.rb_Ano.CheckedChanged += new System.EventHandler(this.rb_Ano_CheckedChanged);
            // 
            // rb_Mes
            // 
            this.rb_Mes.AutoSize = true;
            this.rb_Mes.Location = new System.Drawing.Point(130, 29);
            this.rb_Mes.Margin = new System.Windows.Forms.Padding(4);
            this.rb_Mes.Name = "rb_Mes";
            this.rb_Mes.Size = new System.Drawing.Size(62, 24);
            this.rb_Mes.TabIndex = 213;
            this.rb_Mes.Text = "Mes";
            this.rb_Mes.UseVisualStyleBackColor = true;
            this.rb_Mes.CheckedChanged += new System.EventHandler(this.rb_Mes_CheckedChanged);
            // 
            // rb_Dia
            // 
            this.rb_Dia.AutoSize = true;
            this.rb_Dia.Checked = true;
            this.rb_Dia.Location = new System.Drawing.Point(21, 29);
            this.rb_Dia.Margin = new System.Windows.Forms.Padding(4);
            this.rb_Dia.Name = "rb_Dia";
            this.rb_Dia.Size = new System.Drawing.Size(56, 24);
            this.rb_Dia.TabIndex = 212;
            this.rb_Dia.TabStop = true;
            this.rb_Dia.Text = "Dia";
            this.rb_Dia.UseVisualStyleBackColor = true;
            this.rb_Dia.CheckedChanged += new System.EventHandler(this.rb_Dia_CheckedChanged);
            // 
            // gb_Turno
            // 
            this.gb_Turno.Controls.Add(this.rb_3);
            this.gb_Turno.Controls.Add(this.rb_2);
            this.gb_Turno.Controls.Add(this.rb_1);
            this.gb_Turno.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gb_Turno.Location = new System.Drawing.Point(920, 679);
            this.gb_Turno.Margin = new System.Windows.Forms.Padding(4);
            this.gb_Turno.Name = "gb_Turno";
            this.gb_Turno.Padding = new System.Windows.Forms.Padding(4);
            this.gb_Turno.Size = new System.Drawing.Size(192, 80);
            this.gb_Turno.TabIndex = 247;
            this.gb_Turno.TabStop = false;
            this.gb_Turno.Text = "Turno";
            // 
            // rb_3
            // 
            this.rb_3.AutoSize = true;
            this.rb_3.Location = new System.Drawing.Point(123, 38);
            this.rb_3.Margin = new System.Windows.Forms.Padding(4);
            this.rb_3.Name = "rb_3";
            this.rb_3.Size = new System.Drawing.Size(39, 24);
            this.rb_3.TabIndex = 214;
            this.rb_3.Text = "3";
            this.rb_3.UseVisualStyleBackColor = true;
            this.rb_3.CheckedChanged += new System.EventHandler(this.rb_3_CheckedChanged);
            // 
            // rb_2
            // 
            this.rb_2.AutoSize = true;
            this.rb_2.Location = new System.Drawing.Point(69, 38);
            this.rb_2.Margin = new System.Windows.Forms.Padding(4);
            this.rb_2.Name = "rb_2";
            this.rb_2.Size = new System.Drawing.Size(39, 24);
            this.rb_2.TabIndex = 213;
            this.rb_2.Text = "2";
            this.rb_2.UseVisualStyleBackColor = true;
            this.rb_2.CheckedChanged += new System.EventHandler(this.rb_2_CheckedChanged);
            // 
            // rb_1
            // 
            this.rb_1.AutoSize = true;
            this.rb_1.Checked = true;
            this.rb_1.Location = new System.Drawing.Point(16, 38);
            this.rb_1.Margin = new System.Windows.Forms.Padding(4);
            this.rb_1.Name = "rb_1";
            this.rb_1.Size = new System.Drawing.Size(39, 24);
            this.rb_1.TabIndex = 212;
            this.rb_1.TabStop = true;
            this.rb_1.Text = "1";
            this.rb_1.UseVisualStyleBackColor = true;
            this.rb_1.CheckedChanged += new System.EventHandler(this.rb_1_CheckedChanged);
            // 
            // Dtp_data
            // 
            this.Dtp_data.CustomFormat = "dd/MM/yyyy ";
            this.Dtp_data.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.Dtp_data.Location = new System.Drawing.Point(1429, 690);
            this.Dtp_data.Margin = new System.Windows.Forms.Padding(4);
            this.Dtp_data.Name = "Dtp_data";
            this.Dtp_data.Size = new System.Drawing.Size(124, 22);
            this.Dtp_data.TabIndex = 246;
            this.Dtp_data.ValueChanged += new System.EventHandler(this.Dtp_data_ValueChanged);
            // 
            // G_Grafico_Geral
            // 
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
            chartArea1.AxisY.Title = "QUANTIDADE";
            chartArea1.AxisY.TitleFont = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            chartArea1.Name = "ChartArea1";
            this.G_Grafico_Geral.ChartAreas.Add(chartArea1);
            legend1.Enabled = false;
            legend1.Name = "Legend1";
            this.G_Grafico_Geral.Legends.Add(legend1);
            this.G_Grafico_Geral.Location = new System.Drawing.Point(10, 13);
            this.G_Grafico_Geral.Margin = new System.Windows.Forms.Padding(4);
            this.G_Grafico_Geral.Name = "G_Grafico_Geral";
            series1.ChartArea = "ChartArea1";
            series1.IsValueShownAsLabel = true;
            series1.LabelAngle = -90;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            dataPoint1.LabelAngle = 90;
            series1.Points.Add(dataPoint1);
            series1.SmartLabelStyle.Enabled = false;
            this.G_Grafico_Geral.Series.Add(series1);
            this.G_Grafico_Geral.Size = new System.Drawing.Size(1768, 628);
            this.G_Grafico_Geral.TabIndex = 263;
            this.G_Grafico_Geral.Text = "chart1";
            // 
            // tb_CIRAtual
            // 
            this.tb_CIRAtual.Location = new System.Drawing.Point(48, 711);
            this.tb_CIRAtual.Margin = new System.Windows.Forms.Padding(4);
            this.tb_CIRAtual.Name = "tb_CIRAtual";
            this.tb_CIRAtual.ReadOnly = true;
            this.tb_CIRAtual.Size = new System.Drawing.Size(188, 22);
            this.tb_CIRAtual.TabIndex = 265;
            this.tb_CIRAtual.TextChanged += new System.EventHandler(this.tb_CIRAtual_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(94, 681);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 25);
            this.label5.TabIndex = 264;
            this.label5.Text = "CIR Atual";
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "NewDataSet";
            this.dataSet1.Tables.AddRange(new System.Data.DataTable[] {
            this.dataTable1});
            // 
            // dataTable1
            // 
            this.dataTable1.TableName = "Table1";
            // 
            // F_GraficosReforma
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1788, 824);
            this.Controls.Add(this.tb_CIRAtual);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.G_Grafico_Geral);
            this.Controls.Add(this.cb_DefeitoEspecifico);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cb_FamiliaPeça);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cb_FamiliaDefeitoProcurado);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cb_PeçaProcurada);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lb_fim);
            this.Controls.Add(this.lb_inicio);
            this.Controls.Add(this.Dta_fim);
            this.Controls.Add(this.cb_TipoRelatorio);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lb_Data);
            this.Controls.Add(this.gb_TipoDeBusqueda);
            this.Controls.Add(this.gb_Turno);
            this.Controls.Add(this.Dtp_data);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "F_GraficosReforma";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "F_GraficosReforma";
            this.Load += new System.EventHandler(this.F_GraficosReforma_Load);
            this.gb_TipoDeBusqueda.ResumeLayout(false);
            this.gb_TipoDeBusqueda.PerformLayout();
            this.gb_Turno.ResumeLayout(false);
            this.gb_Turno.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.G_Grafico_Geral)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cb_DefeitoEspecifico;
        public System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cb_FamiliaPeça;
        public System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cb_FamiliaDefeitoProcurado;
        public System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cb_PeçaProcurada;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label lb_fim;
        public System.Windows.Forms.Label lb_inicio;
        private System.Windows.Forms.DateTimePicker Dta_fim;
        private System.Windows.Forms.ComboBox cb_TipoRelatorio;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label lb_Data;
        private System.Windows.Forms.GroupBox gb_TipoDeBusqueda;
        private System.Windows.Forms.RadioButton rb_Semana;
        private System.Windows.Forms.RadioButton rb_Ano;
        private System.Windows.Forms.RadioButton rb_Mes;
        private System.Windows.Forms.RadioButton rb_Dia;
        private System.Windows.Forms.GroupBox gb_Turno;
        private System.Windows.Forms.RadioButton rb_3;
        private System.Windows.Forms.RadioButton rb_2;
        private System.Windows.Forms.RadioButton rb_1;
        private System.Windows.Forms.DateTimePicker Dtp_data;
        private System.Windows.Forms.DataVisualization.Charting.Chart G_Grafico_Geral;
        private System.Windows.Forms.TextBox tb_CIRAtual;
        private System.Windows.Forms.Label label5;
        private System.Data.DataSet dataSet1;
        private System.Data.DataTable dataTable1;
    }
}