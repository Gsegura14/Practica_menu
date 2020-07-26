using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Practica_menu
{
    public partial class MaestroClientes : Form
    {
        public CConexionBD conexionBD = new CConexionBD();
        public MaestroClientes()
        {
            InitializeComponent();
        }

       
        private void Recargar(int rowIndex = 0)
        {
            CClientesBD clientesBD = new CClientesBD();

            dataGridViewMClientes.DataSource = clientesBD.Seleccionar();

            if (dataGridViewMClientes.RowCount > 0)
            {
                if (rowIndex >= dataGridViewMClientes.RowCount)
                    rowIndex = dataGridViewMClientes.RowCount - 1;
                if (rowIndex < 0)
                {
                    rowIndex = 0;
                    dataGridViewMClientes.CurrentCell = dataGridViewMClientes[1, rowIndex];
                }
            }
          
        }

        private void MaestroClientes_Load(object sender, EventArgs e)
        {
            Recargar();
            dataGridViewMClientes.AllowUserToAddRows = false;
            dataGridViewMClientes.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            // dataGridViewMClientes.Columns["Cliente_id"].Visible = false;
            dataGridViewMClientes.DefaultCellStyle.NullValue = "---";
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            string codigo = dataGridViewMClientes.CurrentRow.Cells[1].Value.ToString();
            string nombre = dataGridViewMClientes.CurrentRow.Cells[2].Value.ToString();
            string direccion = dataGridViewMClientes.CurrentRow.Cells[4].Value.ToString();
            string telefono = dataGridViewMClientes.CurrentRow.Cells[9].Value.ToString();
            // string codigoCliente = dataGridViewMClientes.CurrentRow.Cells[4].Value.ToString();
            
            SDClientes.codigoCliente = codigo;
            SDClientes.nombreCliente = nombre;
            SDClientes.direccionCliente = direccion;
            SDClientes.telefonoCliente = telefono;
            
            this.Close();
        }

        private void txtBuscaClientes_KeyUp(object sender, KeyEventArgs e)
        {
            conexionBD.Abrir();
            SqlCommand sqlCommand= new SqlCommand();
            sqlCommand.Connection = conexionBD.Connection;

            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.CommandText = "SELECT * FROM clientes WHERE cliente LIKE ('" + txtBuscaClientes.Text + "%')";
            sqlCommand.ExecuteNonQuery();

            DataTable dataTable = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(sqlCommand);

            da.Fill(dataTable);
            dataGridViewMClientes.DataSource = dataTable;
            conexionBD.Cerrar();
        }

        private void txtBuscaClientes_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
