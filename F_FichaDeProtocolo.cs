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
    public partial class F_FichaDeProtocolo : Form
    {
        string CICE;
        string CICG;
        string Condição;
        DataTable dt;
        DataTable dt2;
        DataTable dt3;
        public F_FichaDeProtocolo(string cICE, string cICG)
        {
            InitializeComponent();
            CICE = cICE;
            CICG = cICG;
        }

        private void F_FichaDeProtocolo_Load(object sender, EventArgs e)
        {
           dt = Banco.Procurar("Operaçoes", "*", "CICE", "'%" + CICE + "%'", "CICE asc");
           dt2= Banco.Procurar("CadastroGeralContenedores", "*", "CICG", "'%" + CICG + "%'", "CICG");
           dt3 = Banco.Procurar("CadastroEspecificoContenedores", "*", "CICE", "'%" + CICE + "%'", "CICG, id asc");

           // MessageBox.Show("dt= "+dt.Rows.Count.ToString()+" dt2= "+dt2.Rows.Count.ToString()+" Dt3= "+ dt3.Rows.Count.ToString());
            if (dt.Rows.Count>0 && dt2.Rows.Count>0 && dt3.Rows.Count > 0)
            {
                MessageBox.Show(dt2.Rows[0].Field<string>("Foto"));
                pb_Contenedor.ImageLocation = dt2.Rows[0].Field<string>("Foto");
                tb_CICE.Text = CICE;
                tb_Estado.Text = dt.Rows[0].Field<string>("EstadoPeça");
                int condi = Convert.ToInt32(dt3.Rows[0].Field<Int64>("CONDIÇOES"));
                switch (condi)
                {
                    case 0:
                        tb_Condição.Text = "ÓTIMO";
                        break;
                    case 1:
                        tb_Condição.Text = "DETERIORADO";
                        break;
                    case 2:
                        tb_Condição.Text = "DANIFICADO";
                        break;
                    case 3:
                        tb_Condição.Text = "ESTRAGADO";
                        break;
                }
                tb_NomeCICE.Text = dt3.Rows[0].Field<string>("NomeCIC");

                int UA = Convert.ToInt32(dt3.Rows[0].Field<Int64>("UbicaçãoAtual"));
                switch (UA)
                {
                    case 0:
                        tb_UA.Text = "ESTOQUE DESCARGA";
                        break;
                    case 1:
                        tb_UA.Text = "ESTOQUE CARGA SEM ESMALTAR";
                        break;
                    case 2:
                        tb_UA.Text = "ESTOQUE CARGA REFORMA AUTOMATIZADA";
                        break;
                    case 3:
                        tb_UA.Text = "ESTOQUE CARGA REFORMA MANUAL";
                        break;
                    case 4:
                        tb_UA.Text = "REFORMA ";
                        break;
                    case 5:
                        tb_UA.Text = "PROCESSO I ";
                        break;
                    case 6:
                        tb_UA.Text = "PROCESSO II";
                        break;
                    case 7:
                        tb_UA.Text = "CLIENTE";
                        break;
                    case 8:
                        tb_UA.Text = "CLIENTE SAC";
                        break;
                    case 9:
                        tb_UA.Text = "FORNECEDOR";
                        break;
                    case 10:
                        tb_UA.Text = "MENUTENÇÃO";
                        break;
                    case 11:
                        tb_UA.Text = "DESCARTE";
                        break;
                    case 12:
                        tb_UA.Text = "DEPÓSITO";
                        break;
                }
                tb_Cap.Text = dt3.Rows[0].Field<Int64>("CapacidadeEsperada").ToString();
                tb_IOE.Text= dt3.Rows[0].Field<Int64>("IOE").ToString();


            }
            else
            {
                MessageBox.Show("CICE e CICG Não Cadastrados");
            }
           

        }

        private void tb_NomeCICE_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
