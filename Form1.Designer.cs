
namespace Atlas_projeto
{
    partial class Form1
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgv_GestãoContenedores = new System.Windows.Forms.DataGridView();
            this.tb_CIC = new System.Windows.Forms.TextBox();
            this.btn_OK = new System.Windows.Forms.Button();
            this.lb_CIC = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_GestãoContenedores)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_GestãoContenedores
            // 
            this.dgv_GestãoContenedores.AllowUserToAddRows = false;
            this.dgv_GestãoContenedores.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_GestãoContenedores.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_GestãoContenedores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Salmon;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_GestãoContenedores.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_GestãoContenedores.Location = new System.Drawing.Point(9, 3);
            this.dgv_GestãoContenedores.Name = "dgv_GestãoContenedores";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_GestãoContenedores.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_GestãoContenedores.RowHeadersVisible = false;
            this.dgv_GestãoContenedores.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_GestãoContenedores.Size = new System.Drawing.Size(302, 270);
            this.dgv_GestãoContenedores.TabIndex = 80;
            // 
            // tb_CIC
            // 
            this.tb_CIC.Location = new System.Drawing.Point(106, 332);
            this.tb_CIC.Name = "tb_CIC";
            this.tb_CIC.Size = new System.Drawing.Size(100, 20);
            this.tb_CIC.TabIndex = 87;
            this.tb_CIC.Visible = false;
            // 
            // btn_OK
            // 
            this.btn_OK.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btn_OK.Location = new System.Drawing.Point(125, 377);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(61, 37);
            this.btn_OK.TabIndex = 88;
            this.btn_OK.Text = "OK";
            this.btn_OK.UseVisualStyleBackColor = true;
            // 
            // lb_CIC
            // 
            this.lb_CIC.AutoSize = true;
            this.lb_CIC.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lb_CIC.Location = new System.Drawing.Point(135, 300);
            this.lb_CIC.Name = "lb_CIC";
            this.lb_CIC.Size = new System.Drawing.Size(36, 20);
            this.lb_CIC.TabIndex = 89;
            this.lb_CIC.Text = "CIC";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(323, 450);
            this.Controls.Add(this.lb_CIC);
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.tb_CIC);
            this.Controls.Add(this.dgv_GestãoContenedores);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_GestãoContenedores)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_GestãoContenedores;
        private System.Windows.Forms.TextBox tb_CIC;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.Label lb_CIC;
    }
}