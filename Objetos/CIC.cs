using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas_projeto
{
    class CIC
    {
        private int digitoCICE;
        private string digitoAreaUbicacão;
        private string digitoTipoContenedor;
        private int digitoFamilia;
        private int digitoModelo;
        private int digitoCaracteristica;
        private int digitoSetor;


        public string DigitoAreaUbicacão { get => digitoAreaUbicacão; set => digitoAreaUbicacão = value; }
        public string DigitoTipoContenedor { get => digitoTipoContenedor; set => digitoTipoContenedor = value; }
        public int DigitoFamilia { get => digitoFamilia; set => digitoFamilia = value; }
        public int DigitoModelo { get => digitoModelo; set => digitoModelo = value; }
        public int DigitoCaracteristica { get => digitoCaracteristica; set => digitoCaracteristica = value; }
        public int DigitoCICE { get => digitoCICE; set => digitoCICE = value; }
        public int DigitoSetor { get => digitoSetor; set => digitoSetor = value; }
    }
}
