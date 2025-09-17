using Atlas_projeto.Objetos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Atlas_projeto
{
    public partial class F_Login : Form
    {
        F_Principal f2;
        public F_Login(F_Principal f)
        {
            InitializeComponent();

            f2 = f;        
                    
            
        }
        #region ConfguracionEspecial
        //prmitem movimentar a janela co o mouse
        [DllImport("user32.dll", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        private extern static void SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void F_Login_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        #endregion;

        private void F_Login_FormClosing(object sender, FormClosingEventArgs e)
        {
           
            if (!Globais.logado)
            { 
                Application.Exit();
            }
         
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

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


                    Globais.logado = dt.Rows[0].Field<bool>("logado");
                    UserCache.Nome = dt.Rows[0].Field<string>("Nome");

                    UserCache.Turno = (int)dt.Rows[0].Field<Int64>("Turno");
                    UserCache.Nivel = (int)dt.Rows[0].Field<Int64>("Nivel");
                    UserCache.Status = dt.Rows[0].Field<string>("Status");
                    UserCache.Senha = dt.Rows[0].Field<string>("Senha");
                    UserCache.Usuario = dt.Rows[0].Field<string>("Usuario");
                    UserCache.AreaDeTrabalho = (int)dt.Rows[0].Field<Int64>("AreaDeTrabalho");
                    Globais.AreaDeTrabalho = (int)dt.Rows[0].Field<Int64>("AreaDeTrabalho");
                    Globais.nivel = (int)dt.Rows[0].Field<Int64>("Nivel");

                    

                   if (UserCache.Status == "A")
                   {

                        if (dt.Rows.Count == 1 && UserCache.Senha == mtb_Senha.Text)
                        {
                           // if (Globais.logado == false)
                            //{
                                UserCache.Logado = true;
                                Banco.Atualizar("Usuarios", "Logado= 'true'", "Usuario", "'" + UserCache.Usuario + "'");


                                f2.lb_nivel.Text = dt.Rows[0].Field<Int64>("Nivel").ToString();
                                f2.lb_nome.Text = dt.Rows[0].Field<string>("Nome");
                                f2.Pb_led.Image = Properties.Resources.LuzVerde;                                
                                f2.menuStrip1.Enabled = true;
                                f2.mi_login.Enabled = false;
                               Globais.logado = true;
                                this.Close();

                          /*  }
                            else
                            {
                                DialogResult res = MessageBox.Show("Deseja encerrar sesion en otros dispositivos?", "USUARIO JA LOGADO", MessageBoxButtons.YesNo);
                                if (res == DialogResult.Yes)
                                {
                                    bool Atualizou=Banco.Atualizar("Usuarios", "Logado= 'false'", "Usuario", "'" + UserCache.Usuario + "'");
                                    if (Atualizou)
                                    {
                                        MessageBox.Show("Desconectado com sucesso, agora pode logar desde este dispositivo");
                                    }
                                }
                            }*/
                        }
                        else { MessageBox.Show("Usuario ou senha incorretos"); }
                   }
                   else
                   {
                       MessageBox.Show("Accesso Negado: Intentando Logar Un Usuario não ativo");
                   }
                   
                }
                else
                {
                    MessageBox.Show("Usuario ou senha incorretos");
                }
            }
        }
        private void iconButton1_Click(object sender, EventArgs e)
        {
            Globais.Sair();
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
        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var RecuperaSenha = new F_RecuperarSenha();
            RecuperaSenha.ShowDialog();
            
        }
    }
}
