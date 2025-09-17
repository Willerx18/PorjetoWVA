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
using System.IO;
using Microsoft.VisualBasic;

namespace Atlas_projeto
{

    public partial class F_CadastroEControleDeContenedores : Form
    {
        public F_CadastroEControleDeContenedores()
        {
            InitializeComponent();
        }

        #region Variaveis Gerais

        #region DataTables
        DataTable dt;
        DataTable sorteddt2;
        DataTable dt2;
        DataTable dt3;
        DataTable dt4;
        DataTable dt5;
        DataTable dt6;
        #endregion;

        #region Numeros

        #region Enteros
        int linha = 0;
        public int modo = 0;
        int index = -1;
        int cont = 0;
        int[] IndicedeSimbolos = new int[5];
        int[] IndexPontoCIC = new int[4];
        int[] QIdVazios = new int[20];
        public int[,] CICE_Condiçoes = new int[5, 4];
        int E0 = 0;
        int E1 = 0;
        int E2 = 0;
        int E3 = 0;
        int E4 = 0;

        #endregion;

        #region Decimal
        decimal AtribuidosEstado;
        decimal AtribuidosCondição;
        decimal RestaEstado;
        decimal RestaCondição;
        #endregion;

        #endregion;

        #region string
        string DigitoCICE;
        string CICG;
        string CICE;
        string CICE_E;
        string CICE_C;
        string ESTADO = "FALTAM";
        string CONDIÇÃO = "FALTAM";
        string IdSeleccionados = "";
        string IdSeleccionados2 = "";
        string NomeCIC;
        string OrigemCompleto = "";
        string foto = "";
        string PastaDestino = Globais.CaminhoFotos+@"Contenedores\";
        string DestinoCompleto = "";
        string DestinoCompletoA = "";
        #endregion;

        #region Objetos
        Contenedor c = new Contenedor();
        CIC CIC = new CIC();
        #endregion;

        #region bool
        public bool AçãoAutorizada;
        bool Control = false;
        bool m = true;
        public bool Assignou = false;
        bool Espera;
        bool sustituir = true;
        #endregion;  

        #endregion;



        private void F_CadastroEControleDeContenedores_Load(object sender, EventArgs e)
        {
            #region Popular ComboBoxes

            #region Area de ubicação
            Dictionary<string, string> Area = new Dictionary<string, string>();
            Area.Add("B", "MATERIA PRIMA");
            Area.Add("C", "SEMI-ACABADO(A)");
            Area.Add("D", "ACABADO(A)");
            Area.Add("A", "EM ANÁLISE");
            Area.Add("R", "REFORMADO");

            cb_Area.Items.Clear();
            cb_Area.DataSource = new BindingSource(Area, null);
            cb_Area.DisplayMember = "Value";
            cb_Area.ValueMember = "key";

            Dictionary<string, string> Area2 = new Dictionary<string, string>();
            Area2.Add("", ""); 
            Area2.Add("B", "MATERIA PRIMA");
            Area2.Add("C", "SEMI-ACABADO(A)");
            Area2.Add("D", "ACABADO(A)");
            Area2.Add("A", "EM ANÁLISE");
            Area2.Add("R", "REFORMADO");
            cb_FiltrarAreaDeUbicação.Items.Clear();
            cb_FiltrarAreaDeUbicação.DataSource = new BindingSource(Area2, null);
            cb_FiltrarAreaDeUbicação.DisplayMember = "Value";
            cb_FiltrarAreaDeUbicação.ValueMember = "key";
            #endregion;       

            #region TIPO DE CONTENEDOR
            Dictionary<string, string> Tipo = new Dictionary<string, string>();

            Tipo.Add("C", "CARRINHO");
            Tipo.Add("X", "CAIXA");
            Tipo.Add("P", "PALETE");

            cb_Tipo.Items.Clear();
            cb_Tipo.DataSource = new BindingSource(Tipo, null);
            cb_Tipo.DisplayMember = "Value";
            cb_Tipo.ValueMember = "key";

            Dictionary<string, string> Tipo2 = new Dictionary<string, string>();
            Tipo2.Add("", "");
            Tipo2.Add("C", "CARRINHO");
            Tipo2.Add("X", "CAIXA");
            Tipo2.Add("P", "PALETE");
            cb_FiltarTipo.DataSource = new BindingSource(Tipo2, null);
            cb_FiltarTipo.DisplayMember = "Value";
            cb_FiltarTipo.ValueMember = "key";

            #endregion;

            #region SETOR
            cb_Setor.Items.Clear();
            cb_Setor.DataSource = Banco.ObterTodos("Setores", "*");
            cb_Setor.DisplayMember = "Nome";
            cb_Setor.ValueMember = "Id";

            cb_FiltrarSetor.Items.Clear();
            cb_FiltrarSetor.DataSource = Banco.ObterTodos("Setores", "*");
            cb_FiltrarSetor.DisplayMember = "Nome";
            cb_FiltrarSetor.ValueMember = "Id";
            #endregion;

            #region FAMILIA
            cb_Familia.Items.Clear();
            cb_Familia.DataSource = Banco.ObterTodos("Familias", "*");
            cb_Familia.DisplayMember = "Familia";
            cb_Familia.ValueMember = "IdFamilia";

            cb_FiltarFamilia.DataSource = Banco.ObterTodos("Familias", "*");
            cb_FiltarFamilia.DisplayMember = "Familia";
            cb_FiltarFamilia.ValueMember = "IdFamilia";
            #endregion;

            #region CARACTERISTICA
            cb_Caracteristica.Items.Clear();
            cb_Caracteristica.DataSource = Banco.ObterTodos("CaracteristicasContenedores", "*", "IdCaracteristica");
            cb_Caracteristica.DisplayMember = "Caracteristica";
            cb_Caracteristica.ValueMember = "IdCaracteristica";

            cb_FIltrarCaracteristica.DataSource = Banco.ObterTodos("CaracteristicasContenedores", "*", "IdCaracteristica");
            cb_FIltrarCaracteristica.DisplayMember = "Caracteristica";
            cb_FIltrarCaracteristica.ValueMember = "IdCaracteristica";
            #endregion;

            #region  ComboBox MODELO
            cb_Modelo.Items.Clear();
            cb_Modelo.DataSource = Banco.ObterTodos("Modelos", "*");
            cb_Modelo.DisplayMember = "Modelo";
            cb_Modelo.ValueMember = "IdModelos";

            cb_FiltarModelo.DataSource = Banco.ObterTodos("Modelos", "*");
            cb_FiltarModelo.DisplayMember = "Modelo";
            cb_FiltarModelo.ValueMember = "IdModelos";
            #endregion;            

            #region ESTADO ATUAL
            Dictionary<string, string> Estado = new Dictionary<string, string>();
            Estado.Add("0", "DISPONIVEL");
            Estado.Add("1", "EM USO");
            Estado.Add("2", "EM MANUTENÇÃO");
            Estado.Add("3", "DESCARTADO");
            Estado.Add("4", "FORA DE USO");

            cb_EstadoAtual.Items.Clear();
            cb_EstadoAtual.DataSource = new BindingSource(Estado, null);
            cb_EstadoAtual.DisplayMember = "Value";
            cb_EstadoAtual.ValueMember = "key";

            #endregion;

            #region Area de ubicação Atual Y condiçoes

            AssiganarDADOSaCbxUBICAÇÂOATUALcomBaseenESTADO();

            #endregion;

            #endregion;

            #region Popular DGV
            //Popular DataGriwView
            dt = Banco.Procurar("CadastroEspecificoContenedores", "*", "CICE", "'%'", "CICG, id asc");
            dgv_GestãoContenedores.DataSource = Banco.Procurar("CadastroEspecificoContenedores", "CICE, NomeCIC", "CICE", "'%'", "CICG, id asc");
            dgv_GestãoContenedores.Columns[0].Width = 70;
            dgv_GestãoContenedores.Columns[1].Width = 350;
            #endregion;

            #region Config.Gerais


            #endregion;
        }


        #region PROCEDIMENTOS
        private void Filtar()
        {
            if (dgv_GestãoContenedores.Rows.Count > 0)
            {
                string n = nud_Dcice.Value.ToString();
                string t = cb_FiltarTipo.SelectedValue.ToString();
                string a = cb_FiltrarAreaDeUbicação.SelectedValue.ToString();
                string f = cb_FiltarFamilia.SelectedValue.ToString();
                string m = cb_FiltarModelo.SelectedValue.ToString();
                string c = cb_FIltrarCaracteristica.SelectedValue.ToString();
                string Texto = "'%'";
                if (t == "")
                {
                    cb_FiltrarAreaDeUbicação.Enabled = false;
                    cb_FiltarFamilia.Enabled = false;
                    cb_FiltarModelo.Enabled = false;
                    cb_FIltrarCaracteristica.Enabled = false;

                    cb_FiltrarAreaDeUbicação.SelectedValue = "";
                    cb_FIltrarCaracteristica.SelectedValue = 0;
                    cb_FiltarModelo.SelectedValue = 0;
                    cb_FiltarFamilia.SelectedValue = 0;
                   
                   
                }
                else
                {
                    cb_FiltrarAreaDeUbicação.Enabled = true;
                }

                if (a == "")
                {

                    cb_FiltarFamilia.Enabled = false;
                    cb_FiltarModelo.Enabled = false;
                    cb_FIltrarCaracteristica.Enabled = false;
                    cb_FiltarFamilia.SelectedValue = 0;
                    cb_FiltarModelo.SelectedValue = 0;
                    cb_FiltarFamilia.SelectedValue = 0;
                }
                else
                {
                    cb_FiltarFamilia.Enabled = true;
                }

                if (f == "0")
                {
                    cb_FiltarModelo.Enabled = false;
                    cb_FIltrarCaracteristica.Enabled = false;
                    cb_FIltrarCaracteristica.SelectedValue = 0;
                    cb_FiltarModelo.SelectedValue = 0;
                 
                }
                else
                {
                    cb_FiltarModelo.Enabled = true;
                }

                if (m == "0")
                {
                    cb_FIltrarCaracteristica.Enabled = false;
                    cb_FIltrarCaracteristica.SelectedValue = 0;
                }
                else
                {
                    cb_FIltrarCaracteristica.Enabled = true;
                }


                if (n == "0")
                {
                    if (t != "" && a == "")
                    {
                        Texto = "'%" + t + "%'";
                    }

                    if (t != "" && a != "")
                    {
                        Texto = "'%" + t + a + "-" + f + "." + m + "." + c + "%'";
                    }

                }
                else
                {
                    if (t=="" && a=="")
                    {
                        Texto =  "'" + n + "-" +  "%'";
                    }

                    if (t != "" && a == "")
                    {
                        Texto = "'" + n + "-" + t + "%'";
                    }

                    if (t != "" && a != "")
                    {
                        Texto = "'" + n + "-" + t + a + "-" + f + "." + m + "." + c + "%'";
                    }
                }



                DataTable dt = Banco.Procurar("CadastroEspecificoContenedores", "CICE, NomeCIC", "CICE", Texto, "CICG, id asc");
                if (dt.Rows.Count > 0)
                {
                    dgv_GestãoContenedores.DataSource = dt;
                    dgv_GestãoContenedores.Columns[0].Width = 70;
                    dgv_GestãoContenedores.Columns[1].Width = 350;
                }
                else
                {
                    MessageBox.Show("Nenhum dado no cadastro coincide com o parámetro de busca\n\n\nParámetro de busca= "+Texto,"Nehum Dado Achado",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);

                }
            }
        }
        private void EstablecerIndiceDeSimbolosCICEeCIC(string CICE, string CICG)
        {
            string Buscado = ".";
            string Buscado2 = "-";
            string c;

            int m = 0;
            int n = 0;

            for (int i = 0; i < CICE.Length; i++)
            {
                c = CICE.Substring(i, 1);
                if (c == Buscado || c == Buscado2)
                {

                    IndicedeSimbolos[m] = i;
                    m += 1;

                }
            }

            for (int i = 0; i < CICG.Length; i++)
            {
                c = CICG.Substring(i, 1);
                if (c == Buscado)
                {

                    IndexPontoCIC[n] = i;
                    n += 1;
                }
            }
        }
        private void AtualizarCICApartirdeNudEtextbox()
        {
            DigitoCICE = nud_DigitoCICE.Value.ToString();
            CICG = "" + cb_Tipo.SelectedValue + cb_Area.SelectedValue + "-" + cb_Familia.SelectedIndex + "." + cb_Modelo.SelectedIndex + "." + cb_Caracteristica.SelectedIndex;
            CICE = "" + nud_DigitoCICE.Value + "-" + CICG;
            CICE_E = CICE + cb_EstadoAtual.SelectedIndex + cb_Condições.SelectedIndex;
        }
       
        private void HabilitarouInhabilitarNudsRESUMOSemFunciondeQUANTIDADEE()
        {
            if (nud_Quantidade.Value > 0)
            {
                nud_E0.Enabled = true;
                nud_E1.Enabled = true;
                nud_E2.Enabled = true;

                nud_E3.Enabled = true;
                nud_C0.Enabled = true;
                nud_C1.Enabled = true;
                nud_C2.Enabled = true;

            }
            else
            {
                nud_E0.Enabled = false;
                nud_E1.Enabled = false;
                nud_E2.Enabled = false;
                nud_E4.Enabled = false;
                nud_E3.Enabled = false;
                nud_C0.Enabled = false;
                nud_C1.Enabled = false;
                nud_C2.Enabled = false;
                nud_C3.Enabled = false;
            }
        }

