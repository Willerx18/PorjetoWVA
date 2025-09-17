using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Atlas_projeto
{
    public partial class F_SalvarOUatualizarContenedores : Form
    {
        bool Salvou = false;        
        int Maximum;
        DataTable DatosE;
        DataTable DatosG;
        F_CadastroEControleDeContenedores F;
        public F_SalvarOUatualizarContenedores(DataTable datosE, DataTable datosG, int maximum, F_CadastroEControleDeContenedores f )
        {
            InitializeComponent();
            DatosE = datosE;
            DatosG = datosG;
            Maximum = maximum;
            Pb_Salvos.Maximum = Maximum;
            Pb_Salvos.Minimum = 0;
            Pb_Salvos.Value = 0;
            pb_Geral.Maximum = datosG.Rows.Count;
            pb_Geral.Minimum = 0;
            pb_Geral.Value = 0;
            button2.Visible = true;
            button1.Visible = false;
            F = f;
        }
      
        private void button1_Click(object sender, EventArgs e)//OK
        {
            F.modo = 0;
            this.Close();
        }

        int ConteoSalvos = 0;
        int ConteoErros=0;

        private void button2_Click(object sender, EventArgs e)//INICIAR
        {
            if (F.SalvarArquivo())
            {
                MessageBox.Show("Operação Cancelada");
                this.Close();
            }

            List<Contenedor> ListCadContenedorE = new List<Contenedor>();
            foreach (DataRow row in DatosE.Rows)
            {
                Contenedor conte = new Contenedor();
                conte.Id= row["id"].ToString();
                conte.CICE = row["CICE"].ToString();
                conte.CICG = row["CICG"].ToString();
                conte.NomeCIC = row["NomeCIC"].ToString();
                conte.CAPACIDADE = int.Parse(row["Capacidade"].ToString());
                conte.IOE = int.Parse(row["IOE"].ToString());
                conte.CAP_ESPERADA = int.Parse(row["CapacidadeEsperada"].ToString());               
                conte.UbicaçãoAtual = row["UbicaçãoAtual"].ToString();
                conte.Condição = int.Parse(row["CONDIÇOES"].ToString());
                conte.Estado = int.Parse(row["ESTADO"].ToString());
                conte.CICE_E = row["CICE_E"].ToString();                
                conte.CICE_C = row["CICE_C"].ToString();              
                conte.CICE_EC = row["CICE_EC"].ToString();                
                ListCadContenedorE.Add(conte);

            }
           
            List<Contenedor> ListCadContenedorG = new List<Contenedor>();
            foreach (DataRow row in DatosG.Rows)
            {
                Contenedor conte = new Contenedor();        
                conte.CICG = row["CICG"].ToString();
                conte.QUANTIDADE = int.Parse((string)row["Quantidade"]);           
                conte.Cumprimento = decimal.Parse(row["X"].ToString());
                conte.Largura = decimal.Parse(row["Y"].ToString());
                conte.Altura = decimal.Parse(row["Z"].ToString());
                conte.Foto = row["Foto"].ToString();
                conte.CapacidadeGeral1=Convert.ToInt64(row["Capacidade"].ToString());
                conte.Massa = row["Massa"].ToString();
                ListCadContenedorG.Add(conte);

            }

            ConteoSalvos = 0;
            ConteoErros = 0;
            foreach (Contenedor contene in ListCadContenedorE)
            {              
               
                DataTable dt = Banco.ObterTodosOnde("CadastroEspecificoContenedores", "CICE", "'" + contene.CICE + "'");
                if (dt.Rows.Count > 0)
                {

                    Salvou = Banco.Atualizar("CadastroEspecificoContenedores", "id='"+contene.Id+"', CICE='" + contene.CICE + "',CICG='" + contene.CICG + "', NomeCIC='" + contene.NomeCIC + "', Capacidade= '" + contene.CAPACIDADE+ " ',  IOE='" + contene.IOE + "', CapacidadeEsperada ='" + contene.CAP_ESPERADA + "', UbicaçãoAtual= '" + contene.UbicaçãoAtual + "', ESTADO= '" + contene.Estado +  "', CICE_E= '" + contene.CICE_E + "', CONDIÇOES= '" + contene.Condição + "', CICE_C= '" + contene.CICE_C + "', CICE_EC= '" + contene.CICE_EC + "'", "CICE", "'" + contene.CICE + "'");
                }
                else
                {
                   
                    Salvou = Banco.Salvar("CadastroEspecificoContenedores", "id, CICE, CICG, NomeCIC, Capacidade, IOE, CapacidadeEsperada ,  UbicaçãoAtual, CICE_E, ESTADO, CICE_C, CONDIÇOES, CICE_EC", "'" + contene.Id+"', '"+ contene.CICE + "',  '" + contene.CICG + "', '" + contene.NomeCIC + "', '" + contene.CAPACIDADE + "' ,'" + contene.IOE + "','" + contene.CAP_ESPERADA + "', '" + contene.UbicaçãoAtual + "', '" + contene.CICE_E + "', '" + contene.Estado + "', '" + contene.CICE_C + "', '" + contene.Condição + "', '" + contene.CICE_EC + "'");
                
                }

                if (Salvou)
                {
                    ConteoSalvos += 1; 
                   
                    lb_QuantiaSalvos.Text = "" + ConteoSalvos.ToString() + "/" + Maximum;
                                     
                    
                    Pb_Salvos.Increment(1);
                    Thread.Sleep(100);
                }
                else
                {
                    ConteoErros += 1;
                    
                    lb_erros.Text = "" + ConteoErros.ToString() + "/" + Maximum;
                    
                    if (Maximum < 50)
                    {
                        Thread.Sleep(100);
                    }
                    Pb_Salvos.Increment(1);
                }

              
            }
            
            ConteoSalvos = 0;
            ConteoErros = 0;
            foreach (Contenedor contene in ListCadContenedorG)
            {
               
                DataTable dt = Banco.ObterTodosOnde("CadastroGeralContenedores", "CICG", "'" + contene.CICG + "'");


                if (dt.Rows.Count > 0)
                {
                    int Quantidade = Convert.ToInt32(dt.Rows[0].Field<Int64>("N_Contenedores"));
                    int Qtotal = Quantidade + Convert.ToInt32(Math.Floor(Convert.ToDouble(contene.QUANTIDADE)));
                    Salvou = Banco.Atualizar("CadastroGeralContenedores", "CICG='" + contene.CICG + "', N_Contenedores= '" + Qtotal + "', Cumprimento ='" + contene.Cumprimento + "', Largura= '" + contene.Largura + "', Altura= '" + contene.Altura + "', Foto= '" + contene.Foto + "', Capacidade= '" + contene.CapacidadeGeral1 + "', Massa= '" + contene.Massa + "'", "CICG", "'" + contene.CICG + "'");
                }
                else
                {
                    if (contene.QUANTIDADE > 0)
                    {
                        Salvou = Banco.Salvar("CadastroGeralContenedores", "CICG, N_Contenedores,  Cumprimento, Largura,  Altura, Foto, Capacidade, Massa", "'" + contene.CICG + "', '" + contene.QUANTIDADE + "' ,'" + contene.Cumprimento + "' , '" + contene.Largura + "', '" + contene.Altura + "', '" + contene.Foto + "', '" + contene.CapacidadeGeral1 + "', '" + contene.Massa + "'");

                    }
                    else
                    {
                        MessageBox.Show("Quantidade menor do que 1 não é permitida para novos cadastros", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Salvou = false;
                    }
                }

                if (Salvou)
                {
                    ConteoSalvos += 1;

                    lb_QsG.Text = "" + ConteoSalvos.ToString() + "/" + DatosG.Rows.Count;


                    pb_Geral.Increment(1);
                    Thread.Sleep(100);

                }
                else
                {

                    ConteoErros += 1;

                    lb_QeG.Text = "" + ConteoErros.ToString() + "/" + DatosG.Rows.Count;

                    if (DatosG.Rows.Count < 50)
                    {
                        Thread.Sleep(100);
                    }
                    pb_Geral.Increment(1);
                }

                
            }

            button2.Visible = false;
            button1.Visible = true;

        }

        
    }
}
