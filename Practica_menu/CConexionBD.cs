using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Practica_menu
{
    // Conexion servidor
    public class CConexionBD
    {
        static private string cadenaConexion = @"Server=DESKTOP-BHI57B9\SQLEXPRESS;DataBase=uERP;Integrated Security=true;";
        // Conexión a la base de datos.
        public SqlConnection Connection { get; } = new SqlConnection(cadenaConexion);

        public void Abrir()
        {
            //abrir si está cerrada
            if (Connection.State == ConnectionState.Closed)
                Connection.Open();
          
            
        }
        public void Cerrar()
        {
            //cerrar si está abierta
            if (Connection.State == ConnectionState.Open)
                Connection.Close();
        }



    }
}