        private bool AssiganarMaximosNUdsResumosEstados(NumericUpDown x)
        {

            int elemento1 = Convert.ToInt32(nud_E0.Value + nud_E1.Value + nud_E2.Value + nud_E4.Value + nud_E3.Value);

            if (elemento1 > nud_Quantidade.Value)
            {
                nud_E0.Maximum = nud_E1.Maximum = nud_E2.Maximum = nud_E3.Maximum = nud_E4.Maximum = nud_C0.Maximum = nud_C1.Maximum = nud_C2.Maximum = nud_C3.Maximum = nud_Quantidade.Value;
                nud_Quantidade.Minimum = 0;

                return true;
            }
            else
            {

                nud_E0.Maximum = (nud_Quantidade.Value - elemento1) + nud_E0.Value;
                nud_E1.Maximum = (nud_Quantidade.Value - elemento1) + nud_E1.Value;
                nud_E2.Maximum = (nud_Quantidade.Value - elemento1) + nud_E2.Value;
                nud_E4.Maximum = (nud_Quantidade.Value - elemento1) + nud_E4.Value;
                nud_E3.Maximum = (nud_Quantidade.Value - elemento1) + nud_E3.Value;

                return false;

            }

        }
        private bool AssiganarMaximosNUdsResumosCondiçoes(NumericUpDown x)
        { int elemento1 = Convert.ToInt32(nud_C0.Value + nud_C1.Value + nud_C2.Value + nud_C3.Value);

            if (elemento1 > nud_Quantidade.Value)
            {

                nud_E0.Maximum = nud_E1.Maximum = nud_E2.Maximum = nud_E3.Maximum = nud_E4.Maximum = nud_C0.Maximum = nud_C1.Maximum = nud_C2.Maximum = nud_C3.Maximum = nud_Quantidade.Value;

                nud_Quantidade.Minimum = 0;

                return true;
            }
            else
            {

                nud_C0.Maximum = (nud_Quantidade.Value - elemento1) + nud_C0.Value;
                nud_C1.Maximum = (nud_Quantidade.Value - elemento1) + nud_C1.Value;
                nud_C2.Maximum = (nud_Quantidade.Value - elemento1) + nud_C2.Value;
                nud_C3.Maximum = (nud_Quantidade.Value - elemento1) + nud_C3.Value;

                return false;
            }

        }

