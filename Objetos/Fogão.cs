using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas_projeto
{
    class Fogão
    {
        private string id;
        private string nomeFogão;
        private string modelo;
        private string caracteristica;
        private string bandeja="1A";
        private DataTable bases = new DataTable();        
        private int costa=1;
        private int difusor=1;
        private int lateral=2;
        private int porta=1;
        private int quadroFrontal=1;
        private int teto= 1;
        private int vedação=0;
        private int mesa = 1;


        public string ModeloDasPeças { get => modelo; set => modelo = value; }
        public string Bandeja { get => bandeja; set => bandeja = value; }
        public DataTable Bases { get => bases; set => bases = value; }
        public int Costa { get => costa; set => costa = value; }
        public int Difusor { get => difusor; set => difusor = value; }
        public int Lateral { get => lateral; set => lateral = value; }
        public int Porta { get => porta; set => porta = value; }
        public int QuadroFrontal { get => quadroFrontal; set => quadroFrontal = value; }
        public int Teto { get => teto; set => teto = value; }
        public int Vedação { get => vedação; set => vedação = value; }
        public string Caracteristica { get => caracteristica; set => caracteristica = value; }
        public int Mesa { get => mesa; set => mesa = value; }
        public string NomeFogão { get => nomeFogão; set => nomeFogão = value; }
        public string Id { get => id; set => id = value; }
       
    }
}
