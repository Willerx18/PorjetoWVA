using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas_projeto
{
    public static class UserCache
    {
        private static string nome;
        private static string usuario;
        private static string senha;
        private static int nivel;
        private static string status;
        private static int turno;
        private static int id;
        private static int areaDeTrabalho;
        private static bool logado;

        public static string Nome { get => nome; set => nome = value; }
        public static string Usuario { get => usuario; set => usuario = value; }
        public static string Senha { get => senha; set => senha = value; }
        public static int Nivel { get => nivel; set => nivel = value; }
        public static string Status { get => status; set => status = value; }
        public static int Turno { get => turno; set => turno = value; }
        public static int Id { get => id; set => id = value; }
        public static int AreaDeTrabalho { get => areaDeTrabalho; set => areaDeTrabalho = value; }
        public static bool Logado { get => logado; set => logado = value; }
    }
}
