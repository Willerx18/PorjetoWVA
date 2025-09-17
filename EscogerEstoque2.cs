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
    public partial class EscogerEstoque2 : Form
    {
        Form2 F;
        public EscogerEstoque2(Form2 f)
        {
            InitializeComponent();
            F = f;
        }

        private void Carga_Click(object sender, EventArgs e)
        {
            F.AbreFormHijo(new F_NiveisDeEstoque("CARGA",lb_Setor.Text));
        }

        private void Pocesso_Click(object sender, EventArgs e)
        {
            F.AbreFormHijo(new F_NiveisDeEstoque("PROCESSO", lb_Setor.Text));
        }

        private void Descarga_Click(object sender, EventArgs e)
        {
            F.AbreFormHijo(new F_NiveisDeEstoque("DESCARGA", lb_Setor.Text));
        }

        private void Reforma_Click(object sender, EventArgs e)
        {
            F.AbreFormHijo(new F_NiveisDeEstoque("REFORMA", lb_Setor.Text));
        }
    }
}
