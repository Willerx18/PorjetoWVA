using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FontAwesome.Sharp;
using MetroFramework.Forms;

namespace Atlas_projeto
{
    public partial class Form2 : Form
    {
        #region VARIAVEIS

        private IconButton btnAtual;
        private Panel bordeIzqbtn;
        private Form fHijoAtual;
        #endregion;
        public Form2()
        {
            InitializeComponent();
            F_Login f_Login = new F_Login(new F_Principal());
            
            f_Login.ShowDialog();
            bordeIzqbtn = new Panel();
            bordeIzqbtn.Size = new Size(7,60);
            panelMenu.Controls.Add(bordeIzqbtn);
            bordeIzqbtn.Visible = false;
            this.Text = string.Empty;
            this.ControlBox = false;
            this.DoubleBuffered= true;
            this.MaximizedBounds= Screen.FromHandle(this.Handle).WorkingArea;
          
            OcultarPanelesSubMenus();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            lb_nivel.Text = UserCache.Nivel.ToString();
            lb_nome.Text = UserCache.Nome;
            if(UserCache.Logado)
            Pb_led.Image = Properties.Resources.LuzVerde;
            else { Pb_led.Image = Properties.Resources.LuzVermelha; }
            AbreFormHijo(new F_GestãoEControleDeRetrablho(1,this));
        }

        #region PROCEDIMENTOS
        private void EsconderSubMenus(Color c)
        {
            if (c == Color.DarkRed)
            {
                Reset();
                p_Indicadores.Visible = false;
              
            }
            if (c == Color.DarkGreen)
            {
                Reset();
                p_Config.Visible = false;
            }
            if (c == Color.DarkGoldenrod)
            {
                Reset();
               p_Usuarios.Visible = false;
            }
            if (c == Color.DarkBlue)
            {
                Reset();
                p_Estoque.Visible = false;
                
            }

        }

        private void OcultarPanelesSubMenus()
        {
            

            p_Indicadores.Visible=false;
            p_Config.Visible=false;
            p_Usuarios.Visible=false;
            p_Estoque.Visible=false;
        }

        private void MostrarSubMenu(Panel SubMenu)
        {
            if (SubMenu.Visible==false)
            {
                OcultarPanelesSubMenus();
                SubMenu.Visible=true;

            }
            else
            {
                SubMenu.Visible=false;
            }
        }


        public void AbreFormHijo(Form HijoForm)
        {
            
            fHijoAtual = HijoForm;
            HijoForm.TopLevel = false;
            HijoForm.FormBorderStyle = FormBorderStyle.None;
            HijoForm.Dock = DockStyle.Fill;
            panelConteudo.Controls.Add(fHijoAtual);
            panelConteudo.Tag = HijoForm;
            HijoForm.BringToFront();
            HijoForm.Show();
            lb_Inicio.Text = HijoForm.Text;
        }
        private void Reset()
        {
            Desativarbtn();
            if (fHijoAtual != null)
            {
                fHijoAtual.Close();

            }
            bordeIzqbtn.Visible = false;
            iconformhjoAtual.IconChar = IconChar.Home;
            iconformhjoAtual.IconColor = Color.White;
            barraTitulo.BackColor = Color.Coral;
            BarraUsuario.BackColor = Color.Coral;
            lb_Inicio.Text = "Inicio";
           
        }
        private void Ativarbtn(object senderbtn, Color color)
        {
            if (senderbtn != null )
            {
                if (fHijoAtual != null)
                {
                    fHijoAtual.Close();

                }
                Desativarbtn();

                btnAtual = (IconButton)senderbtn;
                btnAtual.BackColor = Color.Aquamarine;
                btnAtual.ForeColor = color;
                btnAtual.TextAlign = ContentAlignment.MiddleCenter;
                btnAtual.IconColor = color;
                btnAtual.TextImageRelation = TextImageRelation.TextBeforeImage;
                btnAtual.ImageAlign = ContentAlignment.MiddleRight;
               
                //BORDE EZQ
                bordeIzqbtn.BackColor = color;
                bordeIzqbtn.Location = new Point(0,btnAtual.Location.Y);
                bordeIzqbtn.Visible = true;
                bordeIzqbtn.BringToFront();

                //ICONE ATUAL FILHO

                iconformhjoAtual.IconChar= btnAtual.IconChar;
                iconformhjoAtual.IconColor= color;
                barraTitulo.BackColor= Color.Aquamarine;
                BarraUsuario.BackColor = Color.Aquamarine;

            }

        }
        private void Desativarbtn()
        {
            if (btnAtual != null)
            {
                
                btnAtual.BackColor = Color.Blue;
                btnAtual.ForeColor = Color.White;
                btnAtual.TextAlign = ContentAlignment.MiddleLeft;
                btnAtual.IconColor = Color.DarkOrange;
                btnAtual.TextImageRelation = TextImageRelation.ImageBeforeText;
                btnAtual.ImageAlign = ContentAlignment.MiddleLeft;

              
            }

        }
       
