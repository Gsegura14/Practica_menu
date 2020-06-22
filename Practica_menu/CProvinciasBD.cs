using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;

namespace Practica_menu
{
   public class CProvinciasBD : CConexionBD
    {
        //conectar a la bbdd
        private CConexionBD conexionBD = new CConexionBD();
        // ejecutar script procedimientos almacenados
        private SqlCommand sqlCommand = new SqlCommand();

        //Almacenar los datos de una sentencia SELECT
        private SqlDataReader SqlDataReader;

        //Propiedades para almacenar los datos de un registro de la tabla.

        public int Provincia_id { get; set; }
        public int Codigo { get; set; }
        public String Provincia { get; set; }

        public DataTable Seleccionar(int provincia_id = 0)
        {
            // Almacenar la tabla leída en memoria.

            DataTable dataTable = new DataTable();

            try
            {
                // Realizamos la conexión.
                conexionBD.Abrir();
                // Y le asignamos al mcomando SQL
                sqlCommand.Connection = conexionBD.Connection;
                //Indicamos el tipo de comando. En este caso una sentencia SQL

                sqlCommand.CommandType = CommandType.Text;
                // Si me han pedido todas las provincias

                if (provincia_id == 0)
                {
                    sqlCommand.CommandText =
                        "SELECT provincia_id AS Id,codigo AS Codigo,provincia AS Provincia" +
                        " FROM provincias";
                }
                else
                {
                    //en caso contrario una provincia en concreto
                    sqlCommand.CommandText =
                        "SELECT provincia_id AS Id,codigo AS Codigo, provincia AS Provincia" +
                        " FROM provincias" +
                        " WHERE provincia_id=" + Provincia_id;
                }
                //Ejecutamos la sentencia
                SqlDataReader = sqlCommand.ExecuteReader();
                // Guardamos el resultado en memoria
                dataTable.Load(SqlDataReader);
                // Si me indicaron que seleccionase un unico registro y este ya existe

                if ((Provincia_id != 0) && (dataTable.Rows.Count != 0))
                {
                    // obtenemos las filas de la tabla en memoria 
                    DataRow[] rows = dataTable.Select();
                    //Asignamos a cada propiedad el valor del registro leido
                    Provincia_id = provincia_id;
                    Codigo = Convert.ToInt32(rows[0]["codigo"].ToString());
                    Provincia = rows[0]["provincia"].ToString();




                }
            }
            finally
            {
                //Cerramos los datos leídos
                SqlDataReader.Close();

                // Cerramos la conexión.
                conexionBD.Cerrar();
            }
            // Devolvemos la tabla almacenada en memoria
            return dataTable;
        }

        public bool Insertar()
        {
            // Para devolver si la operación se hizo correctamente, o no.
            bool bInsertada = false;
            try
            {
                // Similar a la selección , salvo a la sentencia SQL
                conexionBD.Abrir();
                sqlCommand.Connection = conexionBD.Connection;
                sqlCommand.CommandType = CommandType.Text;
                // Hemos utilizado Format para ocnstruir la sentencia.
                sqlCommand.CommandText =
                    string.Format("INSERT INTO provincias VALUES ({0},'{1}')",
                                   Codigo, Convert.ToString(Provincia));
                // Ejecutamos la sentencia, indicando que no es suna consulta SELECT y
                // aprovechamos el número de registros que nos devuelve.En este caso debe ser 1.
                bInsertada = sqlCommand.ExecuteNonQuery() == 1;
                // Se la inserci´n fue correcta,obtenemos el valor de la clave primaria.
                if (bInsertada)
                {
                    Provincia_id = UltimoId();
                }
            }
            finally
            {
                conexionBD.Cerrar();
            }
            // Devolvemos si la operación fue correcta o no.
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
                    string.Format("UPDATE provincias SET Provincia_id={0}, Codigo ={1}, Provincia = '{2}'" +
                    " WHERE provincia_id={4}", Provincia_id, Codigo, Convert.ToString(Provincia));
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
                sqlCommand.CommandText = "DELETE provincias WHERE Provincia_id=" + Provincia_id;
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
                // Esta sentencia obtiene la última provincia insertada.
                SqlDataReader = sqlCommand.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(SqlDataReader);
                DataRow[] rows = dataTable.Select();
                // Obtenemos la clave primaria de la última provincia insertada.
                ultimo_id = Convert.ToInt32(rows[0]["ultimo_id"].ToString());
            }
            finally
            {
                SqlDataReader.Close();
            }
            return ultimo_id;
        }
    }
}
