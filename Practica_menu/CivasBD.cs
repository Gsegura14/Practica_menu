using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica_menu
{
    class CivasBD : CConexionBD
    {
        private CConexionBD conexionBD = new CConexionBD();
        private SqlCommand sqlCommand = new SqlCommand();
        private SqlDataReader sqlDataReader;

        public int Iva_id { get; set; }
        public double Iva { get; set; }
        public double Re { get; set; }

        public DataTable Seleccionar(int iva_id = 0)
        {
            DataTable dataTable = new DataTable();
            try
            {
                conexionBD.Abrir();
                sqlCommand.Connection = conexionBD.Connection;
                sqlCommand.CommandType = CommandType.Text;

                if (iva_id == 0)
                    sqlCommand.CommandText = "SELECT * FROM ivas ORDER BY iva ASC";
                else
                    sqlCommand.CommandText = "SELECT * FROM ivas WHERE iva_id=" + iva_id;
                sqlDataReader = sqlCommand.ExecuteReader();
                dataTable.Load(sqlDataReader);

                if ((iva_id!=0) && (dataTable.Rows.Count != 0))
                {
                    DataRow[] rows=dataTable.Select();
                    Iva_id = iva_id;
                    Iva = Convert.ToDouble(rows[0]["iva"].ToString());
                    Re = Convert.ToDouble(rows[0]["re"].ToString());

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
                    string.Format("INSERT INTO ivas VALUES ({0},{1})",
                    Convert.ToString(Iva).Replace(",","."),
                    Convert.ToString(Re).Replace(",","."));

                bInsertada = sqlCommand.ExecuteNonQuery() == 1;
                if (bInsertada)
                {
                    Iva_id = UltimoId();
                }

            }
            finally
            {
                conexionBD.Cerrar();
            }
            return bInsertada;
        }
        public bool Editar()
        {
            bool bEditada = false;
            try
            {
                conexionBD.Abrir();
                sqlCommand.Connection = conexionBD.Connection;
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText =
                    string.Format("UPDATE ivas SET iva={0},re={1}" +
                        "WHERE iva_id={2}",
                        Convert.ToString(Iva).Replace(",", "."), Convert.ToString(Re).Replace(",", "."),
                        Iva_id);
                bEditada = sqlCommand.ExecuteNonQuery() == 1;
            }
            finally
            {
                conexionBD.Cerrar();
            }
            return bEditada;
        }
        public bool Borrar()
        {
            bool bBorrada = false;
            try
            {
                conexionBD.Abrir();
                sqlCommand.Connection = conexionBD.Connection;
                sqlCommand.CommandText = "DELETE ivas WHERE iva_id=" + Iva_id;
                sqlCommand.CommandType = CommandType.Text;
                bBorrada = sqlCommand.ExecuteNonQuery() == 1;
            }
            finally
            {
                conexionBD.Cerrar();

            }
            return bBorrada;
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
