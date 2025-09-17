
namespace Atlas_projeto.Relatorios
{
    partial class F_RelatorioEspecificaçõesPeças
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.rv_Peças = new Microsoft.Reporting.WinForms.ReportViewer();
            this.PeçaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.PeçaBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // rv_Peças
            // 
            this.rv_Peças.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.PeçaBindingSource;
            this.rv_Peças.LocalReport.DataSources.Add(reportDataSource1);
            this.rv_Peças.LocalReport.ReportEmbeddedResource = "Atlas_projeto.Relatorios.Report1.rdlc";
            this.rv_Peças.Location = new System.Drawing.Point(0, 0);
            this.rv_Peças.Name = "rv_Peças";
            this.rv_Peças.ServerReport.BearerToken = null;
            this.rv_Peças.Size = new System.Drawing.Size(800, 656);
            this.rv_Peças.TabIndex = 0;
            // 
            // PeçaBindingSource
            // 
            this.PeçaBindingSource.DataSource = typeof(Atlas_projeto.Peça);
            // 
            // F_RelatorioEspecificaçõesPeças
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 656);
            this.Controls.Add(this.rv_Peças);
            this.Name = "F_RelatorioEspecificaçõesPeças";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Relatorio Especificações de Peças";
            this.Load += new System.EventHandler(this.F_RelatorioEspecificaçõesPeças_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PeçaBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer rv_Peças;
        private System.Windows.Forms.BindingSource PeçaBindingSource;
    }
}