using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace Atlas_projeto.Relatorios
{
    public partial class F_Relatorio : Form
    {
        DataTable sorteddt2;
        string Titulo;
        string Subtitulo;
        string Resp_Setor;
        string Resp_Apontamento;
        string Turno;
        string CIR;
        public F_Relatorio(DataTable Sorteddt2, string titulo, string subtitulo, string turno, string resp_Setor, string resp_Apontamento, string cIR)
        {
            InitializeComponent();
            sorteddt2 = Sorteddt2;
            Titulo = titulo;
            Turno = turno;
            Subtitulo = subtitulo;
            Resp_Apontamento = resp_Apontamento;
            Resp_Setor = resp_Setor;
            CIR = cIR;
        }

        private void F_Relatorio_Load(object sender, EventArgs e)
        {

                

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.SetParameters(new ReportParameter("Titulo", Titulo));
            reportViewer1.LocalReport.SetParameters(new ReportParameter("CIR", CIR));
            reportViewer1.LocalReport.SetParameters(new ReportParameter("Turno", Turno.ToString()));
            reportViewer1.LocalReport.SetParameters(new ReportParameter("Resp_Apontamento", Resp_Apontamento));
            reportViewer1.LocalReport.SetParameters(new ReportParameter("Resp_Setor", Resp_Setor));
            reportViewer1.LocalReport.SetParameters(new ReportParameter("Subtitulo", Subtitulo));
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", sorteddt2));

            reportViewer1.RefreshReport();       
                
        }
    }
}
