namespace Atlas_projeto.Objetos
{
    partial class F_RecuperarSenha
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
            this.btn_EnviarSenha = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_Email = new System.Windows.Forms.TextBox();
            this.lb_textmsg = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_EnviarSenha
            // 
            this.btn_EnviarSenha.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_EnviarSenha.Location = new System.Drawing.Point(162, 113);
            this.btn_EnviarSenha.Name = "btn_EnviarSenha";
            this.btn_EnviarSenha.Size = new System.Drawing.Size(143, 35);
            this.btn_EnviarSenha.TabIndex = 0;
            this.btn_EnviarSenha.Text = "Enviar Senha";
            this.btn_EnviarSenha.UseVisualStyleBackColor = true;
            this.btn_EnviarSenha.Click += new System.EventHandler(this.btn_EnviarSenha_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(39, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(217, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Ingrese seu Emal cadastrado";
            // 
            // tb_Email
            // 
            this.tb_Email.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_Email.Location = new System.Drawing.Point(43, 72);
            this.tb_Email.Name = "tb_Email";
            this.tb_Email.Size = new System.Drawing.Size(359, 26);
            this.tb_Email.TabIndex = 2;
            // 
            // lb_textmsg
            // 
            this.lb_textmsg.AutoSize = true;
            this.lb_textmsg.Location = new System.Drawing.Point(43, 168);
            this.lb_textmsg.Name = "lb_textmsg";
            this.lb_textmsg.Size = new System.Drawing.Size(0, 13);
            this.lb_textmsg.TabIndex = 3;
            // 
            // F_RecuperarSenha
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(455, 235);
            this.Controls.Add(this.lb_textmsg);
            this.Controls.Add(this.tb_Email);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_EnviarSenha);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "F_RecuperarSenha";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Recperar Senha de Usuario";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_EnviarSenha;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_Email;
        private System.Windows.Forms.Label lb_textmsg;
    }
}