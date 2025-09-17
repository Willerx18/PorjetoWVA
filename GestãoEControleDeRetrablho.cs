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


namespace Atlas_projeto
{
    public partial class F_GestãoEControleDeRetrablho : Form
    {
        #region VariablesGenerales
        DataTable dt;
        DataTable dtA;
        DataTable dt2;
        DataTable sorteddt2;
        Apontamento A = new Apontamento();
        bool EsUsuario1;
        bool EsUsuario2;
        bool debebloquereltextoUsu1;
        bool EsdefeitoEspecifico;
        public bool puedeActivar=false;
        public bool AçãoAutorizada = false;
        DataTable Dt_xD;
        DataTable Dt_xD2;
        DataTable Dt_geral2;
        DataTable Dt_geral1;
        DataTable Novo;
        DataTable Novo2;
        int Tipo;
        int DigitoMotivo1 = 0;
        int DigitoMotivo2 = 0;
        int NumeroDePuntos2 = 0;
        int NumeroDePuntos = 0;
        int NumeroDeDigitos;
        int[] m1 = new int[7];
        DataTable Defeitos = Banco.ObterTodos("FamiliasDefeitos", "*", "ID_Familia");
        DataTable DefeitosEspecificos = Banco.ObterTodos("TiposDefeitos", "*", "CID");
        Form2 F;
        string charTecla="";
        #endregion;

        public F_GestãoEControleDeRetrablho(int tipo, Form2 f)
        {
            InitializeComponent();
            Tipo = tipo;
            F = f;

            #region EscuhandoEventosDelTeclado

            tb_CódigoDefeito.KeyDown += tb_CódigoDefeito_KeyDown;
            tb_CódigoDefeito.KeyPress += tb_CódigoDefeito_KeyPress;
            tb_CódigoDefeito.KeyUp += tb_CódigoDefeito_KeyUp;

            tb_CódigoDefeito2.KeyDown += tb_CódigoDefeito2_KeyDown;
            tb_CódigoDefeito2.KeyPress += tb_CódigoDefeito2_KeyPress;
            tb_CódigoDefeito2.KeyUp += tb_CódigoDefeito2_KeyUp;

            #endregion;


        }

        #region ConfigDeCargaDoForm
        private void F_GestãoEControleDeRetrablho_Load(object sender, EventArgs e)
        {

            #region PopularComboBoxes

            #region PopulandoComboBoxesUsu1
            // populando ComboBoxFamilia
            cb_FamilaPeça.Items.Clear();
            cb_FamilaPeça.DataSource = Banco.ObterTodos("Familias", "*", "IdFamilia");
            cb_FamilaPeça.DisplayMember = "Familia";
            cb_FamilaPeça.ValueMember = "IdFamilia";


            //Populando ComboBox NomeCIP
            cb_NomeCIP.Items.Clear();
            cb_NomeCIP.DataSource = Banco.ObterTodos("Peças", "*", "CIP");
            cb_NomeCIP.DisplayMember = "NomeCIP";
            cb_NomeCIP.ValueMember = "CIP";

            //Populando ComboBox Clasificação
            Dictionary<string, string> TipoCadastro = new Dictionary<string, string>();

            TipoCadastro.Add("0", "");
            TipoCadastro.Add("R", "RETRABALHO");
            TipoCadastro.Add("S", "SUCATA");
            TipoCadastro.Add("B", "BOAS");

            cb_Clasificação.Items.Clear();
            cb_Clasificação.DataSource = new BindingSource(TipoCadastro, null);
            cb_Clasificação.DisplayMember = "Value";
            cb_Clasificação.ValueMember = "key";

            //Populando ComboBox DefeitoGeral

            cb_DefeitoGeral.Items.Clear();
            cb_DefeitoGeral.DataSource = Banco.ObterTodos("FamiliasDefeitos", "*", "ID_Familia");
            cb_DefeitoGeral.DisplayMember = "T_Nome";
            cb_DefeitoGeral.ValueMember = "ID_Familia";


            //Populando ComboBox Motivo
            Dictionary<string, string> Motivo = new Dictionary<string, string>();

            Motivo.Add("0", "");
            Motivo.Add("1", "Defeito Ainda Visivel");
            Motivo.Add("2", "Novo defeito");
            Motivo.Add("3", "Novo defeito + Defeito Anterior");
            cb_Motivo.Items.Clear();
            cb_Motivo.DataSource = new BindingSource(Motivo, null);
            cb_Motivo.DisplayMember = "Value";
            cb_Motivo.ValueMember = "key";

            //Populando Com boBox DefeitoEspecifico

            cb_DefeitoEspecifico.Items.Clear();
            cb_DefeitoEspecifico.DataSource = Banco.ObterTodos("TiposDefeitos", "*", "CID");
            cb_DefeitoEspecifico.DisplayMember = "NomeTipo";
            cb_DefeitoEspecifico.ValueMember = "CID";

            //Populando ComboBox DefeitoAnterior

            cb_DefeitoAnterior.Items.Clear();
            cb_DefeitoAnterior.DataSource = Banco.ObterTodos("TiposDefeitos", "*", "CID");
            cb_DefeitoAnterior.DisplayMember = "NomeTipo";
            cb_DefeitoAnterior.ValueMember = "CID";

            //Populando ComboBox DefeitoNovo

            cb_DefeitoNovo.Items.Clear();
            cb_DefeitoNovo.DataSource = Banco.ObterTodos("TiposDefeitos", "*", "CID");
            cb_DefeitoNovo.DisplayMember = "NomeTipo";
            cb_DefeitoNovo.ValueMember = "CID";

            #endregion;

            #region PopulandoComboBoxesUsu2
            // populando ComboBoxFamilia2
            cb_FamilaPeça2.Items.Clear();
            cb_FamilaPeça2.DataSource = Banco.ObterTodos("Familias", "*", "IdFamilia");
            cb_FamilaPeça2.DisplayMember = "Familia";
            cb_FamilaPeça2.ValueMember = "IdFamilia";

            //cb_Familia.SelectedIndex = 0;

            //Populando ComboBox NomeCIP2
            cb_NomeCIP2.Items.Clear();
            cb_NomeCIP2.DataSource = Banco.ObterTodos("Peças", "*", "CIP");
            cb_NomeCIP2.DisplayMember = "NomeCIP";
            cb_NomeCIP2.ValueMember = "CIP";

            //Populando ComboBox Clasificação2

            cb_Clasificação2.Items.Clear();
            cb_Clasificação2.DataSource = new BindingSource(TipoCadastro, null);
            cb_Clasificação2.DisplayMember = "Value";
            cb_Clasificação2.ValueMember = "key";

            //Populando ComboBox DefeitoGeral2

            cb_DefeitoGeral2.Items.Clear();
            cb_DefeitoGeral2.DataSource = Banco.ObterTodos("FamiliasDefeitos", "*", "ID_Familia");
            cb_DefeitoGeral2.DisplayMember = "T_Nome";
            cb_DefeitoGeral2.ValueMember = "ID_Familia";


            //Populando ComboBox TipoCamadaAlta2
            cb_Motivo2.Items.Clear();
            cb_Motivo2.DataSource = new BindingSource(Motivo, null);
            cb_Motivo2.DisplayMember = "Value";
            cb_Motivo2.ValueMember = "key";

            //Populando ComboBox DefeitoEspecifico2

            cb_DefeitoEspecifico2.Items.Clear();
            cb_DefeitoEspecifico2.DataSource = Banco.ObterTodos("TiposDefeitos", "*", "CID");
            cb_DefeitoEspecifico2.DisplayMember = "NomeTipo";
            cb_DefeitoEspecifico2.ValueMember = "CID";

            //Populando ComboBox DefeitoEspecifico2

            cb_DefeitoAnterior2.Items.Clear();
            cb_DefeitoAnterior2.DataSource = Banco.ObterTodos("TiposDefeitos", "*", "CID");
            cb_DefeitoAnterior2.DisplayMember = "NomeTipo";
            cb_DefeitoAnterior2.ValueMember = "CID";

            //Populando ComboBox DefeitoNovo2

            cb_DefeitoNovo2.Items.Clear();
            cb_DefeitoNovo2.DataSource = Banco.ObterTodos("TiposDefeitos", "*", "CID");
            cb_DefeitoNovo2.DisplayMember = "NomeTipo";
            cb_DefeitoNovo2.ValueMember = "CID";
            #endregion;

            #endregion;


            #region PrenchendoDataGripView
            //Preenchendo el DataGrip...
            dtA = new DataTable();            
            dtA.Columns.Add("CIR", typeof(string));
            dtA.Columns.Add("CIDE", typeof(string));
            dtA.Columns.Add("Nome_Peça", typeof(string));
            dtA.Columns.Add("CIP", typeof(string));
            dtA.Columns.Add("Clasificação", typeof(string));
            dtA.Columns.Add("CID", typeof(string));
            dtA.Columns.Add("Quantidade", typeof(int));
            dtA.Columns.Add("Resp_Apontamento", typeof(string));
            dtA.Columns.Add("Data", typeof(string));
            dtA.Columns.Add("Turno", typeof(string));
            dtA.Columns.Add("Resp_Setor", typeof(string));
            dgv_GestãoRetrabalho.ReadOnly = true;
            dgv_GestãoRetrabalho.AllowUserToDeleteRows = false;
            #endregion;


            #region ConfiguracionesGenerales
            int x = EstablecerTurno();
            cb_DefeitoGeral.Enabled = false;
            dgv_GestãoRetrabalho.DataSource = dtA;
            lb_NomeUsuario1.Text = F.lb_nome.Text;
            cb_Clasificação.SelectedIndex = Tipo;
            cb_Clasificação2.SelectedIndex = Tipo;
            tb_CódigoDefeito.Focus();
            cb_FamilaPeça2.SelectedIndex = 1;
            //Populando label Responsable por el Sector
            DataTable xD = Banco.ObterTodosOnde2Criterios("Usuarios", "Nivel", " 2 ", "Turno", "" + x);
            if (xD.Rows.Count >0)
            {
                lb_Resp_Setor.Text = xD.Rows[0].Field<string>("Nome");
            }
            else
            {
                lb_Resp_Setor.Text = "Desconhecido";
            }
           
            #endregion;

        }
        #endregion;


        #region Procedimientos
        private void ObterIndiceDosPontos(string texto)
        {
            string Buscado = ".";
            string c;

            int m = 0;

            for (int i = 0; i < texto.Length; i++)
            {
                c = texto.Substring(i, 1);
                if (c == Buscado)
                {

                    m1[m] = i;
                    m += 1;

                }
            }


        }
        private int ContarPontosUsi1()
        {
            int n = 0;
            char Buscado = '.';

            foreach (char c in tb_Codigo1.Text)
            {
                if (c == Buscado)
                {
                    n += 1;
                }
            }
            return n;
        }
        private int ContarPontosUsi2()
        {
            int n = 0;
            char Buscado = '.';

            foreach (char c in tb_Codigo2.Text)
            {
                if (c == Buscado)
                {
                    n += 1;
                }
            }
            return n;
        }
        private void AtualizarComboBoxes1()
        {
            ObterIndiceDosPontos(tb_Codigo1.Text);
            int n = ContarPontosUsi1();
            int Digitos;
            switch (n)
            {
                case 1:

                    cb_DefeitoGeral.SelectedIndex = int.Parse(tb_Codigo1.Text.Substring(0, m1[0]));

                    break;
                case 2:

                    cb_DefeitoEspecifico.SelectedIndex = int.Parse(tb_Codigo1.Text.Substring(m1[0] + 1, 1)) - 1;
                    cb_Motivo.SelectedValue = 1;
                    break;
                case 3:

                    cb_Motivo.SelectedIndex = int.Parse(tb_Codigo1.Text.Substring(m1[1] + 1, 1));
                    cb_DefeitoAnterior.SelectedValue = 1;
                    cb_DefeitoNovo.SelectedValue = 1;
                    break;
                case 4:
                    int x = cb_Motivo.SelectedIndex;
                    switch (x)
                    {
                        case 1:
                            Digitos = int.Parse(tb_Codigo1.Text.Substring(m1[2] + 1, m1[3] - m1[2] - 1));
                            dt = Banco.Procurar("TiposDefeitos", "CID, NomeTipo", "CID", "'" + Digitos + ".%'", "CID");
                            cb_DefeitoNovo.DataSource = dt;


                            break;
                        case 2:
                            cb_DefeitoNovo.SelectedValue = 1;
                            break;
                        case 3:
                            Digitos = int.Parse(tb_Codigo1.Text.Substring(m1[2] + 1, m1[3] - m1[2] - 1));
                            dt = Banco.Procurar("TiposDefeitos", "CID, NomeTipo", "CID", "'" + Digitos + ".%'", "CID");
                            cb_DefeitoNovo.DataSource = dt;
                            break;

                    }
                    break;
                case 5:
                    int t = cb_Motivo.SelectedIndex;
                    switch (t)
                    {
                        case 1:

                            cb_DefeitoAnterior.SelectedIndex = int.Parse(tb_Codigo1.Text.Substring(m1[3] + 1, 1))-1;
                            cb_DefeitoNovo.SelectedValue = 1;
                            break;
                        case 2:
                            cb_DefeitoNovo.SelectedValue = 1;
                            break;
                        case 3:
                            cb_DefeitoAnterior.SelectedIndex = int.Parse(tb_Codigo1.Text.Substring(m1[3] + 1, 1)) - 1;
                            cb_DefeitoNovo.SelectedValue = 1;
                            break;

                    }
                    break;
                case 6:

                    Digitos = int.Parse(tb_Codigo1.Text.Substring(m1[4] + 1, m1[5] - m1[4] - 1));
                    dt = Banco.Procurar("TiposDefeitos", "CID, NomeTipo", "CID", "'" + Digitos + ".%'", "CID");
                    cb_DefeitoNovo.DataSource = dt;
                    break;



            }
        }
        private void AtualizarComboBoxes2()
        {
            ObterIndiceDosPontos(tb_Codigo2.Text);
            int n = ContarPontosUsi2();
            int Digitos;
            switch (n)
            {
                case 1:

                    cb_DefeitoGeral2.SelectedIndex = int.Parse(tb_Codigo2.Text.Substring(0, m1[0]));

                    break;
                case 2:

                    cb_DefeitoEspecifico2.SelectedIndex = int.Parse(tb_Codigo2.Text.Substring(m1[0] + 1, 1)) - 1;
                    cb_Motivo2.SelectedValue = 1;
                    break;
                case 3:

                    cb_Motivo2.SelectedIndex = int.Parse(tb_Codigo2.Text.Substring(m1[1] + 1, 1));
                    cb_DefeitoAnterior2.SelectedValue = 1;
                    cb_DefeitoNovo2.SelectedValue = 1;
                    break;
                case 4:
                    int x = cb_Motivo2.SelectedIndex;
                    switch (x)
                    {
                        case 1:
                            Digitos = int.Parse(tb_Codigo2.Text.Substring(m1[2] + 1, m1[3] - m1[2] - 1));
                            dt = Banco.Procurar("TiposDefeitos", "CID, NomeTipo", "CID", "'" + Digitos + ".%'", "CID");
                            cb_DefeitoNovo2.DataSource = dt;


                            break;
                        case 2:
                            cb_DefeitoNovo2.SelectedValue = 1;
                            break;
                        case 3:
                            Digitos = int.Parse(tb_Codigo2.Text.Substring(m1[2] + 1, m1[3] - m1[2] - 1));
                            dt = Banco.Procurar("TiposDefeitos", "CID, NomeTipo", "CID", "'" + Digitos + ".%'", "CID");
                            cb_DefeitoNovo2.DataSource = dt;
                            break;

                    }
                    break;
                case 5:
                    int t = cb_Motivo2.SelectedIndex;
                    switch (t)
                    {
                        case 1:

                            cb_DefeitoAnterior2.SelectedIndex = int.Parse(tb_Codigo2.Text.Substring(m1[3] + 1, 1))-1;
                            cb_DefeitoNovo2.SelectedValue = 1;
                            break;
                        case 2:
                            cb_DefeitoNovo2.SelectedValue = 1;
                            break;
                        case 3:
                            cb_DefeitoAnterior2.SelectedIndex = int.Parse(tb_Codigo2.Text.Substring(m1[3] + 1, 1)) - 1;
                            cb_DefeitoNovo2.SelectedValue = 1;

                            break;

                    }
                    break;
                case 6:

                    Digitos = int.Parse(tb_Codigo2.Text.Substring(m1[4] + 1, m1[5] - m1[4] - 1));
                    dt = Banco.Procurar("TiposDefeitos", "CID, NomeTipo", "CID", "'" + Digitos + ".%'", "CID");
                    cb_DefeitoNovo2.DataSource = dt;
                    break;



            }
        }
        private void BorrarValoesTb_Codigo1()
        {
            ObterIndiceDosPontos(tb_Codigo1.Text);

            int n = ContarPontosUsi1();
            if (n > 1)
            {
                if (n == 1 || n == 2)
                {
                    tb_Codigo1.Text = "";
                    cb_DefeitoGeral.SelectedIndex = 0;
                }
                else
                {
                    if (n == 3)
                    {
                        tb_Codigo1.Text = tb_Codigo1.Text.Substring(0, (m1[n - 2] + 1));
                        AtualizarComboBoxes1();
                    }
                    else
                    {


                        if (n == 7)
                        {
                            tb_Codigo1.Text = tb_Codigo1.Text.Substring(0, (m1[n - 3] + 1));
                            AtualizarComboBoxes1();
                        }
                        else
                        {
                            if (n == 5)
                            {
                                tb_Codigo1.Text = tb_Codigo1.Text.Substring(0, (m1[n - 3] + 1));
                                AtualizarComboBoxes1();
                            }
                            else
                            {
                                tb_Codigo1.Text = tb_Codigo1.Text.Substring(0, (m1[n - 2] + 1));
                                AtualizarComboBoxes1();
                            }
                        }



                    }

                }
            }
            else
            {

                tb_Codigo1.Text = "";
                cb_DefeitoGeral.SelectedIndex = 0;


            }

        }
        private void ReestabelecerDGVdtA()
        {
            dtA = new DataTable();
            dtA.Columns.Add("CIR", typeof(string));
            dtA.Columns.Add("CIDE", typeof(string));
            dtA.Columns.Add("Nome_Peça", typeof(string));
            dtA.Columns.Add("CIP", typeof(string));
            dtA.Columns.Add("Clasificação", typeof(string));
            dtA.Columns.Add("CID", typeof(string));
            dtA.Columns.Add("Quantidade", typeof(int));
            dtA.Columns.Add("Resp_Apontamento", typeof(string));
            dtA.Columns.Add("Data", typeof(string));
            dtA.Columns.Add("Turno", typeof(string));
            dtA.Columns.Add("Resp_Setor", typeof(string));
            dgv_GestãoRetrabalho.ReadOnly = true;
            dgv_GestãoRetrabalho.AllowUserToDeleteRows = false;
        }
        private void BorrarValoesTb_Codigo2()
        {
            ObterIndiceDosPontos(tb_Codigo2.Text);

            int n = ContarPontosUsi2();
            if (n > 1)
            {
                if (n == 1 || n == 2)
                {
                    tb_Codigo2.Text = "";
                    cb_DefeitoGeral2.SelectedIndex = 0;
                }
                else
                {
                    if (n == 3)
                    {
                        tb_Codigo2.Text = tb_Codigo2.Text.Substring(0, (m1[n - 2] + 1));
                        AtualizarComboBoxes2();
                    }
                    else
                    {
                        if (n == 7)
                        {
                            tb_Codigo2.Text = tb_Codigo2.Text.Substring(0, (m1[n - 3] + 1));
                            AtualizarComboBoxes2();

                        }
                        else
                        {
                            if (n == 5)
                            {
                                tb_Codigo2.Text = tb_Codigo2.Text.Substring(0, (m1[n - 3] + 1));
                                AtualizarComboBoxes2();
                            }
                            else
                            {
                                tb_Codigo2.Text = tb_Codigo2.Text.Substring(0, (m1[n - 2] + 1));
                                AtualizarComboBoxes2();
                            }
                        }


                    }


                }
            }
            else
            {

                tb_Codigo2.Text = "";
                cb_DefeitoGeral2.SelectedIndex = 0;


            }

        }

