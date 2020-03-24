using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace RendszerfejlesztesServer
{
    class db
    {
        private SQLiteConnection con = new SQLiteConnection("Data source=kdb.db");

        public SQLiteConnection GetConnection()
        {
            return con;
        }
        public void openConnection()
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
        }

        // create a function to close the connection
        public void closeConnection()
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
        }
    }
}
