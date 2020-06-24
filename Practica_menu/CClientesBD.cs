using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica_menu
{
    public class CClientesBD : CConexionBD
    {
        // Conexion
        private CConexionBD conexionBD = new CConexionBD();
        //Ejecutar procedimiento o ejecutar las sentencias SQL
        private SqlCommand sqlCommand = new SqlCommand();
        // Almacenar los datos de una sentencia SELECT
        private SqlDataReader SqlDataReader;

        // Propiedades para almacenar los datos de un registro de la tabla.
        public int Cliente_id { get; set; }
        public int Codigo_id { get; set; }
        public String Cliente { get; set; }
        public String Cif { get; set; }
        public String Direccion { get; set; }
        public String Cp { get; set; }
        public String Poblacion { get; set; }
        public int Provincia_id { get; set; }
        public String Telefono { get; set; }
        public String Email { get; set; }

        public DataTable Seleccionar(int cliente_id = 0)
        {
            //Almacenar la tabla leída
            DataTable dataTable = new DataTable();
            try
            {
                //Realizamos la conexion
                conexionBD.Abrir();

                //Asignamos al comado SQL
                sqlCommand.Connection = conexionBD.Connection;

                //Indicamos el tipo de comando 
                sqlCommand.CommandType = CommandType.Text;

                // Si piden todos los clientes
                if (cliente_id == 0)
                {
                    sqlCommand.CommandText =
                        "SELECT cliente_id AS Id,clientes.codigo AS Codigo,cliente AS cliente,cif AS CIF,direccion AS Dirección," + 
                        " cp AS Código_Postal,poblacion AS Población,provincias.provincia_id AS Código_Provincia,"+
                        " telefono AS Teléfono,email AS Email"+
                        " FROM clientes INNER JOIN provincias ON clientes.provincia_id = provincias.provincia_id"+
                        " ORDER BY codigo";



                }
                else
                {
                    // En caso contrario un cliente en concreto.
                    sqlCommand.CommandText =
                        "SELECT cliente_id AS Id,clientes.codigo AS Codigo,cliente AS cliente,cif AS CIF,direccion AS Dirección," +
                        " cp AS Código_Postal,poblacion AS Población,provincias.provincia_id AS Código_Provincia,telefono AS Teléfono" +
                        " email AS Email" +
                        " FROM clientes" +
                        " INNER JOIN provincias ON clientes.provincia_id=provincias.provincia_id" +
                        " WHERE cliente_id=" + cliente_id;
                }
                //Ejecutamos la sentencia
                SqlDataReader = sqlCommand.ExecuteReader();

                // Guardamos el resutlaqdo en la memoria
                dataTable.Load(SqlDataReader);

                // Si me indicaton que le seleccionase un único registro, y este existe.
                if ((Cliente_id != 0) && (dataTable.Rows.Count != 0))
                {
                    // Obtenemos las filas de la table en memoria.
                    DataRow[] rows = dataTable.Select();

                    // ASignamos a cada propiedad el valor del registro leido
                    Cliente_id = cliente_id;
                    Codigo_id = Convert.ToInt32((rows[0])["codigo_id"].ToString());
                    Cliente = rows[0]["cliente"].ToString();
                    Cif = rows[0]["cif"].ToString();
                    Direccion = rows[0]["direccion"].ToString();
                    Cp = rows[0]["cp"].ToString();
                    Poblacion = rows[0]["poblacion"].ToString();
                    Provincia_id = Convert.ToInt32((rows[0])["provincia_id"].ToString());
                    Telefono = rows[0]["telefono"].ToString();
                    Email = rows[0]["email"].ToString();
                }
            }
            finally
            {
                //Cerramos los datos leídos
                SqlDataReader.Close();
                //Cerramos la conexion
                conexionBD.Cerrar();
            }
            //Devolvemos la tabla almacenada en memeria
            return dataTable;

        }
        public bool Insertar()
        {
            // Para devolver si la operacion se hizo corerctamente o no.
            bool bInsertada = false;
            try
            {
                // Es similar a la seleccion,salvo en la sentencia SQL
                conexionBD.Abrir();
                sqlCommand.Connection = conexionBD.Connection;
                sqlCommand.CommandType = CommandType.Text;
                // Hemos utilizado format para construis la sentencia
                //El cliente se ha pueste entre comillas('{2}') `porque es una cadena

                sqlCommand.CommandText =
                        string.Format("INSERT INTO clientes VALUES ({0},'{1}','{2}','{3}','{4}','{5}',{6},'{7}','{8}'",
                        Codigo_id,Convert.ToString(Cliente));
                        //Ejecutamos la sentencia,indicando que no es una consulta SELECT,
                        //Aprovechamos el numero  de registros que nos decuelce en este caso debe ser 1

                        bInsertada = sqlCommand.ExecuteNonQuery() == 1;
                //Si la inserccion fue correcta,obtenemos el valor de la clave primaria

                if (bInsertada)
                {
                    Cliente_id = UltimoId();

                }
            }
            finally
            {
                conexionBD.Cerrar();
            }
            //Devolvemos si la operacion fue correcta o no
            return bInsertada;
        }
        public bool Editar()
        {
            bool bEditada = false;
            try {
                conexionBD.Abrir();
                sqlCommand.Connection = conexionBD.Connection;
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText =
                    string.Format("UPDATE clientes SET Cliente_id={0}, Codigo_id={1},Cliente='{2}',Cif='{3},'Direccion='{4}',cp='{5}',Poblacion='{6}',Provincia_id={7},Telefono='{8}',email='{9}'" +
                    " WHERE codigo_id={0}",
                    Cliente_id,Codigo_id,Convert.ToString(Cliente),Convert.ToString(Cif),Convert.ToString(Direccion),Convert.ToString(Cp),Convert.ToString(Poblacion), Provincia_id,Convert.ToString(Telefono),Convert.ToString(Email));
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
                sqlCommand.CommandText = "DELETE clientes WHERE cliente_id=" + Cliente_id;
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
                // Esta sentencia obtiene el ultiom producto insertado
                sqlCommand.CommandText = "SELECT @@IDENTITY as ultimo_id";
                SqlDataReader = sqlCommand.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(SqlDataReader);
                DataRow[] rows = dataTable.Select();
                // Obtenemos la clace primaria del ultimo producto insertado.
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
