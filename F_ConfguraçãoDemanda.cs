using System;
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
    public partial class F_ConfguraçãoDemanda : Form
    {
        public F_ConfguraçãoDemanda()
        {
            InitializeComponent();
        }

        #region VARIAVEIS
        bool Control = false;
        Demanda D = new Demanda();
        DataTable dt = new DataTable();
        DataTable Demanda = new DataTable();
        DataTable DemandaDGV = new DataTable();
        Fogão F = new Fogão();
        int VdP;
        int VdC;
        string NomeCIP;
        string CICG;
        int TC;
        string IdSeleccionados;
        string IdSeleccionados2;
        bool mudou = false;
        int[] IndicedePontos = new int[3];
        int[] IndiceFMCV = new int[4];
        bool cargou=false;
        #endregion;

        private void F_ConfguraçãoDemanda_Load(object sender, EventArgs e)
        {
            #region Popular ComboBoxes
            #region NomeCIP
            //Popular combobox Nome CIP
            cb_Peça.Items.Clear();
            cb_Peça.DataSource = Banco.ObterTodos("peças", "*", "CIP"); ;
            cb_Peça.DisplayMember = "CIP";
            cb_Peça.ValueMember = "NomeCIP";
            #endregion;

            #region CICG
            //Popular combobox Nome CIP
            tb_CICG.Items.Clear();
            tb_CICG.DataSource = Banco.ObterTodos("CadastroDeCapacidadeGeral", "*", "CICG"); ;
            tb_CICG.DisplayMember = "CICG";
            tb_CICG.ValueMember = "CICG";
            #endregion;

            #region Clientes
            cb_Clientes.Items.Clear();
            cb_Clientes.DataSource = Banco.ObterTodos("Clientes", "*", "Id");
            cb_Clientes.DisplayMember = "Nome";
            cb_Clientes.ValueMember = "Id";

            cb_ClienteTMC.Items.Clear();
            cb_ClienteTMC.DataSource = Banco.ObterTodos("Clientes", "*", "Id");
            cb_ClienteTMC.DisplayMember = "Nome";
            cb_ClienteTMC.ValueMember = "Id";

            cb_ClienteCapaciadade.Items.Clear();
            cb_ClienteCapaciadade.DataSource = Banco.ObterTodos("Clientes", "*", "Id");
            cb_ClienteCapaciadade.DisplayMember = "Nome";
            cb_ClienteCapaciadade.ValueMember = "Id";

            cb_ClientePeças.Items.Clear();
            cb_ClientePeças.DataSource = Banco.ObterTodos("Clientes", "*", "Id");
            cb_ClientePeças.DisplayMember = "Nome";
            cb_ClientePeças.ValueMember = "Id";


            #endregion;

            #region Fogão
            cb_Fogão.Items.Clear();
            cb_Fogão.DataSource = Banco.ObterTodos("Fogão", "*", "Id");
            cb_Fogão.DisplayMember = "NomeFogão";
            cb_Fogão.ValueMember = "Id";
            #endregion;

            #region Horarios

            cb_Horario.Items.Clear();
            cb_Horario.DataSource = Banco.Procurar("Horarios", "*", "Turno", "'1%'", "Id");
            cb_Horario.DisplayMember = "Horario";
            cb_Horario.ValueMember = "Id";


            #region DemandaCapacidade
            cb_HorarioDemandaCapacidade.Items.Clear();
            cb_HorarioDemandaCapacidade.DataSource = Banco.Procurar("Horarios", "*", "Turno", "'1%'", "Id");
            cb_HorarioDemandaCapacidade.DisplayMember = "Horario";
            cb_HorarioDemandaCapacidade.ValueMember = "Id";
            #endregion;

            #region DemandaPeças
            cb_HorarioDemandaPeças.Items.Clear();
            cb_HorarioDemandaPeças.DataSource = Banco.Procurar("Horarios", "*", "Turno", "'1%'", "Id");
            cb_HorarioDemandaPeças.DisplayMember = "Horario";
            cb_HorarioDemandaPeças.ValueMember = "Id";
            #endregion;

            #region TMC
            cb_HorarioTMC.Items.Clear();
            cb_HorarioTMC.DataSource = Banco.Procurar("Horarios", "*", "Turno", "'1%'", "Id");
            cb_HorarioTMC.DisplayMember = "Horario";
            cb_HorarioTMC.ValueMember = "Id";
            #endregion;


            #endregion;

            #region Turnos
            Dictionary<string, string> Turno = new Dictionary<string, string>();

            Turno.Add("1", "1");
            Turno.Add("2", "2");
            Turno.Add("3", "3");

            cb_Turno.Items.Clear();
            cb_Turno.DataSource = new BindingSource(Turno, null);
            cb_Turno.DisplayMember = "Value";
            cb_Turno.ValueMember = "key";

            cb_TDemandaCapacidade.Items.Clear();
            cb_TDemandaCapacidade.DataSource = new BindingSource(Turno, null);
            cb_TDemandaCapacidade.DisplayMember = "Value";
            cb_TDemandaCapacidade.ValueMember = "key";

            cb_TDemandaPeças.Items.Clear();
            cb_TDemandaPeças.DataSource = new BindingSource(Turno, null);
            cb_TDemandaPeças.DisplayMember = "Value";
            cb_TDemandaPeças.ValueMember = "key";

            cb_Ttmc.Items.Clear();
            cb_Ttmc.DataSource = new BindingSource(Turno, null);
            cb_Ttmc.DisplayMember = "Value";
            cb_Ttmc.ValueMember = "key";

            #endregion;

            #region Caracteristica
            cb_Caracteristica.Items.Clear();
            cb_Caracteristica.DataSource = Banco.Procurar("Caracteristicas", "*", "Tipo", "'%F%'", "IdCaracteristica");
            cb_Caracteristica.DisplayMember = "Caracteristica";
            cb_Caracteristica.ValueMember = "IdCaracteristica";
            #endregion;
            #endregion;


            #region PrencherGraficos

            dt = Banco.Procurar("Demanda", "*", "IdDemanda", "'%"+dtp_Data.Value.ToShortDateString()+"%'", "Cliente, Horario, NomeCIP");
            
            PrencherGrafico(ProGerais.Contar(dt,"VdC","NomeCIP","CICG","VdC","NomeCIP"), G_DemandaContainer,"Demanda-Contêiner", 1,"VdC", "VelocidadeDeConteiner" );

            PrencherGrafico(dt, G_DemandaPeças, "Demanda-Peças", 2, "VdP", "VelocidadeDePeças");

            PrencherGrafico(dt, G_TMC, "Tempo Medio de Consumo", 1, "TMC", "TeMeCo");

            #endregion;

            #region Popular DGV
            dgv_DemandaGeral.DataSource = Banco.Procurar("Demanda", "IdDemanda, Cliente, Horario, NomeCIP, VdP,VdC,TMC", "IdDemanda", "'%'", "Cliente, Horario, NomeCIP");
            ConfigDGV();

            #endregion;

            #region Confg Gerais
            CriarDataTableDemanda();
            #endregion;
            cargou= true;
        }

        #region PROCEDIMENTOS



        private void PrencherGrafico(DataTable Dados,Chart G, string NomeGrafico, int modoX, string NomeY, string NomeSerie)
        {
            string x;
            G.Series.Clear();
            G.Titles.Clear();

            Title title = new Title();
            title.Font = new Font("Arial", 12, FontStyle.Bold);
            title.ForeColor = Color.Green;            
            title.Text = "Grafico "+NomeGrafico;
            G.Titles.Add(title);           
            G.Series.Add(NomeSerie);
            
            foreach (DataRow dr in Dados.Rows)
            {
                if (modoX==1)
                {
                    x= dr["CICG"] + "--" + dr["NomeCIP"];
                }
                else
                {
                    x = (string)dr["NomeCIP"];
                }
                
                Int64 y = Convert.ToInt64(dr[NomeY]);

                G.Series[NomeSerie].ChartType = SeriesChartType.Column;
                G.Series[NomeSerie].BorderWidth = 4;

                G.Series[NomeSerie].Points.AddXY(x, y);

                G.Series[NomeSerie].IsValueShownAsLabel = true;
                G.Series[NomeSerie].LabelAngle = 90;

            }



            G.Series[0].SmartLabelStyle.Enabled = false;
            foreach (DataPoint dp in G.Series[0].Points)
            {
                dp.LabelAngle = -90;
            }
            G.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
            G.ChartAreas[0].AxisX.LabelStyle.Angle = 90;
            G.ChartAreas[0].AxisX.Interval = 1;


        }
        private void ConfigDGV()
        {
            /*  dgv_DemandaGeral.Columns[0].Width = 60;
              dgv_DemandaGeral.Columns[1].Width = 120;
              dgv_DemandaGeral.Columns[2].Width = 100;
              dgv_DemandaGeral.Columns[3].Width = 260;
              dgv_DemandaGeral.Columns[4].Width = 50;
              dgv_DemandaGeral.Columns[5].Width = 50;
              dgv_DemandaGeral.Columns[6].Width = 30;
              */
        }

        private void CriarDemanda()
        {
            D.IdDemanda = tb_IdDemanda.Text;
            D.Cliente = cb_Clientes.Text;
            D.Data = dtp_Data.Value.ToShortDateString();
            D.NomeFogão = cb_Fogão.Text;
            D.Caracteristica = cb_Caracteristica.SelectedIndex.ToString();
            D.Horario = cb_Horario.Text;
            D.Qf = nud_Qf.Value.ToString();
            D.Turno = cb_Turno.SelectedValue.ToString();
            D.CICG = tb_CICG.Text;
            D.VdP = Convert.ToInt32(dt.Rows[0].Field<Int64>("VdP"));
            D.VdC = Convert.ToInt32(dt.Rows[0].Field<Int64>("VdC"));
            D.TMC = Convert.ToInt32(dt.Rows[0].Field<Int64>("TMC"));
        }

    
        private void CriarDemanda(DataTable dt)
        {
            DataTable dt2 = Banco.Procurar("Horarios", "*", "Horario", "'" + dt.Rows[0].Field<string>("Horario") + "'", "Turno");
            D.IdDemanda = dt.Rows[0].Field<string>("IdDemanda");
            D.Cliente = dt.Rows[0].Field<string>("Cliente");
            D.Data = dt.Rows[0].Field<string>("Data");
            D.NomeFogão = dt.Rows[0].Field<string>("NomeFogão");
            D.Caracteristica = dt.Rows[0].Field<string>("Caracteristica");
            D.Horario = dt.Rows[0].Field<string>("Horario");
            D.Qf = dt.Rows[0].Field<string>("Qf");
            D.Turno = dt2.Rows[0].Field<string>("Turno");
            if (dt.Rows[0].Field<string>("CICG") != "")
            {
                D.CICG = dt.Rows[0].Field<string>("CICG");
            }
            else
            {
                D.CICG = 1234.ToString();
            }
            D.NomeCIP = dt.Rows[0].Field<string>("NomeCIP");
            D.VdP = Convert.ToInt32(dt.Rows[0].Field<Int64>("VdP"));
            D.VdC = Convert.ToInt32(dt.Rows[0].Field<Int64>("VdC"));
            D.TMC = Convert.ToInt32(dt.Rows[0].Field<Int64>("TMC"));
        }
        private void CriarDemanda(DataRow dr)
        {

            D.IdDemanda = dr.Field<string>("IdDemanda");
            D.Cliente = dr.Field<string>("Cliente");
            D.Data = dr.Field<string>("Data");
            D.NomeFogão = dr.Field<string>("NomeFogão");
            D.Caracteristica = dr.Field<string>("Caracteristica");
            D.Horario = dr.Field<string>("Horario");
            D.Qf = dr.Field<string>("Qf");
            D.NomeCIP = dr.Field<string>("NomeCIP");
            D.CICG = dr.Field<string>("CICG");
            D.VdP = Convert.ToInt32(dr.Field<Int64>("VdP"));
            D.VdC = Convert.ToInt32(dr.Field<Int64>("VdC"));
            D.TMC = Convert.ToInt32(dr.Field<Int64>("TaxaDeConsumo"));
        }


        private void AssignarDemandaaosControles()
        {
            cb_Peça.Enabled = true;


            tb_IdDemanda.Text = D.IdDemanda;
            cb_Clientes.Text = D.Cliente;
            cb_Fogão.Text = D.NomeFogão;
            cb_Caracteristica.SelectedIndex = int.Parse(D.Caracteristica);
            nud_Qf.Value = int.Parse(D.Qf);
            cb_Turno.Text = D.Turno;
            cb_Horario.Text = D.Horario;
            dtp_Data.Text = D.Data;
            tb_CICG.SelectedValue = D.CICG;
            cb_Peça.SelectedValue = D.NomeCIP;
            nud_VdP.Value = D.VdP;
            nud_VdC.Value = D.VdC;
            nud_TC.Value = D.TMC;

        }
        private void CriarDataTableDemanda()
        {
            Demanda = new DataTable();
            Demanda.Columns.Add("IdDemanda");
            Demanda.Columns.Add("Cliente");
            Demanda.Columns.Add("NomeFogão");
            Demanda.Columns.Add("Caracteristica");
            Demanda.Columns.Add("Horario");
            Demanda.Columns.Add("Data");
            Demanda.Columns.Add("NomeCIP");
            Demanda.Columns.Add("VdP");
            Demanda.Columns.Add("VdC");
            Demanda.Columns.Add("CICG");
            Demanda.Columns.Add("Qf");
            Demanda.Columns.Add("TaxaDeConsumo");

        }
        private void CriarDataTableDemandaDGV()
        {
            DemandaDGV = new DataTable();
            DemandaDGV.Columns.Add("IdDemanda");
            DemandaDGV.Columns.Add("Cliente");
            DemandaDGV.Columns.Add("Horario");
            DemandaDGV.Columns.Add("NomeCIP");
            DemandaDGV.Columns.Add("VdP");
            DemandaDGV.Columns.Add("VdC");
            DemandaDGV.Columns.Add("TMC");


        }

        private void AtualizarSelección()
        {
            IdSeleccionados = dgv_DemandaGeral.Rows[dgv_DemandaGeral.SelectedRows[0].Index].Cells[0].Value.ToString();
            IdSeleccionados2 = dgv_DemandaGeral.Rows[dgv_DemandaGeral.SelectedRows[0].Index].Cells[3].Value.ToString();
            //    MessageBox.Show("IdDemanda= " + IdSeleccionados +"\n\nNomeCIP= " + IdSeleccionados2);

            dt = Banco.Procurar2Criterios("Demanda", "*", "IdDemanda", "NomeCIP", "'" + IdSeleccionados + "%'", "'" + IdSeleccionados2 + "'", "IdDemanda");

            //   MessageBox.Show(dt.Rows.Count.ToString());
            if (dt.Rows.Count > 0)
            {
                CriarDemanda(dt);
                AssignarDemandaaosControles();

            }

        }
        private void CriarFogão(DataTable dt, DataTable dt2)
        {
            F.Id = dt.Rows[0].Field<Int64>("Id").ToString();
            F.NomeFogão = dt.Rows[0].Field<string>("NomeFogão");
            F.Caracteristica = dt.Rows[0].Field<string>("Caracteristica");
            F.ModeloDasPeças = dt.Rows[0].Field<string>("ModeloDasPeças");

            F.Bandeja = dt.Rows[0].Field<string>("Bandeja");
            F.Costa = int.Parse(dt.Rows[0].Field<string>("Costa"));
            F.Difusor = int.Parse(dt.Rows[0].Field<string>("Difusor"));
            F.Lateral = int.Parse(dt.Rows[0].Field<string>("Lateral"));
            F.Porta = int.Parse(dt.Rows[0].Field<string>("Porta"));
            F.QuadroFrontal = int.Parse(dt.Rows[0].Field<string>("QuadroFrontal"));
            F.Teto = int.Parse(dt.Rows[0].Field<string>("Teto"));
            F.Vedação = int.Parse(dt.Rows[0].Field<string>("Vedação"));
            F.Mesa = int.Parse(dt.Rows[0].Field<string>("Mesa"));
            F.Bases = dt2;



        }
        private void CalcularCapacidadeVdCVdPeTC(string Nome2, string CICG)
        {
            DataTable dtCap;
            decimal Capacidade;
            dtCap = Banco.Procurar("CadastroEspecificoContenedores", "CICG,NomeCIC, CapacidadeEsperada", "NomeCIC", "'%" + CICG + "%'", "CapacidadeEsperada");

            if (dtCap.Rows.Count > 0)
            {
                Capacidade = dtCap.Rows[0].Field<Int64>("CapacidadeEsperada");
                VdC = Convert.ToInt32(Math.Ceiling(VdP / Capacidade));
                TC = Convert.ToInt32(Math.Floor((Capacidade * 60) / VdP));
                this.CICG = dtCap.Rows[0].Field<string>("CICG");
                MessageBox.Show("EntrouC1");
            }
            else
            {
                dtCap = Banco.Procurar("CadastroEspecificoContenedores", "CICG,NomeCIC, CapacidadeEsperada", "NomeCIC", "'%" + Nome2 + " " + cb_Caracteristica.Text + "%'", "CapacidadeEsperada");

                if (dtCap.Rows.Count > 0)
                {
                    Capacidade = dtCap.Rows[0].Field<Int64>("CapacidadeEsperada");
                    VdC = Convert.ToInt32(Math.Ceiling(VdP / Capacidade));
                    TC = Convert.ToInt32(Math.Floor((Capacidade * 60) / VdP));
                    this.CICG = dtCap.Rows[0].Field<string>("CICG");
                    MessageBox.Show("EntrouC2");
                }
                else
                {
                    dtCap = Banco.Procurar("CadastroGeralContenedores", "CICG,Capacidade", "CICG", "'% PD-0.0.0%'", "CICG");

                    if (dtCap.Rows.Count > 0)
                    {
                        Capacidade = int.Parse(dtCap.Rows[0].Field<string>("Capacidade"));
                        VdC = Convert.ToInt32(Math.Ceiling(VdP / Capacidade));
                        TC = Convert.ToInt32(Math.Floor((Capacidade * 60) / VdP));
                        this.CICG = "PD-0.0.0";
                        MessageBox.Show("EntrouC3");
                    }
                    else
                    {
                        VdC = 0;
                        TC = 0;
                        this.CICG = "";
                        MessageBox.Show("EntrouC4");
                        MessageBox.Show("Não há nenhum contêiner cadastrado para a peça: " + CICG, "Contêiner Não cadastrado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void CalcularCapacidadeVdCVdPeTC(string CICG, int Value, string CIP)
        {
            DataTable dtCap;
            decimal Capacidade;                     
            
            
            dtCap = Banco.Procurar("CadastroDeCapacidadeGeral", "CICG, Capacidade", "CICG", "'%" + CICG + "%'", "CICG");
            nud_VdP.Value = VdP = Convert.ToInt32(nud_Qf.Value) * Value;
            if (dtCap.Rows.Count > 0)
            {
                Capacidade = int.Parse(dtCap.Rows[0].Field<string>("Capacidade"));
                VdC = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(VdP / Capacidade)));
                TC = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal((Capacidade * 60) / VdP)));
            }
            else
            {

                do
                {
                    MessageBox.Show("Não há nenhum contêiner cadastrado para a peça: " + tb_CICG.Text + ", Deve fazer o cadastro para poder continuar, sera dirigido à página de cadastro", "Capacidade não Cadastrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    EstablecerValorIndicesFMCV(CIP);
                    F_CalculoDeCMAA Calculo = new F_CalculoDeCMAA(new F_CadastroEControleDeContenedores(), CIP, IndiceFMCV[0]);
                    Globais.Abreform(1, Calculo);
                    dtCap = Banco.Procurar("CadastroDeCapacidadeGeral", "CICG, Capacidade", "CICG", "'%" + CICG + "%'", "CICG");

                    if (dtCap.Rows.Count > 0)
                    {
                        Capacidade = int.Parse(dtCap.Rows[0].Field<string>("Capacidade"));
                        VdC = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(VdP / Capacidade)));
                        TC = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal((Capacidade * 60) / VdP)));
                    }

                } while (dtCap.Rows.Count <= 0);

            }


        }
        private void EstablecerIndicesPontosCIP(string CIP)
        {
            string Buscado = ".";
            string c;

            int m = 0;

            for (int i = 0; i < CIP.Length; i++)
            {
                c = CIP.Substring(i, 1);
                if (c == Buscado)
                {

                    IndicedePontos[m] = i;
                    m += 1;

                }
            }

        }
        private void EstablecerValorIndicesFMCV(string CIP)
        {
            EstablecerIndicesPontosCIP(CIP);


            IndiceFMCV[0] = int.Parse(CIP.Substring(0, IndicedePontos[0]));

            IndiceFMCV[1] = int.Parse(CIP.Substring(IndicedePontos[0] + 1, IndicedePontos[1] - (IndicedePontos[0] + 1)));

            IndiceFMCV[2] = int.Parse(CIP.Substring(IndicedePontos[1] + 1, IndicedePontos[2] - (IndicedePontos[1] + 1)));

          
        }
        private bool CalcularCapacidadeVdCVdPeTC(string CICG, DataTable Bases, int i)
        {
            DataTable dtCap;
            decimal Capacidade;

            if (Bases.Rows.Count > 0)
            {

                VdP = Convert.ToInt32(nud_Qf.Value) * int.Parse(F.Bases.Rows[i].Field<string>("Quantidade"));     

                dtCap = Banco.Procurar("CadastroDeCapacidadeGeral", "CICG, Capacidade", "CICG", "'%" + CICG + "%'", "CICG");
             
                if (dtCap.Rows.Count > 0)
                {
                    Capacidade = int.Parse(dtCap.Rows[0].Field<string>("Capacidade"));
                    VdC = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(VdP / Capacidade)));
                    TC = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal((Capacidade * 60) / VdP)));
                    return true;
                }
                else
                {

                    do
                    {
                        MessageBox.Show("Não há nenhum contêiner cadastrado para a peça: " + NomeCIP + ", Deve fazer o cadastro para poder continuar, sera dirigido à página de cadastro", "Capacidade não Cadastrada", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        EstablecerValorIndicesFMCV(F.Bases.Rows[i].Field<string>("CIP"));
                        F_CalculoDeCMAA Calculo = new F_CalculoDeCMAA(new F_CadastroEControleDeContenedores(), F.Bases.Rows[i].Field<string>("CIP"), IndiceFMCV[0]);
                        Globais.Abreform(1, Calculo);
                        dtCap = Banco.Procurar("CadastroDeCapacidadeGeral", "CICG, Capacidade", "CICG", "'%" + CICG + "%'", "CICG");

                        if (dtCap.Rows.Count > 0)
                        {
                            Capacidade = int.Parse(dtCap.Rows[0].Field<string>("Capacidade"));
                            VdC = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(VdP / Capacidade)));
                            TC = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal((Capacidade * 60) / VdP)));
                        }

                    } while (dtCap.Rows.Count <= 0);
                    return true;
                }

            }
            else
            {
                return false;
            }




        }
        private bool AssignarNomeCIPCapacidadeVdcVdPTC(int Quantia, string Nome2)
        {


            if (Quantia > 0)
            {
                if (F.ModeloDasPeças == "FORNO ELETRICO" && (Nome2 == "LATERAL" || Nome2 == "LATERAL2"))
                {
                    VdP = Convert.ToInt32(nud_Qf.Value);
                }
                else
                {
                    VdP = Convert.ToInt32(nud_Qf.Value) * Quantia;
                }

                if (cb_Caracteristica.Text == "N/A")
                {
                    if (F.ModeloDasPeças == "FORNO ELETRICO" && Nome2 == "LATERAL2")
                    {
                        NomeCIP = "LATERAL FORNO ELETRICO ESQUERDA";
                    }
                    else
                    {
                        if (F.ModeloDasPeças == "FORNO ELETRICO" && Nome2 == "LATERAL")
                        {
                            NomeCIP = "LATERAL FORNO ELETRICO DIREITA";
                        }
                        else
                        {
                            NomeCIP = Nome2 + " " + F.ModeloDasPeças;
                        }
                    }
                }
                else
                {
                    if (Nome2 == "LATERAL")
                    {
                        NomeCIP = Nome2 + " " + F.ModeloDasPeças;
                    }
                    else
                    {
                        if (F.ModeloDasPeças == "BBB" && Nome2 == "PORTA")
                        {
                            NomeCIP = "PORTA ÁGILE " + cb_Caracteristica.Text;
                        }
                        else
                        {
                            if (F.ModeloDasPeças == "BBB" && Nome2 == "BANDEJA" && cb_Caracteristica.Text == "5Q")
                            {
                                NomeCIP = "BANDEJA ÁGILE " + cb_Caracteristica.Text;
                            }
                            else
                            {
                                NomeCIP = Nome2 + " " + F.ModeloDasPeças + " " + cb_Caracteristica.Text + "";
                            }

                        }
                    }

                }
                string CIP; 

                DataTable dt = Banco.Procurar("Peças","*","NomeCIP","'"+NomeCIP+"%'","CIP");
                
                CIP = dt.Rows[0].Field<string>("CIP");

                DataTable dt2 = Banco.Procurar("CadastroDeCapacidadeGeral", "*","CIP","'"+CIP+"%'","CICG");
                if (dt2.Rows.Count<=0)
                {
                    do
                    {
                        MessageBox.Show("Não há nenhum contêiner cadastrado para a peça: " + NomeCIP + ", Deve fazer o cadastro para poder continuar, sera dirigido à página de cadastro", "Capacidade não Cadastrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        EstablecerValorIndicesFMCV(CIP);
                        F_CalculoDeCMAA Calculo = new F_CalculoDeCMAA(new F_CadastroEControleDeContenedores(), cb_Peça.Text, IndiceFMCV[0]);
                        Globais.Abreform(1, Calculo);
                        dt2 = Banco.Procurar("CadastroDeCapacidadeGeral", "*", "CIP", "'" + CIP + "%'", "CICG");
                    } while (dt2.Rows.Count <= 0);
                }
                CICG = dt2.Rows[0].Field<string>("CICG");
                CalcularCapacidadeVdCVdPeTC(CICG, Quantia, CIP);
               
                return true;
            }
            else
            {
                if (F.Costa <= 0)
                {
                    return false;
                }
            }
            return false;
        }

        private void CalcularValoresCap(string CIP)
        {
            DataTable dtCap;
            decimal Capacidade;
            dtCap = Banco.Procurar("CadastroDeCapacidadeGeral", "CICG, Capacidade", "CIP", "'%" + CIP + "%'", "CICG");

            if (dtCap.Rows.Count > 0)
            {
                CICG = dtCap.Rows[0].Field<string>("CICG");
                Capacidade = int.Parse(dtCap.Rows[0].Field<string>("Capacidade"));
                VdC = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(VdP / Capacidade)));
                TC = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal((Capacidade * 60) / VdP)));
            }
            else
            {

                do
                {
                    MessageBox.Show("Não há nenhum contêiner cadastrado para a peça: " + CIP+ ", Deve fazer o cadastro para poder continuar, sera dirigido à página de cadastro", "Capacidade não Cadastrada", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    EstablecerValorIndicesFMCV(CIP);
                    F_CalculoDeCMAA Calculo = new F_CalculoDeCMAA(new F_CadastroEControleDeContenedores(), CIP, IndiceFMCV[0]);
                    Globais.Abreform(1, Calculo);
                    dtCap = Banco.Procurar("CadastroDeCapacidadeGeral", "CICG, Capacidade", "CIP", "'%" + CIP + "%'", "CICG");

                    if (dtCap.Rows.Count > 0)
                    {
                        CICG = dtCap.Rows[0].Field<string>("CICG");
                        Capacidade = int.Parse(dtCap.Rows[0].Field<string>("Capacidade"));
                        VdC = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(VdP / Capacidade)));
                        TC = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal((Capacidade * 60) / VdP)));
                    }

                } while (dtCap.Rows.Count <= 0);

            }
        }
        private bool DeterminaNomeCICGeCalculoDeVdPeVdC(int L, DataTable dt2, int i)
        {
      
          
           
            #region case
            switch (L)
            {
                case 1:

                    if (F.Bandeja == "1A" || F.Bandeja == "1B")
                    {
                  
                        if (F.Bandeja == "1A")
                        {
                            VdP = Convert.ToInt32(nud_Qf.Value) * int.Parse(F.Bandeja.Substring(0,1));//areglar salida del nombre de la bandeja no esta saliendo
                            if (F.ModeloDasPeças == "BBB" && cb_Caracteristica.Text == "5Q")
                            {
                                NomeCIP = "BANDEJA ÁGILE " + cb_Caracteristica.Text;
                            }
                            else
                            {
                                if (F.ModeloDasPeças == "FORNO ELETRICO")
                                {
                                    NomeCIP = "BANDEJA " + F.ModeloDasPeças;
                                }
                                else
                                {
                                    NomeCIP = "BANDEJA " + F.ModeloDasPeças + " " + cb_Caracteristica.Text + "";
                                }
                             
                            }
                            
                            DataTable dt = Banco.Procurar("Peças", "*", "NomeCIP", "'" + NomeCIP + "%'", "CIP");
                            CalcularValoresCap(dt.Rows[0].Field<string>("CIP"));
                        }
                        else
                        {
                            VdP = Convert.ToInt32(nud_Qf.Value) * int.Parse(F.Bandeja.Substring(0, 1));
                            NomeCIP = "BANDEJA " + F.ModeloDasPeças + " " + cb_Caracteristica.Text + "";
                            DataTable dt = Banco.Procurar("Peças", "*", "NomeCIP", "'" + NomeCIP + "%'", "CIP");
                            CalcularValoresCap(dt.Rows[0].Field<string>("CIP"));
                        }
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case 2:
                    if (dt2.Rows.Count>0)
                    {                        
                            VdP = Convert.ToInt32(nud_Qf.Value) * int.Parse(F.Bases.Rows[i].Field<string>("Quantidade"));
                            NomeCIP = F.Bases.Rows[i].Field<string>("Base");

                        
                        DataTable dt = Banco.Procurar("CadastroDeCapacidadeGeral","*","CIP","'"+ F.Bases.Rows[i].Field<string>("CIP") + "'","CICG");
                        if (dt.Rows.Count>0)
                        {
                            CICG = dt.Rows[0].Field<string>("CICG");

                            CalcularCapacidadeVdCVdPeTC(CICG, F.Bases, i);

                        }
                        else
                        {
                            do
                            {
                                MessageBox.Show("Não há nenhum contêiner cadastrado para a peça: " + tb_CICG.Text + ", Deve fazer o cadastro para poder continuar, sera dirigido à página de cadastro", "Capacidade não Cadastrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                EstablecerValorIndicesFMCV(F.Bases.Rows[i].Field<string>("CIP"));
                                F_CalculoDeCMAA Calculo = new F_CalculoDeCMAA(new F_CadastroEControleDeContenedores(), F.Bases.Rows[i].Field<string>("CIP"), IndiceFMCV[0]);
                                Globais.Abreform(1, Calculo);
                                 dt = Banco.Procurar("CadastroDeCapacidadeGeral", "*", "CIP", "'" + F.Bases.Rows[i].Field<string>("CIP") + "'", "CICG");

                                if (dt.Rows.Count > 0)
                                {
                                    CICG = dt.Rows[0].Field<string>("CICG");

                                    CalcularCapacidadeVdCVdPeTC(CICG, F.Bases, i);
                                }

                            } while (dt.Rows.Count <= 0);

                        }
               
                       
                        
                      
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("0f");
                        return false;
                       
                    }           
                   
                case 3:                    
                    
                    return AssignarNomeCIPCapacidadeVdcVdPTC(F.Costa, "COSTA");


                case 4:

                    return AssignarNomeCIPCapacidadeVdcVdPTC(F.Difusor, "DIFUSOR");
                    
                        
                case 5:
                    
                    return AssignarNomeCIPCapacidadeVdcVdPTC(F.Lateral, "LATERAL");

                case 6:

                    if (F.ModeloDasPeças=="FORNO ELETRICO") 
                    {
                        return AssignarNomeCIPCapacidadeVdcVdPTC(F.Lateral, "LATERAL2");
                    }
                    else
                    {
                        return false;
                    }


                case 7:

                    return AssignarNomeCIPCapacidadeVdcVdPTC(F.Porta, "PORTA");

                case 8:

                    return AssignarNomeCIPCapacidadeVdcVdPTC(F.QuadroFrontal, "QUADRO FRONTAL");
                   
                case 9:

                    return AssignarNomeCIPCapacidadeVdcVdPTC(F.Teto, "TETO");
                   
                case 10:

                    return AssignarNomeCIPCapacidadeVdcVdPTC(F.Vedação, "VEDAÇÃO");
                   
                case 11:

                    return AssignarNomeCIPCapacidadeVdcVdPTC(F.Mesa, "MESA");                    

                default:
                    return false;
              




            }
          





            #endregion;
        }
        private void RestablecerNovo()
        {            
            btn_Salvar.Text = "Atualizar";
            btn_Novo.Text = "Novo";
            btn_Excluir.Enabled = true;
            btn_add.Enabled = false;
            btn_Excluir2.Enabled = false;
            Control = false;
            cb_Fogão.Enabled = false;
            cb_Caracteristica.Enabled = false;
            gb_DataEHorario.Enabled = false;
        }

        #endregion;

        #region BTN CLICK
        private void btn_Excluir_Click(object sender, EventArgs e)
        {
            Banco.Excluir("Demanda", "IdDemanda", "'" + tb_IdDemanda.Text + "'");
            dgv_DemandaGeral.DataSource = Banco.Procurar("Demanda", "IdDemanda, Cliente, Horario, NomeCIP, VdP,VdC,TMC", "IdDemanda", "'%'", "Cliente, Horario, NomeCIP");
        }

        private void btn_Novo_Click(object sender, EventArgs e)
        {
            if (btn_Novo.Text == "Novo")
            {
                btn_add.Enabled = true;
                btn_Excluir2.Enabled = true;
                cb_Fogão.Enabled = true;
                cb_Caracteristica.Enabled = true;
                gb_DataEHorario.Enabled = true;
                btn_Novo.Text = "Cancelar";
                btn_Salvar.Text = "Salvar";



                if (!Control)
                {
                    CriarDataTableDemandaDGV();
                    dgv_DemandaGeral.DataSource = DemandaDGV;
                    Control = true;
                    cb_Turno.SelectedValue = 1235;
                    cb_Horario.SelectedValue = 10000;
                    cb_Caracteristica.SelectedValue = 1000;
                    cb_Clientes.SelectedIndex = 0;
                    cb_Peça.SelectedValue = 1234;
                    nud_TC.Value = 0;
                    nud_VdC.Value = 0;
                    nud_VdP.Value = 0;
                    tb_CICG.Text = "";
                    cb_Fogão.SelectedValue = 1235;
                    nud_Qf.Value = 0;
                    dtp_Data.Value = DateTime.Today;
                    btn_add.Enabled = true;
                    btn_Excluir2.Enabled = true;
                    btn_Novo.Text = "Cancelar";
                    btn_Salvar.Text = "Salvar";
                    tb_IdDemanda.Text = "";
                    btn_Excluir.Enabled = false;

                }

            }
            else
            {
                if (btn_Novo.Text == "Cancelar")
                {
                    dgv_DemandaGeral.DataSource = Banco.Procurar("Demanda", "IdDemanda, Cliente, Horario, NomeCIP, VdP,VdC, TMC", "IdDemanda", "'%'", "Cliente, Horario, NomeCIP");
                    ConfigDGV();
                    RestablecerNovo();
                }

            }

        }

        private void btn_Salvar_Click(object sender, EventArgs e)
        {
            int linha;

            if (btn_Salvar.Text == "Atualizar")
            {
                DialogResult res = MessageBox.Show("Quer mesmo Atualizar os dados?", "Quer atualizar?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {

                    CriarDemanda();
                    bool Atualizou;
                    dt = Banco.ObterTodosOnde("Demanda", "IdDemanda", "'" + tb_IdDemanda.Text + "'");

                    if (dt.Rows.Count > 0)
                    {
                        try
                        {
                            linha = dgv_DemandaGeral.SelectedRows[0].Index;
                            MessageBox.Show("Cliente= '" + D.Cliente + "', VdP= '" + D.VdP + "', VdC= '" + D.VdC + "', CICG= '" + D.CICG + "', Qf= '" + D.Qf + "', TMC= '" + D.TMC + "'" + "iDdEMANDA= " + tb_IdDemanda.Text + " NomeCIP= " + D.NomeCIP);
                            Atualizou = Banco.Atualizar("Demanda", "Cliente= '" + D.Cliente + "', VdP= '" + D.VdP + "', VdC= '" + D.VdC + "', CICG= '" + D.CICG + "', Qf= '" + D.Qf + "', TMC= '" + D.TMC + "'", "IdDemanda", "NomeCIP", "'" + tb_IdDemanda.Text + "'", "'" + D.NomeCIP + "'");


                            if (Atualizou)
                            {
                                dgv_DemandaGeral.DataSource = Banco.Procurar("Demanda", "IdDemanda, Cliente, Horario, NomeCIP, VdP,VdC, TMC", "IdDemanda", "'%'", "Cliente, Horario, NomeCIP");
                                dgv_DemandaGeral.Rows[linha].Selected = true;
                                dgv_DemandaGeral.CurrentCell = dgv_DemandaGeral[0, linha];
                                MessageBox.Show("Dados Atualizados");
                                RestablecerNovo();

                            }
                            else
                            {

                                MessageBox.Show("Não Foi Possivel Atualizar os Dados");


                            }

                        }
                        catch (ArgumentOutOfRangeException ex)
                        {
                            MessageBox.Show("Erro: " + ex.Message + "\nvalor actual: " + ex.ActualValue + "\n" + ex.ParamName + "\naqui=" + ex.InnerException);
                        }
                    }
                    else { MessageBox.Show("Impossivel atualizar: Fogão não existe na base de dados!", "Dados não existem", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                }
                else
                {
                    MessageBox.Show("Operação Cancelada"); return;
                }

            }
            else
            {
                DialogResult res = MessageBox.Show("Quer mesmo Salvar os dados?", "Quer Salvar?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {

                    if (dgv_DemandaGeral.Rows.Count > 0)
                    {

                        bool Salvou = false;
                        int Salvos = 0;
                        int Erros = 0;
                        CriarDemanda();
                        dt = Banco.ObterTodosOnde("Demanda", "IdDemanda", "'" + tb_IdDemanda.Text + "'");

                        try
                        {
                            linha = dgv_DemandaGeral.CurrentRow.Index;

                            foreach (DataRow dr in Demanda.Rows)
                            {
                                CriarDemanda(dr);
                                if (dt.Rows.Count > 0)
                                {
                                    Salvou = Banco.Atualizar("Demanda", "IdDemanda= '" + D.IdDemanda + "', Cliente= '" + D.Cliente + "', NomeFogão= '" + D.NomeFogão + "', Caracteristica='" + D.Caracteristica + "', Horario= '" + D.Horario + "', Data= '" + D.Data + "', NomeCIP= '" + D.NomeCIP + "', VdP= '" + D.VdP + "', VdC= '" + D.VdC + "', CICG= '" + D.CICG + "', Qf= '" + D.Qf + "', TMC= '" + D.TMC + "'", "IdDemanda", "'" + tb_IdDemanda.Text + "'");

                                }
                                else
                                {
                                    Salvou = Banco.Salvar("Demanda", "IdDemanda, Cliente, NomeFogão, Caracteristica, Horario, Data, NomeCIP, VdP, VdC, CICG, Qf, TMC", "'" + D.IdDemanda + "', '" + D.Cliente + "', '" + D.NomeFogão + "', '" + D.Caracteristica + "', '" + D.Horario + "', '" + D.Data + "','" + D.NomeCIP + "', '" + D.VdP + "', '" + D.VdC + "',  '" + D.CICG + "', '" + D.Qf + "',  '" + D.TMC + "'");
                                }

                                if (Salvou)
                                {
                                    Salvos += 1;
                                }
                                else
                                {
                                    Erros += 1;
                                }
                            }


                            if (Salvou)
                            {
                                dgv_DemandaGeral.DataSource = Banco.Procurar("Demanda", "IdDemanda, Cliente, Horario, NomeCIP, VdP,VdC, TMC", "IdDemanda", "'%'", "Cliente, Horario, NomeCIP");
                                RestablecerNovo();
                                dgv_DemandaGeral.Rows[linha].Selected = true;
                                dgv_DemandaGeral.CurrentCell = dgv_DemandaGeral[0, linha];
                                MessageBox.Show("Dados Salvos: " + Salvos + "\n\n~Dados Não Salvos: " + Erros, "Resumo de Cadastro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {

                                MessageBox.Show("Não Foi Possivel Salvar os Dados", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }


                        }
                        catch (ArgumentOutOfRangeException ex)
                        {
                            MessageBox.Show("Erro: hola" + ex.Message);
                        }
                    }

                    else { MessageBox.Show("Não há dados para salvar", "Faltam Dados", MessageBoxButtons.OK, MessageBoxIcon.Error); }

                }
                else
                {
                    MessageBox.Show("Operação Cancelada"); return;
                }
            }


        }

        private void btn_add_Click(object sender, EventArgs e)
        {


            if (cb_Clientes.SelectedIndex != 0)
            {
                if (cb_Fogão.Text != "")
                {
                    if (cb_Caracteristica.Text != "")
                    {
                        if (cb_Horario.Text != "")
                        {
                            if (nud_Qf.Value != 0)
                            {
                                if (cb_Fogão.Text == "FORNO ELETRICO")
                                {
                                    cb_Caracteristica.SelectedIndex = 4;
                                }

                                dt = Banco.Procurar2Criterios("Fogão", "*", "NomeFogão", "Caracteristica", "'" + cb_Fogão.Text + "'", "'" + cb_Caracteristica.SelectedIndex + "'", "Caracteristica");
                                IdSeleccionados = dt.Rows[0].Field<Int64>("Id").ToString();
                                DataTable dt2 = Banco.Procurar("BasesFogão", "*", "IdFogão", "'" + IdSeleccionados + "%'", "IdFogão");
                                MessageBox.Show(dt2.Rows.Count.ToString());
                                if (dt.Rows.Count > 0)
                                {
                                    CriarFogão(dt, dt2);
                                    CriarDemanda();
                                    dt = Banco.Procurar("Demanda", "*", "IdDemanda", "'" + cb_Fogão.SelectedValue.ToString() + cb_Clientes.SelectedValue.ToString() + cb_Horario.SelectedValue.ToString() + "-" + dtp_Data.Value.ToShortDateString() + "'", "IdDemanda");

                                    if (dt.Rows.Count < 1)
                                    {
                                        for (int i = 1; i < 12; i++)
                                        {
                                            if (i != 2)
                                            {
                                                if (DeterminaNomeCICGeCalculoDeVdPeVdC(i, dt2, 0))
                                                {
                                                    MessageBox.Show("CICG= " + CICG + "\n\nNomeCIP= " + NomeCIP);

                                                    DataRow dr = Demanda.NewRow();
                                                    dr["IdDemanda"] = cb_Fogão.SelectedValue.ToString() + "." + cb_Clientes.SelectedValue.ToString() + "." + cb_Horario.SelectedValue.ToString() + "-" + dtp_Data.Value.ToShortDateString();
                                                    dr["Cliente"] = cb_Clientes.Text;
                                                    dr["NomeFogão"] = cb_Fogão.Text;
                                                    dr["Caracteristica"] = D.Caracteristica;
                                                    dr["Horario"] = cb_Horario.Text;
                                                    dr["Data"] = dtp_Data.Value.ToShortDateString();
                                                    dr["NomeCIP"] = NomeCIP;
                                                    dr["VdP"] = VdP;
                                                    dr["VdC"] = VdC;
                                                    dr["CICG"] = CICG;
                                                    dr["Qf"] = nud_Qf.Value;
                                                    dr["TaxaDeConsumo"] = TC;

                                                    Demanda.Rows.Add(dr);


                                                    DataRow row = DemandaDGV.NewRow();
                                                    row["IdDemanda"] = cb_Fogão.SelectedValue.ToString() + "." + cb_Clientes.SelectedValue.ToString() + "." + cb_Horario.SelectedValue.ToString() + "-" + dtp_Data.Value.ToShortDateString();
                                                    row["Cliente"] = cb_Clientes.Text;
                                                    row["Horario"] = cb_Horario.Text;
                                                    row["NomeCIP"] = NomeCIP;
                                                    row["VdP"] = VdP;
                                                    row["VdC"] = VdC;
                                                    row["TMC"] = TC;

                                                    DemandaDGV.Rows.Add(row);
                                                }

                                            }
                                            else
                                            {
                                                MessageBox.Show("EntrouCase2");
                                                if (i == 2 && dt2.Rows.Count > 0)
                                                {
                                                    MessageBox.Show("Entrou0");

                                                    for (int j = 0; j < dt2.Rows.Count; j++)
                                                    {

                                                        if (DeterminaNomeCICGeCalculoDeVdPeVdC(i, dt2, j))
                                                        {


                                                            DataRow dr = Demanda.NewRow();
                                                            dr["IdDemanda"] = cb_Fogão.SelectedValue.ToString() + "." + cb_Clientes.SelectedValue.ToString() + "." + cb_Horario.SelectedValue.ToString() + "-" + dtp_Data.Value.ToShortDateString();
                                                            dr["Cliente"] = cb_Clientes.Text;
                                                            dr["NomeFogão"] = cb_Fogão.Text;
                                                            dr["Caracteristica"] = D.Caracteristica;
                                                            dr["Horario"] = cb_Horario.Text;
                                                            dr["Data"] = dtp_Data.Value.ToShortDateString();
                                                            dr["NomeCIP"] = NomeCIP;
                                                            dr["VdP"] = VdP;
                                                            dr["VdC"] = VdC;
                                                            dr["CICG"] = CICG;
                                                            dr["Qf"] = nud_Qf.Value;
                                                            dr["TaxaDeConsumo"] = TC;

                                                            Demanda.Rows.Add(dr);


                                                            DataRow row = DemandaDGV.NewRow();
                                                            row["IdDemanda"] = cb_Fogão.SelectedValue.ToString() + "." + cb_Clientes.SelectedValue.ToString() + "." + cb_Horario.SelectedValue.ToString() + "-" + dtp_Data.Value.ToShortDateString();
                                                            row["Cliente"] = cb_Clientes.Text;
                                                            row["Horario"] = cb_Horario.Text;
                                                            row["NomeCIP"] = NomeCIP;
                                                            row["VdP"] = VdP;
                                                            row["VdC"] = VdC;
                                                            row["TMC"] = TC;

                                                            DemandaDGV.Rows.Add(row);
                                                        }
                                                    }

                                                }
                                            }
                                        }
                                        dgv_DemandaGeral.DataSource = DemandaDGV;
                                    }
                                    else
                                    {
                                        MessageBox.Show("Item já existe na Base de dados", "Não Permitido Duplicar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                    //  btn_add.Enabled = false;
                                    // btn_Excluir2.Enabled = false;
                                    //btn_Novo.Text = "Novo";
                                }
                                else
                                {
                                    MessageBox.Show("Fagão não existe na base de dados", "Dado Não Cadastrado", MessageBoxButtons.OK, MessageBoxIcon.Error); nud_Qf.Focus();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Quantidade de Fogões (Qf) não pode ser Zero", "Faltam Dados", MessageBoxButtons.OK, MessageBoxIcon.Stop); nud_Qf.Focus();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Deve escolher um HORARIO antes de continuar", "Faltam Dados", MessageBoxButtons.OK, MessageBoxIcon.Stop); cb_Horario.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Deve escolher uma CARACTERISTICA antes de continuar", "Faltam Dados", MessageBoxButtons.OK, MessageBoxIcon.Stop); cb_Caracteristica.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Deve escolher um FOGÃO antes de continuar", "Faltam Dados", MessageBoxButtons.OK, MessageBoxIcon.Stop); cb_Fogão.Focus();
                }
            }
            else
            {
                MessageBox.Show("Deve escolher um CLIENTE antes de continuar", "Faltam Dados", MessageBoxButtons.OK, MessageBoxIcon.Stop); cb_Clientes.Focus();
            }


        }

        private void btn_Excluir2_Click(object sender, EventArgs e)
        {
            if (dgv_DemandaGeral.Rows.Count > 0)
            {
                string c = dgv_DemandaGeral.Rows[dgv_DemandaGeral.SelectedRows[0].Index].Cells[0].Value.ToString();
                DialogResult res = MessageBox.Show("Certeza que deseja Excluir?\n\n", "Excluir", MessageBoxButtons.YesNo);
                if (res == DialogResult.Yes)
                {
                    DemandaDGV = (DataTable)dgv_DemandaGeral.DataSource;
                    int conta = DemandaDGV.Rows.Count;

                    for (int i = conta - 1; i >= 0; i--)
                    {
                        DataRow dr = DemandaDGV.Rows[i];


                        if ((string)dr["IdDemanda"] == c)
                        {
                            dr.Delete();
                        }

                    }
                    DemandaDGV.AcceptChanges();
                    dgv_DemandaGeral.DataSource = DemandaDGV;
                }
            }

        }

        #endregion;

        #region COMBOBOXES CAMBIAM

        #region TURNOS
        private void cb_Turno_SelectedIndexChanged(object sender, EventArgs e)
        {
            cb_Horario.DataSource = Banco.Procurar("Horarios", "*", "Turno", "'" + cb_Turno.Text + "%'", "Id");
        }
        private void cb_Ttmc_SelectedIndexChanged(object sender, EventArgs e)//gRAFICOTMC
        {
            if (cargou)
            {
                cb_HorarioTMC.DataSource = Banco.Procurar("Horarios", "*", "Turno", "'" + cb_Ttmc.Text + "%'", "Id");
                cb_TDemandaPeças.SelectedIndex = cb_Ttmc.SelectedIndex;
                cb_TDemandaCapacidade.SelectedIndex = cb_Ttmc.SelectedIndex;
            }
        }
        private void cb_TDemandaCapacidade_SelectedIndexChanged(object sender, EventArgs e)//GRAFICODEMANDACAPACIDADE
        {
            if (cargou)
            {
                cb_HorarioDemandaCapacidade.DataSource = Banco.Procurar("Horarios", "*", "Turno", "'" + cb_TDemandaCapacidade.Text + "%'", "Id");
                cb_TDemandaPeças.SelectedIndex = cb_TDemandaCapacidade.SelectedIndex;
                cb_Ttmc.SelectedIndex = cb_TDemandaCapacidade.SelectedIndex;
            }
        }
        private void cb_TDemandaPeças_SelectedIndexChanged(object sender, EventArgs e)//GRAFICODEMANDAPEÇAS
        {
            if (cargou)
            {
                cb_HorarioDemandaPeças.DataSource = Banco.Procurar("Horarios", "*", "Turno", "'" + cb_TDemandaPeças.Text + "%'", "Id");
                cb_TDemandaCapacidade.SelectedIndex = cb_TDemandaPeças.SelectedIndex;
                cb_Ttmc.SelectedIndex = cb_TDemandaPeças.SelectedIndex;
            }
        }
        #endregion;

        #region CLIENTES
        private void cb_ClienteTMC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cargou)
            {
                if (cb_ClienteTMC.SelectedIndex == 0)
                {
                    dt = Banco.Procurar("Demanda", "*", "IdDemanda", "'%" + dtp_DataTMC.Value.ToShortDateString() + "%'", "Cliente, Horario, NomeCIP");
                }
                else
                {
                    dt = Banco.Procurar2Criterios("Demanda", "*", "IdDemanda", "IdDemanda", "'%." + cb_ClienteTMC.SelectedIndex + ".%'", "'%" + dtp_DataTMC.Value.ToShortDateString() + "%'", "Cliente, Horario, NomeCIP");
                }


                PrencherGrafico(dt, G_TMC, "Demanda-Tempo Medio de Consumo", 1, "TMC", "TempoMedioDeConsumo");
            }

        }
        #endregion;

        #region HORARIOS
        private void cb_HorarioTMC_SelectedIndexChanged(object sender, EventArgs e)//gRAFICOTMC
        {
            if (cargou)
            {
                dt = Banco.Procurar("Demanda", "*", "IdDemanda", "'%." + cb_Horario.SelectedValue + "-" + dtp_DataTMC.Value.ToShortDateString() + "'", "Cliente, Horario, NomeCIP");

                PrencherGrafico(dt, G_TMC, "Demanda-Tempo Medio de Consumo", 1, "TMC", "TempoMedioDeConsumo");

                cb_HorarioDemandaPeças.SelectedIndex = cb_HorarioTMC.SelectedIndex;
                cb_HorarioDemandaCapacidade.SelectedIndex = cb_HorarioTMC.SelectedIndex;
            }

        }
        private void cb_HorarioDemandaCapacidade_SelectedIndexChanged(object sender, EventArgs e)//GRAFICODEMANDACAPACIDADE
        {
            if (cargou)
            {
                dt = Banco.Procurar("Demanda", "*", "IdDemanda", "'%." + cb_HorarioDemandaCapacidade.SelectedValue + "-" + dtp_DataDemandaCapacidade.Value.ToShortDateString() + "'", "Cliente, Horario, NomeCIP");

                PrencherGrafico(dt, G_DemandaContainer, "Demanda-Contêiner", 1, "VdC", "VelocidadeDeConteiner");
                cb_HorarioDemandaPeças.SelectedIndex = cb_HorarioDemandaCapacidade.SelectedIndex;
                cb_HorarioTMC.SelectedIndex = cb_HorarioDemandaCapacidade.SelectedIndex;
            }
        }
        private void cb_HorarioDemandaPeças_SelectedIndexChanged(object sender, EventArgs e)//GRAFICODEMANDAPEÇAS
        {
            if (cargou)
            {
                dt = Banco.Procurar("Demanda", "*", "IdDemanda", "'%." + cb_HorarioDemandaPeças.SelectedValue + "-" + dtp_DataDemandaPeças.Value.ToShortDateString() + "'", "Cliente, Horario, NomeCIP");

                PrencherGrafico(dt, G_DemandaPeças, "Demanda-Peças", 2, "VdP", "VelocidadeDasPeças");
                cb_HorarioDemandaCapacidade.SelectedIndex = cb_HorarioDemandaPeças.SelectedIndex;
                cb_HorarioTMC.SelectedIndex = cb_HorarioDemandaPeças.SelectedIndex;
            }
        }


        #endregion;

        #region DATAS

        private void dtp_DataTMC_ValueChanged(object sender, EventArgs e)//gRAFICOTMC
        {
            if (cargou)
            {
                dt = Banco.Procurar("Demanda", "*", "IdDemanda", "'%" + dtp_DataTMC.Value.ToShortDateString() + "%'", "Cliente, Horario, NomeCIP");

                PrencherGrafico(dt, G_TMC, "Demanda-Tempo Medio de Consumo", 1, "TMC", "TempoMedioDeConsumo");

                dtp_DataDemandaPeças.Value = dtp_DataTMC.Value;
                dtp_DataDemandaCapacidade.Value = dtp_DataTMC.Value;

            }
        }
        private void dtp_DataDemandaCapacidade_ValueChanged(object sender, EventArgs e)//GRAFICODEMANDACAPACIDADE
        {
            if (cargou)
            {
                dt = Banco.Procurar("Demanda", "*", "IdDemanda", "'%" + dtp_DataDemandaCapacidade.Value.ToShortDateString() + "%'", "Cliente, Horario, NomeCIP");

                PrencherGrafico(dt, G_DemandaContainer, "Demanda-Contêiner", 1, "VdC", "VelocidadeDeConteiner");

                dtp_DataDemandaPeças.Value = dtp_DataDemandaCapacidade.Value;
                dtp_DataTMC.Value = dtp_DataDemandaCapacidade.Value;
            }
        }
        private void dtp_DataDemandaPeças_ValueChanged(object sender, EventArgs e)//GRAFICODEMANDAPEÇAS
        {
            if (cargou)
            {
                dt = Banco.Procurar("Demanda", "*", "IdDemanda", "'%" + dtp_DataDemandaPeças.Value.ToShortDateString() + "%'", "Cliente, Horario, NomeCIP");

                PrencherGrafico(dt, G_DemandaPeças, "Demanda-Peças", 2, "VdP", "VelocidadeDasPeças");
                dtp_DataDemandaCapacidade.Value = dtp_DataDemandaPeças.Value;
                dtp_DataTMC.Value = dtp_DataDemandaPeças.Value;
            }
        }

        #endregion;

        private void tb_CICG_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
              if (dgv_DemandaGeral.Rows.Count > 0)
              {

                  IdSeleccionados2 = dgv_DemandaGeral.Rows[dgv_DemandaGeral.SelectedRows[0].Index].Cells[3].Value.ToString();
                  dt = Banco.Procurar2Criterios("Fogão", "*", "NomeFogão", "Caracteristica", "'" + cb_Fogão.Text + "'", "'" + cb_Caracteristica.SelectedIndex + "'", "Caracteristica");
                  IdSeleccionados = dt.Rows[0].Field<Int64>("Id").ToString();
                  DataTable dt2 = Banco.Procurar("BasesFogão", " Base, Quantidade", "IdFogão", "'" + IdSeleccionados + "%'", "IdFogão");
                  CriarFogão(dt, dt2);
                  if (IdSeleccionados2.Contains("BANDEJA"))
                  {

                      CalcularCapacidadeVdCVdPeTC(tb_CICG.Text, int.Parse(F.Bandeja.Substring(0,1)),cb_Peça.Text);

                  }
                  else
                  {
                      if (IdSeleccionados2.Contains("COSTA"))
                      {

                          CalcularCapacidadeVdCVdPeTC(tb_CICG.Text, F.Costa, cb_Peça.Text);

                      }
                      else
                      {
                          if (IdSeleccionados2.Contains("DIFUSOR"))
                          {

                              CalcularCapacidadeVdCVdPeTC(tb_CICG.Text, F.Difusor, cb_Peça.Text);


                          }
                          else
                          {
                              if (IdSeleccionados2.Contains("LATERAL"))
                              {
                                  CriarDemanda();
                                  CalcularCapacidadeVdCVdPeTC(tb_CICG.Text, F.Lateral, cb_Peça.Text);
                                  AssignarDemandaaosControles();
                              }
                              else
                              {
                                  if (IdSeleccionados2.Contains("PORTA"))
                                  {

                                      CalcularCapacidadeVdCVdPeTC(tb_CICG.Text, F.Porta, cb_Peça.Text);

                                  }
                                  else
                                  {
                                      if (IdSeleccionados2.Contains("QUADRO"))
                                      {

                                          CalcularCapacidadeVdCVdPeTC(tb_CICG.Text, F.QuadroFrontal, cb_Peça.Text);

                                      }
                                      else
                                      {
                                          if (IdSeleccionados2.Contains("TETO"))
                                          {
                                              CriarDemanda();
                                              CalcularCapacidadeVdCVdPeTC(tb_CICG.Text, F.Teto, cb_Peça.Text);
                                              AssignarDemandaaosControles();
                                          }
                                          else
                                          {
                                              if (IdSeleccionados2.Contains("VEDAÇÃO"))
                                              {
                                                  CriarDemanda();
                                                  CalcularCapacidadeVdCVdPeTC(tb_CICG.Text, F.Vedação, cb_Peça.Text);
                                                  AssignarDemandaaosControles();
                                              }
                                              else
                                              {
                                                  if (IdSeleccionados2.Contains("MESA"))
                                                  {
                                                      CriarDemanda();
                                                      CalcularCapacidadeVdCVdPeTC(tb_CICG.Text, F.Mesa, cb_Peça.Text);

                                                  }
                                                  else
                                                  {
                                                      if (IdSeleccionados2.Contains("BASE"))
                                                      {

                                                              CriarDemanda();
                                                              CalcularCapacidadeVdCVdPeTC(tb_CICG.Text, F.Bases,0);
                                                              AssignarDemandaaosControles();


                                                      }

                                                  }
                                              }
                                          }
                                      }
                                  }
                              }
                          }
                      }
                  }
              }*/
        }

        #endregion;

        #region DGV CAMBIA

        private void dgv_DemandaGeral_SelectionChanged(object sender, EventArgs e)
        {
            int contilinha = dgv_DemandaGeral.Rows.Count;
            if (contilinha > 0)
            {
                int row = dgv_DemandaGeral.CurrentRow.Index;
                dgv_DemandaGeral.Rows[row].Cells[0].Selected = true;

                try
                {

                    AtualizarSelección();



                    int dgvIndex = dgv_DemandaGeral.CurrentRow.Index;
                    dgv_DemandaGeral.Rows[dgvIndex].Selected = true;
                    dgv_DemandaGeral.CurrentCell = dgv_DemandaGeral[0, dgvIndex];



                }
                catch (ArgumentOutOfRangeException ex)
                {
                    MessageBox.Show("Erro: " + ex.Message);

                }
            }
        }

        #endregion;

      
      
    }
}
