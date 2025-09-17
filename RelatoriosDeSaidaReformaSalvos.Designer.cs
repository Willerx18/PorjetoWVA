
namespace Atlas_projeto
{
    partial class RelatoriosDeSaidaReformaSalvos
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Dtp_data = new System.Windows.Forms.DateTimePicker();
            this.rb_1 = new System.Windows.Forms.RadioButton();
            this.rb_2 = new System.Windows.Forms.RadioButton();
            this.gb_Turno = new System.Windows.Forms.GroupBox();
            this.rb_3 = new System.Windows.Forms.RadioButton();
            this.gb_TipoDeBusqueda = new System.Windows.Forms.GroupBox();
            this.rb_Semana = new System.Windows.Forms.RadioButton();
            this.rb_Ano = new System.Windows.Forms.RadioButton();
            this.rb_Mes = new System.Windows.Forms.RadioButton();
            this.rb_Dia = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.lb_Data = new System.Windows.Forms.Label();
            this.cb_TipoRelatorio = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tb_CIRAtual = new System.Windows.Forms.TextBox();
            this.lb_inicio = new System.Windows.Forms.Label();
            this.Btn_Voltar = new System.Windows.Forms.Button();
            this.Btn_Sair = new System.Windows.Forms.Button();
            this.cb_PeçaProcurada = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cb_FamiliaDefeitoProcurado = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cb_FamiliaPeça = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cb_DefeitoEspecifico = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dgv_RelatoriosSalvos = new System.Windows.Forms.DataGridView();
            this.btn_Imprimir = new System.Windows.Forms.Button();
            this.btn_VerGrafico = new System.Windows.Forms.Button();
            this.Dta_fim = new System.Windows.Forms.DateTimePicker();
            this.lb_fim = new System.Windows.Forms.Label();
            this.gb_Turno.SuspendLayout();
            this.gb_TipoDeBusqueda.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_RelatoriosSalvos)).BeginInit();
            this.SuspendLayout();
            // 
            // Dtp_data
            // 
            this.Dtp_data.CustomFormat = "dd/MM/yyyy ";
            this.Dtp_data.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.Dtp_data.Location = new System.Drawing.Point(897, 570);
            this.Dtp_data.Name = "Dtp_data";
            this.Dtp_data.Size = new System.Drawing.Size(94, 20);
            this.Dtp_data.TabIndex = 211;
            this.Dtp_data.ValueChanged += new System.EventHandler(this.Dtp_data_ValueChanged);
            // 
            // rb_1
            // 
            this.rb_1.AutoSize = true;
            this.rb_1.Checked = true;
            this.rb_1.Location = new System.Drawing.Point(12, 31);
            this.rb_1.Name = "rb_1";
            this.rb_1.Size = new System.Drawing.Size(34, 21);
            this.rb_1.TabIndex = 212;
            this.rb_1.TabStop = true;
            this.rb_1.Text = "1";
            this.rb_1.UseVisualStyleBackColor = true;
            this.rb_1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // rb_2
            // 
            this.rb_2.AutoSize = true;
            this.rb_2.Location = new System.Drawing.Point(52, 31);
            this.rb_2.Name = "rb_2";
            this.rb_2.Size = new System.Drawing.Size(34, 21);
            this.rb_2.TabIndex = 213;
            this.rb_2.Text = "2";
            this.rb_2.UseVisualStyleBackColor = true;
            this.rb_2.CheckedChanged += new System.EventHandler(this.rb_2_CheckedChanged);
            // 
            // gb_Turno
            // 
            this.gb_Turno.Controls.Add(this.rb_3);
            this.gb_Turno.Controls.Add(this.rb_2);
            this.gb_Turno.Controls.Add(this.rb_1);
            this.gb_Turno.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gb_Turno.Location = new System.Drawing.Point(872, 316);
            this.gb_Turno.Name = "gb_Turno";
            this.gb_Turno.Size = new System.Drawing.Size(144, 65);
            this.gb_Turno.TabIndex = 214;
            this.gb_Turno.TabStop = false;
            this.gb_Turno.Text = "Turno";
            // 
            // rb_3
            // 
            this.rb_3.AutoSize = true;
            this.rb_3.Location = new System.Drawing.Point(92, 31);
            this.rb_3.Name = "rb_3";
            this.rb_3.Size = new System.Drawing.Size(34, 21);
            this.rb_3.TabIndex = 214;
            this.rb_3.Text = "3";
            this.rb_3.UseVisualStyleBackColor = true;
            this.rb_3.CheckedChanged += new System.EventHandler(this.rb_3_CheckedChanged);
            // 
            // gb_TipoDeBusqueda
            // 
            this.gb_TipoDeBusqueda.Controls.Add(this.rb_Semana);
            this.gb_TipoDeBusqueda.Controls.Add(this.rb_Ano);
            this.gb_TipoDeBusqueda.Controls.Add(this.rb_Mes);
            this.gb_TipoDeBusqueda.Controls.Add(this.rb_Dia);
            this.gb_TipoDeBusqueda.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gb_TipoDeBusqueda.Location = new System.Drawing.Point(893, 387);
            this.gb_TipoDeBusqueda.Name = "gb_TipoDeBusqueda";
            this.gb_TipoDeBusqueda.Size = new System.Drawing.Size(101, 151);
            this.gb_TipoDeBusqueda.TabIndex = 215;
            this.gb_TipoDeBusqueda.TabStop = false;
            this.gb_TipoDeBusqueda.Text = "Tipo de Busqueda";
            // 
            // rb_Semana
            // 
            this.rb_Semana.AutoSize = true;
            this.rb_Semana.Location = new System.Drawing.Point(16, 73);
            this.rb_Semana.Name = "rb_Semana";
            this.rb_Semana.Size = new System.Drawing.Size(78, 21);
            this.rb_Semana.TabIndex = 227;
            this.rb_Semana.Text = "Semana";
            this.rb_Semana.UseVisualStyleBackColor = true;
            this.rb_Semana.CheckedChanged += new System.EventHandler(this.rb_Semana_CheckedChanged);
            // 
            // rb_Ano
            // 
            this.rb_Ano.AutoSize = true;
            this.rb_Ano.Location = new System.Drawing.Point(17, 123);
            this.rb_Ano.Name = "rb_Ano";
            this.rb_Ano.Size = new System.Drawing.Size(51, 21);
            this.rb_Ano.TabIndex = 214;
            this.rb_Ano.Text = "Ano";
            this.rb_Ano.UseVisualStyleBackColor = true;
            this.rb_Ano.CheckedChanged += new System.EventHandler(this.rb_Ano_CheckedChanged);
            // 
            // rb_Mes
            // 
            this.rb_Mes.AutoSize = true;
            this.rb_Mes.Location = new System.Drawing.Point(16, 96);
            this.rb_Mes.Name = "rb_Mes";
            this.rb_Mes.Size = new System.Drawing.Size(52, 21);
            this.rb_Mes.TabIndex = 213;
            this.rb_Mes.Text = "Mes";
            this.rb_Mes.UseVisualStyleBackColor = true;
            this.rb_Mes.CheckedChanged += new System.EventHandler(this.rb_Mes_CheckedChanged);
            // 
            // rb_Dia
            // 
            this.rb_Dia.AutoSize = true;
            this.rb_Dia.Checked = true;
            this.rb_Dia.Location = new System.Drawing.Point(17, 46);
            this.rb_Dia.Name = "rb_Dia";
            this.rb_Dia.Size = new System.Drawing.Size(47, 21);
            this.rb_Dia.TabIndex = 212;
            this.rb_Dia.TabStop = true;
            this.rb_Dia.Text = "Dia";
            this.rb_Dia.UseVisualStyleBackColor = true;
            this.rb_Dia.CheckedChanged += new System.EventHandler(this.rb_Dia_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(913, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 25);
            this.label1.TabIndex = 217;
            this.label1.Text = "Filtros";
            // 
            // lb_Data
            // 
            this.lb_Data.AutoSize = true;
            this.lb_Data.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_Data.Location = new System.Drawing.Point(893, 548);
            this.lb_Data.Name = "lb_Data";
            this.lb_Data.Size = new System.Drawing.Size(114, 17);
            this.lb_Data.TabIndex = 218;
            this.lb_Data.Text = "Escolha Semana";
            // 
            // cb_TipoRelatorio
            // 
            this.cb_TipoRelatorio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_TipoRelatorio.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.cb_TipoRelatorio.FormattingEnabled = true;
            this.cb_TipoRelatorio.Location = new System.Drawing.Point(879, 102);
            this.cb_TipoRelatorio.Name = "cb_TipoRelatorio";
            this.cb_TipoRelatorio.Size = new System.Drawing.Size(131, 21);
            this.cb_TipoRelatorio.TabIndex = 220;
            this.cb_TipoRelatorio.SelectedIndexChanged += new System.EventHandler(this.cb_TipoRelatorio_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(892, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 17);
            this.label3.TabIndex = 219;
            this.label3.Text = "Tipo de Relatorio";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(908, 2);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 20);
            this.label5.TabIndex = 222;
            this.label5.Text = "CIR Atual";
            // 
            // tb_CIRAtual
            // 
            this.tb_CIRAtual.BackColor = System.Drawing.SystemColors.Control;
            this.tb_CIRAtual.ForeColor = System.Drawing.Color.Black;
            this.tb_CIRAtual.Location = new System.Drawing.Point(873, 28);
            this.tb_CIRAtual.Name = "tb_CIRAtual";
            this.tb_CIRAtual.ReadOnly = true;
            this.tb_CIRAtual.Size = new System.Drawing.Size(142, 20);
            this.tb_CIRAtual.TabIndex = 223;
            this.tb_CIRAtual.Text = "123";
            this.tb_CIRAtual.TextChanged += new System.EventHandler(this.tb_CIRAtual_TextChanged);
            // 
            // lb_inicio
            // 
            this.lb_inicio.AutoSize = true;
            this.lb_inicio.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_inicio.Location = new System.Drawing.Point(856, 570);
            this.lb_inicio.Name = "lb_inicio";
            this.lb_inicio.Size = new System.Drawing.Size(40, 17);
            this.lb_inicio.TabIndex = 225;
            this.lb_inicio.Text = "Inicio";
            this.lb_inicio.Visible = false;
            // 
            // Btn_Voltar
            // 
            this.Btn_Voltar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Btn_Voltar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_Voltar.Location = new System.Drawing.Point(664, 570);
            this.Btn_Voltar.Name = "Btn_Voltar";
            this.Btn_Voltar.Size = new System.Drawing.Size(73, 27);
            this.Btn_Voltar.TabIndex = 236;
            this.Btn_Voltar.Text = "Voltar";
            this.Btn_Voltar.UseVisualStyleBackColor = true;
            this.Btn_Voltar.Click += new System.EventHandler(this.Btn_Voltar_Click);
            // 
            // Btn_Sair
            // 
            this.Btn_Sair.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Btn_Sair.Location = new System.Drawing.Point(752, 570);
            this.Btn_Sair.Name = "Btn_Sair";
            this.Btn_Sair.Size = new System.Drawing.Size(73, 27);
            this.Btn_Sair.TabIndex = 237;
            this.Btn_Sair.Text = "Sair";
            this.Btn_Sair.UseVisualStyleBackColor = true;
            this.Btn_Sair.Click += new System.EventHandler(this.Btn_Sair_Click);
            // 
            // cb_PeçaProcurada
            // 
            this.cb_PeçaProcurada.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_PeçaProcurada.Enabled = false;
            this.cb_PeçaProcurada.FormattingEnabled = true;
            this.cb_PeçaProcurada.Location = new System.Drawing.Point(879, 197);
            this.cb_PeçaProcurada.Name = "cb_PeçaProcurada";
            this.cb_PeçaProcurada.Size = new System.Drawing.Size(131, 21);
            this.cb_PeçaProcurada.TabIndex = 239;
            this.cb_PeçaProcurada.SelectedIndexChanged += new System.EventHandler(this.cb_PeçaProcurada_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(896, 175);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 17);
            this.label2.TabIndex = 238;
            this.label2.Text = "Peça Procurada";
            // 
            // cb_FamiliaDefeitoProcurado
            // 
            this.cb_FamiliaDefeitoProcurado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_FamiliaDefeitoProcurado.FormattingEnabled = true;
            this.cb_FamiliaDefeitoProcurado.Location = new System.Drawing.Point(879, 244);
            this.cb_FamiliaDefeitoProcurado.Name = "cb_FamiliaDefeitoProcurado";
            this.cb_FamiliaDefeitoProcurado.Size = new System.Drawing.Size(131, 21);
            this.cb_FamiliaDefeitoProcurado.TabIndex = 241;
            this.cb_FamiliaDefeitoProcurado.SelectedIndexChanged += new System.EventHandler(this.cb_FamiliaDefeitoProcurado_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(870, 219);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(166, 17);
            this.label4.TabIndex = 240;
            this.label4.Text = " Defeito Geral Procurado";
            // 
            // cb_FamiliaPeça
            // 
            this.cb_FamiliaPeça.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_FamiliaPeça.FormattingEnabled = true;
            this.cb_FamiliaPeça.Location = new System.Drawing.Point(879, 150);
            this.cb_FamiliaPeça.Name = "cb_FamiliaPeça";
            this.cb_FamiliaPeça.Size = new System.Drawing.Size(131, 21);
            this.cb_FamiliaPeça.TabIndex = 243;
            this.cb_FamiliaPeça.SelectedIndexChanged += new System.EventHandler(this.cb_FamiliaPeça_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(878, 130);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(149, 17);
            this.label6.TabIndex = 242;
            this.label6.Text = "Peça Geral Procurada";
            // 
            // cb_DefeitoEspecifico
            // 
            this.cb_DefeitoEspecifico.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_DefeitoEspecifico.Enabled = false;
            this.cb_DefeitoEspecifico.FormattingEnabled = true;
            this.cb_DefeitoEspecifico.Location = new System.Drawing.Point(879, 291);
            this.cb_DefeitoEspecifico.Name = "cb_DefeitoEspecifico";
            this.cb_DefeitoEspecifico.Size = new System.Drawing.Size(131, 21);
            this.cb_DefeitoEspecifico.TabIndex = 245;
            this.cb_DefeitoEspecifico.SelectedIndexChanged += new System.EventHandler(this.cb_DefeitoEspecifico_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(890, 269);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(123, 17);
            this.label7.TabIndex = 244;
            this.label7.Text = "Defeito Procurado";
            // 
            // dgv_RelatoriosSalvos
            // 
            this.dgv_RelatoriosSalvos.AllowUserToAddRows = false;
            this.dgv_RelatoriosSalvos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgv_RelatoriosSalvos.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_RelatoriosSalvos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_RelatoriosSalvos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Salmon;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_RelatoriosSalvos.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_RelatoriosSalvos.Location = new System.Drawing.Point(10, 2);
            this.dgv_RelatoriosSalvos.Name = "dgv_RelatoriosSalvos";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_RelatoriosSalvos.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_RelatoriosSalvos.RowHeadersVisible = false;
            this.dgv_RelatoriosSalvos.RowHeadersWidth = 51;
            this.dgv_RelatoriosSalvos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_RelatoriosSalvos.Size = new System.Drawing.Size(843, 536);
            this.dgv_RelatoriosSalvos.TabIndex = 246;
            // 
            // btn_Imprimir
            // 
            this.btn_Imprimir.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Imprimir.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Imprimir.Location = new System.Drawing.Point(573, 570);
            this.btn_Imprimir.Name = "btn_Imprimir";
            this.btn_Imprimir.Size = new System.Drawing.Size(73, 27);
            this.btn_Imprimir.TabIndex = 247;
            this.btn_Imprimir.Text = "Imprimir";
            this.btn_Imprimir.UseVisualStyleBackColor = true;
            this.btn_Imprimir.Click += new System.EventHandler(this.btn_Imprimir_Click);
            // 
            // btn_VerGrafico
            // 
            this.btn_VerGrafico.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_VerGrafico.Location = new System.Drawing.Point(347, 556);
            this.btn_VerGrafico.Name = "btn_VerGrafico";
            this.btn_VerGrafico.Size = new System.Drawing.Size(137, 49);
            this.btn_VerGrafico.TabIndex = 248;
            this.btn_VerGrafico.Text = "Ver Graficos";
            this.btn_VerGrafico.UseVisualStyleBackColor = true;
            this.btn_VerGrafico.Click += new System.EventHandler(this.btn_VerGrafico_Click_1);
            // 
            // Dta_fim
            // 
            this.Dta_fim.CustomFormat = "dd/MM/yyyy ";
            this.Dta_fim.Enabled = false;
            this.Dta_fim.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.Dta_fim.Location = new System.Drawing.Point(897, 594);
            this.Dta_fim.Name = "Dta_fim";
            this.Dta_fim.Size = new System.Drawing.Size(94, 20);
            this.Dta_fim.TabIndex = 224;
            this.Dta_fim.Visible = false;
            // 
            // lb_fim
            // 
            this.lb_fim.AutoSize = true;
            this.lb_fim.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_fim.Location = new System.Drawing.Point(865, 596);
            this.lb_fim.Name = "lb_fim";
            this.lb_fim.Size = new System.Drawing.Size(30, 17);
            this.lb_fim.TabIndex = 226;
            this.lb_fim.Text = "Fim";
            this.lb_fim.Visible = false;
            // 
            // RelatoriosDeSaidaReformaSalvos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1038, 629);
            this.Controls.Add(this.btn_VerGrafico);
            this.Controls.Add(this.btn_Imprimir);
            this.Controls.Add(this.dgv_RelatoriosSalvos);
            this.Controls.Add(this.cb_DefeitoEspecifico);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cb_FamiliaPeça);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cb_FamiliaDefeitoProcurado);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cb_PeçaProcurada);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Btn_Voltar);
            this.Controls.Add(this.Btn_Sair);
            this.Controls.Add(this.lb_fim);
            this.Controls.Add(this.lb_inicio);
            this.Controls.Add(this.Dta_fim);
            this.Controls.Add(this.tb_CIRAtual);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cb_TipoRelatorio);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lb_Data);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gb_TipoDeBusqueda);
            this.Controls.Add(this.gb_Turno);
            this.Controls.Add(this.Dtp_data);
            this.Name = "RelatoriosDeSaidaReformaSalvos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Relatorios De Saida Reforma ";
            this.Load += new System.EventHandler(this.RelatoriosDeSaidaReformaSalvos_Load);
            this.gb_Turno.ResumeLayout(false);
            this.gb_Turno.PerformLayout();
            this.gb_TipoDeBusqueda.ResumeLayout(false);
            this.gb_TipoDeBusqueda.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_RelatoriosSalvos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DateTimePicker Dtp_data;
        private System.Windows.Forms.RadioButton rb_1;
        private System.Windows.Forms.RadioButton rb_2;
        private System.Windows.Forms.GroupBox gb_Turno;
        private System.Windows.Forms.RadioButton rb_3;
        private System.Windows.Forms.GroupBox gb_TipoDeBusqueda;
        private System.Windows.Forms.RadioButton rb_Ano;
        private System.Windows.Forms.RadioButton rb_Mes;
        private System.Windows.Forms.RadioButton rb_Dia;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label lb_Data;
        private System.Windows.Forms.ComboBox cb_TipoRelatorio;
        public System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tb_CIRAtual;
        public System.Windows.Forms.Label lb_inicio;
        private System.Windows.Forms.RadioButton rb_Semana;
        private System.Windows.Forms.Button Btn_Voltar;
        private System.Windows.Forms.Button Btn_Sair;
        private System.Windows.Forms.ComboBox cb_PeçaProcurada;
        public System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cb_FamiliaDefeitoProcurado;
        public System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cb_FamiliaPeça;
        public System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cb_DefeitoEspecifico;
        public System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView dgv_RelatoriosSalvos;
        private System.Windows.Forms.Button btn_Imprimir;
        private System.Windows.Forms.Button btn_VerGrafico;
        private System.Windows.Forms.DateTimePicker Dta_fim;
        public System.Windows.Forms.Label lb_fim;
    }
}