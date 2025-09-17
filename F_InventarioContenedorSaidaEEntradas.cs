using Microsoft.VisualBasic;
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
using System.Windows.Media.Media3D;


namespace Atlas_projeto
{
    public partial class F_InventarioContenedorSaidaEEntradas : Form
    {
        #region Variaveis Gerais
        DataTable DadosOperações;
        DataTable ResumoOperações;
        DataTable NuOperações;
        DataTable DataAux=new DataTable();
        DataTable DadosConteudo;
        DataTable dt6;
        DataTable ResumoConteiner;
        Form2 d;
        bool puede=false;
        Operação ope = new Operação();
        long conta;
        long total;
        int Turno;
        int contagemSalvas = 0;
        int contagemSalvasErros = 0;
        int contagemAtualizadas = 0;
        int contagemAtualizadasErros = 0;
        int Estado;
        DateTime fecha;
        bool populou = false;
        bool ComConteiner = true;
        bool Esmaltada = true;
        bool Entrada;
        string MassaPeça="0";
        string MassaContenedor= "0";
        string CICG="";
        string AreaUbiNormal;
        int[] IndicedePontos = new int[4];
        int[] Quantidades = new int[20];
        int[] IndicedeComas = new int[20];
        int[] IndicedeEspaço = new int[4];
        int[] IndiceFMCSV = new int[5];
        string CIP, CIT,D,O;
        string[] NomesCIP = new string[20];
        #endregion;
        public F_InventarioContenedorSaidaEEntradas(Form2 p, string setor)
        {
            InitializeComponent();
            d = p;
            lb_Setor.Text= setor;
        }

        private void F_InventarioContenedor_Load(object sender, EventArgs e)
        {
            #region Popular comboboxes
            #region Destino
            //Popular combobox Destino das peças
            Dictionary<string, string> Destino = new Dictionary<string, string>();

            Destino.Add("0", "ESTOQUE FINAL");
            Destino.Add("1", "ESTOQUE DE MATERIA PRIMA");
            Destino.Add("2", "ÁREA DE ANÁLISE DE PEÇAS");
            Destino.Add("3", "ESTOQUE DE RETRABALHO A");
            Destino.Add("4", "ESTOQUE DE RETRABALHO B");
            Destino.Add("5", "PROCESSO I");
            Destino.Add("6", "PROCESSO II");
            Destino.Add("7", "CLIENTE");
            Destino.Add("8", "CLIENTE SAC");
            Destino.Add("9", "FORNECEDOR");
            Destino.Add("10", "DESCARTE");



            cb_Destino.DataSource = new BindingSource(Destino, null);
            cb_Destino.DisplayMember = "Value";
            cb_Destino.ValueMember = "key";

            #endregion;

            #region Origem
            AssignarValoresComboBoxesSegundoOTipoDeOperação();
          
            #endregion;

            #region FamiliaProcurada
            // populando ComboBox FamiliaPeçaProcurada
            cb_FamiliaPeça.Items.Clear();
            cb_FamiliaPeça.DataSource = Banco.ObterTodos("Familias", "*", "IdFamilia");
            cb_FamiliaPeça.DisplayMember = "Familia";
            cb_FamiliaPeça.ValueMember = "IdFamilia";
            #endregion;

          
            #region CIC
            // populando ComboBox CIC
            cb_CIC.Items.Clear();
            cb_CIC.DataSource = Banco.ObterTodos("CadastroEspecificoContenedores", "*", "CICG, id asc");
            cb_CIC.DisplayMember = "CICE";
            cb_CIC.ValueMember = "CICE";
            #endregion;

            #region CICE
            // populando ComboBox CIC
            cb_NomeCICE.Items.Clear();
            cb_NomeCICE.DataSource = Banco.ObterTodos("CadastroEspecificoContenedores", "*", "CICG, id asc");
            cb_NomeCICE.DisplayMember = "NomeCIC";
            cb_NomeCICE.ValueMember = "CICE";
            #endregion;

          
           
            #endregion;

            #region NomeCIP
            //Popular combobox Nome CIP

            cb_PeçaProcurada.DataSource = Banco.ObterTodos("peças", "*", "CIP"); ;
            cb_PeçaProcurada.DisplayMember = "NomeCIP";
            cb_PeçaProcurada.ValueMember = "CIP";
            tb_CIP.Text = (string)cb_PeçaProcurada.SelectedValue;
            #endregion;

            #region ResponsavelM
            //Popular combobox Nome CIP

            cb_ResponsavelM.DataSource = Banco.ObterTodos("ResponsaveisMovimentaçoes", "*", "NomeResponsavel");
            cb_ResponsavelM.DisplayMember = "NomeResponsavel";
            cb_ResponsavelM.ValueMember = "NomeResponsavel";
            cb_ResponsavelM.SelectedValue = "";
            #endregion;

            #region NomeCIO
            cb_NomeCIO.DataSource = Banco.ObterTodos("CIO", "*", "CIO"); ;
            cb_NomeCIO.DisplayMember = "NomeCIO";
            cb_NomeCIO.ValueMember = "CIO";

            #endregion;

            #region CIO
            cb_CIO.DataSource = Banco.ObterTodos("CIO", "*", "CIO"); ;
            cb_CIO.DisplayMember = "CIO";
            cb_CIO.ValueMember = "CIO";

            #endregion;

            #region CIT
            cb_CIT.DataSource = Banco.ObterTodos("CIO", "*", "CIO"); ;
            cb_CIT.DisplayMember = "CIT";
            cb_CIT.ValueMember = "CIO";

            #endregion;



            AtualizarTodo();
           
            #region ConfigGerais

            AtualizarNdeOpe();
            DtDatosOperação();

            cb_CIO.Enabled = false;
            tb_CIP.Text = "0.0.0.0.0";
            cb_Destino.SelectedIndex = int.Parse(D);

            cb_OrigemDasPeças.SelectedValue = O;
            #endregion;


            populou = true;
        }


        #region PROCEDIMENTOS
    
