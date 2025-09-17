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
using Microsoft.Reporting.WinForms;

namespace Atlas_projeto
{
    
    public partial class RelatoriosDeSaidaReformaSalvos : Form
    {
        string NomeTabla ;
        string Titulo;
        string Subtitulo;
        DataTable dt;
        DataTable dt2;        
        DataTable temporal;
        DataTable Base;
        DataTable sorteddt2;
        int Turno;
        string Resp_Setor;
        string Resp_Apontamento;
        string CIR;
        string TurnoString;
        string TipoRela;
        public RelatoriosDeSaidaReformaSalvos(string TipoDeRelatotio)
        {
            InitializeComponent();
            
            TipoRela= TipoDeRelatotio;
        }
        private void RelatoriosDeSaidaReformaSalvos_Load(object sender, EventArgs e)
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

            #region ConfigDataGripView
            //Preenchendo el DataGrip...
            AssignarValorInicialDGV();
            #endregion;

            #region DGVDataTableVazio
            Base = new DataTable();           
            Base.Columns.Add(new DataColumn("NomePeça", typeof(string)));
            Base.Columns.Add(new DataColumn("CIP", typeof(string)));
            Base.Columns.Add(new DataColumn("CID", typeof(string)));
            Base.Columns.Add(new DataColumn("Quantidade", typeof(Int64)));
            Base.Columns.Add(new DataColumn("Data", typeof(string)));
            Base.Columns.Add(new DataColumn("Turno", typeof(Int64)));
            Base.Columns.Add(new DataColumn("Resp. Apontamento", typeof(string)));
            Base.Columns.Add(new DataColumn("Resp. Setor", typeof(string)));

            #endregion;

            #region ConfigGerales
            cb_DefeitoEspecifico.SelectedValue = string.Empty;
            cb_PeçaProcurada.SelectedValue = string.Empty;
            cb_TipoRelatorio.SelectedValue = TipoRela;
            #endregion;
           
        }

