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
using Org.BouncyCastle.Asn1;

namespace Atlas_projeto
{
    public partial class F_Peças : Form
    {
        #region VARIABLES
        string IdSeleccionados,setor;
        DataTable dt;
        DataTable Familias;
        DataTable Modelos;
        DataTable Caracteristicas;
        string CIP,AuxCIP;
       
        bool control = false;
        string NomeCIP;
        string NomeFormal;
        int linha;
        int[] IndicedePontos = new int[5];
        int[] IndicedeEspaço = new int[4];
        int[] IndiceFMCSV = new int[5];
        string[] IndiceNomesFMCV = new string[5];
        decimal ValorBagPó = 0;
        decimal CapacidadeBagPó = 0;
        string OrigemCompleto = "";
        string foto = "";
        string PastaDestino = Globais.CaminhoFotos + @"Peças\";
        string DestinoCompleto = "";
        bool sustituir = true;
        Peça p;
        string setorRef;
        string NomeTablaPeça = "";

        #endregion;

        public F_Peças(string Setor)
        {
            InitializeComponent();
            setor = Setor;
            
        }

        private void F_Peças_Load(object sender, EventArgs e)
        {
            #region POPULAR COMBOBOXES
            //populando ComboBoxFamilia
            cb_Familia.Items.Clear();
            cb_Familia.DataSource = Banco.ObterTodos("Familias", "*", "IdFamilia");
            cb_Familia.DisplayMember = "Familia";
            cb_Familia.ValueMember = "IdFamilia";

            //populando ComboBoxSetor
            cb_Setor.Items.Clear();
            cb_Setor.DataSource = Banco.ObterTodos("Setores", "*", "Id");
            cb_Setor.DisplayMember = "Nome";
            cb_Setor.ValueMember = "Id";

            //Populando ComboBox Modelo
            cb_Modelo.Items.Clear();
            cb_Modelo.DataSource = Banco.ObterTodos("Modelos", "*", "IdModelos");
            cb_Modelo.DisplayMember = "Modelo";
            cb_Modelo.ValueMember = "IdModelos";

            //Populando ComboBox Caracteristica
            cb_Caracteristica.Items.Clear();
            cb_Caracteristica.DataSource = Banco.ObterTodos("Caracteristicas", "*", "IdCaracteristica");
            cb_Caracteristica.DisplayMember = "Caracteristica";
            cb_Caracteristica.ValueMember = "IdCaracteristica";

            //Populando ComboBox Filtro
            cb_Filtro.Items.Clear();
            cb_Filtro.DataSource = Banco.ObterTodos("Familias", "*", "IdFamilia");
            cb_Filtro.DisplayMember = "Familia";
            cb_Filtro.ValueMember = "IdFamilia";
            #endregion;

            #region CONFIGURANDO DGV
            //Preenchendo el DataGrip...
            dgv_Peças.DataSource = null;
           
            dt = Banco.Procurar("Setores", "Id", "Nome", "'" + setor + "'", "Id");
            nud_Setor.Value= cb_Setor.SelectedIndex = Convert.ToInt32(dt.Rows[0].Field<Int64>("Id"));

            AssignarNomeTablaPeça();
            dgv_Peças.DataSource = Banco.Procurar(NomeTablaPeça, "CIP, NomeCIP", "NomeCIP", "'" + string.Empty + "%'", "CIP");

            dgv_Peças.Columns[0].Width = 60;
            dgv_Peças.Columns[1].Width = 300;
          

            #endregion;

            #region CONFIGURAÇOES GERAIS
            Familias = Banco.ObterTodos("Familias");
            Modelos = Banco.ObterTodos("Modelos");

            Caracteristicas = Banco.ObterTodos("Caracteristicas");
            DataTable dt2 = Banco.Procurar("Custos", "Quantidade, Valor", "Item", "'Bag de Pó'", "Quantidade");

            ValorBagPó = dt2.Rows[0].Field<decimal>("Valor");
            CapacidadeBagPó = dt2.Rows[0].Field<Int64>("Quantidade");

            nud_Familia.Maximum = Familias.Rows.Count - 1;
            nud_Modelo.Maximum = Modelos.Rows.Count - 1;
            nud_Caracteristica.Maximum = Caracteristicas.Rows.Count - 1;
            #endregion;

        }


