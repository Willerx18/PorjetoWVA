using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas_projeto
{
    class Peça
    {
        private string cIP;
        private string imagem;
        private string nomeCIP;
        private string nomeFormal;
        private string código;
        private string circular;
        private decimal cumprimento;
        private decimal largura;
        private decimal altura;
        private decimal diametro;
        private decimal alto;
        private decimal alto2;
        private decimal valorDaPeçaEstampada;
        private decimal valorProcessoEsmaltação; //Retrabalho: Valor da quantidade de pó usada na peça(((areaPeça*alturadelacamadadepó-->(L))*valordacapacidadoBagdepó(L))/Capcidadedobagdepó(L) + Valor da mão de obra en relação a duração do processo+gastos de energia dos equipamentos
        private decimal valorSucataEsmaltada1;//Sucata nao retrabalhada:Valor da peça estampada+Valor do Processo de Esmaltação
        private decimal valorSucataEsmaltada2;//Sucata nao retrabalhada:Valor da peça estampada+Valor do Processo de Esmaltação
        private decimal camadaMin;
        private decimal camadaMax;
        private string massaSemEsmaltar;
        private string massaEsmaltada;
        private bool circular2;
        public string CIP { get => cIP; set => cIP = value; }
        public string NomeCIP { get => nomeCIP; set => nomeCIP = value; }
        public string NomeFormal { get => nomeFormal; set => nomeFormal = value; }
        public string Código { get => código; set => código = value; }
        public decimal Cumprimento { get => cumprimento; set => cumprimento = value; }
        public decimal Largura { get => largura; set => largura = value; }
        public decimal Altura { get => altura; set => altura = value; }
        public decimal ValorEstampada { get => valorDaPeçaEstampada; set => valorDaPeçaEstampada = value; }
        public decimal ValorProcesso { get => valorProcessoEsmaltação; set => valorProcessoEsmaltação = value; }
        public decimal ValorSucata1 { get => valorSucataEsmaltada1; set => valorSucataEsmaltada1 = value; }
        public decimal ValorSucata2 { get => valorSucataEsmaltada2; set => valorSucataEsmaltada2 = value; }
        public decimal Diametro { get => diametro; set => diametro = value; }
        public decimal Alto { get => alto; set => alto = value; }
        public decimal Alto2 { get => alto2; set => alto2 = value; }
        public decimal CamadaMin { get => camadaMin; set => camadaMin = value; }
        public decimal CamadaMax { get => camadaMax; set => camadaMax = value; }
        public string Imagem { get => imagem; set => imagem = value; }
        public string Circular { get => circular; set => circular = value; }
        public string MassaSemEsmaltar { get => massaSemEsmaltar; set => massaSemEsmaltar = value; }
        public string MassaEsmaltada { get => massaEsmaltada; set => massaEsmaltada = value; }
        public bool Circular2 { get => circular2; set => circular2 = value; }
    }
}
