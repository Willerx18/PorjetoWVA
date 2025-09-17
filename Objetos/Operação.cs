using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas_projeto
{
    class Operação
    {
        private int iD = 0;
        private int númerodeOperação = 0;
        private string cIO = "";
        private string cO = "";
        private string cIT = "";
        private string tipodeOperação = "";
        private string origem = "";
        private string destino = "";
        private string cICE = "";
        private string conteudo = "";
        private string quantidade = "";
        private string data = "";
        private string hora = "";
        private int turno = 0;
        private string rm = "";
        private string rl = "";
        private string estadoCon = "";
        private string estadoPeça = "";
        private string nomeCIP = "";

      
        
    

        public string Data { get => data; set => data = value; }
        public string Hora { get => hora; set => hora = value; }
       
        public string Rm { get => rm; set => rm = value; }
        public int Turno { get => turno; set => turno = value; }
        public string CICE { get => cICE; set => cICE = value; }
        public string Conteudo { get => conteudo; set => conteudo = value; }
        public string NomeCIP { get => nomeCIP; set => nomeCIP = value; }
        public string Quantidade { get => quantidade; set => quantidade = value; }
        public string TipodeOperação { get => tipodeOperação; set => tipodeOperação = value; }
        public string Origem { get => origem; set => origem = value; }
        public string Destino { get => destino; set => destino = value; }
        public int NúmerodeOperação { get => númerodeOperação; set => númerodeOperação = value; }
        public string CIO { get => cIO; set => cIO = value; }
        public string Rl { get => rl; set => rl = value; }
        public string EstadoCon { get => estadoCon; set => estadoCon = value; }
        public string EstadoPeça { get => estadoPeça; set => estadoPeça = value; }
        public int ID { get => iD; set => iD = value; }
        public string CIT { get => cIT; set => cIT = value; }
        public string CO { get => cO; set => cO = value; }
    }
}