        private void AtualizarNud(NumericUpDown x)
        {
            nud_Quantidade.Minimum = 0;
            if (x.Value < 0)
            {
                x.Value = 0;
            }
            if (x == nud_E0)
            {

                if (modo == 1)
                {
                    AssiganarMaximosNUdsResumosEstados(x);
                }
                else
                {
                    x.Maximum = 1000;
                }

                if (x.Value > 0)
                {
                    nud_E0.BackColor = Color.LightGreen;
                    E0 = 1;
                }
                else
                {
                    E0 = 0;
                    x.BackColor = Color.White;
                }

            }
            else
            {
                if (x == nud_E1)
                {
                    if (modo == 1)
                    {
                        AssiganarMaximosNUdsResumosEstados(x);
                    }
                    else
                    {
                        x.Maximum = 1000;
                    }

                    if (x.Value > 0)
                    {
                        nud_E1.BackColor = Color.LightSkyBlue;
                        E1 = 1;
                    }
                    else
                    {
                        E1 = 0;
                        x.BackColor = Color.White;
                    }

                }
                else
                {
                    if (x == nud_E2)
                    {
                        if (modo == 1)
                        {
                            AssiganarMaximosNUdsResumosEstados(x);
                        }
                        else
                        {
                            x.Maximum = 1000;
                        }

                        if (x.Value > 0)
                        {
                            E2 = 1;
                            nud_E2.BackColor = Color.Yellow;
                        }
                        else
                        {
                            E2 = 0;
                            x.BackColor = Color.White;
                        }
                    }
                    else
                    {
                        if (x == nud_E4)
                        {
                            if (modo == 1)
                            {
                                AssiganarMaximosNUdsResumosEstados(x);
                            }
                            else
                            {
                                x.Maximum = 1000;
                            }

                            if (x.Value > 0)
                            {
                                E4 = 1;
                                x.ForeColor = Color.White;
                                nud_E4.BackColor = Color.Red;

                            }
                            else
                            {
                                E4 = 0;
                                x.ForeColor = Color.Black;
                                x.BackColor = Color.White;

                            }
                        }
                        else
                        {
                            if (x == nud_E3)
                            {
                                if (modo == 1)
                                {
                                    AssiganarMaximosNUdsResumosEstados(x);
                                }
                                else
                                {
                                    x.Maximum = 1000;
                                }

                                if (x.Value > 0)
                                {
                                    E3 = 1;
                                    nud_E3.BackColor = Color.DarkGray;
                                }
                                else
                                {
                                    E3 = 0;
                                    x.BackColor = Color.White;
                                }
                            }
                            else
                            {
                                if (x == nud_C0)
                                {
                                    if (modo == 1)
                                    {

                                        AssiganarMaximosNUdsResumosCondiçoes(x);
                                    }
                                    else
                                    {
                                        x.Maximum = 1000;
                                    }

                                    if (x.Value > 0)
                                    {

                                        nud_C0.BackColor = Color.LightGreen;
                                    }
                                    else
                                    {
                                        x.BackColor = Color.White;
                                    }
                                }
                                else
                                {
                                    if (x == nud_C1)
                                    {
                                        if (modo == 1)
                                        {

                                            AssiganarMaximosNUdsResumosCondiçoes(x);
                                        }
                                        else
                                        {
                                            x.Maximum = 1000;
                                        }
                                        if (x.Value > 0)
                                        {
                                            nud_C1.BackColor = Color.Yellow;
                                        }
                                        else
                                        {
                                            x.BackColor = Color.White;
                                        }
                                    }
                                    else
                                    {
                                        if (x == nud_C2)
                                        {
                                            if (modo == 1)
                                            {

                                                AssiganarMaximosNUdsResumosCondiçoes(x);
                                            }
                                            else
                                            {
                                                x.Maximum = 1000;
                                            }
                                            if (x.Value > 0)
                                            {
                                                nud_C2.BackColor = Color.Orange;
                                            }
                                            else
                                            {
                                                x.BackColor = Color.White;
                                            }
                                        }
                                        else
                                        {
                                            if (x == nud_C3)
                                            {
                                                if (modo == 1)
                                                {

                                                    AssiganarMaximosNUdsResumosCondiçoes(x);
                                                }
                                                else
                                                {
                                                    x.Maximum = 1000;
                                                }

                                                if (x.Value > 0)
                                                {
                                                    nud_C3.BackColor = Color.Red;
                                                    nud_C3.ForeColor = Color.White;
                                                }
                                                else
                                                {
                                                    x.BackColor = Color.White;
                                                    x.ForeColor = Color.Black;
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
            bool entrou = false;
            bool entrou2 = false;
            bool entrou3 = false;
            if (modo == 1)
            {
                lb_E0.Visible = lb_E1.Visible = lb_E2.Visible = true;
                lb_C1.Visible = lb_C2.Visible = lb_C3.Visible = true;

                if (x == nud_Quantidade)//  por defecto agrego los contenedores em otimos e disponiveis e autocompleto ...
                {


                    if (!Espera)
                    {
                        if (nud_Quantidade.Value > 0)
                        {

                            AtribuidosEstado = nud_Quantidade.Value - (nud_Quantidade.Value - (nud_E0.Value + nud_E1.Value + nud_E2.Value + nud_E3.Value + nud_E4.Value));
                            AtribuidosCondição = nud_Quantidade.Value - (nud_Quantidade.Value - (nud_C0.Value + nud_C1.Value + nud_C2.Value + nud_C3.Value));
                            RestaEstado = nud_Quantidade.Value - (nud_E0.Value + nud_E1.Value + nud_E2.Value + nud_E3.Value + nud_E4.Value);
                            RestaCondição = nud_Quantidade.Value - (nud_C0.Value + nud_C1.Value + nud_C2.Value + nud_C3.Value);

                            bool EntrouCondiçoesLimites0 = AssiganarMaximosNUdsResumosCondiçoes(x);
                            bool EntrouEstadosLimites0 = AssiganarMaximosNUdsResumosEstados(x);
                            nud_Quantidade.Maximum = 1000;


                            if ((nud_E1.Value + nud_E2.Value + nud_E3.Value + nud_E4.Value) > 0)
                            {
                                if ((nud_C1.Value + nud_C2.Value + nud_C3.Value) > 0)
                                {
                                    if ((RestaEstado < 0 && RestaCondição < 0) || (AtribuidosEstado < 0 && AtribuidosCondição < 0))/// restar
                                    {

                                        if (nud_E0.Value > 0)
                                        {
                                            nud_E0.Value = (AtribuidosEstado + RestaEstado) - (nud_E1.Value + nud_E2.Value + nud_E3.Value + nud_E4.Value);
                                        }
                                        else
                                        {
                                            if (nud_E1.Value > 0)
                                            {
                                                nud_E1.Value = (AtribuidosEstado + RestaEstado) - (nud_E1.Value + nud_E2.Value + nud_E3.Value + nud_E4.Value);
                                            }
                                            else
                                            {
                                                if (nud_E2.Value > 0)
                                                {
                                                    nud_E2.Value = (AtribuidosEstado + RestaEstado) - (nud_E1.Value + nud_E2.Value + nud_E3.Value + nud_E4.Value);
                                                }
                                                else
                                                {
                                                    if (nud_E3.Value > 0)
                                                    {
                                                        nud_E3.Value = (AtribuidosEstado + RestaEstado) - (nud_E1.Value + nud_E2.Value + nud_E3.Value + nud_E4.Value);
                                                    }
                                                    else
                                                    {
                                                        if (nud_E4.Value > 0)
                                                        {
                                                            nud_E4.Value = (AtribuidosEstado + RestaEstado) - (nud_E1.Value + nud_E2.Value + nud_E3.Value + nud_E4.Value);
                                                        }
                                                        else
                                                        {
                                                            x.Value = 0;
                                                        }

                                                    }
                                                }
                                            }
                                        }


                                        if (nud_C0.Value > 0)
                                        {
                                            nud_C0.Value = (AtribuidosCondição + RestaCondição) - (nud_C1.Value + nud_C2.Value + nud_C3.Value);
                                        }
                                        else
                                        {
                                            if (nud_C1.Value > 0)
                                            {
                                                nud_C1.Value = (AtribuidosCondição + RestaCondição) - (nud_C1.Value + nud_C2.Value + nud_C3.Value); ;
                                            }
                                            else
                                            {
                                                if (nud_C2.Value > 0)
                                                {
                                                    nud_C2.Value = (AtribuidosCondição + RestaCondição) - (nud_C1.Value + nud_C2.Value + nud_C3.Value); ;
                                                }
                                                else
                                                {
                                                    if (nud_C3.Value > 0)
                                                    {
                                                        nud_C3.Value = (AtribuidosCondição + RestaCondição) - (nud_C1.Value + nud_C2.Value + nud_C3.Value); ;
                                                    }
                                                    else
                                                    {
                                                        x.Value = 0;
                                                    }
                                                }
                                            }
                                        }

                                    }
                                    else
                                    {
                                        nud_E0.Value = (AtribuidosEstado + RestaEstado) - (nud_E1.Value + nud_E2.Value + nud_E3.Value + nud_E4.Value);
                                        nud_C0.Value = (AtribuidosCondição + RestaCondição) - (nud_C1.Value + nud_C2.Value + nud_C3.Value);

                                        entrou = true;
                                    }
                                }
                                else
                                {
                                    if ((RestaEstado < 0 && RestaCondição < 0) || (AtribuidosEstado < 0 && AtribuidosCondição < 0))//RESTAR
                                    {
                                        if (nud_E0.Value > 0)
                                        {
                                            nud_E0.Value = (AtribuidosEstado + RestaEstado) - (nud_E1.Value + nud_E2.Value + nud_E3.Value + nud_E4.Value);
                                        }
                                        else
                                        {
                                            if (nud_E1.Value > 0)
                                            {
                                                nud_E1.Value = (AtribuidosEstado + RestaEstado) - (nud_E1.Value + nud_E2.Value + nud_E3.Value + nud_E4.Value);
                                            }
                                            else
                                            {
                                                if (nud_E2.Value > 0)
                                                {
                                                    nud_E2.Value = (AtribuidosEstado + RestaEstado) - (nud_E1.Value + nud_E2.Value + nud_E3.Value + nud_E4.Value);
                                                }
                                                else
                                                {
                                                    if (nud_E3.Value > 0)
                                                    {
                                                        nud_E3.Value = (AtribuidosEstado + RestaEstado) - (nud_E1.Value + nud_E2.Value + nud_E3.Value + nud_E4.Value);
                                                    }
                                                    else
                                                    {
                                                        if (nud_E4.Value > 0)
                                                        {
                                                            nud_E4.Value = (AtribuidosEstado + RestaEstado) - (nud_E1.Value + nud_E2.Value + nud_E3.Value + nud_E4.Value);
                                                        }
                                                        else
                                                        {
                                                            x.Value = 0;
                                                        }

                                                    }
                                                }
                                            }
                                        }

                                    }
                                    else
                                    {
                                        nud_E0.Value = (AtribuidosEstado + RestaEstado) - (nud_E1.Value + nud_E2.Value + nud_E3.Value + nud_E4.Value);
                                        nud_C0.Value = (AtribuidosCondição + RestaCondição);

                                        entrou = true;

                                    }
                                }

                            }
                            else
                            {
                                if (EntrouEstadosLimites0 || EntrouCondiçoesLimites0)
                                {
                                    if ((nud_C1.Value + nud_C2.Value + nud_C3.Value) > 0)
                                    {
                                        nud_E0.Value = (AtribuidosEstado + RestaEstado);

                                        if (nud_C0.Value > 0)
                                        {
                                            nud_C0.Value = (AtribuidosCondição + RestaCondição) - (nud_C1.Value + nud_C2.Value + nud_C3.Value);
                                        }
                                        else
                                        {
                                            if (nud_C1.Value > 0)
                                            {
                                                nud_C1.Value = (AtribuidosCondição + RestaCondição) - (nud_C0.Value + nud_C2.Value + nud_C3.Value);
                                            }
                                            else
                                            {
                                                if (nud_C2.Value > 0)
                                                {
                                                    nud_C2.Value = (AtribuidosCondição + RestaCondição) - (nud_C1.Value + nud_C0.Value + nud_C3.Value);
                                                }
                                                else
                                                {
                                                    if (nud_C3.Value > 0)
                                                    {
                                                        nud_C3.Value = (AtribuidosCondição + RestaCondição) - (nud_C1.Value + nud_C2.Value + nud_C0.Value);
                                                    }
                                                    else
                                                    {
                                                        x.Value = 0;
                                                    }
                                                }
                                            }

                                        }

                                        entrou2 = true;

                                    }
                                    else
                                    {
                                        nud_E0.Value = (AtribuidosEstado + RestaEstado);
                                        if (nud_C0.Value > 0)
                                        {
                                            nud_C0.Value = (AtribuidosCondição + RestaCondição);
                                        }
                                        else
                                        {
                                            if (nud_C1.Value > 0)
                                            {
                                                nud_C1.Value = (AtribuidosCondição + RestaCondição);
                                            }
                                            else
                                            {
                                                if (nud_C2.Value > 0)
                                                {
                                                    nud_C2.Value = (AtribuidosCondição + RestaCondição);
                                                }
                                                else
                                                {
                                                    if (nud_C3.Value > 0)
                                                    {
                                                        nud_C3.Value = (AtribuidosCondição + RestaCondição);
                                                    }
                                                    else
                                                    {
                                                        x.Value = 0;
                                                    }
                                                }
                                            }

                                        }
                                    }

                                }
                                else
                                {
                                    if ((nud_C1.Value + nud_C2.Value + nud_C3.Value) > 0)
                                    {
                                        nud_E0.Value = (AtribuidosEstado + RestaEstado);
                                        nud_C0.Value = (AtribuidosCondição + RestaCondição) - (nud_C1.Value + nud_C2.Value + nud_C3.Value);

                                        entrou3 = true;

                                    }
                                    else
                                    {
                                        nud_E0.Value = (AtribuidosEstado + RestaEstado);
                                        nud_C0.Value = (AtribuidosCondição + RestaCondição);

                                    }

                                }

                            }

                        }
                        else
                        {

                            nud_E0.Value = nud_E1.Value = nud_E2.Value = nud_E3.Value = nud_E4.Value = 0;
                            nud_C0.Value = nud_C1.Value = nud_C2.Value = nud_C3.Value = 0;
                            AssiganarMaximosNUdsResumosCondiçoes(x);
                            AssiganarMaximosNUdsResumosEstados(x);
                            nud_Quantidade.Maximum = 1000;

                        }


                    }
                    else
                    {
                        Espera = false;
                    }
                }

                lb_E0.Text = (nud_Quantidade.Value - (nud_Quantidade.Value - (nud_E0.Value + nud_E1.Value + nud_E2.Value + nud_E3.Value + nud_E4.Value))).ToString();
                lb_E2.Text = nud_Quantidade.Value.ToString();
                if (entrou)
                {
                    lb_C1.Text = (nud_Quantidade.Value - (nud_Quantidade.Value - (nud_C0.Value + nud_C1.Value + nud_C2.Value + nud_C3.Value))).ToString();
                }
                else
                {
                    if (entrou2)
                    {
                        lb_C1.Text = (nud_Quantidade.Value - (nud_Quantidade.Value - (nud_C0.Value + nud_C1.Value + nud_C2.Value + nud_C3.Value))).ToString();
                    }
                    else
                    {
                        if (entrou3)
                        {
                            lb_C1.Text = (AtribuidosCondição - RestaCondição).ToString();
                        }

                        lb_C1.Text = (nud_Quantidade.Value - (nud_Quantidade.Value - (nud_C0.Value + nud_C1.Value + nud_C2.Value + nud_C3.Value))).ToString();
                    }

                }

                lb_C3.Text = nud_Quantidade.Value.ToString();

                if (lb_E0.Text == lb_E2.Text)
                {
                    lb_E0.BackColor = lb_E1.BackColor = lb_E2.BackColor = Color.LightGreen;
                }
                else
                {
                    lb_E0.BackColor = lb_E1.BackColor = lb_E2.BackColor = Color.Salmon;
                }
                if (lb_C1.Text == lb_C3.Text)
                {
                    lb_C1.BackColor = lb_C2.BackColor = lb_C3.BackColor = Color.LightGreen;
                }
                else
                {
                    lb_C1.BackColor = lb_C2.BackColor = lb_C3.BackColor = Color.Salmon;
                }
            }
            else
            {
                lb_E0.Visible = lb_E1.Visible = lb_E2.Visible = false;
                lb_C1.Visible = lb_C2.Visible = lb_C3.Visible = false;
            }


        }

        private void PreencherNudsResumoESTADOCIC(string CICG)
        {
            DataTable dt1 = Banco.Procurar("CadastroEspecificoContenedores", "CICG,ESTADO,CICE_E,CICE_C,CONDIÇOES", "CICG", "'" + CICG + "'", "CICG, id asc");
            RestablecerDTResumos();

            //llenar datos resumo total de estados
            ArrayList array = new ArrayList();
            foreach (DataRow dr in dt1.Rows)
            {
                object total;
                CICE_E = (string)dr["CICE_E"];
                if (array.IndexOf(CICE_E) < 0)
                {
                    total = dt1.Compute(String.Format("Count(CICG)"), "CICE_E like '" + CICE_E + "'");

                    dt3.Rows.Add(new object[] { dr["CICE_E"], dr["ESTADO"], Convert.ToInt32(total) });


                    array.Add(CICE_E);
                }
            }


            //llenar datos resumo total de Condiçoes
            ArrayList array2 = new ArrayList();
            foreach (DataRow dr in dt1.Rows)
            {
                object total;
                CICE_C = (string)dr["CICE_C"];

                if (array2.IndexOf(CICE_C) < 0)
                {

                    total = dt1.Compute(String.Format("Count(CICG)"), "CICE_C like '" + CICE_C + "'");                   
                    dt4.Rows.Add(new object[] { dr["CICE_C"], dr["CONDIÇOES"], Convert.ToInt32(total) });

                    array2.Add(CICE_C);
                }
            }




            for (int i = 0; i < 4; i++)
            {
                bool y = false;
                switch (i)
                {
                    case 0:
                        foreach (DataRow dr in dt4.Rows)
                        {
                            int n = Convert.ToInt32(dr["CONDIÇÃO"]);
                            int x = Convert.ToInt32(dr["Quantidade"]);
                            if (n == i)
                            {
                                nud_C0.Value = x;
                                y = true;
                            }
                        }
                        if (!y)
                        {
                            nud_C0.Value = 0;
                        }
                        break;
                    case 1:
                        foreach (DataRow dr in dt4.Rows)
                        {
                            int n = Convert.ToInt32(dr["CONDIÇÃO"]);
                            int x = Convert.ToInt32(dr["Quantidade"]);
                            if (n == i)
                            {
                                nud_C1.Value = x;
                                y = true;
                            }
                        }
                        if (!y)
                        {
                            nud_C1.Value = 0;
                        }
                        break;

                    case 2:
                        foreach (DataRow dr in dt4.Rows)
                        {
                            int n = Convert.ToInt32(dr["CONDIÇÃO"]);
                            int x = Convert.ToInt32(dr["Quantidade"]);
                            if (n == i)
                            {
                                nud_C2.Value = x;
                                y = true;
                            }
                        }
                        if (!y)
                        {
                            nud_C2.Value = 0;
                        }
                        break;

                    case 3:
                        foreach (DataRow dr in dt4.Rows)
                        {
                            int n = Convert.ToInt32(dr["CONDIÇÃO"]);
                            int x = Convert.ToInt32(dr["Quantidade"]);
                            if (n == i)
                            {
                                nud_C3.Value = x;
                                y = true;
                            }
                        }
                        if (!y)
                        {
                            nud_C3.Value = 0;
                        }
                        break;

                }
            }


            for (int i = 0; i < 5; i++)
            {
                bool y = false;
                switch (i)
                {
                    case 0:
                        foreach (DataRow dr in dt3.Rows)
                        {
                            int n = Convert.ToInt32(dr["ESTADO"]);
                            int x = Convert.ToInt32(dr["Quantidade"]);
                            if (n == i)
                            {
                                nud_E0.Value = x;
                                y = true;
                            }
                        }
                        if (!y)
                        {
                            nud_E0.Value = 0;
                        }
                        break;
                    case 1:
                        foreach (DataRow dr in dt3.Rows)
                        {
                            int n = Convert.ToInt32(dr["ESTADO"]);
                            int x = Convert.ToInt32(dr["Quantidade"]);
                            if (n == i)
                            {
                                nud_E1.Value = x;
                                y = true;
                            }
                        }
                        if (!y)
                        {
                            nud_E1.Value = 0;
                        }
                        break;

                    case 2:
                        foreach (DataRow dr in dt3.Rows)
                        {
                            int n = Convert.ToInt32(dr["ESTADO"]);
                            int x = Convert.ToInt32(dr["Quantidade"]);
                            if (n == i)
                            {
                                nud_E2.Value = x;
                                y = true;
                            }
                        }
                        if (!y)
                        {
                            nud_E2.Value = 0;
                        }
                        break;

                    case 3:
                        foreach (DataRow dr in dt3.Rows)
                        {
                            int n = Convert.ToInt32(dr["ESTADO"]);
                            int x = Convert.ToInt32(dr["Quantidade"]);
                            if (n == i)
                            {
                                nud_E4.Value = x;
                                y = true;
                            }
                        }
                        if (!y)
                        {
                            nud_E4.Value = 0;
                        }

                        break;
                    case 4:
                        foreach (DataRow dr in dt3.Rows)
                        {
                            int n = Convert.ToInt32(dr["ESTADO"]);
                            int x = Convert.ToInt32(dr["Quantidade"]);
                            if (n == i)
                            {
                                nud_E3.Value = x;
                                y = true;
                            }
                        }
                        if (!y)
                        {
                            nud_E3.Value = 0;
                        }
                        break;
                }
            }



        }
        private void ProcurarDatosParaPreencherNudsResumos(DataTable dt1, string CICG)
        {
            foreach (DataRow dr in dt1.Rows)
            {
                DataRow row = dt5.NewRow();
                if ((string)dr["CICG"]==CICG)
                {
                    row[0] = dr[0];
                    row[1] = dr[1];
                    row[2] = dr[2];
                    row[3] = dr[3];
                    row[4] = dr[4];
                    row[5] = dr[5];
                    row[6] = dr[6];
                    row[7] = dr[7];
                    row[8] = dr[8];
                    row[9] = dr[9];
                    row[10] = dr[10];
                    row[11] = dr[11];
                    
                    dt5.Rows.Add(row);
                }
             

            }
         
        }
        private void PreencherNudsResumoESTADOCIC(DataTable dt1, string CICG)
        {
            RestablecerDTResumos();

            ProcurarDatosParaPreencherNudsResumos(dt1, CICG);
            //llenar datos resumo total de estados
            ArrayList array = new ArrayList();          
            if (dt5.Rows.Count > 0)
            {
                foreach (DataRow dr in dt5.Rows)
                {
                    object total;
                    CICE_E = (string)dr["CICE_E"];

                    if (array.IndexOf(CICE_E) < 0)
                    {
                        total = dt5.Compute(String.Format("Count(CICG)"), "CICE_E like '%" + CICE_E + "%'");

                        dt3.Rows.Add(new object[] { dr["CICE_E"], dr["ESTADO"], Convert.ToInt32(total) });

                        array.Add(CICE_E);
                    }
                }


                //llenar datos resumo total de Condiçoes
                ArrayList array2 = new ArrayList();
                foreach (DataRow dr in dt5.Rows)
                {
                    object total;
                    CICE_C = (string)dr["CICE_C"];

                    if (array2.IndexOf(CICE_C) < 0)
                    {

                        total = dt5.Compute(String.Format("Count(CICG)"), "CICE_C like '%" + CICE_C + "%'");

                        dt4.Rows.Add(new object[] { dr["CICE_C"], dr["CONDIÇOES"], Convert.ToInt32(total) });

                        array2.Add(CICE_C);
                    }
                }


                for (int i = 0; i < 4; i++)
                {
                    bool y = false;
                    switch (i)
                    {
                        case 0:
                            foreach (DataRow dr in dt4.Rows)
                            {
                                int n = Convert.ToInt32(dr["CONDIÇÃO"]);
                                int x = Convert.ToInt32(dr["Quantidade"]);
                                if (n == i)
                                {
                                    nud_C0.Value = x;
                                    y = true;
                                }
                            }
                            if (!y)
                            {
                                nud_C0.Value = 0;
                            }
                            break;
                        case 1:
                            foreach (DataRow dr in dt4.Rows)
                            {
                                int n = Convert.ToInt32(dr["CONDIÇÃO"]);
                                int x = Convert.ToInt32(dr["Quantidade"]);
                                if (n == i)
                                {
                                    nud_C1.Value = x;
                                    y = true;
                                }
                            }
                            if (!y)
                            {
                                nud_C1.Value = 0;
                            }
                            break;

                        case 2:
                            foreach (DataRow dr in dt4.Rows)
                            {
                                int n = Convert.ToInt32(dr["CONDIÇÃO"]);
                                int x = Convert.ToInt32(dr["Quantidade"]);
                                if (n == i)
                                {
                                    nud_C2.Value = x;
                                    y = true;
                                }
                            }
                            if (!y)
                            {
                                nud_C2.Value = 0;
                            }
                            break;

                        case 3:
                            foreach (DataRow dr in dt4.Rows)
                            {
                                int n = Convert.ToInt32(dr["CONDIÇÃO"]);
                                int x = Convert.ToInt32(dr["Quantidade"]);
                                if (n == i)
                                {
                                    nud_C3.Value = x;
                                    y = true;
                                }
                            }
                            if (!y)
                            {
                                nud_C3.Value = 0;
                            }
                            break;

                    }
                }



                for (int i = 0; i < 5; i++)
                {
                    bool y = false;
                    switch (i)
                    {
                        case 0:
                            foreach (DataRow dr in dt3.Rows)
                            {
                                int n = Convert.ToInt32(dr["ESTADO"]);
                                int x = Convert.ToInt32(dr["Quantidade"]);
                                if (n == i)
                                {
                                    nud_E0.Value = x;
                                    y = true;
                                }
                            }
                            if (!y)
                            {
                                nud_E0.Value = 0;
                            }
                            break;
                        case 1:
                            foreach (DataRow dr in dt3.Rows)
                            {
                                int n = Convert.ToInt32(dr["ESTADO"]);
                                int x = Convert.ToInt32(dr["Quantidade"]);
                                if (n == i)
                                {
                                    nud_E1.Value = x;
                                    y = true;
                                }
                            }
                            if (!y)
                            {
                                nud_E1.Value = 0;
                            }
                            break;

                        case 2:
                            foreach (DataRow dr in dt3.Rows)
                            {
                                int n = Convert.ToInt32(dr["ESTADO"]);
                                int x = Convert.ToInt32(dr["Quantidade"]);
                                if (n == i)
                                {
                                    nud_E2.Value = x;
                                    y = true;
                                }
                            }
                            if (!y)
                            {
                                nud_E2.Value = 0;
                            }
                            break;

                        case 3:
                            foreach (DataRow dr in dt3.Rows)
                            {
                                int n = Convert.ToInt32(dr["ESTADO"]);
                                int x = Convert.ToInt32(dr["Quantidade"]);
                                if (n == i)
                                {
                                    nud_E4.Value = x;
                                    y = true;
                                }
                            }
                            if (!y)
                            {
                                nud_E4.Value = 0;
                            }
                            break;
                        case 4:
                            foreach (DataRow dr in dt3.Rows)
                            {
                                int n = Convert.ToInt32(dr["ESTADO"]);
                                int x = Convert.ToInt32(dr["Quantidade"]);
                                if (n == i)
                                {
                                    nud_E3.Value = x;
                                    y = true;
                                }
                            }
                            if (!y)
                            {
                                nud_E3.Value = 0;
                            }
                            break;
                    }
                }

            }
            else
            {
                MessageBox.Show("Base de dados para preencher resumos nula ou vazia", "ERROR DE BASE DE DADOS",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

        }
        private void AssiganarValoresDimençoes(string CICG)
        {
            DataTable dt = Banco.ObterTodosOnde("CadastroGeralContenedores", "CICG", "'" + CICG + "'");
            if (dt.Rows.Count > 0)               
            {
                c.Largura = decimal.Parse(dt.Rows[0].Field<string>("Largura"));
                c.Altura = decimal.Parse(dt.Rows[0].Field<string>("Altura"));
                c.Cumprimento = decimal.Parse(dt.Rows[0].Field<string>("Cumprimento"));
                c.Foto = dt.Rows[0].Field<string>("Foto");
                c.CapacidadeGeral1 = Convert.ToInt32(dt.Rows[0].Field<Int64>("Capacidade"));
            }
        }
        private int EstablecerQuantidadeCICCadastradaEmDT(string CICG)
        {
            DataTable dt1 = Banco.Procurar("CadastroEspecificoContenedores", "*", "CICG", "'%" + CICG + "%'", "CICG, id asc");

            object total = dt1.Compute(String.Format("Count(CICG)"), "CICG = '" + CICG + "'");
            if (total != DBNull.Value)
            {
                return (int)total;
            }
            else { return 0; }

        }
        private int EstabelecerOValorDo_DigitoCICE(DataTable Dados)
        {
            int IdAnterior = 0;
            int IdAtual;
            int control = 0;
            int control2 = 0;
            int id;
            int n = 0;

            foreach (DataRow dr in Dados.Rows)
            { 
                
                EstablecerValorIndiceDeCICEeCIC((string)dr["CICE"], (string)dr["CICG"]);
                if (control2 >= control)
                {
                  
                    IdAtual = CIC.DigitoCICE - IdAnterior;

                    if (IdAtual > 1)
                    {
                        id = IdAnterior + 1;
                        QIdVazios[n]= id;
                        n += 1;
                    }
                    

                }

                IdAnterior = CIC.DigitoCICE;
                control += 1;
                control2 = 1 + control;


               
            }

            return n;

        }

        private void assiganarValordebaseDeDadosDelDadoSeleccionadoAControlesBASE_DE_DATOS(DataTable dt, DataTable dt6)
        {

            if (dt.Rows.Count > 0)
            {
                btn_AgregarALista.Enabled = false;
                btn_Exluir.Visible = true;
                btn_Facturar.Text = "Atualizar";

                cb_Area.Enabled = false;
                cb_Tipo.Enabled = false;
                cb_Familia.Enabled = false;
                cb_Modelo.Enabled = false;
                cb_Caracteristica.Enabled = false;
                cb_EstadoAtual.Enabled = true;
                cb_Condições.Enabled = true;

                gb_Filtros.Enabled = true;
                gb_Quantidade.Enabled = false;
                gb_ResumoCondiçoes.Enabled = false;
                gb_ResumoDeEstadoCIC.Enabled = false;
                nud_Capacidade.Enabled = true;
                nud_IOE.Enabled = true;
                btn_NovaOperação.Text = "NOVO CADASTRO";
                nud_C3.Enabled = true;
                nud_E4.Enabled = true;
                modo = 0;

                dgv_GestãoContenedores.ReadOnly = true;


                CICG = dt.Rows[0].Field<string>("CICG");
                CICE = dt.Rows[0].Field<string>("CICE");
                AssignarValoresACadaComponenteCIC(CICE, CICG);
                nud_Quantidade.Value = EstablecerQuantidadeCICCadastradaEmDT(CICG);


                PreencherNudsResumoESTADOCIC(CICG);

                AssiganarValoresDimençoes(CICG);
                Dz.Value = c.Altura;
                Dx.Value = c.Cumprimento;
                Dy.Value = c.Largura;
                nud_CI.Value = c.CapacidadeGeral1;
                pb_FotoContenedor.ImageLocation = c.Foto;
                DestinoCompleto = c.Foto;
                DestinoCompletoA= c.Foto;
                if (DestinoCompleto!= "")
                {
                    btn_AddFoto.Text = "Trocar Foto";
                    btn_ExcluirFoto.Enabled = true;
                }
                else
                {
                    btn_AddFoto.Text = "Add Foto";
                    btn_ExcluirFoto.Enabled = false;
                }
                tb_NomeCIC.Text = dt.Rows[0].Field<string>("NomeCIC");
                cb_EstadoAtual.SelectedIndex = (int)dt.Rows[0].Field<Int64>("ESTADO");

                cb_UbicaçãoAtual.SelectedValue = dt.Rows[0].Field<Int64>("UbicaçãoAtual").ToString();
                cb_Condições.SelectedValue = dt.Rows[0].Field<Int64>("CONDIÇOES").ToString();


                nud_Capacidade.Value = dt.Rows[0].Field<Int64>("Capacidade");
                nud_IOE.Value = dt.Rows[0].Field<Int64>("IOE");

            }
            else
            {
                MessageBox.Show("Nehum Dado encontrado com " + IdSeleccionados);
            }

        }
        private void EstablecerRowselecionada(DataTable dt, string CICG)
        {

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i].Field<string>("CICG") == CICG)
                {
                    index = i;
                    return;
                }

            }

        }
        private void assiganarValordebaseDeDadosDelDadoSeleccionadoAControlesAGREDOSALISTA(DataTable dt, DataTable dt6, string CICG)
        {
           
            if (dt.Rows.Count > 0)
            {
               
                btn_AgregarALista.Enabled = false;
            
                EstablecerRowselecionada(dt, CICG);

                AssignarValoresACadaComponenteCIC(CICE, CICG);

                if (((DataTable)dgv_GestãoContenedores.DataSource).Rows.Count > 0)
                {
                    EstablecerRowselecionada((DataTable)dgv_GestãoContenedores.DataSource, CICG);
                    nud_Quantidade.Value = int.Parse(((DataTable)dgv_GestãoContenedores.DataSource).Rows[index].Field<string>("Quantidade"));

                }
                else
                {
                    nud_Quantidade.Value = 0;
                }

                PreencherNudsResumoESTADOCIC(dt,CICG);
                
                if (dt6.Rows.Count>0)
                {
                    Dz.Value = decimal.Parse(dt6.Rows[0].Field<string>("Cumprimento"));
                    Dx.Value = decimal.Parse(dt6.Rows[0].Field<string>("Largura"));
                    Dy.Value = decimal.Parse(dt6.Rows[0].Field<string>("Altura"));
                    pb_FotoContenedor.ImageLocation = dt6.Rows[0].Field<string>("Foto");
                    DestinoCompletoA = dt6.Rows[0].Field<string>("Foto");
                    DestinoCompleto = dt6.Rows[0].Field<string>("Foto");
                    if (pb_FotoContenedor.ImageLocation != "")
                    {
                        btn_AddFoto.Text = "Trocar Foto";
                        btn_ExcluirFoto.Enabled = true;
                    }
                    else
                    {
                        btn_AddFoto.Text = "Add Foto";
                        btn_ExcluirFoto.Enabled = false;
                    }
                    

                    nud_CI.Value = int.Parse(dt6.Rows[0].Field<string>("Capacidade"));

                }
                else
                {
                    Dz.Value = 0;
                    Dx.Value = 0;
                    Dy.Value = 0;
                    pb_FotoContenedor.ImageLocation = "";
                    nud_CI.Value = 0;
                }
               
               

                tb_NomeCIC.Text = dt.Rows[index].Field<string>("NomeCIC");
                cb_EstadoAtual.SelectedValue = "";
                
                cb_UbicaçãoAtual.SelectedValue = dt.Rows[index].Field<string>("UbicaçãoAtual").ToString();
                cb_Condições.SelectedValue = "";                
                nud_Capacidade.Value = int.Parse(dt.Rows[index].Field<string>("Capacidade"));
                nud_IOE.Value = int.Parse(dt.Rows[index].Field<string>("IOE"));
               
            
               
               
            }
            else
            {
                MessageBox.Show("Nehum Dado encontrado com " + IdSeleccionados,"SEM DADOS",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

        }

        private void AssignarValoresACadaComponenteCIC(string CICE, string CICG)
        {

            EstablecerValorIndiceDeCICEeCIC(CICE, CICG);

            tb_AU_CIC.Text = CIC.DigitoAreaUbicacão;
            tb_Tipo_CIC.Text = CIC.DigitoTipoContenedor;
            nud_Familia_CIC.Value = CIC.DigitoFamilia;
            nud_Modelo_CIC.Value = CIC.DigitoModelo;
            nud_Caracteristica_CIC.Value = CIC.DigitoCaracteristica;
            nud_DigitoCICE.Value = CIC.DigitoCICE;
            nud_Setor_CIC.Value = CIC.DigitoSetor;

        }
        private void EstablecerValorIndiceDeCICEeCIC(string CICE, string CICG)
        {
            EstablecerIndiceDeSimbolosCICEeCIC(CICE, CICG); 
            CIC.DigitoCICE = int.Parse(CICE.Substring(0, IndicedeSimbolos[0]));
            CIC.DigitoAreaUbicacão = CICG.Substring(1, 1);
            CIC.DigitoTipoContenedor = CICG.Substring(0, 1);
            CIC.DigitoFamilia = int.Parse(CICE.Substring(IndicedeSimbolos[1] + 1, IndicedeSimbolos[2] - (IndicedeSimbolos[1] + 1)));
            CIC.DigitoModelo = int.Parse(CICG.Substring(IndexPontoCIC[0] + 1, IndexPontoCIC[1] - (IndexPontoCIC[0] + 1)));
            CIC.DigitoCaracteristica = int.Parse(CICG.Substring(IndexPontoCIC[1] + 1, IndexPontoCIC[2] - (IndexPontoCIC[1] + 1)));
            CIC.DigitoSetor = int.Parse(CICG.Substring(IndexPontoCIC[1] + 1, CICG.Length - (IndexPontoCIC[2] + 1)));

        }

        private void RestablecerDTResumos()
        {
            dt3 = new DataTable();
            dt3.Columns.Add("CICE_E");
            dt3.Columns.Add("ESTADO");
            dt3.Columns.Add("Quantidade");

            dt4 = new DataTable();
            dt4.Columns.Add("CICE_C");
            dt4.Columns.Add("CONDIÇÃO");
            dt4.Columns.Add("Quantidade");

            dt5 = new DataTable();
            dt5.Columns.Add("CICE");
            dt5.Columns.Add("CICG");
            dt5.Columns.Add("NomeCIC");
            dt5.Columns.Add("Capacidade");
            dt5.Columns.Add("IOE");
            dt5.Columns.Add("CapacidadeEsperada");
            dt5.Columns.Add("UbicaçãoAtual");
            dt5.Columns.Add("ESTADO");
            dt5.Columns.Add("CONDIÇOES");
            dt5.Columns.Add("CICE_E");
            dt5.Columns.Add("CICE_C");
            dt5.Columns.Add("CICE_EC");
           
         
        }
        private void RestablecerDTs()
        {
            dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("CICE");
            dt.Columns.Add("CICG");
            dt.Columns.Add("NomeCIC");
            dt.Columns.Add("Capacidade");
            dt.Columns.Add("IOE");
            dt.Columns.Add("CapacidadeEsperada");
            dt.Columns.Add("UbicaçãoAtual");
            dt.Columns.Add("ESTADO");
            dt.Columns.Add("CONDIÇOES");
            dt.Columns.Add("CICE_E");
            dt.Columns.Add("CICE_C");
            dt.Columns.Add("CICE_EC");
                      
           

            dt2 = new DataTable();
            dt2.Columns.Add("CICG");
            dt2.Columns.Add("NomeCIC");
            dt2.Columns.Add("Quantidade");

            dt6 = new DataTable();
            dt6.Columns.Add("CICG");       
            dt6.Columns.Add("Quantidade");                 
            dt6.Columns.Add("X");
            dt6.Columns.Add("Y");
            dt6.Columns.Add("Z");
            dt6.Columns.Add("Foto");
            dt6.Columns.Add("Capacidade");
            dt6.Columns.Add("Massa");




            dgv_GestãoContenedores.DataSource = dt;
            sorteddt2 = dt2;
        }
        private bool EstaEntabla(DataTable tabla)
        {

            foreach (DataRow dr in tabla.Rows)
            {
                if ((string)dr["CICG"] == CICG)
                {
                    return true;
                }
                
            }
            return false;
           
        }
        private int QuantosEstadosAtivos()
        {
            return (E0 + E1 + E2 + E3 + E4);
        }
        private int QualEsatdoActivo()
        {
            if (E0>0)
            {
                return 0;
            }
            else
            {
                if (E1 > 0)
                {
                    return 1;
                }
                else
                {
                    if (E2 > 0)
                    {
                        return 2;
                    }
                    else
                    {
                        if (E3 > 0)
                        {
                            return 3;
                        }
                        else
                        {
                            if (E4 > 0)
                            {
                                return 4;
                            }
                            else
                            {
                                return -1;
                            }
                        }
                    }
                }
            }
        }
        private bool EstanTodosAssignados()
        {
            bool TodosAtribuidos= true;

            if (lb_C1.Text == lb_C3.Text)
            {                
                CONDIÇÃO = "COMPLETAS";
            }
            else
            {
                TodosAtribuidos = false;
                CONDIÇÃO = "FALTAM";
            }

            if (lb_E0.Text == lb_E2.Text)
            {               
                ESTADO = "COMPLETAS";
            }
            else
            {
                TodosAtribuidos = false;
                ESTADO = "FALTAM";
            }


            return TodosAtribuidos;
        }
        private void AtribuirValoresAMatrizEstadoCondição(int Estado)
        {
           
                for (int Condição = 0; Condição < 3; Condição++)
                {                                        
                            switch (Condição)
                            {
                                case 0:
                                    CICE_Condiçoes[Estado, Condição] = Convert.ToInt32(nud_C0.Value);
                                    break;
                                case 1:
                                    CICE_Condiçoes[Estado, Condição] = Convert.ToInt32(nud_C1.Value);
                                    break;
                                case 2:
                                    CICE_Condiçoes[Estado, Condição] = Convert.ToInt32(nud_C3.Value);
                                    break;
                            }                        
                     
                }
            
        }
        private void AtualizarDGVdepoisdeExcluir(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                ArrayList array = new ArrayList();
                foreach (DataRow dr in dt.Rows)
                {
                    object total;

                    if (array.IndexOf(dr["CICG"]) < 0)
                    {

                        total = dt.Compute(String.Format("Count(CICG)"), "CICG = '" + dr["CICG"] + "'");
                        dt2.Rows.Add(new object[] { dr["CICG"], dr["NomeCIC"], Convert.ToInt32(total) });
                        dt6.Rows.Add(new object[] { dr["CICG"], Convert.ToInt32(total), Dx.Value, Dy.Value, Dz.Value, DestinoCompleto, nud_CI.Value });

                        array.Add(dr["CICG"]);
                    }
                }

                DataView dv = dt2.DefaultView;
                dv.Sort = "CICG";
                sorteddt2 = dv.ToTable();
                dgv_GestãoContenedores.DataSource = sorteddt2;
                AtualizarSeleccion();
                return;
            }
            else
            {
                RestablecerDTs();
                dgv_GestãoContenedores.DataSource = dt2;
                ConfigurarTodoParaAgregarNovo1();
            }
        }
        private void ExcluirQuandoEstaEmModoNovo(DataTable dt,string  CICG)
        { 
            if (dt.Rows.Count > 0)
            {
                bool f;
                string Resposta;
                bool x;
                do
                {
                    f = true;
                    Resposta = Interaction.InputBox("DIGITE O NUMERO DA SUA OPÇÃO PARA ELIMINAR:\n\n 1.-TODOS         2.-Inserir o valor a deletar\n\n", "OPÇÕES");
                    x = false;
                    if (Resposta != "")
                    {
                        foreach (char c in Resposta)
                        {
                            
                            if (!char.IsDigit(c) || c < '1' || c > '2')
                            {
                               
                                x = true;
                            }

                        }

                        if (x)
                        {
                            DialogResult res = MessageBox.Show("Só pode inserir os Valores 1 ou 2\n\n TENTAR NOVAMENTE? ", "DADO INVALIDO", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                            if (res == DialogResult.No)
                            {
                                return;
                            }
                            else
                            {
                                f = false;
                            }

                        }
                    }
                    else
                    {
                        DialogResult res = MessageBox.Show("Deseja Cancelar a Operação?", "CANCELAR", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (res == DialogResult.Yes)
                        {
                            return;
                        }
                        else
                        {
                            f = false;
                        }
                    }

                } while (!f);

                if (Resposta == "1")
                {
                    dt.AcceptChanges();

                    for (int i = dt.Rows.Count - 1; i >= 0; i--)
                    {
                        DataRow dr = dt.Rows[i];
                        if ((string)dr["CICG"] == CICG)
                        {
                            dr.Delete();
                        }

                    }

                    dt.AcceptChanges();

                    AtualizarDGVdepoisdeExcluir(dt);
                }
                else
                {
                    do
                    {
                        f = true;
                        Resposta = Interaction.InputBox("\n\nDIGITE A QUANTIDADE DE CONTENEDORES QUE DESEJA DELETAR\n\n", "DIGITE O VALOR");
                        x = false;
                        if (Resposta != "")
                        {
                            foreach (char c in Resposta)
                            {
                                if (!char.IsDigit(c))
                                {
                                    x = true;
                                }
                                
                            }

                            if (x)
                            {
                                DialogResult res = MessageBox.Show("Só pode inserir Valores Numéricos\n\n TENTAR NOVAMENTE? ", "DADO INVALIDO", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                                if (res == DialogResult.No)
                                {
                                    return;
                                }
                                else
                                {
                                    f = false;
                                }

                            }
                            else
                            {
                                if (int.Parse(Resposta)>dt.Rows.Count|| int.Parse(Resposta) < 0 )
                                {
                                    DialogResult res = MessageBox.Show("O valor inserido deve ser maior ou igual a zero e menor ou igual à quantidade de contenedores\n\n TENTAR NOVAMENTE? ", "DADO INVALIDO", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                                    if (res == DialogResult.No)
                                    {
                                        return;
                                    }
                                    else
                                    {
                                        f = false;
                                    }
                                }
                            }
                        }
                        else
                        {
                            DialogResult res = MessageBox.Show("Deseja Cancelar a Operação?", "CANCELAR", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (res == DialogResult.Yes)
                            {
                                return;
                            }
                            else
                            {
                                f = false;
                            }
                        }

                    } while (!f);
                 
                    dt.AcceptChanges();

                    for (int i = dt.Rows.Count - 1; i >= (dt.Rows.Count - int.Parse(Resposta)); i--)
                    {                       
                        DataRow dr = dt.Rows[i];
                        if ((string)dr["CICG"] == CICG)
                        {
                            dr.Delete();
                        }
                    }

                    dt.AcceptChanges();
                    AtualizarDGVdepoisdeExcluir(dt);
                 
                }

             
            }
            else
            {
                MessageBox.Show("Não há dados para exluir", "SEM DADOS NA TÁBUA",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }
        }
        private void AgregarAListaDGV()
        {
            if (!EstanTodosAssignados())
            {
                MessageBox.Show("Deve atribuir todos os campos para Estado e Condição\n\nRESUMO:\n\nEstado=  " + ESTADO+"\nCondição= "+CONDIÇÃO, "FALTAM POR ATRIBUIR",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            if (QuantosEstadosAtivos()>1 )
            {
                do
                {
                    F_EspecificarEStadoCondição f = new F_EspecificarEStadoCondição(this);
                    Globais.Abreform(1, f);

                    if (!Assignou)
                    {

                        DialogResult res = MessageBox.Show("VALORES NÃO ASSINADOS\n\nDeseja cancelar a Operação? ", "Cancelar", MessageBoxButtons.YesNo);
                        if (res == DialogResult.Yes)
                        {
                            return;

                        }


                    }
                } while (!Assignou);
            }
            else
            {
                if (QuantosEstadosAtivos() == 1)
                {
                    AtribuirValoresAMatrizEstadoCondição(QualEsatdoActivo());
                }
                else
                {
                    MessageBox.Show("Deve atribuir valores aos campos ESTADO e CONDIÇÂO antes de adicionar");
                    return;
                }
            }
            cont = 0;
            int suma ;
            AtualizarCICApartirdeNudEtextbox();
            NomeCIC = tb_NomeCIC.Text;            
            
            ArrayList array= new ArrayList();

            DataTable dt2 = new DataTable();           
            dt2.Columns.Add("CICG");
            dt2.Columns.Add("NomeCIC");
            dt2.Columns.Add("Quantidade");
            
            if (dt.Rows.Count > 0 && EstaEntabla(dt) == true)
            {               
                List<DataRow> ListaFilas = new List<DataRow>();
                DataTable dt43 = Banco.Procurar("CadastroEspecificoContenedores", "*", "CICG", "'" + CICG + "%'", "CICG, id asc");
                int cont1 = EstabelecerOValorDo_DigitoCICE(dt43);
                cont = dt43.Rows.Count;
                suma = cont1 + cont;

              

                foreach (DataRow dr in dt.Rows)
                {
                    object total;
                   
                    if (array.IndexOf(dr["CICG"]) < 0 )
                    {
                        total = dt.Compute(String.Format("Count(CICG)"), "CICG = '" + dr["CICG"] + "'");                       
                       
                        if ((string)dr["CICG"]==CICG)
                        {
                            for (int w = 0; w < 5; w++)
                            {
                                for (int i = 0; i < 4; i++)
                                {
                                    bool k = true;
                                    for (int l = 0; l < CICE_Condiçoes[w, i]; l++)
                                    {
                                        DataRow row = dt.NewRow();

                                        if (k)
                                        {
                                            if (cont1 > 0)
                                            {
                                                if (cont1 > l)
                                                {
                                                    row["id"] = QIdVazios[l];
                                                    row["CICE"] = QIdVazios[l] + "-" + CICG;
                                                }
                                                else
                                                {
                                                    suma += (int)total + 1;
                                                    row["id"] = suma;
                                                    row["CICE"] = suma + "-" + CICG;
                                                }

                                            }
                                            else
                                            {
                                                cont += (int)total + 1;
                                                row["id"] = cont;
                                                row["CICE"] = cont + "-" + CICG;
                                            }

                                           
                                            k = false;
                                        }
                                        else
                                        {
                                            if (cont1 > 0)
                                            {
                                                if (cont1 > l)
                                                {
                                                    row["id"] = QIdVazios[l];
                                                    row["CICE"] = QIdVazios[l] + "-" + CICG;
                                                }
                                                else
                                                {
                                                    suma += 1;
                                                    row["id"] = suma;
                                                    row["CICE"] = suma + "-" + CICG;
                                                }

                                            }
                                            else
                                            {
                                                cont +=  1;
                                                row["id"] = cont;
                                                row["CICE"] = cont + "-" + CICG;
                                            }
                                           
                                        }
                                       

                                        row["CICG"] = CICG;
                                        row["NomeCIC"] = NomeCIC;
                                        row["Capacidade"] = Convert.ToInt32(nud_Capacidade.Value);
                                        row["IOE"] = nud_IOE.Value;
                                        row["CapacidadeEsperada"] = Math.Round((nud_Capacidade.Value * (nud_IOE.Value / 100)), 0);
                                        row["UbicaçãoAtual"] = cb_UbicaçãoAtual.SelectedValue;
                                        row["ESTADO"] = w;
                                        row["CONDIÇOES"] = i;
                                        row["CICE_EC"] = CICG + "_" + w + "_" + i;
                                        row["CICE_E"] = CICG + "_" + w;
                                        row["CICE_C"] = CICG + "_" + i;

                                        ListaFilas.Add(row);


                                    }

                                }
                            }
                            
                        }

                        array.Add(dr["CICG"]);
                    }
                }

             
                foreach (DataRow dr in ListaFilas)
                {                    
                    dt.Rows.Add(dr);
                   
                }


                array = new ArrayList();
                foreach (DataRow dr in dt.Rows)
                {
                    object total;

                    if (array.IndexOf(dr["CICG"]) < 0)
                    {
                       
                        total = dt.Compute(String.Format("Count(CICG)"), "CICG = '" + dr["CICG"] + "'");
                        dt2.Rows.Add(new object[] { dr["CICG"], dr["NomeCIC"], Convert.ToInt32(total) });
                   
                        dt6.Rows.Add(new object[] { dr["CICG"], Convert.ToInt32(total), Dx.Value, Dy.Value, Dz.Value, DestinoCompleto, nud_CI.Value, nud_Massa.Value});
                       
                        array.Add(dr["CICG"]);
                    }
                }

                DataView dv = dt2.DefaultView;
                dv.Sort = "CICG";
                sorteddt2 = dv.ToTable();
                dgv_GestãoContenedores.DataSource = sorteddt2;
            }
            else
            {
             
                DataTable dt43 = Banco.Procurar("CadastroEspecificoContenedores", "*", "CICG" , "'" + CICG + "%'", "CICG, id asc");
               int  cont1 = EstabelecerOValorDo_DigitoCICE(dt43);
                cont = dt43.Rows.Count;
                suma = cont1 + cont;
               
                for (int w = 0; w < 5; w++)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        for (int l = 0; l < CICE_Condiçoes[w, i]; l++)
                        {
                            DataRow row = dt.NewRow();
                            

                            if (cont1 > 0 )
                            {
                                if (cont1>l)
                                {
                                    row["id"] = QIdVazios[l];
                                    row["CICE"] = QIdVazios[l] + "-" + CICG;
                                }
                                else
                                {
                                    suma += 1;
                                    row["id"] = suma;
                                    row["CICE"] = suma+"-" + CICG;
                                }
                               
                            }
                            else
                            {
                                cont += 1;
                                row["id"] = cont;
                                row["CICE"] = cont + "-" + CICG;
                            }
                       
                            row["CICG"] = CICG;
                            row["NomeCIC"] = NomeCIC;
                            row["Capacidade"] = Convert.ToInt32(nud_Capacidade.Value);
                            row["IOE"] = nud_IOE.Value;
                            row["CapacidadeEsperada"] = Math.Round((nud_Capacidade.Value * (nud_IOE.Value / 100)), 0);
                            row["UbicaçãoAtual"] = cb_UbicaçãoAtual.SelectedValue;
                            row["ESTADO"] = w;
                            row["CONDIÇOES"] = i;
                            row["CICE_EC"] = CICG + "_" + w + "_" + i;
                            row["CICE_E"] = CICG + "_" + w;
                            row["CICE_C"] = CICG + "_" + i;                       
                           
                            dt.Rows.Add(row);
                           

                        }

                    }
                }
                

                array = new ArrayList();
                foreach (DataRow dr in dt.Rows)
                {
                    object total;

                    if (array.IndexOf(dr["CICG"]) < 0)
                    {  
                        total = dt.Compute(String.Format("Count(CICG)"), "CICG = '" + dr["CICG"] + "'");
                        dt2.Rows.Add(new object[] { dr["CICG"], dr["NomeCIC"], Convert.ToInt32(total) });
                        dt6.Rows.Add(new object[] { dr["CICG"],  Convert.ToInt32(total), Dx.Value, Dy.Value, Dz.Value, DestinoCompleto, nud_CI.Value, nud_Massa.Value });                  
                        array.Add(dr["CICG"]);
                    }
                    
                }

           

                DataView dv = dt2.DefaultView;
                dv.Sort = "CICG";
                sorteddt2 = dv.ToTable();
                dgv_GestãoContenedores.DataSource = sorteddt2;

                btn_MaisUm.Visible = true;
                lb_C1.Visible = false;
                lb_C2.Visible = false;
                lb_C3.Visible = false;
                lb_E0.Visible = false;
                lb_E1.Visible = false;
                lb_E2.Visible = false;
                ConfigurarTodoParaSalvar_dpoisdeApertarNovo();
                gb_Capacidade.Enabled = false;
                gb_Dimeções.Enabled = false;
                btn_AddFoto.Enabled = false;
                btn_ExcluirFoto.Enabled = false;
            }


        }

        private void AssignarTipoQuandoFamiliaCambia()
        {
            if ((Int64)cb_Familia.SelectedValue == 1)
            {
                if (cb_Area.SelectedIndex == 0 || cb_Area.SelectedIndex == 2)
                {
                    cb_Tipo.SelectedIndex = 0;
                }
                else
                {
                    cb_Tipo.SelectedIndex = 2;
                }

            }
            else
            {
                if ((Int64)cb_Familia.SelectedValue == 2)
                {
                    if (cb_Area.SelectedIndex == 0 || cb_Area.SelectedIndex == 2)
                    {
                        cb_Tipo.SelectedIndex = 1;
                    }
                    else
                    {
                        cb_Tipo.SelectedIndex = 2;
                    }

                }
                else
                {
                    if ((Int64)cb_Familia.SelectedValue == 3)
                    {
                        cb_Tipo.SelectedIndex = 2;
                    }
                    else
                    {
                        if ((Int64)cb_Familia.SelectedValue == 4)
                        {
                            if (cb_Area.SelectedIndex == 0 || cb_Area.SelectedIndex == 2)
                            {
                                cb_Tipo.SelectedIndex = 1;
                            }
                            else
                            {
                                cb_Tipo.SelectedIndex = 2;
                            }

                        }
                        else
                        {
                            if ((Int64)cb_Familia.SelectedValue == 5)
                            {
                                if (cb_Area.SelectedIndex == 0 || cb_Area.SelectedIndex == 2)
                                {
                                    cb_Tipo.SelectedIndex = 0;
                                }
                                else
                                {
                                    cb_Tipo.SelectedIndex = 2;
                                }

                            }
                            else
                            {
                                if ((Int64)cb_Familia.SelectedValue == 6)
                                {
                                    if (cb_Area.SelectedIndex == 0 || cb_Area.SelectedIndex == 2)
                                    {
                                        cb_Tipo.SelectedIndex = 0;
                                    }
                                    else
                                    {
                                        cb_Tipo.SelectedIndex = 2;
                                    }

                                }
                                else
                                {
                                    if ((Int64)cb_Familia.SelectedValue == 7)
                                    {
                                        if (cb_Area.SelectedIndex == 0 || cb_Area.SelectedIndex == 2)
                                        {
                                            cb_Tipo.SelectedIndex = 0;
                                        }
                                        else
                                        {
                                            cb_Tipo.SelectedIndex = 2;
                                        }

                                    }
                                    else
                                    {
                                        if ((Int64)cb_Familia.SelectedValue == 8)
                                        {
                                            cb_Tipo.SelectedIndex = 2;
                                        }
                                        else
                                        {
                                            if ((Int64)cb_Familia.SelectedValue == 9)
                                            {
                                                cb_Tipo.SelectedIndex = 1;
                                            }
                                            else
                                            {
                                                if ((Int64)cb_Familia.SelectedValue == 10)
                                                {
                                                    cb_Tipo.SelectedIndex = 1;
                                                }
                                                else
                                                {
                                                
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
        private void AtualizarNomeCIC()
        {
            if (cb_Familia.Text!="")
            {
                NomeCIC = tb_NomeCIC.Text = ("" + cb_Tipo.Text+ " DA " + cb_Setor.Text+ " PARA " + cb_Familia.Text + " " + cb_Modelo.Text + " "+cb_Caracteristica.Text+" "+cb_Area.Text).ToUpper();
                MessageBox.Show("quase assigno a");
                tb_NomeCIC.Text = NomeCIC;
                MessageBox.Show("assigno a");
            }
            else
            {
                if (cb_Modelo.SelectedIndex > 0 || cb_Caracteristica.SelectedIndex > 0 )
                {
                    cb_Modelo.SelectedIndex = 0;
                    cb_Caracteristica.SelectedIndex = 0;
                    NomeCIC = tb_NomeCIC.Text = ("" + cb_Tipo.Text + " PARA MATERIAL " + cb_Area.Text + "   DA " + cb_Setor.Text).ToUpper();
                    MessageBox.Show("quase assigno b");
                    tb_NomeCIC.Text = NomeCIC;
                    MessageBox.Show("assigno b");
                }
                //Que mierda cagaste todo
            }
               
        }
        private void Assignarvaloresde_AREAS_de_UBICACION_NORMAL_en_Funcion_De_TIPO()
        {
            if (cb_Tipo.SelectedIndex==0)//CARRINHO
            {
                Dictionary<string, string> Area = new Dictionary<string, string>();
                Area.Add("D", "DESCARGA");             
                Area.Add("R", "REFORMA");
             
                cb_Area.DataSource = new BindingSource(Area, null);
                cb_Area.DisplayMember = "Value";
                cb_Area.ValueMember = "key";

            }
            else
            {
                Dictionary<string, string> Area = new Dictionary<string, string>();
                Area.Add("D", "DESCARGA");
                Area.Add("C", "CARGA");
                Area.Add("R", "REFORMA");

                cb_Area.DataSource = new BindingSource(Area, null);
                cb_Area.DisplayMember = "Value";
                cb_Area.ValueMember = "key";
            }
        }
       
        private void AssiganarDADOSaCbxUBICAÇÂOATUALcomBaseenESTADO()
        {
            if (cb_EstadoAtual.SelectedIndex == 0 )//disponivel, disponivel danificado
            {
                if (cb_Area.Text!="REFORMA")
                {
                    if (cb_Area.Text == "DESCARGA")
                    {
                        Dictionary<string, string> AreaAtual = new Dictionary<string, string>();
                        AreaAtual.Add("0", "ESTOQUE FINAL");

                        cb_UbicaçãoAtual.DataSource = new BindingSource(AreaAtual, null);
                        cb_UbicaçãoAtual.DisplayMember = "Value";
                        cb_UbicaçãoAtual.ValueMember = "key";
                        cb_UbicaçãoAtual.Enabled = false;
                    }
                    else
                    {
                       
                        Dictionary<string, string> AreaAtual = new Dictionary<string, string>();
                        AreaAtual.Add("1", "ESTOQUE MATERIA PRIMA");

                        cb_UbicaçãoAtual.DataSource = new BindingSource(AreaAtual, null);
                        cb_UbicaçãoAtual.DisplayMember = "Value";
                        cb_UbicaçãoAtual.ValueMember = "key";
                        cb_UbicaçãoAtual.Enabled = false;
                    }
                  

                    Dictionary<string, string> CONDIÇÃO = new Dictionary<string, string>();
                    CONDIÇÃO.Add("0", "OTIMO");
                    CONDIÇÃO.Add("1", "DETERIORADO");
                    CONDIÇÃO.Add("2", "DANIFICADO");                 
                    cb_Condições.DataSource = new BindingSource(CONDIÇÃO, null);
                    cb_Condições.DisplayMember = "Value";
                    cb_Condições.ValueMember = "key";
                }
                else
                {
                    Dictionary<string, string> AreaAtual = new Dictionary<string, string>();
                    AreaAtual.Add("2", "ÁREA DE ANÁLISE DE PEÇAS");

                    cb_UbicaçãoAtual.DataSource = new BindingSource(AreaAtual, null);
                    cb_UbicaçãoAtual.DisplayMember = "Value";
                    cb_UbicaçãoAtual.ValueMember = "key";
                    cb_UbicaçãoAtual.Enabled = false;

                    Dictionary<string, string> CONDIÇÃO = new Dictionary<string, string>();
                    CONDIÇÃO.Add("0", "OTIMO");
                    CONDIÇÃO.Add("1", "DETERIORADO");
                    CONDIÇÃO.Add("2", "DANIFICADO");

                    cb_Condições.DataSource = new BindingSource(CONDIÇÃO, null);
                    cb_Condições.DisplayMember = "Value";
                    cb_Condições.ValueMember = "key";
                }
  
            }
            else
            {
                if (cb_EstadoAtual.SelectedIndex == 1)//em uso
                {
                    Dictionary<string, string> AreaAtual = new Dictionary<string, string>();
                    
                    AreaAtual.Add("0", "ESTOQUE FINAL");
                    AreaAtual.Add("1", "ESTOQUE MATERIA PRIMA");
                    AreaAtual.Add("2", "ÁREA DE ANÁLISE DE PEÇAS");
                    AreaAtual.Add("3", "ESTOQUE DE RETABALHO A");
                    AreaAtual.Add("4", "ESTOQUE DE RETRABALHO B");                                    
                    AreaAtual.Add("5", "PROCESSO I");
                    AreaAtual.Add("6", "PROCESSO II");               
                    AreaAtual.Add("7", "CLIENTE");
                    AreaAtual.Add("8", "CLIENTE SAC");
                    AreaAtual.Add("9", "FORNECEDOR");
                    AreaAtual.Add("10", "DESCARTE");

                    cb_UbicaçãoAtual.DataSource = new BindingSource(AreaAtual, null);
                    cb_UbicaçãoAtual.DisplayMember = "Value";
                    cb_UbicaçãoAtual.ValueMember = "key";
                    cb_UbicaçãoAtual.Enabled = true;

                    Dictionary<string, string> CONDIÇÃO = new Dictionary<string, string>();
                    CONDIÇÃO.Add("0", "OTIMO");
                    CONDIÇÃO.Add("1", "DETERIORADO");
                    CONDIÇÃO.Add("2", "DANIFICADO");
                    cb_Condições.DataSource = new BindingSource(CONDIÇÃO, null);
                    cb_Condições.DisplayMember = "Value";
                    cb_Condições.ValueMember = "key";
                }
                else
                {
                    if (cb_EstadoAtual.SelectedIndex == 2)//manutenção
                    {
                        Dictionary<string, string> AreaAtual = new Dictionary<string, string>();

                        AreaAtual.Add("11", "MANUTENÇÃO");

                        cb_UbicaçãoAtual.DataSource = new BindingSource(AreaAtual, null);
                        cb_UbicaçãoAtual.DisplayMember = "Value";
                        cb_UbicaçãoAtual.ValueMember = "key";
                        cb_UbicaçãoAtual.Enabled = false;

                        Dictionary<string, string> CONDIÇÃO = new Dictionary<string, string>();                 
                        CONDIÇÃO.Add("1", "DETERIORADO");
                        CONDIÇÃO.Add("2", "DANIFICADO");
                        CONDIÇÃO.Add("3", "ESTRAGADO");
                        
                        cb_Condições.DataSource = new BindingSource(CONDIÇÃO, null);
                        cb_Condições.DisplayMember = "Value";
                        cb_Condições.ValueMember = "key";

                    }
                    else
                    {
                        if (cb_EstadoAtual.SelectedIndex == 3)//descartado
                        {
                            Dictionary<string, string> AreaAtual = new Dictionary<string, string>();

                            AreaAtual.Add("10", "DESCARTE");

                            cb_UbicaçãoAtual.DataSource = new BindingSource(AreaAtual, null);
                            cb_UbicaçãoAtual.DisplayMember = "Value";
                            cb_UbicaçãoAtual.ValueMember = "key";
                            cb_UbicaçãoAtual.Enabled = false;

                            Dictionary<string, string> CONDIÇÃO = new Dictionary<string, string>();
                            CONDIÇÃO.Add("1", "DETERIORADO");
                            CONDIÇÃO.Add("2", "DANIFICADO");
                            CONDIÇÃO.Add("3", "ESTRAGADO");

                            cb_Condições.DataSource = new BindingSource(CONDIÇÃO, null);
                            cb_Condições.DisplayMember = "Value";
                            cb_Condições.ValueMember = "key";
                        }
                        else
                        {
                            if (cb_EstadoAtual.SelectedIndex == 4)//fora de uso
                            {
                                Dictionary<string, string> AreaAtual = new Dictionary<string, string>();
                                AreaAtual.Add("12", "DEPÓSITO");

                                cb_UbicaçãoAtual.DataSource = new BindingSource(AreaAtual, null);
                                cb_UbicaçãoAtual.DisplayMember = "Value";
                                cb_UbicaçãoAtual.ValueMember = "key";
                                cb_UbicaçãoAtual.Enabled = false;

                                Dictionary<string, string> CONDIÇÃO = new Dictionary<string, string>();
                                CONDIÇÃO.Add("0", "OTIMO");
                                CONDIÇÃO.Add("1", "DETERIORADO");
                                CONDIÇÃO.Add("2", "DANIFICADO");
                                CONDIÇÃO.Add("3", "ESTRAGADO");
                             
                                cb_Condições.DataSource = new BindingSource(CONDIÇÃO, null);
                                cb_Condições.DisplayMember = "Value";
                                cb_Condições.ValueMember = "key";
                            }

                        }
                    }
                }

            }
        }

        private void ConfigurarTodoParaAgregarNovo1()
        {   
            Espera = true;
            btn_AgregarALista.Enabled = true;
            btn_Exluir.Visible = true;
            btn_Facturar.Text = "Salvar";
            cb_Area.Enabled = true;
            cb_Tipo.Enabled = true;
            cb_Familia.Enabled = true;
            cb_Modelo.Enabled = true;
            cb_Caracteristica.Enabled = true;
            cb_Familia.SelectedIndex = 0;
            cb_Modelo.SelectedIndex = 0;
            cb_Caracteristica.SelectedIndex = 0;
            cb_EstadoAtual.SelectedValue = "";
            cb_Condições.SelectedValue = "";
            cb_EstadoAtual.Enabled = false;
            cb_Condições.Enabled = false;
            gb_Filtros.Enabled = false;
            gb_Quantidade.Enabled = true;
            gb_ResumoCondiçoes.Enabled = true;
            gb_ResumoDeEstadoCIC.Enabled = true;

            btn_AddFoto.Text = "Add Foto";
            btn_ExcluirFoto.Enabled = false;
            pb_FotoContenedor.ImageLocation = "";

            nud_Capacidade.Enabled = true;
            nud_IOE.Enabled = true;
            nud_C3.Enabled = false;
            nud_E4.Enabled = false;
            nud_Quantidade.Value = 0;
            nud_Quantidade.Maximum = 1000;
            nud_E0.Value = 0;
            nud_E1.Value = 0;
            nud_E2.Value = 0;
            nud_E4.Value = 0;
            nud_E3.Value = 0;


            nud_C0.Value = 0;
            nud_C1.Value = 0;
            nud_C2.Value = 0;
            nud_E4.Value = 0;

            lb_C1.Visible = true;//barras
            lb_C2.Visible = true;
            lb_C3.Visible = true;

            lb_E0.Visible = true;
            lb_E1.Visible = true;
            lb_E2.Visible = true;

            nud_E4.Enabled = false;
            nud_C3.Enabled = false;

            btn_NovaOperação.Text = "CANCELAR";
            dgv_GestãoContenedores.AllowUserToDeleteRows = true;
           
        }
        private void ConfigurarTodoParaSalvar_dpoisdeApertarNovo()
        {           
            btn_AgregarALista.Enabled = false;
            btn_Exluir.Visible = true;
          

            cb_Area.Enabled = false;
            cb_Tipo.Enabled = false;
            cb_Familia.Enabled = false;
            cb_Modelo.Enabled = false;
            cb_Caracteristica.Enabled = false;          

           
            gb_Quantidade.Enabled = false;
            gb_ResumoCondiçoes.Enabled = false;
            gb_ResumoDeEstadoCIC.Enabled = false;
            nud_Capacidade.Enabled = true;
            nud_IOE.Enabled = true;           
          
        }
        public bool SalvarArquivo()
        {
            if (DestinoCompleto == "")
            {
                if (MessageBox.Show("Foto não Selecionada, deseja continuar?", "Foto Não Selecionada", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return true;
                }
                return false;

            }
            else
            {
                try
                {
                    if (!File.Exists(DestinoCompleto))
                    {
                        if (DestinoCompleto != "")
                        {
                            if (OrigemCompleto != "")
                            {
                                File.Copy(OrigemCompleto, DestinoCompleto, sustituir);
                            }

                            if (File.Exists(DestinoCompleto))
                            {
                                pb_FotoContenedor.ImageLocation = DestinoCompleto;
                            }
                            else
                            {
                                if (MessageBox.Show("Foto não encontrada, deseja continuar?", "ERRO", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.No)
                                {
                                    return true;
                                }

                            }
                        }
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro: " + ex.Message, "ERRO", MessageBoxButtons.OK,MessageBoxIcon.Error);
                }

                return false;
            }

        }
        private void AtualizarSeleccion()
        {
            if (modo == 1)
            {

                int contilinha = dgv_GestãoContenedores.Rows.Count;

                if (contilinha > 0)
                {
                    int row = dgv_GestãoContenedores.CurrentRow.Index;
                    dgv_GestãoContenedores.Rows[row].Cells[0].Selected = true;

                    if (Control)
                    {
                        dgv_GestãoContenedores.Rows[linha].Cells[0].Selected = true;

                        dgv_GestãoContenedores.CurrentCell = dgv_GestãoContenedores[0, linha];
                        Control = false;

                    }

                    try
                    {
                       
                        if (dgv_GestãoContenedores.Rows.Count > 0 && dgv_GestãoContenedores.CurrentRow.Selected && m == true && dt.Rows.Count > 0)
                        {


                            IdSeleccionados = dgv_GestãoContenedores.Rows[dgv_GestãoContenedores.SelectedRows[0].Index].Cells[0].Value.ToString();
                          //  IdSeleccionados2 = dgv_GestãoContenedores.Rows[dgv_GestãoContenedores.SelectedRows[0].Index].Cells[1].Value.ToString();
                          
                           DataTable dt6 = Banco.Procurar("CadastroGeralContenedores", "*", "CICG", "'%" + IdSeleccionados + "%'", "CICG");
                          
                            assiganarValordebaseDeDadosDelDadoSeleccionadoAControlesAGREDOSALISTA(dt, dt6, IdSeleccionados);



                        }
                    }
                    catch (ArgumentOutOfRangeException ex)
                    {


                    }

                }
            }
            else
            {
                if (dgv_GestãoContenedores.Rows.Count > 0 && dgv_GestãoContenedores.CurrentRow.Selected && m == true )
                {
                    
                    IdSeleccionados = dgv_GestãoContenedores.Rows[dgv_GestãoContenedores.SelectedRows[0].Index].Cells[0].Value.ToString();
                    IdSeleccionados2 = dgv_GestãoContenedores.Rows[dgv_GestãoContenedores.SelectedRows[0].Index].Cells[1].Value.ToString();                  
                    DataTable dt = Banco.Procurar("CadastroEspecificoContenedores", "*", "CICE", "'%" + IdSeleccionados + "%'", "CICG, id asc");
                    DataTable dt6 = Banco.Procurar("CadastroGeralContenedores", "*", "CICG", "'%" + IdSeleccionados + "%'", "CICG");

                   
                    assiganarValordebaseDeDadosDelDadoSeleccionadoAControlesBASE_DE_DATOS(dt, dt6);
                  
                }
            }
        }
        private void VoltarAModo0()
        {
            modo = 0;
            ConfigurarTodoParaSalvar_dpoisdeApertarNovo();
            nud_C0.Maximum = 100;
            nud_C1.Maximum = 100;
            nud_C2.Maximum = 100;
            nud_C3.Maximum = 100;
            nud_E0.Maximum = 100;
            nud_E1.Maximum = 100;
            nud_E2.Maximum = 100;
            nud_E3.Maximum = 100;
            nud_E4.Maximum = 100;

            btn_AddFoto.Enabled = true;
            btn_ExcluirFoto.Enabled = true;
            gb_Capacidade.Enabled = true;
            gb_Dimeções.Enabled = true;
            nud_C3.Enabled = true;
            nud_E4.Enabled = true;
            cb_EstadoAtual.Enabled = true;
            cb_Condições.Enabled = true;
            btn_MaisUm.Visible = false;
            gb_Filtros.Enabled = true;
            btn_Facturar.Text = "Atualizar";
            btn_NovaOperação.Text = "NOVO CADASTRO";
            dgv_GestãoContenedores.AllowUserToDeleteRows = false;
            dgv_GestãoContenedores.DataSource = Banco.Procurar("CadastroEspecificoContenedores", "CICE, NomeCIC", "CICE", "'%'", "CICG, id asc");
            dgv_GestãoContenedores.Columns[0].Width = 60;
            dgv_GestãoContenedores.Columns[1].Width = 350;
        }

     
        #endregion;


        #region CAMBIOS DE SELECCION

        #region DGV CAMBIA
        private void dgv_GestãoContenedores_SelectionChanged(object sender, EventArgs e)
        {
            AtualizarSeleccion();          
        }
        #endregion;

        #region NUD e TB CAMBIA

        private void nud_Dcice_ValueChanged(object sender, EventArgs e)
        {
            Filtar();
        }

        private void Dx_ValueChanged(object sender, EventArgs e)
        {
            if (cb_Tipo.SelectedIndex != 0)
            {
                nud_CI.Value = (Dz.Value * Dx.Value * Dy.Value) / 1000;
                lb_UniCI.Text = "L";
            }
            else
            {
                lb_UniCI.Text = "P";
            }
        }

        private void Dy_ValueChanged(object sender, EventArgs e)
        {
            if (cb_Tipo.SelectedIndex != 0)
            {
                nud_CI.Value = (Dz.Value * Dx.Value * Dy.Value) / 1000;
                lb_UniCI.Text = "L";
            }
            else
            {
                lb_UniCI.Text = "P";
            }
        }

        private void Dz_ValueChanged(object sender, EventArgs e)
        {
            if (cb_Tipo.SelectedIndex != 0)
            {
                nud_CI.Value = (Dz.Value * Dx.Value * Dy.Value) / 1000;
                lb_UniCI.Text = "L";
            }
            else
            {
                lb_UniCI.Text = "P";
            }
        }
        private void nud_IOE_ValueChanged(object sender, EventArgs e)
        {
            if (nud_IOE.Value > 99)
            {
                nud_IOE.Value = 100;
            }
            if (nud_IOE.Value < 1)
            {
                nud_IOE.Value = 0;
            }
        }

        private void nud_Caracteristica_CIC_ValueChanged(object sender, EventArgs e)
        {

            nud_Caracteristica_CICE.Value = cb_Caracteristica.SelectedIndex = Convert.ToInt32(nud_Caracteristica_CIC.Value);
        }

        private void nud_Modelo_CIC_ValueChanged(object sender, EventArgs e)
        {
          
            nud_Modelo_CICE.Value= cb_Modelo.SelectedIndex = Convert.ToInt32(nud_Modelo_CIC.Value);
             
        }

        private void nud_Familia_CIC_ValueChanged(object sender, EventArgs e)
        {
           
            nud_Familia_CICE.Value= cb_Familia.SelectedIndex = Convert.ToInt32(nud_Familia_CIC.Value);
         
        }
        private void nud_Setor_CIC_ValueChanged(object sender, EventArgs e)
        {
            cb_Setor.SelectedIndex= Convert.ToInt32(nud_Setor_CIC.Value);
            nud_Setor_CICE.Value = Convert.ToInt32(nud_Setor_CIC.Value);
        }
        private void nud_Quantidade_ValueChanged(object sender, EventArgs e)
        {          
            AtualizarNud((NumericUpDown)sender);
           
            HabilitarouInhabilitarNudsRESUMOSemFunciondeQUANTIDADEE();
            
        }

        private void nud_Disponiveis_ValueChanged(object sender, EventArgs e)
        {
            AtualizarNud((NumericUpDown)sender);
        }

        private void nud_EnUso_ValueChanged(object sender, EventArgs e)
        {
            AtualizarNud((NumericUpDown)sender);
        }

        private void nud_EmManutenção_ValueChanged(object sender, EventArgs e)
        {
            AtualizarNud((NumericUpDown)sender);
        }

        private void nud_Descartados_ValueChanged(object sender, EventArgs e)
        {
            AtualizarNud((NumericUpDown)sender);
        }

        private void nud_ForadeUso_ValueChanged(object sender, EventArgs e)
        {
            AtualizarNud((NumericUpDown)sender);
        }

        private void nud_Otimos_ValueChanged(object sender, EventArgs e)
        {
            AtualizarNud((NumericUpDown)sender);
        }

        private void nud_Deteriorados_ValueChanged(object sender, EventArgs e)
        {
            AtualizarNud((NumericUpDown)sender);
        }

        private void nud_Danificado_ValueChanged(object sender, EventArgs e)
        {
            AtualizarNud((NumericUpDown)sender);
        }

        private void nud_Estragados_ValueChanged(object sender, EventArgs e)
        {
            AtualizarNud((NumericUpDown)sender);
        }

        private void tb_Tipo_CIC_TextChanged(object sender, EventArgs e)
        {
            cb_Tipo.SelectedValue =tb_Tipo_CICE.Text= tb_Tipo_CIC.Text;
        }

        private void tb_AU_CIC_TextChanged(object sender, EventArgs e)
        {
            cb_Area.SelectedValue =tb_AU_CICE.Text= tb_AU_CIC.Text;
        }

        #endregion;

        #region COMBOX CAMBIA
        private void cb_FiltrarAreaDeUbicação_SelectedIndexChanged(object sender, EventArgs e)
        {
            Filtar();
        }

        private void cb_FiltarTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Filtar();
        }


        private void cb_FiltarFamilia_SelectedIndexChanged(object sender, EventArgs e)
        {
            Filtar();
        }

        private void cb_FiltarModelo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Filtar();
        }

        private void cb_FIltrarCaracteristica_SelectedIndexChanged(object sender, EventArgs e)
        {
            Filtar();
        }
        private void cb_EstadoAtual_SelectedIndexChanged(object sender, EventArgs e)
        {
            AssiganarDADOSaCbxUBICAÇÂOATUALcomBaseenESTADO();
        }
        int cont5 = 0;
        private void cb_Area_SelectedIndexChanged(object sender, EventArgs e)
        {
            AtualizarNomeCIC();
            cb_UbicaçãoAtual.Text = cb_Area.Text;

            if (cont5 > 2)
            {             
                tb_AU_CIC.Text = tb_AU_CICE.Text = (string)cb_Area.SelectedValue;
                Assignarvaloresde_AREAS_de_UBICACION_NORMAL_en_Funcion_De_TIPO();
                AssignarTipoQuandoFamiliaCambia();
            }
            else { cont5 = cont5 + 1; }

            AssiganarDADOSaCbxUBICAÇÂOATUALcomBaseenESTADO();

        }      

        int cont6 = 0;
        private void cb_Tipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_Tipo.SelectedIndex != 0 || (cb_Tipo.SelectedIndex == 0 && cb_Familia.SelectedIndex == 5))
            {
                btn_calcular.Enabled = true;
            }
            else
            {
                btn_calcular.Enabled = false;
            }
           
            if (cont6 > 1)
            {
                tb_Tipo_CIC.Text = tb_Tipo_CICE.Text = (string)cb_Tipo.SelectedValue;

            }
            else
            {
                cont6 += 1;
            }
            AtualizarNomeCIC();
        }

        int cont7 = 0;
        private void cb_Familia_SelectedIndexChanged(object sender, EventArgs e)
        {
            AtualizarNomeCIC();
            if (cb_Tipo.SelectedIndex != 0 || (cb_Tipo.SelectedIndex == 0 && cb_Familia.SelectedIndex == 5))
            {
                btn_calcular.Enabled = true;
            }
            else
            {
                btn_calcular.Enabled = false;
            }

            if (cont7 > 1)
            {
               

                nud_Familia_CICE.Value = nud_Familia_CIC.Value = cb_Familia.SelectedIndex;
                AssignarTipoQuandoFamiliaCambia();
       
            }
            else
            {
                cont7 += 1;
            }
        }
        
        int cont8 = 0;
        private void cb_Modelo_SelectedIndexChanged(object sender, EventArgs e)
        {
            AtualizarNomeCIC();

            if (cont8 > 1)
            {
              
                nud_Modelo_CICE.Value = nud_Modelo_CIC.Value = cb_Modelo.SelectedIndex;
             
            }
            else
            {
                cont8 += 1;
            }


        }

        int cont9 = 0;
        private void cb_Caracteristica_SelectedIndexChanged(object sender, EventArgs e)
        {
            AtualizarNomeCIC();
            if (cont9 > 1)
            {
               
                nud_Caracteristica_CICE.Value = nud_Caracteristica_CIC.Value = cb_Caracteristica.SelectedIndex;
               
            }
            else
            {
                cont9 += 1;
            }

        }
        int cont10 = 0;
        private void cb_Setor_SelectedIndexChanged(object sender, EventArgs e)
        {
            AtualizarNomeCIC();
            if (cont10 > 1)
            {
                if (cb_Setor.SelectedIndex>0)
                {
                    nud_Setor_CIC.Value = nud_Setor_CICE.Value = cb_Setor.SelectedIndex;
                }
                else
                {
                    nud_Setor_CIC.Minimum = nud_Setor_CICE.Minimum = -1;
                    nud_Setor_CIC.Value = nud_Setor_CICE.Value = -1;

                }

            }
            else
            {
                cont10 += 1;
            }
        }

        #endregion;

        #endregion;


        #region BOTONES CLICK
        private void btn_AgregarALista_Click(object sender, EventArgs e)
        {
            AgregarAListaDGV();
            
        }      
       
        private void btn_Facturar_Click(object sender, EventArgs e)
        {
            if (modo != 1)//atualizar
            {
                AtualizarCICApartirdeNudEtextbox();
                AtualizarNomeCIC();

                if (SalvarArquivo())
                {
                    return;
                }
              
                DataTable Busca = Banco.Procurar("CadastroGeralContenedores","*", "CICG", "'"+CICG+"'", "CICG");
                if (Busca.Rows.Count>0)
                {
                    int Quantidade = Convert.ToInt32(Busca.Rows[0].Field<Int64>("N_Contenedores"));                   
                    int total = Convert.ToInt32(Math.Floor(nud_Capacidade.Value * (nud_IOE.Value / 100)));

                    string CICE_E = CICG + "_" + cb_EstadoAtual.SelectedValue;
                    string CICE_C = CICG + "_" + cb_Condições.SelectedValue;
                    string CICE_EC = CICE_E + "_" + cb_Condições.SelectedValue;
                   
                    bool AtualizouE=Banco.Atualizar("CadastroEspecificoContenedores", "id= '"+DigitoCICE+ "', CICE='" + CICE + "',CICG='" + CICG + "', NomeCIC='" + tb_NomeCIC.Text + "', Capacidade= '" + Math.Floor(nud_Capacidade.Value) + "' ,  IOE='" + Math.Floor(nud_IOE.Value) + "', CapacidadeEsperada ='" + total + "', UbicaçãoAtual= '" + cb_UbicaçãoAtual.SelectedValue + "', ESTADO= '" + cb_EstadoAtual.SelectedValue + "', CICE_E= '" + CICE_E + "', CONDIÇOES= '" + cb_Condições.SelectedValue + "', CICE_C= '" + CICE_C + "', CICE_EC= '" + CICE_EC + "'", "CICE", "'" + CICE + "'");
                    bool AtualizouG= Banco.Atualizar("CadastroGeralContenedores", "CICG='" + CICG + "', N_Contenedores= " + Quantidade + ", Cumprimento ='" + Dx.Value + "', Largura= '" + Dy.Value + "', Altura= '" + Dz.Value + "', Foto= '" + DestinoCompleto + "', Capacidade= '" + nud_CI.Value + "', Massa= '" + nud_Massa.Value + "'", "CICG", "'" + CICG + "'");
                    if (DestinoCompleto != DestinoCompletoA)
                    {
                        if (File.Exists(DestinoCompletoA))
                        {
                            File.Delete(DestinoCompletoA);
                            MessageBox.Show("Foto Antiga Excluida");
                        }
                    }

                    if (AtualizouE )
                    {
                        if (AtualizouG)
                        {
                            MessageBox.Show("Dados Atualizados");
                        }
                        else
                        {
                            MessageBox.Show("Erro Ao Atualizar Cadastro Geral");
                        }

                    }
                    else
                    {
                        if (AtualizouG)
                        {
                            MessageBox.Show("Erro Ao atualizar Cadastro Especifico");
                        }
                        else
                        {
                            MessageBox.Show("Não foi posivel atualizar nenhum dado");
                        }
                    }
                    dgv_GestãoContenedores.DataSource = Banco.Procurar("CadastroEspecificoContenedores", "CICE, NomeCIC", "CICE", "'%'", "CICG, id asc");
                    dgv_GestãoContenedores.Columns[0].Width = 60;
                    dgv_GestãoContenedores.Columns[1].Width = 350;
                }
                else
                {
                    MessageBox.Show("Dado não encontrado no Cadastro Geral","NÃO FOI POSSIVEL ATUALIZAR",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
               
            }
            else
            {
              

                if (dt.Rows.Count>0)
                {
                    AutorizarAção autorizar = new AutorizarAção(this);
                    Globais.Abreform(1, autorizar);
                    if (AçãoAutorizada)
                    {
                        
                        F_SalvarOUatualizarContenedores f_SalvarOUatualizar = new F_SalvarOUatualizarContenedores(dt, dt6,  dt.Rows.Count, this);
                        Globais.Abreform(1, f_SalvarOUatualizar);
                        VoltarAModo0();
                   
                    }
                    else
                    {
                        MessageBox.Show("Ação não Autorizada :(");
                    }
                }
                else
                {
                    MessageBox.Show("Não ha dados para salvar :(");
                }
               
                
            }

        }

        private void btn_Voltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Sair_Click(object sender, EventArgs e)
        {
            Globais.Sair();
        }
        
        private void btn_NovaOperação_Click(object sender, EventArgs e)
        {
            modo = modo+1;
            if (modo == 1)
            {
                ConfigurarTodoParaAgregarNovo1();
                dgv_GestãoContenedores.ReadOnly = true;
                RestablecerDTs();
                dgv_GestãoContenedores.DataSource = dt2;
                dgv_GestãoContenedores.Columns[0].Width = 60;
                dgv_GestãoContenedores.Columns[1].Width = 300;
                dgv_GestãoContenedores.Columns[2].Width = 70;

            }
            else
            {

                VoltarAModo0();
               
              
            }


        }           

        private void btn_Exluir_Click(object sender, EventArgs e)
        {
            if (modo == 1)
            {               
                ExcluirQuandoEstaEmModoNovo(dt, IdSeleccionados);
            }
            else
            {
                AtualizarCICApartirdeNudEtextbox();
                DataTable dt = Banco.Procurar("CadastroEspecificoContenedores","CICE, CICG, id","CICE","'"+CICE+"'", "CICG, id asc") ;

                if (dt.Rows.Count>1)
                {
                    DialogResult res = MessageBox.Show("Yes= exluir todos os dados\n\n No= Excluir somente o seleccionado", "Quais dados deseja exluir?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (res == DialogResult.Yes)
                    {
                        Banco.Excluir("CadastroEspecificoContenedores", "CICG", "'" + CICG + "'");
                        Banco.ExcluirDirecto("CadastroGeralContenedores", "CICG", "'" + CICG + "'");
                        if (File.Exists(DestinoCompleto))
                        {
                            File.Delete(DestinoCompleto);
                            MessageBox.Show("Arquivo Excluido");
                        }

                        dgv_GestãoContenedores.DataSource = Banco.Procurar("CadastroEspecificoContenedores", "CICE, NomeCIC", "CICE", "'%'", "CICG, id asc");

                    }
                    else
                    {
                        if (res == DialogResult.No)
                        {
                            Banco.Excluir("CadastroEspecificoContenedores", "CICE", "'" + CICE + "'");
                            dgv_GestãoContenedores.DataSource = Banco.Procurar("CadastroEspecificoContenedores", "CICE, NomeCIC", "CICE", "'%'", "CICG, id asc");
                        }
                    }
                }
                else
                {
                    DialogResult res = MessageBox.Show("Deseja exluir comletamente este dado do cadastro?", "ÚLTIMO DADO DO CADASTRO", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (res == DialogResult.Yes)
                    {
                        Banco.Excluir("CadastroEspecificoContenedores", "CICG", "'" + CICG + "'");
                        Banco.ExcluirDirecto("CadastroGeralContenedores", "CICG", "'" + CICG + "'");
                        if (File.Exists(DestinoCompleto))
                        {
                            File.Delete(DestinoCompleto);
                            MessageBox.Show("Arquivo Excluido");
                        }

                        dgv_GestãoContenedores.DataSource = Banco.Procurar("CadastroEspecificoContenedores", "CICE, NomeCIC", "CICE", "'%'", "CICG, id asc");

                    }

                }
            }
        }         
   
     
        
        private void btn_calcular_Click(object sender, EventArgs e)
        {
            F_CalculoDeCMAA f_Calculo = new F_CalculoDeCMAA(this, ""+nud_Familia_CIC.Value+"."+nud_Modelo_CIC.Value+"."+nud_Caracteristica_CIC.Value+"."+"0", Convert.ToInt32(nud_Familia_CIC.Value));
            Globais.Abreform(1,f_Calculo);
        }

        private void btn_ExcluirFoto_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja Excluir Permanentemente a Foto ubicada em:\n" + DestinoCompleto + " ?", "Excluir?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (File.Exists(DestinoCompleto))
                {
                    File.Delete(DestinoCompleto);
                    MessageBox.Show("Arquivo Excluido");
                }

                
                string bla = string.Empty;
                bool Excluiu = Banco.Atualizar("CadastroGeralContenedores", "Foto= '" + bla + "'", "Foto", "'" + pb_FotoContenedor.ImageLocation + "'");
                if (Excluiu)
                {
                    AtualizarSeleccion();
                    pb_FotoContenedor.ImageLocation = DestinoCompleto;
                    btn_AddFoto.Text = "Add Foto";
                    btn_ExcluirFoto.Enabled = false;
                    MessageBox.Show("Dados Atualizados");
                }
                else
                {
                    AtualizarSeleccion();
                    MessageBox.Show("Exclusão cancelada");
                    return;
                }
            }
        }

        private void btn_AddFoto_Click(object sender, EventArgs e)
        {

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                OrigemCompleto = openFileDialog1.FileName;
                foto = openFileDialog1.SafeFileName;
                DestinoCompleto = PastaDestino + foto;

            }
            else
            {
                return;
            }


            if (File.Exists(DestinoCompleto))
            {
                if (MessageBox.Show("Existe uma foto com o mesmo nome no cadastro, deseja substituir?\n\nIMPORTANTE:\nSe um contenedor diferente esta usando a foto com o mesmo nome pasará a usar tambem esta foto, recomenda-se renomear nesse caso.", "Sustituir", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    btn_AddFoto.Text = "Trocar Foto";
                    btn_ExcluirFoto.Enabled = true;
                    pb_FotoContenedor.ImageLocation = OrigemCompleto;
                    sustituir = true;
                }
                else
                {
                      return;
                }

            }
            else
            {
                sustituir = false;
                btn_AddFoto.Text = "Trocar Foto";
                btn_ExcluirFoto.Enabled = true;
                pb_FotoContenedor.ImageLocation = OrigemCompleto;

            }

        }

        private void btn_MaisUm_Click(object sender, EventArgs e)
        {
            ConfigurarTodoParaAgregarNovo1();
            gb_Capacidade.Enabled = true;
            gb_Dimeções.Enabled = true;
            btn_AddFoto.Enabled = true;
            btn_ExcluirFoto.Enabled = true;
            btn_AddFoto.Text = "Add Foto";
            btn_ExcluirFoto.Enabled = false;
            pb_FotoContenedor.ImageLocation = "";
        }

        private void Btn_VerFicha_Click(object sender, EventArgs e)
        {/*
            AtualizarCICApartirdeNudEtextbox();
            F_FichaDeProtocolo f = new F_FichaDeProtocolo(CICE, CICG);
            Globais.Abreform(1, f);*/
            AtualizarNomeCIC();

        }




        #endregion;

        private void dgv_GestãoContenedores_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void F_CadastroEControleDeContenedores_SizeChanged(object sender, EventArgs e)
        {

           // dgv_GestãoContenedores.Location = new Point(675, 8);
        }

       
    }
}
