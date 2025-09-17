using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Data;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Microsoft.VisualBasic;
using System.Xml;
using System.Drawing.Imaging;

namespace Atlas_projeto
{
    public partial class menuEstoque : Form
    {
        #region VARIAVEIS
        Panel barraSelect;
        DataTable Data;
        string OrigemCompleto = "";
        string foto = "";
        string PastaDestino = Globais.CaminhoFotos + @"Setores\";
        string DestinoCompleto = "";
        bool sustituir = true;
        static List<Label> l;
        static List<PictureBox> pb;
        int QdSetores = 0;
        DataTable Dt;
        Form2 F;
        Label lant=null;
        PictureBox Pant=null;
        #endregion;
        public menuEstoque(Form2 f)
        {
            InitializeComponent();
           F= f;
        }

        private void menuEstoque_Load(object sender, EventArgs e)
        {
            CargarObjetoslbPb();
        }



        #region PROCEDIMIENTOS
        private void ContarSetoresCadastrados()
        {
            Dt = Banco.ObterTodos("Setores");
            QdSetores = Dt.Rows.Count;
        }
        
        private void CargarObjetoslbPb()
        {   panel1.Controls.Clear();
            barraSelect = new Panel();
            barraSelect.Size = new Size(60, 7);
            barraSelect.BackColor = Color.DarkRed;
            panel1.Controls.Add(barraSelect);
            barraSelect.Visible = false;
            int x = 99;
            int y = 246;
            int aux = 262;
            l = new List<Label>();
            ContarSetoresCadastrados();           
            for (int i=0; i<QdSetores;i++)
            {
                l.Add(crearLabelNovo(i,x,y,51,20,12F, Dt.Rows[i].Field<string>("Nome")));
                panel1.Controls.Add(l[i]);
                x = aux+37;
                aux = aux + 200;
            }
            x = 62;
            y = 74;
            pb = new List<PictureBox>();
            for (int i = 0; i < QdSetores; i++)
            { 
                pb.Add(CrearPctureBoxNova(i,x,y,160,153,12F, Dt.Rows[i].Field<string>("Foto"), Dt.Rows[i].Field<string>("Nome")));

                panel1.Controls.Add(pb[i]);
                pb[i].MouseDoubleClick += new MouseEventHandler(pb_Click);
                pb[i].Click += new EventHandler(pb_Select);
                x = x + 200;
              
            };



        }
        private bool SalvarArquivo()
        {
            if (DestinoCompleto == "")
            {
                if (MessageBox.Show("Foto não Selecionada, deseja continuar?", "Foto Não Selecionada", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                {
                    return true;
                }
                return false;

            }
            else
            {
                try
                {
                    if (DestinoCompleto != "")
                    {
                        if (OrigemCompleto != "")
                        {
                            File.Copy(OrigemCompleto, DestinoCompleto, sustituir);
                        }

                        if (!File.Exists(DestinoCompleto))
                        {
                            if (MessageBox.Show("Foto não encontrada, deseja continuar?", "ERRO", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.No)
                            {
                                return true;
                            }

                        }
                    }



                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro: " + ex.Message, "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                return false;
            }

        }
        private void AddFotoSetor()
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                OrigemCompleto = openFileDialog1.FileName;
                foto = openFileDialog1.SafeFileName;
                DestinoCompleto = PastaDestino + foto;

            }
            else
            {
                return;
            }          

           
        }
       
        private void verificaSetor(string Setor)
        {DataTable dt = new DataTable();
            if (Setor!=""&&Setor!="add")
            {
                dt = Banco.ObterTodos(Setor);
                if (dt!=null && dt.Rows.Count > 0)
                {
                    Data = dt;
                }
                else
                {
                    DialogResult res = MessageBox.Show("Quer cadastrar um novo Setor?", "Novo Cadastro", MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                    if (res == DialogResult.Yes)
                    {
                        CadastrarNovoEstoqueSetor(Setor);
                    }
                } 
            }
        }
        private void ExcluirSetor(string Setor)
        {
            DialogResult res = MessageBox.Show("Certeza que deseja Excluir o Setor: \n\n" + Setor, "Excluir", MessageBoxButtons.YesNo);
            if (res == DialogResult.Yes)
            {
                bool x, y, z, w, g;

                x = Banco.EliminarTabla("'PEÇAS" + Setor + "'");
                if (!x)
                {
                    y = Banco.EliminarTabla("'InventarioMATERIAPRIMA" + Setor + "'");
                    if (!y)
                    {
                        z = Banco.EliminarTabla("'InventarioPROCESSO" + Setor + "'");
                       
                        if (!z)
                        {
                            w = Banco.EliminarTabla("'InventarioFINAL" + Setor + "'");
                            if (!w)
                            {
                                g = Banco.EliminarTabla("'InventarioREFORMA" + Setor + "'");
                                if (!g)
                                {
                                    Banco.ExcluirDirecto("Setores","Nome","'"+Setor+"'");
                                    MessageBox.Show("Setor excluido com sucesso");
                                    CargarObjetoslbPb();
                                }
                                else
                                {
                                    MessageBox.Show("Não foi possivel apagar a tabela InventarioReforma" + Setor+"\n\nElimnadas com sucesso= 4");
                                    Banco.CrearTabla(@"PEÇAS" + Setor,
                                               "'CIP'   STRING PRIMARY KEY  , " +
                                               "'Código'  STRING," +
                                               "'NomeCIP' STRING," +
                                               "'NomeFormal'     STRING," +
                                               "'Largura'        STRING NOT NULL DEFAULT (0)," +
                                               "'Cumprimento'    STRING NOT NULL DEFAULT (0)," +
                                               "'Altura'        STRING NOT NULL DEFAULT (0)," +
                                               "'Diametro'       STRING NOT NULL DEFAULT (0)," +
                                               "'Alto'           STRING NOT NULL DEFAULT (0)," +
                                               "'Alto2'          STRING NOT NULL DEFAULT (0)," +
                                               "'ValorEstampada' STRING NOT NULL DEFAULT (0)," +
                                               "'ValorProcesso'  STRING NOT NULL DEFAULT (0)," +
                                               "'ValorSucata1'   STRING NOT NULL DEFAULT (0)," +
                                               "'ValorSucata2'   STRING NOT NULL DEFAULT (0)," +
                                               "'CamadaMin'      STRING NOT NULL DEFAULT (0)," +
                                               "'CamadaMax'     STRING NOT NULL DEFAULT (0)," +
                                               "'Foto'          STRING," +
                                               "'Circular'       STRING DEFAULT Não NOT NULL," +
                                               "'MSE'            STRING DEFAULT (0)," +
                                               "'ME'             STRING DEFAULT (0)," +
                                               "'Tipo'           STRING," +
                                               "'PadrãoDeArmazenamento'       INTEGER DEFAULT (1)");

                                    Banco.CrearTabla(@"InventarioMATERIAPRIMA" + Setor, @"'CIP' STRING REFERENCES PEÇAS" + Setor + ", 'Quantidade' INTEGER PRIMARY KEY  DEFAULT (0) ");
                                    Banco.CrearTabla(@"InventarioPROCESSO" + Setor, @"'CIP' STRING REFERENCES PEÇAS" + Setor + ", 'Quantidade' INTEGER PRIMARY KEY  DEFAULT (0) ");
                                    Banco.CrearTabla(@"InventarioFINAL" + Setor, @"'CIP' STRING REFERENCES PEÇAS" + Setor + ", 'Quantidade' INTEGER PRIMARY KEY  DEFAULT (0) ");
                                    
                                }
                            }
                            else
                            {
                                MessageBox.Show("Não foi possivel apagar a tabela InventarioFinal" + Setor + "\n\nElimnadas com sucesso= 3");
                                Banco.CrearTabla(@"PEÇAS" + Setor,
                                               "'CIP'   STRING PRIMARY KEY  , " +
                                               "'Código'  STRING," +
                                               "'NomeCIP' STRING," +
                                               "'NomeFormal'     STRING," +
                                               "'Largura'        STRING NOT NULL DEFAULT (0)," +
                                               "'Cumprimento'    STRING NOT NULL DEFAULT (0)," +
                                               "'Altura'        STRING NOT NULL DEFAULT (0)," +
                                               "'Diametro'       STRING NOT NULL DEFAULT (0)," +
                                               "'Alto'           STRING NOT NULL DEFAULT (0)," +
                                               "'Alto2'          STRING NOT NULL DEFAULT (0)," +
                                               "'ValorEstampada' STRING NOT NULL DEFAULT (0)," +
                                               "'ValorProcesso'  STRING NOT NULL DEFAULT (0)," +
                                               "'ValorSucata1'   STRING NOT NULL DEFAULT (0)," +
                                               "'ValorSucata2'   STRING NOT NULL DEFAULT (0)," +
                                               "'CamadaMin'      STRING NOT NULL DEFAULT (0)," +
                                               "'CamadaMax'     STRING NOT NULL DEFAULT (0)," +
                                               "'Foto'          STRING," +
                                               "'Circular'       STRING DEFAULT Não NOT NULL," +
                                               "'MSE'            STRING DEFAULT (0)," +
                                               "'ME'             STRING DEFAULT (0)," +
                                               "'Tipo'           STRING," +
                                                 "'PadrãoDeArmazenamento'       INTEGER DEFAULT (1)");

                                Banco.CrearTabla(@"InventarioMATERIAPRIMA" + Setor, @"'CIP' STRING REFERENCES PEÇAS" + Setor + ", 'Quantidade' INTEGER PRIMARY KEY  DEFAULT (0) ");
                                Banco.CrearTabla(@"InventarioPROCESSO" + Setor, @"'CIP' STRING REFERENCES PEÇAS" + Setor + ", 'Quantidade' INTEGER PRIMARY KEY  DEFAULT (0) ");
                                
                            }

                        }
                        else
                        {
                            MessageBox.Show("Não foi possivel apagar a tabela InventarioProcesso" + Setor + "\n\nElimnadas com sucesso= 2");
                            Banco.CrearTabla(@"PEÇAS" + Setor,
                                               "'CIP'   STRING PRIMARY KEY  , " +
                                               "'Código'  STRING," +
                                               "'NomeCIP' STRING," +
                                               "'NomeFormal'     STRING," +
                                               "'Largura'        STRING NOT NULL DEFAULT (0)," +
                                               "'Cumprimento'    STRING NOT NULL DEFAULT (0)," +
                                               "'Altura'        STRING NOT NULL DEFAULT (0)," +
                                               "'Diametro'       STRING NOT NULL DEFAULT (0)," +
                                               "'Alto'           STRING NOT NULL DEFAULT (0)," +
                                               "'Alto2'          STRING NOT NULL DEFAULT (0)," +
                                               "'ValorEstampada' STRING NOT NULL DEFAULT (0)," +
                                               "'ValorProcesso'  STRING NOT NULL DEFAULT (0)," +
                                               "'ValorSucata1'   STRING NOT NULL DEFAULT (0)," +
                                               "'ValorSucata2'   STRING NOT NULL DEFAULT (0)," +
                                               "'CamadaMin'      STRING NOT NULL DEFAULT (0)," +
                                               "'CamadaMax'     STRING NOT NULL DEFAULT (0)," +
                                               "'Foto'          STRING," +
                                               "'Circular'       STRING DEFAULT Não NOT NULL," +
                                               "'MSE'            STRING DEFAULT (0)," +
                                               "'ME'             STRING DEFAULT (0)," +
                                               "'Tipo'           STRING," +
                                               "'PadrãoDeArmazenamento'       INTEGER DEFAULT (1)");

                            Banco.CrearTabla(@"InventarioMATERIAPRIMA" + Setor, @"'CIP' STRING REFERENCES PEÇAS" + Setor + ", 'Quantidade' INTEGER PRIMARY KEY  DEFAULT (0) ");
                            
                        }

                    }
                    else
                    {
                        MessageBox.Show("Não foi possivel apagar a tabela InventarioMateriaPrima" + Setor + "\n\nElimnadas com sucesso= 1");
                        Banco.CrearTabla(@"PEÇAS" + Setor,
                                            "'CIP'   STRING PRIMARY KEY  , " +
                                            "'Código'  STRING," +
                                            "'NomeCIP' STRING," +
                                            "'NomeFormal'     STRING," +
                                            "'Largura'        STRING NOT NULL DEFAULT (0)," +
                                            "'Cumprimento'    STRING NOT NULL DEFAULT (0)," +
                                            "'Altura'        STRING NOT NULL DEFAULT (0)," +
                                            "'Diametro'       STRING NOT NULL DEFAULT (0)," +
                                            "'Alto'           STRING NOT NULL DEFAULT (0)," +
                                            "'Alto2'          STRING NOT NULL DEFAULT (0)," +
                                            "'ValorEstampada' STRING NOT NULL DEFAULT (0)," +
                                            "'ValorProcesso'  STRING NOT NULL DEFAULT (0)," +
                                            "'ValorSucata1'   STRING NOT NULL DEFAULT (0)," +
                                            "'ValorSucata2'   STRING NOT NULL DEFAULT (0)," +
                                            "'CamadaMin'      STRING NOT NULL DEFAULT (0)," +
                                            "'CamadaMax'     STRING NOT NULL DEFAULT (0)," +
                                            "'Foto'          STRING," +
                                            "'Circular'       STRING DEFAULT Não NOT NULL," +
                                            "'MSE'            STRING DEFAULT (0)," +
                                            "'ME'             STRING DEFAULT (0)," +
                                            "'Tipo'           STRING," +
                                            "'PadrãoDeArmazenamento'       INTEGER DEFAULT (1)");

                    }



                }
                else
                {
                    MessageBox.Show("Não foi possivel apagar a tabela PEÇAS" + Setor + "\n\nElimnadas com sucesso= 0");
                }

            }
        }
        private void CadastrarNovoEstoqueSetor(string Setor)
        {
            Banco.CrearTabla(@"PEÇAS" + Setor,
                "'CIP'   STRING PRIMARY KEY  , " +
                "'Código'  STRING," +
                "'NomeCIP' STRING," +
                "'NomeFormal'     STRING," +
                "'Largura'        STRING NOT NULL DEFAULT (0)," +
                "'Cumprimento'    STRING NOT NULL DEFAULT (0)," +
                "'Altura'        STRING NOT NULL DEFAULT (0)," +
                "'Diametro'       STRING NOT NULL DEFAULT (0)," +
                "'Alto'           STRING NOT NULL DEFAULT (0)," +
                "'Alto2'          STRING NOT NULL DEFAULT (0)," +
                "'ValorEstampada' STRING NOT NULL DEFAULT (0)," +
                "'ValorProcesso'  STRING NOT NULL DEFAULT (0)," +
                "'ValorSucata1'   STRING NOT NULL DEFAULT (0)," +
                "'ValorSucata2'   STRING NOT NULL DEFAULT (0)," +
                "'CamadaMin'      STRING NOT NULL DEFAULT (0)," +
                "'CamadaMax'     STRING NOT NULL DEFAULT (0)," +
                "'Foto'          STRING," +
                "'Circular'       STRING DEFAULT Não NOT NULL," +
                "'MSE'            STRING DEFAULT (0)," +
                "'ME'             STRING DEFAULT (0)," +
                "'Tipo'           STRING,"+
                "'PadrãoDeArmazenamento'       INTEGER DEFAULT (1)");

            Banco.CrearTabla(@"InventarioMATERIAPRIMA"+Setor, @"'CIP' STRING REFERENCES PEÇAS"+Setor+", 'Quantidade' INTEGER PRIMARY KEY  DEFAULT (0) ");
            Banco.CrearTabla(@"InventarioPROCESSO" + Setor, @"'CIP' STRING REFERENCES PEÇAS" + Setor + ", 'Quantidade' INTEGER PRIMARY KEY  DEFAULT (0) ");
            Banco.CrearTabla(@"InventarioFINAL" + Setor, @"'CIP' STRING REFERENCES PEÇAS" + Setor + ", 'Quantidade' INTEGER PRIMARY KEY  DEFAULT (0) ");
            Banco.CrearTabla(@"InventarioREFORMA" + Setor, @"'CIP' STRING REFERENCES PEÇAS" + Setor + " , 'Quantidade' INTEGER PRIMARY KEY  DEFAULT (0) ");
            
            DialogResult res = MessageBox.Show("quer cadastrar uma imagem do setor", "Nova imagem do setor", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
           
            if (res == DialogResult.Yes)
            {
                AddFotoSetor();
                if (!SalvarArquivo())
                {

                    Banco.Salvar("Setores", "Nome, Foto", "'" + Setor + "', '"+DestinoCompleto+"'");
                }
                else 
                {
                    MessageBox.Show("Não foi possivel salvar o arquivo");
                    Banco.Salvar("Setores", "Nome", "'" + Setor + "'");
                }

            }
            else
            {
                Banco.Salvar("Setores", "Nome", "'" + Setor + "'");
            }
            CargarObjetoslbPb();
            F_Peças p =new F_Peças(label2.Text);
            MessageBox.Show("Sera tranferido a area de cadastro de peças para inciar o cadasto das mismas para este setor");
            p.ShowDialog();
        }
        private Label crearLabelNovo(int id, int Px, int Py,int ZizeX, int ZizeY, float Fontzize, string Text)
        {
         Label l=  new Label
            {
                Name = Text+id,
                Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right))),
                AutoSize = true,
                Location = new Point(Px, Py),
                Size = new Size(ZizeX, ZizeY),
                TabIndex = id,
                Visible = true,
                Font = new Font("Microsoft Sans Serif", Fontzize, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0))),
                Text = Text
            };
            return l;
        }
        private PictureBox CrearPctureBoxNova(int id, int Px, int Py, int ZizeX, int ZizeY, float Fontzize,string localImage, string Nome)
        {
           PictureBox p=
            new PictureBox
            {
                Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right))),
                BorderStyle = BorderStyle.FixedSingle,                
                Location = new Point(Px, Py),
                Name = Nome,
                Size = new Size(ZizeX, ZizeY),
                SizeMode = PictureBoxSizeMode.StretchImage,
                TabIndex = QdSetores + id + 1,
                TabStop = false,
                Visible = true,
                ImageLocation = localImage,

            };return p;


        }
        
        private void desativarSelect()
        {
            if (Pant!=null&&lant!=null)
            {           
                
                  barraSelect.Visible = false;     
                  Pant.Location = new Point(Pant.Location.X, Pant.Location.Y + 12);     
                  lant.ForeColor = Color.Black;
                        
                    
                Pant = null;
                lant = null;
            }
          
        }
        private void AtivarSelect()
        {
            foreach (PictureBox c in pb)
            {
                if (c.Name == label2.Text)
                {   c.Location= new Point(c.Location.X, c.Location.Y - 12);
                    barraSelect.Location = new Point(c.Location.X + 50, c.Location.Y - 24);
                    barraSelect.BringToFront();
                    barraSelect.Visible = true;
                    Pant = c;
                }
            }
            foreach (Label c in l)
            {
                if (c.Name.Contains(label2.Text))
                {
                    c.ForeColor = Color.DarkRed;
                    lant = c;
                }

            }

        }
        private void EstabelecerSelect()
        {
            desativarSelect();
            AtivarSelect();
        }
        #endregion;

        #region BTN CLICKS
        private void btn_NovoSetor_Click(object sender, EventArgs e)
        {
            string Valor = Interaction.InputBox("Digite o Nome Do novo Setor:", "INSERIR UM VALOR");

            verificaSetor(Valor);
        }

        private void pb_Click(Object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            if (p.Name == "Esmaltação")
            {
                F_NiveisDeEstoque f = new F_NiveisDeEstoque("DESCARGA","Esmaltação");
                F.AbreFormHijo(f);
            }
            else
            {
                F_Peças f = new F_Peças(label2.Text);
                F.AbreFormHijo(f);
            }
           
        }
        #endregion;

        private void btn_ExcluirSetor_Click(object sender, EventArgs e)
        {
           
            ExcluirSetor(label2.Text);
        }

        private void pb_Select(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            label2.Text = p.Name;
            EstabelecerSelect();
        }


    }
}
