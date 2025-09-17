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
    public partial class F_RelatorioEspecificaçõesPeças : Form
    { DataTable dt;
        string Destino;
        int Modo;
        public F_RelatorioEspecificaçõesPeças(DataTable Peças, string RutaImagem , int modo)
        {
            InitializeComponent();
            dt = Peças;
            Destino = RutaImagem;
            Modo = modo;
        }

        private void F_RelatorioEspecificaçõesPeças_Load(object sender, EventArgs e)
        {
            if (Modo == 1)
            {
                rv_Peças.LocalReport.DataSources.Clear();
                rv_Peças.LocalReport.EnableExternalImages = true;
                this.rv_Peças.LocalReport.ReportEmbeddedResource = "Atlas_projeto.Relatorios.DadosdaPeça.rdlc";
                // rv_Peças.LocalReport.ReportPath = Globais.CaminhoRelatoriosRdlc+@"DadosdaPeça.rdlc";
                rv_Peças.LocalReport.SetParameters(new ReportParameter("Imagen", Destino));
                rv_Peças.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", dt));
                rv_Peças.RefreshReport();
               
            }
            else
            {
                if (Modo == 2)
                {
                    rv_Peças.LocalReport.DataSources.Clear();

                    rv_Peças.LocalReport.EnableExternalImages = true;
                    this.rv_Peças.LocalReport.ReportEmbeddedResource = "Atlas_projeto.Relatorios.ListaPeçasCustos.rdlc";
                    //rv_Peças.LocalReport.ReportPath = Globais.CaminhoRelatoriosRdlc+ @"ListaPeçasCustos.rdlc";
                    rv_Peças.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", dt));
                    rv_Peças.RefreshReport();
                }

            }

        }

    }
}

// this.rv_Peças.LocalReport.ReportEmbeddedResource = "Atlas_projeto.Relatorios.DadosdaPeça.rdlc"; sirve para poner como valor predeterminado esta direccion