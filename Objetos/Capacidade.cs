using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas_projeto
{
    class Capacidade
    {
        private string cICG;       
        private string cIP;
        private decimal iOE;
        private decimal cI;
        private decimal capacidade;
        private bool pares;
        private decimal compartimentos;
        private decimal padrão;
        private bool cpre;
        private string familia;
        public string CICG { get => cICG; set => cICG = value; }      
        public string CIP { get => cIP; set => cIP = value; }
        public decimal IOE { get => iOE; set => iOE = value; }
        public decimal Capacidad { get => capacidade; set => capacidade = value; }
        public bool Pares { get => pares; set => pares = value; }
        public decimal Compartimentos { get => compartimentos; set => compartimentos = value; }
        public decimal Padrão { get => padrão; set => padrão = value; }
        public decimal CI { get => cI; set => cI = value; }
        public bool Cpre { get => cpre; set => cpre = value; }
        public string Familia { get => familia; set => familia = value; }
    }
}
