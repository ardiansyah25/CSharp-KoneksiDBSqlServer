using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//1.
using System.Data.SqlClient;

namespace KoneksiDBSQLServer
{
    class Koneksi
    {
        //2.
        public SqlConnection GetConn()
        {
            SqlConnection Conn = new SqlConnection();
            Conn.ConnectionString = "Data source = ITD027; initial catalog=DB_CONTOH; integrated security=true";
            return Conn;
        }
    }
}
