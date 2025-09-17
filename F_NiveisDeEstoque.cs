using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;

namespace Atlas_projeto
{
    public partial class F_NiveisDeEstoque : Form
    {
        #region VARIAVEIS
        int VcumCIP = "0.0.0.0".Length;
        Peça p;
        Contenedor c;
        DataTable dt = new DataTable();
        bool cambio = false;
        string NomeBaseDadosEstoque1 = "";
        string NomeBaseDadosEstoque2 = "";
        string _Estoque = "";
        string CIP;
        int CapUsada,CapNãoDisp,CapCliente = 0;
        int CapDisponivel,Disponiveis, EmUso,EmManutenção ,Descartados , ForadeUso,QcontNDisp, QcontDisp = 0;
        int CapTotal;
        int[] IndicedePontos = new int[4];
        int Cap0 = 0;
        int cap2 = 0;
        decimal QconteUsados;
        string Ns1 = "Estocada", Ns2= "Disponivel", Ns3="Não Disponiveis", Ns4 = "Com o Cliente";

        #endregion;

        public F_NiveisDeEstoque(string estoque, string setor)
        {
            InitializeComponent();
            _Estoque = estoque;
            lb_Setor.Text = setor;
        }
        private void F_NiveisDeEstoque_Load(object sender, EventArgs e)
        {
            #region PopulandoComboBox 

            //Populando combo Box Estoque

            Dictionary<string, string> Estoque = new Dictionary<string, string>();

            Estoque.Add("1", "DESCARGA");
            Estoque.Add("2", "CARGA");
            Estoque.Add("3", "REFORMA");
            Estoque.Add("4", "PROCESSO");


            cb_QualEstoque.Items.Clear();
            cb_QualEstoque.DataSource = new BindingSource(Estoque, null);
            cb_QualEstoque.DisplayMember = "Value";
            cb_QualEstoque.ValueMember = "key";

            //Populando ComboBox Filtro
            cb_Filtrar.Items.Clear();
            cb_Filtrar.DataSource = Banco.ObterTodos("Familias", "*", "IdFamilia");
            cb_Filtrar.DisplayMember = "Familia";
            cb_Filtrar.ValueMember = "IdFamilia";

            cambio = true;
            #endregion;

            #region cargandoelgráfico...
            if (_Estoque != "")
            {
                cb_QualEstoque.Text = _Estoque;
            }
          
            Graficar(Angulo());

            cambio = true;
            #endregion;
        }

        #region PROCEDIMIENTOS
        private void AssignartituloeSeries(Color color)
        {
            G_Grafico_Geral.Series.Clear();
            G_Grafico_Geral.Titles.Clear();

            Title title2 = new Title();
            title2.Font = new Font("Arial", 12, FontStyle.Bold);
            title2.ForeColor = color;
            title2.Text = "Nivel de Estoque\n" + cb_QualEstoque.Text + " \n" + cb_Filtrar.Text;
            G_Grafico_Geral.Titles.Add(title2);

            G_Grafico_Geral.Series.Add(Ns1);
            G_Grafico_Geral.Series.Add((Ns2));
            G_Grafico_Geral.Series.Add((Ns3));
            G_Grafico_Geral.Series.Add((Ns4));

        }
        private int EstablecerCapUsada(DataTable dados, string CIPprocurado)
        { int aux, aux2, Cap = 0;
            string Dado, Dado2;
            int Dado3;
            if (dados.Rows.Count>0)
            {
                for (int i = 0; i < dados.Rows.Count; i++)
                {
                    Dado = dados.Rows[i].Field<string>("PEÇAcontida");
                    Dado2 = dados.Rows[i].Field<string>("CapUsada");
                    Dado3= Convert.ToInt32(dados.Rows[i].Field<Int64>("UbicaçãoAtual"));
                    if (Dado.Contains(CIPprocurado))
                    {
                        aux = EstablecerIndices(Dado);
                        for (int y = 0; y < aux; y++)
                        {
                            if (y == 0)
                            {
                                EstablecerIndices(Dado);

                                if (Dado.Substring(0, IndicedePontos[y]) == CIPprocurado)
                                {
                                   
                                    if (Dado3!=7 && Dado3 !=8 )
                                    {
                                        aux2 = EstablecerIndices(Dado2);

                                        Cap += int.Parse(Dado2.Substring(0, IndicedePontos[y]));
                                    }
                                    else
                                    {
                                        
                                        aux2 = EstablecerIndices(Dado2);
                                        CapCliente += int.Parse(Dado2.Substring(0, IndicedePontos[y]));
                                    }
                                    
                                }
                            }
                            else
                            {
                                if (Dado.Substring(IndicedePontos[y - 1] + 1, IndicedePontos[y] - IndicedePontos[y - 1] - 1) == CIPprocurado)
                                {
                                    
                                    if (Dado3 != 7 && Dado3 != 8)
                                    {
                                        aux2 = EstablecerIndices(Dado2);

                                        Cap += int.Parse(Dado2.Substring(IndicedePontos[y - 1] + 1, IndicedePontos[y] - IndicedePontos[y - 1] - 1));

                                    }
                                    else
                                    {
                                        
                                        aux2 = EstablecerIndices(Dado2);

                                        CapCliente += int.Parse(Dado2.Substring(IndicedePontos[y - 1] + 1, IndicedePontos[y] - IndicedePontos[y - 1] - 1));
                                        
                                    }
                                }
                            }
                        }
                    }
                }


            }

            return Cap;
        }
        private int EstablecerIndices(string Cadena)
        {
            string Buscado = ",";
            string c;

            int m = 0;

            for (int i = 0; i < Cadena.Length; i++)
            {
                c = Cadena.Substring(i, 1);
                if (c == Buscado)
                {

                    IndicedePontos[m] = i;
                    m += 1;

                }
            }
            return m;
        }
        private int EstablecerIndices2(string Cadena)
        {
            string Buscado = ".";
            string c;

            int m = 0;
                        
            for (int i = 0; i < Cadena.Length; i++)
            {
                c = Cadena.Substring(i, 1);
                if (c == Buscado)
                {
                  
                    IndicedePontos[m] = i;
                    m += 1;

                }
            }
          
            return m;
        }
       
