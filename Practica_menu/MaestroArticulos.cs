using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Text;
using System.Windows.Forms;

namespace Practica_menu
{
    public partial class MaestroArticulos : Form
    {
        public CConexionBD conexionBD = new CConexionBD();
       
        
        public MaestroArticulos()
        {
            InitializeComponent();
        }

        private void MaestroArticulos_Load(object sender, EventArgs e)
        {

            Recargar();
            dataGridViewArticulos.AllowUserToAddRows = false;
            dataGridViewArticulos.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewArticulos.Columns["ID"].Visible = false;
            dataGridViewArticulos.Columns["precio"].DefaultCellStyle.Format = "c";
            dataGridViewArticulos.Columns["precio"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewArticulos.DefaultCellStyle.NullValue = "---";

        }
        private void Recargar(int rowIndex = 0)
        {
            CProductosBD productosBD = new CProductosBD();
            dataGridViewArticulos.DataSource = productosBD.Seleccionar();

            if (dataGridViewArticulos.RowCount > 0)
            {
                if (rowIndex >= dataGridViewArticulos.RowCount)
                    rowIndex = dataGridViewArticulos.RowCount - 1;
                if (rowIndex < 0)
                {
                    rowIndex = 0;
                    dataGridViewArticulos.CurrentCell = dataGridViewArticulos[1, rowIndex];
                }
            }

        }



        private void btnseleccionar_Click(object sender, EventArgs e)
        {
            string codigo = dataGridViewArticulos.CurrentRow.Cells[1].Value.ToString();
            string producto = dataGridViewArticulos.CurrentRow.Cells[2].Value.ToString();
            string precio = dataGridViewArticulos.CurrentRow.Cells[3].Value.ToString();
            Selecciona_Datos.codigo = codigo;
            Selecciona_Datos.producto = producto;
            Selecciona_Datos.precio = precio;
            this.Close();
        }

        private void txtBuscar_KeyUp(object sender, KeyEventArgs e)
        {
         

            conexionBD.Abrir();
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = conexionBD.Connection;
         
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.CommandText = "SELECT * FROM productos WHERE producto LIKE ('" + txtBuscar.Text + "%')";
            sqlCommand.ExecuteNonQuery();

            DataTable dataTable = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(sqlCommand);

            da.Fill(dataTable);
            dataGridViewArticulos.DataSource = dataTable;
            conexionBD.Cerrar();
           
          

        }
    }
}
