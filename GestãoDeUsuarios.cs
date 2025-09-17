using Atlas_projeto.Objetos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Atlas_projeto
{
    public partial class F_GestãoDeUsuarios : Form
    {   string IdSeleccionados;
        DataTable dt = new DataTable();
        string usuario, nivel, senha, status, nome, turno, id;

        F_Principal f;
        public F_GestãoDeUsuarios(F_Principal h) 
        {
            InitializeComponent();
            f = h;
            
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView dgv= (DataGridView)sender;
            int contilinha = dgv.Rows.Count;
            
            if (contilinha>0)
            {
                int row = dgv.CurrentRow.Index;
                dgv_GestãodeUsuarios.Rows[row].Cells[0].Selected = true;
                dgv_GestãodeUsuarios.FirstDisplayedScrollingRowIndex = row;
                try
                {



                    IdSeleccionados = dgv.Rows[dgv.SelectedRows[0].Index].Cells[0].Value.ToString();


                    dt = Banco.ObterTodosOnde("Usuarios", "IdUsuario", IdSeleccionados);



                    lb_msg.Visible = false;


                    tb_id.Text = dt.Rows[0].Field<Int64>("IdUsuario").ToString();
                    tb_nome.Text = dt.Rows[0].Field<string>("Nome");
                    tb_Email.Text = dt.Rows[0].Field<string>("Email");
                    cb_Nivel.SelectedValue = dt.Rows[0].Field<Int64>("Nivel").ToString();
                    cb_Turno.SelectedValue = dt.Rows[0].Field<Int64>("Turno").ToString();
                    Cb_Status.SelectedValue = dt.Rows[0].Field<string>("Status");
                    cb_AreaDeTrabalho.SelectedIndex =(int)dt.Rows[0].Field<Int64>("AreaDeTrabalho");
                    mtb_Senha.Text = dt.Rows[0].Field<string>("Senha");
                    mtb_Usuario.Text = dt.Rows[0].Field<string>("Usuario");
                    mtb_Senha.ReadOnly = true;
                    mtb_Usuario.ReadOnly = true;
                    btn_Atualizar.Text = "Atualizar";
                    btn_Excluir.Enabled = true;
                    tb_id.ReadOnly = true;
                    btn_Novo.Enabled = true;
                    ch_Permitir.Checked = false;
                    ch_Olhar.Checked = false;
                    btn_Cancelar.Enabled = false;
                    
                    usuario = mtb_Usuario.Text;
                    senha = mtb_Senha.Text;
                    nome = tb_nome.Text;
                    id = tb_id.Text;
                    turno = cb_Turno.Text;
                    status = Cb_Status.Text;
                    nivel = cb_Nivel.Text;


                    if ((tb_nome.Text == f.lb_nome.Text) || int.Parse(f.lb_nivel.Text) >= int.Parse("3"))
                    {
                        ch_Permitir.Visible = true;
                        ch_Olhar.Visible = false;
                    }
                    else
                    {
                        ch_Permitir.Visible = false;
                        ch_Olhar.Visible = true;
                    }
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    MessageBox.Show(ex.Message);
                   
                }
            }
        }

        private void F_GestãoDeUsuarios_Load(object sender, EventArgs e)

        {
            
            //popular ComboBox nivel
            Dictionary<string, string> nivel = new Dictionary<string, string>();

            nivel.Add("0", "Operador 1");
            nivel.Add("1", "Operador 2");
            nivel.Add("2", "Operador 3");
            nivel.Add("3", "Lider");
            nivel.Add("4", "Supervisor");
            nivel.Add("5", "MasterSystem");

            cb_Nivel.Items.Clear();
            cb_Nivel.DataSource = new BindingSource(nivel, null);
            cb_Nivel.DisplayMember = "Value";
            cb_Nivel.ValueMember = "key";

            //popular ComboBox Area de Trabalho
            Dictionary<string, string> ADT = new Dictionary<string, string>();

            ADT.Add("0", "Processo");          
            ADT.Add("1", "Reforma");
            ADT.Add("2", "Estoques");
            ADT.Add("3", "Operador de equipamentos");
            ADT.Add("4", "Gestão");           
            ADT.Add("5", "Sistema");

            cb_AreaDeTrabalho.Items.Clear();
            cb_AreaDeTrabalho.DataSource = new BindingSource(ADT, null);
            cb_AreaDeTrabalho.DisplayMember = "Value";
            cb_AreaDeTrabalho.ValueMember = "key";

            //popular ComboBox Status.
            Dictionary<string, string> Status = new Dictionary<string, string>();

            Status.Add("A", "Ativo");
            Status.Add("F", "Afastado");
            Status.Add("I", "Inativo");
            Status.Add("B", "Bloqueado");
            

            Cb_Status.Items.Clear();
            Cb_Status.DataSource = new BindingSource(Status, null);
            Cb_Status.DisplayMember = "Value";
            Cb_Status.ValueMember = "key";

            //popular ComboBox Turno.

            Dictionary<string, string> turno = new Dictionary<string, string>();

            
            turno.Add("1", "Primeiro");
            turno.Add("2", "Segundo ");
            turno.Add("3", "Tercero");
            turno.Add("4", "Comercial");

            cb_Turno.Items.Clear();
            cb_Turno.DataSource = new BindingSource(turno, null);
            cb_Turno.DisplayMember = "Value";
            cb_Turno.ValueMember = "key";

            //Preenchendo el DataGrip...
            dgv_GestãodeUsuarios.DataSource = Banco.ObterTodos("Usuarios", "IdUsuario, Nome"); 
            dgv_GestãodeUsuarios.Columns[0].Width = 60;
            dgv_GestãodeUsuarios.Columns[1].Width = 200;

         
           
        }

        private void ch_Olhar_CheckedChanged(object sender, EventArgs e)
        {

            if (((ch_Olhar.Checked == true) && (UserCache.Nivel > Convert.ToInt64(cb_Nivel.SelectedValue))&&(UserCache.AreaDeTrabalho>3))||((ch_Olhar.Checked == true) && (tb_nome.Text==UserCache.Nome)))
            {
               
                mtb_Senha.PasswordChar = '\u0000';
                mtb_Usuario.PasswordChar = '\u0000';
            }
            else
            {
                if (ch_Olhar.Checked == false)
                {
                    mtb_Senha.PasswordChar = '*';
                    mtb_Usuario.PasswordChar = '*'; 
                }
                else
                {
                    MessageBox.Show("Nivel de acceso insuficente para revelar os dados");
                    ch_Olhar.Checked = false;
                }     
             }
        }

        private void btn_Novo_Click(object sender, EventArgs e)
        {
            btn_Atualizar.Text = "Salvar";
            btn_Excluir.Enabled = false;
            tb_nome.Text = "";
            
            tb_id.Text = "";
            mtb_Senha.Text = "";
            mtb_Senha.ReadOnly = false;
            mtb_Usuario.Text = "";
            mtb_Usuario.ReadOnly = false;
            cb_Nivel.Text = "";
            cb_Turno.Text = "";
            Cb_Status.Text = "";
            tb_id.ReadOnly = false;
            ch_Permitir.Visible = false;
            ch_Olhar.Visible = false;
            mtb_Senha.PasswordChar = '\u0000';
            mtb_Usuario.PasswordChar = '\u0000';
            btn_Novo.Enabled = false;
            btn_Cancelar.Enabled = true;

        }

        private void mtb_Usuario_TextChanged(object sender, EventArgs e)
        {
            ch_Olhar.Checked = false;
        }

        private void mtb_Senha_TextChanged(object sender, EventArgs e)
        {
            ch_Olhar.Checked = false;
        }

        private void F_GestãoDeUsuarios_FormClosing(object sender, FormClosingEventArgs e)
        {
            f.Show();
        }
        
        private void cb_AreaDeTrabalho_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void btn_Atualizar_Click(object sender, EventArgs e)
        {
            if (btn_Atualizar.Text=="Atualizar") 
            {
                
                int linha = dgv_GestãodeUsuarios.SelectedRows[0].Index;
                User u = new User();
                u.Nome = tb_nome.Text;
                u.Nivel =Convert.ToInt32(cb_Nivel.SelectedValue);
                u.Usuario = mtb_Usuario.Text;
                u.Senha = mtb_Senha.Text;
                u.Status = Cb_Status.SelectedValue.ToString();
                u.Id = int.Parse(tb_id.Text);
                u.Turno = (int)cb_Turno.SelectedValue;
                u.AreaDeTrabalho = (int)cb_AreaDeTrabalho.SelectedValue;
                u.Email=tb_Email.Text;
                
                if ((u.Nivel < UserCache.Nivel) || (tb_nome.Text == UserCache.Nome) )
                {
                    dt=Banco.ObterTodosOnde("Usuarios", "Usuario" , "'"+u.Usuario+"'" );

                    if (dt.Rows.Count > 0 )
                    {
                       
                        Banco.Atualizar("Usuarios", "Nome= '" + u.Nome + "', Turno=" + u.Turno + ", Nivel=" + u.Nivel + ", Senha='" + u.Senha + "', AreaDeTrabalho='" + u.AreaDeTrabalho + "', Status='" + u.Status + "'", "IdUsuario", "" + u.Id);
                        dgv_GestãodeUsuarios[0, linha].Value = u.Id;
                        dgv_GestãodeUsuarios[1, linha].Value = u.Nome;
                       
                       
                        mtb_Usuario.Text = usuario;
                        if (u.Usuario != usuario)
                        {
                            lb_msg.Visible = true;
                            lb_msg.ForeColor = Color.DarkRed;
                            lb_msg.Text = "Usuario Já existia";
                        }
                    }
                    else
                    {
                        Banco.Atualizar("Usuarios", "Nome= '" + u.Nome + "', Turno=" + u.Turno + ", Nivel=" + u.Nivel + ", Senha='" + u.Senha + "', AreaDeTrabalho='" + u.AreaDeTrabalho + "', Usuario='" + u.Usuario + "', Status='" + u.Status + "'", "IdUsuario", "" + u.Id);
                        dgv_GestãodeUsuarios[0, linha].Value = u.Id;
                        dgv_GestãodeUsuarios[1, linha].Value = u.Nome;
                       
                        

                    }
                    

                }
                else { MessageBox.Show("Nivel de aturização insuficente para atualizar este usurio"); }
             }
            else 
            { 
                if(tb_id.Text == "")
                {
                    if(tb_nome.Text != "")
                    {
                        if(cb_Nivel.Text != "")
                        {
                            if (Cb_Status.Text != "")
                            {
                                if(cb_Turno.Text != "")
                                {
                                    if (mtb_Senha.Text != "")
                                    {
                                        if (mtb_Usuario.Text != "")
                                        {
                                            if (tb_Email.Text != "") 
                                            {


                                                User u = new User();
                                                u.Nome = tb_nome.Text;
                                                u.Nivel = Convert.ToInt32(cb_Nivel.SelectedValue);
                                                u.Senha = mtb_Senha.Text;
                                                u.Status = Cb_Status.SelectedValue.ToString();

                                                u.Turno = (int)cb_Turno.SelectedValue;
                                                u.Usuario = mtb_Usuario.Text;
                                                u.Email = tb_Email.Text;

                                                if (u.Nivel < UserCache.Nivel && (UserCache.AreaDeTrabalho>3))
                                                {
                                                    Banco.Salvar("Usuarios", " Nome, Turno, Nivel, Senha, AreaDeTrabalho, Usuario, Status, Email", "'" + u.Nome + "', '" + u.Turno + "', " + u.Nivel + ", '" + u.Senha + "', '" + u.AreaDeTrabalho + "', '" + u.Usuario + "', '" + u.Status + "'" + "', '" + u.Email + "'");





                                                }
                                                else { MessageBox.Show("Nivel de aturização insuficente para autorizar este tipo de usurio"); }

                                            }
                                            else
                                            {
                                                MessageBox.Show("Campo \"Email\" não pode estar vazio"); tb_Email.Focus();
                                            }
                                        }
                                        else { MessageBox.Show("Campo \"Usurio\" não pode estar vazio"); mtb_Usuario.Focus(); }
                                    }
                                    else { MessageBox.Show("Campo \"Senha\" não pode estar vazio"); mtb_Senha.Focus(); }
                                }
                                else { MessageBox.Show("Campo \"Turno\" não pode estar vazio"); cb_Turno.Focus(); }
                            }
                            else { MessageBox.Show("Campo \"Status\" não pode estar vazio"); Cb_Status.Focus(); }
                        }
                        else { MessageBox.Show("Campo \"Nivel\" não pode estar vazio"); cb_Nivel.Focus(); }
                    }
                    else { MessageBox.Show("Campo \"Nome\" não pode estar vazio"); tb_nome.Focus(); }
                }
                else { MessageBox.Show("Campo \"Id\" não pode estar vazio"); tb_id.Focus(); }
                dgv_GestãodeUsuarios.DataSource = Banco.ObterTodos("Usuarios", "IdUsuario, Nome");
            }
        }

        private void ch_Permitir_CheckedChanged(object sender, EventArgs e)
        {
            User u = new User();
            u.Nivel = Convert.ToInt32(cb_Nivel.SelectedValue);
            if (u.Nivel < UserCache.Nivel && (UserCache.AreaDeTrabalho > 3))
            {
                if (ch_Permitir.Checked == true)
                {
                    mtb_Senha.ReadOnly = false;
                    mtb_Usuario.ReadOnly = false;
                    mtb_Senha.PasswordChar = '\u0000';
                    mtb_Usuario.PasswordChar = '\u0000';

                }
                else
                {
                    mtb_Senha.ReadOnly = true;
                    mtb_Usuario.ReadOnly = true;
                    mtb_Senha.PasswordChar = '*';
                    mtb_Usuario.PasswordChar = '*';
                }
            }
            else { MessageBox.Show("Nivel de aturização insuficente para autorizar este tipo de usurio"); }

         
        }

        private void btn_Excluir_Click(object sender, EventArgs e)
        {
            User u = new User();
            u.Nivel = Convert.ToInt32(cb_Nivel.SelectedValue);
            if (u.Nivel < UserCache.Nivel && (UserCache.AreaDeTrabalho > 3))
            {
                Banco.Excluir("Usuarios", "IdUsuario", tb_id.Text);
                dgv_GestãodeUsuarios.Rows.Remove(dgv_GestãodeUsuarios.CurrentRow);

            }
            else { MessageBox.Show("Nivel de aturização insuficente para autorizar este tipo de usurio"); }

            
        }

        private void Btn_Voltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Btn_Sair_Click(object sender, EventArgs e)
        {
            Globais.Sair();
        }

        private void btn_Cancelar_Click(object sender, EventArgs e)
        {
            btn_Atualizar.Text = "Atualizar";
            btn_Excluir.Enabled = true;
            tb_nome.Text = nome;

            tb_id.Text = id;
            mtb_Senha.Text = senha;
            mtb_Senha.ReadOnly =true;
            mtb_Usuario.Text = usuario;
            mtb_Usuario.ReadOnly = true;
            cb_Nivel.Text = nivel;
            cb_Turno.Text = turno;
            Cb_Status.Text = status;
            tb_id.ReadOnly = true;
            ch_Permitir.Visible = true;
           
            mtb_Senha.PasswordChar = '*';
            mtb_Usuario.PasswordChar = '*';
            btn_Novo.Enabled = true;
            btn_Cancelar.Enabled = false;
            ch_Permitir.Checked = false;
        }
    }
}
