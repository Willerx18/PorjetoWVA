using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas_projeto
{
   class Apontamento
    {
        private string cIR="";
        private string nomePeça = "";
        private string cIP = "";
        private string clasificação = "";
        private string cID = "";
        private string cIDE = "";
        private int quantidade=0;
        private string resp_Apontamento = "";
        private string data = "";
        private int turno=0;
        private string resp_Setor = "";

        public string CIR { get => cIR; set => cIR = value; }
        public string NomePeça { get => nomePeça; set => nomePeça = value; }
        public string CIP { get => cIP; set => cIP = value; }
        public string Clasificação { get => clasificação; set => clasificação = value; }
        public string CID { get => cID; set => cID = value; }
        public string CIDE { get => cIDE; set => cIDE = value; }
        public int Quantidade { get => quantidade; set => quantidade = value; }
        public string Resp_Apontamento { get => resp_Apontamento; set => resp_Apontamento = value; }
        public string Data { get => data; set => data = value; }
        public int Turno { get => turno; set => turno = value; }
        public string Resp_Setor { get => resp_Setor; set => resp_Setor = value; }
    }
}
