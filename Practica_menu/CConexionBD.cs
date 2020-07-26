using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Practica_menu
{
   public class CConexionBD
    {
        static private string cadenaConexion = @"Server=DESKTOP-BHI57B9\SQLEXPRESS;DataBase=uERP;Integrated Security=true;";

        public SqlConnection Connection { get; } = new SqlConnection(cadenaConexion);

        public void Abrir()
        {
            if (Connection.State == ConnectionState.Closed)
                Connection.Open();
        }
        public void Cerrar()
        {
            if (Connection.State == ConnectionState.Open)
                Connection.Close();
                
        }
    }
}
