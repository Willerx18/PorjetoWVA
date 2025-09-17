using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
namespace Atlas_projeto
{
    class Globais
    {
        public static string versão = "1.0";
        public static bool logado = true;
        public static int nivel = 4;
        public static int AreaDeTrabalho=0;
        //public static string caminho = System.Environment.CurrentDirectory;
        public static string caminho = AppDomain.CurrentDomain.BaseDirectory.ToString();
        public static string NomeBanco = "EsmaltaçãoDB.db";
        public static string CaminhoBanco = caminho + @"\Banco\"+NomeBanco;
        public static string CaminhoFotos = caminho + @"\Fotos\" ;
        public static string CaminhoRelatoriosRdlc = caminho + @"\Relatorios\";
        public static string CaminhoRelatorioRetrabalho = caminho + @"\Relatorios\Retrabalho\";
        public static string CaminhoRelatorioSucata = caminho + @"\Relatorios\Sucata\";
        public static string CaminhoRelatorioBoas = caminho + @"\Relatorios\Boas\";
        public static bool Autorizado = false;
        public static void Abreform(int n, Form f, int areaDeTrabalho)
        {
            if (UserCache.Logado) 
            {
                if (nivel >= n && AreaDeTrabalho == areaDeTrabalho)
                {
                    f.ShowDialog();
                }
                else
                {
                    MessageBox.Show("ACESSO NÃO PERMITIDO!");
                }
            }
          else
            {
                MessageBox.Show("DEBE FAZER LOGIN PRIMEIRO");
                 
            }
        }

        public static void Abreform(int n, Form f)
        {
            if (UserCache.Logado)
            {
                if (nivel >= n )
                {
                    f.ShowDialog();
                }
                else
                {
                    MessageBox.Show("ACESSO NÃO PERMITIDO!");
                }
            }
            else
            {
                MessageBox.Show("DEBE FAZER LOGIN PRIMEIRO");

            }
        }
        public static void Sair()
        {
            DialogResult res = MessageBox.Show("Desaja fechar o Programa", "fechar?", MessageBoxButtons.YesNo);
            if (res==DialogResult.Yes)
            {
                Application.Exit();
            }
           
        }

        public static void gerarGraficoColumnas(string titulo, Chart grafico, string torreA, string torreB, int anchocolumna )
        {
            Title title = new Title();
            title.Font = new Font("Arial", 14, FontStyle.Bold);
            title.ForeColor = Color.Red;
            title.Text = titulo;
            grafico.Titles.Add(title);
            Legend legend = new Legend();
            grafico.Legends.Add(legend);
            grafico.Legends[0].Title = "Legenda";


            grafico.Series.Add(torreA);
            grafico.Series[torreA].LegendText = torreA;
            grafico.Series[torreA].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            grafico.Series[torreA].BorderWidth = anchocolumna; 




        }

    }
}
