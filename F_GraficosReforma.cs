using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Atlas_projeto
{
    public partial class F_GraficosReforma : Form
    {

        string NomeTabla;
        DataTable dt;
        DataTable dt2;   
        DataTable temporal;
        DataTable Base;
       
        int Turno;
        public F_GraficosReforma()
        {
            InitializeComponent();
        }



        private void F_GraficosReforma_Load(object sender, EventArgs e)
        {
            #region PopularComboBoxes
            //Popular ComboBox Tipo de relatorio
            Dictionary<string, string> Clasificação = new Dictionary<string, string>();
                      
            Clasificação.Add("R", "RETRABALHO");
            Clasificação.Add("S", "SUCATA");
            Clasificação.Add("B", "BOAS");            


            cb_TipoRelatorio.Items.Clear();
            cb_TipoRelatorio.DataSource = new BindingSource(Clasificação, null);
            cb_TipoRelatorio.DisplayMember = "Value";
            cb_TipoRelatorio.ValueMember = "key";

            // populando ComboBox FamiliaPeçaProcurada
            cb_FamiliaPeça.Items.Clear();
            cb_FamiliaPeça.DataSource = Banco.ObterTodos("Familias", "*", "IdFamilia");
            cb_FamiliaPeça.DisplayMember = "Familia";
            cb_FamiliaPeça.ValueMember = "IdFamilia";

            // populando ComboBox PeçaProcurada
            cb_PeçaProcurada.Items.Clear();
            cb_PeçaProcurada.DataSource = Banco.ObterTodos("Peças", "*", "CIP");
            cb_PeçaProcurada.DisplayMember = "NomeCIP";
            cb_PeçaProcurada.ValueMember = "CIP";

            //Populando ComboBox DefeitoGeral

            cb_FamiliaDefeitoProcurado.Items.Clear();
            cb_FamiliaDefeitoProcurado.DataSource = Banco.ObterTodos("FamiliasDefeitos", "*", "ID_Familia");
            cb_FamiliaDefeitoProcurado.DisplayMember = "T_Nome";
            cb_FamiliaDefeitoProcurado.ValueMember = "ID_Familia";

            //Populando ComboBox DefeitoEspecifico

            cb_DefeitoEspecifico.Items.Clear();
            cb_DefeitoEspecifico.DataSource = Banco.ObterTodos("TiposDefeitos", "*", "CID");
            cb_DefeitoEspecifico.DisplayMember = "NomeTipo";
            cb_DefeitoEspecifico.ValueMember = "CID";
            #endregion;

            #region ConfigGerales
            cb_DefeitoEspecifico.SelectedValue = string.Empty;
            cb_PeçaProcurada.SelectedValue = string.Empty;
            AssignarTablaProcurada();
            AssignarValorTurno();
            tb_CIRAtual.Text = Turno + "-" + Dtp_data.Value.ToShortDateString();
            #endregion;

            #region DatGripViewBasicoVazio
            Base = new DataTable();
            Base.Columns.Add(new DataColumn("CIDE", typeof(string)));
            Base.Columns.Add(new DataColumn("NomePeça", typeof(string)));
            Base.Columns.Add(new DataColumn("CIP", typeof(string)));
            Base.Columns.Add(new DataColumn("CID", typeof(string)));
            Base.Columns.Add(new DataColumn("Quantidade", typeof(Int64)));
            Base.Columns.Add(new DataColumn("Data", typeof(string)));
            Base.Columns.Add(new DataColumn("Turno", typeof(Int64)));
            Base.Columns.Add(new DataColumn("Resp. Apontamento", typeof(string)));
            Base.Columns.Add(new DataColumn("Resp. Setor", typeof(string)));
            
            DataRow row = Base.NewRow();           
            row["CIDE"] = string.Empty;
            row["NomePeça"] = "Vazio";
            row["CIP"] = string.Empty;           
            row["CID"] = string.Empty;
            row["Quantidade"] = 0;
            row["Data"] = null;
            row["Turno"] = 0;
            row["Resp. Apontamento"] = string.Empty;
            row["Resp. Setor"] = string.Empty;
            Base.Rows.Add(row);

            #endregion;

        }


        #region PROCEDIMIENTOS
        private void AssignarTablaProcurada()
        {
            if (cb_TipoRelatorio.SelectedIndex == 0)
            {
                NomeTabla = "RelatorioSaidaReforma_Retrabalho";
            }
            else
            {
                if (cb_TipoRelatorio.SelectedIndex == 1)
                {
                    NomeTabla = "RelatorioSaidaReforma_Sucata";
                }
                else
                {
                    if (cb_TipoRelatorio.SelectedIndex == 2)
                    {
                        NomeTabla = "RelatorioSaidaReforma_Boas";
                    }
                }
            }
        }
        private void AssignarValorTurno()
        {
            #region AssignandoValorTurno
            Turno = 0;
            if (rb_1.Checked)
            {
                Turno = 1;
            }
            else
            {
                if (rb_2.Checked)
                {
                    Turno = 2;
                }
                else
                {
                    Turno = 3;
                }
            }
           
            #endregion;
        }

        private void PrencherGrafico(DataTable Dados)
         {           
             G_Grafico_Geral.Series.Clear();
             G_Grafico_Geral.Titles.Clear();

             Title title = new Title();
             title.Font = new Font("Arial", 12, FontStyle.Bold);
             title.ForeColor = Color.Green;
             string FamiliaDefeito = cb_FamiliaDefeitoProcurado.Text.ToUpper();
             title.Text = "Grafico Defeito-Peça\n" + cb_TipoRelatorio.Text+ " --" + cb_FamiliaPeça.Text+"\n"+FamiliaDefeito;
             G_Grafico_Geral.Titles.Add(title);
             
            G_Grafico_Geral.Series.Add("Produção");

            foreach (DataRow dr in Dados.Rows)
            { string x = dr["NomePeça"] + "--" + dr["CID"];
                
                Int64 y = Convert.ToInt64( dr["Quantidade"]);

                G_Grafico_Geral.Series["Produção"].ChartType = SeriesChartType.Column;
                G_Grafico_Geral.Series["Produção"].BorderWidth = 4;

                G_Grafico_Geral.Series["Produção"].Points.AddXY(x, y);

                 G_Grafico_Geral.Series["Produção"].IsValueShownAsLabel = true;
                 G_Grafico_Geral.Series["Produção"].LabelAngle = 90;

            }



            G_Grafico_Geral.Series[0].SmartLabelStyle.Enabled = false;
             foreach (DataPoint dp in G_Grafico_Geral.Series[0].Points)
             {
                 dp.LabelAngle = -90;
             }
             G_Grafico_Geral.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
             G_Grafico_Geral.ChartAreas[0].AxisX.LabelStyle.Angle = 90;
             G_Grafico_Geral.ChartAreas[0].AxisX.Interval = 1;


         }
        private DataTable AtualizarDatosDelDGV()
        {
            #region AtualizandoDGV
            AssignarTablaProcurada();
            if (rb_Dia.Checked)
            {

                dt = Banco.Procurar3Criterios(NomeTabla, "*", "CIR", "CIP", "CID", "'" + tb_CIRAtual.Text + "%'", "'" + cb_PeçaProcurada.SelectedValue + "%'", "'" + cb_DefeitoEspecifico.SelectedValue + "%'", "CIR");

                if (dt.Rows.Count > 0)
                {
                    ArrayList array = new ArrayList();
                    dt2 = new DataTable();
                    dt2.Columns.Add(new DataColumn("CIDE", typeof(string)));
                    dt2.Columns.Add(new DataColumn("NomePeça", typeof(string)));
                    dt2.Columns.Add(new DataColumn("CIP", typeof(string)));
                    dt2.Columns.Add(new DataColumn("CID", typeof(string)));
                    dt2.Columns.Add(new DataColumn("Quantidade", typeof(int)));
                    dt2.Columns.Add(new DataColumn("Data", typeof(string)));
                    dt2.Columns.Add(new DataColumn("Turno", typeof(string)));
                    dt2.Columns.Add(new DataColumn("Resp. Apontamento", typeof(string)));
                    dt2.Columns.Add(new DataColumn("Resp. Setor", typeof(string)));


                    foreach (DataRow dr in dt.Rows)
                    {
                        object total;

                        if (array.IndexOf(dr["CIDE"]) < 0)
                        {

                            total = dt.Compute(String.Format("SUM(Quantidade)"), "CIDE = '" + dr["CIDE"] + "'");

                            dt2.Rows.Add(new object[] { dr["CIDE"], dr["NomePeça"], dr["CIP"], dr["CID"], Convert.ToInt32(total), Dtp_data.Text, dr["Turno"], dr["Resp. Apontamento"], dr["Resp. Setor"] });

                            array.Add(dr["CIDE"]);

                        }

                    }
                    DataView dv = dt2.DefaultView;
                    dv.Sort = "CIP";
                    DataTable sorteddt2 = dv.ToTable();                    

                    tb_CIRAtual.BackColor = Color.White;
                    tb_CIRAtual.ForeColor = Color.DarkGreen;
                    return sorteddt2;

                }
                else
                {                   
                    tb_CIRAtual.BackColor = Color.White;
                    tb_CIRAtual.ForeColor = Color.Red;
                    return Base;
                }

            }
            else
            {
                if (rb_Mes.Checked)
                {
                    AssignarValorTurno();

                    dt = Banco.Procurar4Criterios(NomeTabla, "*", "CIR", "CIP", "CID", "Turno", "'%/" + Dtp_data.Value.Month + "/%'", "'" + cb_PeçaProcurada.SelectedValue + "%'", "'" + cb_DefeitoEspecifico.SelectedValue + "%'", "" + Turno + "", "CIR");

                    if (dt.Rows.Count > 0)
                    {
                        ArrayList array = new ArrayList();

                        dt2 = new DataTable();
                        dt2.Columns.Add(new DataColumn("CIDE", typeof(string)));
                        dt2.Columns.Add(new DataColumn("NomePeça", typeof(string)));
                        dt2.Columns.Add(new DataColumn("CIP", typeof(string)));
                        dt2.Columns.Add(new DataColumn("CID", typeof(string)));
                        dt2.Columns.Add(new DataColumn("Quantidade", typeof(int)));
                        dt2.Columns.Add(new DataColumn("Data", typeof(string)));
                        dt2.Columns.Add(new DataColumn("Turno", typeof(string)));
                        dt2.Columns.Add(new DataColumn("Resp. Apontamento", typeof(string)));
                        dt2.Columns.Add(new DataColumn("Resp. Setor", typeof(string)));


                        foreach (DataRow dr in dt.Rows)

                        {

                            object total;


                            if (array.IndexOf(dr["CIDE"]) < 0)

                            {

                                total = dt.Compute(String.Format("SUM(Quantidade)"), "CIDE = '" + dr["CIDE"] + "'");

                                dt2.Rows.Add(new object[] { dr["CIDE"], dr["NomePeça"], dr["CIP"], dr["CID"], Convert.ToInt32(total), Dtp_data.Text + "/" + Dtp_data.Value.Year, dr["Turno"], dr["Resp. Apontamento"], dr["Resp. Setor"] });

                                array.Add(dr["CIDE"]);

                            }

                        }

                        DataView dv = dt2.DefaultView;
                        dv.Sort = "CIP";
                        DataTable sorteddt2 = dv.ToTable();
                     

                        tb_CIRAtual.BackColor = Color.White;
                        tb_CIRAtual.ForeColor = Color.DarkGreen;
                        return sorteddt2;
                    }
                    else
                    {                        
                        tb_CIRAtual.BackColor = Color.White;
                        tb_CIRAtual.ForeColor = Color.Red;
                        return Base;
                    }


                }
                else
                {
                    if (rb_Ano.Checked)
                    {
                        AssignarValorTurno();

                        dt = Banco.Procurar4Criterios(NomeTabla, "*", "CIR", "CIP", "CID", "Turno", "'%/" + Dtp_data.Value.Year + "'", "'" + cb_PeçaProcurada.SelectedValue + "%'", "'" + cb_DefeitoEspecifico.SelectedValue + "%'", "" + Turno + "", "CIR");

                        if (dt.Rows.Count > 0)
                        {
                            ArrayList array = new ArrayList();

                            dt2 = new DataTable();
                            dt2.Columns.Add(new DataColumn("CIDE", typeof(string)));
                            dt2.Columns.Add(new DataColumn("NomePeça", typeof(string)));
                            dt2.Columns.Add(new DataColumn("CIP", typeof(string)));
                            dt2.Columns.Add(new DataColumn("CID", typeof(string)));
                            dt2.Columns.Add(new DataColumn("Quantidade", typeof(int)));
                            dt2.Columns.Add(new DataColumn("Data", typeof(string)));
                            dt2.Columns.Add(new DataColumn("Turno", typeof(string)));
                            dt2.Columns.Add(new DataColumn("Resp. Apontamento", typeof(string)));
                            dt2.Columns.Add(new DataColumn("Resp. Setor", typeof(string)));


                            foreach (DataRow dr in dt.Rows)

                            {

                                object total;


                                if (array.IndexOf(dr["CIDE"]) < 0)

                                {

                                    total = dt.Compute(String.Format("SUM(Quantidade)"), "CIDE = '" + dr["CIDE"] + "'");

                                    dt2.Rows.Add(new object[] { dr["CIDE"], dr["NomePeça"], dr["CIP"], dr["CID"], Convert.ToInt32(total), Dtp_data.Value.Year, dr["Turno"], dr["Resp. Apontamento"], dr["Resp. Setor"] });

                                    array.Add(dr["CIDE"]);

                                }

                            }

                            DataView dv = dt2.DefaultView;
                            dv.Sort = "CIP";
                            DataTable sorteddt2 = dv.ToTable();
                          
                            tb_CIRAtual.BackColor = Color.White;
                            tb_CIRAtual.ForeColor = Color.DarkGreen;
                            return sorteddt2;
                        }
                        else
                        {                           
                            tb_CIRAtual.BackColor = Color.White;
                            tb_CIRAtual.ForeColor = Color.Red;
                            return Base;
                        }
                    }
                    else
                    {
                        if (rb_Semana.Checked)
                        {
                            AssignarValorTurno();

                            dt = new DataTable();
                            dt.Columns.Add(new DataColumn("CIR", typeof(string)));
                            dt.Columns.Add(new DataColumn("CIDE", typeof(string)));
                            dt.Columns.Add(new DataColumn("NomePeça", typeof(string)));
                            dt.Columns.Add(new DataColumn("CIP", typeof(string)));
                            dt.Columns.Add(new DataColumn("CID", typeof(string)));
                            dt.Columns.Add(new DataColumn("Quantidade", typeof(Int64)));
                            dt.Columns.Add(new DataColumn("Data", typeof(string)));
                            dt.Columns.Add(new DataColumn("Turno", typeof(Int64)));
                            dt.Columns.Add(new DataColumn("Resp. Apontamento", typeof(string)));
                            dt.Columns.Add(new DataColumn("Resp. Setor", typeof(string)));


                            for (DateTime i = Dtp_data.Value; i <= Dta_fim.Value; i = i.AddDays(1))
                            {
                                temporal = new DataTable();
                                temporal = Banco.Procurar3Criterios(NomeTabla, "*", "CIR", "CIP", "CID", "'" + Turno + "-" + i.ToShortDateString() + "'", "'" + cb_PeçaProcurada.SelectedValue + "%'", "'" + cb_DefeitoEspecifico.SelectedValue + "%'", "CIR");
                                dt.Merge(temporal, true);
                            }

                            if (dt.Rows.Count > 0)
                            {
                                ArrayList array = new ArrayList();

                                dt2 = new DataTable();
                                dt2.Columns.Add(new DataColumn("CIDE", typeof(string)));
                                dt2.Columns.Add(new DataColumn("NomePeça", typeof(string)));
                                dt2.Columns.Add(new DataColumn("CIP", typeof(string)));
                                dt2.Columns.Add(new DataColumn("CID", typeof(string)));
                                dt2.Columns.Add(new DataColumn("Quantidade", typeof(int)));
                                dt2.Columns.Add(new DataColumn("Data", typeof(string)));
                                dt2.Columns.Add(new DataColumn("Turno", typeof(string)));
                                dt2.Columns.Add(new DataColumn("Resp. Apontamento", typeof(string)));
                                dt2.Columns.Add(new DataColumn("Resp. Setor", typeof(string)));


                                foreach (DataRow dr in dt.Rows)

                                {

                                    object total;


                                    if (array.IndexOf(dr["CIDE"]) < 0)

                                    {

                                        total = dt.Compute(String.Format("SUM(Quantidade)"), "CIDE = '" + dr["CIDE"] + "'");

                                        dt2.Rows.Add(new object[] { dr["CIDE"], dr["NomePeça"], dr["CIP"], dr["CID"], Convert.ToInt32(total), Dtp_data.Value.ToShortDateString() + "--" + Dta_fim.Value.ToShortDateString(), dr["Turno"], dr["Resp. Apontamento"], dr["Resp. Setor"] });

                                        array.Add(dr["CIDE"]);

                                    }

                                }

                                DataView dv = dt2.DefaultView;
                                dv.Sort = "CIP";
                                DataTable sorteddt2 = dv.ToTable();                          


                                tb_CIRAtual.BackColor = Color.White;
                                tb_CIRAtual.ForeColor = Color.DarkGreen;
                                return sorteddt2;
                            }
                            else
                            {                                
                                tb_CIRAtual.BackColor = Color.White;
                                tb_CIRAtual.ForeColor = Color.Red;

                                return Base;
                            }
                        }
                        else
                        {
                            return Base;
                        }
                    }
                }
            }
            #endregion;
        }
        
        #endregion;


        private void rb_1_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_1.Checked && rb_Mes.Checked == false && rb_Ano.Checked == false && rb_Semana.Checked == false)
            {
                tb_CIRAtual.Text = rb_1.Text + "-" + Dtp_data.Value.ToShortDateString();
            }
            else
            {
                if (rb_Mes.Checked && rb_1.Checked)
                {
                    Dtp_data.CustomFormat = "MMMM";
                    Dtp_data.ShowUpDown = true;
                    tb_CIRAtual.Text = gb_Turno.Controls.OfType<RadioButton>().SingleOrDefault(RadioButton => RadioButton.Checked).Text + "-" + Dtp_data.Value.Month.ToString() + "/" + Dtp_data.Value.Year.ToString();
                    lb_Data.Text = "Escolha um Mes";
                }

                if (rb_Ano.Checked && rb_1.Checked)
                {
                    Dtp_data.CustomFormat = "yyyy";
                    Dtp_data.ShowUpDown = true;
                    tb_CIRAtual.Text = gb_Turno.Controls.OfType<RadioButton>().SingleOrDefault(RadioButton => RadioButton.Checked).Text + "-" + Dtp_data.Text;
                    lb_Data.Text = "Escolha um Ano";
                }

                if (rb_Semana.Checked && rb_1.Checked)
                {
                    Dtp_data.CustomFormat = "dd/MM/yyyy";
                    Dtp_data.ShowUpDown = false;

                    DateTime datafim = new DateTime(Dtp_data.Value.Year, Dtp_data.Value.Month, Dtp_data.Value.Day);


                    Dta_fim.Value = datafim.AddDays(7);
                    tb_CIRAtual.Text = gb_Turno.Controls.OfType<RadioButton>().SingleOrDefault(RadioButton => RadioButton.Checked).Text + "-" + Dtp_data.Value.ToShortDateString() + ".." + Dta_fim.Value.ToShortDateString();
                    lb_Data.Text = "Escolha a Semana";
                    lb_inicio.Visible = true;
                    lb_fim.Visible = true;
                    Dta_fim.Visible = true;
                }
                else
                {
                    lb_inicio.Visible = false;
                    lb_fim.Visible = false;
                    Dta_fim.Visible = false;
                }

            }
        }

        private void rb_2_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_2.Checked && rb_Dia.Checked)
            {
                tb_CIRAtual.Text = rb_2.Text + "-" + Dtp_data.Value.ToShortDateString();
            }
            else
            {
                if (rb_Mes.Checked && rb_2.Checked)
                {
                    Dtp_data.CustomFormat = "MMMM";
                    Dtp_data.ShowUpDown = true;
                    tb_CIRAtual.Text = gb_Turno.Controls.OfType<RadioButton>().SingleOrDefault(RadioButton => RadioButton.Checked).Text + "-" + Dtp_data.Value.Month.ToString() + "/" + Dtp_data.Value.Year.ToString();
                    lb_Data.Text = "Escolha um Mes";
                }

                if (rb_Ano.Checked && rb_2.Checked)
                {
                    Dtp_data.CustomFormat = "yyyy";
                    Dtp_data.ShowUpDown = true;
                    tb_CIRAtual.Text = gb_Turno.Controls.OfType<RadioButton>().SingleOrDefault(RadioButton => RadioButton.Checked).Text + "-" + Dtp_data.Text;
                    lb_Data.Text = "Escolha um Ano";
                }
                if (rb_Semana.Checked && rb_2.Checked)
                {
                    Dtp_data.CustomFormat = "dd/MM/yyyy";
                    Dtp_data.ShowUpDown = false;

                    DateTime datafim = new DateTime(Dtp_data.Value.Year, Dtp_data.Value.Month, Dtp_data.Value.Day);


                    Dta_fim.Value = datafim.AddDays(7);
                    tb_CIRAtual.Text = gb_Turno.Controls.OfType<RadioButton>().SingleOrDefault(RadioButton => RadioButton.Checked).Text + "-" + Dtp_data.Value.ToShortDateString() + ".." + Dta_fim.Value.ToShortDateString();
                    lb_Data.Text = "Escolha a Semana";
                    lb_inicio.Visible = true;
                    lb_fim.Visible = true;
                    Dta_fim.Visible = true;
                }
                else
                {
                    lb_inicio.Visible = false;
                    lb_fim.Visible = false;
                    Dta_fim.Visible = false;
                }

            }

        }

        private void rb_3_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_3.Checked && rb_Dia.Checked)
            {
                tb_CIRAtual.Text = rb_3.Text + "-" + Dtp_data.Value.ToShortDateString();
            }
            else
            {
                if (rb_Mes.Checked && rb_3.Checked)
                {
                    Dtp_data.CustomFormat = "MMMM";
                    Dtp_data.ShowUpDown = true;
                    tb_CIRAtual.Text = gb_Turno.Controls.OfType<RadioButton>().SingleOrDefault(RadioButton => RadioButton.Checked).Text + "-" + Dtp_data.Value.Month.ToString() + "/" + Dtp_data.Value.Year.ToString();
                    lb_Data.Text = "Escolha um Mes";
                }

                if (rb_Ano.Checked && rb_3.Checked)
                {
                    Dtp_data.CustomFormat = "yyyy";
                    Dtp_data.ShowUpDown = true;
                    tb_CIRAtual.Text = gb_Turno.Controls.OfType<RadioButton>().SingleOrDefault(RadioButton => RadioButton.Checked).Text + "-" + Dtp_data.Text;
                    lb_Data.Text = "Escolha um Ano";
                }
                if (rb_Semana.Checked && rb_3.Checked)
                {
                    Dtp_data.CustomFormat = "dd/MM/yyyy";
                    Dtp_data.ShowUpDown = false;

                    DateTime datafim = new DateTime(Dtp_data.Value.Year, Dtp_data.Value.Month, Dtp_data.Value.Day);


                    Dta_fim.Value = datafim.AddDays(7);
                    tb_CIRAtual.Text = gb_Turno.Controls.OfType<RadioButton>().SingleOrDefault(RadioButton => RadioButton.Checked).Text + "-" + Dtp_data.Value.ToShortDateString() + ".." + Dta_fim.Value.ToShortDateString();
                    lb_Data.Text = "Escolha a Semana";
                    lb_inicio.Visible = true;
                    lb_fim.Visible = true;
                    Dta_fim.Visible = true;
                }
                else
                {
                    lb_inicio.Visible = false;
                    lb_fim.Visible = false;
                    Dta_fim.Visible = false;
                }

            }
        }

        private void rb_Dia_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_Dia.Checked)
            {
                Dtp_data.CustomFormat = "dd/MM/yyyy";
                Dtp_data.ShowUpDown = false;
                tb_CIRAtual.Text = gb_Turno.Controls.OfType<RadioButton>().SingleOrDefault(RadioButton => RadioButton.Checked).Text + "-" + Dtp_data.Value.ToShortDateString();
                lb_Data.Text = "Escolha um Dia";
            }
        }

        private void rb_Semana_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_Semana.Checked)
            {
                Dtp_data.CustomFormat = "dd/MM/yyyy";
                Dtp_data.ShowUpDown = false;

                DateTime datafim = new DateTime(Dtp_data.Value.Year, Dtp_data.Value.Month, Dtp_data.Value.Day);


                Dta_fim.Value = datafim.AddDays(7);
                tb_CIRAtual.Text = gb_Turno.Controls.OfType<RadioButton>().SingleOrDefault(RadioButton => RadioButton.Checked).Text + "-" + Dtp_data.Value.ToShortDateString() + ".." + Dta_fim.Value.ToShortDateString();
                lb_Data.Text = "Escolha a Semana";
                lb_inicio.Visible = true;
                lb_fim.Visible = true;
                Dta_fim.Visible = true;
            }
            else
            {
                lb_inicio.Visible = false;
                lb_fim.Visible = false;
                Dta_fim.Visible = false;
            }
        }

        private void rb_Mes_CheckedChanged(object sender, EventArgs e)
        {

            if (rb_Mes.Checked)
            {
                Dtp_data.CustomFormat = "MMMM";
                Dtp_data.ShowUpDown = true;
                tb_CIRAtual.Text = gb_Turno.Controls.OfType<RadioButton>().SingleOrDefault(RadioButton => RadioButton.Checked).Text + "-" + Dtp_data.Value.Month.ToString() + "/" + Dtp_data.Value.Year.ToString();
                lb_Data.Text = "Escolha um Mes";
            }
        }

        private void rb_Ano_CheckedChanged(object sender, EventArgs e) 
        {
            if (rb_Ano.Checked)
            {
                Dtp_data.CustomFormat = "yyyy";
                Dtp_data.ShowUpDown = true;
                tb_CIRAtual.Text = gb_Turno.Controls.OfType<RadioButton>().SingleOrDefault(RadioButton => RadioButton.Checked).Text + "-" + Dtp_data.Text;
                lb_Data.Text = "Escolha um Ano";
            }
        }

        private void Dtp_data_ValueChanged(object sender, EventArgs e)
        {
            if (rb_Semana.Checked)
            {

                DateTime datafim = new DateTime(Dtp_data.Value.Year, Dtp_data.Value.Month, Dtp_data.Value.Day);
                Dta_fim.Value = datafim.AddDays(7);
                tb_CIRAtual.Text = gb_Turno.Controls.OfType<RadioButton>().SingleOrDefault(RadioButton => RadioButton.Checked).Text + "-" + Dtp_data.Value.ToShortDateString() + ".." + Dta_fim.Value.ToShortDateString();
            }
            if (rb_Ano.Checked)
            {
                Dtp_data.CustomFormat = "yyyy";
                Dtp_data.ShowUpDown = true;
                tb_CIRAtual.Text = gb_Turno.Controls.OfType<RadioButton>().SingleOrDefault(RadioButton => RadioButton.Checked).Text + "-" + Dtp_data.Text;
                lb_Data.Text = "Escolha um Ano";
            }
            if (rb_Mes.Checked)
            {
                Dtp_data.CustomFormat = "MMMM";
                Dtp_data.ShowUpDown = true;
                tb_CIRAtual.Text = gb_Turno.Controls.OfType<RadioButton>().SingleOrDefault(RadioButton => RadioButton.Checked).Text + "-" + Dtp_data.Value.Month.ToString() + "/" + Dtp_data.Value.Year.ToString();
                lb_Data.Text = "Escolha um Mes";
            }
            else
            {

            }
            if (rb_Dia.Checked)
            {
                Dtp_data.CustomFormat = "dd/MM/yyyy";
                Dtp_data.ShowUpDown = false;
                tb_CIRAtual.Text = gb_Turno.Controls.OfType<RadioButton>().SingleOrDefault(RadioButton => RadioButton.Checked).Text + "-" + Dtp_data.Value.ToShortDateString();
                lb_Data.Text = "Escolha um Dia";
            }

        }
        
        int cont4 = 0;
        private void cb_FamiliaDefeitoProcurado_SelectedIndexChanged(object sender, EventArgs e)
        {
            #region ActivaroDesactivarDefeitoProcurada
            if (cb_FamiliaDefeitoProcurado.Text != "")
            {
                cb_DefeitoEspecifico.Enabled = true;
            }
            else
            {
                cb_DefeitoEspecifico.SelectedValue = string.Empty;
                cb_DefeitoEspecifico.Enabled = false;

            }
            #endregion;

            if (cont4 > 1 && cb_DefeitoEspecifico.Enabled == true)
            {

                DataTable dt = Banco.Procurar("TiposDefeitos", "CID, NomeTipo", "NomeTipo", "'" + cb_FamiliaDefeitoProcurado.Text + "%'", "CID");

                if (dt.Rows.Count > 0)
                {
                    cb_DefeitoEspecifico.DataSource = dt;

                    PrencherGrafico(AtualizarDatosDelDGV());
                }
                else
                {
                    MessageBox.Show("Não há nenhum defeito do tipo procurado");

                }
            }
            else
            {
                cont4 += 1;
            }

        }
        
        int cont6 = 0;
        private void cb_DefeitoEspecifico_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cont6 > 1 && cb_DefeitoEspecifico.Enabled == true)
            {

                PrencherGrafico(AtualizarDatosDelDGV());
            }
            else
            {
                cont6 += 1;
            }
        }

        int cont3=0;
        private void cb_FamiliaPeça_SelectedIndexChanged(object sender, EventArgs e)
        {
            #region ActivaroDesactivarPeçaProcurada
            if (cb_FamiliaPeça.Text != "")
            {
                cb_PeçaProcurada.Enabled = true;
            }
            else
            {
                cb_PeçaProcurada.SelectedValue = string.Empty;
                cb_PeçaProcurada.Enabled = false;

            }
            #endregion;

            if (cont3 > 1 && cb_PeçaProcurada.Enabled == true)
            {

                DataTable dt = Banco.Procurar("Peças", "CIP, NomeCIP", "NomeCIP", "'" + cb_FamiliaPeça.Text + "%'", "CIP");

                if (dt.Rows.Count > 0)
                {
                    cb_PeçaProcurada.DataSource = dt;

                    PrencherGrafico(AtualizarDatosDelDGV());
                }
                else
                {
                    MessageBox.Show("Não há nenhum defeito do tipo procurado");

                }
            }
            else
            {
                cont3 += 1;
            }

        }
        
        int cont5 = 0;
        private void cb_PeçaProcurada_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cont5 > 1 && cb_PeçaProcurada.Enabled == true)
            {

                PrencherGrafico(AtualizarDatosDelDGV());

            }
            else
            {
                cont5 += 1;
            }
        }
       
        int Control = 0;        
        private void cb_TipoRelatorio_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Control >1)
            {
                PrencherGrafico(AtualizarDatosDelDGV());
            }
            else
            {
                Control += 1;
            }
        }
        int cont7 = 0;
        private void tb_CIRAtual_TextChanged(object sender, EventArgs e)
        {
            if (cont7>1)
            {

                
                PrencherGrafico(AtualizarDatosDelDGV());
            }
            else
            {
                cont7 += 1;
            }
        }
    }
}
