using System;
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
    public partial class F_CalculoDeCMAA : Form
    {
        F_CadastroEControleDeContenedores t = new F_CadastroEControleDeContenedores();
        string cIP;
        int Familia;
        
        int por;
        public F_CalculoDeCMAA(F_CadastroEControleDeContenedores f, string CIP, int Familia)
        {
            InitializeComponent();

            t = f;
            cIP = CIP;
            this.Familia = Familia;
            gb_Compartimentos.Enabled = false;
        }
        bool cargou = false;
        DataTable dt = new DataTable();
        int Cont = 0;

        decimal Rx;
        decimal Ry;
        decimal Qp;
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
        decimal Qpv = 0;
        Capacidade c = new Capacidade();
        decimal CI;
        decimal Vc;
        decimal Vp;
        int[] IndicedePontos = new int[5];
        int[] IndiceFMCV = new int[5];
        string CIPc = "";
        string IdSeleccionados;
       
        int n = 2;
        private void F_CalculoDeCMAA_Load(object sender, EventArgs e)
        {
            #region NomeCIP
            //Popular combobox Nome CIP
            cb_Peça.Items.Clear();
            dt = Banco.ObterTodos("peças", "*", "CIP");
            cb_Peça.DataSource = dt;
            cb_Peça.DisplayMember = "NomeCIP";
            cb_Peça.ValueMember = "CIP";
            #endregion;

            #region Area de ubicação
            Dictionary<string, string> Area = new Dictionary<string, string>();
            Area.Add("0", "");
            Area.Add("D", "DESCARGA");
            Area.Add("C", "CARGA");
            Area.Add("R", "REFORMA");

            cb_Area.Items.Clear();
            cb_Area.DataSource = new BindingSource(Area, null);
            cb_Area.DisplayMember = "Value";
            cb_Area.ValueMember = "key";


            #endregion;       

            #region TIPO DE CONTENEDOR
            Dictionary<string, string> Tipo = new Dictionary<string, string>();
            Tipo.Add("0", "");
            Tipo.Add("C", "CARRINHO");
            Tipo.Add("X", "CAIXA");
            Tipo.Add("P", "PALETE");

            cb_Tipo.Items.Clear();
            cb_Tipo.DataSource = new BindingSource(Tipo, null);
            cb_Tipo.DisplayMember = "Value";
            cb_Tipo.ValueMember = "key";


            #endregion;

            #region Familia
            // populando ComboBox Familia
            cb_Familia.Items.Clear();
            cb_Familia.DataSource = Banco.ObterTodos("Familias", "*", "IdFamilia");
            cb_Familia.DisplayMember = "Familia";
            cb_Familia.ValueMember = "IdFamilia";
            #endregion;

            #region Popular Dgv
            dt = Banco.Procurar("CadastroDeCapacidadeGeral", "CICG,Capacidade", "CICG", "'%'", "CICG");
            dgv_CadastroGeralCapacidade.DataSource = dt;
            cb_Familia.SelectedIndex = Familia;
            cb_Peça.SelectedValue = cIP;
            #endregion;
            cargou = true;

        }


        #region PROCEDIMENTOS
        private void ZerarValores()
        {
            nud_Qdx.Value = 0;
            nud_Qdy.Value = 0;
            nud_Qdz.Value = 0;
            nud_Qpx.Value = 0;
            nud_Qpy.Value = 0;
            nud_Qpz.Value = 0;
            nudPz.Value = 0;

            nud_CI.Value = Math.Floor((nud_Cz.Value * nud_Cy.Value * nud_Cx.Value) * 0.001m);
            nud_NiveisCheios.Value = 0;
            nud_NiveisTotais.Value = 0;
            nud_CMAA.Value = Math.Floor((nud_Cz.Value * nud_Cy.Value * nud_Cx.Value) * 0.001m);
            nud_IOE.Value = 1;


        }
        private void AssignarDadosaosControles()
        {
            cb_Familia.SelectedIndex = int.Parse(c.Familia);
            cb_Peça.SelectedValue = c.CIP;
            Ch_Pares.Checked = c.Pares;
            Ch_CPre.Checked = c.Cpre;

            nud_Padron.Value = c.Padrão;
            tb_CICG.Text = c.CICG;

            nud_IOE.Value = c.IOE;
            nud_CMAA.Value = c.Capacidad;
            nud_Compartimentos.Value = c.Compartimentos;
            nud_CI.Value = c.CI;
            if (c.Pares)
            {
                nud_CPre.Value = nud_Compartimentos.Value * 2;
            }
            else
            {
                nud_CPre.Value = nud_Compartimentos.Value;
            }

        }
        private void AtualizarSelección()
        {
            if (dgv_CadastroGeralCapacidade.Rows.Count > 0 && dgv_CadastroGeralCapacidade.CurrentRow.Selected)
            {
                DeterminarQuienAtivo();
                IdSeleccionados = dgv_CadastroGeralCapacidade.Rows[dgv_CadastroGeralCapacidade.SelectedRows[0].Index].Cells[0].Value.ToString();

                dt = Banco.Procurar("CadastroDeCapacidadeGeral", "*", "CICG", "'%" + IdSeleccionados + "%'", "CICG");

                if (dt.Rows.Count > 0)
                {
                    CrearCapacidade(dt);
                    AssignarDadosaosControles();

                }
            }


        }
        private void Calcular()
        {
            if (!Ch_CPre.Checked)
            {
                if (Ch_Pares.Checked)
                {
                    n = 2;
                }
                else
                {
                    n = 1;
                }

                if (Ch_Circular.Checked)
                {

                    decimal x;
                    if (nud_Padron.Value == 1)
                    {
                        n = 2;
                        x = nud_Alto.Value;
                    }
                    else
                    {
                        n = 1;
                        x = nud_Alto2.Value;
                    }
                    if (nud_Diametro.Value == 0 || nud_Alto.Value == 0 || nud_Alto2.Value == 0)
                    {
                        nud_NPf.Value = 0;
                        nud_Nf.Value = 0;
                        return;
                    }

                    #region Columnasyfilas

                    Rx = Math.Floor(nud_Cx.Value / nud_Diametro.Value);

                    Ry = Math.Floor(nud_Cy.Value / nud_Diametro.Value);
                    Qp = Rx * Ry;

                    Ao = Convert.ToDecimal(Math.PI * Math.Pow(Convert.ToDouble(nud_Diametro.Value), 2) / 4) * Qp;
                    At = nud_Cx.Value * nud_Cy.Value;
                    Ar = At - Ao;
                    Qpv = Math.Floor(Convert.ToDecimal(Math.Sqrt(Convert.ToDouble(Ar))) / nud_Diametro.Value);
                    nud_NPf.Value = Qp + Qpv;
                    nud_Nf.Value = nud_Cz.Value / x;
                    nud_CI.Value = nud_NPf.Value * nud_Nf.Value * n;
                    nud_CMAA.Value = nud_NPf.Value * nud_Nf.Value * n;

                    if (CI > 0)
                    {
                        nud_IOE.Value = (nud_CMAA.Value * 100) / CI;
                        nud_CI.Value = CI;
                    }
                    else
                    {
                        nud_IOE.Value = 0;
                    }

                    #endregion;
                }
                else
                {

                    if (nud_Padron.Value == 1)
                    {
                        if ((nud_Px.Value == 0 || nud_Pz.Value == 0 || nud_Py.Value == 0))
                        {

                            return;
                        }


                        if (nud_Cy.Value <= 0 || nud_Cx.Value <= 0 || nud_Cz.Value <= 0)
                        {
                            return;
                        }
                        Q1 = Math.Floor(nud_Cx.Value / nud_Py.Value);
                        Qfh = Math.Floor(nud_Cy.Value / nud_Px.Value);
                        Qfv = Math.Floor(nud_Cz.Value / nud_Pz.Value);


                        Qfh2 = Math.Floor(nud_Cx.Value / nud_Px.Value);

                        Evh = nud_Cy.Value - (nud_Px.Value * Qfh);
                        Qvh = Evh / nud_Py.Value;

                        Evv = nud_Cz.Value - (nud_Pz.Value * Qfv);
                        Qvv = Evv / nud_Py.Value;

                        Vc = nud_Cz.Value * nud_Cy.Value * nud_Cx.Value;
                        Vp = nud_Px.Value * nud_Py.Value * nud_Pz.Value;
                        CI = (int)Math.Floor(Vc / Vp) * n;

                        nud_CMAA.Value = ((Q1 * Qfh * Qfv) + (Qvh * Qfh2 * Qfv) + (Qvv * Qfh2 * Math.Floor(nud_Cy.Value / nud_Pz.Value))) * n;



                        nud_Qpx.Value = Q1;
                        nud_Qpy.Value = Qvh;
                        nud_Qpz.Value = Qvv;
                        nud_Qdx.Value = 0;
                        nud_Qdy.Value = 0;
                        nud_Qdz.Value = 0;
                        nud_Fh.Value = Qfh;
                        nud_Fv.Value = Qfv;
                        if (Qvv > 0)
                        {
                            nudPz.Value = Qfh2 * Math.Floor(nud_Cy.Value / nud_Pz.Value);//columnas      
                        }
                        else
                        {
                            nudPz.Value = 0;//columnas
                        }
                        if (CI > 0)
                        {
                            nud_IOE.Value = (nud_CMAA.Value * 100) / CI;
                            nud_CI.Value = CI;
                        }
                        else
                        {
                            nud_IOE.Value = 0;
                        }



                        nud_NiveisCheios.Value = Qfv;
                        if (Evv > 0)
                        {
                            nud_NiveisTotais.Value = Qfv + 1;
                        }
                        else
                        {
                            nud_NiveisTotais.Value = Qfv;
                        }




                    }
                    else
                    {
                        if (nud_Padron.Value == 2)
                        {

                            if (nud_Px.Value == 0 || nud_Pz.Value == 0 || nud_Py.Value == 0)
                            {
                                ZerarValores();
                                return;
                            }
                            gb_Compartimentos.Enabled = false;

                            if (nud_Cy.Value <= 0 || nud_Cx.Value <= 0 || nud_Cz.Value <= 0)
                            {
                                return;
                            }
                            Q1 = Math.Floor(nud_Cx.Value / nud_Py.Value);
                            Qfh = Math.Floor(nud_Cy.Value / nud_Pz.Value);
                            Qfv = Math.Floor(nud_Cz.Value / nud_Px.Value);


                            Qfh2 = Math.Floor(nud_Cx.Value / nud_Pz.Value);

                            Evh = nud_Cy.Value - (nud_Pz.Value * Qfh);
                            Qvh = Evh / nud_Py.Value;

                            Evv = nud_Cz.Value - (nud_Px.Value * Qfv);
                            Qvv = Evv / nud_Py.Value;

                            Vc = nud_Cz.Value * nud_Cy.Value * nud_Cx.Value;
                            Vp = nud_Px.Value * nud_Py.Value * nud_Pz.Value;
                            CI = (int)Math.Floor(Vc / Vp) * n;

                            nud_CMAA.Value = ((Q1 * Qfh * Qfv) + (Qvh * Qfh2 * Qfv) + (Qvv * Qfh * Math.Floor(nud_Cx.Value / nud_Px.Value))) * n;


                            nud_Qpx.Value = 0;
                            nud_Qpy.Value = 0;
                            nud_Qpz.Value = 0;
                            nud_Qdx.Value = Q1;
                            nud_Qdy.Value = Qvh;
                            nud_Qdz.Value = Qvv;
                            nud_Fh.Value = Qfh;
                            nud_Fv.Value = Qfv;
                            if (Qvv > 0)
                            {
                                nudPz.Value = Qfh * Math.Floor(nud_Cx.Value / nud_Px.Value);//columnas
                            }
                            else
                            {
                                nudPz.Value = 0;//columnas
                            }

                            if (CI > 0)
                            {
                                nud_IOE.Value = (nud_CMAA.Value * 100) / CI;
                                nud_CI.Value = CI;
                            }
                            else
                            {
                                nud_IOE.Value = 0;
                            }



                            nud_NiveisCheios.Value = Qfv;
                            if (Evv > 0)
                            {
                                nud_NiveisTotais.Value = Qfv + 1;
                            }
                            else
                            {
                                nud_NiveisTotais.Value = Qfv;
                            }


                        }
                        // gb_Compartimentos.Enabled = true;
                        //nud_CMAA.Value = (nud_Cx.Value * nud_Compartimentos.Value) / nud_Pz.Value;
                    }
                }


            }
        }
        private void CrearCICG()
        {
            EstablecerValorIndicesFMCV(cb_Peça.SelectedValue.ToString());
            if (cb_Tipo.Text != "" && cb_Area.Text != "")
            {
                tb_CICG.Text = cb_Tipo.SelectedValue.ToString() + cb_Area.SelectedValue + "-" + CIPc;
            }


        }
        private void CrearCapacidade(DataTable d)
        {
            c.Familia = d.Rows[0].Field<string>("Familia");
            c.CIP = d.Rows[0].Field<string>("CIP");
            c.CICG = d.Rows[0].Field<string>("CICG");
            c.Cpre = d.Rows[0].Field<bool>("Cpre");
            c.Pares = d.Rows[0].Field<bool>("Pares");

            c.Padrão = decimal.Parse(d.Rows[0].Field<string>("Padrão"));

            c.IOE = decimal.Parse(d.Rows[0].Field<string>("IOE"));
            c.Capacidad = decimal.Parse(d.Rows[0].Field<string>("Capacidade"));
            c.Compartimentos = decimal.Parse(d.Rows[0].Field<string>("Compartimentos"));
            c.CI = decimal.Parse(d.Rows[0].Field<string>("CI"));
        }

        private void CriarCapacidade()
        {
            c.Cpre = Ch_CPre.Checked;
            c.Capacidad = nud_CMAA.Value;
            c.CICG = tb_CICG.Text;
            c.CIP = (string)cb_Peça.SelectedValue;
            c.CI = nud_CI.Value;
            c.IOE = nud_IOE.Value;
            c.Pares = Ch_Pares.Checked;
            c.Compartimentos = nud_Compartimentos.Value;
            c.Padrão = nud_Padron.Value;
            c.Familia = cb_Familia.SelectedIndex.ToString();
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

            CIPc = IndiceFMCV[0].ToString() + "." + IndiceFMCV[1] + "." + IndiceFMCV[2];
        }
        private void AssignarTipoQuandoFamiliaCambia()
        {
            if (IndiceFMCV[0] == 1)
            {
                if (cb_Area.SelectedIndex == 1 || cb_Area.SelectedIndex == 3)
                {
                    cb_Tipo.SelectedIndex = 1;
                }
                else
                {
                    cb_Tipo.SelectedIndex = 3;
                }

            }
            else
            {
                if (IndiceFMCV[0] == 2)
                {
                    if (cb_Area.SelectedIndex == 1 || cb_Area.SelectedIndex == 3)
                    {
                        cb_Tipo.SelectedIndex = 2;
                    }
                    else
                    {
                        cb_Tipo.SelectedIndex = 3;
                    }

                }
                else
                {
                    if (IndiceFMCV[0] == 3)
                    {
                        cb_Tipo.SelectedIndex = 3;
                    }
                    else
                    {
                        if (IndiceFMCV[0] == 4)
                        {
                            if (cb_Area.SelectedIndex == 1 || cb_Area.SelectedIndex == 3)
                            {
                                cb_Tipo.SelectedIndex = 2;
                            }
                            else
                            {
                                cb_Tipo.SelectedIndex = 3;
                            }

                        }
                        else
                        {
                            if (IndiceFMCV[0] == 5)
                            {
                                if (cb_Area.SelectedIndex == 1 || cb_Area.SelectedIndex == 3)
                                {
                                    cb_Tipo.SelectedIndex = 1;
                                }
                                else
                                {
                                    cb_Tipo.SelectedIndex = 3;
                                }

                            }
                            else
                            {
                                if (IndiceFMCV[0] == 6)
                                {
                                    if (cb_Area.SelectedIndex == 1 || cb_Area.SelectedIndex == 3)
                                    {
                                        cb_Tipo.SelectedIndex = 1;
                                    }
                                    else
                                    {
                                        cb_Tipo.SelectedIndex = 3;
                                    }

                                }
                                else
                                {
                                    if (IndiceFMCV[0] == 7)
                                    {
                                        if (cb_Area.SelectedIndex == 1 || cb_Area.SelectedIndex == 3)
                                        {
                                            cb_Tipo.SelectedIndex = 1;
                                        }
                                        else
                                        {
                                            cb_Tipo.SelectedIndex = 3;
                                        }

                                    }
                                    else
                                    {
                                        if (IndiceFMCV[0] == 8)
                                        {
                                            cb_Tipo.SelectedIndex = 3;
                                        }
                                        else
                                        {
                                            if (IndiceFMCV[0] == 9)
                                            {
                                                cb_Tipo.SelectedIndex = 2;
                                            }
                                            else
                                            {
                                                if (IndiceFMCV[0] == 10)
                                                {
                                                    cb_Tipo.SelectedIndex = 2;
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
        #endregion;


        private void nud_Px_ValueChanged(object sender, EventArgs e)

        {

            Calcular();
        }

        private void nud_Py_ValueChanged(object sender, EventArgs e)
        {
            Calcular();
        }

        private void nud_Pz_ValueChanged(object sender, EventArgs e)
        {
            Calcular();
        }

        private void nud_Cx_ValueChanged(object sender, EventArgs e)
        {
            Calcular();
        }

        private void nud_Cy_ValueChanged(object sender, EventArgs e)
        {
            Calcular();
        }

        private void nud_Cz_ValueChanged(object sender, EventArgs e)
        {
            Calcular();
        }

        private void nud_Padron_ValueChanged(object sender, EventArgs e)
        {
            if (nud_Padron.Value > 2)
            {
                nud_Padron.Value = 2;
            }
            else
            {
                if (nud_Padron.Value < 1)
                {
                    nud_Padron.Value = 1;
                }
            }
            Calcular();
        }

        private void btn_Voltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Sair_Click(object sender, EventArgs e)
        {
            Globais.Sair();
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            if (!Ch_CPre.Checked)
            {
                t.nud_Capacidade.Value = (int)Math.Floor(nud_CMAA.Value);
                t.nud_IOE.Value = (int)Math.Floor(nud_IOE.Value);
            }
            else
            {
                t.nud_Capacidade.Value = (int)Math.Floor(nud_CPre.Value);
                t.nud_IOE.Value = 1;
            }

            this.Close();


        }
        private void DeterminarQuienAtivo()
        {
            if (!Ch_Circular.Checked)
            {
                nud_Diametro.Value = 0;
                nud_Alto.Value = 0;
                nud_Alto2.Value = 0;
                nud_NPf.Value = 0;
                nud_Nf.Value = 0;
            }
            else
            {
                ZerarValores();

            }
            if (!Ch_CPre.Checked)
            {               
                nud_Compartimentos.Value = 0;
                nud_CPre.Value = 0;
            }

          

            
        }
       

        private void cb_Peça_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            if (cb_Peça.Text.Contains("BASE"))
            {
                if (cb_Peça.Text.Contains("DIPLOMATA"))
                {
                    CrearCICG();
                    cb_Area.SelectedIndex = 1;
                    AssignarTipoQuandoFamiliaCambia();
                    Ch_Circular.Checked = false;
                    Ch_CPre.Checked = false;
                    dt = Banco.Procurar("Peças", "*", "NomeCIP", "'" + cb_Peça.Text + "'", "CIP");
                    nud_Px.Value = decimal.Parse(dt.Rows[0].Field<string>("Cumprimento"));
                    nud_Py.Value = decimal.Parse(dt.Rows[0].Field<string>("Largura"));
                    nud_Pz.Value = decimal.Parse(dt.Rows[0].Field<string>("Altura"));
                    nud_Diametro.Value = 0;
                    nud_Alto.Value = 0;
                    nud_Alto2.Value = 0;
                }
                else
                {
                    Ch_Circular.Checked = true;
                    Ch_CPre.Checked = false;
                    CrearCICG();
                    cb_Area.SelectedIndex = 1;
                    AssignarTipoQuandoFamiliaCambia();
                
                    dt = Banco.Procurar("Peças", "*", "NomeCIP", "'" + cb_Peça.Text + "'", "CIP");
                    nud_Px.Value = 0;
                    nud_Py.Value = 0;
                    nud_Pz.Value = 0;
                    nud_Diametro.Value = decimal.Parse(dt.Rows[0].Field<string>("Diametro"));
                    nud_Alto.Value = decimal.Parse(dt.Rows[0].Field<string>("Alto"));
                    nud_Alto2.Value = decimal.Parse(dt.Rows[0].Field<string>("Alto2"));
                   
                }
               
            }
            else
            {
                Ch_Circular.Checked = false;
            }

            if (cb_Peça.Text==""&&cargou)
            {
                tb_CICG.Text = cb_Tipo.SelectedValue.ToString() + cb_Area.SelectedValue + "-0.0.0";
            }

            if (cargou && !Ch_Circular.Checked && !Ch_CPre.Checked)
            {
                dt = Banco.Procurar("Peças", "*", "NomeCIP", "'" + cb_Peça.Text + "'", "CIP");
                if (dt.Rows.Count>0)
                {
                    nud_Px.Value = decimal.Parse(dt.Rows[0].Field<string>("Cumprimento"));
                    nud_Py.Value = decimal.Parse(dt.Rows[0].Field<string>("Largura"));
                    nud_Pz.Value = decimal.Parse(dt.Rows[0].Field<string>("Altura"));

                  
                    CrearCICG();
                    cb_Area.SelectedIndex = 1;
                    AssignarTipoQuandoFamiliaCambia();
                }
                else
                {
                    nud_Px.Value = 0;
                    nud_Py.Value = 0;
                    nud_Pz.Value = 0;
                }
            }
            DeterminarQuienAtivo();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_Peça.Text!="")
            {
                CrearCICG();
            }
            else
            {
                tb_CICG.Text = "";
            
            }

            if (cb_Tipo.SelectedIndex==1)
            {
                Ch_CPre.Checked = true;
            }
            else
            {
                Ch_CPre.Checked = false;
            }

            if (cb_Peça.Text.Contains("BASE"))
            {
                Ch_Circular.Checked = true;

            }
            else
            {
                Ch_Circular.Checked = false;
            }
        }

        private void cb_Area_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_Peça.Text != "")
            {
                CrearCICG();
            }
            else
            {
                tb_CICG.Text = "";                
          
            }
        }

        private void tb_CICG_TextChanged(object sender, EventArgs e)
        {
            if (cb_Tipo.Text==""||cb_Area.Text=="")
            {
                tb_CICG.Text = "";
                return;
            }

            if (cb_Peça.Text=="")
            {
                tb_CICG.Text = cb_Tipo.SelectedValue.ToString()+cb_Area.SelectedValue+"-0.0.0";
            }

            dt = Banco.Procurar("CadastroGeralContenedores","*","CICG","'"+tb_CICG.Text+"%'","CICG");
            Cont = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {
                nud_Cx.Value = decimal.Parse(dt.Rows[0].Field<string>("Cumprimento"));
                nud_Cy.Value = decimal.Parse(dt.Rows[0].Field<string>("Largura"));
                nud_Cz.Value = decimal.Parse(dt.Rows[0].Field<string>("Altura"));
                nud_CPre.Value = decimal.Parse(dt.Rows[0].Field<string>("Capacidade"));
            }
            else
            {
                dt = Banco.Procurar("CadastroGeralContenedores", "*", "CICG", "'" + cb_Tipo.SelectedValue + cb_Area.SelectedValue + "%'", "CICG");
                Cont = dt.Rows.Count;
                if (dt.Rows.Count > 0)
                {
                    
                    nud_Cx.Value = decimal.Parse(dt.Rows[0].Field<string>("Cumprimento"));
                    nud_Cy.Value = decimal.Parse(dt.Rows[0].Field<string>("Largura"));
                    nud_Cz.Value = decimal.Parse(dt.Rows[0].Field<string>("Altura"));
                    nud_CPre.Value = Convert.ToInt32(dt.Rows[0].Field<Int64>("Capacidade"));
                }
                else
                {
                  
                    nud_Cx.Value = 100;
                    nud_Cy.Value = 100;
                    nud_Cz.Value = 120;
                }
            }

        }

        private void Ch_CPre_CheckedChanged(object sender, EventArgs e)
        {
            if (Ch_CPre.Checked)
            {
                gb_Predefinidos.Enabled = true;
                gb_PC.Enabled=false;
                gb_PNC.Enabled = false;
            }
            else
            {
                gb_Predefinidos.Enabled = false;
                
                if (Ch_Circular.Checked)
                {
                    gb_PNC.Enabled = false;
                    gb_PC.Enabled = true;
                }
                else
                {
                    gb_PNC.Enabled = true;
                    gb_PC.Enabled = false;
                }
                nud_Compartimentos.Value = 0;
                nud_CPre.Value = 0;
            }
        }

        private void Ch_Circular_CheckedChanged(object sender, EventArgs e)
        {
            if (Ch_Circular.Checked)
            {
                gb_PNC.Enabled = false;
                gb_PC.Enabled = true;
            }
            else
            {
                gb_PNC.Enabled = true;
                gb_PC.Enabled = false;
            }
        }

        private void Ch_Pares_CheckedChanged(object sender, EventArgs e)
        {
            if (Ch_CPre.Checked)
            {
                if (Ch_Pares.Checked)
                {
                    n = 2;
                }
                else
                {
                    n = 1;
                }

                nud_CPre.Value = nud_Compartimentos.Value * n;
            }
            else
            {
                Calcular();
            }
           
        }

        private void nud_Compartimentos_ValueChanged(object sender, EventArgs e)
        {
            if (nud_Compartimentos.Value==0)
            {
                nud_CPre.Value = 0;
            }
            else
            {
                nud_CPre.Value = nud_CMAA.Value = nud_CI.Value = nud_Compartimentos.Value * n;
                nud_IOE.Value = 1;
            }
            if (Ch_CPre.Checked)
            {
                if (Ch_Pares.Checked)
                {
                    n = 2;
                }
                else
                {
                    n = 1;
                }

                

            }
        }

        private void nud_Diametro_ValueChanged(object sender, EventArgs e)
        {
            Calcular();
        }

        private void nud_Alto_ValueChanged(object sender, EventArgs e)
        {
            Calcular();
        }

        private void nud_Alto2_ValueChanged(object sender, EventArgs e)
        {
            Calcular();
        }

        private void cb_Familia_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (dgv_CadastroGeralCapacidade.Rows.Count > 0)
            //  {
            if (cargou)
            {

                if (cb_Familia.Text != "")
                {
                    cb_Peça.Enabled = true;

                    if (cb_Familia.Text.Contains("BASE"))
                    {
                        DataTable dt = Banco.Procurar2Criterios("Peças", "CIP, NomeCIP", "NomeCIP", "Tipo", "'" + cb_Familia.Text + "%'", "'N%'", "CIP");
                        if (dt.Rows.Count > 0)
                        {
                            cb_Peça.DataSource = dt;
                        }
                    }
                    else
                    {
                        DataTable dt = Banco.Procurar("Peças", "CIP, NomeCIP", "NomeCIP", "'" + cb_Familia.Text + "%'", "CIP");
                        if (dt.Rows.Count > 0)
                        {
                            cb_Peça.DataSource = dt;
                        }
                    }
                }
                else
                {
                    DataTable dt = Banco.Procurar("Peças", "CIP, NomeCIP", "NomeCIP", "'" + cb_Familia.Text + "%'", "CIP");
                    if (dt.Rows.Count > 0)
                    {
                        cb_Peça.DataSource = dt;
                    }

                    cb_Peça.Enabled = false;
                    cb_Peça.SelectedValue = 0234;
                    cb_Tipo.SelectedValue = 22342;
                    cb_Area.SelectedValue = 32444;
                }

               
            }
            //}
        }

     
        private void dgv_CadastroGeralCapacidade_SelectionChanged(object sender, EventArgs e)
        {
           AtualizarSelección();
        }
        int Index;
        string CIP;
        private void btn_Novo_Click(object sender, EventArgs e)
        {
            if (btn_Novo.Text=="Novo")
            {
                Index = cb_Familia.SelectedIndex;
                CIP = (string)cb_Peça.SelectedValue;
                cb_Familia.SelectedIndex = 0;
                ZerarValores();
                btn_Novo.Text = "Cancelar";
                btn_Salvar.Text = "Salvar";
                btn_Excluir.Enabled = false;
                tb_CICG.ReadOnly = false;
            }
            else
            {
                if (btn_Novo.Text == "Cancelar")
                {

                    cb_Familia.SelectedIndex = Index;
                    cb_Peça.SelectedValue = CIP;
                    btn_Novo.Text = "Novo";
                    btn_Salvar.Text = "Atualizar";
                    btn_Excluir.Enabled = true;
                    tb_CICG.ReadOnly = true;

                }
            }
        }

        private void btn_Salvar_Click(object sender, EventArgs e)
        {
            int linha=0;
            if (btn_Salvar.Text=="Atualizar")
            {
                DialogResult res = MessageBox.Show("Quer mesmo Atualizar os dados?", "Quer atualizar?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {

                    CriarCapacidade();
                    bool Atualizou;
                    dt = Banco.ObterTodosOnde("CadastroDeCapacidadeGeral", "CICG", "'" + tb_CICG.Text + "'");

                    if (dt.Rows.Count > 0)
                    {
                        try
                        {
                            linha = dgv_CadastroGeralCapacidade.SelectedRows[0].Index;

                             Atualizou = Banco.Atualizar("CadastroDeCapacidadeGeral", "CICG= '" + c.CICG + "', CIP= '" + c.CIP + "', CI= '" + c.CI + "', IOE= '" + c.IOE + "', Capacidade= '" + c.Capacidad + "', Pares= " + c.Pares + ", Compartimentos= '" + c.Compartimentos + "', Padrão= '" + c.Padrão + "', Cpre= " + c.Cpre + ", Familia= '" + c.Familia + "'", "CICG", "'" + tb_CICG.Text + "'");


                            if (Atualizou)
                            {
                                dgv_CadastroGeralCapacidade.DataSource = Banco.Procurar("CadastroDeCapacidadeGeral", "CICG,Capacidade", "CICG", "'"+string.Empty+"%'", "CICG");
                                dgv_CadastroGeralCapacidade.Rows[linha].Selected = true;
                                dgv_CadastroGeralCapacidade.CurrentCell = dgv_CadastroGeralCapacidade[0, linha];
                                btn_Novo.Text = "Novo";
                                tb_CICG.ReadOnly = true;
                                MessageBox.Show("Dados Atualizados");
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
                    else { MessageBox.Show("Impossivel atualizar: Capacidade não cadastrada!", "Dados não existem", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                }
                else
                {
                    MessageBox.Show("Operação Cancelada"); return;
                }

            }
            else
            {
                if (btn_Salvar.Text=="Salvar")
                {
                    DialogResult res = MessageBox.Show("Quer mesmo Salvar os dados?", "Quer Salvar?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (res == DialogResult.Yes)
                    {


                        dt = Banco.ObterTodosOnde("CadastroDeCapacidadeGeral", "CICG", "'" + tb_CICG.Text + "'");
                        bool Salvou;                            
                            CriarCapacidade();
                         
                           try
                           {
                                if (dgv_CadastroGeralCapacidade.Rows.Count > 0)
                                {
                                    linha = dgv_CadastroGeralCapacidade.CurrentRow.Index;
                                }
                                

                                if (dt.Rows.Count > 0)
                                {
                                    Salvou = Banco.Atualizar("CadastroDeCapacidadeGeral", "CICG= '" + c.CICG + "', CIP= '" + c.CIP + "', CI= '" + c.CI + "', IOE= '" + c.IOE + "', Capacidade= '" + c.Capacidad + "', Pares= " + c.Pares + " , Compartimentos= '" + c.Compartimentos + "', Padrão= '" + c.Padrão + "', Cpre= " + c.Cpre + ", Familia= '" + c.Familia + "'", "CICG", "'" + tb_CICG.Text + "'");

                                }
                                else
                                {
                                    Salvou = Banco.Salvar("CadastroDeCapacidadeGeral", "CICG, CIP, CI, IOE, Capacidade, Pares, Compartimentos, Padrão, Cpre, Familia", "'" + c.CICG + "', '" + c.CIP + "', '" + c.CI + "','" + c.IOE + "',  '" + c.Capacidad + "', " + c.Pares + ", '" + c.Compartimentos + "', '" + c.Padrão + "',  " + c.Cpre + ",  '" + c.Familia+"'");
                                }



                                if (Salvou)
                                {                               
                                    dgv_CadastroGeralCapacidade.DataSource = Banco.Procurar("CadastroDeCapacidadeGeral", "CICG,Capacidade", "CICG", "'" + string.Empty + "%'", "CICG");
                                    if (dgv_CadastroGeralCapacidade.Rows.Count > 0)
                                    {
                                        dgv_CadastroGeralCapacidade.Rows[linha].Selected = true;
                                        dgv_CadastroGeralCapacidade.CurrentCell = dgv_CadastroGeralCapacidade[0, linha];
                                    }
                                    btn_Novo.Text = "Novo";
                                tb_CICG.ReadOnly = true;
                                MessageBox.Show("Dado Salvo", "Resumo de Cadastro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {

                                    MessageBox.Show("Não Foi Possivel Salvar o Dado", "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }


                           }
                            catch (ArgumentOutOfRangeException ex)
                           {
                               MessageBox.Show("Erro: hola" + ex.Message);
                           }
                    }
                    else
                    {
                        MessageBox.Show("Operação Cancelada"); return;
                    }
                }

            }
        }

        private void btn_Excluir_Click(object sender, EventArgs e)
        {
          
            if (dgv_CadastroGeralCapacidade.Rows.Count > 0)
            {
                Banco.Excluir("CadastroDeCapacidadeGeral", "CICG","'"+tb_CICG.Text+"'");
                dgv_CadastroGeralCapacidade.DataSource = Banco.Procurar("CadastroDeCapacidadeGeral", "CICG,Capacidade", "CICG", "'" + string.Empty + "%'", "CICG");
               
            }
        }
    
    }
}
