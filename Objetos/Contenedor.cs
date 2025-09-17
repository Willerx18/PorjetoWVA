using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas_projeto
{
    class Contenedor
    {  // Datos para o Cadastro de Contenedores Especifico
        private string id = "";
        private string cice = "";
        private string cicg = "";
        private string nomeCIC = "";       
        private long CapacidadePeças = 0;    
        private long ioe = 0;
        private long CapEsperada = 0;
        private string ubicaçãoAtual = "";
        private string cice_e = "";
        private int estado = 0;
        private string cice_c = "";
        private int condição = 0;
        private string cice_ec = "";

        // Datos Para o cadastro de contenedores Geral
        private long CapacidadeGeral = 0;
        private long N_Contenedores = 0;     
        private decimal largura = 0;
        private decimal cumprimento = 0;
        private decimal altura = 0;
        private long padrão = 0;
        private string foto = "";
        private string massa;
        public string CICG { get => cicg; set => cicg = value; }
        public string CICE { get => cice; set => cice = value; }
        public string NomeCIC{ get => nomeCIC; set => nomeCIC = value; }
        public long IOE { get => ioe;  set => ioe = value; }
        public long CAPACIDADE { get => CapacidadePeças; set => CapacidadePeças = value; }
        public long CAP_ESPERADA { get => CapEsperada; set => CapEsperada = value; }
        public long QUANTIDADE { get => N_Contenedores; set => N_Contenedores = value; }        
        public decimal Largura { get => largura; set => largura = value; }   
        public decimal Cumprimento { get => cumprimento; set => cumprimento = value; }
        public decimal Altura { get => altura; set => altura = value; }
        public string CICE_E { get => cice_e; set => cice_e = value; }
        public int Estado { get => estado; set => estado = value; }
        public string CICE_C { get => cice_c; set => cice_c = value; }
        public int Condição { get => condição; set => condição = value; }
        public string CICE_EC { get => cice_ec; set => cice_ec = value; }
        public long Padrão { get => padrão; set => padrão = value; }
        public string Foto { get => foto; set => foto = value; }       
        public string UbicaçãoAtual { get => ubicaçãoAtual; set => ubicaçãoAtual = value; }
        public long CapacidadeGeral1 { get => CapacidadeGeral; set => CapacidadeGeral = value; }
        public string Id { get => id; set => id = value; }
        public string Massa { get => massa; set => massa = value; }
    }  
}
