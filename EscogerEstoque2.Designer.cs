namespace Atlas_projeto
{
    partial class EscogerEstoque2
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Reforma = new System.Windows.Forms.Button();
            this.Descarga = new System.Windows.Forms.Button();
            this.Pocesso = new System.Windows.Forms.Button();
            this.Carga = new System.Windows.Forms.Button();
            this.lb_Setor = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lb_Setor);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 140);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(200, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(418, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "De qual estoque deseja informaçoes?";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.Reforma);
            this.panel2.Controls.Add(this.Descarga);
            this.panel2.Controls.Add(this.Pocesso);
            this.panel2.Controls.Add(this.Carga);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 140);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(800, 175);
            this.panel2.TabIndex = 1;
            // 
            // Reforma
            // 
            this.Reforma.BackColor = System.Drawing.Color.White;
            this.Reforma.Dock = System.Windows.Forms.DockStyle.Top;
            this.Reforma.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Reforma.Font = new System.Drawing.Font("Mongolian Baiti", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Reforma.ForeColor = System.Drawing.SystemColors.InfoText;
            this.Reforma.Location = new System.Drawing.Point(0, 120);
            this.Reforma.Name = "Reforma";
            this.Reforma.Size = new System.Drawing.Size(800, 40);
            this.Reforma.TabIndex = 12;
            this.Reforma.Text = "Estoque de Refoma";
            this.Reforma.UseVisualStyleBackColor = false;
            this.Reforma.Click += new System.EventHandler(this.Reforma_Click);
            // 
            // Descarga
            // 
            this.Descarga.BackColor = System.Drawing.Color.White;
            this.Descarga.Dock = System.Windows.Forms.DockStyle.Top;
            this.Descarga.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Descarga.Font = new System.Drawing.Font("Mongolian Baiti", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Descarga.ForeColor = System.Drawing.SystemColors.InfoText;
            this.Descarga.Location = new System.Drawing.Point(0, 80);
            this.Descarga.Name = "Descarga";
            this.Descarga.Size = new System.Drawing.Size(800, 40);
            this.Descarga.TabIndex = 15;
            this.Descarga.Text = "Estoque Final";
            this.Descarga.UseVisualStyleBackColor = false;
            this.Descarga.Click += new System.EventHandler(this.Descarga_Click);
            // 
            // Pocesso
            // 
            this.Pocesso.BackColor = System.Drawing.Color.White;
            this.Pocesso.Dock = System.Windows.Forms.DockStyle.Top;
            this.Pocesso.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Pocesso.Font = new System.Drawing.Font("Mongolian Baiti", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Pocesso.ForeColor = System.Drawing.SystemColors.InfoText;
            this.Pocesso.Location = new System.Drawing.Point(0, 40);
            this.Pocesso.Name = "Pocesso";
            this.Pocesso.Size = new System.Drawing.Size(800, 40);
            this.Pocesso.TabIndex = 14;
            this.Pocesso.Text = "Estoque de Pocesso";
            this.Pocesso.UseVisualStyleBackColor = false;
            this.Pocesso.Click += new System.EventHandler(this.Pocesso_Click);
            // 
            // Carga
            // 
            this.Carga.BackColor = System.Drawing.Color.White;
            this.Carga.Dock = System.Windows.Forms.DockStyle.Top;
            this.Carga.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Carga.Font = new System.Drawing.Font("Mongolian Baiti", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Carga.ForeColor = System.Drawing.SystemColors.InfoText;
            this.Carga.Location = new System.Drawing.Point(0, 0);
            this.Carga.Name = "Carga";
            this.Carga.Size = new System.Drawing.Size(800, 40);
            this.Carga.TabIndex = 13;
            this.Carga.Text = "Estoque de Materia Prima";
            this.Carga.UseVisualStyleBackColor = false;
            this.Carga.Click += new System.EventHandler(this.Carga_Click);
            // 
            // lb_Setor
            // 
            this.lb_Setor.AutoSize = true;
            this.lb_Setor.Location = new System.Drawing.Point(708, 9);
            this.lb_Setor.Name = "lb_Setor";
            this.lb_Setor.Size = new System.Drawing.Size(62, 13);
            this.lb_Setor.TabIndex = 1;
            this.lb_Setor.Text = "Esmaltação";
            // 
            // EscogerEstoque2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "EscogerEstoque2";
            this.Text = "EscogerEstoque2";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button Reforma;
        private System.Windows.Forms.Button Descarga;
        private System.Windows.Forms.Button Pocesso;
        private System.Windows.Forms.Button Carga;
        private System.Windows.Forms.Label lb_Setor;
    }
}