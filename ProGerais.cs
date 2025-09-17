using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Drawing;
using System.Collections;

namespace Atlas_projeto
{
    public class ProGerais
    {
        public static DataTable Contar(DataTable Dados, string NomeColumAsumar, string ColumOnde, string X,string Y, string Z)
        {
            DataTable DtConteneDados = new DataTable();

            DtConteneDados.Columns.Add(new DataColumn(X, typeof(string)));
            DtConteneDados.Columns.Add(new DataColumn(Y, typeof(int)));
            DtConteneDados.Columns.Add(new DataColumn(Z, typeof(string)));
            ArrayList array = new ArrayList();
            foreach (DataRow dr in Dados.Rows)
            {

                object total;


                if (array.IndexOf(dr[ColumOnde]) < 0)
                {


                    total = Dados.Compute(string.Format("SUM({0})",NomeColumAsumar), ColumOnde+"= '" + dr[ColumOnde] + "'");

                    DtConteneDados.Rows.Add(new object[] { dr[X], Convert.ToInt32(total), dr[Z] });
                    array.Add(dr[ColumOnde]);

                }

            }

            DataView dv = DtConteneDados.DefaultView;
            dv.Sort = X;
            DataTable sorteddt4 = dv.ToTable();
            return sorteddt4;
        }
    }
}
