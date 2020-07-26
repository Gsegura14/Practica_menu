using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Practica_menu
{
    public class CConexionAlbaranes
    {
      public  SqlConnection ConexionAlb = new SqlConnection("Server=DESKTOP-BHI57B9\\SQLEXPRESS;DataBase=uERP;Integrated Security=true;");

        

    }
}
