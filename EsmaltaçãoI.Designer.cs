
namespace Atlas_projeto
{
    partial class F_EsmaltaçãoI
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.planejamentoDeProduçãoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.programaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.indicadoresDeProduçãoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.taxaDeEntradaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.taxaDeSaidaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(-11, 31);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(811, 410);
            this.dataGridView1.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.planejamentoDeProduçãoToolStripMenuItem,
            this.indicadoresDeProduçãoToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // planejamentoDeProduçãoToolStripMenuItem
            // 
            this.planejamentoDeProduçãoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.programaToolStripMenuItem});
            this.planejamentoDeProduçãoToolStripMenuItem.Name = "planejamentoDeProduçãoToolStripMenuItem";
            this.planejamentoDeProduçãoToolStripMenuItem.Size = new System.Drawing.Size(162, 20);
            this.planejamentoDeProduçãoToolStripMenuItem.Text = "Planejamento de Produção";
            // 
            // programaToolStripMenuItem
            // 
            this.programaToolStripMenuItem.Name = "programaToolStripMenuItem";
            this.programaToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.programaToolStripMenuItem.Text = "Programa";
            // 
            // indicadoresDeProduçãoToolStripMenuItem
            // 
            this.indicadoresDeProduçãoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.taxaDeEntradaToolStripMenuItem,
            this.taxaDeSaidaToolStripMenuItem});
            this.indicadoresDeProduçãoToolStripMenuItem.Name = "indicadoresDeProduçãoToolStripMenuItem";
            this.indicadoresDeProduçãoToolStripMenuItem.Size = new System.Drawing.Size(150, 20);
            this.indicadoresDeProduçãoToolStripMenuItem.Text = "Indicadores de Produção";
            // 
            // taxaDeEntradaToolStripMenuItem
            // 
            this.taxaDeEntradaToolStripMenuItem.Name = "taxaDeEntradaToolStripMenuItem";
            this.taxaDeEntradaToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.taxaDeEntradaToolStripMenuItem.Text = "Taxa de entrada";
            // 
            // taxaDeSaidaToolStripMenuItem
            // 
            this.taxaDeSaidaToolStripMenuItem.Name = "taxaDeSaidaToolStripMenuItem";
            this.taxaDeSaidaToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.taxaDeSaidaToolStripMenuItem.Text = "Taxa de Saida";
            // 
            // F_EsmaltaçãoI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 597);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "F_EsmaltaçãoI";
            this.Text = "Gestão de Esmaltação I";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem planejamentoDeProduçãoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem programaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem indicadoresDeProduçãoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem taxaDeEntradaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem taxaDeSaidaToolStripMenuItem;
    }
}