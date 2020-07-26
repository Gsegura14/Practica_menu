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
            int numero = Convert.ToInt32(txtId.Text);
            numero++;
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
            double precio = 0;
            double cantidad = 0;
            double total = 0;
            precio = Convert.ToDouble(txtPrecio.Text);
            cantidad = Convert.ToDouble(txtCantidad.Text);
            total = cantidad * precio;
            txtTotal.Text = total.ToString();

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


    }





}



