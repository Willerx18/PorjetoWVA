namespace Atlas_projeto
{
    partial class menuEstoque
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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_NovoSetor = new FontAwesome.Sharp.IconButton();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_ExcluirSetor = new FontAwesome.Sharp.IconButton();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(153, 108);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(856, 416);
            this.panel1.TabIndex = 3;
            // 
            // btn_NovoSetor
            // 
            this.btn_NovoSetor.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btn_NovoSetor.FlatAppearance.BorderSize = 0;
            this.btn_NovoSetor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_NovoSetor.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_NovoSetor.ForeColor = System.Drawing.Color.Black;
            this.btn_NovoSetor.IconChar = FontAwesome.Sharp.IconChar.PlusCircle;
            this.btn_NovoSetor.IconColor = System.Drawing.Color.ForestGreen;
            this.btn_NovoSetor.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btn_NovoSetor.IconSize = 32;
            this.btn_NovoSetor.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_NovoSetor.Location = new System.Drawing.Point(393, 539);
            this.btn_NovoSetor.Name = "btn_NovoSetor";
            this.btn_NovoSetor.Padding = new System.Windows.Forms.Padding(10, 0, 20, 0);
            this.btn_NovoSetor.Size = new System.Drawing.Size(193, 60);
            this.btn_NovoSetor.TabIndex = 5;
            this.btn_NovoSetor.Text = "   Novo Setor";
            this.btn_NovoSetor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_NovoSetor.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_NovoSetor.UseVisualStyleBackColor = false;
            this.btn_NovoSetor.Click += new System.EventHandler(this.btn_NovoSetor_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Crimson;
            this.label1.Location = new System.Drawing.Point(423, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(317, 45);
            this.label1.TabIndex = 6;
            this.label1.Text = "Escolha um Setor";
            // 
            // btn_ExcluirSetor
            // 
            this.btn_ExcluirSetor.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btn_ExcluirSetor.FlatAppearance.BorderSize = 0;
            this.btn_ExcluirSetor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ExcluirSetor.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ExcluirSetor.ForeColor = System.Drawing.Color.Black;
            this.btn_ExcluirSetor.IconChar = FontAwesome.Sharp.IconChar.Trash;
            this.btn_ExcluirSetor.IconColor = System.Drawing.Color.Crimson;
            this.btn_ExcluirSetor.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btn_ExcluirSetor.IconSize = 32;
            this.btn_ExcluirSetor.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_ExcluirSetor.Location = new System.Drawing.Point(625, 539);
            this.btn_ExcluirSetor.Name = "btn_ExcluirSetor";
            this.btn_ExcluirSetor.Padding = new System.Windows.Forms.Padding(10, 0, 20, 0);
            this.btn_ExcluirSetor.Size = new System.Drawing.Size(185, 60);
            this.btn_ExcluirSetor.TabIndex = 7;
            this.btn_ExcluirSetor.Text = "   Excluir Setor";
            this.btn_ExcluirSetor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_ExcluirSetor.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_ExcluirSetor.UseVisualStyleBackColor = false;
            this.btn_ExcluirSetor.Click += new System.EventHandler(this.btn_ExcluirSetor_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1086, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "label2";
            // 
            // menuEstoque
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1146, 669);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_ExcluirSetor);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_NovoSetor);
            this.Controls.Add(this.panel1);
            this.Name = "menuEstoque";
            this.Text = "Escolher Setor";
            this.Load += new System.EventHandler(this.menuEstoque_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Panel panel1;
        private FontAwesome.Sharp.IconButton btn_NovoSetor;
        private System.Windows.Forms.Label label1;
        private FontAwesome.Sharp.IconButton btn_ExcluirSetor;
        private System.Windows.Forms.Label label2;
    }
}