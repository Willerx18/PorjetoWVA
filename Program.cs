using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Atlas_projeto
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new Form2());
        }

        // F_CalculoDeCMAA(new F_CadastroEControleDeContenedores()) F_InventarioContenedorSaidaEEntradas(new F_Principal() new F_CalculoDeCMAA(new F_CapacidadeDeArmazenamento(), 1))
    }
}
