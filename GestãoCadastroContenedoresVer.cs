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
    public partial class F_GestãoCadastroContenedoresVer : Form
    {
        public F_GestãoCadastroContenedoresVer()
        {
            InitializeComponent();
        }

        private void GestãoCadastroContenedoresVer_Load(object sender, EventArgs e)
        {
            //populando ComboBoxFamilia
            cb_Familia.Items.Clear();

            cb_Familia.DataSource = Banco.ObterTodos("Familias", "*", "IdFamilia");
            cb_Familia.DisplayMember = "Familia";
            cb_Familia.ValueMember = "IdFamilia";
            
            //Popular combobox Tipo de Cadastro
            Dictionary<string, string> Cadastro = new Dictionary<string, string>();

            Cadastro.Add("1", "GERAL");
            Cadastro.Add("2", "ESPECIFICO");


            cb_TipoCadastro.Items.Clear();
            cb_TipoCadastro.DataSource = new BindingSource(Cadastro, null);
            cb_TipoCadastro.DisplayMember = "Value";
            cb_TipoCadastro.ValueMember = "key";

            //Preenchendo el DataGripView...

            dgv_GestãoContenedoresVer.DataSource = null;
            dgv_GestãoContenedoresVer.DataSource = Banco.ObterTodos("CadastroGeralContenedores", "*", "CIC");
            

        }

        private void btn_Atualizar_Click(object sender, EventArgs e)
        {

        }
    }
}
