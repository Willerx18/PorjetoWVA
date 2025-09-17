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
        private string tipodeOperação = "";
        private string origem = "";
        private string destino = "";
        private string cICE = "";
        private string cIP = "";
        private int quantidade = 0;
        private string esmaltação = "";
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
       
        public string Responsavel { get => rm; set => rm = value; }
        public int Turno { get => turno; set => turno = value; }
        public string CICE { get => cICE; set => cICE = value; }
        public string CIP { get => cIP; set => cIP = value; }
        public string NomeCIP { get => nomeCIP; set => nomeCIP = value; }
        public int Quantidade { get => quantidade; set => quantidade = value; }
        public string TipodeOperação { get => tipodeOperação; set => tipodeOperação = value; }
        public string Origem { get => origem; set => origem = value; }
        public string Destino { get => destino; set => destino = value; }
        public int NúmerodeOperação { get => númerodeOperação; set => númerodeOperação = value; }
        public string Esmaltação { get => esmaltação; set => esmaltação = value; }
        public string CIO { get => cIO; set => cIO = value; }
        public string RL { get => rl; set => rl = value; }
        public string EstadoCon { get => estadoCon; set => estadoCon = value; }
        public string EstadoPeça { get => estadoPeça; set => estadoPeça = value; }
        public int ID { get => iD; set => iD = value; }
    }
}