        private void CalcularCapacidadeDisponivel(string CIP, string CICG, int PadrãoArmazenamento)
        {
            DataTable dados = Banco.Procurar("CadastroGeralContenedores", "Capacidade, N_Contenedores", "CICG", "'" + CICG + "'", "CICG");

            int CapC = Convert.ToInt32(dados.Rows[0].Field<Int64>("Capacidade"));
            int CapG = CalcularCapacidadeTotal(CICG, CIP, PadrãoArmazenamento, dados);

             EstablecerIndices2(CICG);
             if (CICG.Substring(IndicedePontos[0] + 1, 1) == "0")
             {

                 CapDisponivel = Cap0 * Disponiveis;
                 CapNãoDisp = Cap0 * QcontNDisp;
             }
             else
             {

                 CapDisponivel = CapG - (CapUsada+CapCliente);
                 CapNãoDisp = CapC * QcontNDisp;
             }
            
        }
        private void EstablecerCapDisponivelSegundoEstado(string CICG,string CIPprocurado)
        {
            DataTable disponiveis = Banco.Procurar2Criterios("CadastroEspecificoContenedores", "ESTADO, CICG, CapUsada, PEÇAcontida", "ESTADO", "CICG", "0", "'" + CICG + "'", "CICG");
            DataTable emUso = Banco.Procurar2Criterios("CadastroEspecificoContenedores", "ESTADO, CICG, CapUsada, PEÇAcontida,UbicaçãoAtual", "ESTADO", "CICG", "1", "'" + CICG + "'", "CICG");
            DataTable emManutenção= Banco.Procurar2Criterios("CadastroEspecificoContenedores", "ESTADO, CICG", "ESTADO", "CICG", "2", "'" + CICG + "'", "CICG");
            DataTable descartados = Banco.Procurar2Criterios("CadastroEspecificoContenedores", "ESTADO, CICG", "ESTADO", "CICG", "3", "'" + CICG + "'", "CICG");
            DataTable foradeUso = Banco.Procurar2Criterios("CadastroEspecificoContenedores", "ESTADO, CICG", "ESTADO", "CICG", "4", "'" + CICG + "'", "CICG");

            Disponiveis = disponiveis.Rows.Count;
            EmUso=emUso.Rows.Count;
            QcontDisp = Disponiveis + EmUso;
            EmManutenção=emManutenção.Rows.Count;
            Descartados=descartados.Rows.Count; 
            ForadeUso=foradeUso.Rows.Count;
            CapUsada = EstablecerCapUsada(emUso, CIPprocurado);
            QcontNDisp =EmManutenção+Descartados+ForadeUso;

        }
       
