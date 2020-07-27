using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Practica_menu
{


    public partial class FAlbaranBD : Form

    {
        public SqlConnection ConexionAlb = new SqlConnection("Server=DESKTOP-BHI57B9\\SQLEXPRESS;DataBase=uERP;Integrated Security=true;");



        public FAlbaranBD()
        {
            InitializeComponent();

        }

        private void FAlbaranBD_Load(object sender, EventArgs e)
        {
            int numero = 0;
            CargarNumeroAlbaran(ref numero);
            //numero++;
            txtId.Text = Convert.ToString(numero);
            DateTime fecha = DateTime.Today;
            txtFecha.Text = fecha.ToString("d");
        }



        private void btnBuscaProducto_Click(object sender, EventArgs e)
        {

            MaestroArticulos maestroProductos = new MaestroArticulos();
            maestroProductos.ShowDialog();
            //Autocompletar al seleccionar un articulo del maestro de articulos
            txtCodigo.Text = Selecciona_Datos.codigo;
            txtProducto.Text = Selecciona_Datos.producto;
            txtPrecio.Text = Selecciona_Datos.precio;
            txtPrecio.Text.Replace(",", ".");


        }



        private void txtCantidad_Leave(object sender, EventArgs e)
        {
            try {
                double precio = 0;
                double cantidad = 0;
                double total = 0;
                precio = Convert.ToDouble(txtPrecio.Text);
                cantidad = Convert.ToDouble(txtCantidad.Text);
                total = cantidad * precio;
                txtTotal.Text = total.ToString();

            }
            catch
            {
                MessageBox.Show("La cantidad no puede ser 0", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }




        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            MaestroClientes maestroClientes = new MaestroClientes();
            maestroClientes.ShowDialog();
            lblNombreCliente.Text = SDClientes.nombreCliente;
            lblDireccionCliente.Text = SDClientes.direccionCliente;
            lblTelefonoCliente.Text = SDClientes.telefonoCliente;
            txtIdCliente.Text = SDClientes.codigoCliente;

        }




        private void btnAceptarCliente_Click(object sender, EventArgs e)
        {
            try
            {
                ConexionAlb.Open();

                btnBuscaProducto.Enabled = true;
                txtCodigo.Enabled = true;

                string consulta = "INSERT INTO albaranes (numero,fecha,cliente_id)" +
                    " VALUES (@numero,@fecha,@clienteid)";
                int codigo = Convert.ToInt32(txtIdCliente.Text);

                CargarIdCliente(ref codigo);

                SqlCommand comando = new SqlCommand(consulta, ConexionAlb);
                comando.Parameters.AddWithValue("@numero", Convert.ToInt32(txtId.Text));
                comando.Parameters.AddWithValue("@fecha", Convert.ToDateTime(txtFecha.Text));

                comando.Parameters.AddWithValue("@clienteid", codigo);
                comando.ExecuteNonQuery();
                ConexionAlb.Close();
            }
            catch
            {
                MessageBox.Show("El cliente no puede ser vacio. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ConexionAlb.Close();
            }
        }

        static void CargarIdCliente(ref int IdParam)
        {
            CClientesBD clientesBD = new CClientesBD();
           
            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader sqlDataReader;
            DataTable dataTable = new DataTable();
            clientesBD.Abrir();
            sqlCommand.Connection = clientesBD.Connection;
            sqlCommand.CommandType = CommandType.Text;


             sqlCommand.CommandText= "SELECT cliente_id FROM clientes WHERE codigo=" + IdParam;
             sqlDataReader = sqlCommand.ExecuteReader();
             dataTable.Load(sqlDataReader);
             DataRow[] rows = dataTable.Select();
             IdParam = Convert.ToInt32(rows[0]["cliente_id"].ToString());
           

        }
        static void CargarNumeroAlbaran(ref int NumParam)
        {
            CAlbaranesBD cAlbaranesBD = new CAlbaranesBD();
            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader sqlDataReader;
            DataTable dataTable = new DataTable();
            cAlbaranesBD.Abrir();
            sqlCommand.Connection = cAlbaranesBD.Connection;
            sqlCommand.CommandType = CommandType.Text;

            sqlCommand.CommandText = "SELECT MAX(numero) as numero FROM albaranes";
            sqlDataReader = sqlCommand.ExecuteReader();
            dataTable.Load(sqlDataReader);
            DataRow[] rows = dataTable.Select();
            NumParam = (Convert.ToInt32(rows[0]["numero"].ToString())) + 1;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            int n = dataGridViewlineas.Rows.Add();
            
            int NumAlbaran = Convert.ToInt32(txtId.Text);
            cargarIdAlbaran(ref NumAlbaran);
            string IdProducto =(txtCodigo.Text);
            cargarIdProducto(ref IdProducto);
           


            dataGridViewlineas.Rows[n].Cells[0].Value = NumAlbaran;
            dataGridViewlineas.Rows[n].Cells[1].Value = txtCantidad.Text;
            dataGridViewlineas.Rows[n].Cells[2].Value = IdProducto;
            dataGridViewlineas.Rows[n].Cells[3].Value = txtPrecio.Text.Replace(".",",");
            dataGridViewlineas.Rows[n].Cells[6].Value = txtTotal.Text.Replace(".",".");
            txtCodigo.Text = "";
            txtProducto.Text = "";
            txtCantidad.Text = "";
            txtTotal.Text = "";

            

        
            
        }

        private void btnTerminar_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand guardar = new SqlCommand("INSERT INTO albaraneslineas (albaran_id,cantidad,producto_id,precio,iva,re,importe) VALUES (@albaranid,@cantidad,@productoid,@precio,@iva,@re,@importe)", ConexionAlb);
                ConexionAlb.Open();


                foreach (DataGridViewRow row in dataGridViewlineas.Rows)
                {
                    guardar.Parameters.Clear();

                    guardar.Parameters.AddWithValue("@albaranid", Convert.ToInt32(row.Cells["Albaran_id"].Value));
                    guardar.Parameters.AddWithValue("@cantidad", Convert.ToInt32(row.Cells["Cantidad"].Value));
                    guardar.Parameters.AddWithValue("@productoid", Convert.ToInt32(row.Cells["Producto_id"].Value));
                    guardar.Parameters.AddWithValue("@precio", Convert.ToDouble(row.Cells["Precio"].Value));
                    guardar.Parameters.AddWithValue("@iva", 0);
                    guardar.Parameters.AddWithValue("@re", 0);
                    guardar.Parameters.AddWithValue("@importe", Convert.ToDouble(row.Cells["Importe"].Value));
                    guardar.ExecuteNonQuery();

                }
            }
            finally
            {
                this.Controls.Clear();
                this.InitializeComponent();
                ConexionAlb.Close();
                DateTime fecha = DateTime.Today;
                txtFecha.Text = fecha.ToString("d");
            }
            
            



        }
        static void cargarIdAlbaran(ref int NumParam)
        {
            
                CAlbaranesBD cAlbaranesBD = new CAlbaranesBD();
                SqlCommand sqlCommand = new SqlCommand();
                SqlDataReader sqlDataReader;
                DataTable dataTable = new DataTable();
                cAlbaranesBD.Abrir();
                sqlCommand.Connection = cAlbaranesBD.Connection;
                sqlCommand.CommandType = CommandType.Text;

                sqlCommand.CommandText = "SELECT albaran_id FROM albaranes WHERE numero =" + NumParam;
                sqlDataReader = sqlCommand.ExecuteReader();
                dataTable.Load(sqlDataReader);
                DataRow[] rows = dataTable.Select();
                NumParam = Convert.ToInt32(rows[0]["albaran_id"].ToString());
            
           
         }
        static void cargarIdProducto(ref string NumParam)
        {
            CAlbaranesBD cAlbaranesBD = new CAlbaranesBD();
            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader sqlDataReader;
            DataTable dataTable = new DataTable();
            cAlbaranesBD.Abrir();
            sqlCommand.Connection = cAlbaranesBD.Connection;
            sqlCommand.CommandType = CommandType.Text;

            sqlCommand.CommandText = "SELECT producto_id FROM productos WHERE codigo =" + NumParam;
            sqlDataReader = sqlCommand.ExecuteReader();
            dataTable.Load(sqlDataReader);
            DataRow[] rows = dataTable.Select();
            NumParam = rows[0]["producto_id"].ToString();


        }

       
    }





}



