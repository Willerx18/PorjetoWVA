using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas_projeto.EmailServer
{
    internal class SystemSuportMail:MasterServerEmail
    { DataTable dt;
        public SystemSuportMail() 
        {
            cargarDados("Suporte");


        }

    }
}