        private int CalcularCapacidadeTotal( string CICG, string CIP, int PadrãoArmazenamento, DataTable dados)
        {/* Estado.Add("0", "DISPONIVEL");
            Estado.Add("1", "EM USO");
            Estado.Add("2", "EM MANUTENÇÃO");
            Estado.Add("3", "DESCARTADO");
            Estado.Add("4", "FORA DE USO");*/
            int Quantidade;
            int x;
            EstablecerCapDisponivelSegundoEstado(CICG, CIP);
            Calcular(CIP, CICG, PadrãoArmazenamento);

            EstablecerIndices2(CICG);
             if (CICG.Substring(IndicedePontos[0]+1,1)=="0")
            {
                Quantidade = Convert.ToInt32(dados.Rows[0].Field<Int64>("N_Contenedores"))-QcontNDisp;
                x = Cap0 * Quantidade;
                
            }
            else
            {
                
                int Capacidade = Convert.ToInt32(dados.Rows[0].Field<Int64>("Capacidade"));              
                Quantidade = Convert.ToInt32(dados.Rows[0].Field<Int64>("N_Contenedores"))-QcontNDisp;
                x = Capacidade * Quantidade;
               
            }

            


            return x;
        }
        private void CrearPeça(string CIP, string Setor)
        {
            DataTable dt;
            if (Setor!="Esmaltação")
            {
                dt = Banco.Procurar("PEÇAS" + Setor, "*", "CIP", "'" + CIP + "'", "CIP");
            }
            else
            {
               dt = Banco.Procurar("Peças", "*", "CIP", "'" + CIP + "'", "CIP");
            }
            
            p = new Peça();
            p.CIP = CIP;
            p.NomeCIP = dt.Rows[0].Field<string>("NomeCIP");
            p.NomeFormal = dt.Rows[0].Field<string>("NomeFormal");
            p.Código = dt.Rows[0].Field<string>("Código");
            p.Altura = Decimal.Parse(dt.Rows[0].Field<string>("Altura"));
            p.Cumprimento = Decimal.Parse(dt.Rows[0].Field<string>("Cumprimento"));
            p.Largura = Decimal.Parse(dt.Rows[0].Field<string>("Largura"));
            p.ValorEstampada = Decimal.Parse(dt.Rows[0].Field<string>("ValorEstampada"));
            p.ValorProcesso = Decimal.Parse(dt.Rows[0].Field<string>("ValorProcesso"));
            p.ValorSucata1 = Decimal.Parse(dt.Rows[0].Field<string>("ValorSucata1"));
            p.ValorSucata2 = Decimal.Parse(dt.Rows[0].Field<string>("ValorSucata2"));
            p.Diametro = Decimal.Parse(dt.Rows[0].Field<string>("Diametro"));
            p.Alto = Decimal.Parse(dt.Rows[0].Field<string>("Alto"));
            p.Alto2 = Decimal.Parse(dt.Rows[0].Field<string>("Alto2"));
            p.CamadaMin = Decimal.Parse(dt.Rows[0].Field<string>("CamadaMin"));
            p.CamadaMax = Decimal.Parse(dt.Rows[0].Field<string>("CamadaMax"));
            p.Imagem = dt.Rows[0].Field<string>("Foto");
            p.MassaSemEsmaltar = dt.Rows[0].Field<string>("MSE");
            p.MassaEsmaltada = dt.Rows[0].Field<string>("ME");

            if (dt.Rows[0].Field<string>("Circular")=="SIM")
            {
                p.Circular2 = true;
            }
            else
            {
                p.Circular2 = false;
            }
        }
        private void CrearContenedor(string CICG)
        {
         c= new Contenedor();
           DataTable dt= Banco.Procurar("CadastroGeralContenedores", "*", "CICG","'"+CICG+"'","CICG");

            c.CICG = CICG;
            c.Cumprimento = decimal.Parse(dt.Rows[0].Field<string>("Cumprimento"));
            c.Largura = decimal.Parse(dt.Rows[0].Field<string>("Largura"));
            c.Altura = decimal.Parse(dt.Rows[0].Field<string>("Altura"));
        }
        private void Calcular(string CIP, string CICG,  int PadrãoArmazenamnto)
        {
            int Rx;
            int Ry;
            int Qp;
            decimal Q1 = 0;
            decimal Qvv = 0;
            decimal Qvh = 0;
            decimal Evh = 0;
            decimal Evv = 0;
            decimal Qfh = 0;
            decimal Qfv = 0;
            decimal Qfh2 = 0;
            decimal Ar = 0;
            decimal Ao = 0;
            decimal At = 0;
            int Qpv = 0;           
            decimal CI;
            decimal Vc;
            decimal Vp;
            int Npeçafila;
            int Nfilas;
            int Qpadrão;
          
            
            CrearPeça(CIP, lb_Setor.Text);
            CrearContenedor(CICG);

            decimal x;
            if (PadrãoArmazenamnto == 1)
            {
                Qpadrão = 2;
                x = p.Alto;
            }
            else
            {
                Qpadrão = 1;
                x = p.Alto2;
            }

            if (p.Circular2)
            {
                

                if (p.Diametro == 0 || p.Alto == 0 || p.Alto2 == 0)
                {
                    return;
                }

                #region Columnasyfilas

                Rx = Convert.ToInt32(Math.Floor(c.Cumprimento / p.Diametro));

                Ry = Convert.ToInt32(Math.Floor(c.Largura / p.Diametro));
                Qp = Rx * Ry;

                Ao = Convert.ToDecimal(Math.PI * Math.Pow(Convert.ToDouble(p.Diametro), 2) / 4) * Qp;
                At = c.Cumprimento * c.Largura;
                Ar = At - Ao;
                Qpv = Convert.ToInt32(Math.Floor(Convert.ToDecimal(Math.Sqrt(Convert.ToDouble(Ar))) / p.Diametro));
                Npeçafila = Qp + Qpv;
                Nfilas = Convert.ToInt32(Math.Floor(c.Altura / x));
                Cap0 =Convert.ToInt32( Math.Ceiling( Npeçafila * Nfilas * x));

                #endregion;
            }
            else
            {

                if (PadrãoArmazenamnto == 1)
                {
                    if ((p.Cumprimento == 0 || p.Largura == 0 || p.Altura == 0))
                    {
                       // MessageBox.Show("Faltam dados de dimeções da peça " + CIP + "\nDeve ir até o casdastro e inserir os dados", "Faltam dados da peça", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        return;
                    }


                    if (c.Largura <= 0 || c.Cumprimento <= 0 || c.Altura <= 0)
                    {
                       // MessageBox.Show("Faltam dados de dimeções da contentor" + CIP + "\nDeve ir até o casdastro e inserir os dados", "Faltam dados do contentor", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        return;
                    }
                    Q1 = Math.Floor(c.Cumprimento / p.Largura);
                    Qfh = Math.Floor(c.Largura / p.Cumprimento);
                    Qfv = Math.Floor(c.Altura / p.Altura);


                    Qfh2 = Math.Floor(c.Cumprimento / p.Cumprimento);

                    Evh = c.Largura - (p.Cumprimento * Qfh);
                    Qvh = Evh / p.Largura;

                    Evv = c.Altura - (p.Altura * Qfv);
                    Qvv = Evv / p.Largura;

                    Vc = c.Altura * c.Largura * c.Cumprimento;
                    Vp = p.Cumprimento * p.Largura * p.Altura;
                    CI = (int)Math.Floor(Vc / Vp) * Qpadrão;

                    Cap0 = Convert.ToInt32(((Q1 * Qfh * Qfv) + (Qvh * Qfh2 * Qfv) + (Qvv * Qfh2 * Math.Floor(c.Largura / p.Altura))) * Qpadrão);



                }
                else
                {
                    if (PadrãoArmazenamnto == 2)
                    {

                        if (p.Cumprimento == 0 || p.Altura == 0 || p.Largura == 0)
                        {
                            //MessageBox.Show("Faltam dados de dimeções da  " + CIP + "\nDeve ir até o casdastro e inserir os dados", "Faltam dados da peça", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            return;
                        }

                        if (c.Largura <= 0 || c.Cumprimento <= 0 || c.Altura <= 0)
                        {
                         //   MessageBox.Show("Faltam dados de dimeções do contentor " + CIP + "\nDeve ir até o casdastro e inserir os dados", "Faltam dados da contentor", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            return;
                        }
                        Q1 = Math.Floor(c.Cumprimento / p.Largura);
                        Qfh = Math.Floor(c.Largura / p.Altura);
                        Qfv = Math.Floor(c.Altura / p.Cumprimento);

                        Qfh2 = Math.Floor(c.Cumprimento / p.Altura);

                        Evh = c.Largura - (p.Altura * Qfh);
                        Qvh = Evh / p.Largura;

                        Evv = p.Altura - (p.Cumprimento * Qfv);
                        Qvv = Evv / p.Largura;

                        Vc = c.Altura * c.Largura * c.Cumprimento;
                        Vp = p.Cumprimento * p.Largura * p.Altura;
                        cap2 = (int)Math.Floor(Vc / Vp) * Qpadrão;

                        Cap0 = Convert.ToInt32(((Q1 * Qfh * Qfv) + (Qvh * Qfh2 * Qfv) + (Qvv * Qfh * Math.Floor(c.Cumprimento / p.Cumprimento))) * Qpadrão);

                    }
                }


            }

           
        }

