using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Atlas_projeto.Objetos
{
    public partial class F_RecuperarSenha : Form
    {
        public F_RecuperarSenha()
        {
            InitializeComponent();
        }

        private void btn_EnviarSenha_Click(object sender, EventArgs e)
        {   var user = new User();
            if (tb_Email.Text!="")
            {
                var resultado = user.RecuperarSenhaUsuario(tb_Email.Text);
                lb_textmsg.Text = resultado;
                
            }
            
        }
    }
}
