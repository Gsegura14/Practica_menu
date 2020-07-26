﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Practica_menu
{
    class CProvinciasBD : CConexionBD
    {
        private CConexionBD conexionBD = new CConexionBD();
        private SqlCommand sqlCommand = new SqlCommand();
        private SqlDataReader sqlDataReader;
        public int Provincia_id { get; set; }
        public string Provincia { get; set; }

        public DataTable Seleccionar(int provincia_id = 0)
        {
            DataTable dataTable = new DataTable();

            try
            {
                conexionBD.Abrir();
                sqlCommand.Connection = conexionBD.Connection;
                sqlCommand.CommandType = CommandType.Text;

                if (provincia_id == 0)
                {
                    sqlCommand.CommandText = "SELECT * FROM provincias ORDER BY provincia ASC";
                }
                else
                {
                    sqlCommand.CommandText = "SELECT * FROM provincias WHERE provincia_id=" + provincia_id;
                }
                sqlDataReader = sqlCommand.ExecuteReader();
                dataTable.Load(sqlDataReader);
                if ((provincia_id != 0) && (dataTable.Rows.Count != 0))
                {
                    DataRow[] rows = dataTable.Select();
                    Provincia_id = provincia_id;
                    Provincia = rows[0]["provincia"].ToString();
                }
            }
            finally
            {
                sqlCommand.Parameters.Clear();
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
                    string.Format("INSERT INTO provincias VALUES ('{0}')",
                    Provincia);
                bInsertada = sqlCommand.ExecuteNonQuery() == 1;
                if (bInsertada)
                {
                    Provincia_id = UltimoId();
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
                    string.Format("UPDATE provincias SET provincia='{0}'"+
                    " WHERE provincia_id={1}",
                    Provincia,Provincia_id);
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
                sqlCommand.CommandText = "DELETE provincias WHERE provincia_id=" + Provincia_id;
                sqlCommand.CommandType = CommandType.Text;
                bBorrada = sqlCommand.ExecuteNonQuery() == 1;
            }
            finally {
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
