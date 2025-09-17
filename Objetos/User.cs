using Atlas_projeto.EmailServer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Atlas_projeto.Objetos
{
    public class User
    {
        private string nome;
        private string usuario;
        private string senha;
        private int nivel;
        private string status;
        private int turno;
        private int id;
        private int areaDeTrabalho;
        private bool logado;
        private string email;

        public string Nome { get => nome; set => nome = value; }
        public string Usuario { get => usuario; set => usuario = value; }
        public string Senha { get => senha; set => senha = value; }
        public int Nivel { get => nivel; set => nivel = value; }
        public string Status { get => status; set => status = value; }
        public int Turno { get => turno; set => turno = value; }
        public int Id { get => id; set => id = value; }
        public  int AreaDeTrabalho { get => areaDeTrabalho; set => areaDeTrabalho = value; }
        public  bool Logado { get => logado; set => logado = value; }
        public string Email { get => email; set => email = value; }


        public string RecuperarSenhaUsuario(string Emailsolicitate)
        {
            DataTable dt=Banco.ObterTodosOnde("Usuarios", "Email","'"+Emailsolicitate+"'");
            if(dt.Rows.Count > 0)
            {
                string UserName = dt.Rows[0].Field<string>("Nome");                
                string UserSenha= dt.Rows[0].Field<string>("Senha");
               
                var mailService = new SystemSuportMail();
                mailService.EnviarEmail(
                    asunto: "SYSTEM: Recuperação da senha",
                    cuerpo: "OI, " + UserName + ", você solicitou sua senha\nSua senha Atual é: " + UserSenha,
                    destinatario:new List<string> { Emailsolicitate }
                    ) ;
                return "Olá, " + UserName + " , enviamos sua senha atual para sua direcção de correio cadastrada.";

            }
            else 
            {
                return "Olá,  correio não cadastrado.";
            }
        }


    }

}
