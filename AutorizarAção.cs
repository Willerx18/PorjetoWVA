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
   
    public partial class AutorizarAção : Form
    {
        readonly dynamic F;
        public AutorizarAção(dynamic f)
        {
            InitializeComponent();
            F = f;
        }

        

        private void btn_login_Click(object sender, EventArgs e)
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
                if (dt.Rows.Count>0)
                {
                    string senha = dt.Rows[0].Field<string>("Senha");
                    string nome = dt.Rows[0].Field<string>("Nome");
                    int Nivel = (int)dt.Rows[0].Field<Int64>("Nivel");
                    if (dt.Rows.Count == 1 && senha == mtb_Senha.Text &&  Nivel >= 2)
                    {
                        F.AçãoAutorizada = true;
                        this.Close();
                      
                    }
                    else { MessageBox.Show("Ação Não Autorizada:\nCredenciais invalidas ou Nivel Insuficiente"); F.AçãoAutorizada = false; }
                }
                else
                {
                    MessageBox.Show("Ação Não Autorizada:\nCredenciais invalidas ou Nivel Insuficiente");
                    F.AçãoAutorizada = false;
                }
            }
        }
                

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
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
            this.Close();
            F.AçãoAutorizada = false;
        }
    }

}

