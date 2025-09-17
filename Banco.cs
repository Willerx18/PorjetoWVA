using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;
using System.IO;

namespace Atlas_projeto
{
    class Banco
    {
        private static SQLiteConnection conexão;

        private static SQLiteConnection ConexãoBanco()
        {
            conexão = new SQLiteConnection("Data Source="+Globais.CaminhoBanco);            
            conexão.Open();
            return conexão;
        }
        public static bool EliminarTabla(string NomeTabla)
        {
           
                SQLiteDataAdapter da;
                try
                {
                    int flag = 0;

                    var vcom = ConexãoBanco();
                    var cmd = vcom.CreateCommand();
                    cmd.CommandText = "DROP TABLE IF EXISTS " + NomeTabla;
                    da = new SQLiteDataAdapter(cmd.CommandText, vcom);
                    flag = cmd.ExecuteNonQuery();
                    vcom.Close();

                    if (flag >= 1)
                    {
                        return true;
                    }
                    else { return false; }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("NÃO FOI POSSIVEL EXECUTAR: " + ex.Message);
                    return false;
                }
             

        }

        public static bool CrearTabla(string NomeTabla, string NomeColum)
        {
            SQLiteDataAdapter da;
            try
            {
                int flag = 0;

                var vcom = ConexãoBanco();
                var cmd = vcom.CreateCommand();
                cmd.CommandText = "CREATE TABLE "+NomeTabla+" ("+NomeColum+")" ;
                da = new SQLiteDataAdapter(cmd.CommandText, vcom);
                flag = cmd.ExecuteNonQuery();
                vcom.Close();

                if (flag >= 1)
                {
                    return true;
                }
                else { return false; }
            }
            catch (Exception ex)
            {
                MessageBox.Show("NÃO FOI POSSIVEL EXECUTAR: " + ex.Message);
                return false;
            }


        }
        public static DataTable ObterTodos(string tabla)
        {
            
            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                var vcom = ConexãoBanco();
                var cmd = vcom.CreateCommand();
                cmd.CommandText = "SELECT * FROM "+tabla;
                da = new SQLiteDataAdapter(cmd.CommandText, vcom);
                da.Fill(dt);
                vcom.Close();
                return dt;
            }
            catch (Exception ex) { MessageBox.Show("Não foi possivel Obter os dados: " + ex.Message); return null; }

        }
        public static DataTable ObterTodos(string tabla, string columnas)
        {

            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                var vcom = ConexãoBanco();
                var cmd = vcom.CreateCommand();
                cmd.CommandText = "SELECT " + columnas + " FROM " + tabla;
                da = new SQLiteDataAdapter(cmd.CommandText, vcom);
                da.Fill(dt);
                vcom.Close();
                return dt;
            }
            catch (Exception ex) { MessageBox.Show("Não foi possivel Obter os dados: " + ex.Message); return null; }
         }
        public static DataTable ObterTodos(string tabla, string columnas, string itemOrdenador)
        {

            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                var vcom = ConexãoBanco();
                var cmd = vcom.CreateCommand();
                cmd.CommandText = "SELECT " + columnas + " FROM " + tabla+ " ORDER BY "+ itemOrdenador;
                da = new SQLiteDataAdapter(cmd.CommandText, vcom);
                da.Fill(dt);
                vcom.Close();
                return dt;
            }
            catch (Exception ex) { MessageBox.Show("Não foi possivel Obter os dados: " + ex.Message); return null; }

        }

        public static DataTable ObterTodos(string tabla1, string tabla2, string columnas, string IgualColum1,  string itemOrdenador)
        {

            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                var vcom = ConexãoBanco();
                var cmd = vcom.CreateCommand();
                cmd.CommandText = "SELECT " + columnas + " FROM " + tabla1 + " INNER JOIN "+tabla2+" ON "+tabla1+"."+IgualColum1+" = "+tabla2+"."+IgualColum1+" ORDER BY " + itemOrdenador;
                da = new SQLiteDataAdapter(cmd.CommandText, vcom);
                da.Fill(dt);
                vcom.Close();
                return dt;
            }
            catch (Exception ex) { MessageBox.Show("Não foi possivel Obter os dados: " + ex.Message); return null; }

        }
        public static DataTable ObterTodosOnde(string tabla, string Colum1, string Colum2)
        {

            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                var vcom = ConexãoBanco();
                var cmd = vcom.CreateCommand();
                cmd.CommandText = "SELECT * FROM " + tabla+" Where "+Colum1+" = "+Colum2;
                da = new SQLiteDataAdapter(cmd.CommandText, vcom);
                da.Fill(dt);
                vcom.Close();
                return dt;
            }
            catch (Exception ex) { MessageBox.Show("Não foi possivel Obter os dados: " + ex.Message); return null; }

        }
        public static DataTable ObterTodosOnde2Criterios(string tabla, string Colum1, string Colum12, string Colum2,string Colum21)
        {

            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                var vcom = ConexãoBanco();
                var cmd = vcom.CreateCommand();
                cmd.CommandText = "SELECT * FROM " + tabla + " Where " + Colum1 + " = " + Colum12+" AND " + Colum2 + " = " + Colum21;
                da = new SQLiteDataAdapter(cmd.CommandText, vcom);
                da.Fill(dt);
                vcom.Close();
                return dt;
            }
            catch (Exception ex) { MessageBox.Show("Não foi possivel Obter os dados: " + ex.Message); return null; }

        }
        public static DataTable ObterTodosOndeInner(string tabla1, string tabla2, string columnas, string IgualColum1,  string Colum1, string Colum2)
        {

            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                var vcom = ConexãoBanco();
                var cmd = vcom.CreateCommand();
                cmd.CommandText = "SELECT "+columnas+" FROM " + tabla1 + " INNER JOIN " + tabla2 + " ON " + tabla1 + "." + IgualColum1 + " = " + tabla2 + "." + IgualColum1 + " Where " + Colum1 + " = " + Colum2;
                da = new SQLiteDataAdapter(cmd.CommandText, vcom);
                da.Fill(dt);
                vcom.Close();
                return dt;
            }
            catch (Exception ex) { MessageBox.Show("Não foi possivel Obter os dados: " + ex.Message); return null; }

        }
        public static DataTable ObterTodosOndeInner(string tabla1, string tabla2, string columnas, string IgualColum1)
        {

            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                var vcom = ConexãoBanco();
                var cmd = vcom.CreateCommand();
                cmd.CommandText = "SELECT " + columnas + " FROM " + tabla1 + " INNER JOIN " + tabla2 + " ON " + tabla1 + "." + IgualColum1 + " = " + tabla2 + "." + IgualColum1 ;
                da = new SQLiteDataAdapter(cmd.CommandText, vcom);
                da.Fill(dt);
                vcom.Close();
                return dt;
            }
            catch (Exception ex) { MessageBox.Show("Não foi possivel Obter os dados: " + ex.Message); return null; }

        }
        public static DataTable ProcurarcomInner(string tabla1, string tabla2, string columnas, string IgualColum1, string Colum1, string palavra)
        {

            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                var vcom = ConexãoBanco();
                var cmd = vcom.CreateCommand();
                cmd.CommandText = "SELECT " + columnas + " FROM " + tabla1 + " INNER JOIN " + tabla2 + " ON " + tabla1 + "." + IgualColum1 + " = " + tabla2 + "." + IgualColum1 + " Where " + Colum1 + " LIKE " + palavra;
                da = new SQLiteDataAdapter(cmd.CommandText, vcom);
                da.Fill(dt);
                vcom.Close();
                return dt;
            }
            catch (Exception ex) { MessageBox.Show("Não foi possivel Obter os dados: " + ex.Message); return null; }

        }
        public static DataTable Procurar(string tabla1, string columnas, string Colum1, string palavra, string ItemOrdenador)
        {

            SQLiteDataAdapter da = null;
            DataTable dg = new DataTable();
            try
            {
                var vcom = ConexãoBanco();
                var cmd = vcom.CreateCommand();
                cmd.CommandText = "SELECT " + columnas + " FROM " + tabla1 +" Where " + Colum1 + " LIKE " + palavra+ " ORDER BY " + ItemOrdenador;
                da = new SQLiteDataAdapter(cmd.CommandText, vcom);
                da.Fill(dg);
                vcom.Close();
                return dg;
            }
            catch (Exception ex) { MessageBox.Show("Não foi possivel Obter os dados: " + ex.Message); return null; }

        }
        public static DataTable Procurar2Criterios(string tabla1, string columnas, string Colum1, string Colum2, string palavra1, string palavra2, string ItemOrdenador)
        {

            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                var vcom = ConexãoBanco();
                var cmd = vcom.CreateCommand();
                cmd.CommandText = "SELECT " + columnas + " FROM " + tabla1 + " Where (" + Colum1 + " LIKE " + palavra1 + ") AND (" + Colum2 + " LIKE " + palavra2 + ") ORDER BY " + ItemOrdenador;
                da = new SQLiteDataAdapter(cmd.CommandText, vcom);
                da.Fill(dt);
                vcom.Close();
                return dt;
            }
            catch (Exception ex) { MessageBox.Show("Não foi possivel Obter os dados: " + ex.Message); return null; }

        }
        public static DataTable Procurar3Criterios(string tabla1, string columnas, string Colum1, string Colum2, string Colum3, string palavra1, string palavra2, string palavra3, string ItemOrdenador)
        {

            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                var vcom = ConexãoBanco();
                var cmd = vcom.CreateCommand();
                cmd.CommandText = "SELECT " + columnas + " FROM " + tabla1 + " Where (" + Colum1 + " LIKE " + palavra1 + ") AND (" + Colum2 + " LIKE " + palavra2 + ") AND (" + Colum3 + " LIKE " + palavra3 + ") ORDER BY " + ItemOrdenador;
                da = new SQLiteDataAdapter(cmd.CommandText, vcom);
                da.Fill(dt);
                vcom.Close();
                return dt;
            }
            catch (Exception ex) { MessageBox.Show("Não foi possivel Obter os dados: " + ex.Message); return null; }

        }
        public static DataTable Procurar4Criterios(string tabla1, string columnas, string Colum1, string Colum2, string Colum3, string Colum4, string palavra1, string palavra2, string palavra3, string palavra4, string ItemOrdenador)
        {

            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                var vcom = ConexãoBanco();
                var cmd = vcom.CreateCommand();
                cmd.CommandText = "SELECT " + columnas + " FROM " + tabla1 + " Where (" + Colum1 + " LIKE " + palavra1 + ") AND (" + Colum2 + " LIKE " + palavra2 + ") AND (" + Colum3 + " LIKE " + palavra3 + ") AND (" + Colum4 + " LIKE " + palavra4 + ") ORDER BY " + ItemOrdenador;
                da = new SQLiteDataAdapter(cmd.CommandText, vcom);
                da.Fill(dt);
                vcom.Close();
                return dt;
            }
            catch (Exception ex) { MessageBox.Show("Não foi possivel Obter os dados: " + ex.Message); return null; }

        }
        public static DataTable ProcurarInner(string tabla1, string tabla2, string columnas, string Colum1, string palavra, string ItemOrdenador, string IgualColum1)
        {

            SQLiteDataAdapter da = null;
            DataTable dt = new DataTable();
            try
            {
                var vcom = ConexãoBanco();
                var cmd = vcom.CreateCommand();
                cmd.CommandText = "SELECT " + columnas + " FROM " + tabla1 + " INNER JOIN " + tabla2 + " ON " + tabla1 + "." + IgualColum1 + " = " + tabla2 + "." + IgualColum1+" Where " + Colum1 + " LIKE " + palavra + " ORDER BY " + ItemOrdenador;
                da = new SQLiteDataAdapter(cmd.CommandText, vcom);
                da.Fill(dt);
                vcom.Close();
                return dt;
            }
            catch (Exception ex) { MessageBox.Show("Não foi possivel Obter os dados: " + ex.Message); return null; }

        }


        public static bool Atualizar(string tabela, string Columnas, string Cosa1, string Cosa2)
        {
            SQLiteDataAdapter da;
            try
            {
                int flag = 0;

                var vcom = ConexãoBanco();
                var cmd = vcom.CreateCommand();
                cmd.CommandText = "UPDATE "+tabela+" SET "+Columnas+" WHERE "+Cosa1+"="+Cosa2+"";
                da = new SQLiteDataAdapter(cmd.CommandText, vcom);
                flag = cmd.ExecuteNonQuery();
                vcom.Close();

                if (flag >= 1)
                {
                    return true;
                }
                else { return false; }
            }
            catch (Exception ex)
            {
                MessageBox.Show("NÃO FOI POSSIVEL EXECUTAR: " + ex.Message);
                return false;
            }

        }
        public static bool Atualizar(string tabela, string Columnas, string Cosa1, string Cosa2, string e1, string e2)
        {
            SQLiteDataAdapter da;
            try
            {
                int flag = 0;

                var vcom = ConexãoBanco();
                var cmd = vcom.CreateCommand();
                cmd.CommandText = "UPDATE " + tabela + " SET " + Columnas + " WHERE " + Cosa1 + "=" + e1 + " and "+Cosa2+"="+e2+"";
                da = new SQLiteDataAdapter(cmd.CommandText, vcom);
                flag = cmd.ExecuteNonQuery();
                vcom.Close();

                if (flag >= 1)
                {
                    return true;
                }
                else { return false; }
            }
            catch (Exception ex)
            {
                MessageBox.Show("NÃO FOI POSSIVEL EXECUTAR: " + ex.Message);
                return false;
            }

        }

        public static bool Salvar(string tabela, string Columnas, string Valores)
        {
            SQLiteDataAdapter da;
            
            try
            {
                int flag = 0;

                var vcom = ConexãoBanco();
                var cmd = vcom.CreateCommand();
                cmd.CommandText = "INSERT INTO " + tabela + " (" + Columnas + ") VALUES (" + Valores +")";
                da = new SQLiteDataAdapter(cmd.CommandText, vcom);
                flag = cmd.ExecuteNonQuery();
                vcom.Close();

                if (flag >= 1)
                {
                   return true;
                }
                else { return false; }

            }
            catch (Exception ex)
            {
                MessageBox.Show("NÃO FOI POSSIVEL EXECUTAR: " + ex.Message);
                return false;
            }

        }
        public static bool Excluir(string tabela,  string Cosa1, string Cosa2)
        {
            DialogResult res = MessageBox.Show("Certeza que deseja Excluir o item: \n\n"+Cosa2, "Excluir", MessageBoxButtons.YesNo);
            if (res == DialogResult.Yes)
            {
                SQLiteDataAdapter da;
                
                try
                {
                    int flag = 0;

                    var vcom = ConexãoBanco();
                    var cmd = vcom.CreateCommand();
                    cmd.CommandText = "DELETE FROM " + tabela +  " WHERE " + Cosa1 + "=" + Cosa2 + "";
                    da = new SQLiteDataAdapter(cmd.CommandText, vcom);
                    flag = cmd.ExecuteNonQuery();
                    vcom.Close();

                    if (flag >= 1)
                    {
                        return true;
                        
                    }
                    else { return false; }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("NÃO FOI POSSIVEL EXECUTAR: " + ex.Message); 
                    return false;
                }
                
               
            }
            return false;

        }
        public static bool ExcluirDirecto(string tabela, string Cosa1, string Cosa2)
        {
           
                SQLiteDataAdapter da;

                try
                {
                    int flag = 0;

                    var vcom = ConexãoBanco();
                    var cmd = vcom.CreateCommand();
                    cmd.CommandText = "DELETE FROM " + tabela + " WHERE " + Cosa1 + "=" + Cosa2 + "";
                    da = new SQLiteDataAdapter(cmd.CommandText, vcom);
                    flag = cmd.ExecuteNonQuery();
                    vcom.Close();

                    if (flag >= 1)
                    {
                        return true;

                    }
                    else {return false; }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("NÃO FOI POSSIVEL EXECUTAR: " + ex.Message);
                    return false;
                }


           

        }
        
        public static bool Excluir(string tabela, string OndeIsto, string IgualPalavra, string nome)
        {
            DialogResult res = MessageBox.Show("Certeza que deseja Excluir o item:\n\n"+nome, "Excluir", MessageBoxButtons.YesNo);
            if (res == DialogResult.Yes)
            {

                SQLiteDataAdapter da;

                try
                {
                    int flag = 0;

                    var vcom = ConexãoBanco();
                    var cmd = vcom.CreateCommand();
                    cmd.CommandText = "DELETE FROM " + tabela + " WHERE " + OndeIsto + "=" + IgualPalavra + "";
                    da = new SQLiteDataAdapter(cmd.CommandText, vcom);
                    flag = cmd.ExecuteNonQuery();
                    vcom.Close();

                    if (flag >= 1)
                    {
                        return true;
                    }
                    else { MessageBox.Show("ERRO"); return false; }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("NÃO FOI POSSIVEL EXECUTAR: " + ex.Message);
                    return false;
                }
              

            }
            return false;
        }


    }



}