        private bool AssignarValoresCodigoAComboboxUsuario1()
        {
            bool Consegui = true;
            int Digitos;
            string Codigo;
            Digitos = int.Parse(tb_CódigoDefeito.Text);
            if (Digitos <= cb_DefeitoGeral.Items.Count && Digitos >= 0)
            {
                NumeroDePuntos = ContarPontosUsi1() + 1;

                switch (NumeroDePuntos)
                {

                    case 1:
                        if (tb_CódigoDefeito.Text.Length > 0 && tb_CódigoDefeito.Text.Length < 3)
                        {
                           

                            if (Digitos <= Defeitos.Rows.Count && (Digitos.ToString().Length < 3 && Digitos.ToString().Length > 0) )
                            {
                                cb_DefeitoGeral.SelectedIndex = Digitos;

                                Consegui = true;

                                tb_CódigoDefeito.Text = "";

                            }
                            else
                            {
                                MessageBox.Show("Codigo de Defeito Geral 1 não encontrado na Base de Dados. \n Apague e tente novamente");
                                Consegui = false;
                            }


                        }

                        break;
                    case 2:
                        if (tb_CódigoDefeito.Text.Length == 1)
                        {
                            Codigo = tb_Codigo1.Text + tb_CódigoDefeito.Text;

                            dt = Banco.ObterTodosOnde("TiposDefeitos", "CID", "'" + Codigo + "'");
                            if (dt.Rows.Count > 0)
                            {
                                cb_DefeitoEspecifico.SelectedValue = Codigo;
                                tb_Codigo1.Text = Codigo + ".";
                                Consegui = true;
                                tb_CódigoDefeito.Text = "";
                            }
                            else
                            {
                                MessageBox.Show("Codigo de Defeito Especifico 1 não encontrado na Base de Dados. \n Apague e tente novamente");
                                Consegui = false;
                            }
                        }

                        break;

                    case 3:

                        if (cb_DefeitoGeral.SelectedIndex == 3 && tb_CódigoDefeito.Text.Length == 1)
                        {
                            

                            if ((Digitos > 0 && Digitos < 4) && (Digitos.ToString().Length == 1))
                            {
                                DigitoMotivo1 = Digitos;
                                cb_Motivo.SelectedIndex = Digitos;
                                if (DigitoMotivo1 == 2)
                                {
                                    tb_Codigo1.Text = tb_Codigo1.Text + tb_CódigoDefeito.Text + "." + "0.0.";
                                }
                                else
                                {
                                    tb_Codigo1.Text = tb_Codigo1.Text + tb_CódigoDefeito.Text + ".";
                                }

                                tb_CódigoDefeito.Text = "";
                                Consegui = true;
                            }
                            else
                            {
                                MessageBox.Show("Codigo de Motivo não encontrado na Base de Dados. \n Apague e tente novamente");
                                Consegui = false;
                            }
                        }

                        break;

                    case 4:
                        if (cb_DefeitoGeral.SelectedIndex == 3 && (tb_CódigoDefeito.Text.Length > 0 && tb_CódigoDefeito.Text.Length < 3) && cb_Motivo.SelectedIndex != 2)
                        {

                           



                            if (Digitos > 0 && Digitos < DefeitosEspecificos.Rows.Count)
                            {

                                Novo2 = Banco.Procurar("TiposDefeitos", "CID, NomeTipo", "CID", "'" + Digitos + "%'", "CID");
                                cb_DefeitoAnterior.DataSource = Novo2;

                                if (Novo2.Rows.Count == 1)// si hay uno solo lo pongo directo
                                {
                                    tb_Codigo1.Text = tb_Codigo1.Text + tb_CódigoDefeito.Text + ".1.0.0.";
                                    tb_CódigoDefeito.Text = "";

                                    Consegui = true;
                                }
                                else
                                {
                                    tb_Codigo1.Text = tb_Codigo1.Text + tb_CódigoDefeito.Text + ".";
                                    tb_CódigoDefeito.Text = "";

                                    Consegui = true;

                                }



                            }
                            else
                            {

                                MessageBox.Show("Codigo de defeito anteior invalido. \n Apague e tente novamente");
                                Consegui = false;
                            }
                        }
                        break;

                    case 5:
                        if (cb_DefeitoGeral.SelectedIndex == 3 && tb_CódigoDefeito.Text.Length == 1)
                        {


                            if (Digitos > 0 && Digitos <= Novo2.Rows.Count)
                            {
                                if (cb_Motivo.SelectedIndex != 1)
                                {
                                    cb_DefeitoAnterior.SelectedIndex = Digitos - 1;
                                    tb_Codigo1.Text = tb_Codigo1.Text + tb_CódigoDefeito.Text + ".";
                                    tb_CódigoDefeito.Text = "";
                                    Consegui = true;
                                }
                                else
                                {
                                    cb_DefeitoAnterior.SelectedIndex = Digitos - 1;
                                    tb_Codigo1.Text = tb_Codigo1.Text + tb_CódigoDefeito.Text + ".0.0.";
                                    tb_CódigoDefeito.Text = "";
                                    Consegui = true;

                                }
                            }
                            else
                            {

                                MessageBox.Show("Codigo de defeito anteior invalido. \n Apague e tente novamente");
                                Consegui = false;
                            }
                        }
                        break;
                    case 6:
                        if (DigitoMotivo1 != 1)
                        {
                            if (cb_DefeitoGeral.SelectedIndex == 3 && cb_DefeitoEspecifico.SelectedIndex == 0 && (tb_CódigoDefeito.Text.Length > 0 && tb_CódigoDefeito.Text.Length < 3))
                            {

                             

                                if (Digitos > 0 && Digitos < DefeitosEspecificos.Rows.Count)
                                {
                                    Dt_xD2 = Banco.Procurar("TiposDefeitos", "CID, NomeTipo", "CID", "'" + Digitos + ".%'", "CID");

                                    cb_DefeitoNovo.DataSource = Dt_xD2;
                                    if (Dt_xD2.Rows.Count == 1)
                                    {
                                        cb_DefeitoNovo.SelectedIndex = 0;
                                        tb_Codigo1.Text = tb_Codigo1.Text + tb_CódigoDefeito.Text + ".1.";
                                        tb_CódigoDefeito.Text = "";
                                        Consegui = true;
                                    }
                                    else
                                    {
                                        tb_Codigo1.Text = tb_Codigo1.Text + tb_CódigoDefeito.Text + ".";
                                        tb_CódigoDefeito.Text = "";
                                        Consegui = true;
                                    }

                                }
                                else
                                {
                                    MessageBox.Show("Codigo de Defeito Novo invalido. \n Apague e tente novamente");
                                    Consegui = false;
                                }

                            }
                        }


                        break;

                    case 7:

                        if (cb_DefeitoGeral.SelectedIndex == 3 && cb_DefeitoEspecifico.SelectedIndex == 0 && tb_CódigoDefeito.Text.Length == 1 && cb_Motivo.SelectedIndex != 1)
                        {

                          

                            if (Digitos > 0 && Digitos < DefeitosEspecificos.Rows.Count)
                            {
                                if (Dt_xD2.Rows.Count != 1)
                                {
                                    cb_DefeitoNovo.SelectedIndex = Digitos - 1;
                                    tb_Codigo1.Text = tb_Codigo1.Text + tb_CódigoDefeito.Text + ".";
                                    tb_CódigoDefeito.Text = "";
                                    Consegui = true;
                                }


                            }
                            else
                            {
                                MessageBox.Show("Codigo de Defeito Novo invalido. \n Apague e tente novamente");
                                Consegui = false;
                            }

                        }
                        break;

                }
            }
            else
            {
                MessageBox.Show("Codigo de Defeito Novo invalido. \n Apague e tente novamente");
                Consegui = false;
            }
            return Consegui;

        }

        private bool AssignarValoresCodigoAComboboxUsuario2()
        {
            bool Consegui = true;
            int Digitos;
            string Codigo;
           
            NumeroDePuntos2 = ContarPontosUsi2() + 1;

            switch (NumeroDePuntos2)
            {

                case 1:
                    if (tb_CódigoDefeito2.Text.Length > 0 && tb_CódigoDefeito2.Text.Length < 3)
                    {

                        Digitos = int.Parse(tb_CódigoDefeito2.Text);
                    
                        if (Digitos <= Defeitos.Rows.Count && (Digitos.ToString().Length < 3 && Digitos.ToString().Length > 0))
                        {
                            cb_DefeitoGeral2.SelectedIndex = Digitos;
                           
                            Consegui = true;

                            tb_CódigoDefeito2.Text = "";

                        }
                        else
                        {
                            MessageBox.Show("Codigo de Defeito Geral1 não encontrado na Base de Dados. \n Apague e tente novamente");
                            Consegui = false;
                        }


                    }

                    break;
                case 2:
                    if (tb_CódigoDefeito2.Text.Length == 1)
                    {
                        Codigo = tb_Codigo2.Text + tb_CódigoDefeito2.Text;

                        dt = Banco.ObterTodosOnde("TiposDefeitos", "CID", "'" + Codigo + "'");
                        if (dt.Rows.Count > 0)
                        {
                            cb_DefeitoEspecifico2.SelectedValue = Codigo;
                            tb_Codigo2.Text = Codigo + ".";
                            Consegui = true;
                           tb_CódigoDefeito2.Text = "";
                        }
                        else
                        {
                            MessageBox.Show("Codigo de Defeito Especifico1 não encontrado na Base de Dados. \n Apague e tente novamente");
                            Consegui = false;
                        }
                    }

                    break;

                case 3:

                    if (cb_DefeitoGeral2.SelectedIndex == 3 && cb_DefeitoEspecifico2.SelectedIndex == 0 && tb_CódigoDefeito2.Text.Length == 1)
                    {
                        Digitos = int.Parse(tb_CódigoDefeito2.Text);

                        if ((Digitos > 0 && Digitos < 4) && (Digitos.ToString().Length == 1))
                        {
                            DigitoMotivo2 = Digitos;
                            cb_Motivo2.SelectedIndex = Digitos;
                            if (DigitoMotivo2 == 2)
                            {
                                tb_Codigo2.Text = tb_Codigo2.Text + tb_CódigoDefeito2.Text + "." + "0.0.";
                            }
                            else
                            {
                                tb_Codigo2.Text = tb_Codigo2.Text + tb_CódigoDefeito2.Text + ".";
                            }

                            tb_CódigoDefeito2.Text = "";
                            Consegui = true;
                        }
                        else
                        {
                            MessageBox.Show("Codigo de Motivo1 não encontrado na Base de Dados. \n Apague e tente novamente");
                            Consegui = false;
                        }
                    }

                    break;

                case 4:
                    if (cb_DefeitoGeral2.SelectedIndex == 3 && cb_DefeitoEspecifico2.SelectedIndex == 0 && (tb_CódigoDefeito2.Text.Length > 0 && tb_CódigoDefeito2.Text.Length < 3))
                    {

                        Digitos = int.Parse(tb_CódigoDefeito2.Text);



                        if (Digitos > 0 && Digitos < DefeitosEspecificos.Rows.Count)
                        {
                            Novo = Banco.Procurar("TiposDefeitos", "CID, NomeTipo", "CID", "'" + Digitos + ".%'", "CID");
                            cb_DefeitoAnterior2.DataSource = Novo;                           
                            if (Novo.Rows.Count == 1)
                            {
                                tb_Codigo2.Text = tb_Codigo2.Text + tb_CódigoDefeito2.Text + ".1.0.0.";
                                tb_CódigoDefeito2.Text = "";
                                Consegui = true;
                            }
                            else
                            {
                                tb_Codigo2.Text = tb_Codigo2.Text + tb_CódigoDefeito2.Text + ".";
                                tb_CódigoDefeito2.Text = "";
                                Consegui = true;
                            }


                        }
                        else
                        {
                            if (Digitos == 0)
                            {
                                break;
                            }
                            MessageBox.Show("Codigo de defeito anteior1 invalido. \n Apague e tente novamente");
                            Consegui = false;
                        }
                    }
                    break;

                case 5:
                    if (cb_DefeitoGeral2.SelectedIndex == 3 && cb_DefeitoEspecifico2.SelectedIndex == 0 && tb_CódigoDefeito2.Text.Length == 1)
                    {

                        if (cb_DefeitoGeral2.SelectedIndex == 3 && tb_CódigoDefeito2.Text.Length == 1)
                        {

                            Digitos = int.Parse(tb_CódigoDefeito2.Text);                            
                            if (Digitos > 0 && Digitos <= Novo.Rows.Count)
                            {
                                if (cb_Motivo2.SelectedIndex != 1)
                                {
                                    cb_DefeitoAnterior2.SelectedIndex = Digitos - 1;
                                    tb_Codigo2.Text = tb_Codigo2.Text + Digitos + ".";
                                    tb_CódigoDefeito2.Text = "";
                                    Consegui = true;
                                }
                                else
                                {
                                    cb_DefeitoAnterior2.SelectedIndex = Digitos - 1;
                                    tb_Codigo2.Text = tb_Codigo2.Text + Digitos + ".0.0.";
                                    tb_CódigoDefeito2.Text = "";
                                    Consegui = true;

                                }
                            }
                            else
                            {

                                MessageBox.Show("Codigo de defeito anteior1 invalido. \n Apague e tente novamente");
                                Consegui = false;
                            }
                        }
                    }
                        break;
                case 6:
                    if (DigitoMotivo2 != 1)
                    {
                        if (cb_DefeitoGeral2.SelectedIndex == 3 && cb_DefeitoEspecifico2.SelectedIndex == 0 && (tb_CódigoDefeito2.Text.Length > 0 && tb_CódigoDefeito2.Text.Length < 3))
                        {

                            Digitos = int.Parse(tb_CódigoDefeito2.Text);



                            if (Digitos > 0 && Digitos < DefeitosEspecificos.Rows.Count)
                            {
                                Dt_xD = Banco.Procurar("TiposDefeitos", "CID, NomeTipo", "CID", "'" + Digitos + "%'", "CID");
                                cb_DefeitoNovo2.DataSource = Dt_xD;
                                if (Dt_xD.Rows.Count == 1)
                                {
                                    cb_DefeitoNovo2.SelectedIndex = 0;
                                    tb_Codigo2.Text = tb_Codigo2.Text + tb_CódigoDefeito2.Text + ".1.";
                                    tb_CódigoDefeito2.Text = "";
                                    Consegui = true;
                                }
                                else
                                {
                                    tb_Codigo2.Text = tb_Codigo2.Text + tb_CódigoDefeito2.Text + ".";
                                    tb_CódigoDefeito2.Text = "";
                                    Consegui = true;
                                }

                            }
                            else
                            {
                                MessageBox.Show("Codigo de Defeito Novo1 invalido. \n Apague e tente novamente");
                                Consegui = false;
                            }

                        }
                    }


                    break;

                case 7:

                    if (cb_DefeitoGeral2.SelectedIndex == 3 && cb_DefeitoEspecifico2.SelectedIndex == 0 && tb_CódigoDefeito2.Text.Length == 1)
                    {

                        Digitos = int.Parse(tb_CódigoDefeito2.Text);



                        if (Digitos > 0 && Digitos < DefeitosEspecificos.Rows.Count)
                        {
                            if (Dt_xD.Rows.Count != 1)
                            {
                                cb_DefeitoNovo2.SelectedIndex = Digitos - 1;
                                tb_Codigo2.Text = tb_Codigo2.Text + tb_CódigoDefeito2.Text + ".";
                                tb_CódigoDefeito2.Text = "";
                                Consegui = true;
                            }


                        }
                        else
                        {
                            MessageBox.Show("Codigo de Defeito Novo1 invalido. \n Apague e tente novamente");
                            Consegui = false;
                        }

                    }
                    break;

            }

            return Consegui;
        }