        #region PROCEDIMIENTOS
        private void AssignarTablaProcurada()
        {
            if (cb_TipoRelatorio.SelectedIndex == 0)
            {
               
                NomeTabla = "RelatorioSaidaReforma_Retrabalho";
                Titulo = "RELATORIO DE RETRABALHO";
             

                if (cb_FamiliaPeça.Text != "")
                {
                    if (cb_FamiliaPeça.SelectedIndex == 1 || cb_FamiliaPeça.SelectedIndex == 2 || cb_FamiliaPeça.SelectedIndex == 3 || cb_FamiliaPeça.SelectedIndex == 6)
                    {
                        Subtitulo = cb_PeçaProcurada.Text + " Retrabalhada";
                    }
                    else
                    {
                        if (cb_FamiliaPeça.SelectedIndex != 10)
                        {
                            Subtitulo = cb_PeçaProcurada.Text + " Retrabalhado";     
                        }
                    }

                }
                else
                {
                    Subtitulo = "Peças Retrabalhadas";
                }
                
                if (cb_FamiliaDefeitoProcurado.Text != "")
                {
                    
                        Subtitulo +=  " com o Defeito: "+ cb_DefeitoEspecifico.Text+"\n";                  

                }
                cb_FamiliaDefeitoProcurado.Enabled = true;
            }
            else
            {
                if (cb_TipoRelatorio.SelectedIndex == 1)
                {
                   
                    NomeTabla = "RelatorioSaidaReforma_Sucata";
                    Titulo = "RELATORIO DE SUCATA";



                    if (cb_FamiliaPeça.Text != "")
                    {
                        if (cb_FamiliaPeça.SelectedIndex == 1 || cb_FamiliaPeça.SelectedIndex == 2 || cb_FamiliaPeça.SelectedIndex == 3 || cb_FamiliaPeça.SelectedIndex == 6)
                        {
                            Subtitulo = cb_PeçaProcurada.Text + " Sucateada";
                        }
                        else
                        {
                            if (cb_FamiliaPeça.SelectedIndex != 10)
                            {
                                Subtitulo = cb_PeçaProcurada.Text + " Sucateado";
                            }
                        }

                    }
                    else
                    {
                        Subtitulo = "Peças Sucateadas";
                    }

                    if (cb_FamiliaDefeitoProcurado.Text != "")
                    {

                        Subtitulo += " com o Defeito: " + cb_DefeitoEspecifico.Text + "\n";

                    }
                    cb_FamiliaDefeitoProcurado.Enabled = true;
                }
                else
                {
                    if (cb_TipoRelatorio.SelectedIndex == 2)
                    {
              
                        cb_FamiliaDefeitoProcurado.SelectedValue = 1728;
                        NomeTabla = "RelatorioSaidaReforma_Boas";
                        Titulo = "RELATORIO DE PEÇAS BOAS";

                        if (cb_FamiliaPeça.Text != "")
                        {
                            if (cb_FamiliaPeça.SelectedIndex == 1 || cb_FamiliaPeça.SelectedIndex == 2 || cb_FamiliaPeça.SelectedIndex == 3 || cb_FamiliaPeça.SelectedIndex == 6)
                            {
                                Subtitulo = cb_PeçaProcurada.Text + " Boas Devolvidas ao Processo";
                            }
                            else
                            {
                                if (cb_FamiliaPeça.SelectedIndex != 10)
                                {
                                    Subtitulo = cb_PeçaProcurada.Text + " Bons Devolvidos ao Processo";
                                }
                            }

                        }
                        else
                        {
                            Subtitulo = "Peças Boas Devolvidas ao Processo";
                        }
                        cb_FamiliaDefeitoProcurado.Enabled = false;
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
                TurnoString = "Primer Turno";
            }
            else
            {
                if (rb_2.Checked)
                {
                    Turno = 2;
                    TurnoString = "Segundo Turno";
                }
                else
                {
                    Turno = 3;
                    TurnoString = "Tercer Turno";
                }
            }

            #endregion;
        }
        private void AtualizarDatosDelDGV()
        {
            #region AtualizandoDGV
            AssignarTablaProcurada();

            if (rb_Dia.Checked)
            {
                
               dt = Banco.Procurar3Criterios(NomeTabla, "*", "CIR", "CIP", "CID", "'" + tb_CIRAtual.Text + "'", "'" + cb_PeçaProcurada.SelectedValue + "%'", "'" + cb_DefeitoEspecifico.SelectedValue + "%'", "CIR");
               
                if (dt.Rows.Count > 0)
                {
                    ArrayList array = new ArrayList();

                    RestablecerDGVdt2();

                    foreach (DataRow dr in dt.Rows)
                    {
                        object total;

                        if (array.IndexOf(dr["CIDE"].ToString().Substring(2)) < 0)
                        {

                            total = dt.Compute(String.Format("SUM(Quantidade)"), "CIDE LIKE '%" + dr["CIDE"].ToString().Substring(2) + "'");

                            dt2.Rows.Add(new object[] {  dr["NomePeça"], dr["CIP"], dr["CID"], Convert.ToInt32(total), Dtp_data.Text, dr["Turno"], dr["Resp. Apontamento"], dr["Resp. Setor"] });

                            array.Add(dr["CIDE"].ToString().Substring(2));

                        }

                    }
                   
                    AssignarValorTurno();
                    DataView dv = dt2.DefaultView;
                    dv.Sort = "CIP";
                    Subtitulo += " no  Dia " + Dtp_data.Text+" em "+TurnoString;              
                    sorteddt2 = dv.ToTable();
                    CIR= Turno+"-"+Dtp_data.Text;
                    Resp_Setor = sorteddt2.Rows[0].Field<string>("Resp. Setor");
                    Resp_Apontamento = sorteddt2.Rows[0].Field<string>("Resp. Apontamento");
                    dgv_RelatoriosSalvos.DataSource = null;
                    dgv_RelatoriosSalvos.DataSource = sorteddt2;

                    
                    tb_CIRAtual.BackColor = Color.White;
                    tb_CIRAtual.ForeColor = Color.DarkGreen;
                    btn_Imprimir.Enabled =  true;
                }
                else
                {                    
                    Subtitulo = "Nehuma peça foi processada no  Dia " + Dtp_data.Text + " em " + TurnoString;
                    dgv_RelatoriosSalvos.DataSource = null;
                    dgv_RelatoriosSalvos.DataSource = Base;
                    btn_Imprimir.Enabled = false;
                    tb_CIRAtual.BackColor = Color.White;
                    tb_CIRAtual.ForeColor = Color.Red;
                    
                   
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

                        RestablecerDGVdt2();


                        foreach (DataRow dr in dt.Rows)

                        {

                            object total;


                            if (array.IndexOf(dr["CIDE"].ToString().Substring(2)) < 0)

                            {

                                total = dt.Compute(String.Format("SUM(Quantidade)"), "CIDE Like '%" + dr["CIDE"].ToString().Substring(2) + "'");

                                dt2.Rows.Add(new object[] {  dr["NomePeça"], dr["CIP"], dr["CID"], Convert.ToInt32(total), Dtp_data.Text + "/" + Dtp_data.Value.Year, dr["Turno"], dr["Resp. Apontamento"], dr["Resp. Setor"] });

                                array.Add(dr["CIDE"].ToString().Substring(2));

                            }

                        }

                        DataView dv = dt2.DefaultView;
                        dv.Sort = "CIP";
                        sorteddt2 = dv.ToTable();
                        CIR = Turno + "-" + Dtp_data.Text + "/" + Dtp_data.Value.Year ;
                        Resp_Setor = sorteddt2.Rows[0].Field<string>("Resp. Setor");
                        Resp_Apontamento = sorteddt2.Rows[0].Field<string>("Resp. Apontamento");
                        Subtitulo += " no  Mes de " + Dtp_data.Text + " em " + TurnoString;

                        dgv_RelatoriosSalvos.DataSource = null;
                        dgv_RelatoriosSalvos.DataSource = sorteddt2;
                        
                        btn_Imprimir.Enabled = true;

                        tb_CIRAtual.BackColor = Color.White;
                        tb_CIRAtual.ForeColor = Color.DarkGreen;
                       
                    }
                    else
                    {
                        sorteddt2 = Base;
                        btn_Imprimir.Enabled = false ;
                        Subtitulo = "Nehuma peça foi processada no  Mes de " + Dtp_data.Text + " em " + TurnoString; ;
                        dgv_RelatoriosSalvos.DataSource = null;
                        dgv_RelatoriosSalvos.DataSource = sorteddt2;

                        tb_CIRAtual.BackColor = Color.White;
                        tb_CIRAtual.ForeColor = Color.Red;
                        

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

                            RestablecerDGVdt2();

                            foreach (DataRow dr in dt.Rows)
                            {
                                object total;


                                if (array.IndexOf(dr["CIDE"].ToString().Substring(2)) < 0)

                                {

                                    total = dt.Compute(String.Format("SUM(Quantidade)"), "CIDE LIKE '%" + dr["CIDE"].ToString().Substring(2) + "'");

                                    dt2.Rows.Add(new object[] {  dr["NomePeça"], dr["CIP"], dr["CID"], Convert.ToInt32(total), Dtp_data.Value.Year, dr["Turno"], dr["Resp. Apontamento"], dr["Resp. Setor"] });

                                    array.Add(dr["CIDE"].ToString().Substring(2));

                                }

                            }

                            DataView dv = dt2.DefaultView;
                            dv.Sort = "CIP";
                            sorteddt2 = dv.ToTable();
                            dgv_RelatoriosSalvos.DataSource = null;
                            dgv_RelatoriosSalvos.DataSource = sorteddt2;
                            CIR = Turno+"-"+Dtp_data.Value.Year.ToString();
                            Resp_Setor = sorteddt2.Rows[0].Field<string>("Resp. Setor");
                            Resp_Apontamento = sorteddt2.Rows[0].Field<string>("Resp. Apontamento");
                            Subtitulo += " no  Ano de " + Dtp_data.Value.Year + " em " + TurnoString; ;
                            tb_CIRAtual.BackColor = Color.White;
                            tb_CIRAtual.ForeColor = Color.DarkGreen;
                            btn_Imprimir.Enabled = true;
                        }
                        else
                        {                           
                            Subtitulo = "Nehuma Peça foi Processada no  Ano de " + Dtp_data.Value.Year + " em " + TurnoString; ;
                            btn_Imprimir.Enabled = false;
                            dgv_RelatoriosSalvos.DataSource = null;
                            dgv_RelatoriosSalvos.DataSource = Base;
                            
                            tb_CIRAtual.BackColor = Color.White;
                            tb_CIRAtual.ForeColor = Color.Red;
                        

                        }
                    }
                    else
                    {
                        if (rb_Semana.Checked)
                        {
                            AssignarValorTurno();
                           
                            RestablecerDGVdt();

                            for (DateTime i = Dtp_data.Value; i <= Dta_fim.Value; i = i.AddDays(1))
                            {
                                temporal = new DataTable();
                                temporal = Banco.Procurar3Criterios(NomeTabla, "*", "CIR", "CIP", "CID", "'" + Turno + "-" + i.ToShortDateString() + "%'", "'" + cb_PeçaProcurada.SelectedValue + "%'", "'" + cb_DefeitoEspecifico.SelectedValue + "%'", "CIR");
                                dt.Merge(temporal, true);
                              
                    

                            }
                          
                            if (dt.Rows.Count > 0)
                            {
                               
                                ArrayList array = new ArrayList();

                                RestablecerDGVdt2();

                                foreach (DataRow dr in dt.Rows)

                                {

                                    object total;


                                    if (array.IndexOf(dr["CIDE"].ToString().Substring(2)) < 0)

                                    {

                                        total = dt.Compute(String.Format("SUM(Quantidade)"), "CIDE LIKE '%" + dr["CIDE"].ToString().Substring(2) + "'");

                                        dt2.Rows.Add(new object[] { dr["NomePeça"], dr["CIP"], dr["CID"], Convert.ToInt32(total), Dtp_data.Value.ToShortDateString() + "--" + Dta_fim.Value.ToShortDateString(), dr["Turno"], dr["Resp. Apontamento"], dr["Resp. Setor"] });

                                        array.Add(dr["CIDE"].ToString().Substring(2));

                                    }

                                }
                       
                                DataView dv = dt2.DefaultView;
                                dv.Sort = "CIP";
                                sorteddt2 = dv.ToTable();
                                dgv_RelatoriosSalvos.DataSource = null;
                                dgv_RelatoriosSalvos.DataSource = sorteddt2;
                                CIR = Turno +"-"+ Dtp_data.Value.ToShortDateString() + "--" + Dta_fim.Value.ToShortDateString();
                                Resp_Setor = sorteddt2.Rows[0].Field<string>("Resp. Setor");
                                Resp_Apontamento = sorteddt2.Rows[0].Field<string>("Resp. Apontamento");
                                Subtitulo += " na semana do dia " + Dtp_data.Value.ToShortDateString() + " ao dia " + Dta_fim.Value.ToShortDateString() + " em " + TurnoString; 
                                btn_Imprimir.Enabled = true;
                                tb_CIRAtual.BackColor = Color.White;
                                tb_CIRAtual.ForeColor = Color.DarkGreen;
                              
                            }
                            else
                            {                                
                                dgv_RelatoriosSalvos.DataSource = Base;
                                btn_Imprimir.Enabled = false;
                                tb_CIRAtual.BackColor = Color.White;
                                tb_CIRAtual.ForeColor = Color.Red;
                            }
                        }

                    }
                }
            }
            #endregion;
        }
        private void RestablecerDGVdt2()
        {
            dt2 = new DataTable();           
            dt2.Columns.Add(new DataColumn("NomePeça", typeof(string)));
            dt2.Columns.Add(new DataColumn("CIP", typeof(string)));
            dt2.Columns.Add(new DataColumn("CID", typeof(string)));
            dt2.Columns.Add(new DataColumn("Quantidade", typeof(Int64)));
            dt2.Columns.Add(new DataColumn("Data", typeof(string)));
            dt2.Columns.Add(new DataColumn("Turno", typeof(Int64)));
            dt2.Columns.Add(new DataColumn("Resp. Apontamento", typeof(string)));
            dt2.Columns.Add(new DataColumn("Resp. Setor", typeof(string)));

        }

