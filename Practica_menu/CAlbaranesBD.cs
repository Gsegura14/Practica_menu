using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica_menu
{

    class CAlbaranesBD : CConexionBD

    {
        private CConexionBD conexionBD = new CConexionBD();
        private SqlCommand sqlCommand = new SqlCommand();
        private SqlDataReader sqlDataReader;
        
        public int Albaran_id { get; set; }
        public int Numero_alb { get; set; }
        public DateTime Fecha { get; set; }
        public int Cliente_id { get; set; }

        public DataTable Seleccionar (int albaran_id = 0)
        {
            DataTable dataTable = new DataTable();
            try
            {
                conexionBD.Abrir();
                sqlCommand.Connection = conexionBD.Connection;
                sqlCommand.CommandType = CommandType.Text;

                if (albaran_id == 0)
                    sqlCommand.CommandText = "SELECT * FROM albaranes";
                else
                    sqlCommand.CommandText = "SELECT * FROM albaranes WHERE albaran_id=" + albaran_id;
                sqlDataReader = sqlCommand.ExecuteReader();
                dataTable.Load(sqlDataReader);

                if ((albaran_id!=0)&&(dataTable.Rows.Count != 0))
                {
                    DataRow[] rows = dataTable.Select();
                    Albaran_id = albaran_id;
                    Numero_alb = albaran_id;
                    Fecha = Convert.ToDateTime(rows[0]["fecha"].ToString());
                    Cliente_id = Convert.ToInt32(rows[0]["cliente_id"].ToString());
                }
            }
            finally
            {
                sqlDataReader.Close();
                conexionBD.Cerrar();
            }
            return dataTable;

        }
        public bool Insertar()
        {
            bool bInsertada = false;
            try
            {
                conexionBD.Abrir();
                sqlCommand.Connection = conexionBD.Connection;
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText =
                    string.Format("INSERT INTO albaranes VALUES ({0},{1},{2})",
                    Convert.ToString(Numero_alb), Fecha, Convert.ToString(Cliente_id));
                bInsertada = sqlCommand.ExecuteNonQuery() == 1;
                if (bInsertada)
                {
                    Albaran_id = UltimoId();
                }

            }
            finally
            {
                conexionBD.Cerrar();
            }
            return bInsertada;
        }

        private int UltimoId()
        {
            int ultimo_id = 0;
            try
            {
                sqlCommand.CommandText = "SELECT @@IDENTITY as ultimo_id";
                sqlDataReader = sqlCommand.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(sqlDataReader);
                DataRow[] rows = dataTable.Select();
                ultimo_id = Convert.ToInt32(rows[0]["ultimo_id"].ToString());

            }
            finally
            {
                sqlDataReader.Close();
            }
            return ultimo_id;
        }
    
    }

    

}