        public int EstablecerTurno()
        {
            
            DateTime fecha = DateTime.Now;
          
            DateTime ini1turno = new DateTime(2021, 01, 01, 05, 15, 00);
            DateTime Fim1turno = new DateTime(2021, 01, 01, 15, 10, 00);
            DateTime ini2turno = new DateTime(2021, 01, 01, 15, 10, 00);
            DateTime Fim2turno = new DateTime(2021, 01, 01, 00, 40, 00);
            
            int turno;
          
            if (fecha.Hour > ini1turno.Hour && fecha.Hour < Fim1turno.Hour)
            {
                turno = 1;
            }
            else
            {
                if (fecha.Hour >= ini2turno.Hour && fecha.Hour < 24)
                {
                    turno = 2;
                }
                else
                {
                    if (fecha.Hour == 0 && fecha.Minute < 40)
                    {
                        turno = 2;
                    }
                    else
                    {
                        turno = 3;
                    }
                      
                }
            }
            return turno;
        }
       
        #endregion;


        #region CambiosDeSeleccion
        #region CambiosEnLaSeleccion2
        
        private void cb_Motivo2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_Motivo2.SelectedIndex == 2 || cb_Motivo2.SelectedIndex == 3)
            {
                cb_DefeitoNovo2.Enabled = true;
                if (cb_Motivo2.SelectedIndex == 2)
                {
                    cb_DefeitoAnterior2.Enabled = false;
                    cb_DefeitoNovo2.Enabled = true;
                }
                else
                {
                    cb_DefeitoAnterior2.Enabled = true;
                    cb_DefeitoNovo2.Enabled = true;
                }
            }
            else
            {
                cb_DefeitoNovo2.Enabled = false;
                cb_DefeitoAnterior2.Enabled = true;
            }
        }

        private void cb_DefeitoGeral2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Usuario 2------------------------------------------
          
            if (cont2 > 3)
            {


                Dt_geral2 = Banco.Procurar("TiposDefeitos", "CID, NomeTipo", "NomeTipo", "'" + cb_DefeitoGeral2.Text + "%'", "CID");

                if (Dt_geral2.Rows.Count > 0)
                {


                    cb_DefeitoEspecifico2.DataSource = Dt_geral2;
                    if (Dt_geral2.Rows.Count == 1)
                    {
                        tb_Codigo2.Text = tb_CódigoDefeito2.Text + ".1.";

                        tb_CódigoDefeito2.Text = "";
                    }
                    else
                    {
                        if (tb_CódigoDefeito2.Text != "")
                        {
                            tb_Codigo2.Text = tb_CódigoDefeito2.Text + ".";
                        }

                    }

                }
                else
                {
                    MessageBox.Show("Não há nenhum defeito do tipo procurado");

                }
            }
            else
            {
                cont2 += 1;
            }
            




            if (cont2 > 3)
            {


                if (cb_DefeitoGeral2.Text == "")
                {
                    cb_DefeitoEspecifico2.SelectedValue = 1;
                    cb_DefeitoEspecifico2.Enabled = false;
                    cb_DefeitoEspecifico2.Text = "";
                    cb_Motivo2.Text = "";
                    cb_DefeitoAnterior2.Text = "";
                    cb_DefeitoNovo2.Text = "";

                }
                else
                {
                    cb_DefeitoEspecifico2.Enabled = true;
                }


            }
        }

        private void cb_FamilaPeça2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cont3 > 3)
            {
                DataTable dt;
                dt = Banco.Procurar("Peças", "CIP, NomeCIP", "NomeCIP", "'" + cb_FamilaPeça2.Text + "%'", "CIP");
                if (dt.Rows.Count > 0)
                {
                    cb_NomeCIP2.DataSource = dt;

                }
                else { MessageBox.Show("Não há nenhuma peça do tipo procurado"); }


                if (cb_FamilaPeça2.Text == "")
                {

                    cb_NomeCIP2.Text = "";
                    cb_NomeCIP2.Enabled = false;
                    cb_NomeCIP2.SelectedValue = 1;

                }
                else
                {
                    cb_NomeCIP2.Enabled = true;
                }

            }
            else
            {
                cont3 += 1;
            }
           
          

        }
        private void cb_DefeitoEspecifico2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_DefeitoGeral2.SelectedIndex == 3 && cb_DefeitoEspecifico2.Text == "Camada Alta por Retrabalho.")

            {
                cb_DefeitoAnterior2.Enabled = true;
                cb_Motivo2.Enabled = true;

            }
            else
            {
                cb_Motivo2.Text = "";
                cb_Motivo2.Enabled = false;
                cb_DefeitoAnterior2.Text = "";
                cb_DefeitoAnterior2.Enabled = false;
                cb_DefeitoNovo2.Text = "";
                cb_DefeitoNovo2.Enabled = false;
            }
        }

        #region BotonClasificação2SelectedIndexChanged
        private void cb_Clasificação2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_Clasificação2.SelectedIndex == 3)
            {
                cb_DefeitoGeral2.SelectedIndex = 0;
                tb_Codigo2.Text = "0.0.";
                cb_DefeitoGeral2.Enabled = false;
            }
            else
            {
                tb_Codigo2.Text = "";
                cb_DefeitoGeral2.Enabled = true;
            }



        }
        #endregion;


        #endregion;

        #region CambiosEnLaSelección1

        int cont2 = 0;
        int cont3 = 0;

        private void cb_Motivo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_Motivo.SelectedIndex == 2 || cb_Motivo.SelectedIndex == 3)
            {
                cb_DefeitoNovo.Enabled = true;
                if (cb_Motivo.SelectedIndex == 2)
                {
                    cb_DefeitoAnterior.SelectedValue = 1;
                    cb_DefeitoAnterior.Enabled = false;

                    cb_DefeitoNovo.Enabled = true;
                }
                else
                {
                    cb_DefeitoAnterior.Enabled = true;
                    cb_DefeitoNovo.Enabled = true;
                }
            }
            else
            {
                cb_DefeitoNovo.SelectedValue = 1;
                cb_DefeitoNovo.Enabled = false;

                cb_DefeitoAnterior.Enabled = true;
            }
        }

        private void cb_DefeitoGeral_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cont2 > 1)
            {



                Dt_geral1 = Banco.Procurar("TiposDefeitos", "CID, NomeTipo", "NomeTipo", "'" + cb_DefeitoGeral.Text + "%'", "CID");

                if (Dt_geral1.Rows.Count > 0)
                {


                    cb_DefeitoEspecifico.DataSource = Dt_geral1;
                    if (Dt_geral1.Rows.Count == 1)
                    {
                        tb_Codigo1.Text = tb_CódigoDefeito.Text + ".1.";

                        tb_CódigoDefeito.Text = "";
                    }
                    else
                    {
                        if (tb_CódigoDefeito.Text != "")
                        {
                            tb_Codigo1.Text = tb_CódigoDefeito.Text + ".";
                        }

                    }

                }
                else
                {
                    MessageBox.Show("Não há nenhum defeito do tipo procurado");

                }

            }
            else
            {
                cont2 += 1;
            }
            
            

            if (cont2 > 1)
            {



                if (cb_DefeitoGeral.Text == "")
                {
                    cb_DefeitoEspecifico.SelectedValue = 1;
                    cb_DefeitoEspecifico.Enabled = false;
                    cb_DefeitoEspecifico.Text = "";
                    cb_Motivo2.Text = "";
                    cb_DefeitoAnterior.Text = "";
                    cb_DefeitoNovo.Text = "";

                }
                else
                {
                    cb_DefeitoEspecifico.Enabled = true;
                }

            }
        }

        private void cb_FamilaPeça_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cont3 > 1)
            {
                DataTable dt;
                dt = Banco.Procurar("Peças", "CIP, NomeCIP", "NomeCIP", "'" + cb_FamilaPeça.Text + "%'", "CIP");
                if (dt.Rows.Count > 0)
                {

                    cb_NomeCIP.DataSource = dt;



                }
                else { MessageBox.Show("Não há nenhuma peça do tipo procurado"); }


                if (cb_FamilaPeça.Text == "")
                {

                    cb_NomeCIP.Text = "";
                    cb_NomeCIP.Enabled = false;
                    cb_NomeCIP.SelectedValue = 1;

                }
                else
                {
                    cb_NomeCIP.Enabled = true;
                }

            }
            else
            {
                cont3 += 1;
            }
        }

        private void cb_DefeitoEspecifico_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_DefeitoGeral.SelectedIndex == 3 && cb_DefeitoEspecifico.Text == "Camada Alta por Retrabalho.")

            {
                cb_DefeitoAnterior.Enabled = true;
                cb_Motivo.Enabled = true;



            }
            else
            {


                cb_Motivo.Text = "";
                cb_Motivo.Enabled = false;
                cb_DefeitoAnterior.Text = "";
                cb_DefeitoAnterior.Enabled = false;
                cb_DefeitoNovo.Text = "";
                cb_DefeitoNovo.Enabled = false;
            }
        }

        #region BotonClasificaçãoselectedIndexChanged
        private void cb_Clasificação_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cb_Clasificação.SelectedIndex == 3)
            {
                cb_DefeitoGeral.SelectedIndex = 0;
                tb_Codigo1.Text = "0.0.";
                cb_DefeitoGeral.Enabled = false;
            }
            else
            {   
                tb_Codigo1.Text = "";
                if (puedeActivar)
                {
                    cb_DefeitoGeral.Enabled = true;
                }
                else
                {
                    cb_DefeitoGeral.Enabled = false;
                }
                
            }



        }
        #endregion;

        #endregion;
        #endregion;


        #region BototonesApretadosEnlaForm
        bool control145 = false;
        private void F_GestãoEControleDeRetrablho_KeyDown(object sender, KeyEventArgs e)
        {
            #region TeclasFocarCadaTextbox
            if (e.KeyCode == Keys.NumLock)
            {
                tb_CódigoDefeito.Focus();
                tb_CódigoDefeito.Select(0, tb_CódigoDefeito.Text.Length);
            }

            if (e.KeyCode == Keys.Q)
            {
                tb_CódigoDefeito2.Focus();
                tb_CódigoDefeito2.Select(0, tb_CódigoDefeito2.Text.Length);
            }
            #endregion;

            #region Usurio1TeclasClasificacion

            if (e.KeyCode == Keys.Divide)//Retrabalho
            {
                tb_CódigoDefeito.Focus();

                tb_CódigoDefeito.Select(0, tb_CódigoDefeito.Text.Length);
                cb_Clasificação.SelectedIndex = 1;

            }
            if (e.KeyCode == Keys.Multiply)//Sucata
            {
                tb_CódigoDefeito.Focus();
                tb_CódigoDefeito.Select(0, tb_CódigoDefeito.Text.Length);
                cb_Clasificação.SelectedIndex = 2;
            }
            if (e.KeyValue==187)//Boas
            {
                tb_CódigoDefeito.Focus();
                tb_CódigoDefeito.Select(0, tb_CódigoDefeito.Text.Length);
                cb_Clasificação.SelectedIndex = 3;
            }

            #endregion;

            #region Usurio2TeclasClasificacion

            if (e.KeyCode == Keys.R)//Retrabalho
            {
                tb_CódigoDefeito2.Focus();

                tb_CódigoDefeito2.Select(0, tb_CódigoDefeito2.Text.Length);
                cb_Clasificação2.SelectedIndex = 1;

            }
            if (e.KeyCode == Keys.S)//Sucata
            {
                tb_CódigoDefeito2.Focus();
                tb_CódigoDefeito2.Select(0, tb_CódigoDefeito2.Text.Length);
                cb_Clasificação2.SelectedIndex = 2;
            }
            if (e.KeyCode == Keys.B)//Boas
            {
                tb_CódigoDefeito2.Focus();
                tb_CódigoDefeito2.Select(0, tb_CódigoDefeito2.Text.Length);
                cb_Clasificação2.SelectedIndex = 3;
            }

            #endregion;

            #region CambiarDePiezaUsuario1



            if (e.KeyCode == Keys.NumLock)
            {
                if (control145)
                {
                    control145 = false;
                }
                else
                {
                    control145 = true;
                }

            }

            if ((control145 && e.KeyCode == Keys.Left) || (control145 && e.KeyCode == Keys.Right))
            {
                cb_FamilaPeça.Focus();
            }

            if ((control145 && e.KeyCode == Keys.Up) || (control145 && e.KeyCode == Keys.Down))
            {
                cb_NomeCIP.Focus();
            }

            if ((control145 == false && e.KeyCode == Keys.Left) || (control145 == false && e.KeyCode == Keys.Right))
            {
                cb_FamilaPeça2.Focus();
            }

            if ((control145 == false && e.KeyCode == Keys.Up) || (control145 == false && e.KeyCode == Keys.Down))
            {
                cb_NomeCIP2.Focus();
            }
            #endregion;


        }

        private void F_GestãoEControleDeRetrablho_KeyPress(object sender, KeyPressEventArgs e)
        {
          

            if (e.KeyChar == '.' || e.KeyChar == ',' || e.KeyChar == '/' || e.KeyChar == '*' || e.KeyChar == '+' || e.KeyChar == '-' || e.KeyChar == 's' || e.KeyChar == 'r' || e.KeyChar == 'R' || e.KeyChar == 'S' || e.KeyChar == 'b' || e.KeyChar == 'B' || e.KeyChar == 'P' || e.KeyChar == 'p')
            {
                e.Handled = true;
            }


        }
       
        private void F_GestãoEControleDeRetrablho_KeyUp(object sender, KeyEventArgs e)
        {

        }

        #endregion;

        #region BotonesApretadosUsuario2
        bool control = true;
        int cont45;
        bool control1234 = false;
        bool control23;
        private void tb_CódigoDefeito2_KeyDown(object sender, KeyEventArgs e)
        {
           
            #region ConfigurandoElUsodelasTeclasUsuario2

            if ((e.KeyValue < 96 || e.KeyValue > 105) && e.KeyValue != 8 && e.KeyValue != 107 && e.KeyValue != 109 && e.KeyValue != 106 && e.KeyValue != 111 && e.KeyValue != 110 && e.KeyValue != 190)
            {
               
                EsUsuario1 = false;
                if ((e.KeyValue < 48 || e.KeyValue > 57) && e.KeyValue != 8 && e.KeyValue != 187 && e.KeyValue != 189 && e.KeyCode != Keys.P && e.KeyValue != 82 && e.KeyValue != 83 && e.KeyValue != 66)
                {

                    control = true;
                }
                else
                {
                    control = false;
                    if (e.KeyCode == Keys.P && tb_CódigoDefeito2.Text != "")
                    {
                       
                        AssignarValoresCodigoAComboboxUsuario2();
                    }
                    else
                    {
                        cont45 = 0;
                    }

                    if (e.KeyCode == Keys.Back && tb_CódigoDefeito2.Text != "")
                    {
                        tb_CódigoDefeito2.Text = tb_CódigoDefeito2.Text.Substring(0, tb_CódigoDefeito2.Text.Length - 1);
                    }

                    tb_CódigoDefeito2.Focus();

                    tb_CódigoDefeito2.Select(tb_CódigoDefeito2.Text.Length, 0);
                    EsUsuario1 = false;


                }

            }
            else
            {
                MessageBox.Show("Es usuario 1");
                EsUsuario1 = true;

            }
            #endregion;

            #region BotonesDeborradoUsuario2

            if (e.KeyCode == Keys.Delete)
            {
                if (tb_CódigoDefeito.Text.Length > 0)
                {
                    tb_CódigoDefeito.Focus();
                    tb_CódigoDefeito.Select(tb_CódigoDefeito.Text.Length - 1, 0);
                }
            }

            if (e.KeyCode == Keys.Back && tb_CódigoDefeito.Text == "" && tb_Codigo1.Text != "" && cb_Clasificação.SelectedIndex != 3)
            {
                BorrarValoesTb_Codigo1();
            }
            #endregion;
        }
        private void tb_CódigoDefeito2_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if (EsUsuario1 == false)
            {
                e.Handled = true;
            }

            #region CambiarAlUsurio1
            if (EsUsuario1 == false && cont45 == 0 && control == false)
            {
                MessageBox.Show("Entrou cambiar usurio para 1 = \n" + e.KeyChar.ToString());
               
                if (tb_CódigoDefeito2.Text.Length < 2)
                {
                    tb_CódigoDefeito2.Text = tb_CódigoDefeito2.Text + e.KeyChar.ToString();
                    tb_CódigoDefeito2.Focus();
                    tb_CódigoDefeito2.Select(tb_CódigoDefeito2.Text.Length, 0);

                   cont45 = 1;
                }
                else
                {
                    tb_CódigoDefeito2.Focus();
                    tb_CódigoDefeito2.Select(tb_CódigoDefeito2.Text.Length, 0);

                  cont45 = 1;
                }


            }
           
            #endregion;

            #region RestriccioneDeTeclasUsuario1

            if (ContarPontosUsi1() == 2 && cb_DefeitoGeral.SelectedIndex != 3)
            {
                e.Handled = true;
            }

            if (ContarPontosUsi1() == 0 && tb_CódigoDefeito.Text.Length > 2)
            {
                e.Handled = true;
            }
            else
            {
                if (ContarPontosUsi1() == 1 && tb_CódigoDefeito.Text.Length > 1)
                {
                    e.Handled = true;
                }
                else
                {
                    if (ContarPontosUsi1() == 2 && tb_CódigoDefeito.Text.Length > 1)
                    {
                        e.Handled = true;
                    }
                    else
                    {
                        if (ContarPontosUsi1() == 3 && tb_CódigoDefeito.Text.Length > 2)
                        {
                            e.Handled = true;
                        }
                        else
                        {
                            if (ContarPontosUsi1() == 4 && tb_CódigoDefeito.Text.Length > 1)
                            {
                                e.Handled = true;
                            }
                            else
                            {
                                if (ContarPontosUsi1() == 5 && tb_CódigoDefeito.Text.Length > 2)
                                {
                                    e.Handled = true;
                                }
                                else
                                {
                                    if (ContarPontosUsi1() == 6 && tb_CódigoDefeito.Text.Length > 1)
                                    {
                                        e.Handled = true;
                                    }
                                    else
                                    {
                                        if (ContarPontosUsi1() == 7 && tb_CódigoDefeito.Text.Length > 1)
                                        {
                                            e.Handled = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (ContarPontosUsi1() == 7)
                {
                    e.Handled = true;

                }
            }

            int n = ContarPontosUsi1();

            if ((e.KeyChar == '3' && n == 3) || (e.KeyChar == '3' && n == 5))
            {
                e.Handled = true;
            }

            #endregion;
        }
        private void tb_CódigoDefeito2_KeyUp(object sender, KeyEventArgs e)
        {
            #region FuncionBorrar
            if (control1234)
            {
                control23 = false;
                if ((tb_CódigoDefeito.Text.Length > 0 && tb_CódigoDefeito.Text.Length < 3) && e.KeyValue == 190 && EsUsuario1)
                {
                    if (AssignarValoresCodigoAComboboxUsuario1() == false)
                    {

                        debebloquereltextoUsu1 = true;
                        control1234 = false;
                    }
                }
                else
                {
                    debebloquereltextoUsu1 = false;
                }
            }
            else
            {
                control1234 = true;
            }

            #endregion;

            #region FuncionApontarEnelUltimoPonto

           
            int n = ContarPontosUsi1();
       
            #region ApontarCuandoDiferenteDeCamadaAlta
            if (e.KeyValue == 190 && cb_DefeitoGeral.SelectedIndex != 3 && n==2)
            {
                DateTime fecha = DateTime.Now;
                int comp = DateTime.Compare(fecha, Dtp_data.Value);

                if (comp ==0)
                {


                    if (tb_Codigo1.Text != "" && cb_DefeitoGeral.SelectedIndex != 3)
                    {
                        DataTable Defeito = Banco.ObterTodosOnde("TiposDefeitos", "CID", "'" + tb_Codigo1.Text.Substring(0, tb_Codigo1.Text.Length - 1) + "'");

                        if (Defeito.Rows.Count > 0)
                        {  //obter os dados
                            A.NomePeça = cb_NomeCIP.Text;
                            A.CIP = (string)cb_NomeCIP.SelectedValue;
                            A.CID = tb_Codigo1.Text.Substring(0, tb_Codigo1.Text.Length - 1);
                            A.Quantidade = 1;
                            A.Clasificação = (string)cb_Clasificação.SelectedValue;
                            A.Data = fecha.ToShortDateString();
                            A.Turno = EstablecerTurno();
                            A.Resp_Apontamento = lb_NomeUsuario2.Text;
                            A.Resp_Setor = lb_Resp_Setor.Text;                           
                            A.CIDE = "2-"+A.CIP + "-" + A.CID + "-" + A.Clasificação;
                            A.CIR = A.Turno + "-" + A.Data;
                           
                            //criar filas
                            DataRow row = dtA.NewRow();                           
                            row["CIR"] = A.CIR;
                            row["CIDE"] = A.CIDE;
                            row["Nome_Peça"] = A.NomePeça;
                            row["CIP"] = A.CIP;
                            row["Clasificação"] = A.Clasificação;
                            row["CID"] = A.CID;
                            row["Quantidade"] = A.Quantidade;
                            row["Data"] = A.Data;
                            row["Turno"] = A.Turno;
                            row["Resp_Apontamento"] = A.Resp_Apontamento;
                            row["Resp_Setor"] = A.Resp_Setor;
                            dtA.Rows.Add(row);

                            //para contar
                            ArrayList array = new ArrayList();

                            dt2 = new DataTable();                          
                            dt2.Columns.Add(new DataColumn("CIR", typeof(string)));
                            dt2.Columns.Add(new DataColumn("CIDE", typeof(string)));
                            dt2.Columns.Add(new DataColumn("Nome_Peça", typeof(string)));
                            dt2.Columns.Add(new DataColumn("CIP", typeof(string)));
                            dt2.Columns.Add(new DataColumn("Clasificação", typeof(string)));
                            dt2.Columns.Add(new DataColumn("CID", typeof(string)));
                            dt2.Columns.Add(new DataColumn("Quantidade", typeof(int)));
                            dt2.Columns.Add(new DataColumn("Data", typeof(string)));
                            dt2.Columns.Add(new DataColumn("Turno", typeof(string)));

                            dt2.Columns.Add(new DataColumn("Resp_Apontamento", typeof(string)));

                            dt2.Columns.Add(new DataColumn("Resp_Setor", typeof(string)));


                            foreach (DataRow dr in dtA.Rows)

                            {

                                object total;


                                if (array.IndexOf(dr["CIDE"]) < 0)

                                {


                                    total = dtA.Compute(String.Format("SUM(Quantidade)"), "CIDE = '" + dr["CIDE"] + "'");

                                    dt2.Rows.Add(new object[] {  dr["CIR"], dr["CIDE"], dr["Nome_Peça"], dr["CIP"], dr["Clasificação"], dr["CID"], Convert.ToInt32(total), dr["Data"], dr["Turno"], dr["Resp_Apontamento"], dr["Resp_Setor"] });

                                    array.Add(dr["CIDE"]);

                                }

                            }

                            DataView dv = dt2.DefaultView;
                            dv.Sort = "CIP";
                            sorteddt2 = dv.ToTable();
                            tb_CIR.Text = A.CIR;
                            dgv_GestãoRetrabalho.DataSource = sorteddt2;




                        }
                        else
                        {
                            MessageBox.Show("Defeito não existe na base de dados");
                        }
                    }
                }
                else
                {
                    if (comp>0)
                    {
                        if (tb_Codigo1.Text != "" && cb_DefeitoGeral.SelectedIndex != 3)
                        {
                            DataTable Defeito = Banco.ObterTodosOnde("TiposDefeitos", "CID", "'" + tb_Codigo1.Text.Substring(0, tb_Codigo1.Text.Length - 1) + "'");

                            if (Defeito.Rows.Count > 0)
                            {
                                A.NomePeça = cb_NomeCIP.Text;
                                A.CIP = (string)cb_NomeCIP.SelectedValue;
                                A.CID = tb_Codigo1.Text.Substring(0, tb_Codigo1.Text.Length - 1);

                                A.Quantidade = 1;
                                A.Clasificação = (string)cb_Clasificação.SelectedValue;
                                A.Data = Dtp_data.Value.ToShortDateString();
                                A.Turno = EstablecerTurno();
                                A.Resp_Apontamento = lb_NomeUsuario2.Text;
                                A.Resp_Setor = lb_Resp_Setor.Text;
                                A.CIDE = "2-" + A.CIP + "-" + A.CID + "-" + A.Clasificação;
                                A.CIR = A.Turno + "-" + A.Data;                                

                                DataRow row = dtA.NewRow();                               
                                row["CIR"] = A.CIR;
                                row["CIDE"] = A.CIDE;
                                row["Nome_Peça"] = A.NomePeça;
                                row["CIP"] = A.CIP;
                                row["Clasificação"] = A.Clasificação;
                                row["CID"] = A.CID;
                                row["Quantidade"] = A.Quantidade;
                                row["Data"] = A.Data;
                                row["Turno"] = A.Turno;
                                row["Resp_Apontamento"] = A.Resp_Apontamento;
                                row["Resp_Setor"] = A.Resp_Setor;
                                dtA.Rows.Add(row);


                                ArrayList array = new ArrayList();

                                dt2 = new DataTable();                               
                                dt2.Columns.Add(new DataColumn("CIR", typeof(string)));
                                dt2.Columns.Add(new DataColumn("CIDE", typeof(string)));
                                dt2.Columns.Add(new DataColumn("Nome_Peça", typeof(string)));
                                dt2.Columns.Add(new DataColumn("CIP", typeof(string)));
                                dt2.Columns.Add(new DataColumn("Clasificação", typeof(string)));
                                dt2.Columns.Add(new DataColumn("CID", typeof(string)));
                                dt2.Columns.Add(new DataColumn("Quantidade", typeof(int)));
                                dt2.Columns.Add(new DataColumn("Data", typeof(string)));
                                dt2.Columns.Add(new DataColumn("Turno", typeof(string)));

                                dt2.Columns.Add(new DataColumn("Resp_Apontamento", typeof(string)));

                                dt2.Columns.Add(new DataColumn("Resp_Setor", typeof(string)));


                                foreach (DataRow dr in dtA.Rows)

                                {

                                    object total;


                                    if (array.IndexOf(dr["CIDE"]) < 0)

                                    {


                                        total = dtA.Compute(String.Format("SUM(Quantidade)"), "CIDE = '" + dr["CIDE"] + "'");

                                        dt2.Rows.Add(new object[] { dr["CIR"], dr["CIDE"], dr["Nome_Peça"], dr["CIP"], dr["Clasificação"], dr["CID"], Convert.ToInt32(total), dr["Data"], dr["Turno"], dr["Resp_Apontamento"], dr["Resp_Setor"] });

                                        array.Add(dr["CIDE"]);

                                    }

                                }

                                DataView dv = dt2.DefaultView;
                                dv.Sort = "CIP";
                                sorteddt2 = dv.ToTable();
                                tb_CIR.Text = A.CIR;
                                dgv_GestãoRetrabalho.DataSource = sorteddt2;




                            }
                            else
                            {
                                MessageBox.Show("Defeito não existe na base de dados");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Não é possivel apontar dados com datas futuras.\n Revise a data e tente novamente.");
                    }
                }
                #endregion;

            }
            else
            {
                #region AponatrQuandoCamadaAlta
                DateTime fecha = DateTime.Now;
                int comp2 = DateTime.Compare(fecha, Dtp_data.Value);

                if (comp2 == 0)
                {
                    if (e.KeyValue == 190 && cb_DefeitoGeral.SelectedIndex == 3 && n == 7)
                    {
                        if (tb_Codigo1.Text != "" && cb_DefeitoGeral.SelectedIndex == 3 && (string)cb_DefeitoEspecifico.SelectedValue == "3.1")
                        {
                            DataTable Defeito = Banco.ObterTodosOnde("TiposDefeitos", "CID", "'" + tb_Codigo1.Text.Substring(0, 3) + "'");

                            if (Defeito.Rows.Count > 0)
                            {
                                A.NomePeça = cb_NomeCIP.Text;
                                A.CIP = (string)cb_NomeCIP.SelectedValue;
                                A.CID = tb_Codigo1.Text.Substring(0, tb_Codigo1.Text.Length - 1);
                                A.Quantidade = 1;
                                A.Clasificação = (string)cb_Clasificação.SelectedValue;
                                A.Data = fecha.ToShortDateString();
                                A.Turno = EstablecerTurno();
                                A.Resp_Apontamento = lb_NomeUsuario2.Text;
                                A.Resp_Setor = lb_Resp_Setor.Text;
                                A.CIDE = "2-" + A.CIP + "-" + A.CID + "-" + A.Clasificação;
                                A.CIR = A.Turno + "-" + A.Data;                               

                                DataRow row = dtA.NewRow();                               
                                row["CIR"] = A.CIR;
                                row["CIDE"] = A.CIDE;
                                row["Nome_Peça"] = A.NomePeça;
                                row["CIP"] = A.CIP;
                                row["Clasificação"] = A.Clasificação;
                                row["CID"] = A.CID;
                                row["Quantidade"] = A.Quantidade;
                                row["Data"] = A.Data;
                                row["Turno"] = A.Turno;
                                row["Resp_Apontamento"] = A.Resp_Apontamento;
                                row["Resp_Setor"] = A.Resp_Setor;
                                dtA.Rows.Add(row);


                                ArrayList array = new ArrayList();

                                dt2 = new DataTable();                               
                                dt2.Columns.Add(new DataColumn("CIR", typeof(string)));
                                dt2.Columns.Add(new DataColumn("CIDE", typeof(string)));
                                dt2.Columns.Add(new DataColumn("Nome_Peça", typeof(string)));
                                dt2.Columns.Add(new DataColumn("CIP", typeof(string)));
                                dt2.Columns.Add(new DataColumn("Clasificação", typeof(string)));
                                dt2.Columns.Add(new DataColumn("CID", typeof(string)));
                                dt2.Columns.Add(new DataColumn("Quantidade", typeof(int)));
                                dt2.Columns.Add(new DataColumn("Data", typeof(string)));
                                dt2.Columns.Add(new DataColumn("Turno", typeof(string)));
                                dt2.Columns.Add(new DataColumn("Resp_Apontamento", typeof(string)));

                                dt2.Columns.Add(new DataColumn("Resp_Setor", typeof(string)));


                                foreach (DataRow dr in dtA.Rows)

                                {

                                    object total;


                                    if (array.IndexOf(dr["CIDE"]) < 0)

                                    {


                                        total = dtA.Compute(String.Format("SUM(Quantidade)"), "CIDE = '" + dr["CIDE"] + "'");

                                        dt2.Rows.Add(new object[] {  dr["CIR"], dr["CIDE"], dr["Nome_Peça"], dr["CIP"], dr["Clasificação"], dr["CID"], Convert.ToInt32(total), dr["Data"], dr["Turno"], dr["Resp_Apontamento"], dr["Resp_Setor"] });

                                        array.Add(dr["CIDE"]);

                                    }

                                }

                                DataView dv = dt2.DefaultView;
                                dv.Sort = "CIP";
                                sorteddt2 = dv.ToTable();
                                tb_CIR.Text = A.CIR;
                                dgv_GestãoRetrabalho.DataSource = sorteddt2;




                            }
                            else
                            {
                                MessageBox.Show("Defeito não existe na base de dados");
                            }


                        }

                    }
                }
                else
                {
                    if (comp2>0)
                    {
                        if (e.KeyValue == 190 && cb_DefeitoGeral.SelectedIndex == 3 && n == 7)
                        {
                            if (tb_Codigo1.Text != "" && cb_DefeitoGeral.SelectedIndex == 3 && (string)cb_DefeitoEspecifico.SelectedValue == "3.1")
                            {
                                DataTable Defeito = Banco.ObterTodosOnde("TiposDefeitos", "CID", "'" + tb_Codigo1.Text.Substring(0, 3) + "'");

                                if (Defeito.Rows.Count > 0)
                                {
                                    A.NomePeça = cb_NomeCIP.Text;
                                    A.CIP = (string)cb_NomeCIP.SelectedValue;
                                    A.CID = tb_Codigo1.Text.Substring(0, tb_Codigo1.Text.Length - 1);
                                    A.Quantidade = 1;
                                    A.Clasificação = (string)cb_Clasificação.SelectedValue;
                                    A.Data = Dtp_data.Value.ToShortDateString();
                                    A.Turno = EstablecerTurno();
                                    A.Resp_Apontamento = lb_NomeUsuario2.Text;
                                    A.Resp_Setor = lb_Resp_Setor.Text;
                                    A.CIDE = "2-" + A.CIP + "-" + A.CID + "-" + A.Clasificação;
                                    A.CIR = A.Turno + "-" + A.Data;


                                    DataRow row = dtA.NewRow();
                                    row["CIR"] = A.CIR;
                                    row["CIDE"] = A.CIDE;
                                    row["Nome_Peça"] = A.NomePeça;
                                    row["CIP"] = A.CIP;
                                    row["Clasificação"] = A.Clasificação;
                                    row["CID"] = A.CID;
                                    row["Quantidade"] = A.Quantidade;
                                    row["Data"] = A.Data;
                                    row["Turno"] = A.Turno;
                                    row["Resp_Apontamento"] = A.Resp_Apontamento;
                                    row["Resp_Setor"] = A.Resp_Setor;
                                    dtA.Rows.Add(row);


                                    ArrayList array = new ArrayList();

                                    dt2 = new DataTable();
                                    dt2.Columns.Add(new DataColumn("CIR", typeof(string)));
                                    dt2.Columns.Add(new DataColumn("CIDE", typeof(string)));
                                    dt2.Columns.Add(new DataColumn("Nome_Peça", typeof(string)));
                                    dt2.Columns.Add(new DataColumn("CIP", typeof(string)));
                                    dt2.Columns.Add(new DataColumn("Clasificação", typeof(string)));
                                    dt2.Columns.Add(new DataColumn("CID", typeof(string)));
                                    dt2.Columns.Add(new DataColumn("Quantidade", typeof(int)));
                                    dt2.Columns.Add(new DataColumn("Data", typeof(string)));
                                    dt2.Columns.Add(new DataColumn("Turno", typeof(string)));

                                    dt2.Columns.Add(new DataColumn("Resp_Apontamento", typeof(string)));

                                    dt2.Columns.Add(new DataColumn("Resp_Setor", typeof(string)));


                                    foreach (DataRow dr in dtA.Rows)

                                    {

                                        object total;


                                        if (array.IndexOf(dr["CIDE"]) < 0)

                                        {


                                            total = dtA.Compute(String.Format("SUM(Quantidade)"), "CIDE = '" + dr["CIDE"] + "'");

                                            dt2.Rows.Add(new object[] { dr["CIR"], dr["CIDE"], dr["Nome_Peça"], dr["CIP"], dr["Clasificação"], dr["CID"], Convert.ToInt32(total), dr["Data"], dr["Turno"], dr["Resp_Apontamento"], dr["Resp_Setor"] });

                                            array.Add(dr["CIDE"]);

                                        }

                                    }

                                    DataView dv = dt2.DefaultView;
                                    dv.Sort = "CIP";
                                    sorteddt2 = dv.ToTable();
                                    tb_CIR.Text = A.CIR;
                                    dgv_GestãoRetrabalho.DataSource = sorteddt2;




                                }
                                else
                                {
                                    MessageBox.Show("Defeito não existe na base de dados");
                                }


                            }

                        }
                    }
                    else
                    {
                        MessageBox.Show("Não é posivel aponatar dados com datas futuras. Revise a data e tente novamente.");
                    }
                }
                #endregion;
            }

            #endregion;

        }

        #endregion;

        #region BotonesApretadosUsuario1
        bool control2 = false;
        int cont6;
        bool control231 = false;
        private void tb_CódigoDefeito_KeyDown(object sender, KeyEventArgs e)
        {
          // MessageBox.Show("KeyValue= "+e.KeyValue.ToString());
            #region FuncionesDeBotonesUsuario2
            if ((e.KeyValue < 48 || e.KeyValue > 57) && e.KeyValue != 8 && e.KeyValue != 187 && e.KeyValue != 189 && e.KeyValue != 32 && e.KeyValue != 82 && e.KeyValue != 83 && e.KeyValue != 66 && e.KeyCode != Keys.P)
            {
               
                EsUsuario2 = false;
                if ((e.KeyValue < 96 || e.KeyValue > 105) && e.KeyValue != 8 && e.KeyValue != 107 && e.KeyValue != 109 && e.KeyValue != 106 && e.KeyValue != 111 && e.KeyValue != 110 && e.KeyValue != 190)
                {
                    control2 = false;
                }
                else
                {
                    control2 = true;
                    if (e.KeyValue == 190 && tb_CódigoDefeito.Text != "")
                    {
                       // MessageBox.Show("KeyValue= " + e.KeyValue.ToString());
                        AssignarValoresCodigoAComboboxUsuario1();
                    }
                    else
                    {
                        if (lb_NomeUsuario2.Text!= "--")
                        {
                            cont6 = 0;//cambiar usurio aprovado ver em keypress
                          
                        }
                        else
                        {
                            cont6 = 1;
                        }
                    }


                    if (e.KeyCode == Keys.Back && tb_CódigoDefeito.Text != "")
                    {
                        tb_CódigoDefeito.Text = tb_CódigoDefeito.Text.Substring(0, tb_CódigoDefeito.Text.Length - 1);
                    }
                    tb_CódigoDefeito.Focus();
                    tb_CódigoDefeito.Select(tb_CódigoDefeito.Text.Length, 0);
                }


            }
            else
            {
                MessageBox.Show("Es usuario 2");
                EsUsuario2 = true;

            }
                              

            #region FucionBorrar
            if (control2)
            {
                if (e.KeyCode == Keys.Delete && tb_CódigoDefeito2.Text == "" && tb_Codigo2.Text != "" && cb_Clasificação2.SelectedIndex != 3)
                {


                    BorrarValoesTb_Codigo2();

                    control2 = false;

                }
            }
            else
            {
                control2 = true;
            }
            #endregion;


        }

        private void tb_CódigoDefeito_KeyPress(object sender, KeyPressEventArgs e)
        {
           
            if (EsUsuario2 == false)
            {
                e.Handled = true;
            }
            
            #region CambiarAlUsuario2
            if (EsUsuario2 == false && cont6==0 && control2 == true)
            {
                MessageBox.Show("Entrou cambiar usurio para 2 = \n" + e.KeyChar.ToString());

                if (tb_CódigoDefeito.Text.Length < 2)
                {
                    tb_CódigoDefeito.Text = tb_CódigoDefeito.Text + e.KeyChar.ToString();
                    tb_CódigoDefeito.Focus();
                    tb_CódigoDefeito.Select(tb_CódigoDefeito.Text.Length, 0);
                   // cont6 = 1;
                }
                else
                {
                    tb_CódigoDefeito.Focus();
                    tb_CódigoDefeito.Select(tb_CódigoDefeito.Text.Length, 0);
                   // cont6 = 1;
                }


            }
            #endregion;


            #region RestriccionesDetecladoUsuaro2

            if (ContarPontosUsi2() == 2 && cb_DefeitoGeral2.SelectedIndex != 3 && e.KeyChar != 8)
            {
                e.Handled = true;
            }


            if (ContarPontosUsi2() == 0 && tb_CódigoDefeito2.Text.Length > 2 && e.KeyChar != 8)
            {
                e.Handled = true;
            }
            else
            {
                if (ContarPontosUsi2() == 1 && tb_CódigoDefeito2.Text.Length > 1 && e.KeyChar != 8)
                {
                    e.Handled = true;
                }
                else
                {
                    if (ContarPontosUsi2() == 2 && tb_CódigoDefeito2.Text.Length > 1 && e.KeyChar != 8)
                    {
                        e.Handled = true;
                    }
                    else
                    {
                        if (ContarPontosUsi2() == 3 && tb_CódigoDefeito2.Text.Length > 2 && e.KeyChar != 8)
                        {
                            e.Handled = true;
                        }
                        else
                        {
                            if (ContarPontosUsi2() == 4 && tb_CódigoDefeito2.Text.Length > 1 && e.KeyChar != 8)
                            {
                                e.Handled = true;
                            }
                            else
                            {
                                if (ContarPontosUsi2() == 5 && tb_CódigoDefeito2.Text.Length > 2 && e.KeyChar != 8)
                                {
                                    e.Handled = true;
                                }
                                else
                                {
                                    if (ContarPontosUsi2() == 1 && tb_CódigoDefeito2.Text.Length > 1 && e.KeyChar != 8)
                                    {
                                        e.Handled = true;
                                    }
                                    else
                                    {
                                        if (ContarPontosUsi2() == 7 && tb_CódigoDefeito2.Text.Length > 1 && e.KeyChar != 8)
                                        {
                                            e.Handled = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (ContarPontosUsi2() == 7 && e.KeyChar != 8)
                {
                    e.Handled = true;
                }
            }
            #endregion;

            #region BloquearCamdaAltaComoDefeitoSecundario
            int n = ContarPontosUsi2();

            if ((e.KeyChar == '3' && n == 3) || (e.KeyChar == '3' && n == 5))
            {
                e.Handled = true;
            }
            #endregion;
        }

        private void tb_CódigoDefeito_KeyUp(object sender, KeyEventArgs e)
        {
            if (control231)
            {
                #region AssignarValoresComboBoxUsu2
                control231 = false;
                if (e.KeyCode == Keys.P && EsUsuario2)
                {

                    if (AssignarValoresCodigoAComboboxUsuario2() == false)
                    {

                        e.Handled = true;
                        control231 = false;
                    }

                }
                else
                {
                    //PuedeAssignarUsu2 = false;
                }

                #endregion;


                #region FuncionApontarEnelUltimoPonto


                int n = ContarPontosUsi2();

                #region ApontarCuandoDiferenteDeCamadaAlta
                if (e.KeyCode == Keys.P && cb_DefeitoGeral2.SelectedIndex != 3 && n == 2)
                {
                    DateTime fecha = DateTime.Now;
                    int comp = DateTime.Compare(fecha, Dtp_data.Value);

                    if (comp == 0)
                    {



                        if (tb_Codigo2.Text != "" && cb_DefeitoGeral2.SelectedIndex != 3)
                        {
                            DataTable Defeito = Banco.ObterTodosOnde("TiposDefeitos", "CID", "'" + tb_Codigo2.Text.Substring(0, tb_Codigo2.Text.Length - 1) + "'");

                            if (Defeito.Rows.Count > 0)
                            {
                                A.NomePeça = cb_NomeCIP2.Text;
                                A.CIP = (string)cb_NomeCIP2.SelectedValue;
                                A.CID = tb_Codigo2.Text.Substring(0, tb_Codigo2.Text.Length - 1);
                                A.Quantidade = 1;
                                A.Clasificação = (string)cb_Clasificação2.SelectedValue;
                                A.Data = fecha.ToShortDateString();
                                A.Turno = EstablecerTurno();
                                A.Resp_Apontamento = lb_NomeUsuario1.Text;
                                A.Resp_Setor = lb_Resp_Setor.Text;
                                A.CIDE = "1-" + A.CIP + "-" + A.CID + "-" + A.Clasificação;
                                A.CIR = A.Turno + "-" + A.Data;                               

                                DataRow row = dtA.NewRow();                               
                                row["CIR"] = A.CIR;
                                row["CIDE"] = A.CIDE;
                                row["Nome_Peça"] = A.NomePeça;
                                row["CIP"] = A.CIP;
                                row["Clasificação"] = A.Clasificação;
                                row["CID"] = A.CID;
                                row["Quantidade"] = A.Quantidade;
                                row["Data"] = A.Data;
                                row["Turno"] = A.Turno;
                                row["Resp_Apontamento"] = A.Resp_Apontamento;
                                row["Resp_Setor"] = A.Resp_Setor;
                                dtA.Rows.Add(row);


                                ArrayList array = new ArrayList();

                                dt2 = new DataTable();
                                dt2.Columns.Add(new DataColumn("CIR", typeof(string)));
                                dt2.Columns.Add(new DataColumn("CIDE", typeof(string)));
                                dt2.Columns.Add(new DataColumn("Nome_Peça", typeof(string)));
                                dt2.Columns.Add(new DataColumn("CIP", typeof(string)));
                                dt2.Columns.Add(new DataColumn("Clasificação", typeof(string)));
                                dt2.Columns.Add(new DataColumn("CID", typeof(string)));
                                dt2.Columns.Add(new DataColumn("Quantidade", typeof(int)));
                                dt2.Columns.Add(new DataColumn("Data", typeof(string)));
                                dt2.Columns.Add(new DataColumn("Turno", typeof(string)));

                                dt2.Columns.Add(new DataColumn("Resp_Apontamento", typeof(string)));

                                dt2.Columns.Add(new DataColumn("Resp_Setor", typeof(string)));


                                foreach (DataRow dr in dtA.Rows)

                                {

                                    object total;


                                    if (array.IndexOf(dr["CIDE"]) < 0)

                                    {


                                        total = dtA.Compute(String.Format("SUM(Quantidade)"), "CIDE = '" + dr["CIDE"] + "'");

                                        dt2.Rows.Add(new object[] {  dr["CIR"], dr["CIDE"], dr["Nome_Peça"], dr["CIP"], dr["Clasificação"], dr["CID"], Convert.ToInt32(total), dr["Data"], dr["Turno"], dr["Resp_Apontamento"], dr["Resp_Setor"] });

                                        array.Add(dr["CIDE"]);

                                    }

                                }

                                DataView dv = dt2.DefaultView;
                                dv.Sort = "CIP";
                                sorteddt2 = dv.ToTable();
                                tb_CIR.Text = A.CIR;
                                dgv_GestãoRetrabalho.DataSource = sorteddt2;




                            }
                            else
                            {
                                MessageBox.Show("Defeito não existe na base de dados");
                            }

                        }
                    }
                    else
                    {
                        if (comp>0)
                        {
                            if (tb_Codigo2.Text != "" && cb_DefeitoGeral2.SelectedIndex != 3)
                            {
                                DataTable Defeito = Banco.ObterTodosOnde("TiposDefeitos", "CID", "'" + tb_Codigo2.Text.Substring(0, tb_Codigo2.Text.Length - 1) + "'");

                                if (Defeito.Rows.Count > 0)
                                {
                                    A.NomePeça = cb_NomeCIP2.Text;
                                    A.CIP = (string)cb_NomeCIP2.SelectedValue;
                                    A.CID = tb_Codigo2.Text.Substring(0, tb_Codigo2.Text.Length - 1);

                                    A.Quantidade = 1;
                                    A.Clasificação = (string)cb_Clasificação2.SelectedValue;
                                    A.Data = Dtp_data.Value.ToShortDateString();
                                    A.Turno = EstablecerTurno();
                                    A.Resp_Apontamento = lb_NomeUsuario1.Text;
                                    A.Resp_Setor = lb_Resp_Setor.Text;
                                    A.CIDE = "1-" + A.CIP + "-" + A.CID + "-" + A.Clasificação;
                                    A.CIR = A.Turno + "-" + A.Data;                                   

                                    DataRow row = dtA.NewRow();                                   
                                    row["CIR"] = A.CIR;
                                    row["CIDE"] = A.CIDE;
                                    row["Nome_Peça"] = A.NomePeça;
                                    row["CIP"] = A.CIP;
                                    row["Clasificação"] = A.Clasificação;
                                    row["CID"] = A.CID;
                                    row["Quantidade"] = A.Quantidade;
                                    row["Data"] = A.Data;
                                    row["Turno"] = A.Turno;
                                    row["Resp_Apontamento"] = A.Resp_Apontamento;
                                    row["Resp_Setor"] = A.Resp_Setor;
                                    dtA.Rows.Add(row);


                                    ArrayList array = new ArrayList();

                                    dt2 = new DataTable();                                    
                                    dt2.Columns.Add(new DataColumn("CIR", typeof(string)));
                                    dt2.Columns.Add(new DataColumn("CIDE", typeof(string)));
                                    dt2.Columns.Add(new DataColumn("Nome_Peça", typeof(string)));
                                    dt2.Columns.Add(new DataColumn("CIP", typeof(string)));
                                    dt2.Columns.Add(new DataColumn("Clasificação", typeof(string)));
                                    dt2.Columns.Add(new DataColumn("CID", typeof(string)));
                                    dt2.Columns.Add(new DataColumn("Quantidade", typeof(int)));
                                    dt2.Columns.Add(new DataColumn("Data", typeof(string)));
                                    dt2.Columns.Add(new DataColumn("Turno", typeof(string)));

                                    dt2.Columns.Add(new DataColumn("Resp_Apontamento", typeof(string)));

                                    dt2.Columns.Add(new DataColumn("Resp_Setor", typeof(string)));


                                    foreach (DataRow dr in dtA.Rows)

                                    {

                                        object total;


                                        if (array.IndexOf(dr["CIDE"]) < 0)

                                        {


                                            total = dtA.Compute(String.Format("SUM(Quantidade)"), "CIDE = '" + dr["CIDE"] + "'");

                                            dt2.Rows.Add(new object[] {  dr["CIR"], dr["CIDE"], dr["Nome_Peça"], dr["CIP"], dr["Clasificação"], dr["CID"], Convert.ToInt32(total), dr["Data"], dr["Turno"], dr["Resp_Apontamento"], dr["Resp_Setor"] });

                                            array.Add(dr["CIDE"]);

                                        }

                                    }

                                    DataView dv = dt2.DefaultView;
                                    dv.Sort = "CIP";
                                    sorteddt2 = dv.ToTable();
                                    tb_CIR.Text = A.CIR;
                                    dgv_GestãoRetrabalho.DataSource = sorteddt2;




                                }
                                else
                                {
                                    MessageBox.Show("Defeito não existe na base de dados");
                                }

                            }
                        }
                        else
                        {
                            MessageBox.Show("Naõ é possivel apontar dados com datas futuras,  revise a data e tente novamente");
                        }
                    }
                }
                #endregion;
                
                else
               
                #region AponatrQuandoCamadaAlta
                {

                    DateTime fecha = DateTime.Now;
                    int comp2 = DateTime.Compare(fecha, Dtp_data.Value );
                    if (comp2 == 0)
                    {

                        if (e.KeyCode == Keys.P && cb_DefeitoGeral2.SelectedIndex == 3 && n == 7)
                        {
                            if (tb_Codigo2.Text != "" && cb_DefeitoGeral2.SelectedIndex == 3)
                            {
                                DataTable Defeito = Banco.ObterTodosOnde("TiposDefeitos", "CID", "'" + tb_Codigo2.Text.Substring(0, 3) + "'");

                                if (Defeito.Rows.Count > 0)
                                {
                                    A.NomePeça = cb_NomeCIP2.Text;
                                    A.CIP = (string)cb_NomeCIP2.SelectedValue;
                                    A.CID = tb_Codigo2.Text.Substring(0, tb_Codigo2.Text.Length - 1);
                                    A.Quantidade = 1;
                                    A.Clasificação = (string)cb_Clasificação2.SelectedValue;
                                    A.Data = fecha.ToShortDateString();
                                    A.Turno = EstablecerTurno();
                                    A.Resp_Apontamento = lb_NomeUsuario1.Text;
                                    A.Resp_Setor = lb_Resp_Setor.Text;
                                    A.CIDE = "1-" + A.CIP + "-" + A.CID + "-" + A.Clasificação;
                                    A.CIR = A.Turno + "-" + A.Data;                                   

                                    DataRow row = dtA.NewRow();                                   
                                    row["CIR"] = A.CIR;
                                    row["CIDE"] = A.CIDE;
                                    row["Nome_Peça"] = A.NomePeça;
                                    row["CIP"] = A.CIP;
                                    row["Clasificação"] = A.Clasificação;
                                    row["CID"] = A.CID;
                                    row["Quantidade"] = A.Quantidade;
                                    row["Data"] = A.Data;
                                    row["Turno"] = A.Turno;
                                    row["Resp_Apontamento"] = A.Resp_Apontamento;
                                    row["Resp_Setor"] = A.Resp_Setor;
                                    dtA.Rows.Add(row);


                                    ArrayList array = new ArrayList();

                                    dt2 = new DataTable();                                   
                                    dt2.Columns.Add(new DataColumn("CIR", typeof(string)));
                                    dt2.Columns.Add(new DataColumn("CIDE", typeof(string)));
                                    dt2.Columns.Add(new DataColumn("Nome_Peça", typeof(string)));
                                    dt2.Columns.Add(new DataColumn("CIP", typeof(string)));
                                    dt2.Columns.Add(new DataColumn("Clasificação", typeof(string)));
                                    dt2.Columns.Add(new DataColumn("CID", typeof(string)));
                                    dt2.Columns.Add(new DataColumn("Quantidade", typeof(int)));
                                    dt2.Columns.Add(new DataColumn("Data", typeof(string)));
                                    dt2.Columns.Add(new DataColumn("Turno", typeof(string)));

                                    dt2.Columns.Add(new DataColumn("Resp_Apontamento", typeof(string)));

                                    dt2.Columns.Add(new DataColumn("Resp_Setor", typeof(string)));


                                    foreach (DataRow dr in dtA.Rows)

                                    {

                                        object total;


                                        if (array.IndexOf(dr["CIDE"]) < 0)

                                        {


                                            total = dtA.Compute(String.Format("SUM(Quantidade)"), "CIDE = '" + dr["CIDE"] + "'");

                                            dt2.Rows.Add(new object[] {  dr["CIR"], dr["CIDE"], dr["Nome_Peça"], dr["CIP"], dr["Clasificação"], dr["CID"], Convert.ToInt32(total), dr["Data"], dr["Turno"], dr["Resp_Apontamento"], dr["Resp_Setor"] });

                                            array.Add(dr["CIDE"]);

                                        }

                                    }

                                    DataView dv = dt2.DefaultView;
                                    dv.Sort = "CIP";
                                    sorteddt2 = dv.ToTable();
                                    tb_CIR.Text = A.CIR;
                                    dgv_GestãoRetrabalho.DataSource = sorteddt2;




                                }
                                else
                                {
                                    MessageBox.Show("Defeito não existe na base de dados");
                                }


                            }

                        }

                    }
                    else
                    {
                        if (comp2 > 0)
                        {

                            if (e.KeyCode == Keys.P && cb_DefeitoGeral2.SelectedIndex == 3 && n == 7)
                            {
                                if (tb_Codigo2.Text != "" && cb_DefeitoGeral2.SelectedIndex == 3)
                                {
                                    DataTable Defeito = Banco.ObterTodosOnde("TiposDefeitos", "CID", "'" + tb_Codigo2.Text.Substring(0, 3) + "'");

                                    if (Defeito.Rows.Count > 0)
                                    {
                                        A.NomePeça = cb_NomeCIP2.Text;
                                        A.CIP = (string)cb_NomeCIP2.SelectedValue;
                                        A.CID = tb_Codigo2.Text.Substring(0, tb_Codigo2.Text.Length - 1);
                                        A.Quantidade = 1;
                                        A.Clasificação = (string)cb_Clasificação2.SelectedValue;
                                        A.Data = Dtp_data.Value.ToShortDateString();
                                        A.Turno = EstablecerTurno();
                                        A.Resp_Apontamento = lb_NomeUsuario1.Text;
                                        A.Resp_Setor = lb_Resp_Setor.Text;
                                        A.CIDE = "1-" + A.CIP + "-" + A.CID + "-" + A.Clasificação;
                                        A.CIR = A.Turno + "-" + A.Data;                                       

                                        DataRow row = dtA.NewRow();
                                        row["CIR"] = A.CIR;
                                        row["CIDE"] = A.CIDE;
                                        row["Nome_Peça"] = A.NomePeça;
                                        row["CIP"] = A.CIP;
                                        row["Clasificação"] = A.Clasificação;
                                        row["CID"] = A.CID;
                                        row["Quantidade"] = A.Quantidade;
                                        row["Data"] = A.Data;
                                        row["Turno"] = A.Turno;
                                        row["Resp_Apontamento"] = A.Resp_Apontamento;
                                        row["Resp_Setor"] = A.Resp_Setor;
                                        dtA.Rows.Add(row);


                                        ArrayList array = new ArrayList();

                                        dt2 = new DataTable();
                                        dt2.Columns.Add(new DataColumn("CIR", typeof(string)));
                                        dt2.Columns.Add(new DataColumn("CIDE", typeof(string)));
                                        dt2.Columns.Add(new DataColumn("Nome_Peça", typeof(string)));
                                        dt2.Columns.Add(new DataColumn("CIP", typeof(string)));
                                        dt2.Columns.Add(new DataColumn("Clasificação", typeof(string)));
                                        dt2.Columns.Add(new DataColumn("CID", typeof(string)));
                                        dt2.Columns.Add(new DataColumn("Quantidade", typeof(int)));
                                        dt2.Columns.Add(new DataColumn("Data", typeof(string)));
                                        dt2.Columns.Add(new DataColumn("Turno", typeof(string)));

                                        dt2.Columns.Add(new DataColumn("Resp_Apontamento", typeof(string)));

                                        dt2.Columns.Add(new DataColumn("Resp_Setor", typeof(string)));


                                        foreach (DataRow dr in dtA.Rows)

                                        {

                                            object total;


                                            if (array.IndexOf(dr["CIDE"]) < 0)

                                            {


                                                total = dtA.Compute(String.Format("SUM(Quantidade)"), "CIDE = '" + dr["CIDE"] + "'");

                                                dt2.Rows.Add(new object[] {  dr["CIR"], dr["CIDE"], dr["Nome_Peça"], dr["CIP"], dr["Clasificação"], dr["CID"], Convert.ToInt32(total), dr["Data"], dr["Turno"], dr["Resp_Apontamento"], dr["Resp_Setor"] });

                                                array.Add(dr["CIDE"]);

                                            }

                                        }

                                        DataView dv = dt2.DefaultView;
                                        dv.Sort = "CIP";
                                        sorteddt2 = dv.ToTable();
                                        tb_CIR.Text = A.CIR;
                                        dgv_GestãoRetrabalho.DataSource = sorteddt2;




                                    }
                                    else
                                    {
                                        MessageBox.Show("Defeito não existe na base de dados");
                                    }


                                }

                            }
                        }
                        else
                        {
                            MessageBox.Show("Não é possivel apontar dados com datas futuras. Revise a data e tente novamente");
                        }
                    }
                }
                #endregion;
                #endregion;
            }
            else
            {
                control231 = true;
            }
            
        }
        #endregion;

        #region Apontar

        #region BotonApontar1
        private void btn_Apontar_Click(object sender, EventArgs e)
        {
            #region QuandoSaoAsMesmasDatas
            DateTime fecha = DateTime.Now;

            int comparacion= DateTime.Compare(fecha, Dtp_data.Value);
            
            if (comparacion == 0)
            {

                if (tb_Codigo1.Text != "" && cb_DefeitoGeral.SelectedIndex != 3)
                {
                    DataTable Defeito = Banco.ObterTodosOnde("TiposDefeitos", "CID", "'" + tb_Codigo1.Text.Substring(0, tb_Codigo1.Text.Length - 1) + "'");

                    if (Defeito.Rows.Count > 0)
                    {
                        A.NomePeça = cb_NomeCIP.Text;
                        A.CIP = (string)cb_NomeCIP.SelectedValue;
                        A.CID = tb_Codigo1.Text.Substring(0, tb_Codigo1.Text.Length - 1);

                        A.Quantidade = 1;
                        A.Clasificação = (string)cb_Clasificação.SelectedValue;
                        A.Data = fecha.ToShortDateString();
                        A.Turno = EstablecerTurno();
                        A.Resp_Apontamento = lb_NomeUsuario2.Text;
                        A.Resp_Setor = lb_Resp_Setor.Text;
                        A.CIDE = A.CIP + "-" + A.CID + "-" + A.Clasificação;
                        A.CIR = A.Turno + "-" + A.Data;                      

                        DataRow row = dtA.NewRow();
                        row["CIR"] = A.CIR;
                        row["CIDE"] = A.CIDE;
                        row["Nome_Peça"] = A.NomePeça;
                        row["CIP"] = A.CIP;
                        row["Clasificação"] = A.Clasificação;
                        row["CID"] = A.CID;
                        row["Quantidade"] = A.Quantidade;
                        row["Data"] = A.Data;
                        row["Turno"] = A.Turno;
                        row["Resp_Apontamento"] = A.Resp_Apontamento;
                        row["Resp_Setor"] = A.Resp_Setor;
                        dtA.Rows.Add(row);


                        ArrayList array = new ArrayList();

                        dt2 = new DataTable();
                        dt2.Columns.Add(new DataColumn("CIR", typeof(string)));
                        dt2.Columns.Add(new DataColumn("CIDE", typeof(string)));
                        dt2.Columns.Add(new DataColumn("Nome_Peça", typeof(string)));
                        dt2.Columns.Add(new DataColumn("CIP", typeof(string)));
                        dt2.Columns.Add(new DataColumn("Clasificação", typeof(string)));
                        dt2.Columns.Add(new DataColumn("CID", typeof(string)));
                        dt2.Columns.Add(new DataColumn("Quantidade", typeof(int)));
                        dt2.Columns.Add(new DataColumn("Data", typeof(string)));
                        dt2.Columns.Add(new DataColumn("Turno", typeof(string)));

                        dt2.Columns.Add(new DataColumn("Resp_Apontamento", typeof(string)));

                        dt2.Columns.Add(new DataColumn("Resp_Setor", typeof(string)));


                        foreach (DataRow dr in dtA.Rows)

                        {

                            object total;


                            if (array.IndexOf(dr["CIDE"]) < 0)

                            {


                                total = dtA.Compute(String.Format("SUM(Quantidade)"), "CIDE = '" + dr["CIDE"] + "'");

                                dt2.Rows.Add(new object[] { dr["CIR"], dr["CIDE"], dr["Nome_Peça"], dr["CIP"], dr["Clasificação"], dr["CID"], Convert.ToInt32(total), dr["Data"], dr["Turno"], dr["Resp_Apontamento"], dr["Resp_Setor"] });

                                array.Add(dr["CIDE"]);

                            }

                        }

                        DataView dv = dt2.DefaultView;
                        dv.Sort = "CIP";
                        sorteddt2 = dv.ToTable();
                        tb_CIR.Text = A.CIR;
                        dgv_GestãoRetrabalho.DataSource = sorteddt2;




                    }
                    else
                    {
                        MessageBox.Show("Defeito não existe na base de dados");
                    }
                }
                else
                {
                    if (tb_Codigo1.Text != "" && cb_DefeitoGeral.SelectedIndex == 3 && (string)cb_DefeitoEspecifico.SelectedValue == "3.1")
                    {
                        DataTable Defeito = Banco.ObterTodosOnde("TiposDefeitos", "CID", "'" + tb_Codigo1.Text.Substring(0, 3) + "'");

                        if (Defeito.Rows.Count > 0)
                        {
                            A.NomePeça = cb_NomeCIP.Text;
                            A.CIP = (string)cb_NomeCIP.SelectedValue;
                            A.CID = tb_Codigo1.Text.Substring(0, tb_Codigo1.Text.Length - 1);
                            A.Quantidade = 1;
                            A.Clasificação = (string)cb_Clasificação.SelectedValue;
                            A.Data = fecha.ToShortDateString();
                            A.Turno = EstablecerTurno();
                            A.Resp_Apontamento = lb_NomeUsuario2.Text;
                            A.Resp_Setor = lb_Resp_Setor.Text;
                            A.CIDE = "2-" + A.CIP + "-" + A.CID + "-" + A.Clasificação;
                            A.CIR = A.Turno + "-" + A.Data;                           

                            DataRow row = dtA.NewRow();
                            row["CIR"] = A.CIR;
                            row["CIDE"] = A.CIDE;
                            row["Nome_Peça"] = A.NomePeça;
                            row["CIP"] = A.CIP;
                            row["Clasificação"] = A.Clasificação;
                            row["CID"] = A.CID;
                            row["Quantidade"] = A.Quantidade;
                            row["Data"] = A.Data;
                            row["Turno"] = A.Turno;
                            row["Resp_Apontamento"] = A.Resp_Apontamento;
                            row["Resp_Setor"] = A.Resp_Setor;
                            dtA.Rows.Add(row);


                            ArrayList array = new ArrayList();

                            dt2 = new DataTable();
                            dt2.Columns.Add(new DataColumn("CIR", typeof(string)));
                            dt2.Columns.Add(new DataColumn("CIDE", typeof(string)));
                            dt2.Columns.Add(new DataColumn("Nome_Peça", typeof(string)));
                            dt2.Columns.Add(new DataColumn("CIP", typeof(string)));
                            dt2.Columns.Add(new DataColumn("Clasificação", typeof(string)));
                            dt2.Columns.Add(new DataColumn("CID", typeof(string)));
                            dt2.Columns.Add(new DataColumn("Quantidade", typeof(int)));
                            dt2.Columns.Add(new DataColumn("Data", typeof(string)));
                            dt2.Columns.Add(new DataColumn("Turno", typeof(string)));

                            dt2.Columns.Add(new DataColumn("Resp_Apontamento", typeof(string)));

                            dt2.Columns.Add(new DataColumn("Resp_Setor", typeof(string)));


                            foreach (DataRow dr in dtA.Rows)

                            {

                                object total;


                                if (array.IndexOf(dr["CIDE"]) < 0)

                                {


                                    total = dtA.Compute(String.Format("SUM(Quantidade)"), "CIDE = '" + dr["CIDE"] + "'");

                                    dt2.Rows.Add(new object[] {  dr["CIR"], dr["CIDE"], dr["Nome_Peça"], dr["CIP"], dr["Clasificação"], dr["CID"], Convert.ToInt32(total), dr["Data"], dr["Turno"], dr["Resp_Apontamento"], dr["Resp_Setor"] });

                                    array.Add(dr["CIDE"]);

                                }

                            }

                            DataView dv = dt2.DefaultView;
                            dv.Sort = "CIP";
                            sorteddt2 = dv.ToTable();
                            tb_CIR.Text = A.CIR;
                            dgv_GestãoRetrabalho.DataSource = sorteddt2;




                        }
                        else
                        {
                            MessageBox.Show("Defeito não existe na base de dados");
                        }


                    }


                }
                
            }
            #endregion;

            else
            {
                #region QuandoAsdatasSaodoPasado
                if (comparacion>0)
                {

                    if (tb_Codigo1.Text != "" && cb_DefeitoGeral.SelectedIndex != 3)
                    {
                        DataTable Defeito = Banco.ObterTodosOnde("TiposDefeitos", "CID", "'" + tb_Codigo1.Text.Substring(0, tb_Codigo1.Text.Length - 1) + "'");

                        if (Defeito.Rows.Count > 0)
                        {
                            A.NomePeça = cb_NomeCIP.Text;
                            A.CIP = (string)cb_NomeCIP.SelectedValue;
                            A.CID = tb_Codigo1.Text.Substring(0, tb_Codigo1.Text.Length - 1);

                            A.Quantidade = 1;
                            A.Clasificação = (string)cb_Clasificação.SelectedValue;
                            A.Data = Dtp_data.Value.ToShortDateString();
                            A.Turno = EstablecerTurno();
                            A.Resp_Apontamento = lb_NomeUsuario2.Text;
                            A.Resp_Setor = lb_Resp_Setor.Text;
                            A.CIDE = "2-"+A.CIP + "-" + A.CID + "-" + A.Clasificação;
                            A.CIR = A.Turno + "-" + A.Data;                           

                            DataRow row = dtA.NewRow();
                            row["CIR"] = A.CIR;
                            row["CIDE"] = A.CIDE;
                            row["Nome_Peça"] = A.NomePeça;
                            row["CIP"] = A.CIP;
                            row["Clasificação"] = A.Clasificação;
                            row["CID"] = A.CID;
                            row["Quantidade"] = A.Quantidade;
                            row["Data"] = A.Data;
                            row["Turno"] = A.Turno;
                            row["Resp_Apontamento"] = A.Resp_Apontamento;
                            row["Resp_Setor"] = A.Resp_Setor;
                            dtA.Rows.Add(row);


                            ArrayList array = new ArrayList();

                            dt2 = new DataTable();
                            dt2.Columns.Add(new DataColumn("CIR", typeof(string)));
                            dt2.Columns.Add(new DataColumn("CIDE", typeof(string)));
                            dt2.Columns.Add(new DataColumn("Nome_Peça", typeof(string)));
                            dt2.Columns.Add(new DataColumn("CIP", typeof(string)));
                            dt2.Columns.Add(new DataColumn("Clasificação", typeof(string)));
                            dt2.Columns.Add(new DataColumn("CID", typeof(string)));
                            dt2.Columns.Add(new DataColumn("Quantidade", typeof(int)));
                            dt2.Columns.Add(new DataColumn("Data", typeof(string)));
                            dt2.Columns.Add(new DataColumn("Turno", typeof(string)));

                            dt2.Columns.Add(new DataColumn("Resp_Apontamento", typeof(string)));

                            dt2.Columns.Add(new DataColumn("Resp_Setor", typeof(string)));


                            foreach (DataRow dr in dtA.Rows)

                            {

                                object total;


                                if (array.IndexOf(dr["CIDE"]) < 0)

                                {


                                    total = dtA.Compute(String.Format("SUM(Quantidade)"), "CIDE = '" + dr["CIDE"] + "'");

                                    dt2.Rows.Add(new object[] {  dr["CIR"], dr["CIDE"], dr["Nome_Peça"], dr["CIP"], dr["Clasificação"], dr["CID"], Convert.ToInt32(total), dr["Data"], dr["Turno"], dr["Resp_Apontamento"], dr["Resp_Setor"] });

                                    array.Add(dr["CIDE"]);

                                }

                            }

                            DataView dv = dt2.DefaultView;
                            dv.Sort = "CIP";
                            sorteddt2 = dv.ToTable();
                            tb_CIR.Text = A.CIR;
                            dgv_GestãoRetrabalho.DataSource = sorteddt2;




                        }
                        else
                        {
                            MessageBox.Show("Defeito não existe na base de dados");
                        }
                    }
                    else
                    {
                        if (tb_Codigo1.Text != "" && cb_DefeitoGeral.SelectedIndex == 3 && (string)cb_DefeitoEspecifico.SelectedValue == "3.1")
                        {
                            DataTable Defeito = Banco.ObterTodosOnde("TiposDefeitos", "CID", "'" + tb_Codigo1.Text.Substring(0, 3) + "'");

                            if (Defeito.Rows.Count > 0)
                            {
                                A.NomePeça = cb_NomeCIP.Text;
                                A.CIP = (string)cb_NomeCIP.SelectedValue;
                                A.CID = tb_Codigo1.Text.Substring(0, tb_Codigo1.Text.Length - 1);
                                A.Quantidade = 1;
                                A.Clasificação = (string)cb_Clasificação.SelectedValue;
                                A.Data = Dtp_data.Value.ToShortDateString();
                                A.Turno = EstablecerTurno();
                                A.Resp_Apontamento = lb_NomeUsuario2.Text;
                                A.Resp_Setor = lb_Resp_Setor.Text;
                                A.CIDE = "2-" + A.CIP + "-" + A.CID + "-" + A.Clasificação;
                                A.CIR = A.Turno + "-" + A.Data;


                                DataRow row = dtA.NewRow();
                                row["CIR"] = A.CIR;
                                row["CIDE"] = A.CIDE;
                                row["Nome_Peça"] = A.NomePeça;
                                row["CIP"] = A.CIP;
                                row["Clasificação"] = A.Clasificação;
                                row["CID"] = A.CID;
                                row["Quantidade"] = A.Quantidade;
                                row["Data"] = A.Data;
                                row["Turno"] = A.Turno;
                                row["Resp_Apontamento"] = A.Resp_Apontamento;
                                row["Resp_Setor"] = A.Resp_Setor;
                                dtA.Rows.Add(row);


                                ArrayList array = new ArrayList();

                                dt2 = new DataTable();
                                dt2.Columns.Add(new DataColumn("CIR", typeof(string)));
                                dt2.Columns.Add(new DataColumn("CIDE", typeof(string)));
                                dt2.Columns.Add(new DataColumn("Nome_Peça", typeof(string)));
                                dt2.Columns.Add(new DataColumn("CIP", typeof(string)));
                                dt2.Columns.Add(new DataColumn("Clasificação", typeof(string)));
                                dt2.Columns.Add(new DataColumn("CID", typeof(string)));
                                dt2.Columns.Add(new DataColumn("Quantidade", typeof(int)));
                                dt2.Columns.Add(new DataColumn("Data", typeof(string)));
                                dt2.Columns.Add(new DataColumn("Turno", typeof(string)));

                                dt2.Columns.Add(new DataColumn("Resp_Apontamento", typeof(string)));

                                dt2.Columns.Add(new DataColumn("Resp_Setor", typeof(string)));


                                foreach (DataRow dr in dtA.Rows)

                                {

                                    object total;


                                    if (array.IndexOf(dr["CIDE"]) < 0)

                                    {


                                        total = dtA.Compute(String.Format("SUM(Quantidade)"), "CIDE = '" + dr["CIDE"] + "'");

                                        dt2.Rows.Add(new object[] { dr["CIR"], dr["CIDE"], dr["Nome_Peça"], dr["CIP"], dr["Clasificação"], dr["CID"], Convert.ToInt32(total), dr["Data"], dr["Turno"], dr["Resp_Apontamento"], dr["Resp_Setor"] });

                                        array.Add(dr["CIDE"]);

                                    }

                                }

                                DataView dv = dt2.DefaultView;
                                dv.Sort = "CIP";
                                sorteddt2 = dv.ToTable();
                                tb_CIR.Text = A.CIR;
                                dgv_GestãoRetrabalho.DataSource = sorteddt2;




                            }
                            else
                            {
                                MessageBox.Show("Defeito não existe na base de dados");
                            }


                        }


                    }
                }
                #endregion;
                
                else
                {
                    MessageBox.Show("Não e posivel apontar dados con data futura. Revise a data e tente novamente");
                }
            }
        }

        #endregion;

        #region BotonApontar2
        private void btn_Apontar2_Click(object sender, EventArgs e)
        {
            DateTime fecha = DateTime.Now;

            int comp = DateTime.Compare(fecha, Dtp_data.Value);

            if (comp == 0)
            {


                if (tb_Codigo2.Text != "" && cb_DefeitoGeral2.SelectedIndex != 3)
                {
                    DataTable Defeito = Banco.ObterTodosOnde("TiposDefeitos", "CID", "'" + tb_Codigo2.Text.Substring(0, tb_Codigo2.Text.Length - 1) + "'");

                    if (Defeito.Rows.Count > 0)
                    {
                        A.NomePeça = cb_NomeCIP2.Text;
                        A.CIP = (string)cb_NomeCIP2.SelectedValue;
                        A.CID = tb_Codigo2.Text.Substring(0, tb_Codigo2.Text.Length - 1);

                        A.Quantidade = 1;
                        A.Clasificação = (string)cb_Clasificação2.SelectedValue;
                        A.Data = fecha.ToShortDateString();
                        A.Turno = EstablecerTurno();
                        A.Resp_Apontamento = lb_NomeUsuario1.Text;
                        A.Resp_Setor = lb_Resp_Setor.Text;
                        A.CIDE = "1-" + A.CIP + "-" + A.CID + "-" + A.Clasificação;
                        A.CIR = A.Turno + "-" + A.Data;                      

                        DataRow row = dtA.NewRow();
                        row["CIR"] = A.CIR;
                        row["CIDE"] = A.CIDE;
                        row["Nome_Peça"] = A.NomePeça;
                        row["CIP"] = A.CIP;
                        row["Clasificação"] = A.Clasificação;
                        row["CID"] = A.CID;
                        row["Quantidade"] = A.Quantidade;
                        row["Data"] = A.Data;
                        row["Turno"] = A.Turno;
                        row["Resp_Apontamento"] = A.Resp_Apontamento;
                        row["Resp_Setor"] = A.Resp_Setor;
                        dtA.Rows.Add(row);


                        ArrayList array = new ArrayList();

                        dt2 = new DataTable();                        
                        dt2.Columns.Add(new DataColumn("CIR", typeof(string)));
                        dt2.Columns.Add(new DataColumn("CIDE", typeof(string)));
                        dt2.Columns.Add(new DataColumn("Nome_Peça", typeof(string)));
                        dt2.Columns.Add(new DataColumn("CIP", typeof(string)));
                        dt2.Columns.Add(new DataColumn("Clasificação", typeof(string)));
                        dt2.Columns.Add(new DataColumn("CID", typeof(string)));
                        dt2.Columns.Add(new DataColumn("Quantidade", typeof(int)));
                        dt2.Columns.Add(new DataColumn("Data", typeof(string)));
                        dt2.Columns.Add(new DataColumn("Turno", typeof(string)));

                        dt2.Columns.Add(new DataColumn("Resp_Apontamento", typeof(string)));

                        dt2.Columns.Add(new DataColumn("Resp_Setor", typeof(string)));


                        foreach (DataRow dr in dtA.Rows)

                        {

                            object total;


                            if (array.IndexOf(dr["CIDE"]) < 0)

                            {


                                total = dtA.Compute(String.Format("SUM(Quantidade)"), "CIDE = '" + dr["CIDE"] + "'");

                                dt2.Rows.Add(new object[] { dr["CIR"], dr["CIDE"], dr["Nome_Peça"], dr["CIP"], dr["Clasificação"], dr["CID"], Convert.ToInt32(total), dr["Data"], dr["Turno"], dr["Resp_Apontamento"], dr["Resp_Setor"] });

                                array.Add(dr["CIDE"]);

                            }

                        }

                        DataView dv = dt2.DefaultView;
                        dv.Sort = "CIP";
                        sorteddt2 = dv.ToTable();
                        tb_CIR.Text = A.CIR;
                        dgv_GestãoRetrabalho.DataSource = sorteddt2;




                    }
                    else
                    {
                        MessageBox.Show("Defeito não existe na base de dados");
                    }
                }
                else
                {
                    if (tb_Codigo2.Text != "" && cb_DefeitoGeral2.SelectedIndex == 3 && (string)cb_DefeitoEspecifico2.SelectedValue == "3.1")
                    {
                        DataTable Defeito = Banco.ObterTodosOnde("TiposDefeitos", "CID", "'" + tb_Codigo2.Text.Substring(0, 3) + "'");

                        if (Defeito.Rows.Count > 0)
                        {
                            A.NomePeça = cb_NomeCIP2.Text;
                            A.CIR = A.Turno + "-" + A.Data;
                            A.CIP = (string)cb_NomeCIP2.SelectedValue;
                            A.CID = tb_Codigo2.Text.Substring(0, tb_Codigo2.Text.Length - 1);
                            A.Quantidade = 1;
                            A.Clasificação = (string)cb_Clasificação2.SelectedValue;
                            A.Data = fecha.ToShortDateString();
                            A.Turno = EstablecerTurno();
                            A.Resp_Apontamento = lb_NomeUsuario1.Text;
                            A.Resp_Setor = lb_Resp_Setor.Text;
                            A.CIDE = A.CIP + "-" + A.CID + "-" + A.Clasificação;                           
                           

                            DataRow row = dtA.NewRow();                           
                            row["CIR"] = A.CIR;
                            row["CIDE"] = A.CIDE;
                            row["Nome_Peça"] = A.NomePeça;
                            row["CIP"] = A.CIP;
                            row["Clasificação"] = A.Clasificação;
                            row["CID"] = A.CID;
                            row["Quantidade"] = A.Quantidade;
                            row["Data"] = A.Data;
                            row["Turno"] = A.Turno;
                            row["Resp_Apontamento"] = A.Resp_Apontamento;
                            row["Resp_Setor"] = A.Resp_Setor;
                            dtA.Rows.Add(row);


                            ArrayList array = new ArrayList();

                            dt2 = new DataTable();                           
                            dt2.Columns.Add(new DataColumn("CIR", typeof(string)));
                            dt2.Columns.Add(new DataColumn("CIDE", typeof(string)));
                            dt2.Columns.Add(new DataColumn("Nome_Peça", typeof(string)));
                            dt2.Columns.Add(new DataColumn("CIP", typeof(string)));
                            dt2.Columns.Add(new DataColumn("Clasificação", typeof(string)));
                            dt2.Columns.Add(new DataColumn("CID", typeof(string)));
                            dt2.Columns.Add(new DataColumn("Quantidade", typeof(int)));
                            dt2.Columns.Add(new DataColumn("Data", typeof(string)));
                            dt2.Columns.Add(new DataColumn("Turno", typeof(string)));

                            dt2.Columns.Add(new DataColumn("Resp_Apontamento", typeof(string)));

                            dt2.Columns.Add(new DataColumn("Resp_Setor", typeof(string)));


                            foreach (DataRow dr in dtA.Rows)

                            {

                                object total;


                                if (array.IndexOf(dr["CIDE"]) < 0)

                                {


                                    total = dtA.Compute(String.Format("SUM(Quantidade)"), "CIDE = '" + dr["CIDE"] + "'");

                                    dt2.Rows.Add(new object[] {dr["CIR"], dr["CIDE"], dr["Nome_Peça"], dr["CIP"], dr["Clasificação"], dr["CID"], Convert.ToInt32(total), dr["Data"], dr["Turno"], dr["Resp_Apontamento"], dr["Resp_Setor"] });

                                    array.Add(dr["CIDE"]);

                                }

                            }

                            DataView dv = dt2.DefaultView;
                            dv.Sort = "CIP";
                            sorteddt2 = dv.ToTable();
                            tb_CIR.Text = A.CIR;
                            dgv_GestãoRetrabalho.DataSource = sorteddt2;




                        }
                        else
                        {
                            MessageBox.Show("Defeito não existe na base de dados");
                        }


                    }


                }
            }
            else
            {
                if (comp>0)
                {

                    if (tb_Codigo2.Text != "" && cb_DefeitoGeral2.SelectedIndex != 3)
                    {
                        DataTable Defeito = Banco.ObterTodosOnde("TiposDefeitos", "CID", "'" + tb_Codigo2.Text.Substring(0, tb_Codigo2.Text.Length - 1) + "'");

                        if (Defeito.Rows.Count > 0)
                        {
                            A.NomePeça = cb_NomeCIP2.Text;
                            A.CIP = (string)cb_NomeCIP2.SelectedValue;
                            A.CID = tb_Codigo2.Text.Substring(0, tb_Codigo2.Text.Length - 1);

                            A.Quantidade = 1;
                            A.Clasificação = (string)cb_Clasificação2.SelectedValue;
                            A.Data = Dtp_data.Value.ToShortDateString();
                            A.Turno = EstablecerTurno();
                            A.Resp_Apontamento = lb_NomeUsuario1.Text;
                            A.Resp_Setor = lb_Resp_Setor.Text;
                            A.CIDE = "1-"+A.CIP + "-" + A.CID + "-" + A.Clasificação;
                            A.CIR = A.Turno + "-" + A.Data;                           

                            DataRow row = dtA.NewRow();                           
                            row["CIR"] = A.CIR;
                            row["CIDE"] = A.CIDE;
                            row["Nome_Peça"] = A.NomePeça;
                            row["CIP"] = A.CIP;
                            row["Clasificação"] = A.Clasificação;
                            row["CID"] = A.CID;
                            row["Quantidade"] = A.Quantidade;
                            row["Data"] = A.Data;
                            row["Turno"] = A.Turno;
                            row["Resp_Apontamento"] = A.Resp_Apontamento;
                            row["Resp_Setor"] = A.Resp_Setor;
                            dtA.Rows.Add(row);


                            ArrayList array = new ArrayList();

                            dt2 = new DataTable();
                            dt2.Columns.Add(new DataColumn("CIR", typeof(string)));
                            dt2.Columns.Add(new DataColumn("CIDE", typeof(string)));
                            dt2.Columns.Add(new DataColumn("Nome_Peça", typeof(string)));
                            dt2.Columns.Add(new DataColumn("CIP", typeof(string)));
                            dt2.Columns.Add(new DataColumn("Clasificação", typeof(string)));
                            dt2.Columns.Add(new DataColumn("CID", typeof(string)));
                            dt2.Columns.Add(new DataColumn("Quantidade", typeof(int)));
                            dt2.Columns.Add(new DataColumn("Data", typeof(string)));
                            dt2.Columns.Add(new DataColumn("Turno", typeof(string)));

                            dt2.Columns.Add(new DataColumn("Resp_Apontamento", typeof(string)));

                            dt2.Columns.Add(new DataColumn("Resp_Setor", typeof(string)));


                            foreach (DataRow dr in dtA.Rows)

                            {

                                object total;


                                if (array.IndexOf(dr["CIDE"]) < 0)

                                {


                                    total = dtA.Compute(String.Format("SUM(Quantidade)"), "CIDE = '" + dr["CIDE"] + "'");

                                    dt2.Rows.Add(new object[] { dr["CIR"], dr["CIDE"], dr["Nome_Peça"], dr["CIP"], dr["Clasificação"], dr["CID"], Convert.ToInt32(total), dr["Data"], dr["Turno"], dr["Resp_Apontamento"], dr["Resp_Setor"] });

                                    array.Add(dr["CIDE"]);

                                }

                            }

                            DataView dv = dt2.DefaultView;
                            dv.Sort = "CIP";
                            sorteddt2 = dv.ToTable();
                            tb_CIR.Text = A.CIR;
                            dgv_GestãoRetrabalho.DataSource = sorteddt2;




                        }
                        else
                        {
                            MessageBox.Show("Defeito não existe na base de dados");
                        }
                    }
                    else
                    {
                        if (tb_Codigo2.Text != "" && cb_DefeitoGeral2.SelectedIndex == 3 && (string)cb_DefeitoEspecifico2.SelectedValue == "3.1")
                        {
                            DataTable Defeito = Banco.ObterTodosOnde("TiposDefeitos", "CID", "'" + tb_Codigo2.Text.Substring(0, 3) + "'");

                            if (Defeito.Rows.Count > 0)
                            {
                                A.NomePeça = cb_NomeCIP2.Text;
                                A.CIR = A.Turno + "-" + A.Data;
                                A.CIP = (string)cb_NomeCIP2.SelectedValue;
                                A.CID = tb_Codigo2.Text.Substring(0, tb_Codigo2.Text.Length - 1);
                                A.Quantidade = 1;
                                A.Clasificação = (string)cb_Clasificação2.SelectedValue;
                                A.Data = Dtp_data.Value.ToShortDateString();
                                A.Turno = EstablecerTurno();
                                A.Resp_Apontamento = lb_NomeUsuario1.Text;
                                A.Resp_Setor = lb_Resp_Setor.Text;
                                A.CIDE = "1-" + A.CIP + "-" + A.CID + "-" + A.Clasificação;                                


                                DataRow row = dtA.NewRow();                                
                                row["CIR"] = A.CIR;
                                row["CIDE"] = A.CIDE;
                                row["Nome_Peça"] = A.NomePeça;
                                row["CIP"] = A.CIP;
                                row["Clasificação"] = A.Clasificação;
                                row["CID"] = A.CID;
                                row["Quantidade"] = A.Quantidade;
                                row["Data"] = A.Data;
                                row["Turno"] = A.Turno;
                                row["Resp_Apontamento"] = A.Resp_Apontamento;
                                row["Resp_Setor"] = A.Resp_Setor;
                                dtA.Rows.Add(row);


                                ArrayList array = new ArrayList();

                                dt2 = new DataTable();
                                dt2.Columns.Add(new DataColumn("CIR", typeof(string)));
                                dt2.Columns.Add(new DataColumn("CIDE", typeof(string)));
                                dt2.Columns.Add(new DataColumn("Nome_Peça", typeof(string)));
                                dt2.Columns.Add(new DataColumn("CIP", typeof(string)));
                                dt2.Columns.Add(new DataColumn("Clasificação", typeof(string)));
                                dt2.Columns.Add(new DataColumn("CID", typeof(string)));
                                dt2.Columns.Add(new DataColumn("Quantidade", typeof(int)));
                                dt2.Columns.Add(new DataColumn("Data", typeof(string)));
                                dt2.Columns.Add(new DataColumn("Turno", typeof(string)));

                                dt2.Columns.Add(new DataColumn("Resp_Apontamento", typeof(string)));

                                dt2.Columns.Add(new DataColumn("Resp_Setor", typeof(string)));


                                foreach (DataRow dr in dtA.Rows)

                                {

                                    object total;


                                    if (array.IndexOf(dr["CIDE"]) < 0)

                                    {


                                        total = dtA.Compute(String.Format("SUM(Quantidade)"), "CIDE = '" + dr["CIDE"] + "'");

                                        dt2.Rows.Add(new object[] { dr["CIR"], dr["CIDE"], dr["Nome_Peça"], dr["CIP"], dr["Clasificação"], dr["CID"], Convert.ToInt32(total), dr["Data"], dr["Turno"], dr["Resp_Apontamento"], dr["Resp_Setor"] });

                                        array.Add(dr["CIDE"]);

                                    }

                                }

                                DataView dv = dt2.DefaultView;
                                dv.Sort = "CIP";
                                sorteddt2 = dv.ToTable();
                                tb_CIR.Text = A.CIR;
                                dgv_GestãoRetrabalho.DataSource = sorteddt2;




                            }
                            else
                            {
                                MessageBox.Show("Defeito não existe na base de dados");
                            }


                        }


                    }
                }
                else
                {
                    MessageBox.Show("Não e posivel apontar dados con datas futuras. Revise a data e tente novamente");
                }
            }
        }
        #endregion;
        #endregion;

        #region ExluirDados

        #region BotonExluir2
        bool dtcontrolcambio = true;
        private void button1_Click_1(object sender, EventArgs e)//excluir
        {
            if (dtcontrolcambio)
            {
                dgv_GestãoRetrabalho.DataSource = dtA;
                button1.Text = "Atualizar";
                dgv_GestãoRetrabalho.AllowUserToDeleteRows = true;
                dtcontrolcambio = false;
            }
            else
            {
                dt2 = new DataTable();
                dt2.Columns.Add(new DataColumn("CIR", typeof(string)));
                dt2.Columns.Add(new DataColumn("CIDE", typeof(string)));
                dt2.Columns.Add(new DataColumn("Nome_Peça", typeof(string)));
                dt2.Columns.Add(new DataColumn("CIP", typeof(string)));
                dt2.Columns.Add(new DataColumn("Clasificação", typeof(string)));
                dt2.Columns.Add(new DataColumn("CID", typeof(string)));
                dt2.Columns.Add(new DataColumn("Quantidade", typeof(int)));
                dt2.Columns.Add(new DataColumn("Data", typeof(string)));
                dt2.Columns.Add(new DataColumn("Turno", typeof(string)));

                dt2.Columns.Add(new DataColumn("Resp_Apontamento", typeof(string)));

                dt2.Columns.Add(new DataColumn("Resp_Setor", typeof(string)));
                ArrayList array = new ArrayList();
                foreach (DataRow dr in dtA.Rows)

                {

                    object total;


                    if (array.IndexOf(dr["CIDE"]) < 0)

                    {


                        total = dtA.Compute(String.Format("SUM(Quantidade)"), "CIDE = '" + dr["CIDE"] + "'");

                        dt2.Rows.Add(new object[] { dr["CIR"], dr["CIDE"], dr["Nome_Peça"], dr["CIP"], dr["Clasificação"], dr["CID"], Convert.ToInt32(total), dr["Data"], dr["Turno"], dr["Resp_Apontamento"], dr["Resp_Setor"] });

                        array.Add(dr["CIDE"]);

                    }

                }

                DataView dv = dt2.DefaultView;
                dv.Sort = "CIP";
                sorteddt2 = dv.ToTable();

                dgv_GestãoRetrabalho.DataSource = sorteddt2;
                button1.Text = "Excluir";
                dtcontrolcambio = true;
                dgv_GestãoRetrabalho.AllowUserToDeleteRows = false;
            }
        }
        #endregion;

        #region BotonExcluir1

        bool dtcontrolcambio2;
        private void btn_Exluir_Click(object sender, EventArgs e)
        {
            if (dtcontrolcambio2)
            {

                dgv_GestãoRetrabalho.DataSource = dtA;
                btn_Exluir.Text = "Atualizar";
                dgv_GestãoRetrabalho.AllowUserToDeleteRows = true;
                dtcontrolcambio2 = false;
            }
            else
            {
                dt2 = new DataTable();      
                dt2.Columns.Add(new DataColumn("CIR", typeof(string)));
                dt2.Columns.Add(new DataColumn("CIDE", typeof(string)));
                dt2.Columns.Add(new DataColumn("Nome_Peça", typeof(string)));
                dt2.Columns.Add(new DataColumn("CIP", typeof(string)));
                dt2.Columns.Add(new DataColumn("Clasificação", typeof(string)));
                dt2.Columns.Add(new DataColumn("CID", typeof(string)));
                dt2.Columns.Add(new DataColumn("Quantidade", typeof(int)));
                dt2.Columns.Add(new DataColumn("Data", typeof(string)));
                dt2.Columns.Add(new DataColumn("Turno", typeof(string)));
                dt2.Columns.Add(new DataColumn("Resp_Apontamento", typeof(string)));
                dt2.Columns.Add(new DataColumn("Resp_Setor", typeof(string)));
                
                ArrayList array = new ArrayList();

                foreach (DataRow dr in dtA.Rows)

                {

                    object total;


                    if (array.IndexOf(dr["CIDE"]) < 0)

                    {
                        total = dtA.Compute(String.Format("SUM(Quantidade)"), "CIDE = '" + dr["CIDE"] + "'");

                        dt2.Rows.Add(new object[] { dr["CIR"], dr["CIDE"], dr["Nome_Peça"], dr["CIP"], dr["Clasificação"], dr["CID"], Convert.ToInt32(total), dr["Data"], dr["Turno"], dr["Resp_Apontamento"], dr["Resp_Setor"] });

                        array.Add(dr["CIDE"]);

                    }

                }

                DataView dv = dt2.DefaultView;
                dv.Sort = "CIP";
                sorteddt2 = dv.ToTable();

                dgv_GestãoRetrabalho.DataSource = sorteddt2;
                btn_Exluir.Text = "Excluir";
                dtcontrolcambio2 = true;
                dgv_GestãoRetrabalho.AllowUserToDeleteRows = false;
            }
        }

        #endregion;

        #endregion;


        #endregion;

        #region BotonHabilitarUsuario2
        public bool controlBtn = true;
        private void button1_Click(object sender, EventArgs e)
        {
            if (controlBtn)
            {
                Reforma h = new Reforma(1, new F_Principal());
                F_AutorizarOuAtivar f_AutorizarOuAtivar = new F_AutorizarOuAtivar(h);

                btn_Habilitar.Text = "Deshabilitar Usuario 2";
                controlBtn = false;
                Globais.Abreform(1, f_AutorizarOuAtivar);


            }
            else
            {
                puedeActivar = false;
                cb_NomeCIP.SelectedValue = 1;
                cb_FamilaPeça.SelectedIndex=0;
                tb_CódigoDefeito.Enabled = false;
                cb_DefeitoGeral.Enabled = false;
                cb_FamilaPeça.Enabled = false;
                lb_NomeUsuario2.Text = "--";
                lb_NomeUsuario2.Enabled = false;
                cb_Clasificação.Enabled = false;
                btn_Apontar.Enabled = false;
                tb_Codigo1.Enabled = false;
                btn_Exluir.Enabled = false;
                btn_Habilitar.Text = "Habilitar Usuario 2";
                
                controlBtn = true;



            }

        }
        #endregion;

        #region SalvarDatos
        private void btn_SalvarUsu2_Click(object sender, EventArgs e)
        {
            if (dtA.Rows.Count>0)
            {


                AutorizarAção Autorizar = new AutorizarAção(this);

                Globais.Abreform(1, Autorizar);


                if (AçãoAutorizada)
                {

                        F_SalvarRelatoriosReforma salvar = new F_SalvarRelatoriosReforma(sorteddt2, sorteddt2.Rows.Count, m1);
                        Globais.Abreform(1,salvar);
                        

                }

                #region RestablecerDataGripView

                ReestabelecerDGVdtA();
                dgv_GestãoRetrabalho.DataSource = dtA;
                #endregion;


            }

        }
        #endregion;

        private void Dtp_data_ValueChanged(object sender, EventArgs e)
        {
            //tb_CIR.Text = Dtp_data.Value.ToShortDateString();
            tb_CIR.Text = Dtp_data.Text;
        }

        private void btn_NovaOperação_Click(object sender, EventArgs e)
        {
            RelatoriosDeSaidaReformaSalvos RelatoriosSalvos = new RelatoriosDeSaidaReformaSalvos("R");
            Globais.Abreform(0,RelatoriosSalvos);
        }

        private void F_GestãoEControleDeRetrablho_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}

