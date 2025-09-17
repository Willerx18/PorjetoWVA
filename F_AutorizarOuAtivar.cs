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
    public partial class F_AutorizarOuAtivar : Form
    {
        readonly Reforma F;
        public F_AutorizarOuAtivar(Reforma f)
        {
            InitializeComponent();
            F = f;
        }


        private void btn_login_Click_1(object sender, EventArgs e)
        {
            DataTable dt;

            if (tb_Usuario.Text == "" || mtb_Senha.Text == "")
            {
                MessageBox.Show("Os Campos de \"Usurio\" ou \"Senha\" não podem estar vazios");
                tb_Usuario.Focus();
                return;
            }
            else
            {
                dt = Banco.ObterTodosOnde("Usuarios", "Usuario", "'" + tb_Usuario.Text + "'");
                if (dt.Rows.Count == 1)
                {   string senha = dt.Rows[0].Field<string>("Senha");
                    string nome = dt.Rows[0].Field<string>("Nome");
                    if (senha == mtb_Senha.Text && F.lb_NomeUsuario1.Text != nome)
                    {
                        F.tb_CódigoDefeito.Enabled = true;
                        F.cb_DefeitoGeral.Enabled = true;
                        F.cb_FamilaPeça.Enabled = true;
                        //F.cb_FamilaPeça.SelectedIndex = 0;
                        F.lb_NomeUsuario2.Text = nome;
                        F.lb_NomeUsuario2.Enabled = true;
                        F.cb_Clasificação.Enabled = true;
                        F.btn_Apontar.Enabled = true;
                        F.btn_Exluir.Enabled = true;
                        F.tb_Codigo1.Enabled = true;
                        F.puedeActivar = true;
                        this.Close();
                    }
                    else { MessageBox.Show("Não Autorizado:\nCredenciais Incorretas"); F.puedeActivar = false; }
                }
                else { MessageBox.Show("Não Autorizado:\nUsuario Não Cadastrado"); F.puedeActivar = false; }
            }
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                mtb_Senha.PasswordChar = '\u0000';
            }
            else
            {
                mtb_Senha.PasswordChar = '*';
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            F.btn_Habilitar.Text = "Habilitar Usuario 2";
            F.controlBtn = true;
            this.Close();
        }

    }

}