        private void RestablecerDGVdt()
        {
            dt = new DataTable();
            dt.Columns.Add(new DataColumn("CIDE", typeof(string)));
            dt.Columns.Add(new DataColumn("NomePeça", typeof(string)));
            dt.Columns.Add(new DataColumn("CIP", typeof(string)));
            dt.Columns.Add(new DataColumn("CID", typeof(string)));
            dt.Columns.Add(new DataColumn("Quantidade", typeof(Int64)));
            dt.Columns.Add(new DataColumn("Data", typeof(string)));
            dt.Columns.Add(new DataColumn("Turno", typeof(Int64)));
            dt.Columns.Add(new DataColumn("Resp. Apontamento", typeof(string)));
            dt.Columns.Add(new DataColumn("Resp. Setor", typeof(string)));

        }
        private void AssignarValorInicialDGV()
        {
            AssignarValorTurno();
            tb_CIRAtual.Text = "TODOS";

            tb_CIRAtual.BackColor = Color.White;
            tb_CIRAtual.ForeColor = Color.DarkGreen;
            
            sorteddt2 = Banco.Procurar("RelatorioSaidaReforma_Retrabalho", "CIR, NomePeça, CIP, CID,Quantidade,Data,Turno, 'Resp. Apontamento', 'Resp. Setor'", "CIR", "'%'", "CIR");
            RestablecerDGVdt();
            RestablecerDGVdt2();
            dt = Banco.Procurar("RelatorioSaidaReforma_Retrabalho", "*", "CIR", "'%'", "CIR");

            dgv_RelatoriosSalvos.DataSource = sorteddt2; 

        }

