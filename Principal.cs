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
    public partial class F_Principal : Form
    {
        public F_Principal()
        {
            InitializeComponent();
          
        }

        private void taxaDeSaidaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void F_Principal_Load(object sender, EventArgs e)
        {
           

        }

        private void mi_login_Click(object sender, EventArgs e)
        {
            mi_login.Enabled = false;
            mi_Logoff.Enabled = true;
            F_Login f_Login = new F_Login(this);
            
            f_Login.ShowDialog();

          
        }

        private void mi_Logoff_Click(object sender, EventArgs e)
        {
            Globais.logado = false;
            Pb_led.Image = Properties.Resources.LuzVermelha;
            lb_nivel.Text = "0";
            lb_nome.Text = "--";
            mi_login.Enabled = true;
            mi_Logoff.Enabled = false;
        }

        private void verUsuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            F_GestãoDeUsuarios f_GestãoDeUsuarios = new F_GestãoDeUsuarios(this);
           // this.Hide();
            Globais.Abreform(2, f_GestãoDeUsuarios, 5);
            
        }

        private void peçasToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void modelosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            F_Peças f_Peças = new F_Peças("Esmaltação");
           // this.Hide();
            Globais.Abreform(2,f_Peças,5);
        }

        private void capaciadeDeArmazenamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            F_CapacidadeDeArmazenamento f_Capacidade = new F_CapacidadeDeArmazenamento();
            Globais.Abreform(4, f_Capacidade,5);
        }

        private void nivelesDeEstoqueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            F_NiveisDeEstoque f_NiveisDeEstoque = new F_NiveisDeEstoque("DESCARGA","Esmaltação");
            if (UserCache.AreaDeTrabalho==5 )
            {
                Globais.Abreform(2, f_NiveisDeEstoque, 5);
            }
            else
            {
                if (UserCache.AreaDeTrabalho == 4)
                {
                    Globais.Abreform(0, f_NiveisDeEstoque, 4);
                }
                else
                {
                    if (UserCache.AreaDeTrabalho == 3)
                    {
                        Globais.Abreform(0, f_NiveisDeEstoque, 3);
                    }
                    else { MessageBox.Show("Acesso Negado: Sua Área de Trabalho não da acceso a esta função"); }
                }
            }
           
        }
        
        private void retrabalhoToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void diariaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void gestionarEstoquesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            F_InventarioContenedorSaidaEEntradas f_InventarioContenedor = new F_InventarioContenedorSaidaEEntradas(new Form2(),"Esmaltação");
            Globais.Abreform(2, f_InventarioContenedor);
        }

        private void gestãoDeCapacidadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            F_CadastroEControleDeContenedores f_CadastroEControleDeContenedores = new F_CadastroEControleDeContenedores();
            Globais.Abreform(3, f_CadastroEControleDeContenedores);
        }

        private void reformaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            RelatoriosDeSaidaReformaSalvos f_CadastroEControleDeRetrabalho = new RelatoriosDeSaidaReformaSalvos("R");
            if (UserCache.AreaDeTrabalho==3)
            {
                Globais.Abreform(0, f_CadastroEControleDeRetrabalho, 3);
            }
            else
            {
                if (Globais.AreaDeTrabalho==5)
                {
                    Globais.Abreform(0, f_CadastroEControleDeRetrabalho, 5);
                }
                else { MessageBox.Show("Acesso Negado: Sua Área de Trabalho não da acceso a esta função"); }
        
            
            }

        

        }

        private void sucataToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            F_GestãoEControleDeRetrablho f_CadastroEControleDeRetrabalho = new F_GestãoEControleDeRetrablho(2, new Form2());
                        
            if (UserCache.AreaDeTrabalho == 3)
            {
                Globais.Abreform(0, f_CadastroEControleDeRetrabalho, 3);
            }
            else
            {
                if (Globais.AreaDeTrabalho == 5)
                {
                    Globais.Abreform(0, f_CadastroEControleDeRetrabalho, 5);
                }
                else { MessageBox.Show("Acesso Negado: Sua Área de Trabalho não da acceso a esta função"); }


            }

            
        }

        private void boasToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            F_GestãoEControleDeRetrablho f_CadastroEControleDeRetrabalho = new F_GestãoEControleDeRetrablho(3, new Form2());
           
            if (UserCache.AreaDeTrabalho == 3)
            {
                Globais.Abreform(0, f_CadastroEControleDeRetrabalho, 3);
            }
            else
            {
                if (UserCache.AreaDeTrabalho == 5)
                {
                    Globais.Abreform(0, f_CadastroEControleDeRetrabalho, 5);
                }
                else { MessageBox.Show("Acesso Negado: Sua Área de Trabalho não da acceso a esta função"); }


            }
        }

        private void atualToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            F_InventarioContenedorSaidaEEntradas f_InventarioContenedor = new F_InventarioContenedorSaidaEEntradas(new Form2(), "Esmaltação");
           
               
                    Globais.Abreform(0, f_InventarioContenedor, 5);
                


        }

        private void verRelatoriosSalvosToolStripMenuItem_Click(object sender, EventArgs e)
        {
          RelatoriosDeSaidaReformaSalvos f_Relatorio = new RelatoriosDeSaidaReformaSalvos("R");
            
            Globais.Abreform(1,f_Relatorio);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
