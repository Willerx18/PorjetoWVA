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
    public partial class F_CapacidadeDeArmazenamento : Form
    {/*
        public F_CapacidadeDeArmazenamento()
        {
            InitializeComponent();
        }

        string IdSeleccionados;
        DataTable dt;
        DataTable Familias;
        DataTable Modelos;
        DataTable Caracteristicas;
        int linha;
        Capacidade c = new Capacidade();
        int ind;
        bool pp;
        bool Cambio;
        bool pc;
        int cont = 0;

        private void dgv_GesCapArm_SelectionChanged(object sender, EventArgs e)
        {
               DataGridView dgv = (DataGridView)sender;
               int contilinha = dgv_GesCapArm.Rows.Count;

               if (contilinha > 0)
               {
                   if (Cambio && cb_Familia.Text=="")
                   {
                       cb_CIP.DataSource = Banco.ObterTodos("Peças", "*", "CIP");
                       Cambio = false;
                   }
                   int row = dgv_GesCapArm.CurrentRow.Index;
                       dgv_GesCapArm.Rows[row].Cells[0].Selected = true;
                       dgv_GesCapArm.FirstDisplayedScrollingRowIndex = row;
                       try
                       {

                           IdSeleccionados = dgv.Rows[dgv.SelectedRows[0].Index].Cells[0].Value.ToString();

                           dt = Banco.ObterTodosOndeInner("Armazenamento", "Peças", "Peças.CIP as 'CIP', Peças.NomeCIP as 'Nome CIP', Armazenamento.CMAA as 'CMAA',Armazenamento.'CMAA-P' as 'CMAA-P', Armazenamento.'CMAA-C' as 'CMAA-C', Armazenamento.CMAE as 'CMAE',Armazenamento.'CMAE-P',Armazenamento.'CMAE-C' as 'CMAE-C',Armazenamento.IOE as 'IOE',Armazenamento.'IOE-P' as 'IOE-P', Armazenamento.'IOE-C' as 'IOE-C',Armazenamento.'N-Carrinhos' as 'NC',Armazenamento.'N-Paletes' as 'NP',Armazenamento.'N-Caixas' as 'NCai',Armazenamento.'Cap. Carrinhos' as 'Cap. Carrinhos',Armazenamento.'Cap. Paletes' as 'Cap. Paletes', Armazenamento.'Cap. Caixas' as 'Cap. Caixas' , P", "CIP", "Peças.NomeCIP", "'" + IdSeleccionados + "'");

                           c.CMAA = Convert.ToInt32(nud_CMAA.Value = dt.Rows[0].Field<Int64>("CMAA"));
                           c.CMAA_P = Convert.ToInt32(nud_CMAAP.Value = dt.Rows[0].Field<Int64>("CMAA-P"));
                           c.CMAA_C = Convert.ToInt32(nud_CMAACaixas.Value = dt.Rows[0].Field<Int64>("CMAA-C"));
                           c.Proporcional = dt.Rows[0].Field<string>("P");

                           c.CMAE= Convert.ToInt32(dt.Rows[0].Field<Int64>("CMAE"));
                           c.CMAE_P = Convert.ToInt32(dt.Rows[0].Field<Int64>("CMAE-P"));
                           c.CMAE_C = Convert.ToInt32(dt.Rows[0].Field<Int64>("CMAE-C"));

                           if (c.Proporcional == "Sim")
                           {

                               ch_Proporcional.Checked = true;
                               pc = true;
                           }
                           else
                           {

                               ch_Proporcional.Checked = false;
                               pc = false;
                           }



                           c.IOE = Convert.ToInt32(nud_IOE.Value = dt.Rows[0].Field<Int64>("IOE"));
                           c.IOE_P = Convert.ToInt32(nud_IOEPaletes.Value = dt.Rows[0].Field<Int64>("IOE-P"));
                           c.IOE_C = Convert.ToInt32(nud_IOECaixas.Value = dt.Rows[0].Field<Int64>("IOE-C"));

                           c.Ncarrinhos = Convert.ToInt32(nud_QCarrinhos.Value = dt.Rows[0].Field<Int64>("NC"));
                           c.NPaletes = Convert.ToInt32(nud_QPaletes.Value = dt.Rows[0].Field<Int64>("NP"));
                           c.NCaixas= Convert.ToInt32(nud_NCaixas.Value = dt.Rows[0].Field<Int64>("NCai"));

                           c.CIP1 = (cb_CIP.SelectedValue = dt.Rows[0].Field<string>("CIP")).ToString();

                               if (ch_Proporcional.Checked)
                               {
                                   c.Proporcional = "Sim";
                               }
                               else
                               {
                                   c.Proporcional = "Não";
                               }

                           //int dgvIndex = dgv_GesCapArm.CurrentRow.Index;
                           //dgv_GesCapArm.Rows[dgvIndex].Selected = true;
                           //dgv_GesCapArm.CurrentCell = dgv_GesCapArm[0, dgvIndex];


                           btn_Atualizar.Text = "Atualizar";
                           btn_Excluir.Enabled = true;
                           btn_Novo.Enabled = true;

                           ind = cb_CIP.SelectedIndex;
                           c.CapacidadeCarrinhos = Convert.ToInt32(nud_CC.Value);
                           c.CapacidadePaletes = Convert.ToInt32(nud_CP.Value);

                       dgv_GesCapArm.Columns[0].Width = 180;// CIP
                       dgv_GesCapArm.Columns[1].Width = 50;// CMAA
                       dgv_GesCapArm.Columns[2].Width = 60;// CMAA-P
                       dgv_GesCapArm.Columns[3].Width = 60;// CMAA-C
                       dgv_GesCapArm.Columns[4].Width = 50;// CMAE
                       dgv_GesCapArm.Columns[5].Width = 60;// CMAE-P
                       dgv_GesCapArm.Columns[6].Width = 60;// CMAE-C
                       dgv_GesCapArm.Columns[7].Width = 40;// IOE
                       dgv_GesCapArm.Columns[8].Width = 40;// IOE-P
                       dgv_GesCapArm.Columns[9].Width = 40;// IOE-C
                       dgv_GesCapArm.Columns[10].Width = 65;// N-Carrinhos
                       dgv_GesCapArm.Columns[11].Width = 60;// N-Paletes
                       dgv_GesCapArm.Columns[12].Width = 60;// N-Caixas
                       dgv_GesCapArm.Columns[13].Width = 70;// Cap. Carrinhos
                       dgv_GesCapArm.Columns[14].Width = 70;// Cap. Paletes
                       dgv_GesCapArm.Columns[15].Width = 70;// Cap. Caixas
                       dgv_GesCapArm.Columns[16].Width = 30;// P
                   }
                       catch (ArgumentOutOfRangeException ex)
                       {
                           MessageBox.Show("Erro: " + ex.Message);

                       }

               }



           }

           private void F_CapacidadeDeArmazenamento_Load(object sender, EventArgs e)
           {

               //populando ComboBoxFamilia
               cb_Familia.Items.Clear();
               cb_Familia.DataSource = Banco.ObterTodos("Familias", "*", "IdFamilia");
               cb_Familia.DisplayMember = "Familia";
               cb_Familia.ValueMember = "IdFamilia";

               //cb_Familia.SelectedIndex = 0;

               //Populando ComboBox CIP
               cb_CIP.Items.Clear();
               cb_CIP.DataSource = Banco.ObterTodos("Peças", "*", "CIP");
               cb_CIP.DisplayMember = "NomeCIP";
               cb_CIP.ValueMember = "CIP";

               //Preenchendo el DataGrip...
               dgv_GesCapArm.DataSource = null;
               dgv_GesCapArm.DataSource = Banco.ObterTodos("Armazenamento", "Peças", "Peças.NomeCIP,Armazenamento.CMAA,Armazenamento.'CMAA-P', Armazenamento.'CMAA-C',Armazenamento.CMAE,Armazenamento.'CMAE-P',Armazenamento.'CMAE-C',Armazenamento.IOE,Armazenamento.'IOE-P', Armazenamento.'IOE-C',Armazenamento.'N-Carrinhos',Armazenamento.'N-Paletes',Armazenamento.'N-Caixas',Armazenamento.'Cap. Carrinhos',Armazenamento.'Cap. Paletes', Armazenamento.'Cap. Caixas', Armazenamento.P", "CIP", "Peças.CIP");








               dgv_GesCapArm.Columns[0].Width = 180;// CIP
               dgv_GesCapArm.Columns[1].Width = 50;// CMAA
               dgv_GesCapArm.Columns[2].Width = 60;// CMAA-P
               dgv_GesCapArm.Columns[3].Width = 60;// CMAA-C
               dgv_GesCapArm.Columns[4].Width = 50;// CMAE
               dgv_GesCapArm.Columns[5].Width = 60;// CMAE-P
               dgv_GesCapArm.Columns[6].Width = 60;// CMAE-C
               dgv_GesCapArm.Columns[7].Width = 40;// IOE
               dgv_GesCapArm.Columns[8].Width = 40;// IOE-P
               dgv_GesCapArm.Columns[9].Width = 40;// IOE-C
               dgv_GesCapArm.Columns[10].Width = 65;// N-Carrinhos
               dgv_GesCapArm.Columns[11].Width = 60;// N-Paletes
               dgv_GesCapArm.Columns[12].Width = 60;// N-Caixas
               dgv_GesCapArm.Columns[13].Width = 70;// Cap. Carrinhos
               dgv_GesCapArm.Columns[14].Width = 70;// Cap. Paletes
               dgv_GesCapArm.Columns[15].Width = 70;// Cap. Caixas
               dgv_GesCapArm.Columns[16].Width = 30;// P



               Familias = Banco.ObterTodos("Familias");
               Modelos = Banco.ObterTodos("Modelos");
               Caracteristicas = Banco.ObterTodos("Caracteristicas");



           }

           private void cb_CIP_SelectedValueChanged(object sender, EventArgs e)
           {
               if (dgv_GesCapArm.Rows.Count > 0) 
               { 

               //cb_Familia.SelectedValue = cb_CIP.SelectedValue.ToString().Substring(0, 1);
               }
           }

           private void btn_Novo_Click(object sender, EventArgs e)
           {
               btn_Atualizar.Text = "Salvar";
               btn_Excluir.Enabled = false;


               btn_Novo.Enabled = false;
               btn_C.Enabled = true;

           }

           private void btn_Atualizar_Click(object sender, EventArgs e)
           {

               if (btn_Atualizar.Text == "Atualizar")
               {

                   linha = dgv_GesCapArm.SelectedRows[0].Index;
                   Capacidade r = new Capacidade();
                   r.CIP1 = cb_CIP.SelectedValue.ToString();

                   r.CMAA = Convert.ToInt32(nud_CMAA.Value);
                   r.CMAA_P = Convert.ToInt32(nud_CMAAP.Value);
                   r.CMAA_C = Convert.ToInt32(nud_CMAACaixas.Value);


                   r.CMAE = Convert.ToInt32(nud_CMAA.Value);
                   r.CMAE_P = Convert.ToInt32(nud_CMAAP.Value);
                   r.CMAE_C = Convert.ToInt32(nud_CMAACaixas.Value);





                   r.IOE = Convert.ToInt32(nud_IOE.Value);
                   r.IOE_P= Convert.ToInt32(nud_IOEPaletes.Value);
                   r.IOE_C = Convert.ToInt32(nud_IOECaixas.Value);


                   r.Ncarrinhos = Convert.ToInt32(nud_QCarrinhos.Value);
                   r.NPaletes = Convert.ToInt32(nud_QPaletes.Value);
                   r.NCaixas= Convert.ToInt32(nud_NCaixas.Value);

                   r.CapacidadeCarrinhos= Convert.ToInt32(nud_CC.Value);
                   r.CapacidadePaletes= Convert.ToInt32(nud_CP.Value);
                   r.CapacidadeCaixas= Convert.ToInt32(nud_CapCaixas.Value);

                   if (ch_Proporcional.Checked)
                   {

                       r.Proporcional = "Sim";
                   }
                   else
                   {
                       r.Proporcional = "Não";
                   }

                   dt = Banco.ObterTodosOnde("Armazenamento", "CIP", "'" + r.CIP1 + "'");

                   if (dt.Rows.Count > 0)
                   {
                       try
                       {


                           linha = dgv_GesCapArm.SelectedRows[0].Index;
                           Banco.Atualizar("Armazenamento", "CIP='" + r.CIP1 + "', CMAA= " + r.CMAA + ", 'CMAA-P'= " + r.CMAA_P + ", 'CMAA-C'= " + r.CMAA_C + ", CMAE= '" + r.CMAE + "', 'CMAE-P'= '" + r.CMAE_P + "', 'CMAE-C'= '" + r.CMAE_C + "', 'N-carrinhos' = " + r.Ncarrinhos + " , 'N-Paletes'= " + r.NPaletes + ", 'N-Caixas'= " + r.NCaixas + " , 'Cap. Carrinhos'= " + r.CapacidadeCarrinhos + " , 'Cap. Paletes'= " + r.CapacidadePaletes + ", 'Cap. Caixas'= " + r.CapacidadeCaixas +", P= '"+r.Proporcional+"' ", "CIP", "'" + r.CIP1 + "'");
                           // dgv_Peças.DataSource = Banco.ObterTodos("Peças", "CIP, NomeCIP", "CIP");
                           dgv_GesCapArm[0, linha].Value = cb_CIP.Text;

                           dgv_GesCapArm[1, linha].Value = r.CMAA;
                           dgv_GesCapArm[2, linha].Value = r.CMAA_P;
                           dgv_GesCapArm[3, linha].Value = r.CMAA_C;

                           dgv_GesCapArm[4, linha].Value = r.IOE;
                           dgv_GesCapArm[5, linha].Value = r.IOE_P;
                           dgv_GesCapArm[6, linha].Value = r.IOE_C;
                           if (ch_Proporcional.Checked)
                           {
                               dgv_GesCapArm[7, linha].Value = nud_CMAE.Value;
                               dgv_GesCapArm[8, linha].Value = nud_CMAEP.Value;
                               dgv_GesCapArm[9, linha].Value = nud_CMAECaixas.Value;
                           }
                           else
                           {
                               dgv_GesCapArm[7, linha].Value = r.CMAE;
                               dgv_GesCapArm[8, linha].Value = r.CMAE_P;
                               dgv_GesCapArm[9, linha].Value = r.CMAE_C;
                           }
                           dgv_GesCapArm[10, linha].Value = r.Ncarrinhos;
                           dgv_GesCapArm[11, linha].Value = r.NPaletes;
                           dgv_GesCapArm[12, linha].Value = r.NCaixas;

                           dgv_GesCapArm[13, linha].Value = r.CapacidadeCarrinhos;
                           dgv_GesCapArm[14, linha].Value = r.CapacidadePaletes;
                           dgv_GesCapArm[15, linha].Value = r.CapacidadeCaixas;

                           dgv_GesCapArm[16, linha].Value = r.Proporcional;
                       }
                       catch (ArgumentOutOfRangeException ex)
                       {
                           MessageBox.Show("Erro: " + ex.Message + "\nvalor actual: " + ex.ActualValue + "\n" + ex.ParamName + "\naqui=" + ex.InnerException);
                       }
                   }
                   else { MessageBox.Show("Impossivel atualizar: Peça não existe na base de dados!"); return; }

               }
               else
               {

                   Capacidade t = new Capacidade();
                   t.CIP1 = cb_CIP.SelectedValue.ToString();
                   t.CMAA = Convert.ToInt32(nud_CMAA.Value);
                   t.CMAA_P = Convert.ToInt32(nud_CMAAP.Value);
                   t.CMAA_C = Convert.ToInt32(nud_CMAACaixas.Value);


                   t.CMAE = Convert.ToInt32(nud_CMAA.Value);
                   t.CMAE_P = Convert.ToInt32(nud_CMAAP.Value);
                   t.CMAE_C = Convert.ToInt32(nud_CMAACaixas.Value);




                   t.IOE = Convert.ToInt32(nud_IOE.Value);
                   t.IOE_P = Convert.ToInt32(nud_IOEPaletes.Value);
                   t.IOE_C = Convert.ToInt32(nud_IOECaixas.Value);


                   t.Ncarrinhos = Convert.ToInt32(nud_QCarrinhos.Value);
                   t.NPaletes = Convert.ToInt32(nud_QPaletes.Value);
                   t.NCaixas = Convert.ToInt32(nud_NCaixas.Value);

                   t.CapacidadeCarrinhos = Convert.ToInt32(nud_CC.Value);
                   t.CapacidadePaletes = Convert.ToInt32(nud_CP.Value);
                   t.CapacidadeCaixas = Convert.ToInt32(nud_CapCaixas.Value);

                   if (ch_Proporcional.Checked)
                   {

                       t.Proporcional = "Sim";
                   }
                   else
                   {
                       t.Proporcional = "Não";
                   }


                   dt = Banco.ObterTodosOnde("Armazenamento", "CIP", "'" + t.CIP1 + "'");



                   try
                    {
                        if (dt.Rows.Count == 0)
                         {

                              linha = dgv_GesCapArm.SelectedRows[0].Index;
                              Banco.Salvar("Armazenamento", " CIP, CMAA,'CMAA-P','CMAA-C', IOE,'IOE-P','IOE-C', CMAE,'CMAE-P','CMAE-C', 'N-Carrinhos', 'N-Paletes', 'N-Caixas', 'Cap. Carrinhos', 'Cap. Paletes', 'Cap. Caixas', P", "'" + t.CIP1 + "', " + t.CMAA + "," + t.CMAA_P + ", " + t.CMAA_C + ",  " + t.IOE + ", " + t.IOE_P + ", " + t.IOE_C + ", " + t.CMAE + ", " + t.CMAE_P + ", " + t.CMAE_C + ",  " + t.Ncarrinhos + ", " + t.NPaletes + ", " + t.NCaixas + ", " + t.CapacidadeCarrinhos +  ", " + t.CapacidadePaletes + ", " + t.CapacidadeCaixas + ", '" + t.Proporcional + "'");
                              dgv_GesCapArm.DataSource = null;
                              dgv_GesCapArm.DataSource = Banco.ObterTodos("Armazenamento", "Peças", "Peças.NomeCIP,Armazenamento.CMAA,Armazenamento.'CMAA-P', Armazenamento.'CMAA-C',Armazenamento.CMAE,Armazenamento.'CMAE-P',Armazenamento.'CMAE-C',Armazenamento.IOE,Armazenamento.'IOE-P', Armazenamento.'IOE-C',Armazenamento.'N-Carrinhos',Armazenamento.'N-Paletes',Armazenamento.'N-Caixas',Armazenamento.'Cap. Carrinhos',Armazenamento.'Cap. Paletes', Armazenamento.'Cap. Caixas', P", "CIP", "Peças.CIP");
                           Cambio = true;
                           cb_Familia.SelectedIndex = 0;
                         }
                         else { MessageBox.Show("Não e possivel Cadastrar, esta peça já existe!"); cb_CIP.Focus(); }
                    }
                     catch (ArgumentOutOfRangeException ex)
                        {
                           MessageBox.Show("Erro: " + ex.Message);
                        }



                   btn_Atualizar.Text = "Atualizar";
                   btn_Novo.Enabled = true;
                   btn_Excluir.Enabled = true;

               }
           }

           private void btn_Excluir_Click(object sender, EventArgs e)
           {
               Banco.Excluir("Armazenamento", "NomeCIP", IdSeleccionados);
           }

           private void btn_C_Click(object sender, EventArgs e)
           {
               btn_Atualizar.Text = "Atualizar";
               btn_Excluir.Enabled = true;

               cb_CIP.SelectedIndex = ind;

               nud_CMAA.Value = c.CMAA;
               nud_CMAAP.Value = c.CMAA_P;
               nud_CMAACaixas.Value = c.CMAA_C;

               nud_CMAE.Value = c.CMAE;
               nud_CMAEP.Value = c.CMAE_P;
               nud_CMAECaixas.Value = c.CMAE_C;

               nud_IOE.Value = c.IOE;
               nud_IOEPaletes.Value = c.IOE_P;
               nud_IOECaixas.Value = c.IOE_C;

               nud_QCarrinhos.Value = c.Ncarrinhos;
               nud_QPaletes.Value = c.NPaletes;
               nud_NCaixas.Value = c.NCaixas;

               nud_CC.Value = c.CapacidadeCarrinhos;
               nud_CP.Value = c.CapacidadePaletes;
               nud_CapCaixas.Value = c.CapacidadeCaixas;

               ch_Proporcional.Checked = pc;

               btn_Novo.Enabled = true;
               btn_C.Enabled = false;
           }

           private void Btn_Voltar_Click(object sender, EventArgs e)
           {
               this.Close();
           }

           private void Btn_Sair_Click(object sender, EventArgs e)
           {
               Globais.Sair();
           }

           private void cb_CIP_SelectedIndexChanged(object sender, EventArgs e)
           {
               btn_C.Enabled = true;
           }

           private void cb_Familia_SelectedIndexChanged(object sender, EventArgs e)
           {
               btn_C.Enabled = true;

               //if (dgv_GesCapArm.Rows.Count > 0)
               //{

                   //if (cb_Familia.SelectedIndex == 0)
                  //{
                   //    cb_Familia.Text = "Todos";
                   //}

                   //dt = Banco.ProcurarcomInner("Armazenamento", "Peças", "Peças.NomeCIP,Armazenamento.CMAA,Armazenamento.'CMAA-P', Armazenamento.'CMAA-C',Armazenamento.CMAE,Armazenamento.'CMAE-P',Armazenamento.'CMAE-C',Armazenamento.IOE,Armazenamento.'IOE-P', Armazenamento.'IOE-C',Armazenamento.'N-Carrinhos',Armazenamento.'N-Paletes',Armazenamento.'N-Caixas',Armazenamento.'Cap. Carrinhos',Armazenamento.'Cap. Paletes', Armazenamento.'Cap. Caixas', Armazenamento.P", "CIP", "Peças.NomeCIP", "'" + cb_Familia.Text + "%'");

               //}
           }


           private void nud_CMAA_ValueChanged(object sender, EventArgs e)
           {
               btn_C.Enabled = true;
               nud_CMAE.Value = nud_CMAA.Value * (nud_IOE.Value/100);
               nud_CC.Value = nud_CMAE.Value * nud_QCarrinhos.Value;
           }

           private void nud_IOE_ValueChanged(object sender, EventArgs e)
           {
               btn_C.Enabled = true;
               nud_CMAE.Value = nud_CMAA.Value * (nud_IOE.Value / 100);
               nud_CC.Value = nud_CMAE.Value * nud_QCarrinhos.Value;

           }

           private void nud_CMAE_ValueChanged(object sender, EventArgs e)
           {
               btn_C.Enabled = true;
               nud_CC.Value = nud_CMAE.Value * nud_QCarrinhos.Value;
           }

           private void nud_QCarrinhos_ValueChanged(object sender, EventArgs e)
           {
               btn_C.Enabled = true;
               nud_CC.Value = nud_CMAE.Value * nud_QCarrinhos.Value;
           }

           private void nud_QPaletes_ValueChanged(object sender, EventArgs e)
           {
               btn_C.Enabled = true;
               nud_CP.Value = nud_CMAEP.Value * nud_QPaletes.Value;

           }

           private void nud_CMAEP_ValueChanged(object sender, EventArgs e)
           {
               btn_C.Enabled = true;
               nud_CP.Value = nud_CMAEP.Value * nud_QPaletes.Value;
           }

           private void nud_CMAAP_ValueChanged(object sender, EventArgs e)
           {
               btn_C.Enabled = true;

               nud_CMAEP.Value = nud_CMAAP.Value * (nud_IOEPaletes.Value / 100);

               nud_CP.Value = nud_CMAEP.Value * nud_QPaletes.Value;

           }

           private void nud_CMAACaixas_ValueChanged(object sender, EventArgs e)
           {
               btn_C.Enabled = true;

               nud_CMAECaixas.Value = nud_CMAACaixas.Value * (nud_IOECaixas.Value / 100);

               nud_CapCaixas.Value = nud_CMAECaixas.Value * nud_NCaixas.Value;

           }

           private void nud_IOECaixas_ValueChanged(object sender, EventArgs e)
           {
               btn_C.Enabled = true;

               nud_CMAECaixas.Value = nud_CMAACaixas.Value * (nud_IOECaixas.Value / 100);

               nud_CapCaixas.Value = nud_CMAECaixas.Value * nud_NCaixas.Value;

           }
               private void nud_IOEPaletes_ValueChanged(object sender, EventArgs e)
           {
               btn_C.Enabled = true;
               nud_CMAEP.Value = nud_CMAAP.Value * (nud_IOEPaletes.Value / 100);
               nud_CP.Value = nud_CMAEP.Value * nud_QPaletes.Value;
           }

           private void nud_CMAECaixas_ValueChanged(object sender, EventArgs e)
           {
               btn_C.Enabled = true;
               nud_CapCaixas.Value = nud_CMAECaixas.Value * nud_NCaixas.Value;
           }

           private void nud_NCaixas_ValueChanged(object sender, EventArgs e)
           {
               btn_C.Enabled = true;
               nud_CapCaixas.Value = nud_CMAECaixas.Value * nud_NCaixas.Value;
           }

           private void ch_ProporcionalCarrinhos_CheckedChanged(object sender, EventArgs e)
           {
               //if (ch_Proporcional.Checked)
              // {
               //    c.Proporcional = "Sim";
              // }
              // else
              // {
              //     c.Proporcional = "Não";
             //  }
           }

           private void cb_Familia_SelectedValueChanged(object sender, EventArgs e)
           {
               if (dgv_GesCapArm.Rows.Count > 0)
               {

                   dt = Banco.ProcurarcomInner("Armazenamento", "Peças", "Peças.NomeCIP,Armazenamento.CMAA,Armazenamento.'CMAA-P', Armazenamento.'CMAA-C',Armazenamento.CMAE,Armazenamento.'CMAE-P',Armazenamento.'CMAE-C',Armazenamento.IOE,Armazenamento.'IOE-P', Armazenamento.'IOE-C',Armazenamento.'N-Carrinhos',Armazenamento.'N-Paletes',Armazenamento.'N-Caixas',Armazenamento.'Cap. Carrinhos',Armazenamento.'Cap. Paletes', Armazenamento.'Cap. Caixas', Armazenamento.P", "CIP", "Peças.NomeCIP", "'" + cb_Familia.Text+ "%'");
                   if (dt.Rows.Count > 0) 
                   {
                       dgv_GesCapArm.DataSource = dt;
                       cb_CIP.DataSource = Banco.Procurar("Peças","*", "NomeCIP", "'"+cb_Familia.Text+"%'", "CIP");
                       Cambio = true;
                   }
                   else { MessageBox.Show("Não há nenhuma peça do tipo procurado"); }
               }
            }

           private void button1_Click(object sender, EventArgs e)
           {

           }

           private void button3_Click(object sender, EventArgs e)
           {

           }

           private void button2_Click(object sender, EventArgs e)
           {

           }
             */
    }

}
