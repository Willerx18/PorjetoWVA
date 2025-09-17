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
    public partial class F_Fogão : Form
    {
        #region Variaveis Gerais
        string IdSeleccionados;
        string IdSeleccionados2;
        DataTable dt;
        DataTable Bases;
        DataTable BasesG;
        Fogão F = new Fogão();
        Fogão G = new Fogão();
        int modo = 0;
        #endregion;

        public F_Fogão()
        {
            InitializeComponent();
        }

        private void F_Fogão_Load(object sender, EventArgs e)
        {
            #region Popular ComboBoxes

            #region Modelo
            cb_Modelo.Items.Clear();
            cb_Modelo.DataSource = Banco.Procurar("Modelos","*", "Tipo","'%F%'","IdModelos");
            cb_Modelo.DisplayMember = "Modelo";
            cb_Modelo.ValueMember = "IdModelos";
            #endregion;

            #region Caracteristica
            cb_Caracteristica.Items.Clear();
            cb_Caracteristica.DataSource = Banco.Procurar("Caracteristicas", "*", "Tipo", "'%F%'", "IdCaracteristica"); 
            cb_Caracteristica.DisplayMember = "Caracteristica";
            cb_Caracteristica.ValueMember = "IdCaracteristica";
            #endregion;

            #region Bases
            cb_Bases.Items.Clear();
            cb_Bases.DataSource = Banco.Procurar2Criterios("Peças", "CIP,NomeCIP", "CIP","Tipo", "'2.%'","'%N%'", "CIP");
            cb_Bases.DisplayMember = "NomeCIP";
            cb_Bases.ValueMember = "CIP";

            #endregion;
            #endregion;

            #region Popular DGVs

            #region dgv_FogoesCadastro

            dgv_FogoesCadastro.DataSource = null;
            dgv_FogoesCadastro.DataSource = Banco.Procurar("Fogão", "Id, NomeFogão","Id", "'"+string.Empty+"%'", "Id");

            dgv_FogoesCadastro.Columns[0].Width = 60;
            dgv_FogoesCadastro.Columns[1].Width = 260;
            #endregion;

            #region dgv_Bases

            dgv_Bases.DataSource = null;
            dgv_Bases.DataSource = Banco.Procurar("BasesFogão", "CIB, Base, Quantidade", "IdFogão", "'1%'", "IdFogão");
            lb_Total.Text = dgv_Bases.Rows.Count.ToString();
            dgv_Bases.Columns[0].Width = 90;
            dgv_Bases.Columns[1].Width = 240;
            dgv_Bases.Columns[2].Width = 90;
            #endregion;

            #endregion;

            #region Config.
            cb_Caracteristica.Enabled = false;
            cb_Modelo.Enabled = false;
            #endregion;
        }



        #region PROCEDIMENTOS
        private int EstabelecerOValorDo_ID(DataTable Dados, string NomeColumnaID)
        {
            int IdAnteior = 0;
            int IdAtual;
            int control = 0;
            int control2 = 0;
            int id = 2;
            foreach (DataRow dr in Dados.Rows)
            {
                if (control2 > control)
                {

                    IdAtual = Convert.ToInt32(dr[NomeColumnaID])-IdAnteior;

                    if (IdAtual > 1)
                    {
                        id = IdAnteior + 1;
                        return id;
                    }
                    else
                    {

                        id = Dados.Rows.Count+1;
                    }

                }

                IdAnteior = Convert.ToInt32(dr[NomeColumnaID]);
                control += 1;
                control2 = 1 + control;



            }
            return id;


        }
        private void CrearDatatableBasesGuardar()
        {
            BasesG = new DataTable();
            BasesG.Columns.Add("IdFogão");
            BasesG.Columns.Add("CIP");
            BasesG.Columns.Add("Base");
            BasesG.Columns.Add("Quantidade");
            BasesG.Columns.Add("CIB");
        }
        private void CrearDatatableBases()
        {
            Bases = new DataTable();           
            Bases.Columns.Add("CIB");
            Bases.Columns.Add("Base");
            Bases.Columns.Add("Quantidade");
        }

        private bool DeterminarSeChecked(string L)
        {
            #region Verificar Cheked
            if (L == "B" && F.Bandeja == "1A"||F.Bandeja=="1B")
            {
                if (F.Bandeja=="1A")
                {
                    Ch_BNormal.Checked = true;
                    Ch_BColetora.Checked = false;
                }
                else
                {
                    Ch_BColetora.Checked = true;
                    Ch_BNormal.Checked = false;
                }
                return true;
            }
            else
            {
                if (L == "B" && F.Bandeja == "0")
                {
                    return false;
                }
                else
                {
                    if (L == "C" && F.Costa > 0)
                    {
                        return true;
                    }
                    else
                    {
                        if (L == "C" && F.Costa <= 0)
                        {
                            return false;
                        }
                        else
                        {
                            if (L == "D" && F.Difusor > 0)
                            {
                                return true;
                            }
                            else
                            {
                                if (L == "D" && F.Difusor <= 0)
                                {
                                    return false;
                                }
                                else
                                {
                                    if (L == "L" && F.Lateral > 0)
                                    {
                                        return true;
                                    }
                                    else
                                    {
                                        if (L == "L" && F.Lateral <= 0)
                                        {
                                            return false;
                                        }
                                        else
                                        {
                                            if (L == "P" && F.Porta > 0)
                                            {
                                                return true;
                                            }
                                            else
                                            {
                                                if (L == "P" && F.Porta <= 0)
                                                {
                                                    return false;
                                                }
                                                else
                                                {
                                                    if (L == "Q" && F.QuadroFrontal > 0)
                                                    {
                                                        return true;
                                                    }
                                                    else
                                                    {
                                                        if (L == "Q" && F.QuadroFrontal <= 0)
                                                        {
                                                            return false;
                                                        }
                                                        else
                                                        {
                                                            if (L == "T" && F.Teto > 0)
                                                            {
                                                                return true;
                                                            }
                                                            else
                                                            {
                                                                if (L == "T" && F.Teto <= 0)
                                                                {
                                                                    return false;
                                                                }
                                                                else
                                                                {
                                                                    if (L == "V" && F.Vedação > 0)
                                                                    {
                                                                        return true;
                                                                    }
                                                                    else
                                                                    {
                                                                        if (L == "V" && F.Vedação <= 0)
                                                                        {
                                                                            return false;
                                                                        }
                                                                        else
                                                                        {
                                                                            if (L == "M" && F.Mesa > 0)
                                                                            {
                                                                                return true;
                                                                            }
                                                                            else
                                                                            {
                                                                                if (L == "M" && F.Mesa <= 0)
                                                                                {
                                                                                    return false;
                                                                                }
                                                                                else
                                                                                {
                                                                                    return false;
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

                        }
                    }
                }
            }



            #endregion;
        }
        private void AssignarCheckBoxes()
        {
            Ch_Bandeja.Checked = DeterminarSeChecked("B");
            Ch_Costa.Checked = DeterminarSeChecked("C");
            Ch_Difusor.Checked = DeterminarSeChecked("D");
            Ch_Lateral.Checked = DeterminarSeChecked("L");
            Ch_Porta.Checked = DeterminarSeChecked("P");
            Ch_QuadroFrontal.Checked = DeterminarSeChecked("Q");
            Ch_TetoBancada.Checked = DeterminarSeChecked("T");
            Ch_Vedação.Checked = DeterminarSeChecked("V");
            Ch_Mesa.Checked = DeterminarSeChecked("M");
        }

        private void CargarBases()
        {
           
            Bases = Banco.Procurar("BasesFogão", "CIB, Base, Quantidade", "IdFogão", "'" + IdSeleccionados + "%'", "IdFogão");
           
            if (Bases.Rows.Count > 0)
            {
                BasesG = Banco.Procurar("BasesFogão", "*", "IdFogão", "'" + IdSeleccionados + "%'", "IdFogão");
                
                ch_Bases.Checked = true;
                dgv_Bases.DataSource = Bases;
                dgv_Bases.Columns[0].Width = 90;
                dgv_Bases.Columns[1].Width = 240;
                dgv_Bases.Columns[2].Width = 90;
                lb_Total.Text = Bases.Rows.Count.ToString();
                dgv_Bases.AllowUserToDeleteRows = false;
                
                btn_ExcluirBase.Visible = true;
            }
            else
            {
            
                ch_Bases.Checked = false;
                lb_Total.Text = 0.ToString();
                dgv_Bases.AllowUserToDeleteRows = true;
                btn_ExcluirBase.Visible = false;
                CrearDatatableBases();
                dgv_Bases.DataSource = Bases;
            }
        }

        private void CriarFogão(DataTable dt)
        {
            F.Id= dt.Rows[0].Field<Int64>("Id").ToString(); 
            F.NomeFogão= dt.Rows[0].Field<string>("NomeFogão"); 
            F.Caracteristica= dt.Rows[0].Field<string>("Caracteristica");
            F.ModeloDasPeças= dt.Rows[0].Field<string>("ModeloDasPeças");
           
            F.Bandeja= dt.Rows[0].Field<string>("Bandeja");
            F.Costa= int.Parse(dt.Rows[0].Field<string>("Costa"));
            F.Difusor = int.Parse(dt.Rows[0].Field<string>("Difusor"));
            F.Lateral = int.Parse(dt.Rows[0].Field<string>("Lateral"));
            F.Porta = int.Parse(dt.Rows[0].Field<string>("Porta"));
            F.QuadroFrontal = int.Parse(dt.Rows[0].Field<string>("QuadroFrontal"));
            F.Teto = int.Parse(dt.Rows[0].Field<string>("Teto"));
            F.Vedação = int.Parse(dt.Rows[0].Field<string>("Vedação"));
            F.Mesa = int.Parse(dt.Rows[0].Field<string>("Mesa"));
           
            
        }
        private void CriarFogão()
        {
            G.Id = tb_ID.Text;      
            G.NomeFogão = tb_NomeFogão.Text;
            G.Caracteristica = cb_Caracteristica.SelectedIndex.ToString();
            G.ModeloDasPeças = cb_Modelo.SelectedValue.ToString(); 
            if (Ch_Bandeja.Checked==true)
            {
                if (nud_BNormal.Value>0)
                {
                    G.Bandeja = "1A";
                }
                else
                {
                    G.Bandeja= "1B";
                }
            }
            else
            {
                G.Bandeja = "0";
            }

            G.Costa = Convert.ToInt32(nud_Costa.Value);
            G.Difusor = Convert.ToInt32(nud_Difusor.Value);
            G.Lateral = Convert.ToInt32(nud_Lateral.Value);
            G.Porta = Convert.ToInt32(nud_Porta.Value);
            G.QuadroFrontal = Convert.ToInt32(nud_QuadroFrontal.Value);
            G.Teto = Convert.ToInt32(nud_TetoBancada.Value);
            G.Vedação = Convert.ToInt32(nud_Vedação.Value);
            G.Mesa = Convert.ToInt32(nud_Mesa.Value);


        }
        private void ActualizarSelección()
        {
            

            IdSeleccionados = dgv_FogoesCadastro.Rows[dgv_FogoesCadastro.SelectedRows[0].Index].Cells[0].Value.ToString();
                        
            dt = Banco.Procurar("Fogão", "*", "Id", "'" + IdSeleccionados + "%'", "Id");
            if (dt.Rows.Count > 0)
            {
                modo = 0;
                CriarFogão(dt);
                tb_ID.Text = F.Id;
                tb_NomeFogão.Text= F.NomeFogão;
                cb_Caracteristica.SelectedIndex= int.Parse(F.Caracteristica);
                cb_Modelo.Text = F.ModeloDasPeças.ToString();
                
                AssignarCheckBoxes();
                CargarBases();

            }


        }

      
        #endregion;

        #region Cambios

        #region CheckedChanged
        private void Ch_BNormal_CheckedChanged(object sender, EventArgs e)
        {
            if (Ch_BNormal.Checked)
            {
                Ch_BColetora.Checked = false;
                nud_BNormal.Value = 1;
            }
            else
            {
                nud_BNormal.Value = 0;
            }
        }

        private void Ch_BColetora_CheckedChanged(object sender, EventArgs e)
        {
            if (Ch_BColetora.Checked)
            {
                Ch_BNormal.Checked = false;
                nud_BColetora.Value = 1;
            }
            else
            {
                nud_BColetora.Value = 0;
            }
        }
        private void Ch_Bandeja_CheckedChanged(object sender, EventArgs e)
        {
            if (Ch_Bandeja.Checked)
            {
                gb_Bandeja.Enabled = true;
                Ch_BNormal.Checked = true;
            }
            else
            {
                gb_Bandeja.Enabled = false;
                Ch_BNormal.Checked = false;
                Ch_BColetora.Checked = false;
               
            }
        }
        private void Ch_Costa_CheckedChanged(object sender, EventArgs e)
        {
            if (Ch_Costa.Checked)
            {
                nud_Costa.Enabled = true;
                nud_Costa.Value = 1;
            }
            else
            {
                nud_Costa.Enabled = false;
                nud_Costa.Value = 0;
            }
        }

        private void Ch_Difusor_CheckedChanged(object sender, EventArgs e)
        {
            if (Ch_Difusor.Checked)
            {
                nud_Difusor.Enabled = true;
                nud_Difusor.Value = 1;
            }
            else
            {
                nud_Difusor.Enabled = false;
                nud_Difusor.Value = 0;
            }

        }

        private void Ch_Lateral_CheckedChanged(object sender, EventArgs e)
        {
            if (Ch_Lateral.Checked)
            {
                nud_Lateral.Enabled = true;
                nud_Lateral.Value = 2;
            }
            else
            {
                nud_Lateral.Enabled = false;
                nud_Lateral.Value = 0;
            }
        }

        private void Ch_Porta_CheckedChanged(object sender, EventArgs e)
        {
            if (Ch_Porta.Checked)
            {
                nud_Porta.Enabled = true;
                nud_Porta.Value = 1;
            }
            else
            {
                nud_Porta.Enabled = false;
                nud_Porta.Value = 0;
            }
        }

        private void Ch_QuadroFrontal_CheckedChanged(object sender, EventArgs e)
        {
            if (Ch_QuadroFrontal.Checked)
            {
                nud_QuadroFrontal.Enabled = true;
                nud_QuadroFrontal.Value = 1;
            }
            else
            {
                nud_QuadroFrontal.Enabled = false;
                nud_QuadroFrontal.Value = 0;
            }
        }

        private void Ch_TetoBancada_CheckedChanged(object sender, EventArgs e)
        {
            if (Ch_TetoBancada.Checked)
            {
                nud_TetoBancada.Enabled = true;
                nud_TetoBancada.Value = 1;
            }
            else
            {
                nud_TetoBancada.Enabled = false;
                nud_TetoBancada.Value = 0;
            }

        }

        private void Ch_Vedação_CheckedChanged(object sender, EventArgs e)
        {
            if (Ch_Vedação.Checked)
            {
                nud_Vedação.Enabled = true;
                nud_Vedação.Value = int.Parse(lb_Total.Text);
                cb_Bases.DataSource = Banco.Procurar2Criterios("Peças", "CIP,NomeCIP", "CIP", "Tipo", "'2.%'", "'%V%'", "CIP");
            }
            else
            {
                cb_Bases.DataSource = Banco.Procurar2Criterios("Peças", "CIP,NomeCIP", "CIP", "Tipo", "'2.%'", "'%N%'", "CIP");
                nud_Vedação.Enabled = false;
                nud_Vedação.Value = 0;
            }

        }

        private void Ch_Mesa_CheckedChanged(object sender, EventArgs e)
        {
            if (Ch_Mesa.Checked)
            {
                nud_Mesa.Enabled = true;
                nud_Mesa.Value = 1;
            }
            else
            {
                nud_Mesa.Enabled = false;
                nud_Mesa.Value = 0;
            }
        }
        private void ch_Bases_CheckedChanged(object sender, EventArgs e)
        {
            if (ch_Bases.Checked)
            {
                gb_Bases.Enabled = true;
               
            }
            else
            {
                gb_Bases.Enabled = false;
                nud_Bases.Value = 0;
                cb_Bases.SelectedValue = 1293;
            }
        }


        #endregion;

        #region dgv_SelectionChanged
        private void dgv_FogoesCadastro_SelectionChanged(object sender, EventArgs e)
        {
            int contilinha = dgv_FogoesCadastro.Rows.Count;
            if (contilinha > 0)
            {
                int row = dgv_FogoesCadastro.CurrentRow.Index;
                dgv_FogoesCadastro.Rows[row].Cells[0].Selected = true;

                try
                {

                    ActualizarSelección();



                    int dgvIndex = dgv_FogoesCadastro.CurrentRow.Index;
                    dgv_FogoesCadastro.Rows[dgvIndex].Selected = true;
                    dgv_FogoesCadastro.CurrentCell = dgv_FogoesCadastro[0, dgvIndex];

                    cb_Caracteristica.Enabled = false;
                    cb_Modelo.Enabled = false;
                    btn_Salvar.Text = "Atualizar";
                    btn_Excluir.Enabled = true;
                    btn_Novo.Enabled = true;


                }
                catch (ArgumentOutOfRangeException ex)
                {
                    MessageBox.Show("Erro: " + ex.Message);

                }
            }

        }

        #endregion;

        #endregion;


        #region btn_CLick
        private void btn_ExcluirBase_Click(object sender, EventArgs e)
        {
            if (dgv_Bases.Rows.Count > 0)
            {
                IdSeleccionados2 = dgv_Bases.Rows[dgv_Bases.SelectedRows[0].Index].Cells[0].Value.ToString();

                DialogResult res = MessageBox.Show("Certeza que deseja Excluir?\n\n", "Excluir", MessageBoxButtons.YesNo);
                if (res == DialogResult.Yes)
                {
                    if (modo == 0)
                    {
                        Banco.ExcluirDirecto("BasesFogão", "CIB", "'" + IdSeleccionados2 + "'");
                        btn_Desfazer.Visible = true;
                    }
                    int x = dgv_Bases.CurrentRow.Index;
                    Bases = (DataTable)dgv_Bases.DataSource;
                    Bases.Rows[x].Delete();
                    BasesG.Rows[x].Delete();
                    Bases.AcceptChanges();
                    BasesG.AcceptChanges();
                    dgv_Bases.DataSource = Bases;
                    lb_Total.Text = Bases.Rows.Count.ToString();



                }
            }

        }


        private void btn_Desfazer_Click(object sender, EventArgs e)
        {
            CargarBases();
            btn_Desfazer.Visible = false;
        }

        private void btn_addBase_Click(object sender, EventArgs e)
        {
            if (nud_Bases.Value > 0)
            {
                foreach (DataRow dr in Bases.Rows)
                {
                    if ((string)dr["CIB"] == tb_ID.Text + "-" + cb_Bases.SelectedValue)
                    {
                        MessageBox.Show("Não é possivel inserir duas bases iguais.\nPara adicionar mais bases do mesmo modelo deve excluir a base cadastrada e adicionar ela novamente com a quantida desejada", "ERRO: Base Repetida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                DataRow row = Bases.NewRow();
                row["CIB"] = tb_ID.Text + "-" + cb_Bases.SelectedValue;
                row["Base"] = cb_Bases.Text;
                row["Quantidade"] = nud_Bases.Value.ToString();
                Bases.Rows.Add(row);
                dgv_Bases.DataSource = Bases;

                DataRow row2 = BasesG.NewRow();
                row2["IdFogão"] = Int64.Parse(tb_ID.Text);
                row2["CIP"] = cb_Bases.SelectedValue;
                row2["Base"] = cb_Bases.Text;
                row2["Quantidade"] = nud_Bases.Value.ToString();
                row2["CIB"] = tb_ID.Text + "-" + cb_Bases.SelectedValue;
                BasesG.Rows.Add(row2);

                if (modo == 0)
                {
                    btn_Desfazer.Visible = true;
                }



            }
            else
            {
                MessageBox.Show("Quantidade deve ser maior que zero", "Valor Invalido", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btn_Novo_Click(object sender, EventArgs e)
        {
            if (btn_Novo.Text == "Novo")
            {
                modo = 1;
                lb_Total.Text = 0.ToString();
                Ch_Bandeja.Checked = true;
                Ch_Costa.Checked = true;
                Ch_Lateral.Checked = true;
                Ch_Difusor.Checked = false;
                Ch_Mesa.Checked = false;
                Ch_Vedação.Checked = false;
                ch_Bases.Checked = true;
                Ch_TetoBancada.Checked = true;
                Ch_QuadroFrontal.Checked = true;
                Ch_Porta.Checked = true;
                tb_ID.Text = "";
                tb_NomeFogão.Text = "";
                cb_Caracteristica.SelectedIndex = 0;
                cb_Modelo.SelectedIndex = 0;
                cb_Bases.SelectedValue = 1233;
                nud_Bases.Value = 0;
                CrearDatatableBases();
                dgv_Bases.DataSource = Bases;
                btn_Novo.Text = "Cancelar";
                btn_Salvar.Text = "Salvar";
                btn_Excluir.Enabled = false;
                dt = Banco.ObterTodos("Fogão");

                tb_ID.Text = EstabelecerOValorDo_ID(dt, "Id").ToString();
                cb_Caracteristica.Enabled = true;
                cb_Modelo.Enabled = true;

            }
            else
            {
                modo = 0;
                ActualizarSelección();
                btn_Salvar.Text = "Atualizar";

                btn_Excluir.Enabled = true;
                btn_Novo.Enabled = true;
                btn_Novo.Text = "Novo";
                cb_Caracteristica.Enabled = false;
                cb_Modelo.Enabled = false;
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

                    CriarFogão();
                    bool Atualizou2 = false;
                    bool Atualizou;
                    dt = Banco.ObterTodosOnde("Fogão", "Id", "'" + tb_ID.Text + "'");
                    DataTable dt2;
                    if (dt.Rows.Count > 0)
                    {
                        try
                        {
                            if ((ch_Bases.Checked && BasesG.Rows.Count > 0) || ch_Bases.Checked == false)
                            {
                                linha = dgv_FogoesCadastro.SelectedRows[0].Index;
                                Atualizou = Banco.Atualizar("Fogão", "Id= '" + G.Id + "', NomeFogão= '" + G.NomeFogão + "', ModelodasPeças= '" + G.ModeloDasPeças + "', Caracteristica='" + G.Caracteristica + "', Bandeja= '" + G.Bandeja + "', Costa= '" + G.Costa + "', Difusor= '" + G.Difusor + "', Lateral= '" + G.Lateral + "', Porta= '" + G.Porta + "', QuadroFrontal= '" + G.QuadroFrontal + "', Teto= '" + G.Teto + "', Vedação= '" + G.Vedação + "', Mesa= '" + G.Mesa + "'", "Id", "'" + tb_ID.Text + "'");

                                if (BasesG.Rows.Count > 0)
                                {
                                    foreach (DataRow dr in BasesG.Rows)
                                    {
                                        dt2 = Banco.Procurar("BasesFogão", "*", "CIB", "'" + (string)dr["CIB"] + "%'", "IdFogão");
                                        if (dt2.Rows.Count > 0)
                                        {
                                            Atualizou2 = Banco.Atualizar("BasesFogão", "IdFogão='" + dr["IdFogão"] + "', CIP = '" + dr["CIP"] + "', Base = '" + dr["Base"] + "', Quantidade='" + dr["Quantidade"] + "', CIB='" + dr["CIB"] + "'", "CIB", "'" + (string)dr["CIB"] + "'");

                                        }
                                        else
                                        {
                                            Atualizou2 = Banco.Salvar("BasesFogão", "IdFogão, CIP , Base , Quantidade, CIB", "'" + dr["IdFogão"] + "', '" + dr["CIP"] + "', '" + dr["Base"] + "', '" + dr["Quantidade"] + "','" + dr["CIB"] + "'");
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Deve desmarcar o Checkbox BASES ou adionar bases antes de continuar", "Deve Adicionar Bases", MessageBoxButtons.OK, MessageBoxIcon.Error); return;

                                }
                                dgv_FogoesCadastro[0, linha].Value = tb_ID.Text;
                                dgv_FogoesCadastro[1, linha].Value = G.NomeFogão;

                                if (Atualizou && Atualizou2)
                                {
                                    ActualizarSelección();
                                    dgv_FogoesCadastro.Rows[linha].Selected = true;
                                    dgv_FogoesCadastro.CurrentCell = dgv_FogoesCadastro[0, linha];
                                    MessageBox.Show("Dados Atualizados");
                                    btn_Novo.Text = "Novo";
                                    modo = 0;
                                }
                                else
                                {
                                    if (Atualizou)
                                    {
                                        btn_Novo.Text = "Novo";
                                        ActualizarSelección();
                                        MessageBox.Show("Apenas dados do Fogão atualizados");
                                        modo = 0;
                                    }
                                    else
                                    {
                                        if (Atualizou2)
                                        {
                                            btn_Novo.Text = "Novo";
                                            ActualizarSelección();
                                            MessageBox.Show("Apenas dados das Bases atualizados");
                                            modo = 0;

                                        }
                                        else
                                        {
                                            modo = 0;
                                            btn_Novo.Text = "Novo";
                                            ActualizarSelección();
                                            MessageBox.Show("Não Foi Possivel Atualizar os Dados");

                                        }
                                    }

                                }
                            }
                            else
                            {
                                MessageBox.Show("Deve desmarcar o Checkbox BASES ou adionar bases antes de continuar", "Deve Adicionar Bases", MessageBoxButtons.OK, MessageBoxIcon.Error); return;

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

                    if (tb_NomeFogão.Text != "")
                    {
                        if (cb_Caracteristica.Text != "")
                        {
                            if (cb_Modelo.SelectedIndex != 0)
                            {
                                if (BasesG.Rows.Count > 0)
                                {
                                    bool Salvou;
                                    bool Salvou2;
                                    DataTable dt2;
                                    CriarFogão();
                                    dt = Banco.ObterTodosOnde("Fogão", "NomeFogão", "'" + tb_NomeFogão.Text + "'");


                                    try
                                    {
                                        if (dt.Rows.Count == 0)
                                        {

                                            linha = dgv_FogoesCadastro.CurrentRow.Index;
                                            Salvou = Banco.Salvar("Fogão", "Id, NomeFogão, ModelodasPeças, Caracteristica, Bandeja, Costa, Difusor, Lateral, Porta, QuadroFrontal, Teto, Vedação, Mesa", "' " + G.Id + "' , '" + G.NomeFogão + "' , '" + G.ModeloDasPeças + "' , '" + G.Caracteristica + "' , '" + G.Bandeja + "' , '" + G.Costa + "' , '" + G.Difusor + "' , '" + G.Lateral + "' , '" + G.Porta + "' , '" + G.QuadroFrontal + "' , '" + G.Teto + "' , '" + G.Vedação + "' , '" + G.Mesa + "'");

                                            foreach (DataRow dr in BasesG.Rows)
                                            {

                                                dt2 = Banco.Procurar("BasesFogão", "*", "CIB", "'" + (string)dr["CIB"] + "%'", "IdFogão");
                                                if (dt2.Rows.Count > 0)
                                                {
                                                    Salvou2 = Banco.Atualizar("BasesFogão", "IdFogão='" + dr["IdFogão"] + "', CIP = '" + dr["CIP"] + "', Base = '" + dr["Base"] + "', Quantidade='" + dr["Quantidade"] + "', CIB='" + dr["CIB"] + "'", "CIB", "'" + (string)dr["CIB"] + "'");

                                                }
                                                else
                                                {
                                                    Salvou2 = Banco.Salvar("BasesFogão", "IdFogão, CIP , Base , Quantidade, CIB", "'" + dr["IdFogão"] + "', '" + dr["CIP"] + "', '" + dr["Base"] + "', '" + dr["Quantidade"] + "','" + dr["CIB"] + "'");
                                                }

                                            }


                                            if (Salvou)
                                            {
                                                dgv_FogoesCadastro.DataSource = Banco.Procurar("Fogão", "Id, NomeFogão", "Id", "'" + string.Empty + "%'", "Id");
                                                modo = 0;
                                                btn_Novo.Text = "Novo";
                                                ActualizarSelección();
                                                dgv_FogoesCadastro.Rows[linha].Selected = true;
                                                dgv_FogoesCadastro.CurrentCell = dgv_FogoesCadastro[0, linha];
                                                MessageBox.Show("Dados Salvos");
                                            }
                                            else
                                            {

                                                MessageBox.Show("Não Foi Possivel Salvar os Dados");
                                            }

                                        }
                                        else { MessageBox.Show("Não e possivel Cadastrar, esta Fogão já existe!"); tb_NomeFogão.Focus(); }
                                    }
                                    catch (ArgumentOutOfRangeException ex)
                                    {
                                        MessageBox.Show("Erro: hola" + ex.Message);
                                    }
                                }
                                else { MessageBox.Show("Deve desmarcar a Checkbox BASES ou adicionar bases!"); cb_Bases.Focus(); }
                            }
                            else { MessageBox.Show("Deve Escolher um Modelo de Peça"); cb_Modelo.Focus(); }
                        }
                        else { MessageBox.Show("Deve escolher uma Caracteristica"); cb_Caracteristica.Focus(); }
                    }
                    else { MessageBox.Show("Campo \"Nome Fogão\" não pode estar vazio"); tb_NomeFogão.Focus(); }

                }
                else
                {
                    MessageBox.Show("Operação Cancelada"); return;
                }
            }

        }

        private void btn_Excluir_Click(object sender, EventArgs e)
        {
            string id = tb_ID.Text;
            bool Excluiu = Banco.Excluir("Fogão", "Id", "'" + id + "'");
            if (Excluiu)
            {
                Banco.ExcluirDirecto("BasesFogão", "IdFogão", "'" + id + "'");
            }
            dgv_FogoesCadastro.DataSource = Banco.Procurar("Fogão", "Id, NomeFogão", "Id", "'" + string.Empty + "%'", "Id");
        }


        #endregion;

      
      
    }
}
