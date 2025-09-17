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
    public partial class F_EspecificarEStadoCondição : Form
    {
        F_CadastroEControleDeContenedores F;
        int TotalDisponiveis;
        int TotalEmUso;
        int TotalEmManutençao;
        int TotalForadeUso;       
        int TotalOtimas;
        int TotalDeteriorados;
        int TotalDanificados;
       

        public F_EspecificarEStadoCondição(F_CadastroEControleDeContenedores f)
        {
            InitializeComponent();
            F = f;
            F.Assignou = false;
        }

        private void F_EspecificarEStadoCondição_Load(object sender, EventArgs e)
        {
            TotalDisponiveis = Convert.ToInt32(F.nud_E0.Value);
            TotalEmUso = Convert.ToInt32(F.nud_E1.Value);
            TotalEmManutençao = Convert.ToInt32(F.nud_E2.Value);
            TotalForadeUso = Convert.ToInt32(F.nud_E3.Value);
            TotalOtimas = Convert.ToInt32(F.nud_C0.Value);
            TotalDeteriorados = Convert.ToInt32(F.nud_C1.Value);
            TotalDanificados = Convert.ToInt32(F.nud_C2.Value);

            lb_02.Text = TotalDisponiveis.ToString();
            lb_12.Text = TotalEmUso.ToString();
            lb_22.Text = TotalEmManutençao.ToString();
            lb_32.Text = TotalForadeUso.ToString();
            lb_C0t.Text=TotalOtimas.ToString();
            lb_C1t.Text =  TotalDeteriorados.ToString();
            lb_C2t.Text = TotalDanificados.ToString();
                       
            
            if (TotalOtimas==0)
            {
                nud_00.Enabled = nud_10.Enabled =  nud_30.Enabled = false;
                lb_C0.Text = "0";
                groupBox1.Enabled = false;
               
            }

            if (TotalDeteriorados == 0)
            {
                nud_01.Enabled = nud_11.Enabled = nud_21.Enabled = nud_31.Enabled = false;
                lb_C1.Text = "0";
                gb_DeterioradasAtribuidas.Enabled = false;
            }

            if (TotalDanificados == 0)
            {
                nud_02.Enabled = nud_12.Enabled = nud_22.Enabled = nud_32.Enabled = false;
                lb_C2.Text = "0";
                gb_DanificadasAtribuidas.Enabled = false;
            }

            if (TotalDisponiveis==0)
            {
                gb_Disponiveis.Enabled= false;
                gb_Disponiveis.BackColor = Color.Transparent;
                lb_00.Text = 0.ToString();
            }
            else
            {                
                lb_00.Text = (TotalDisponiveis - (TotalDisponiveis - (nud_00.Value + nud_01.Value + nud_02.Value))).ToString();
            }

            if (TotalEmUso == 0)
            {
                gb_EmUso.Enabled = false;
                gb_EmUso.BackColor = Color.Transparent;
                lb_10.Text = 0.ToString();
            }
            else
            {                           
                lb_10.Text = (TotalEmUso - (TotalEmUso - (nud_10.Value + nud_11.Value + nud_12.Value))).ToString();
            }

            if (TotalEmManutençao == 0)
            {
                gb_EmManutenção.Enabled = false;
                gb_EmManutenção.BackColor = Color.Transparent;
                lb_20.Text = 0.ToString();
            }
            else
            {               
                lb_20.Text = (TotalEmManutençao - (TotalEmManutençao - ( nud_21.Value + nud_22.Value))).ToString();

            }

            if (TotalForadeUso == 0)
            {
                gb_ForadeUSo.Enabled = false;
                gb_ForadeUSo.BackColor = Color.Transparent;
                lb_30.Text = 0.ToString();
            }
            else
            {               
                lb_30.Text = (TotalForadeUso - (TotalForadeUso - (nud_30.Value + nud_31.Value + nud_32.Value))).ToString();

            }           
            

        }
        

        #region PROCEDIMIENTOS

        private void CalcularLimites(NumericUpDown x, int totalEstado, int totalCondição, Label restaCondição, CheckBox ValorMax0, CheckBox ValorMax1, CheckBox ValorMax2, CheckBox ValorMax3, Label RestanEstado,Label Barra, Label T_Estado, NumericUpDown nud_C1, NumericUpDown nud_C2, NumericUpDown nud_C3, NumericUpDown nud_C4, NumericUpDown nud_E1, NumericUpDown nud_E2, NumericUpDown nud_E3)
        {
           decimal LimiteEstado = (totalEstado - (nud_E1.Value+ nud_E2.Value + nud_E3.Value)) + x.Value;
           decimal LimiteCondição = (totalCondição - (nud_C1.Value + nud_C2.Value + nud_C3.Value + nud_C4.Value)) + x.Value;
           // MessageBox.Show("LILIMITES\n\n LimiteEstado= "+LimiteEstado+ "\n\nLimiteCondição "+ LimiteCondição+"\n\nTOTAIS\nTC: "+ totalCondição+"    TE: "+ totalEstado+"\n\nVALUES\n\nC1: "+ nud_C1.Value +"   C2: "+ nud_C2.Value +"   C3: " +nud_C3.Value +"   C4: "+ nud_C4.Value+"\n\nE1:  "+ nud_E1.Value + "   E2: "+nud_E2.Value +"E3: "+ nud_E3.Value);
           

            if ((LimiteEstado - x.Value) < 1 || (LimiteCondição - x.Value) < 1)
            {
                if (LimiteCondição > LimiteEstado || LimiteCondição == LimiteEstado)
                {
                    x.Maximum = LimiteEstado;
                    x.Value = LimiteEstado;
                    restaCondição.Text = (totalCondição - (LimiteCondição - x.Value)).ToString();
                    RestanEstado.Text = totalEstado.ToString();
                    RestanEstado.BackColor = Barra.BackColor = T_Estado.BackColor = Color.LightGreen;
                    
                  
                }
                else
                {
                    x.Maximum = LimiteCondição;
                    x.Value = LimiteCondição;                 
                    ValorMax0.Checked = ValorMax1.Checked = ValorMax2.Checked = ValorMax3.Checked = true;
                    RestanEstado.Text = (totalEstado - (LimiteEstado - x.Value)).ToString();
                    restaCondição.Text = totalCondição.ToString();
                    
                }

                if ((LimiteCondição - x.Value) < 1)
                {
                    ValorMax0.Checked = ValorMax1.Checked = ValorMax2.Checked = ValorMax3.Checked = true;
                }
              
               
            }
            else
            {
                ValorMax0.Checked = ValorMax1.Checked = ValorMax2.Checked = ValorMax3.Checked = false;
                if (LimiteCondição > LimiteEstado || LimiteCondição == LimiteEstado)
                {
                    nud_C1.Maximum = nud_C2.Maximum = nud_C3.Maximum = nud_C4.Maximum = LimiteEstado;
                    restaCondição.Text = (totalCondição- (LimiteCondição-x.Value)).ToString();
                    RestanEstado.Text = (totalEstado - (LimiteEstado - x.Value)).ToString();
                    RestanEstado.BackColor = Barra.BackColor = T_Estado.BackColor = Color.Salmon;
                   

                }
                else
                {
                    nud_C1.Maximum = nud_C2.Maximum = nud_C3.Maximum = nud_C4.Maximum  = LimiteCondição;
                    restaCondição.Text  = (totalCondição - (LimiteCondição - x.Value)).ToString();
                    RestanEstado.Text = (totalEstado - (LimiteEstado - x.Value)).ToString();
                   
                }

                if ((LimiteCondição - x.Value) < 1)
                {
                    ValorMax0.Checked = ValorMax1.Checked = ValorMax2.Checked = ValorMax3.Checked = true;
                }
            }
        }
        private void EstablecerValorDeCondiçoesEnFUnciondeEstado(NumericUpDown x)
        {
                     //DISPONIVEIS
            if (x==nud_00|| x== nud_01|| x == nud_02)//Disponiveis
            {   
                //otimas Disponiveis
                if (x== nud_00)
                {
                    CalcularLimites(x, TotalDisponiveis, TotalOtimas, lb_C0, ch_00,ch_10,nulo ,ch_30 , lb_00,lb_01,lb_02, nud_00, nud_10, nud0, nud_30, nud_00, nud_01, nud_02);                                      
                }
                else
                {//deterioradas Disponiveis
                    if (x == nud_01)
                    {
                        CalcularLimites(x, TotalDisponiveis, TotalDeteriorados, lb_C1,ch_01, ch_11, ch_21, ch_31, lb_00, lb_01, lb_02, nud_01, nud_11, nud_21, nud_31, nud_00, nud_01, nud_02);
                       
                    }
                    else
                    {//Danificadas Disponiveis
                        if (x == nud_02)
                        {
                            CalcularLimites(x, TotalDisponiveis, TotalDanificados, lb_C2, ch_02, ch_12, ch_22, ch_32, lb_00, lb_01, lb_02, nud_02, nud_12, nud_22, nud_32, nud_00, nud_01, nud_02);

                        }

                    }
                }

            }
            else
            {           //EM USO
                if (x == nud_10 || x == nud_11 || x == nud_12)//EM USO
                {
                    //otimas Em uso
                    if (x == nud_10)
                    {
                        CalcularLimites(x, TotalEmUso, TotalOtimas, lb_C0,  ch_00, ch_10, nulo, ch_30, lb_10, lb_11, lb_12, nud_00, nud_10, nud0, nud_30, nud_10, nud_11, nud_12);
                         
                    }
                    else
                    {//deterioradas Em uso
                        if (x == nud_11)
                        {
                            CalcularLimites(x, TotalEmUso, TotalDeteriorados, lb_C1, ch_01, ch_11, ch_21, ch_31, lb_10, lb_11, lb_12, nud_01, nud_11, nud_21, nud_31, nud_10, nud_11, nud_12);
                        }
                        else
                        {//Danificadas Em USo
                            if (x == nud_12)
                            {
                                CalcularLimites(x, TotalEmUso, TotalDanificados, lb_C2, ch_02, ch_12, ch_22, ch_32, lb_10, lb_11, lb_12, nud_02, nud_12, nud_22, nud_32, nud_10, nud_11, nud_12);

                            }
                        }
                    }
                }
                else
                {             //EM MANUTENÇÂO
                    if ( x == nud_21 || x == nud_22 )
                    {
                        //EmManutençao  otimas NÂO EXISTE solo flojera de quitar ajajja
                        if (x == nud_21)
                        {
                            CalcularLimites(x, TotalEmManutençao, TotalDeteriorados, lb_C1, ch_01, ch_11, ch_21, ch_31, lb_20, lb_21, lb_22, nud_01, nud_11, nud_21, nud_31, nud0, nud_21, nud_22);

                        }
                        else
                        {//deterioradas Em Mmanutemção
                            if (x == nud_22)
                            {
                                CalcularLimites(x, TotalEmManutençao, TotalDanificados, lb_C2, ch_02, ch_12, ch_22, ch_32, lb_20, lb_21, lb_22, nud_02, nud_12, nud_22, nud_32, nud0, nud_21, nud_22);

                            }

                        }
                    }
                    else
                    {       //FORA DE USO
                        if (x == nud_30 || x == nud_31 || x == nud_32 )
                        {
                            //otimas FORA DE USO
                            if (x == nud_30)
                            {
                                CalcularLimites(x, TotalForadeUso, TotalOtimas, lb_C0, ch_00, ch_10, nulo, ch_30, lb_30, lb_31, lb_32, nud_00, nud_10, nud0, nud_30, nud_30, nud_31, nud_32);

                            }
                            else
                            {//deterioradas FORA DE USO
                                if (x == nud_31)
                                {
                                    CalcularLimites(x, TotalForadeUso, TotalDeteriorados, lb_C1, ch_01, ch_11, ch_21, ch_31, lb_30, lb_31, lb_32, nud_01, nud_11, nud_21, nud_31, nud_30, nud_31, nud_32);

                                }
                                else
                                {//Danificadas FORA DE USO
                                    if (x == nud_32)
                                    {
                                        CalcularLimites(x, TotalForadeUso, TotalDanificados, lb_C2, ch_02, ch_12, ch_22, ch_32, lb_30, lb_31, lb_32, nud_02, nud_12, nud_22, nud_32, nud_30, nud_31, nud_32);

                                    }

                                }
                            }
                        }
                        
                    }
                }
            }




           
        }
        private void Atualizarnuds(NumericUpDown x)
        {
           
            
            if (x==nud_00|| x == nud_01 || x == nud_02)
            {
                EstablecerValorDeCondiçoesEnFUnciondeEstado(x);
            }
            else
            {
                if (x == nud_10 || x == nud_11 || x == nud_12)
                {
                    EstablecerValorDeCondiçoesEnFUnciondeEstado(x);
                }
                else
                {
                    if ( x == nud_21 || x == nud_22)
                    {
                        EstablecerValorDeCondiçoesEnFUnciondeEstado(x);


                    }
                    else
                    {
                        if (x == nud_30 || x == nud_31 || x == nud_32)
                        {
                            EstablecerValorDeCondiçoesEnFUnciondeEstado(x);


                        }
                    }
                }
            }

         



        }

        private bool Falta()
        {
            bool Falta = false;
            if (lb_00.Text == lb_02.Text)
            {
                E0.Text = "COMPLETAS";
            }
            else
            {
                E0.Text = "FALTAM";
                Falta = true;
            }

            if (lb_10.Text == lb_12.Text)
            {
                E1.Text = "COMPLETAS";
            }
            else
            {
                E1.Text = "FALTAM";
                Falta = true;
            }

            if (lb_20.Text == lb_22.Text)
            {
                E2.Text = "COMPLETAS";
            }
            else
            {
                E2.Text = "FALTAM";
                Falta = true;
            }

            if (lb_30.Text == lb_32.Text)
            {
                E3.Text = "COMPLETAS";
            }
            else
            {
                E3.Text = "FALTAM";
                Falta = true;
            }

            if (lb_C0.Text == lb_C0t.Text)
            {
                C0.Text = "COMPLETAS";
            }
            else
            {
                C0.Text = "FALTAM";
                Falta = true;
            }

            if (lb_C1.Text == lb_C1t.Text)
            {
                C1.Text = "COMPLETAS";
            }
            else
            {
                C1.Text = "FALTAM";
                Falta = true;
            }

            if (lb_C2.Text == lb_C2t.Text)
            {
                C2.Text = "COMPLETAS";
            }
            else
            {
                C2.Text = "FALTAM";
                Falta = true;
            }

            return Falta;
        }       
        
        #endregion;

        #region CAMBIOS DE SELECCION
        private void nud_00_ValueChanged(object sender, EventArgs e)
        {
            Atualizarnuds((NumericUpDown)sender);
        }

        private void nud_01_ValueChanged(object sender, EventArgs e)
        {
            Atualizarnuds((NumericUpDown)sender);
        }

        private void nud_02_ValueChanged(object sender, EventArgs e)
        {
            Atualizarnuds((NumericUpDown)sender);
        }

        private void nud_10_ValueChanged(object sender, EventArgs e)
        {
            Atualizarnuds((NumericUpDown)sender);
        }

        private void nud_11_ValueChanged(object sender, EventArgs e)
        {
            Atualizarnuds((NumericUpDown)sender);
        }

        private void nud_12_ValueChanged(object sender, EventArgs e)
        {
            Atualizarnuds((NumericUpDown)sender);
        }

        private void nud_20_ValueChanged(object sender, EventArgs e)
        {
            Atualizarnuds((NumericUpDown)sender);
        }

        private void nud_21_ValueChanged(object sender, EventArgs e)
        {
            Atualizarnuds((NumericUpDown)sender);
        }

        private void nud_22_ValueChanged(object sender, EventArgs e)
        {
            Atualizarnuds((NumericUpDown)sender);
        }

        private void nud_30_ValueChanged(object sender, EventArgs e)
        {
            Atualizarnuds((NumericUpDown)sender);
        }

        private void nud_31_ValueChanged(object sender, EventArgs e)
        {
            Atualizarnuds((NumericUpDown)sender);
        }

        private void nud_32_ValueChanged(object sender, EventArgs e)
        {
            Atualizarnuds((NumericUpDown)sender);
        }
        #endregion;

        #region BOtones CLICK
        private void btn_Pronto_Click(object sender, EventArgs e)
        {
            if (!Falta())//si atribui todas correctamente
            {
                for (int Estado = 0; Estado < 4; Estado++)
                {
                    for (int Condição = 0; Condição < 3; Condição++)
                    {
                        switch (Estado)
                        {
                            case 0:
                                switch (Condição)
                                {
                                    case 0:
                                        F.CICE_Condiçoes[Estado, Condição] = Convert.ToInt32(nud_00.Value);
                                        break;
                                    case 1:
                                        F.CICE_Condiçoes[Estado, Condição] = Convert.ToInt32(nud_01.Value);
                                        break;
                                    case 2:
                                        F.CICE_Condiçoes[Estado, Condição] = Convert.ToInt32(nud_02.Value);
                                        break;

                                }
                                break;

                            case 1:
                                switch (Condição)
                                {
                                    case 0:
                                        F.CICE_Condiçoes[Estado, Condição] = Convert.ToInt32(nud_10.Value);
                                        break;
                                    case 1:
                                        F.CICE_Condiçoes[Estado, Condição] = Convert.ToInt32(nud_11.Value);
                                        break;
                                    case 2:
                                        F.CICE_Condiçoes[Estado, Condição] = Convert.ToInt32(nud_12.Value);
                                        break;

                                }

                                break;
                            case 2:
                                switch (Condição)
                                {
                                    case 0:
                                        F.CICE_Condiçoes[Estado, Condição] = Convert.ToInt32(0);
                                        break;
                                    case 1:
                                        F.CICE_Condiçoes[Estado, Condição] = Convert.ToInt32(nud_21.Value);
                                        break;
                                    case 2:
                                        F.CICE_Condiçoes[Estado, Condição] = Convert.ToInt32(nud_22.Value);
                                        break;

                                }
                                break;
                            case 3:
                                switch (Condição)
                                {
                                    case 0:
                                        F.CICE_Condiçoes[Estado, Condição] = Convert.ToInt32(nud_30.Value);
                                        break;
                                    case 1:
                                        F.CICE_Condiçoes[Estado, Condição] = Convert.ToInt32(nud_31.Value);
                                        break;
                                    case 2:
                                        F.CICE_Condiçoes[Estado, Condição] = Convert.ToInt32(nud_32.Value);
                                        break;

                                }
                                break;
                        }

                    }
                }
                F.Assignou = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("DEVE ASSIGNAR TODOS OS CAMPOS\n\nTem algum campo não prenechido corretamente:\n\n ESTADOS:\n\n"+"Disponiveis: "+ E0.Text + "\nEm Uso: "+ E1.Text + "\nEm Manutenção: " + E2.Text + "\nFora de Uso: " + E3.Text + "\n\nCONDIÇÕES\n\nOTIMAS: " + C0.Text + "\nDeterioradas: " + C1.Text + "\nDanificadas: " + C2.Text);
               

                F.Assignou = false;
            }
        }

        private void btn_Cancelar_Click(object sender, EventArgs e)
        {
            F.Assignou = false;
            this.Close();
        }
        #endregion;

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void gb_EmUso_Enter(object sender, EventArgs e)
        {

        }

        private void gb_EmManutenção_Enter(object sender, EventArgs e)
        {

        }

    }
}