        private bool ActivarODesactivarBtn(Button b, Color c)
        {
          
            if (bordeIzqbtn.Visible == false || btn_Ind.ForeColor != c)
            {
                Ativarbtn(b, c);
                // MostrarSubMenu(p_Estoque);
                return true;
            }
            else
            {
                EsconderSubMenus(c);
                return false;
            }

        }
        #endregion;

        #region BOTOES CLICK

        #region Btn ESTOQUES
        private void btn_estoques_Click(object sender, EventArgs e)
        {

            if (ActivarODesactivarBtn((Button)sender, Color.DarkBlue))
            {
                AbreFormHijo(new menuEstoque(this));
            }

           

        }
        #endregion;

        #region Btn INDICADORES
        private void btn_Ind_Click(object sender, EventArgs e)
        {
            if (ActivarODesactivarBtn((Button)sender, Color.DarkRed)) 
            {
            
            }
            
        }
        #endregion;     

        #region Btn USUARIOS
        private void btn_gesUsu_Click(object sender, EventArgs e)
        {
           if(ActivarODesactivarBtn((Button)sender, Color.DarkGoldenrod))
            {

            }           

        }
        #endregion;

        #region Btn CONFIGURACION

        private void btn_conf_Click(object sender, EventArgs e)
        {
            if(ActivarODesactivarBtn((Button)sender, Color.DarkGreen))
            {

            }
          
        }
        #endregion;

        private void btn_reinicio_Click(object sender, EventArgs e)
        {
            if (fHijoAtual!=null)
            {
                fHijoAtual.Close();
            }            
            Reset();
            AbreFormHijo(new F_InventarioContenedorSaidaEEntradas(new Form2(), "Esmaltação"));
        }


        #endregion;


        #region ConfguracionEspecial
        //prmitem movimentar a janela co o mouse
        [DllImport("user32.dll", EntryPoint= "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.dll", EntryPoint= "SendMessage")]
        private extern static void SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam );


        private void barraTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void Fechar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Maximizar_Click(object sender, EventArgs e)
        {
            if (WindowState==FormWindowState.Normal)
            {
                WindowState = FormWindowState.Maximized;
            }
            else
            {
                WindowState = FormWindowState.Normal;
            }
        }

        private void Minimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        #endregion;


        private void panelConteudo_Paint(object sender, PaintEventArgs e)
        {

        }

        private void AbreMenu_Click(object sender, EventArgs e)
        {
            
            panelMenu.Visible = true;
         
            AbreMenu.Visible = false;
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            panelMenu.Visible = false;
            AbreMenu.IconChar = IconChar.Bars;
            AbreMenu.Visible = true;
        }

        private void Pb_led_Click(object sender, EventArgs e)
        {

        }

     
    }
}