        #endregion;



        #region Radiobuttons 
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_1.Checked && rb_Mes.Checked==false && rb_Ano.Checked==false && rb_Semana.Checked==false)
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
                tb_CIRAtual.Text = gb_Turno.Controls.OfType<RadioButton>().SingleOrDefault(RadioButton=> RadioButton.Checked).Text + "-" + Dtp_data.Value.ToShortDateString();
                lb_Data.Text = "Escolha um Dia";
            }
        }

        private void rb_Mes_CheckedChanged(object sender, EventArgs e)
        {

            if (rb_Mes.Checked)
            {
                Dtp_data.CustomFormat = "MMMM";
                Dtp_data.ShowUpDown = true;
                tb_CIRAtual.Text = gb_Turno.Controls.OfType<RadioButton>().SingleOrDefault(RadioButton => RadioButton.Checked).Text + "-" + Dtp_data.Value.Month.ToString()+"/"+ Dtp_data.Value.Year.ToString();
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
        #endregion;

        private void rb_Semana_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_Semana.Checked)
            {
                Dtp_data.CustomFormat = "dd/MM/yyyy";
                Dtp_data.ShowUpDown = false;
            
                DateTime datafim= new DateTime(Dtp_data.Value.Year, Dtp_data.Value.Month, Dtp_data.Value.Day);

            
                Dta_fim.Value = datafim.AddDays(7);
                tb_CIRAtual.Text = gb_Turno.Controls.OfType<RadioButton>().SingleOrDefault(RadioButton => RadioButton.Checked).Text + "-" + Dtp_data.Value.ToShortDateString() + ".."+Dta_fim.Value.ToShortDateString();
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
               
        int Control=0;
        private void cb_TipoRelatorio_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Control==2)
            {

                AssignarTablaProcurada();            
                AtualizarDatosDelDGV();
               
            }
            else
            {
                Control += 1;
            }
        }
       
        private void tb_CIRAtual_TextChanged(object sender, EventArgs e)
        {
                        
                AtualizarDatosDelDGV();
                    
         
           
        }
                
        int cont3 = 0;

        private void cb_FamiliaPeça_SelectedIndexChanged(object sender, EventArgs e)
        {
          
            if (cb_FamiliaPeça.Text != "")
            {
                cb_PeçaProcurada.Enabled = true;
            }
            else
            {
                cb_PeçaProcurada.SelectedValue = string.Empty;
                cb_PeçaProcurada.Enabled = false;
                
            }

            if (cont3 > 1 && cb_PeçaProcurada.Enabled == true)
            {

                DataTable dt = Banco.Procurar("Peças", "CIP, NomeCIP", "NomeCIP", "'" + cb_FamiliaPeça.Text + "%'", "CIP");
                
                if (dt.Rows.Count > 0)
                {
                    cb_PeçaProcurada.DataSource = dt;

                    AtualizarDatosDelDGV();
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

        int cont4=0;

        private void cb_FamiliaDefeitoProcurado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_FamiliaDefeitoProcurado.Text != "")
            {
                cb_DefeitoEspecifico.Enabled = true;
            }
            else
            {
                cb_DefeitoEspecifico.SelectedValue = string.Empty;
                cb_DefeitoEspecifico.Enabled = false;
               
            }

            if (cont4 > 1 && cb_DefeitoEspecifico.Enabled == true)
            {

                DataTable dt = Banco.Procurar("TiposDefeitos", "CID, NomeTipo", "NomeTipo", "'" + cb_FamiliaDefeitoProcurado.Text + "%'", "CID");
                
                if (dt.Rows.Count > 0)
                {
                    cb_DefeitoEspecifico.DataSource = dt;

                    AtualizarDatosDelDGV();

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

        int cont5=0;
        private void cb_PeçaProcurada_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cont5 > 1 && cb_PeçaProcurada.Enabled == true)
            {
                AtualizarDatosDelDGV();

            }
            else
            {
                cont5 += 1;
            }
        }
        int cont6 = 0;
        private void cb_DefeitoEspecifico_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cont6 > 1 && cb_DefeitoEspecifico.Enabled == true)
            {

                AtualizarDatosDelDGV();

            }
            else
            {
                cont6 += 1;
            }
        }

        private void Btn_Sair_Click(object sender, EventArgs e)
        {
            Globais.Sair();
        }

        private void Btn_Voltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void btn_Imprimir_Click(object sender, EventArgs e)
        {
            AssignarValorTurno();
            Relatorios.F_Relatorio _Relatorio = new Relatorios.F_Relatorio(sorteddt2,Titulo,Subtitulo,Turno.ToString(),Resp_Setor, Resp_Apontamento, CIR);
            Globais.Abreform(1,_Relatorio);
        }

        private void btn_VerGrafico_Click_1(object sender, EventArgs e)
        {
            F_GraficosReforma f_Graficos = new F_GraficosReforma();
            Globais.Abreform(1, f_Graficos);
        }
    }
}
