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
    public class CProductosBD : CConexionBD
    {
        private CConexionBD conexionBD = new CConexionBD();
        private SqlCommand sqlCommand = new SqlCommand();
        private SqlDataReader sqlDataReader;


        public int Producto_id { get; set; }
        public string Codigo { get; set; }
        public string Producto { get; set; }
        public double Precio { get; set; }
        public double Iva { get; set; }
        public int Iva_id { get; set; }
        public double Importe { get; set; }

        public DataTable Seleccionar (int producto_id = 0)
        {
            DataTable dataTable = new DataTable();
            try
            {
                conexionBD.Abrir();
                sqlCommand.Connection = conexionBD.Connection;
                sqlCommand.CommandType = CommandType.Text;

                if (producto_id == 0)
                {
                    sqlCommand.CommandText =
                        "SELECT producto_id AS ID,codigo AS Codigo,producto AS Producto,precio AS precio,ivas.iva AS IVA,importe AS importe" +
                        " FROM productos" +
                        " INNER JOIN ivas ON ivas.iva_id=productos.iva_id" +
                        " ORDER BY codigo";
                }
                else
                {
                    sqlCommand.CommandText =
                        "SELECT producto_id AS Id,codigo AS Codigo,producto AS Producto,precio AS Precio,ivas.iva_id AS ID_IVA,ivas.iva AS IVA,importe AS Importe" +
                        " FROM productos" +
                        " INNER JOIN ivas ON ivas.iva_id=productos.iva_id" +
                        " WHERE producto_id=" + producto_id;
                }
                sqlDataReader = sqlCommand.ExecuteReader();
                dataTable.Load(sqlDataReader);

                if ((producto_id != 0) && (dataTable.Rows.Count != 0))
                {
                    DataRow[] rows = dataTable.Select();
                    Producto_id = producto_id;
                    Codigo = rows[0]["codigo"].ToString();
                    Producto = rows[0]["producto"].ToString();
                    Precio = Convert.ToDouble(rows[0]["precio"].ToString());
                    //Iva_id = Convert.ToInt32(rows[0]["iva_id"].ToString());
                    Iva = Convert.ToDouble(rows[0]["iva"].ToString());
                    Importe = Convert.ToDouble(rows[0]["importe"].ToString());


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
                    string.Format("INSERT INTO productos VALUES ('{0}','{1}',{2},{3},{4})",
                                    Codigo, Producto, Convert.ToString(Precio).Replace(",", "."), Convert.ToString(Iva_id), Convert.ToString(Importe).Replace(",", "."));
                bInsertada = sqlCommand.ExecuteNonQuery() == 1;
                if (bInsertada)
                {
                    Producto_id = UltimoId();
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
                string.Format("UPDATE productos SET codigo='{0}',producto='{1}',precio={2},iva_id={3},importe={4}" +
                " WHERE producto_id={5}",
                    Codigo, Producto, Convert.ToString(Precio).Replace(",", "."),Convert.ToString(Iva_id), Convert.ToString(Importe).Replace(",", "."),Convert.ToString(Producto_id));
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
                sqlCommand.CommandText = "DELETE productos WHERE producto_id =" + Producto_id;
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