
namespace Atlas_projeto
{
    partial class F_SalvarRelatoriosReforma
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
            this.Pb_Salvos = new System.Windows.Forms.ProgressBar();
            this.lb_QuantiaSalvos = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lb_erros = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Pb_Salvos
            // 
            this.Pb_Salvos.Location = new System.Drawing.Point(33, 37);
            this.Pb_Salvos.Maximum = 50;
            this.Pb_Salvos.Name = "Pb_Salvos";
            this.Pb_Salvos.Size = new System.Drawing.Size(354, 23);
            this.Pb_Salvos.TabIndex = 0;
            this.Pb_Salvos.Value = 30;
            // 
            // lb_QuantiaSalvos
            // 
            this.lb_QuantiaSalvos.AutoSize = true;
            this.lb_QuantiaSalvos.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_QuantiaSalvos.ForeColor = System.Drawing.Color.Green;
            this.lb_QuantiaSalvos.Location = new System.Drawing.Point(135, 79);
            this.lb_QuantiaSalvos.Name = "lb_QuantiaSalvos";
            this.lb_QuantiaSalvos.Size = new System.Drawing.Size(23, 25);
            this.lb_QuantiaSalvos.TabIndex = 2;
            this.lb_QuantiaSalvos.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(253, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 20);
            this.label3.TabIndex = 3;
            this.label3.Text = "Erros:";
            // 
            // lb_erros
            // 
            this.lb_erros.AutoSize = true;
            this.lb_erros.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_erros.ForeColor = System.Drawing.Color.Maroon;
            this.lb_erros.Location = new System.Drawing.Point(314, 79);
            this.lb_erros.Name = "lb_erros";
            this.lb_erros.Size = new System.Drawing.Size(23, 25);
            this.lb_erros.TabIndex = 4;
            this.lb_erros.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(61, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 20);
            this.label4.TabIndex = 5;
            this.label4.Text = "Salvos:";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(192, 112);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(48, 35);
            this.button1.TabIndex = 7;
            this.button1.Text = "ok";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(167, 115);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(88, 35);
            this.button2.TabIndex = 8;
            this.button2.Text = "Iniciar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // F_SalvarRelatoriosReforma
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(447, 165);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lb_erros);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lb_QuantiaSalvos);
            this.Controls.Add(this.Pb_Salvos);
            this.Name = "F_SalvarRelatoriosReforma";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "F_SalvarRelatoriosReforma";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar Pb_Salvos;
        private System.Windows.Forms.Label lb_QuantiaSalvos;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lb_erros;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}