        private void AssignarDadosOpeação()
        {
            string Conteudo = DadosOperações.Rows[DadosOperações.Rows.Count - 1].Field<string>("Conteudo");
            if (Conteudo=="0.0.0.0.0,")
            {
                Conteudo = "Nada";
            }
                ope.Data =DadosOperações.Rows[DadosOperações.Rows.Count-1].Field<string> ("Data");
                ope.Hora = DadosOperações.Rows[DadosOperações.Rows.Count - 1].Field<string>("Hora");
                ope.Rm = DadosOperações.Rows[DadosOperações.Rows.Count - 1].Field<string>("RM");
                ope.Rl = DadosOperações.Rows[DadosOperações.Rows.Count - 1].Field<string>("RL");
                ope.Turno = int.Parse(DadosOperações.Rows[DadosOperações.Rows.Count - 1].Field<string>("Turno"));
                ope.CICE = DadosOperações.Rows[DadosOperações.Rows.Count - 1].Field<string>("CICE");
                ope.CIO = DadosOperações.Rows[DadosOperações.Rows.Count - 1].Field<string>("CIO");
                ope.CIT =   DadosOperações.Rows[DadosOperações.Rows.Count - 1].Field<string>("CIT");
                ope.EstadoPeça = DadosOperações.Rows[DadosOperações.Rows.Count - 1].Field<string>("EstadoPeça");
                ope.Conteudo = DadosOperações.Rows[DadosOperações.Rows.Count - 1].Field<string>("Conteudo");
                ope.Quantidade = DadosOperações.Rows[DadosOperações.Rows.Count - 1].Field<string>("Quantidade");
                ope.TipodeOperação = DadosOperações.Rows[DadosOperações.Rows.Count - 1].Field<string>("TipodeOperação");
                ope.Origem = DadosOperações.Rows[DadosOperações.Rows.Count - 1].Field<string>("Origem");
                ope.Destino = DadosOperações.Rows[DadosOperações.Rows.Count - 1].Field<string>("Destino");
                ope.NúmerodeOperação = int.Parse(DadosOperações.Rows[DadosOperações.Rows.Count - 1].Field<string>("NúmerodeOperação"));
                ope.CO = cb_Destino.SelectedValue + ","+cb_OrigemDasPeças.SelectedValue.ToString();
               
        }
        private void SalvarOperções()
        {
            contagemSalvas = 0;
            bool salvou;
           salvou= Banco.Salvar("Operaçoes", "CO,CIT,CIO,EstadoPeça,Data, Hora, RM, RL,Turno, CICE, Conteudo, Quantidade, TipoDeOperação, Origem, Destino, NúmeroDeOperação", "'" + ope.CO + "', '" + ope.CIT + "', '"  + ope.CIO + "', '" + ope.EstadoPeça + "', '" + ope.Data + "', " + "'" + ope.Hora + "', " + "'" + ope.Rm + "', " + "'" + ope.Rl + "', " + "" + ope.Turno + ", " + "'" + ope.CICE + "', " + "'" + ope.Conteudo + "', '" + ope.Quantidade + "', " + "'" + ope.TipodeOperação + "', " + "'" + ope.Origem + "', " + "'" + ope.Destino + "', " + "" + ope.NúmerodeOperação + "");
            if (salvou)
            {
                contagemSalvas = 1;
            }
        }
        private void ExcluirOpeAnt()
        {
            DadosOperações = Banco.Procurar("Operaçoes", "*", "CICE", "'%" + tb_CódigoContenedor.Text + "%'", "Data asc");

            bool exc = Banco.Excluir("Operaçoes", "NúmeroDeOperação", DadosOperações.Rows[DadosOperações.Rows.Count-1].Field<string>("NúmeroDeOperação"));
            AtualizarNdeOpe();
        }
        private int EstablecerIndicesPontosCIP(string CIP)
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
            return m;
        }
        private int EstablecerIndicesComasCIP(string CIP)
        {
            string Buscado = ",";
            string c;

            int m = 0;

            for (int i = 0; i < CIP.Length; i++)
            {
                c = CIP.Substring(i, 1);
                if (c == Buscado)
                {

                    IndicedeComas[m] = i;
                    m += 1;

                }
            }
            return m;

        }
        private void EstablecerValorIndicesFMCV(string CIP)
        {
            EstablecerIndicesPontosCIP(CIP);
            EstablecerIndicesComasCIP(CIP);
            IndiceFMCSV[0] = int.Parse(CIP.Substring(0, IndicedePontos[0]));

            IndiceFMCSV[1] = int.Parse(CIP.Substring(IndicedePontos[0] + 1, IndicedePontos[1] - (IndicedePontos[0] + 1)));

            IndiceFMCSV[2] = int.Parse(CIP.Substring(IndicedePontos[1] + 1, IndicedePontos[2] - (IndicedePontos[1] + 1)));

            IndiceFMCSV[3] = int.Parse(CIP.Substring(IndicedePontos[2] + 1, IndicedePontos[3] - (IndicedePontos[2] + 1)));

            IndiceFMCSV[4] = int.Parse(CIP.Substring(IndicedePontos[3] + 1, IndicedeComas[0] - (IndicedePontos[3] + 1)));



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

        private string EstablecerNomeCIP(string CIP)
        {
            DataTable dt = Banco.Procurar(EstablecerNomeTabla(), "CIP, NomeCIP", "CIP", "'" + CIP + "%'", "CIP");// ver
            return dt.Rows[0].Field<string>("NomeCIP");

        }
        private string EstablecerCIP(string NomeCIP)
        {
            string x = "";
            DataTable dt = Banco.Procurar(EstablecerNomeTabla(), "CIP, NomeCIP", "NomeCIP", "'" + NomeCIP + "%'", "CIP");// ver
            return dt.Rows[0].Field<string>("CIP");

        }
        private string RestaurarCadenaConteudo()
        {
            string cadena="";
            foreach (DataRow row in DadosConteudo.Rows)
            {
                cadena += EstablecerCIP(row["Peça"].ToString()) + ",";
            }
            return cadena;
        }
        private string RestaurarQuantidade()
        {
            string Q = "";
            foreach (DataRow row in DadosConteudo.Rows)
            {
                Q += row["Quantidade"].ToString() + ",";
            }
            return Q;
        }
        private string EstablecerEstadoPeça()
        {
           DataTable dt= Banco.Procurar("CadastroEspecificoContenedores","EstadoPeça","CICE","'"+tb_CódigoContenedor.Text+"'","EstadoPeça");

            string Estado = dt.Rows[0].Field<string>("EstadoPeça");
            switch (Estado)
            {
                case "B":
                    return "CONFORME";
                case "S":
                    return "SUCATA";
                case "R":
                    return "RETRABALHO";
                case "E":
                    return "EM ANÁLISE";
                default:
                    return "";

            }
            
        }
        private void EstablecerCIPsEQuantidadeeNomeCIP(string CadenaDeCIPs, string CadenaQuantidade)
        {
            int m = EstablecerIndicesComasCIP(CadenaDeCIPs);

            DtResumoConteudo();

           
                for (int i = 0; i < m; i++)
                {
                    DataRow row= DadosConteudo.NewRow();
            
                    if (i==0)
                    {
                        EstablecerIndicesComasCIP(CadenaDeCIPs);
                        CIP= CadenaDeCIPs.Substring(0, IndicedeComas[i]);
                        row["Peça"] = EstablecerNomeCIP(CIP);
                        CIP = CIP + ",";
                        EstablecerIndicesComasCIP(CadenaQuantidade);
                        row["Quantidade"] = int.Parse(CadenaQuantidade.Substring(0, IndicedeComas[i]));
                        row["Estado"] = EstablecerEstadoPeça();
                        DadosConteudo.Rows.Add(row);

                    }
                    else
                    {
                        EstablecerIndicesComasCIP(CadenaDeCIPs);
                        row["Peça"] = EstablecerNomeCIP(CadenaDeCIPs.Substring(IndicedeComas[i-1] + 1, IndicedeComas[i] - (IndicedeComas[i-1] + 1)));
                        EstablecerIndicesComasCIP(CadenaQuantidade);
                        row["Quantidade"] = int.Parse(CadenaQuantidade.Substring(IndicedeComas[i-1] + 1, IndicedeComas[i] - (IndicedeComas[i-1] + 1)));
                        row["Estado"] = EstablecerEstadoPeça();
                        DadosConteudo.Rows.Add(row);
                       
                    }
                   
                }

            
            
        }
        private void AtualizardgvResumoConteudo()
        {
         DataTable dt= Banco.Procurar("CadastroEspecificoContenedores", "PEÇAcontida as CIP, CapUSada as Quantidade", "CICE", "'" + tb_CódigoContenedor.Text + "%'", "CICE");// ver
           
            if (dt.Rows.Count>0)
            {
                string CIP = dt.Rows[0].Field<string>("CIP");
                string V = dt.Rows[0].Field<string>("Quantidade");

                
                if (CIP!="Nada")
                {
                    EstablecerCIPsEQuantidadeeNomeCIP(CIP, V);
                    EstablecerValorIndicesFMCV(this.CIP);
                    cb_FamiliaPeça.SelectedIndex = IndiceFMCSV[0];                 
                    cb_PeçaProcurada.ValueMember = "CIP";
                    cb_PeçaProcurada.SelectedValue = this.CIP.Substring(0, this.CIP.Length-1);
                   
                    dgv_ResumoConteudo.DataSource = DadosConteudo;
                    cb_EstadoPeça.Text = DadosConteudo.Rows[0].Field<string>("Estado");
                    nud_Quantidade.Value = int.Parse(DadosConteudo.Rows[0].Field<string>("Quantidade"));
                }
                else
                {
                    Zerarconteudo();
                    cb_FamiliaPeça.SelectedIndex = 0;
                    cb_PeçaProcurada.ValueMember = "CIP";
                    cb_PeçaProcurada.SelectedValue = 1048;                   
                    dgv_ResumoConteudo.DataSource = DadosConteudo;
                    cb_EstadoPeça.Text = "";
                    nud_Quantidade.Value = 0;

                }
               
            }
            else
            {
                Zerarconteudo();
                dgv_ResumoConteudo.DataSource =DadosConteudo ;
                cb_FamiliaPeça.SelectedIndex = 0;
                nud_Quantidade.Value = 0;
            }

        }
        private void AtualizarTodo()
        {
            AtualizardgvResumoConteudo();
            AtualizarDgvResumoContêiner();
            AtualizaResumoOperaçoes();

        }
        private void Zerarconteudo()
        {
            DtResumoConteudo();
            DataRow row = DadosConteudo.NewRow();

            row["Peça"] = "Nada";
            row["Quantidade"] = "0";
            row["Estado"] = "--";
            DadosConteudo.Rows.Add(row);

        }
        private void AtualizarDgvResumoContêiner()
        {
            
            DataTable dt = Banco.Procurar("CadastroEspecificoContenedores", "CICE,ESTADO,CONDIÇOES, UbicaçãoAtual", "CICE", "'%" + tb_CódigoContenedor.Text + "%'", "CICE");
            if (dt.Rows.Count>0)
            {
                DtResumoConteiner();
                DataRow row = ResumoConteiner.NewRow();
                row["CICE"] = tb_CódigoContenedor.Text;
                row["ESTADO"] = EstablecerValorEstado(Convert.ToInt32(dt.Rows[0].Field<Int64>("ESTADO")));
                row["CONDIÇÃO"] = EstablecerValorCondição(Convert.ToInt32(dt.Rows[0].Field<Int64>("CONDIÇOES")));
                row["UbicaçãoAtual"] = EstablecerValorUA(Convert.ToInt32(dt.Rows[0].Field<Int64>("UbicaçãoAtual")));
                ResumoConteiner.Rows.Add(row);
                dgv_ResumoConteiner.DataSource = ResumoConteiner;
            }
            else
            {
                
                dgv_ResumoConteiner.DataSource = DtResumoConteiner(); 
            }
        }
        private void AtualizaResumoOperaçoes()
        {          
            DataTable dt = Banco.Procurar("Operaçoes", "CIT, TipodeOperação, Data, Hora, Turno, RM, RL", "CICE", "'%" + tb_CódigoContenedor.Text + "%'", "Data asc");
            DadosOperações= Banco.Procurar("Operaçoes", "*", "CICE", "'%" + tb_CódigoContenedor.Text + "%'", "Data asc");

            if (dt.Rows.Count<6 && dt.Rows.Count>0)
            {
                ResumoOperações = dt;
                dgv_GestãoDeInventarios.DataSource = dt;
                
            }
            else
            {
                if (dt.Rows.Count>0)
                {
                    for (int i = dt.Rows.Count; i == dt.Rows.Count - 5; i--)
                    {
                        DataRow row = ResumoOperações.NewRow();
                        row["CIT"] = dt.Rows[i].Field<string>("CIT");
                        row["TipodeOperação"] = dt.Rows[i].Field<string>("TipodeOperação");                        
                        row["Data"] = dt.Rows[i].Field<string>("Data");
                        row["Hora"] = dt.Rows[i].Field<string>("Hora");
                        row["Turno"] = dt.Rows[i].Field<string>("Turno");
                        row["RM"] = dt.Rows[i].Field<string>("RM");
                        row["RL"] = dt.Rows[i].Field<string>("RL");
                        ResumoOperações.Rows.Add(row);
                    }
                    dgv_GestãoDeInventarios.DataSource = ResumoOperações;
                }
                else
                {
                    dgv_GestãoDeInventarios.DataSource = DtResumoOperaçoes();
                }
            }
            if (DadosOperações.Rows.Count>0&&cb_CIO.SelectedValue!=null)
            {
               
                EstablecerSigCIT();
                EstablecerDestinoEorigememFuncCIO(cb_CIO.SelectedValue.ToString());
               
            }
           
        }
        private void EstablecerSigCIT()
        { string cIT = DadosOperações.Rows[DadosOperações.Rows.Count - 1].Field<string>("CIT");
            int x = int.Parse(cIT.Substring(0, 1));
            string CO = DadosOperações.Rows[DadosOperações.Rows.Count - 1].Field<string>("CO");
            EstablecerIndicesComasCIP(CO);
            O = CO.Substring(IndicedeComas[0] + 1, CO.Length - (IndicedeComas[0] + 1));
            if (x < 4 && x > 0)
            {
                string y = cIT.Substring(2, 1);
                x += 1;
                DataAux = Banco.Procurar2Criterios("CIO", "*", "CIT", "CIT","'" + x + "%'", "'%" + y + "'","CIO");
                if (DataAux.Rows.Count > 0)
                {
                    cb_CIO.SelectedValue = DataAux.Rows[0].Field<string>("CIO");
                    cb_CIT.SelectedValue = DataAux.Rows[0].Field<string>("CIT");
                }

            }
            else
            {
                string y = cIT.Substring(2, 1);
                x =1;
                DataAux = Banco.Procurar2Criterios("CIO", "*", "CIT", "CIT", "'" + x + "%'", "'%" + y + "'", "CIO");
                if (DataAux.Rows.Count > 0)
                {
                    cb_CIO.SelectedValue = DataAux.Rows[0].Field<string>("CIO");
                    cb_CIT.SelectedValue = DataAux.Rows[0].Field<string>("CIT");
                }

            }

        }
        private string EstablecerValorEstado(int Estado)
        {
            if (Estado == 0)
            {
                return "DISPONIVEL";
            }
            else
            {
                if (Estado == 1)
                {
                    return "EM USO";
                }
                else
                {
                    if (Estado == 2)
                    {
                        return "EM MANUTENÇÃO";
                    }
                    else
                    {
                        if (Estado == 3)
                        {
                            return "DESCARTADO";
                        }
                        else
                        {
                            if (Estado == 4)
                            {
                                return "FORA DE USO";
                            }
                            else
                            {
                                return "Desconhecido";
                            }
                        }
                    }
                }
            }
                
        }
        private string EstablecerValorCondição(int Condição)
        {
            if (Condição == 0)
            {
                return "ÓTIMO";
            }
            else
            {
                if (Condição == 1)
                {
                    return "DETERIORADO";
                }
                else
                {
                    if (Condição == 2)
                    {
                        return "DANIFICADO";
                    }
                    else
                    {
                        if (Condição == 3)
                        {
                            return "ESTRAGADO";
                        }
                        else
                        {
                           
                                return "Desconhecida";
                           
                        }
                    }
                }
            }

        }
        private string EstablecerValorUA(int UA)
        {
            if (UA==0)
            {
                return "ESTOQUE FINAL";
            }
            else
            {
                if (UA == 1)
                {
                    return "ESTOQUEMATERIA PRIMA";
                }
                else
                {
                    if (UA == 2)
                    {
                        return "ÁREA DE ANÁLISE DE PEÇAS";
                    }
                    else
                    {
                        if (UA == 3)
                        {
                            return "ESTOQUE DE RETRABALHO A";
                        }
                        else
                        {
                            if (UA == 4)
                            {
                                return "ESTOQUE DE RETRABALHO B";
                            }
                            else
                            {
                                if (UA == 5)
                                {
                                    return "PROCESSO I";
                                }
                                else
                                {
                                    if (UA == 6)
                                    {
                                        return "PROCESSO II";
                                    }
                                    else
                                    {
                                        if (UA == 7)
                                        {
                                            return "CLIENTE";
                                        }
                                        else
                                        {
                                            if (UA == 8)
                                            {
                                                return "CLIENTE SAC";
                                            }
                                            else
                                            {
                                                if (UA == 9)
                                                {
                                                    return "FORNECEDOR";
                                                }
                                                else
                                                {
                                                    if (UA == 10)
                                                    {
                                                        return "DESCARTE";
                                                    }
                                                    else
                                                    {
                                                        if (UA == 11)
                                                        {
                                                            return "MANUTENÇÃO";
                                                        }
                                                        else
                                                        {
                                                            if (UA == 12)
                                                            {
                                                                return "DEPÓSITO";
                                                            }
                                                            else
                                                            {
                                                                return "Desconhecida";
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
                    }
                }
            }
        }
        private void EstablecerNumeroDeOperaçõesCadastradas()
        {
            DtNuOpeçoes();
            DataTable dt = Banco.ObterTodos("Operaçoes");
            ArrayList array = new ArrayList();
            foreach (DataRow dr in dt.Rows)
            {
                object total;
                int Operação = int.Parse((string)dr["NúmeroDeOperação"]);
                if (array.IndexOf(Operação) < 0)
                {
                    total = dt.Compute(String.Format("Count(NúmeroDeOperação)"), "NúmeroDeOperação like '" + Operação + "'");

                    NuOperações.Rows.Add(new object[] { dr["NúmeroDeOperação"], Convert.ToInt32(total) });


                    array.Add(Operação);
                }
            }

        }
        private void AssignarValoresComboBoxesSegundoOTipoDeOperação()
        {
            #region ENTRADAS
            lb_Origem.Text = "Origem";
                lb_Destino.Text = "Destino";
                

                if (cb_Destino.Text == "ESTOQUE FINAL")
                {
                    //Popular combobox Destino das peças
                    Dictionary<string, string> Origem = new Dictionary<string, string>();
                    Origem.Add("6", "PROCESSO II");
                    Origem.Add("7", "CLIENTE");
                    Origem.Add("8", "CLIENTE SAC");
                    Origem.Add("2", "ÁREA DE ANÁLISE DE PEÇAS");


                    cb_OrigemDasPeças.DataSource = new BindingSource(Origem, null);
                    cb_OrigemDasPeças.DisplayMember = "Value";
                    cb_OrigemDasPeças.ValueMember = "key";

                }
                else
                {
                    if (cb_Destino.Text == "ESTOQUE DE MATERIA PRIMA")
                    {
                       

                        //Popular combobox Destino das peças
                        Dictionary<string, string> Origem = new Dictionary<string, string>();
                        Origem.Add("9", "FORNECEDOR");
                        Origem.Add("2", "           ");
                        Origem.Add("5", "PROCESSO I");
                       


                        cb_OrigemDasPeças.DataSource = new BindingSource(Origem, null);
                        cb_OrigemDasPeças.DisplayMember = "Value";
                        cb_OrigemDasPeças.ValueMember = "key";

                    }
                    else
                    {
                        if (cb_Destino.Text == "ESTOQUE DE RETRABALHO B")
                        {
                           
                            //Popular combobox Destino das peças
                            Dictionary<string, string> Origem = new Dictionary<string, string>();

                            Origem.Add("2", "ÁREA DE ANÁLISE DE PEÇAS");                         


                            cb_OrigemDasPeças.DataSource = new BindingSource(Origem, null);
                            cb_OrigemDasPeças.DisplayMember = "Value";
                            cb_OrigemDasPeças.ValueMember = "key";

                        }
                        else
                        {
                            if (cb_Destino.Text == "ESTOQUE DE RETRABALHO A")
                            {
                                

                                //Popular combobox Destino das peças
                                Dictionary<string, string> Origem = new Dictionary<string, string>();

                                Origem.Add("2", "ÁREA DE ANÁLISE DE PEÇAS");

                                cb_OrigemDasPeças.DataSource = new BindingSource(Origem, null);
                                cb_OrigemDasPeças.DisplayMember = "Value";
                                cb_OrigemDasPeças.ValueMember = "key";

                            }
                            else
                            {
                                if (cb_Destino.Text == "PROCESSO I")//5,6
                                {                                    

                                    //Popular combobox Destino das peças
                                    Dictionary<string, string> Origem = new Dictionary<string, string>();

                                    Origem.Add("1", "ESTOQUE DE MATERIA PRIMA");
                                    Origem.Add("4", "ESTOQUE DE RETRABALHO A");
                                    Origem.Add("3", "ESTOQUE DE RETRABALHO B");
                                    Origem.Add("2", "ÁREA DE ANÁLISE DE PEÇAS");                                                                     

                                    cb_OrigemDasPeças.DataSource = new BindingSource(Origem, null);
                                    cb_OrigemDasPeças.DisplayMember = "Value";
                                    cb_OrigemDasPeças.ValueMember = "key";

                                }
                                else
                                {
                                    if (cb_Destino.Text =="ÁREA DE ANÁLISE DE PEÇAS")
                                    {

                                        //Popular combobox Destino das peças
                                        Dictionary<string, string> Origem = new Dictionary<string, string>();
                                        Origem.Add("0", "ESTOQUE FINAL");
                                        Origem.Add("1", "ESTOQUE DE MATERIA PRIMA");
                                        Origem.Add("3", "ESTOQUE DE RETRABALHO A");
                                        Origem.Add("4", "ESTOQUE DE RETRABALHO B");
                                        Origem.Add("5", "PROCESSO I");
                                        Origem.Add("6", "PROCESSO II");
                                        Origem.Add("7", "CLIENTE");
                                        Origem.Add("8", "CLIENTE SAC");                                       
                                        Origem.Add("9", "FORNECEDOR");
                                        Origem.Add("10", "DESCARTE");




                                    cb_OrigemDasPeças.DataSource = new BindingSource(Origem, null);
                                        cb_OrigemDasPeças.DisplayMember = "Value";
                                        cb_OrigemDasPeças.ValueMember = "key";
                                    }
                                    else
                                    {
                                        if (cb_Destino.Text == "DESCARTE")
                                        {

                                            //Popular combobox Destino das peças
                                            Dictionary<string, string> Origem = new Dictionary<string, string>();
                                            Origem.Add("2", "ÁREA DE ANÁLISE DE PEÇAS");



                                        cb_OrigemDasPeças.DataSource = new BindingSource(Origem, null);
                                            cb_OrigemDasPeças.DisplayMember = "Value";
                                            cb_OrigemDasPeças.ValueMember = "key";
                                        }
                                        else
                                        {
                                            if (cb_Destino.Text == "CLIENTE"|| cb_Destino.Text == "CLIENTE SAC")
                                            {

                                                //Popular combobox Destino das peças
                                                Dictionary<string, string> Origem = new Dictionary<string, string>();
                                                Origem.Add("0", "ESTOQUE FINAL");


                                                cb_OrigemDasPeças.DataSource = new BindingSource(Origem, null);
                                                cb_OrigemDasPeças.DisplayMember = "Value";
                                                cb_OrigemDasPeças.ValueMember = "key";
                                            }
                                            else
                                            {
                                                if (cb_Destino.Text == "FORNECEDOR")
                                                {

                                                    //Popular combobox Destino das peças
                                                    Dictionary<string, string> Origem = new Dictionary<string, string>();
                                                    Origem.Add("2", "ÁREA DE ANÁLISE DE PEÇAS");
                                                    Origem.Add("1", "ESTOQUE DE MATERIA PRIMA");

                                                    cb_OrigemDasPeças.DataSource = new BindingSource(Origem, null);
                                                    cb_OrigemDasPeças.DisplayMember = "Value";
                                                    cb_OrigemDasPeças.ValueMember = "key";
                                                }
                                                else 
                                                {
                                                    if (cb_Destino.Text == "PROCESSO II")
                                                    {
                                                        //Popular combobox Destino das peças
                                                         Dictionary<string, string> Origem = new Dictionary<string, string>();

                                                        Origem.Add("0", "ESTOQUE FINAL");
                                                        Origem.Add("4", "ESTOQUE DE RETRABALHO B");
                                                        Origem.Add("3", "ESTOQUE DE RETRABALHO A");
                                                        Origem.Add("2", "ÁREA DE ANÁLISE DE PEÇAS");

                                                        cb_OrigemDasPeças.DataSource = new BindingSource(Origem, null);
                                                        cb_OrigemDasPeças.DisplayMember = "Value";
                                                        cb_OrigemDasPeças.ValueMember = "key";
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                    }




               // }
                #endregion;
            }

        }
        private void AssignarValorTurno()
        {
            #region AssignandoValorTurno
            fecha = DateTime.Now;
            DateTime ini1turno = new DateTime(2021, 01, 01, 05, 15, 00);
            DateTime Fim1turno = new DateTime(2021, 01, 01, 15, 10, 00);
            DateTime ini2turno = new DateTime(2021, 01, 01, 15, 10, 00);
            DateTime Fim2turno = new DateTime(2021, 01, 01, 00, 40, 00);        


            if (fecha.Hour > ini1turno.Hour && fecha.Hour < Fim1turno.Hour)
            {
                Turno = 1;
            }
            else
            {
                if (fecha.Hour > ini2turno.Hour && fecha.Hour < 24)
                {
                    Turno = 2;
                }
                else
                {
                    if (fecha.Hour == 0 && fecha.Minute < 40)
                    {
                        Turno = 2;
                    }
                    else
                       Turno = 3;
                }
            }

            #endregion;
        }
        private void AssiganarValoresDtOperaoes()
        {
            DataRow row = DadosOperações.NewRow();
            row["NúmerodeOperação"] = int.Parse(tb_NúmerodeOperação.Text);
            row["CIO"] = cb_CIO.SelectedValue.ToString();
            row["CIT"] = cb_CIT.Text;
            row["TipodeOperação"] = cb_NomeCIO.Text;

            if (lb_Destino.Text == "Destino")
            {
                row["Origem"] = cb_OrigemDasPeças.Text;
                row["Destino"] = cb_Destino.Text;
            }
            else
            {
                row["Origem"] = cb_Destino.Text;
                row["Destino"] = cb_OrigemDasPeças.Text;
            }

            row["CICE"] = tb_CódigoContenedor.Text;
            if ((DadosConteudo.Rows[0].Field<string>("Peça") == "Nada" && !gb_DC.Enabled))
            {
                row["Conteudo"] = "Nada";
            }
            else
            {
                if (DadosConteudo.Rows.Count > 1 && gb_DC.Enabled)
                {
                    row["Conteudo"] = RestaurarCadenaConteudo() + tb_CIP.Text + ",";

                }
                else
                {
                    if (DadosConteudo.Rows.Count == 1 && gb_DC.Enabled)
                    {

                        row["Conteudo"] = tb_CIP.Text + ",";

                    }
                    else
                    {
                        row["Conteudo"] = RestaurarCadenaConteudo();
                    }
                }

            }


            if (DadosConteudo.Rows[0].Field<string>("Peça") == "Nada" && !gb_DC.Enabled)
            {
                row["Quantidade"] = "0,";
            }
            else
            {
                if (DadosConteudo.Rows.Count > 1 && gb_DC.Enabled)
                {
                    row["Quantidade"] = RestaurarQuantidade() + nud_Quantidade.Value + ",";

                }
                else
                {
                    if (DadosConteudo.Rows.Count == 1 && gb_DC.Enabled)
                    {

                        row["Quantidade"] = +nud_Quantidade.Value + ",";

                    }
                    else
                    {
                        row["Quantidade"] = RestaurarQuantidade();
                    }
                }

            }
            row["EstadoPeça"] = cb_EstadoPeça.Text;
            row["Data"] = fecha.ToShortDateString();
            row["Hora"] = fecha.ToShortTimeString();
            row["Turno"] = Turno;
            row["RM"] = cb_ResponsavelM.Text;
            row["RL"] = d.lb_nome.Text;
            DadosOperações.Rows.Add(row);
            DataRow row2 = ResumoOperações.NewRow();
            row2["CIT"] = cb_CIT.Text;
            row2["TipodeOperação"] = cb_NomeCIO.Text;
            row2["Data"] = fecha.ToShortDateString();
            row2["Hora"] = fecha.ToShortTimeString();
            row2["Turno"] = Turno;
            row2["RM"] = cb_ResponsavelM.Text;
            row2["RL"] = d.lb_nome.Text;
            ResumoOperações.Rows.Add(row2);

            dgv_GestãoDeInventarios.DataSource = ResumoOperações;
            puede = true;
        }
        private void AgregarAlDataTable()
        {
            int x;
            int y;
            string a;
            string b;
            string m;
            if (ResumoOperações.Rows.Count==0)
            {
                x = 0;
                y = int.Parse(cb_CIT.Text.Substring(0, 1));
                a = "";
                b = "";
                m = cb_CIT.Text.Substring(1, 1);
            }
            else
            {
                x = int.Parse(ResumoOperações.Rows[ResumoOperações.Rows.Count - 1].Field<string>("CIT").Substring(0, 1));
                y = int.Parse(cb_CIT.Text.Substring(0, 1));
                a = ResumoOperações.Rows[ResumoOperações.Rows.Count - 1].Field<string>("CIT").Substring(2, 1);
                b= cb_CIT.Text.Substring(2, 1);
                m= cb_CIT.Text.Substring(1, 1);
            }
           

            if (cb_ResponsavelM.Text!="")
            {
               
                if ((Math.Abs(y-x)==1|| Math.Abs(y - x) == 3 || Math.Abs(y - x) == 4) && a==b && a=="D")
                {
                    AssiganarValoresDtOperaoes();
                }
                else
                {
                    if (Math.Abs(y - x) == 1 && a == b && a == "C" && m != "*")
                    {
                        AssiganarValoresDtOperaoes();
                    }
                    else
                    {
                        if (y == 5 && m == "." && a == b && a == "C")
                        {
                            AssiganarValoresDtOperaoes();
                        }
                        else
                        {
                            if (y == 4 && m == "*" && a == b && a == "C")
                            {
                                AssiganarValoresDtOperaoes();
                            }
                            else
                            {
                                MessageBox.Show("Não foi possivel realizar esta operação devido a que não segue a sequência lógica de operções", "Operação deve seguir secuência lógica do CIT!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                puede = false;
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Deve escolher um responsavel pela movimentação do contêiner", "Responsavel Pela Movimentação é Obrigatorio", MessageBoxButtons.OK, MessageBoxIcon.Stop);

               
            }
        }
        private DataTable DtResumoOperaçoes()
        {
            #region Prencher DGV

            //Popular DataGriwView
            ResumoOperações = new DataTable();
            ResumoOperações.Columns.Add("CIT");
            ResumoOperações.Columns.Add("TipodeOperação");           
            ResumoOperações.Columns.Add("Data");
            ResumoOperações.Columns.Add("Hora");
            ResumoOperações.Columns.Add("Turno");
            ResumoOperações.Columns.Add("RM");
            ResumoOperações.Columns.Add("RL");
            #endregion;
            return ResumoOperações;
        }
        private DataTable DtDatosOperação()
        {
            #region Prencher DGV

            //Popular DataGriwView
            DadosOperações = new DataTable();           
            DadosOperações.Columns.Add("NúmeroDeOperação");
            DadosOperações.Columns.Add("CIO");
            DadosOperações.Columns.Add("TipodeOperação");
            DadosOperações.Columns.Add("CIT");
            DadosOperações.Columns.Add("Origem");
            DadosOperações.Columns.Add("Destino");
            DadosOperações.Columns.Add("CICE");
            DadosOperações.Columns.Add("Conteudo");
            DadosOperações.Columns.Add("Quantidade");
            DadosOperações.Columns.Add("EstadoPeça");      
            DadosOperações.Columns.Add("Data");
            DadosOperações.Columns.Add("Hora");
            DadosOperações.Columns.Add("Turno");
            DadosOperações.Columns.Add("RM");
            DadosOperações.Columns.Add("RL");

            return DadosOperações;    

            #endregion;
            
        }
        private void DtNuOpeçoes()
        {
            NuOperações = new DataTable();
            NuOperações.Columns.Add("NúmeroDeOperação");
            NuOperações.Columns.Add("Quantidade");
        }
        private bool ProcurarAreaDeConteiner(string CICE, char Procurado)
        {
           
            foreach (char x in CICE)
            {
                if (x==Procurado)
                {
                    return true;
                }
            }

            return false;
        }
        private void DtResumoConteudo()
        {
            DadosConteudo = new DataTable();
            DadosConteudo.Columns.Add("Peça");
            DadosConteudo.Columns.Add("Quantidade");
            DadosConteudo.Columns.Add("Estado");
           
        }      

        private DataTable DtResumoConteiner()
        {
            ResumoConteiner = new DataTable();
            ResumoConteiner.Columns.Add("CICE");
            ResumoConteiner.Columns.Add("ESTADO");
            ResumoConteiner.Columns.Add("CONDIÇÃO");
            ResumoConteiner.Columns.Add("UbicaçãoAtual");
            return ResumoConteiner;
        }
        private void EstablecerAreaDeUbicaçãoNormal()
        {
            int ArUbiNor = Convert.ToInt32(cb_Destino.SelectedValue);
            switch (ArUbiNor)
            {
                case 0:
                    AreaUbiNormal = "D";
                    break;

                case 1:
                    AreaUbiNormal = "C";
                    break;
                case 2:
                    AreaUbiNormal = "R";
                    break;
                case 3:
                    AreaUbiNormal = "R";
                    break;
                case 4:
                    AreaUbiNormal = "R";
                    break;
                case 5:
                    AreaUbiNormal = "C*";
                    break;
                case 6:
                    AreaUbiNormal = "D*";
                    break;
                case 7:
                    AreaUbiNormal = "D*";
                    break;
                case 8:
                    AreaUbiNormal = "D*";
                    break;
                case 9:
                    AreaUbiNormal = "C*";
                    break;
                case 10:
                    AreaUbiNormal = "R*";
                    break;
                          

                   

            }
        }
        private void AsignarEstado(string CICG, string Quantidade)
        {
            EstablecerAreaDeUbicaçãoNormal();
            if (CICG.Contains(AreaUbiNormal) && Quantidade == "0,")
            {
                Estado = 0;
            }
            else
            {
                Estado = 1;
              
            }
        }
      
        private void SalvarOuAtualizarSaidaEntrada(string Quantidade, string Conteudo ,string CICE, string TipoDeOperação, int NovaUbicação, string CICG)
        {   
            contagemAtualizadas = 0;
            contagemAtualizadasErros = 0;
            bool Salvou2;
            AsignarEstado(CICG,Quantidade);
          
            DataTable dt = Banco.ObterTodosOnde("CadastroEspecificoContenedores", "CICE", "'" + CICE + "'");
            if (Estado==0|| cb_CIT.Text.Contains("*")) 
            {
                if (dt.Rows.Count > 0)
                {
                    if (cb_CIT.Text.Contains("*"))
                    {
                        Banco.ExcluirDirecto("Operaçoes", "CICE", "'" + CICE + "'");
                        DtDatosOperação();

                    }

                    Salvou2 = Banco.Atualizar("CadastroEspecificoContenedores", "PEÇAcontida= 'Nada', CapUsada= '0,', UbicaçãoAtual='"+NovaUbicação+"', Estado= 0, CICE_E= '"+CICG+"_0'", "CICE", "'" + CICE + "'");
                    AtualizardgvResumoConteudo();
                    AtualizarDgvResumoContêiner();

                }
                else
                {
                    MessageBox.Show("Conteiner não cadastrado");
                    Salvou2 = false;
                   
                }

            }
            else
            {

                if (dt.Rows.Count > 0)
                {
                    Salvou2 = Banco.Atualizar("CadastroEspecificoContenedores", "PEÇAcontida= '" + Conteudo + "', CapUsada= '" + Quantidade + "', UbicaçãoAtual=" + NovaUbicação + ", Estado= " + Estado + ", CICE_E= '" + CICG + "_" + Estado + "'", "CICE", "'" + CICE + "'");
                    AtualizardgvResumoConteudo();
                    AtualizarDgvResumoContêiner();
                }
                else
                {
                    MessageBox.Show("Contener Não Cadastrado");
                    Salvou2 = false;

                }


            }

            if (Salvou2)
            {
                contagemAtualizadas += 1;
            }
            else
            {
                contagemAtualizadasErros += 1;
            
            }


        }

        private void AsignarValoresMassasECICG()
        {
            DataTable dt = Banco.Procurar("Peças", "CIP,MSE,ME", "CIP", "'" + tb_CIP.Text + "'", "CIP");
            DataTable dt2 = Banco.Procurar("CadastroEspecificoContenedores", "*", "CICE", "'" + tb_CódigoContenedor.Text + "'", "CICE");
            if (dt2.Rows.Count > 0)
            {
                CICG = dt2.Rows[0].Field<string>("CICG");
                DataTable dt3 = Banco.Procurar("CadastroGeralContenedores", "CICG,Massa", "CICG", "'" + CICG + "'", "CICG");

                if (dt.Rows.Count > 0)
                {
                    if (Esmaltada)
                    {
                        MassaPeça = dt.Rows[0].Field<string>("ME");
                        MassaContenedor = dt3.Rows[0].Field<string>("Massa");
                    }
                    else
                    {
                        MassaPeça = dt.Rows[0].Field<string>("MSE");
                        MassaContenedor = dt3.Rows[0].Field<string>("Massa");
                    }
                }
            }
        }
        private void AtualizarNdeOpe()
        {
            DataTable dt = Banco.ObterTodos("Operaçoes");
            tb_NúmerodeOperação.Text = (dt.Rows.Count + 1).ToString();

        }

        private void EstablecerDestinoEorigememFuncCIO(string CIO)
        {
          
            string CO;
            DataTable dt = Banco.Procurar("CIO","*","CIO","'"+CIO+"'","CIO");
            if (dt.Rows.Count>0)
            {
                CO = dt.Rows[0].Field<string>("CO");

                dt = Banco.Procurar("CIO", "*", "CO", "'%" + CO + "%'", "CIO");
                if (dt.Rows.Count > 0)
                {
                    EstablecerIndicesComasCIP(CO);
                    D = CO.Substring(0, IndicedeComas[0]);
                    O = CO.Substring(IndicedeComas[0] + 1, CO.Length - (IndicedeComas[0] + 1));
                  
                    cb_Destino.SelectedIndex = int.Parse(D);

                    cb_OrigemDasPeças.SelectedValue = O;

                    cb_NomeCIO.SelectedValue = cb_CIO.SelectedValue;
                    cb_CIT.SelectedValue = cb_CIO.SelectedValue;

                    if (DadosConteudo.Rows[0].Field<string>("Peça") != "Nada")
                    {
                        cb_PeçaProcurada.Text = DadosConteudo.Rows[0].Field<string>("Peça");
                        cb_EstadoPeça.Text = DadosConteudo.Rows[0].Field<string>("Estado");
                    }
                    AssignarValoresComboBoxesSegundoOTipoDeOperação();
                }
                else { MessageBox.Show("Operação invalida", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop); }
              
               
            }
        }
        #endregion;

        #region BTN CLICK
        private void btn_AgregarALista_Click(object sender, EventArgs e)
        {
            

            if (dgv_GestãoDeInventarios.Rows.Count>0)
            {
                ExcluirOpeAnt();
                AtualizaResumoOperaçoes();
            }
        }
        private void btn_Facturar_Click(object sender, EventArgs e)
        {
            
            if (tb_NúmerodeOperação.Text != "")
            {
                if (cb_PeçaProcurada.Text != "" || (cb_CIT.Text.Contains("_") && DadosConteudo.Rows[0].Field<string>("Peça")=="Nada"))
                {
                    if (tb_CódigoContenedor.Text != "" /*|| (cb_TipoDeOperação.Text == "ENTRADA" )*/)
                    {
                        if (nud_Quantidade.Value > 0 || (cb_CIT.Text.Contains("_") && DadosConteudo.Rows[0].Field<string>("Peça") == "Nada"))
                        {
                            if (cb_EstadoPeça.Text != "" || (cb_CIT.Text.Contains("_") && DadosConteudo.Rows[0].Field<string>("Peça") == "Nada"))
                            {
                                contagemAtualizadasErros = 0;
                                contagemAtualizadas = 0;
                                AssignarValorTurno();
                                AgregarAlDataTable();
                                if (DadosOperações.Rows.Count>0&&puede)
                                {

                                    AssignarDadosOpeação();


                                    DataTable dt = Banco.ObterTodosOnde("CadastroEspecificoContenedores", "CICE", "'" + ope.CICE + "'");
                                    CICG = dt.Rows[0].Field<string>("CICG");
                                    int X = Convert.ToInt32(cb_Destino.SelectedValue);                                   
                                    if (DadosOperações.Rows.Count > 0)
                                    {
                                        SalvarOuAtualizarSaidaEntrada(ope.Quantidade + ",", ope.Conteudo, ope.CICE, ope.TipodeOperação, X, CICG);

                                    }


                                    SalvarOperções();

                                    AtualizarTodo();
                                    AtualizarNdeOpe();
                                   // MessageBox.Show("Dados Salvos= " + contagemAtualizadas + "\n\nErros= " + contagemAtualizadasErros + "\n\nOperação Salvas= " + contagemSalvas + "\n\nErros= " + (Math.Abs( contagemSalvas-1)),"Resumo",MessageBoxButtons.OK,MessageBoxIcon.Information);

                                }
                            }
                            else { MessageBox.Show("Deve Indicar o Estado das Peças"); cb_EstadoPeça.Focus(); }
                        }
                        else { MessageBox.Show("Deve colocar uma Quantidade!"); nud_Quantidade.Focus(); }
                    }
                    else { MessageBox.Show("Deve Escolher um código de contenedor"); tb_CódigoContenedor.Focus(); }
                }
                else { MessageBox.Show("Campo \"Nome CIP\" não pode estar vazio"); cb_PeçaProcurada.Focus(); }
            }
            else { MessageBox.Show("Campo \"Número de Operção\" não pode estar vazio"); tb_CódigoContenedor.Focus(); }
        }
        private void btn_Voltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btn_Sair_Click(object sender, EventArgs e)
        {
            Globais.Sair();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            dgv_GestãoDeInventarios.DataSource = DtResumoOperaçoes();
            AtualizarNdeOpe();
        }
        private void btn_NovoCadastro_Click(object sender, EventArgs e)
        {
            F_CadastroEControleDeContenedores f = new F_CadastroEControleDeContenedores();
            Globais.Abreform(1, f);
        }
        private void btn_NovoResponsavel_Click(object sender, EventArgs e)
        {
            DialogResult x;
            string Nome;
            do
            {
                Nome = Interaction.InputBox("DIGITE o nome da pessoa que deseja incluir no cadastro");
                if (Nome != "")
                {
                    DialogResult y = MessageBox.Show("O Nome que digitou foi: " + Nome + "\n\nÉ esse o Nome Correto??", "VERIFICANDO", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (y == DialogResult.No)
                    {
                        x = MessageBox.Show("Deseja tentar Novamente??", "Tentar Novamente", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (x == DialogResult.No)
                        {
                            return;
                        }
                    }
                    else
                    {
                        DataTable dt = Banco.Procurar("ResponsaveisMovimentaçoes", "*", "NomeResponsavel", "'" + Nome + "'", "NomeResponsavel");
                        if (dt.Rows.Count > 0)
                        {
                            DialogResult v = MessageBox.Show("Esse Nome já esxiste no Cadastro, Tentar Outro Nome?\n\nDeseja Modificar?", "Nome Já Existe", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                            if (v == DialogResult.No)
                            {
                                MessageBox.Show("Operação Cancelada", "Nome Já Existe", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                            else
                            {
                                x = DialogResult.Yes;
                            }
                        }
                        x = DialogResult.No;
                    }
                }
                else
                {
                    DialogResult m = MessageBox.Show("Quer Cancelar a operação??", "Cancelar?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (m == DialogResult.Yes)
                    {
                        return;
                    }
                    else
                    {
                        x = DialogResult.Yes;
                    }
                }

            } while (x == DialogResult.Yes);

            if (Nome != "")
            {
                bool Salvou = Banco.Salvar("ResponsaveisMovimentaçoes", "NomeResponsavel", "'" + Nome + "'");

                if (Salvou)
                {
                    cb_ResponsavelM.DataSource = Banco.ObterTodos("ResponsaveisMovimentaçoes", "*", "NomeResponsavel");
                    MessageBox.Show("Nome Cadastrado", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Nome Naõ Cadastrado", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Nenhum Nome Digitado", "Nome Não Cadastrado", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }
        private void btn_ExcluirResponsavel_Click(object sender, EventArgs e)
        {
            bool Exluiu = Banco.Excluir("ResponsaveisMovimentaçoes", "NomeResponsavel", "'" + cb_ResponsavelM.Text + "'");
            if (Exluiu)
            {
                MessageBox.Show("Dado Excluido");
            }
            cb_ResponsavelM.DataSource = Banco.ObterTodos("ResponsaveisMovimentaçoes", "*", "NomeResponsavel");
        }
        bool Control5 = false;
        private void AdicionarAoConteiner_Click(object sender, EventArgs e)
        {
            DataTable dt = Banco.Procurar("Conteudos", "*", "CICE", "'%" + tb_CódigoContenedor.Text + "%'", "CICE");
            if (dt.Rows.Count>0)
            {
            
                if (ProcurarAreaDeConteiner(tb_CódigoContenedor.Text, 'R') || (cb_EstadoPeça.SelectedIndex != 0 && dt.Rows[0].Field<string>("EstadoPeça") != "CONFORME"))
                {
                    
                    if (cb_PeçaProcurada.Text != "")
                    {
                        if (cb_EstadoPeça.Text != "")
                        {
                            if (nud_Quantidade.Value != 0)
                            {
                                DataRow row = DadosConteudo.NewRow();
                                row["CIP"] = tb_CIP.Text;
                                row["NomeCIP"] = cb_PeçaProcurada.Text;
                                row["Quantidade"] = nud_Quantidade.Value;
                                row["EstadoPeça"] = cb_EstadoPeça.Text;
                                DadosConteudo.Rows.Add(row);
                                dgv_ResumoConteudo.DataSource = DadosConteudo;
                                Control5 = false;
                            }
                            else
                            {
                                MessageBox.Show("A Quantidade Deve ser maior do que zero", "Quantidade Zero não permitida", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Deve Escolher um EstadoPara Peça", "EstadoNão Escolhido", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }

                    }
                    else
                    {
                        MessageBox.Show("Deve Escolher Uma Peça primeiro", "Peça Não Escolhida", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }

                }
                else
                {
                    MessageBox.Show("Só é possivel colocar peças diferentes nos seguintes casos extraordinários:\n\n1)Nos contêiners da Reforma.\n\n2)Quando TODO o conteúdo do contêiner é apenas composto por peças não conformes\n\nCaso tenha errado a quantidade, seleccione a peça adicionada, delete a peça, e tente adionar com a quantia certa.", "Não é Permitido Misturar Peças", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                }
            }
            else
            {
                if (Control5 && DadosConteudo.Rows.Count>0)
                {

                    if (ProcurarAreaDeConteiner(tb_CódigoContenedor.Text, 'R') || (cb_EstadoPeça.SelectedIndex != 0 && DadosConteudo.Rows[0].Field<string>("EstadoPeça") != "CONFORME"))
                    {

                        if (cb_PeçaProcurada.Text != "")
                        {
                            if (cb_EstadoPeça.Text != "")
                            {
                                if (nud_Quantidade.Value != 0)
                                {
                                    DataRow row = DadosConteudo.NewRow();
                                    row["CIP"] = tb_CIP.Text;
                                    row["NomeCIP"] = cb_PeçaProcurada.Text;
                                    row["Quantidade"] = nud_Quantidade.Value;
                                    row["EstadoPeça"] = cb_EstadoPeça.Text;
                                    DadosConteudo.Rows.Add(row);
                                    dgv_ResumoConteudo.DataSource = DadosConteudo;                                   
                                }
                                else
                                {
                                    MessageBox.Show("A Quantidade Deve ser maior do que zero", "Quantidade Zero não permitida", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Deve Escolher um EstadoPara Peça", "EstadoNão Escolhido", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            }

                        }
                        else
                        {
                            MessageBox.Show("Deve Escolher Uma Peça primeiro", "Peça Não Escolhida", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }

                    }
                    else
                    {
                        MessageBox.Show("Só é possivel colocar peças diferentes nos seguintes casos extraordinários:\n\n1)Nos contêiners da Reforma.\n\n2)Quando TODO o conteúdo do contêiner é apenas composto por peças não conformes\n\nCaso tenha errado a quantidade, seleccione a peça adicionada, delete a peça, e tente adionar com a quantia certa.", "Não é Permitido Misturar Peças", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                    }
                }
                else
                {
                    
                    if (cb_PeçaProcurada.Text != "")
                    {
                        if (cb_EstadoPeça.Text != "")
                        {
                            if (nud_Quantidade.Value != 0)
                            {
                                DataRow row = DadosConteudo.NewRow();
                                row["CIP"] = tb_CIP.Text;
                                row["NomeCIP"] = cb_PeçaProcurada.Text;
                                row["Quantidade"] = nud_Quantidade.Value;
                                row["EstadoPeça"] = cb_EstadoPeça.Text;
                                DadosConteudo.Rows.Add(row);
                                dgv_ResumoConteudo.DataSource = DadosConteudo;
                                Control5 = true;
                            }
                            else
                            {
                                MessageBox.Show("A Quantidade Deve ser maior do que zero", "Quantidade Zero não permitida", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            }
                           
                        }
                        else
                        {
                            MessageBox.Show("Deve Escolher um EstadoPara Peça", "EstadoNão Escolhido", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }

                    }
                    else
                    {
                        MessageBox.Show("Deve Escolher Uma Peça primeiro", "Peça Não Escolhida", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
            }
                
                 

        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            cb_CIC.DataSource = Banco.ObterTodos("CadastroEspecificoContenedores", "*", "CICG, id asc");
        }

        #endregion;


        #region CAMBIOS

        #region CB_Changed

        private void cb_Destino_SelectedIndexChanged(object sender, EventArgs e)
        {
          
            if (populou)
            {
               
                    EstablecerDestinoEorigememFuncCIO(cb_CIO.SelectedValue.ToString());
                    AssignarValoresComboBoxesSegundoOTipoDeOperação();
                    
               

            }

        }
        private void cb_NomeCIP_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (populou)
            {
                if (cb_FamiliaPeça.Text != "")
                {
                    if (cb_PeçaProcurada.SelectedValue!=null)
                    {
                        tb_CIP.Text = cb_PeçaProcurada.SelectedValue.ToString();
                    }
                   
                }
                else
                {
                    tb_CIP.Text = "0.0.0.0.0";
                }

                DataTable dt = Banco.Procurar("Peças", "*", "CIP", "'" + cb_PeçaProcurada.SelectedValue + "'", "CIP");
                if (dt.Rows.Count > 0)
                {
                    pb_Peça.ImageLocation = dt.Rows[0].Field<string>("Foto");
                }

            }


        }
        private void cb_OrigemDasPeças_SelectedIndexChanged(object sender, EventArgs e)
        {

            
            if (populou)
            {
                EstablecerDestinoEorigememFuncCIO(cb_CIO.SelectedValue.ToString());
               // AssignarValoresComboBoxesSegundoOTipoDeOperação();
               
               
            }
        }
        int cont3=0;
        private void cb_FamiliaPeça_SelectedIndexChanged(object sender, EventArgs e)
        {
         
            if (cb_FamiliaPeça.Text != "")
            {
                cb_PeçaProcurada.Enabled = true;
                cb_EstadoPeça.Enabled = true;
            }
            else
            {
                cb_PeçaProcurada.SelectedValue = string.Empty;
                cb_PeçaProcurada.Enabled = false;
                cb_EstadoPeça.SelectedValue= string.Empty;
                cb_EstadoPeça.Enabled = false;
                pb_Peça.ImageLocation= string.Empty;
            }
          
            if (cont3 > 1 && cb_PeçaProcurada.Enabled == true)
            {

                DataTable dt = Banco.Procurar("Peças", "CIP, NomeCIP", "NomeCIP", "'" + cb_FamiliaPeça.Text + "%'", "CIP");

                if (dt.Rows.Count > 0)
                {
                    cb_PeçaProcurada.DataSource = dt;

                }
                else
                {
                    MessageBox.Show("Não há nenhuma peça do tipo procurado");

                }
            }
            else
            {
                cont3 += 1;
            }
        }
        private void cb_NomeCIO_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (populou && cb_NomeCIO.SelectedValue != null)
            {
                cb_CIO.SelectedValue = cb_NomeCIO.SelectedValue;
              
            }

        }
        private void cb_CIO_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (populou && cb_CIO.SelectedValue != null)
            {
                EstablecerDestinoEorigememFuncCIO(cb_CIO.SelectedValue.ToString());
             

            }

        }
        private void cb_CIC_SelectedIndexChanged(object sender, EventArgs e)
        {
            tb_CódigoContenedor.Text = cb_CIC.Text;
            cb_NomeCICE.SelectedValue = tb_CódigoContenedor.Text;
            if (cb_CIC.Text != "")
            {
                DataAux = Banco.Procurar("CadastroEspecificoContenedores", "*", "CICE", "'" + cb_CIC.Text + "%'", "CICG, id asc");
                if (DataAux.Rows.Count > 0)
                {
                    string CICG = DataAux.Rows[0].Field<string>("CICG");
                    DataTable dt2 = Banco.Procurar("CadastroGeralContenedores", "*", "CICG", "'" + CICG + "'", "CICG asc");
                    pb_Conteiner.ImageLocation = dt2.Rows[0].Field<string>("Foto");
                }

                AtualizarTodo();
            }
            //DataTable dt5 = Banco.Procurar("Operaçoes",);

        }

        #endregion;

        #region tb_Changed

        private void tb_NúmerodeOperação_TextChanged(object sender, EventArgs e)
        {

        }       
        private void tb_ProcurarCICE_TextChanged(object sender, EventArgs e)
        {
            DataAux = Banco.Procurar("CadastroEspecificoContenedores", "*", "CICE", "'%" + tb_ProcurarCICE.Text + "%'", "CICG, id asc");

            cb_CIC.DataSource = DataAux;
            if (DadosOperações.Rows.Count <= 0)
            {
                btn_NovoCadastro.Visible = true;
            }
            else
            {
                btn_NovoCadastro.Visible = false;
            }
        }

        #endregion;

        #region nud_Changed
        private void nud_Massa_ValueChanged(object sender, EventArgs e)
        {
            AsignarValoresMassasECICG();
            if (ComConteiner)
            {
                if (nud_Massa.Value > 0 && MassaPeça != "0")
                {

                    // MessageBox.Show("Massa Unitaria da peça: " + MassaPeça + "\nMassa do Contêiner: " + MassaContenedor, "Dados Cadastrados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (nud_Massa.Value - decimal.Parse(MassaContenedor) > 0)
                    {
                        nud_Quantidade.Value = Math.Floor((nud_Massa.Value - decimal.Parse(MassaContenedor)) / decimal.Parse(MassaPeça));
                    }
                    else
                    {
                        nud_Quantidade.Value = 0;
                        nud_Massa.Value = 0;
                    }
                }
                
            }
            else
            {
                if (nud_Massa.Value > 0 && MassaPeça != "0")
                {

                    // MessageBox.Show("Massa Unitaria da peça: " + MassaPeça + "\nMassa do Contêiner: " + MassaContenedor, "Dados Cadastrados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                        nud_Quantidade.Value = Math.Floor(nud_Massa.Value / decimal.Parse(MassaPeça));

                }
                
            }

        }
        private void nud_Quantidade_ValueChanged(object sender, EventArgs e)
        {
            AsignarValoresMassasECICG();
           
            if (ComConteiner)
            {
                nud_Massa.Value = (nud_Quantidade.Value * decimal.Parse(MassaPeça)) + decimal.Parse(MassaContenedor);
            }
            else
            {
                nud_Massa.Value = nud_Quantidade.Value * decimal.Parse(MassaPeça);
            }

        }




        #endregion;

        private void dgv_ResumoConteudo_SelectionChanged(object sender, EventArgs e)
        {
    
            

        }

        #endregion;

        private void Ch_Com_Conteiner_CheckedChanged(object sender, EventArgs e)
        {
           
            if (Ch_Com_Conteiner.Checked)
            {
                ComConteiner = true;
               
            }
            else
            {
                ComConteiner = false;               
            }

            AsignarValoresMassasECICG();
            if (ComConteiner)
            {
                if (nud_Massa.Value > 0 && MassaPeça != "0")
                {

                    // MessageBox.Show("Massa Unitaria da peça: " + MassaPeça + "\nMassa do Contêiner: " + MassaContenedor, "Dados Cadastrados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (nud_Massa.Value - decimal.Parse(MassaContenedor) > 0)
                    {
                        nud_Quantidade.Value = Math.Floor((nud_Massa.Value - decimal.Parse(MassaContenedor)) / decimal.Parse(MassaPeça));
                    }
                    else
                    {
                        nud_Quantidade.Value = 0;
                        nud_Massa.Value = 0;
                    }
                }

            }
            else
            {
                if (nud_Massa.Value > 0 && MassaPeça != "0")
                {

                    // MessageBox.Show("Massa Unitaria da peça: " + MassaPeça + "\nMassa do Contêiner: " + MassaContenedor, "Dados Cadastrados", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    nud_Quantidade.Value = Math.Floor(nud_Massa.Value / decimal.Parse(MassaPeça));

                }

            }


        }

        private void Ch_Esmaltada_CheckedChanged(object sender, EventArgs e)
        {
            if (Ch_Esmaltada.Checked)
            {
                Esmaltada = true;
            }
            else
            {
                Esmaltada = false;
            }
            AsignarValoresMassasECICG();
           if (ComConteiner)
            {
                if (nud_Massa.Value > 0 && MassaPeça != "0")
                {

                    // MessageBox.Show("Massa Unitaria da peça: " + MassaPeça + "\nMassa do Contêiner: " + MassaContenedor, "Dados Cadastrados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (nud_Massa.Value - decimal.Parse(MassaContenedor) > 0)
                    {
                        nud_Quantidade.Value = Math.Floor((nud_Massa.Value - decimal.Parse(MassaContenedor)) / decimal.Parse(MassaPeça));
                    }
                    else
                    {
                        nud_Quantidade.Value = 0;
                        nud_Massa.Value = 0;
                    }
                }

            }
            else
            {
                if (nud_Massa.Value > 0 && MassaPeça != "0")
                {

                    // MessageBox.Show("Massa Unitaria da peça: " + MassaPeça + "\nMassa do Contêiner: " + MassaContenedor, "Dados Cadastrados", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    nud_Quantidade.Value = Math.Floor(nud_Massa.Value / decimal.Parse(MassaPeça));

                }

            }
        }

        private void cb_CIT_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_CIT.Text!="")
            {
                if (cb_CIT.Text.Substring(1, 1) == ".")
                {
                    gb_DC.Enabled = true;
                }
                else
                {

                    gb_DC.Enabled = false;
                }
                if (cb_CIT.Text.Substring(1, 1) == "_")
                {
                    if (DadosConteudo.Rows[0].Field<string>("Peça") == "Nada")
                    {
                        cb_FamiliaPeça.SelectedValue = 1200;
                        cb_EstadoPeça.SelectedValue = 1200;
                        nud_Quantidade.Value = 0;
                    }

                }
                if (cb_CIT.Text.Substring(2, 1) == "R")
                {
                    Ch_Esmaltada.Checked = true;
                    cb_FamiliaPeça.SelectedValue = 1200;
                    nud_Quantidade.Value = 0;

                    #region EstadoPeça
                    //Popular combobox Destino das peças
                    Dictionary<string, string> EstadoPeça = new Dictionary<string, string>();

                    EstadoPeça.Add("E", "EM ANÁLISE");
                    EstadoPeça.Add("R", "RETRABALHO");
                    EstadoPeça.Add("S", "SUCATA");

                    cb_EstadoPeça.DataSource = new BindingSource(EstadoPeça, null);
                    cb_EstadoPeça.DisplayMember = "Value";
                    cb_EstadoPeça.ValueMember = "key";
                    cb_EstadoPeça.SelectedValue = "";
                    #endregion;
                }
                else
                {

                    #region EstadoPeça
                    //Popular combobox Destino das peças
                    Dictionary<string, string> EstadoPeça = new Dictionary<string, string>();
                    EstadoPeça.Add("B", "CONFORME");
                    EstadoPeça.Add("E", "EM ANÁLISE");
                    EstadoPeça.Add("R", "RETRABALHO");
                    EstadoPeça.Add("S", "SUCATA");

                    cb_EstadoPeça.DataSource = new BindingSource(EstadoPeça, null);
                    cb_EstadoPeça.DisplayMember = "Value";
                    cb_EstadoPeça.ValueMember = "key";
                    cb_EstadoPeça.SelectedValue = "";
                    #endregion;

                }

                if (cb_CIT.Text.Substring(2, 1) == "C")
                {
                    Ch_Esmaltada.Checked = false;
                }
                else
                {
                    Ch_Esmaltada.Checked = true;

                }
            }
        }
        

      
    }
}
