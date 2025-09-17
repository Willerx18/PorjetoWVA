using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Atlas_projeto
{
    public partial class F_SalvarRelatoriosReforma : Form
    {
        bool Salvou = false;
        int[] m1;
        int Maximum;
        DataTable Datos;
        int erros = 0;
        public F_SalvarRelatoriosReforma(DataTable Dados, int maximum, int[] M1)
        {
            InitializeComponent();
            m1 = M1;
            Maximum = maximum;
            Datos = Dados;
            Pb_Salvos.Maximum = Maximum;
            Pb_Salvos.Minimum = 0;
            Pb_Salvos.Value = 0;
            button2.Visible =true;
            button1.Visible = false;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ObterIndiceDosPontos(string texto)
        {
            string Buscado = ".";
            string c;

            int m = 0;

            for (int i = 0; i < texto.Length; i++)
            {
                c = texto.Substring(i, 1);
                if (c == Buscado)
                {

                    m1[m] = i;
                    m += 1;

                }
            }


        }


        private void button2_Click(object sender, EventArgs e)
        {
            
            Pb_Salvos.Maximum = Maximum;
            Pb_Salvos.Minimum = 0;
            Pb_Salvos.Value = 0;
            int cont = 0;
            foreach (DataRow dr in Datos.Rows)
            {
                
                cont += 1;
               if ((string)dr["Clasificação"] == "R")
                {
                    Salvou = Banco.Salvar("RelatorioSaidaReforma_Retrabalho", " CIR, CIDE, NomePeça, CIP, CID, Quantidade, Data, Turno, 'Resp. Apontamento', 'Resp. Setor'", "'" + dr["CIR"] + "', " + "'" + dr["CIDE"] + "', " + "'" + dr["Nome_Peça"] + "', " + "'" + dr["CIP"] + "', " + "'" + dr["CID"] + "', " + "" + dr["Quantidade"] + ", " + "'" + dr["Data"] + "', " + "" + dr["Turno"] + ", " + "'" + dr["Resp_Apontamento"] + "', " + "'" + dr["Resp_Setor"] + "' ");

                }
                else
                {
                    if ((string)dr["Clasificação"] == "S")
                    {
                        Salvou = Banco.Salvar("RelatorioSaidaReforma_Sucata", "CIR,CIDE, NomePeça, CIP, CID, Quantidade, Data, Turno, 'Resp. Apontamento', 'Resp. Setor'", "'" + dr["CIR"] + "', " + "'" + dr["CIDE"] + "', " + "'" + dr["Nome_Peça"] + "', " + "'" + dr["CIP"] + "', " + "'" + dr["CID"] + "', " + "" + dr["Quantidade"] + ", " + "'" + dr["Data"] + "', " + "" + dr["Turno"] + ", " + "'" + dr["Resp_Apontamento"] + "', " + "'" + dr["Resp_Setor"] + "' ");
                    }
                    else
                    {
                        if ((string)dr["Clasificação"] == "B")
                        {
                            Salvou = Banco.Salvar("RelatorioSaidaReforma_Boas", " CIR,CIDE, NomePeça, CIP, CID, Quantidade, Data, Turno, 'Resp. Apontamento', 'Resp. Setor'", "'" + dr["CIR"] + "', " + "'" + dr["CIDE"] + "', " + "'" + dr["Nome_Peça"] + "', " + "'" + dr["CIP"] + "', " + "'" + dr["CID"] + "', " + "" + dr["Quantidade"] + ", " + "'" + dr["Data"] + "', " + "" + dr["Turno"] + ", " + "'" + dr["Resp_Apontamento"] + "', " + "'" + dr["Resp_Setor"] + "' ");
                        }

                    }
                }

                ObterIndiceDosPontos((string)dr["CID"]);

                int n = 0;
                char Buscado = '.';

                foreach (char c in dr["CID"].ToString())
                {
                    if (c == Buscado)
                    {
                        n += 1;
                    }
                }

                if (n > 2)
                {

                    int Motivo = int.Parse(dr["CID"].ToString().Substring(m1[n - 5] + 1, 1));
                    string DefeitoAnterior = dr["CID"].ToString().Substring(m1[n - 4] + 1, m1[n - 2] - 6);
                    string DefeitoNovo = dr["CID"].ToString().Substring(m1[n - 2] + 1, (m1[n - 1] - m1[n - 2] + 1));
                    Salvou = Banco.Salvar("RelatorioRetrabalhoControl", " CIR,CIDE, CIP, CID, Quantidade, Clasificação, Motivo, 'Defeito Anterior', 'Defeito Novo', Data, Turno, 'Resp. Apontamento'", "'"  + dr["CIR"] + "', " + "'" + dr["CIDE"] + "', '" + dr["CIP"] + "', " + "'" + dr["CID"] + "', " + dr["Quantidade"] + ", '" + dr["Clasificação"] + "', " + Motivo + ", " + "'" + DefeitoAnterior + "', " + "'" + DefeitoNovo + "', " + "'" + dr["Data"] + "', " + dr["Turno"] + ", " + "'" + dr["Resp_Apontamento"] + "' ");
                }

               
               
                if (Salvou)
                {
                    Pb_Salvos.Value += 1;
                    lb_QuantiaSalvos.Text = "" + Pb_Salvos.Value + "/" + Maximum;
                    
                }
                else
                {
                    Pb_Salvos.Value += 1;
                    erros += 1;
                    lb_erros.Text = "" + erros + "/" + Maximum;
                }

                if (Maximum<20)
                {
                    Thread.Sleep(100);
                }
              
            }

            button2.Visible = false;
            button1.Visible = true;
        
        }
    }

}
