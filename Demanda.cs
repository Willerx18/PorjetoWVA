using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas_projeto
{
    class Demanda
    {
        private string idDemanda;
        private string cliente;
        private string nomeFogão;
        private string nomeCIP;
        private string caracteristica;
        private string horario;
        private string data;
        private string qf;
        private string cICG;
        private int vdP;
        private int vdC;
        private int tC;
        private string turno;
        

        public string Cliente { get => cliente; set => cliente = value; }
        public string NomeFogão { get => nomeFogão; set => nomeFogão = value; }       
        public string Qf { get => qf; set => qf = value; }
        public string Turno { get => turno; set => turno = value; }
        public string Horario { get => horario; set => horario = value; }
        public string Data { get => data; set => data = value; }
        public string IdDemanda { get => idDemanda; set => idDemanda = value; }
        public int VdP { get => vdP; set => vdP = value; }
        public int VdC { get => vdC; set => vdC = value; }
        public int TMC { get => tC; set => tC = value; }
        public string Caracteristica { get => caracteristica; set => caracteristica = value; }
        public string NomeCIP { get => nomeCIP; set => nomeCIP = value; }
        public string CICG { get => cICG; set => cICG = value; }
    }
}