        private void AgregarDados(DataTable CIPsPeças, string Area)
        {
            int i = 0, PadrãoDeArmazenamento;
            string CIP;
            string NomeCIP;
            string CICG;
            CapDisponivel = 0;
            CapNãoDisp = 0;
            CapUsada = 0;
            do
            {
                NomeCIP = CIPsPeças.Rows[i].Field<string>("NomeCIP");

                CIP = CIPsPeças.Rows[i].Field<string>("CIP");
                PadrãoDeArmazenamento =Convert.ToInt32( CIPsPeças.Rows[i].Field<long>("PadrãoDeArmazenamento"));
       
                DataTable dt= Banco.Procurar2Criterios("CadastroEspecificoContenedores", "CICG", "PEÇAcontida", "CICG", "'" + CIP +"%'", "'%" + Area + "%'", "CICG");
               
                if (dt.Rows.Count>0)
                {

                    CalcularCapacidadeDisponivel( CIP, dt.Rows[0].Field<string>("CICG"), PadrãoDeArmazenamento);
                    
                }
                else
                {
                    EstablecerIndices2(CIP);
                    dt = Banco.Procurar2Criterios("CadastroEspecificoContenedores", "CICG", "CICG", "CICG", "'%" + CIP.Substring(0, IndicedePontos[2]) + "%'", "'%" + Area + "%'", "CICG");

                    if (dt.Rows.Count>0)
                    {
                        
                            CalcularCapacidadeDisponivel(CIP, dt.Rows[0].Field<string>("CICG"), PadrãoDeArmazenamento);

                    }
                    else
                    {
                        dt = Banco.Procurar2Criterios("CadastroEspecificoContenedores", "CICG", "CICG", "CICG", "'%" + CIP.Substring(0, IndicedePontos[0]) + "%'", "'%" + Area + "%'", "CICG");
                        if (dt.Rows.Count>0)
                        {
                            CICG = dt.Rows[0].Field<string>("CICG");

                            if (CICG.Substring(IndicedePontos[0] + 1, 1) == "0" && CIP.Substring(0, IndicedePontos[0]) == CICG.Substring(0, IndicedePontos[0]))
                            {
                                CalcularCapacidadeDisponivel(CIP, CICG, PadrãoDeArmazenamento);

                            }
                            else
                            {
                                
                                    CapDisponivel = 0;
                                    CapNãoDisp = 0;
                                    CapUsada = 0;
                                CapCliente = 0;
                                
                            }
                        }
                        else
                        {
                            CapDisponivel = 0;
                            CapNãoDisp = 0;
                            CapUsada = 0;
                            CapCliente =0;

                        }
                        
                        
                    }
                }
          

                G_Grafico_Geral.Series[Ns1].ChartType = SeriesChartType.StackedColumn;
                    G_Grafico_Geral.Series[Ns1].BorderWidth = 4;
                    G_Grafico_Geral.Series[Ns1].Points.AddXY(NomeCIP, CapUsada);
                    G_Grafico_Geral.Series[Ns1].IsValueShownAsLabel = true;
                    G_Grafico_Geral.Series[Ns1].LabelAngle = 90;

                G_Grafico_Geral.Series[Ns4].ChartType = SeriesChartType.StackedColumn;
                G_Grafico_Geral.Series[Ns4].BorderWidth = 4;
                G_Grafico_Geral.Series[Ns4].Points.AddXY(NomeCIP, CapCliente);
                G_Grafico_Geral.Series[Ns4].IsValueShownAsLabel = true;
                G_Grafico_Geral.Series[Ns4].LabelAngle = 0;


                G_Grafico_Geral.Series[Ns2].ChartType = SeriesChartType.StackedColumn;
                G_Grafico_Geral.Series[Ns2].BorderWidth = 4;
                G_Grafico_Geral.Series[Ns2].Points.AddXY(NomeCIP, CapDisponivel);
                G_Grafico_Geral.Series[Ns2].IsValueShownAsLabel = true;
                G_Grafico_Geral.Series[Ns2].LabelAngle = 90;


                  G_Grafico_Geral.Series[Ns3].ChartType = SeriesChartType.StackedColumn;
                    G_Grafico_Geral.Series[Ns3].BorderWidth = 4;
                    G_Grafico_Geral.Series[Ns3].Points.AddXY(NomeCIP, CapNãoDisp);
                    G_Grafico_Geral.Series[Ns3].IsValueShownAsLabel = true;
                    G_Grafico_Geral.Series[Ns3].LabelAngle = 0;

               


                i = i + 1;
            } while (i < CIPsPeças.Rows.Count);
        }
        private void COnfigurarAngulolabel(int Angulo)
        {
          
            G_Grafico_Geral.Series[0].SmartLabelStyle.Enabled = false;
            foreach (DataPoint dp in G_Grafico_Geral.Series[0].Points)
            {
                dp.LabelAngle = -Angulo;
            }
          
            G_Grafico_Geral.Series[1].SmartLabelStyle.Enabled = false;
            foreach (DataPoint dp in G_Grafico_Geral.Series[1].Points)
            {
                dp.LabelAngle = -Angulo;
            }


            G_Grafico_Geral.Series[2].SmartLabelStyle.Enabled = false;
            foreach (DataPoint dp in G_Grafico_Geral.Series[2].Points)
            {
                dp.LabelAngle = -Angulo;
            }
            G_Grafico_Geral.Series[3].SmartLabelStyle.Enabled = false;
            foreach (DataPoint dp in G_Grafico_Geral.Series[3].Points)
            {
                dp.LabelAngle = -Angulo;
            }

            G_Grafico_Geral.ChartAreas[0].AxisX.Interval = 1;
            G_Grafico_Geral.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
            G_Grafico_Geral.ChartAreas[0].AxisX.LabelStyle.Angle = 90;




        }
        private DataTable UnirDatatables(DataTable d1, DataTable d2)
        { DataTable dt = new DataTable();
            
                d1.Merge(d2, true);               

                dt.Columns.Add(new DataColumn("NomeCIP", typeof(string)));
                dt.Columns.Add(new DataColumn("Quantidade", typeof(int)));

                ArrayList array = new ArrayList();
                foreach (DataRow dr in dt.Rows)
                {

                    object total;


                    if (array.IndexOf(dr["CIP"]) < 0)

                    {


                        total = dt.Compute(String.Format("SUM(Quantidade)"), "CIP = '" + dr["CIP"] + "'");

                        dt.Rows.Add(new object[] { dr["NomeCIP"], Convert.ToInt32(total), });
                        array.Add(dr["CIP"]);

                    }

                }
            
            return dt;
        }
        private string EstablecerNomeTabla()
        {
           

            if (lb_Setor.Text == "Esmaltação")
            {
               return "Peças";
            }
            else
            {
                return "PEÇAS" + lb_Setor.Text;
            }
        }
        private void CargarGrafico(string Area, int Angulo)
        {
            
            DataTable Peças;   
            
            
            Peças=Banco.Procurar(EstablecerNomeTabla(),"*", "NomeCIP", "'" + cb_Filtrar.Text + "%' ","CIP");

            if ((Peças.Rows.Count > 0 && cb_Filtrar.Text != "") )
            {
                AssignartituloeSeries(Color.Green);
                AgregarDados(Peças,Area);
            }
            else
            {
                AssignartituloeSeries( Color.Red);
                AgregarDados(Peças,Area);

            }
                    COnfigurarAngulolabel(Angulo);


        }
        private void Graficar(int Angulo)
        {
            if (cb_QualEstoque.Text == "DESCARGA")
            {
                CargarGrafico("D",Angulo);
            }
            else
            {
                if (cb_QualEstoque.Text == "CARGA")
                {
                    CargarGrafico("C", Angulo);

                }
                else
                {
                    if (cb_QualEstoque.Text == "REFORMA")
                    {
                        CargarGrafico("R", Angulo);
                    }
                    else
                    {
                        if (cb_QualEstoque.Text == "PROCESSO")
                        {
                            CargarGrafico("C", Angulo);
                        }
                    }

                }
            }
        }
       
        private int Angulo()
        {
            if (cb_Filtrar.Text!="")
            {
                return 0;
            }
            else
            {
                return 90;
            }
        }

        #endregion;


        private void cb_Filtrar_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (cambio)
            {
               
                Graficar(Angulo());
            }
        }

        private void cb_QualEstoque_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cambio)
            {
                Graficar(Angulo());

            }
        } 
    }
    
}