        #region PROCCEEDMIENTOS
        private void AtualizarCIP()
        {
            Peça p = new Peça();
            p.CIP=tb_Id.Text;

        
            try
            {
                bool Atualizou;
              DataTable dt=  Banco.ObterTodos("Peças");
                for (int i=0; i<dt.Rows.Count;i++)
                {
                    CIP = dt.Rows[i].Field<string>("CIP");
                    
                    if (EstablecerIndicesPontosCIP(CIP)<4)
                    {
                        Atualizou = Banco.Atualizar("Peças", "CIP='" +CIP + ".0'", "CIP", "'" + CIP + "'");
                    }
                }

               

                dgv_Peças.DataSource = Banco.Procurar("Peças", "CIP, NomeCIP", "NomeCIP", "'" + cb_Filtro.Text + "%'", "CIP");
               

              
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show("Erro: " + ex.Message + "\nvalor actual: " + ex.ActualValue + "\n" + ex.ParamName + "\naqui=" + ex.InnerException);
            }
        }
        private bool SalvarArquivo()
        {
            if (DestinoCompleto == "")
            {
                if (MessageBox.Show("Foto não Selecionada, deseja continuar?", "Foto Não Selecionada", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                {
                    return true;
                }
                return false;

            }
            else
            {
                try
                {
                    if (DestinoCompleto != "")
                    {
                        if (OrigemCompleto != "")
                        {
                            File.Copy(OrigemCompleto, DestinoCompleto, sustituir);
                        }

                        if (File.Exists(DestinoCompleto))
                        {
                            pb_Foto.ImageLocation = DestinoCompleto;
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
                catch (Exception ex)
                {
                    MessageBox.Show("Erro: " + ex.Message, "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                return false;
            }

        }
        private void ActualizarNomeNaBarraeID()
        {
            tb_Id.Text = "" + nud_Familia.Value + "." + nud_Modelo.Value + "." + nud_Caracteristica.Value + "." + nud_Setor.Value + "." + nud_Version.Value;
            if (nud_Version.Value == 0)
            {
                tb_NomeCIP.Text = "" + cb_Familia.Text + " " + cb_Modelo.Text + " " + cb_Caracteristica.Text;
            }
            else
            {
                tb_NomeCIP.Text = "" + cb_Familia.Text + " " + cb_Modelo.Text + " " + cb_Caracteristica.Text + " " + nud_Version.Value;
            }
        }
        private void CrearPeça()
        {
            p = new Peça();
            p.CIP = tb_Id.Text;
            p.NomeCIP = tb_NomeCIP.Text;
            p.NomeFormal = tb_NomeFormal.Text.ToUpper();
            p.Código = tb_Código.Text;
            p.Altura = Dz.Value;
            p.Cumprimento = Dx.Value;
            p.Largura = Dy.Value;
            p.ValorEstampada = nud_VAlorEstampada.Value;
            p.ValorProcesso = nud_ValorProcesso.Value;
            p.ValorSucata1 = nud_ValorSucateamento1.Value;
            p.ValorSucata2 = nud_Sucateamento2.Value;
            p.Diametro = nud_Diametro.Value;
            p.Alto = nud_Alto.Value;
            p.Alto2 = nud_Alto2.Value;
            p.CamadaMin = nud_CamadaMin.Value;
            p.CamadaMax = nud_CamadaMax.Value;
            p.Imagem = DestinoCompleto;
            p.MassaSemEsmaltar = nud_MSE.Value.ToString();
            p.MassaEsmaltada = nud_MSE.Value.ToString();

            if (Circular.Checked == true)
            {
                p.Circular = "SIM";
            }
            else
            {
                p.Circular = "NÃO";
            }
        }
        private DataTable PreencherDataTableParaRelatorioPeça()
        {
            CrearPeça();

            DataTable dtPeça = new DataTable();
            dtPeça.Columns.Add("CIP");
            dtPeça.Columns.Add("NomeCIP");
            dtPeça.Columns.Add("NomeFormal");
            dtPeça.Columns.Add("código");
            dtPeça.Columns.Add("Cumprimento");
            dtPeça.Columns.Add("Largura");
            dtPeça.Columns.Add("Altura");
            dtPeça.Columns.Add("ValorEstampada");
            dtPeça.Columns.Add("ValorProcesso");
            dtPeça.Columns.Add("ValorSucata1");
            dtPeça.Columns.Add("ValorSucata2");
            dtPeça.Columns.Add("Diametro");
            dtPeça.Columns.Add("Alto");
            dtPeça.Columns.Add("Alto2");
            dtPeça.Columns.Add("CamadaMin");
            dtPeça.Columns.Add("CamadaMax");
            dtPeça.Columns.Add("Imagem");
            dtPeça.Columns.Add("Circular");

            DataRow row = dtPeça.NewRow();

            row["CIP"] = p.CIP;
            row["NomeCIP"] = p.NomeCIP;
            row["NomeFormal"] = p.NomeFormal;
            row["código"] = p.Código;
            row["Cumprimento"] = p.Cumprimento;
            row["Largura"] = p.Largura;
            row["Altura"] = p.Altura;
            row["ValorEstampada"] = p.ValorEstampada;
            row["ValorProcesso"] = p.ValorProcesso;
            row["ValorSucata1"] = p.ValorSucata1;
            row["ValorSucata2"] = p.ValorSucata2;
            row["Diametro"] = p.Diametro;
            row["Alto"] = p.Alto;
            row["Alto2"] = p.Alto2;
            row["CamadaMin"] = p.CamadaMin;
            row["CamadaMax"] = p.CamadaMax;
            row["Imagem"] = p.Imagem;
            row["Circular"] = p.Circular;


            dtPeça.Rows.Add(row);

            return dtPeça;

        }
        private string AssignarValorAtualizar(int ValorColumna, string NomeColuna)
        {
            string Valor;
            bool x;
            bool control2;
            do
            {
                x = false;
                control2 = true;
                if (ValorColumna != 13)
                {
                    Valor = Interaction.InputBox("COLUMNA ESCOLHIDA:\n\n" + NomeColuna + "\n\nDIGITE O VALOR DESEJADO\n\n Só são permitidos VALORES NUMÉRICOS!!\n\n", "INSERIR UM VALOR");
                    if (Valor != "")
                    {
                        foreach (char c in Valor)
                        {
                            if (!char.IsDigit(c) && c != ',')
                            {
                                x = true;
                            }

                        }
                        if (x)
                        {
                            DialogResult res = MessageBox.Show("Só pode inserir Valores Numéricos, ou separador decimal \",\"\n\n TENTAR NOVAMENTE? ", "DADO INVALIDO", MessageBoxButtons.YesNo);
                            if (res == DialogResult.Yes)
                            {
                                control2 = false;
                            }
                            else
                            {

                                return string.Empty;
                            }
                        }
                    }
                    else
                    {
                        DialogResult res = MessageBox.Show("Deseja Cancelar a Operação?", "CANCELAR", MessageBoxButtons.YesNo);
                        if (res == DialogResult.No)
                        {
                            control2 = false;
                        }
                        else
                        {
                            return string.Empty;
                        }
                    }
                }
                else
                {
                    Valor = Interaction.InputBox("DIGITE UM DOS SEGUINTES NÚMEROS:\n\n 1.-SIM         2.-NÃO!!\n\n", "É CIRCULAR?");
                    foreach (char c in Valor)
                    {
                        if (!char.IsDigit(c))
                        {
                            x = true;
                        }

                    }

                    if (x || (Valor.Length > 1) || decimal.Parse(Valor) > 2 || decimal.Parse(Valor) < 1)
                    {
                        DialogResult res = MessageBox.Show("Insiriu um valor distinto de 1 ou 2\n\nDeve Inserir:\n\n1 para SIM \nou \n2 para NÃO \n\n TENTAR NOVAMENTE?\n ", "DADO INVALIDO", MessageBoxButtons.YesNo);
                        if (res == DialogResult.Yes)
                        {
                            control2 = false;
                        }
                        else
                        {

                            return string.Empty;
                        }
                    }
                }

            }
            while (control2 == false);

            return Valor;
        }
        private void EstablecerValorIndicesFMCV(string CIP)
        {
            EstablecerIndicesPontosCIP(CIP);
           
                IndiceFMCSV[0] = int.Parse(CIP.Substring(0, IndicedePontos[0]));

                IndiceFMCSV[1] = int.Parse(CIP.Substring(IndicedePontos[0] + 1, IndicedePontos[1] - (IndicedePontos[0] + 1)));

                IndiceFMCSV[2] = int.Parse(CIP.Substring(IndicedePontos[1] + 1, IndicedePontos[2] - (IndicedePontos[1] + 1)));

                IndiceFMCSV[3] = int.Parse(CIP.Substring(IndicedePontos[2] + 1, IndicedePontos[3] - (IndicedePontos[2] + 1)));

                IndiceFMCSV[4] = int.Parse(CIP.Substring(IndicedePontos[3] + 1, CIP.Length - (IndicedePontos[3] + 1)));
           
            

        }
        private void ExcluirVariosdeumMismoTipo(string Tabla, string Columnas, string Onde, string Palavra, string ordenar, int tipo, int valor)
        {
            string CIP;


            DataTable Dados = Banco.Procurar(Tabla, Columnas, Onde, Palavra, ordenar);
            if (Dados.Rows.Count > 0)
            {
                foreach (DataRow dr in Dados.Rows)
                {
                    CIP = (string)dr[Onde];

                    EstablecerValorIndicesFMCV(CIP);

                    MessageBox.Show("Valor= " + valor + " IndiceValor=" + IndiceFMCSV[tipo] + "\n\nCIP= " + CIP + "\n INDICE 1" + IndiceFMCSV[0] + "\n INDICE 2" + IndiceFMCSV[1] + "\n INDICE 3" + IndiceFMCSV[2] + "\n INDICE 4" + IndiceFMCSV[3]);
                    if (valor == IndiceFMCSV[tipo])
                    {
                        bool Excuiu = Banco.Excluir(Tabla, Onde, "'" + (string)dr[Onde] + "'", "'" + (string)dr["NomeCIP"] + "'");
                        if (Excuiu)
                        {

                            if (dr["Foto"].ToString() != "")
                            {
                                if (File.Exists(dr["Foto"].ToString()))
                                {
                                    File.Delete(dr["Foto"].ToString());
                                    MessageBox.Show("Arquivo Excluido");
                                }
                                else
                                {
                                    MessageBox.Show("Arquivo  não Excluido", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }

                        }


                    }

                }
            }


        }
        private bool AtualizarVariosNomesdeumMismoTipodePeça(string Tabla, string Columnas, string Onde, string Palavra, string ordenar, int tipo, int valor, string text)
        {
            string CIP;
            string txt2 = "";
            int Sucessos = 0;
            int Erros = 0;
            bool atualizou = false;
            int cont = 0;

            DataTable Dados = Banco.Procurar(Tabla, Columnas, Onde, Palavra, ordenar);
            if (Dados.Rows.Count > 0)
            {
                foreach (DataRow dr in Dados.Rows)
                {
                    CIP = (string)dr[Onde];

                    EstablecerValorIndicesFMCV(CIP);

                    if (valor == IndiceFMCSV[tipo])
                    {
                        cont += 1;
                        EstablecerValorNomesCIPFMCV((string)dr["NomeCIP"]);


                        switch (tipo)
                        {
                            case 0:
                                txt2 = text + " " + IndiceNomesFMCV[1] + " " + IndiceNomesFMCV[2] + " " + IndiceNomesFMCV[3];
                                break;
                            case 1:
                                txt2 = IndiceNomesFMCV[0] + " " + text + " " + IndiceNomesFMCV[2] + " " + IndiceNomesFMCV[3];

                                break;
                            case 2:
                                txt2 = IndiceNomesFMCV[0] + " " + IndiceNomesFMCV[1] + " " + text + " " + IndiceNomesFMCV[3];
                                break;
                        }
                        MessageBox.Show("Nome Atual= " + (string)dr["NomeCIP"] + "\n\nCIP= " + CIP + "\nFAMILIA= " + IndiceNomesFMCV[0] + "\nMODELO= " + IndiceNomesFMCV[1] + "\nCARACTERISTICA= " + IndiceNomesFMCV[2] + "\nVERSÃO= " + IndiceNomesFMCV[3] + "\n\nNomeFinal= " + txt2);


                        bool Atualizou = Banco.Atualizar(Tabla, "NomeCIP= '" + txt2 + "'", "NomeCIP", "'" + (string)dr["NomeCIP"] + "'");
                        if (Atualizou)
                        {

                            Sucessos += 1;
                            atualizou = true;
                        }
                        else
                        {
                            Erros += 1;
                        }


                    }


                }


                if (Sucessos > 0 || Erros > 0)
                {
                    MessageBox.Show("Dados Atualizados: " + Sucessos + "/" + cont + "\n\n Dados não Atualizados: " + Erros + "/" + cont);
                }
                else
                {
                    MessageBox.Show("Sem Dados Para Atualizar");
                }
            }
            else
            {
                MessageBox.Show("Sem Dados Para Atualizar");

            }
            return atualizou;
        }
        private bool AtualizarVariosdeumMismoTipo(int valorFamilia, string NomeColumna, string text, string NomeFamilia)
        {

            int Sucessos = 0;
            int Erros = 0;
            bool atualizou = false;
            int cont = 0;
            DataTable Dados;
            AssignarNomeTablaPeça();
            Dados = Banco.Procurar(NomeTablaPeça, "*", "CIP", "'" + valorFamilia + ".%'", "CIP");



            DialogResult res = MessageBox.Show("Familia a Actualizar= " + NomeFamilia + "\nID= " + valorFamilia + "\n\nTotal de Dados: " + Dados.Rows.Count, "ATUALIZAR?", MessageBoxButtons.YesNo);
            if (res == DialogResult.Yes)
            {

                if (Dados.Rows.Count > 0)
                {
                    foreach (DataRow dr in Dados.Rows)
                    {


                        EstablecerValorIndicesFMCV((string)dr["CIP"]);

                        if (valorFamilia == IndiceFMCSV[0])
                        {
                            cont += 1;
                            DialogResult res2 = MessageBox.Show("PEÇA ATUAL\n\nNome=  " + (string)dr["NomeCIP"] + "\nCIP=  " + (string)dr["CIP"] + "\n\nNOVOS DADOS\nDado Atualizar=  " + NomeColumna + "\nValor Atualizar=  " + text, "ATUALIZAR?", MessageBoxButtons.YesNo);
                            if (res2 == DialogResult.Yes)
                            {
                                bool Atualizou;
                                AssignarNomeTablaPeça();
                                Atualizou = Banco.Atualizar(NomeTablaPeça, "'" + NomeColumna + "'= '" + text + "'", "CIP", "'" + (string)dr["CIP"] + "'");


                                if (Atualizou)
                                {
                                    MessageBox.Show("Atualizou");
                                    Sucessos += 1;
                                    atualizou = true;
                                }
                                else
                                {
                                    MessageBox.Show("ERRO: Não Atualizou");
                                    Erros += 1;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Não Atualizou");

                            }


                        }


                    }


                    if (Sucessos > 0 || Erros > 0)
                    {
                        MessageBox.Show("Dados Atualizados: " + Sucessos + "/" + cont + "\n\n Dados não Atualizados: " + Erros + "/" + cont);
                    }
                    else
                    {
                        MessageBox.Show("Nehum Dado Com os Criterios Para Atualizar");
                    }
                }
                else
                {
                    MessageBox.Show("NÃO há dados Achados Para Atualizar");

                }
            }
            return atualizou;
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
        private void EstablecerValorNomesCIPFMCV(string NomeCIP)
        {
            int m = EstablecerIndicesEspaciosNomeCIP(NomeCIP);

            switch (m)
            {
                case 0:
                    IndiceNomesFMCV[0] = NomeCIP.Substring(0, NomeCIP.Length);

                    IndiceNomesFMCV[1] = string.Empty;

                    IndiceNomesFMCV[2] = string.Empty;

                    IndiceNomesFMCV[3] = string.Empty;

                    break;

                case 1:
                    IndiceNomesFMCV[0] = NomeCIP.Substring(0, IndicedeEspaço[0]);

                    IndiceNomesFMCV[1] = NomeCIP.Substring(IndicedeEspaço[0] + 1, NomeCIP.Length - (IndicedeEspaço[0] + 1));

                    IndiceNomesFMCV[2] = string.Empty;

                    IndiceNomesFMCV[3] = string.Empty;


                    break;

                case 2:
                    IndiceNomesFMCV[0] = NomeCIP.Substring(0, IndicedeEspaço[0]);

                    IndiceNomesFMCV[1] = NomeCIP.Substring(IndicedeEspaço[0] + 1, IndicedeEspaço[1] - (IndicedeEspaço[0] + 1));

                    IndiceNomesFMCV[2] = NomeCIP.Substring(IndicedeEspaço[1] + 1, NomeCIP.Length - (IndicedeEspaço[1] + 1));

                    IndiceNomesFMCV[3] = string.Empty;

                    break;

                case 3:
                    IndiceNomesFMCV[0] = NomeCIP.Substring(0, IndicedeEspaço[0]);

                    IndiceNomesFMCV[1] = NomeCIP.Substring(IndicedeEspaço[0] + 1, IndicedeEspaço[1] - (IndicedeEspaço[0] + 1));

                    IndiceNomesFMCV[2] = NomeCIP.Substring(IndicedeEspaço[1] + 1, IndicedeEspaço[2] - (IndicedeEspaço[1] + 1));

                    IndiceNomesFMCV[3] = NomeCIP.Substring(IndicedeEspaço[2] + 1, NomeCIP.Length - (IndicedeEspaço[2] + 1));


                    break;
            }

        }
        private int EstablecerIndicesEspaciosNomeCIP(string NomeCIP)
        {
            char c;

            int m = 0;

            for (int i = 0; i < NomeCIP.Length; i++)
            {
                c = char.Parse(NomeCIP.Substring(i, 1));

                if (char.IsWhiteSpace(c))
                {
                    IndicedeEspaço[m] = i;
                    m += 1;

                }
            }
            return m;
        }
        private void AssignarCIP()
        {

            IdSeleccionados = dgv_Peças.Rows[dgv_Peças.SelectedRows[0].Index].Cells[0].Value.ToString();
            AssignarNomeTablaPeça();
            dt = Banco.ObterTodosOnde(NomeTablaPeça, "CIP", "'" + IdSeleccionados + "'");


            if (dt.Rows.Count > 0)
            {
                tb_Id.Text =AuxCIP= dt.Rows[0].Field<string>("CIP");
                tb_NomeCIP.Text = dt.Rows[0].Field<string>("NomeCIP");
                tb_NomeFormal.Text = dt.Rows[0].Field<string>("NomeFormal");
                tb_Código.Text = dt.Rows[0].Field<string>("Código");
                nud_VAlorEstampada.Value = decimal.Parse(dt.Rows[0].Field<string>("ValorEstampada"));
                nud_ValorProcesso.Value = decimal.Parse(dt.Rows[0].Field<string>("ValorProcesso"));
                nud_ValorSucateamento1.Value = decimal.Parse(dt.Rows[0].Field<string>("ValorSucata1"));
                nud_Sucateamento2.Value = decimal.Parse(dt.Rows[0].Field<string>("ValorSucata2"));
                Dx.Value = decimal.Parse(dt.Rows[0].Field<string>("Cumprimento"));
                Dy.Value = decimal.Parse(dt.Rows[0].Field<string>("Largura"));
                Dz.Value = decimal.Parse(dt.Rows[0].Field<string>("Altura"));
                nud_Diametro.Value = decimal.Parse(dt.Rows[0].Field<string>("Diametro"));
                nud_Alto.Value = decimal.Parse(dt.Rows[0].Field<string>("Alto"));
                nud_Alto2.Value = decimal.Parse(dt.Rows[0].Field<string>("Alto2"));

                nud_CamadaMin.Value = decimal.Parse(dt.Rows[0].Field<string>("CamadaMin"));
                nud_CamadaMax.Value = decimal.Parse(dt.Rows[0].Field<string>("CamadaMax"));
                DestinoCompleto = dt.Rows[0].Field<string>("Foto");
                pb_Foto.ImageLocation = DestinoCompleto;
                CIP = tb_Id.Text;
                NomeCIP = tb_NomeCIP.Text;
                NomeFormal = tb_NomeFormal.Text;
                btn_AddCaracteristica.Visible = false;
                btn_AddFamilia.Visible = false;
                btn_AddModelo.Visible = false;
                btn_ExcluirCaracteristica.Visible = false;
                btn_ExcluirFamilia.Visible = false;
                btn_ExcluirModelo.Visible = false;

                cb_Familia.Enabled = false;
                cb_Modelo.Enabled = false;
                cb_Caracteristica.Enabled = false;
                nud_Familia.Enabled = false;
                nud_Modelo.Enabled = false;
                nud_Caracteristica.Enabled = false;
                nud_Version.Enabled = false;

                btn_AtualizarFamilia.Visible = false;
                btn_AtualizarModelo.Visible = false;
                btn_AtualizarCaracteristica.Visible = false;
                btn_Imprimir.Enabled = true;



                if (dt.Rows[0].Field<string>("Circular") == "SIM")
                {
                    Circular.Checked = true;

                }
                else
                {
                    Circular.Checked = false;

                }


                if (DestinoCompleto == "" || DestinoCompleto == null)
                {
                    btn_AddFoto.Text = "Add Foto";
                    btn_ExcluirFoto.Enabled = false;
                }
                else
                {
                    btn_AddFoto.Text = "Trocar Foto";
                    btn_ExcluirFoto.Enabled = true;
                }

                EstablecerValorIndicesFMCV(CIP);


                cb_Familia.SelectedValue = IndiceFMCSV[0];

                cb_Modelo.SelectedValue = IndiceFMCSV[1];

                cb_Caracteristica.SelectedValue = IndiceFMCSV[2];

                nud_Version.Value = IndiceFMCSV[3];

                EstablecerSeECircularOuNão();
            }


        }
        private void Restablecer()
        {
            btn_Atualizar.Text = "Atualizar";
            btn_Excluir.Enabled = true;

            int contilinha = dgv_Peças.Rows.Count;
            if (contilinha > 0)
            {
                int row = dgv_Peças.CurrentRow.Index;
                dgv_Peças.Rows[row].Cells[0].Selected = true;
                // dgv_Peças.FirstDisplayedScrollingRowIndex = row;
                if (control)
                {
                    dgv_Peças.Rows[linha].Cells[0].Selected = true;

                    dgv_Peças.CurrentCell = dgv_Peças[0, linha];
                    control = false;

                }
                try
                {

                    AssignarCIP();



                    int dgvIndex = dgv_Peças.CurrentRow.Index;
                    dgv_Peças.Rows[dgvIndex].Selected = true;
                    dgv_Peças.CurrentCell = dgv_Peças[0, dgvIndex];


                    btn_Novo.Enabled = true;
                    btn_C.Enabled = false;
                    tb_Id.ReadOnly = true;

                }
                catch (ArgumentOutOfRangeException ex)
                {
                    MessageBox.Show("Erro: " + ex.Message);

                }

            }
        }
        private void AssignarValorProcesso()
        {
            if (Circular.Checked == true)
            {
                if (CapacidadeBagPó > 0)
                {
                    nud_ValorProcesso.Value = Math.Round(Convert.ToDecimal(Math.Pow(Convert.ToDouble(nud_Diametro.Value / 2), 2) * Math.PI * Math.Pow(10, -4)) * nud_CamadaMax.Value * ValorBagPó / (CapacidadeBagPó * 1000), 2);

                }
            }
            else
            {
                if (CapacidadeBagPó > 0)
                {
                    nud_ValorProcesso.Value = Math.Round((Dx.Value * Dz.Value * Convert.ToDecimal(Math.Pow(10, -4)) * nud_CamadaMax.Value * ValorBagPó) / (CapacidadeBagPó * 1000), 2);

                }
            }
        }
        private void EstablecerSeECircularOuNão()
        {

            if (Circular.Checked == true)
            {
                nud_Diametro.Enabled = true;
                nud_Alto.Enabled = true;
                Dz.Enabled = false;
                Dx.Enabled = false;
                Dy.Enabled = false;
                nud_OM.Enabled = true;

            }
            else
            {
                nud_Diametro.Enabled = false;
                nud_Alto.Enabled = false;
                Dz.Enabled = true;
                Dx.Enabled = true;
                Dy.Enabled = true;
                nud_OM.Enabled = false;
            }
        }
        private int EstabelecerOValorDo_ID(DataTable Dados, string NomeColumnaID)
        {
            int IdAnterior = 0;
            int IdAtual;
            int control = 0;
            int control2 = 0;
            int id = 0;
            foreach (DataRow dr in Dados.Rows)
            {
                if (control2 > control)
                {

                    IdAtual = Convert.ToInt32(dr[NomeColumnaID]) - IdAnterior;

                    if (IdAtual > 1)
                    {
                        id = IdAnterior + 1;
                        return id;
                    }
                    else
                    {

                        id = Dados.Rows.Count;
                    }

                }

                IdAnterior = Convert.ToInt32(dr[NomeColumnaID]);
                control += 1;
                control2 = 1 + control;



            }
            return id;


        }
        private void AssignarNomeTablaPeça()
        {
            if (cb_Setor.Text == "Esmaltação")
            {
                NomeTablaPeça = "Peças";
            }
            else
            {
                NomeTablaPeça = "PEÇAS" + cb_Setor.Text;

            }
        }
        private void Filtrar()
        {
            if (dgv_Peças.Rows.Count > 0)
            {
                if (cb_Filtro.SelectedIndex > 0)
                {
                    btn_AtualizarTodas.Visible = true;
                }
                else
                {
                    btn_AtualizarTodas.Visible = false;
                }
                AssignarNomeTablaPeça();

                DataTable dt = Banco.Procurar(NomeTablaPeça, "CIP, NomeCIP", "NomeCIP", "'" + cb_Filtro.Text + "%'", "CIP");
                if (dt.Rows.Count > 0)
                {
                    dgv_Peças.DataSource = dt;

                    EstablecerSeECircularOuNão();
                }
                else { MessageBox.Show("Não há nenhuma peça do tipo procurado"); }
            }

        }

        private void Atualizar()
        {
            try
            {
                bool Atualizou;

                linha = dgv_Peças.SelectedRows[0].Index;

                Atualizou = Banco.Atualizar(NomeTablaPeça, "CIP='" + p.CIP + "', Código= '" + p.Código + "', NomeCIP= '" + p.NomeCIP + "', NomeFormal='" + p.NomeFormal + "', Largura= '" + p.Largura + "', Cumprimento= '" + p.Cumprimento + "', Altura= '" + p.Altura + "', Diametro= '" + p.Diametro + "', Alto= '" + p.Alto + "', Alto2= '" + p.Alto2 + "', ValorEstampada= '" + p.ValorEstampada + "', ValorProcesso= '" + p.ValorProcesso + "', ValorSucata1= '" + p.ValorSucata1 + "', ValorSucata2= '" + p.ValorSucata2 + "', CamadaMin= '" + p.CamadaMin + "', CamadaMax= '" + p.CamadaMax + "', Foto= '" + p.Imagem + "', Circular= '" + p.Circular + "', MSE= '" + p.MassaSemEsmaltar + "', ME= '" + p.MassaEsmaltada + "'", "CIP", "'" + p.CIP + "'");

                // dgv_Peças.DataSource = Banco.Procurar("Peças", "CIP, NomeCIP", "NomeCIP", "'" + cb_Filtro.Text + "%'", "CIP");
                dgv_Peças[0, linha].Value = p.CIP;
                dgv_Peças[1, linha].Value = p.NomeCIP;

                if (Atualizou)
                {
                    Restablecer();
                    MessageBox.Show("Dados Atualizados");

                }
                else
                {
                    Restablecer();
                    MessageBox.Show("Não Foi Possivel Atualizar os Dados");
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show("Erro: " + ex.Message + "\nvalor actual: " + ex.ActualValue + "\n" + ex.ParamName + "\naqui=" + ex.InnerException);
            }
        }
        private void AtualizarDados()
        {

            DialogResult res = MessageBox.Show("Quer mesmo Atualizar os dados?", "Quer atualizar?", MessageBoxButtons.YesNo);
            if (res == DialogResult.Yes)
            {
                AssignarNomeTablaPeça();
                if (SalvarArquivo())
                {
                    return;
                }

                CrearPeça();

                dt = Banco.ObterTodosOnde(NomeTablaPeça, "CIP", "'" + p.CIP+ "'");

                if (dt.Rows.Count > 0)
                {
                    Atualizar();
                }
                else { MessageBox.Show("Impossivel atualizar: Peça não existe na base de dados!"); return; }
            }
            else
            {
                MessageBox.Show("Operação Cancelada"); return;
            }
        }

        private void Salvar()
        {
            CrearPeça();
            AssignarNomeTablaPeça();
            dt = Banco.ObterTodosOnde(NomeTablaPeça, "CIP", "'" + p.CIP + "'");

            try
            {
                if (dt.Rows.Count == 0)
                {
                    bool Salvou;
                    control = true;
                    linha = dgv_Peças.SelectedRows[0].Index;

                    Salvou = Banco.Salvar(NomeTablaPeça, " CIP, Código, NomeCIP, NomeFormal, Largura, Cumprimento, Altura, Diametro, Alto, Alto2, ValorEstampada, ValorProcesso, ValorSucata1, ValorSucata2 , CamadaMin, CamadaMax, Foto, Circular,MSE,ME,OM ", "'" + p.CIP + "', '" + p.Código + "', '" + p.NomeCIP + "', '" + p.NomeFormal + "', '" + p.Largura + "', '" + p.Cumprimento + "', '" + p.Altura + "', '" + p.Diametro + "', '" + p.Alto + "', '" + p.Alto2 + "', '" + p.ValorEstampada + "', '" + p.ValorProcesso + "', '" + p.ValorSucata1 + "', '" + p.ValorSucata2 + "', '" + p.CamadaMin + "', '" + p.CamadaMax + "', '" + p.Imagem + "', '" + p.Circular + "', '" + p.MassaSemEsmaltar + "', '" + p.MassaEsmaltada + "'");


                    if (Salvou)
                    {

                        dgv_Peças.DataSource = Banco.Procurar(NomeTablaPeça, "CIP, NomeCIP", "NomeCIP", "'" + cb_Filtro.Text + "%'", "CIP");

                        Restablecer();
                        MessageBox.Show("Dados Salvos");
                    }
                    else
                    {
                        Restablecer();
                        MessageBox.Show("Não Foi Possivel Salvar os Dados");
                    }

                }
                else { MessageBox.Show("Não e possivel Cadastrar, esta peça já existe!"); tb_NomeCIP.Focus(); }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show("Erro: hola" + ex.Message);
            }
        }
        private void SalvarDados()
        {

            DialogResult res = MessageBox.Show("Quer mesmo Salvar os dados?", "Quer Salvar?", MessageBoxButtons.YesNo);
            if (res == DialogResult.Yes)
            {
                if (SalvarArquivo())
                {
                    return;
                }

                if (tb_NomeFormal.Text != "")
                {
                    if (tb_NomeCIP.Text != "")
                    {
                        if (cb_Familia.SelectedIndex != 0)
                        {
                            if (cb_Modelo.SelectedIndex != 0)
                            {
                                   Salvar();
                            }
                            else { MessageBox.Show("Deve Escolher um Modelo!"); cb_Modelo.Focus(); }
                        }
                        else { MessageBox.Show("Deve Escolher uma Familia"); cb_Familia.Focus(); }
                    }
                    else { MessageBox.Show("Campo \"Nome CIP\" não pode estar vazio"); tb_NomeCIP.Focus(); }
                }
                else { MessageBox.Show("Campo \"Nome Formal\" não pode estar vazio"); tb_NomeFormal.Focus(); }

                btn_Atualizar.Text = "Atualizar";
                btn_Novo.Enabled = true;
                btn_Excluir.Enabled = true;
            }
            else
            {
                MessageBox.Show("Operação Cancelada"); return;
            }
        }

        private void Excluir()
        {
            AssignarNomeTablaPeça();
            bool Excluiu = Banco.Excluir(NomeTablaPeça, "CIP", "'" + IdSeleccionados + "'");



            if (Excluiu)
            {
                if (File.Exists(DestinoCompleto))
                {
                    File.Delete(DestinoCompleto);
                    MessageBox.Show("Arquivo Excluido");
                }
                else
                {
                    MessageBox.Show("Arquivo  não Excluido");
                }
                dgv_Peças.Rows.Remove(dgv_Peças.CurrentRow);
                Restablecer();
                MessageBox.Show("Peça Excluida");
            }
            else
            {
                MessageBox.Show("Não foi Possivel Excluir a Peça");
            }

        }
        private void ExcluirVarios(string tabla, string columna, string text, int valor, int tipo)
        {
            bool Excluiu = Banco.Excluir(tabla,columna, "'" + text + "'");

            if (Excluiu)
            {
                DialogResult res = MessageBox.Show("Foi Feita exlusão do "+tabla+" da base de Dados\n\n Deseja excluir Tambem TODAS as Peças Cadastradas COM O "+tabla+" que foi Excluido??\n\n Sera permanente Escolha bem", "Excluir Todas as Peças Tambem?", MessageBoxButtons.YesNo);
                if (res == DialogResult.Yes)
                {
                    AssignarNomeTablaPeça();
                    ExcluirVariosdeumMismoTipo(NomeTablaPeça, "*", "CIP", "'%." +valor + ".%'", "CIP", tipo, valor);
                    dgv_Peças.DataSource = Banco.Procurar(NomeTablaPeça, "CIP, NomeCIP", "NomeCIP", "'" + cb_Filtro.Text + "%'", "CIP");

                }


                DataTable dt3 = Banco.ObterTodos(tabla);
                cb_Modelo.DataSource = dt3;
                nud_Modelo.Maximum = dt3.Rows.Count - 1;
                Restablecer();
                MessageBox.Show("Dado Excluido");

            }
            else
            {
                MessageBox.Show("Operação Cancelada");
            }
        }
        #endregion;                 

        #region CAMBIOS DE SELECCIÓN  
        private void Circular_CheckedChanged(object sender, EventArgs e)
        {
            EstablecerSeECircularOuNão();
        }
        private void cb_Filtro_SelectedIndexChanged(object sender, EventArgs e)
        {
            Filtrar();
        }
        private void dgv_Peças_SelectionChanged_1(object sender, EventArgs e)
        {


            
            int contilinha = dgv_Peças.Rows.Count;           
            if (contilinha > 0)
            {
                int row = dgv_Peças.CurrentRow.Index;
                dgv_Peças.Rows[row].Cells[0].Selected = true;
                // dgv_Peças.FirstDisplayedScrollingRowIndex = row;
                if (control)
                {
                    dgv_Peças.Rows[linha].Cells[0].Selected = true;

                    dgv_Peças.CurrentCell = dgv_Peças[0, linha];
                    control = false;

                }
                try
                {
                    
                    AssignarCIP();

                   

                    int dgvIndex = dgv_Peças.CurrentRow.Index;
                    dgv_Peças.Rows[dgvIndex].Selected = true;
                    dgv_Peças.CurrentCell = dgv_Peças[0, dgvIndex];


                    btn_Atualizar.Text = "Atualizar";
                    btn_Excluir.Enabled = true;
                    btn_Novo.Enabled = true;
                    btn_C.Enabled = false;

                }
                catch (ArgumentOutOfRangeException ex)
                {
                    MessageBox.Show("Erro: " + ex.Message);

                }
            }



        }
        private void nud_Version_ValueChanged(object sender, EventArgs e)
        {
            ActualizarNomeNaBarraeID();
        }
        private void cb_Familia_SelectedValueChanged(object sender, EventArgs e)
        {
            //if (dgv_Peças.Rows.Count> 0) 
           // {                
                   nud_Familia.Value = cb_Familia.SelectedIndex;

            ActualizarNomeNaBarraeID();


          // }
        }

        private void cb_Modelo_SelectedValueChanged(object sender, EventArgs e)
        {
           // if (dgv_Peças.Rows.Count > 0)
           // { 
                  nud_Modelo.Value = cb_Modelo.SelectedIndex;
            ActualizarNomeNaBarraeID();
            //  }
        }

        private void cb_Caracteristica_SelectedValueChanged(object sender, EventArgs e)
        {
           
           // if (dgv_Peças.Rows.Count > 0)
           // {
                  nud_Caracteristica.Value = cb_Caracteristica.SelectedIndex;
            ActualizarNomeNaBarraeID();

            // }
        }

        private void nud_Familia_ValueChanged(object sender, EventArgs e)
        {
            if (nud_Familia.Value > nud_Familia.Maximum)
            {
                nud_Familia.Value = nud_Familia.Maximum;
            }
            if (nud_Familia.Value < nud_Familia.Minimum)
            {
                nud_Familia.Value = nud_Familia.Minimum;
            }
            cb_Familia.SelectedIndex = Convert.ToInt32(nud_Familia.Value);
            
           
        }

        private void nud_Modelo_ValueChanged(object sender, EventArgs e)
        {
            if (nud_Modelo.Value>nud_Modelo.Maximum)
            {
                nud_Modelo.Value = nud_Modelo.Maximum;
            }
            if (nud_Modelo.Value < nud_Modelo.Minimum)
            {
                nud_Modelo.Value = nud_Modelo.Minimum;
            }
           cb_Modelo.SelectedIndex = Convert.ToInt32(nud_Modelo.Value);
           
        }

        private void nud_Caracteristica_ValueChanged(object sender, EventArgs e)
        {
            if (nud_Caracteristica.Value > nud_Caracteristica.Maximum)
            {
                nud_Caracteristica.Value = nud_Caracteristica.Maximum;
            }
            if (nud_Caracteristica.Value < nud_Caracteristica.Minimum)
            {
                nud_Caracteristica.Value = nud_Caracteristica.Minimum;
            }

            cb_Caracteristica.SelectedIndex = Convert.ToInt32(nud_Caracteristica.Value);
            
        }
        private void nud_VAlorEstampada_ValueChanged(object sender, EventArgs e)
        {
            AssignarValorProcesso();
            nud_ValorSucateamento1.Value = nud_ValorProcesso.Value + nud_VAlorEstampada.Value;

         
        }

        private void nud_ValorProcesso_ValueChanged(object sender, EventArgs e)
        {
            nud_ValorSucateamento1.Value = nud_ValorProcesso.Value + nud_VAlorEstampada.Value;
        }

        private void nud_ValorSucateamento1_ValueChanged(object sender, EventArgs e)
        {
            nud_Sucateamento2.Value = (2 * nud_ValorProcesso.Value) + nud_VAlorEstampada.Value;
        }
        private void cb_Familia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_Familia.Text == "BASE")
            {
                nud_Diametro.Enabled = true;
                nud_Alto.Enabled = true;
            }
            else
            {
                nud_Diametro.Enabled = false;
                nud_Alto.Enabled = false;
            }
        }
        private void nud_Diametro_ValueChanged(object sender, EventArgs e)
        {

            AssignarValorProcesso();
        }

        private void Dx_ValueChanged(object sender, EventArgs e)
        {
            AssignarValorProcesso();
        }

        private void Dy_ValueChanged(object sender, EventArgs e)
        {
            AssignarValorProcesso();
        }

        private void Dz_ValueChanged(object sender, EventArgs e)
        {
            AssignarValorProcesso();
        }

        private void nud_Alto_ValueChanged(object sender, EventArgs e)
        {
            AssignarValorProcesso();
        }


        private void cb_Setor_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarNomeNaBarraeID();
        }

        #endregion;

        #region BOTONES CLICK
        private void btn_AtualizarTodas_Click(object sender, EventArgs e)
        {
            if (cb_Filtro.SelectedIndex > 0)
            {
                bool control1;
               
                string Columna2;
                int Columna;
                do
                {
                    control1 = true;
                    Columna2 = Interaction.InputBox("DIGITE O NÚMERO DO DADO QUE DESEJA ATUALIZAR:\n\nDIMENÇÕES:\n\n1.-Largura     2.-Cumprimento     3.-Altura\n4.-Diametro     5.-Alto     6.-Alto2\n\nCAMADAS:\n\n7.-Camada min     8.-Camada Max\n\nCUSTOS:\n\n9.-Valor Estampada     10.-Valor Processo     11.-Valor Sucata1     12.-ValorSucata2\n\nOUTROS:\n\n13.-Circular\n\n", "Dado a Alterar");


                    if (Columna2 != "")
                    {

                        if (Columna2.Length > 2)
                        {
                            DialogResult res = MessageBox.Show("VALOR INVALIDO:\n\n  Insiriu mais de dois carateres.\n Só pode inserir Valores numéricos entre 1 e 13\n\n TENTAR NOVAMENTE? ", "DADO INVALIDO", MessageBoxButtons.YesNo);
                            if (res == DialogResult.Yes)
                            {
                                control1 = false;
                            }
                            else
                            {
                                MessageBox.Show("Operação Cancelada");
                                return;
                            }
                        }
                        else
                        {
                            if (Columna2.Length == 1)
                            {
                                if (char.IsDigit(char.Parse(Columna2)))
                                {
                                    control1 = true;
                                }
                                else
                                {
                                    DialogResult res = MessageBox.Show("VALOR INVALIDO:\n\n Só pode inserir Valores numéricos entre 1 e 13\n\n TENTAR NOVAMENTE? ", "DADO INVALIDO", MessageBoxButtons.YesNo);
                                    if (res == DialogResult.Yes)
                                    {
                                        control1 = false;
                                    }
                                    else
                                    {
                                        MessageBox.Show("Operação Cancelada");
                                        return;
                                    }
                                }
                            }
                            else
                            {
                                if (Columna2.Length == 2)
                                {
                                    if (char.IsDigit(char.Parse(Columna2.Substring(0, 1))) && char.IsDigit(char.Parse(Columna2.Substring(1, 1))))
                                    {
                                        if (int.Parse(Columna2) > 13)
                                        {
                                            DialogResult res = MessageBox.Show("VALOR INVALIDO:\n\n Só pode inserir Valores numéricos entre 1 e 13\n\n TENTAR NOVAMENTE? ", "DADO INVALIDO", MessageBoxButtons.YesNo);
                                            if (res == DialogResult.Yes)
                                            {
                                                control1 = false;
                                            }
                                            else
                                            {
                                                MessageBox.Show("Operação Cancelada");
                                                return;
                                            }

                                        }
                                        else
                                        {
                                            control1 = true;
                                        }

                                    }
                                    else
                                    {
                                        DialogResult res = MessageBox.Show("VALOR INVALIDO:\n\n Só pode inserir Valores numéricos entre 1 e 13\n\n TENTAR NOVAMENTE? ", "DADO INVALIDO", MessageBoxButtons.YesNo);
                                        if (res == DialogResult.Yes)
                                        {
                                            control1 = false;
                                        }
                                        else
                                        {
                                            MessageBox.Show("Operação Cancelada");
                                            return;
                                        }
                                    }
                                }
                                
                            }


                        }

                    }
                    else
                    {
                        DialogResult res = MessageBox.Show("Deseja Cancelar a Operação?", "CANCELAR", MessageBoxButtons.YesNo);
                        if (res == DialogResult.No)
                        {
                            control1 = false;
                        }
                        else
                        {
                            MessageBox.Show("Operação Cancelada");
                            return;
                        }


                    }
                }
                while (control1 == false);

                string Valor;  
                Columna = int.Parse(Columna2);

                switch (Columna)
                {                   
                    case 1:
                        Valor = AssignarValorAtualizar(Columna, "Largura");
                        if (Valor!=string.Empty)
                        {
                            AtualizarVariosdeumMismoTipo(cb_Familia.SelectedIndex, "Largura", Valor, cb_Familia.Text);
                            Restablecer();
                        }
                        else
                        {
                            MessageBox.Show("Operação Cancelada");
                        }
                        break;

                    case 2:
                        Valor = AssignarValorAtualizar(Columna, "Cumprimento");
                        if (Valor != string.Empty)
                        {
                            AtualizarVariosdeumMismoTipo(cb_Familia.SelectedIndex, "Cumprimento", Valor, cb_Familia.Text);
                            Restablecer();
                        }
                        else
                        {
                            MessageBox.Show("Operação Cancelada");
                        }                      
                        break;

                    case 3:
                        Valor = AssignarValorAtualizar(Columna, "Altura");
                        if (Valor != string.Empty)
                        {
                            AtualizarVariosdeumMismoTipo(cb_Familia.SelectedIndex, "Altura", Valor, cb_Familia.Text);
                            Restablecer();
                        }
                        else
                        {
                            MessageBox.Show("Operação Cancelada");
                        }
                       
                        break;

                    case 4:
                        Valor = AssignarValorAtualizar(Columna, "Diametro");
                        if (Valor != string.Empty)
                        {
                            AtualizarVariosdeumMismoTipo(cb_Familia.SelectedIndex, "Diametro", Valor, cb_Familia.Text);
                            Restablecer();
                        }
                        else
                        {
                            MessageBox.Show("Operação Cancelada");
                        }                      
                        break;

                    case 5:
                        Valor = AssignarValorAtualizar(Columna, "Alto");
                        if (Valor != string.Empty)
                        {
                            AtualizarVariosdeumMismoTipo(cb_Familia.SelectedIndex, "Alto", Valor, cb_Familia.Text);
                            Restablecer();
                        }
                        else
                        {
                            MessageBox.Show("Operação Cancelada");
                        }                     
                        break;

                    case 6:
                        Valor = AssignarValorAtualizar(Columna, "Alto2");
                        if (Valor != string.Empty)
                        {
                            AtualizarVariosdeumMismoTipo(cb_Familia.SelectedIndex, "Alto2", Valor, cb_Familia.Text);
                            Restablecer();
                        }
                        else
                        {
                            MessageBox.Show("Operação Cancelada");
                        }                      
                        break;

                    case 7:
                        Valor = AssignarValorAtualizar(Columna, "CamadaMin");
                        if (Valor != string.Empty)
                        {
                            AtualizarVariosdeumMismoTipo(cb_Familia.SelectedIndex, "CamadaMin", Valor, cb_Familia.Text);
                            Restablecer();
                        }
                        else
                        {
                            MessageBox.Show("Operação Cancelada");
                        }                     
                        break;

                    case 8:
                        Valor = AssignarValorAtualizar(Columna, "CamadaMax");
                        if (Valor != string.Empty)
                        {
                            AtualizarVariosdeumMismoTipo(cb_Familia.SelectedIndex, "CamadaMax", Valor, cb_Familia.Text);
                            Restablecer();
                        }
                        else
                        {
                            MessageBox.Show("Operação Cancelada");
                        }                     
                        break;

                    case 9:
                        Valor = AssignarValorAtualizar(Columna, "ValorEstampada");
                        if (Valor != string.Empty)
                        {
                            AtualizarVariosdeumMismoTipo(cb_Familia.SelectedIndex, "ValorEstampada", Valor, cb_Familia.Text);
                            Restablecer();
                        }
                        else
                        {
                            MessageBox.Show("Operação Cancelada");
                        }                   
                        break;

                    case 10:
                        Valor = AssignarValorAtualizar(Columna, "ValorProcesso");
                        if (Valor != string.Empty)
                        {
                            AtualizarVariosdeumMismoTipo(cb_Familia.SelectedIndex, "Valor Processo", Valor, cb_Familia.Text);
                            Restablecer();
                        }
                        else
                        {
                            MessageBox.Show("Operação Cancelada");
                        }                       
                        break;

                    case 11:
                        Valor = AssignarValorAtualizar(Columna, "ValorSucata1");
                        if (Valor != string.Empty)
                        {
                            AtualizarVariosdeumMismoTipo(cb_Familia.SelectedIndex, "Valor Sucata 1", Valor, cb_Familia.Text);
                            Restablecer();
                        }
                        else
                        {
                            MessageBox.Show("Operação Cancelada");
                        }                      
                        break;

                    case 12:
                        Valor = AssignarValorAtualizar(Columna, "ValorSucata2");
                        if (Valor != string.Empty)
                        {
                            AtualizarVariosdeumMismoTipo(cb_Familia.SelectedIndex, "Valor Sucata 2", Valor, cb_Familia.Text);
                            Restablecer();
                        }
                        else
                        {
                            MessageBox.Show("Operação Cancelada");
                        }                      
                        break;

                    case 13:                        
                        Valor = AssignarValorAtualizar(Columna, "Circular");
                        if (Valor != string.Empty)
                        {
                            if (Valor == "1")
                            {
                                Valor = "SIM";
                            }
                            else
                            {
                                Valor = "NÃO";
                            }

                            AtualizarVariosdeumMismoTipo(cb_Familia.SelectedIndex, "Circular", Valor, cb_Familia.Text);
                            Restablecer();
                        }
                        else
                        {
                            MessageBox.Show("Operação Cancelada");
                        } 
                        break;
                }

            }
        }
        private void btn_teste_Click(object sender, EventArgs e)
        {
            cb_Filtro.DataSource = Banco.ObterTodos("Familias", "*", "IdFamilia");
            btn_teste.Visible = false;
        }
        private void btn_AddFoto_Click(object sender, EventArgs e)
        {
           
            
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    OrigemCompleto = openFileDialog1.FileName;
                    foto = openFileDialog1.SafeFileName;
                    DestinoCompleto = PastaDestino+ foto;

                }
                else
                {
                    return;
                }


                if (File.Exists(DestinoCompleto))
                {
                    if (MessageBox.Show("Existe um arquivo com o mesmo nome ja salvado, deseja substituir?", "Sustituir", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        btn_AddFoto.Text = "Trocar Foto";
                        pb_Foto.ImageLocation = OrigemCompleto;
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
                    pb_Foto.ImageLocation = OrigemCompleto;               

                }

        }

        private void btn_Novo_Click_1(object sender, EventArgs e)
        {
            btn_Atualizar.Text = "Salvar";
            btn_Excluir.Enabled = false;
            tb_NomeCIP.Text = "";
            tb_NomeFormal.Text = "";           
            tb_NomeFormal.ReadOnly = false;
            tb_Código.Text = "";
            tb_Código.ReadOnly = false;
            btn_Novo.Enabled = false;
            btn_C.Enabled = true;
            pb_Foto.ImageLocation = "";
            btn_AddFoto.Text = "Add Foto";
            btn_ExcluirFoto.Enabled = false;
            nud_Familia.Value = 0;
            nud_Caracteristica.Value = 0;
            nud_Modelo.Value = 0;
            nud_Version.Value = 0;
            nud_MSE.Value = 0;
            nud_ME.Value = 0;
            nud_OM.Value = 0;
            tb_Id.Text = "";
            btn_AddCaracteristica.Visible= true;
            btn_AddFamilia.Visible = true;
            btn_AddModelo.Visible = true;
            btn_ExcluirCaracteristica.Visible = true;
            btn_ExcluirFamilia.Visible = true;
            btn_ExcluirModelo.Visible = true;
            cb_Familia.Enabled = true;
            cb_Modelo.Enabled = true;
            cb_Caracteristica.Enabled = true;
            nud_Familia.Enabled = true;
            nud_Modelo.Enabled = true;
            nud_Caracteristica.Enabled = true;
            nud_Version.Enabled = true;
            btn_AtualizarFamilia.Visible = true;
            btn_AtualizarModelo.Visible = true;
            btn_AtualizarCaracteristica.Visible = true;
            btn_Imprimir.Enabled = false;
        }

        private void btn_Atualizar_Click_1(object sender, EventArgs e)
        {
            if (btn_Atualizar.Text == "Atualizar")
            {
                AtualizarDados();
            }
            else
            {
                SalvarDados();
            }

           
        }

        private void btn_Excluir_Click_1(object sender, EventArgs e)
        {
            Excluir();         
        }

        private void btn_C_Click_1(object sender, EventArgs e)
        {
            Restablecer();
        }

        private void Btn_Voltar_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Btn_Sair_Click_1(object sender, EventArgs e)
        {
            Globais.Sair();
        }

        private void btn_ExcluirFoto_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja Excluir Permanentemente a Foto ubicada em:\n" + DestinoCompleto + " ?", "Excluir?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                AssignarNomeTablaPeça();
                bool Excluiu;
                if (File.Exists(DestinoCompleto))
                {
                    File.Delete(DestinoCompleto);
                    MessageBox.Show("Arquivo Excluido");
                }


                btn_AddFoto.Text = "Add Foto";

                string bla = string.Empty;
                
                    Excluiu = Banco.Atualizar(NomeTablaPeça, "Foto= '" + bla + "'", "Foto", "'" + pb_Foto.ImageLocation + "'");
                
               
                if (Excluiu)
                {
                    Restablecer();
                    pb_Foto.ImageLocation = DestinoCompleto;
                    btn_AddFoto.Text = "Add Foto";
                    btn_ExcluirFoto.Enabled = false;
                    MessageBox.Show("Dados Atualizados");
                }
                else
                {
                    Restablecer();
                    MessageBox.Show("Exclusão cancelada");
                    return;
                }
            }
        }

        private void btn_AddFamilia_Click(object sender, EventArgs e)
        {
            string FamiliaNova = Interaction.InputBox("Escreva o Nome da Nova Familia", "Nova Familia");
            
           
            if (FamiliaNova != string.Empty)
            {
              
                DataTable dt2 = Banco.ObterTodosOnde("Familias", "Familia", "'" + FamiliaNova.ToUpper() + "'");

                if (dt2.Rows.Count > 1)
                {
                    MessageBox.Show("Operação Cancelada: Familia já existe na Base de Dados");

                    return;
                }

                bool Salvou;
                DataTable dt = Banco.ObterTodos("Familias");
            
                int Cuenta = EstabelecerOValorDo_ID(dt, "IdFamilia");              
                Salvou = Banco.Salvar("Familias", " IdFamilia, Familia", Cuenta + ", '" + FamiliaNova.ToUpper() + "'");


                if (Salvou)
                {
                    MessageBox.Show("Dado Salvo");
                    DataTable dt3= Banco.ObterTodos("Familias", "*", "IdFamilia");
                    cb_Familia.DataSource = dt3;                  
                    nud_Familia.Maximum = dt3.Rows.Count - 1;
                    btn_teste.Visible = true;
                }
            }
            else
            {
                MessageBox.Show("Campo Vazio, Operação  Cancelada");
            }

        }

        private void btn_ExcluirFamilia_Click(object sender, EventArgs e)
        {
            if (cb_Familia.SelectedIndex > 0)
            {
                ExcluirVarios("Familias", "Familia", cb_Familia.Text, cb_Familia.SelectedIndex, 0);
                
            }
            else
            {
                MessageBox.Show("Deve Escolher uma Familia na lista");
            }

        }

        private void btn_AddModelo_Click(object sender, EventArgs e)
        {
            string FamiliaNova = Interaction.InputBox("Escreva o Nome do Novo Modelo", "Novo Modelo");
            
           
            if (FamiliaNova != "")
            {
                
                DataTable dt2 = Banco.ObterTodosOnde("Modelos", "Modelo", "'" + FamiliaNova.ToUpper() + "'");
                if (dt2.Rows.Count > 1)
                {
                    MessageBox.Show("Operação Cancelada:  Modelo já existe na Base de Dados");

                    return;
                }
                bool Salvou;
                DataTable dt = Banco.ObterTodos("Modelos");

                int Cuenta = EstabelecerOValorDo_ID(dt, "IdModelos");

                Salvou = Banco.Salvar("Modelos", " IdModelos, Modelo", Cuenta + ", '" + FamiliaNova.ToUpper() + "'");


                if (Salvou)
                {
                    
                    DataTable dt3 = Banco.ObterTodos("Modelos", "*", "IdModelos");
                    cb_Modelo.DataSource = dt3;
                    nud_Modelo.Maximum = dt3.Rows.Count - 1;
                    MessageBox.Show("Dado Salvo");

                }
            }
            else
            {
                MessageBox.Show("Campo Vazio, Operação  Cancelada");
            }
        }

        private void btn_ExcluirModelo_Click(object sender, EventArgs e)
        {
            if (cb_Modelo.SelectedIndex > 0)
            {
                ExcluirVarios("Modelos", "Modelo", cb_Modelo.Text, cb_Modelo.SelectedIndex,1);
                
            }
            else
            {
                MessageBox.Show("Deve Escolher um Modelo na lista");
            }
        }

        private void btn_AddCaracteristica_Click(object sender, EventArgs e)
        {
            string FamiliaNova = Interaction.InputBox("Escreva o Nome da Nova Caracteristica", "Nova Caracteristica");
            
           

            if (FamiliaNova != "")
            {
                
                DataTable dt2 = Banco.ObterTodosOnde("Caracteristicas", "Caracteristica", "'" + FamiliaNova.ToUpper() + "'");
                if (dt2.Rows.Count > 1)
                {
                    MessageBox.Show("Operação Cancelada:  Caracteristica já existe na Base de Dados");

                    return;
                }

                DataTable dt = Banco.ObterTodos("Caracteristicas");

                int Cuenta = EstabelecerOValorDo_ID(dt, "IdCaracteristica");

                bool Salvou = Banco.Salvar("Caracteristicas", " IdCaracteristica, Caracteristica", Cuenta + ", '" + FamiliaNova.ToUpper() + "'");


                if (Salvou)
                {
                    DataTable dt3 = Banco.ObterTodos("Caracteristicas", "*", "IdCaracteristica");
                    cb_Caracteristica.DataSource = dt3;
                    nud_Caracteristica.Maximum = dt3.Rows.Count-1;
                    MessageBox.Show("Dado Salvo");
                }
            }
            else
            {
                MessageBox.Show("Campo Vazio, Operação  Cancelada");
            }
        }

        private void btn_ExcluirCaracteristica_Click(object sender, EventArgs e)
        {
            if (cb_Caracteristica.SelectedIndex > 0)
            {
                ExcluirVarios("Caracteristicas", "Caracteristica", cb_Caracteristica.Text, cb_Caracteristica.SelectedIndex,2);
              
            }
            else
            {
                MessageBox.Show("Deve Escolher uma Caracteristica da lista");
            }
        }       

        private void btn_AtualizarFamilia_Click(object sender, EventArgs e)
        {
            if (cb_Familia.SelectedIndex > 0)
            {
                string FamiliaNova = Interaction.InputBox("Escreva o NOVO NOME  da FAMILIA", "ATUALIZAR FAMILIA");


                if (FamiliaNova != "")
                {
                    bool atualizou2;
                   
                    
                    
                      bool Atualizou = Banco.Atualizar("Familias", "Familia='" + FamiliaNova.ToUpper() + "'", "IdFamilia", "" + cb_Familia.SelectedIndex);

                    
                    if (Atualizou)
                    {
                        MessageBox.Show("Foi atualizada a FAMILIA na base de dados, agora seran atualizados Todos os Dados Relacionados ");
                        AssignarNomeTablaPeça();
                            atualizou2 = AtualizarVariosNomesdeumMismoTipodePeça(NomeTablaPeça, "*", "CIP", "'" + cb_Familia.SelectedIndex + ".%'", "CIP", 0, cb_Familia.SelectedIndex, FamiliaNova.ToUpper());
                      
                        if (atualizou2)
                        {
                            AssignarNomeTablaPeça();
                            dgv_Peças.DataSource = Banco.Procurar(NomeTablaPeça, "CIP, NomeCIP", "NomeCIP", "'" + cb_Filtro.Text + "%'", "CIP");
                        }


                        DataTable dt3 = Banco.ObterTodos("Familias", "*", "IdFamilia");
                        cb_Familia.DataSource = dt3;
                        nud_Familia.Maximum = dt3.Rows.Count - 1;
                        btn_teste.Visible = true;
                        Restablecer();

                    }
                }
                else
                {
                    MessageBox.Show("Campo Vazio, Operação  Cancelada");
                }
            }
            else
            {
                MessageBox.Show("Deve Escolher uma Familia da lista");
            }
        }

        private void btn_AtualizarModelo_Click(object sender, EventArgs e)
        {
            if (cb_Modelo.SelectedIndex > 0)
            {
                string FamiliaNova = Interaction.InputBox("Escreva o NOVO NOME  do MODELO", "ATUALIZAR MODELO");


                if (FamiliaNova != "")
                {
                  
                    bool Atualizou = Banco.Atualizar("Modelos", "Modelo= '" + FamiliaNova.ToUpper() + "'", "IdModelos", "" + cb_Modelo.SelectedIndex);
                    if (Atualizou)
                    {
                        bool atualizou2;
                        MessageBox.Show("Foi atualizado o MODELO na base de dados, agora seran atualizados Todos os Dados Relacionados ");
                        AssignarNomeTablaPeça();
                            atualizou2 = AtualizarVariosNomesdeumMismoTipodePeça(NomeTablaPeça, "*", "CIP", "'%." + cb_Modelo.SelectedIndex + ".%'", "CIP", 1, cb_Modelo.SelectedIndex, FamiliaNova.ToUpper());
                                                
                        if (atualizou2)
                        {
                            AssignarNomeTablaPeça();
                            dgv_Peças.DataSource = Banco.Procurar(NomeTablaPeça, "CIP, NomeCIP", "NomeCIP", "'" + cb_Filtro.Text + "%'", "CIP");                            
                            
                        }

                        DataTable dt3 = Banco.ObterTodos("Modelos", "*", "IdModelos");
                        cb_Modelo.DataSource = dt3;
                        nud_Modelo.Maximum = dt3.Rows.Count - 1;
                        Restablecer();
                    }
                }
                else
                {
                    MessageBox.Show("Campo Vazio, Operação  Cancelada");
                }
            }
            else
            {
                MessageBox.Show("Deve Escolher um MODELO da lista");
            }
        
        }
    

        private void btn_AtualizarCaracteristica_Click(object sender, EventArgs e)
        {
            if (cb_Caracteristica.SelectedIndex > 0)
            {
                string FamiliaNova = Interaction.InputBox("Escreva o NOVO NOME  da CARACTERISTICA", "ATUALIZAR Caracteristica");


                if (FamiliaNova != "")
                {
                    bool atualizou2;
                    bool Atualizou = Banco.Atualizar("Caracteristicas", "Caracteristica= '" + FamiliaNova.ToUpper() + "'", "IdCaracteristica", "" + cb_Caracteristica.SelectedIndex);
                    if (Atualizou)
                    {
                        MessageBox.Show("Foi atualizada a CARACTERISTICA na base de dados, agora seran atualizados Todos os Dados Relacionados ");

                        AssignarNomeTablaPeça();
                            atualizou2 = AtualizarVariosNomesdeumMismoTipodePeça(NomeTablaPeça, "*", "CIP", "'%." + cb_Caracteristica.SelectedIndex + ".%'", "CIP", 2, cb_Caracteristica.SelectedIndex, FamiliaNova.ToUpper());
                          
                        if (atualizou2)
                        {                           
                                dgv_Peças.DataSource = Banco.Procurar(NomeTablaPeça, "CIP, NomeCIP", "NomeCIP", "'" + cb_Filtro.Text + "%'", "CIP");                            
                        }

                        DataTable dt3 = Banco.ObterTodos("Caracteristicas", "*", "IdCaracteristica");
                        cb_Caracteristica.DataSource = dt3;
                        nud_Caracteristica.Maximum = dt3.Rows.Count - 1;
                        Restablecer();

                    }
                }
                else
                {
                    MessageBox.Show("Campo Vazio, Operação  Cancelada");
                }
            }
            else
            {
                MessageBox.Show("Deve Escolher uma Caracteristica da lista");
            }
        }
        private void bnt_Imprimir_Click(object sender, EventArgs e)
        {
            Relatorios.F_RelatorioEspecificaçõesPeças f_Relatorio = new Relatorios.F_RelatorioEspecificaçõesPeças(PreencherDataTableParaRelatorioPeça(), DestinoCompleto, 1);
            Globais.Abreform(1, f_Relatorio);
        }
        private void btn_Imprimir2_Click(object sender, EventArgs e)
        {
            AssignarNomeTablaPeça();
                DataTable dt4 = Banco.Procurar(NomeTablaPeça, "*", "NomeCIP", "'" + cb_Filtro.Text + "%'", "CIP");            
           
            Relatorios.F_RelatorioEspecificaçõesPeças f_Relatorio2 = new Relatorios.F_RelatorioEspecificaçõesPeças(dt4, DestinoCompleto, 2);
            Globais.Abreform(1, f_Relatorio2);
        }

        #endregion;



        private void dgv_Peças_DataError_1(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;

        }

    }
}