using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Practica_menu
{
   
    public partial class FClientesModificar : Form
    {
        public int Cliente_id { get; set; }
        public FClientesModificar(int cliente_id= 0)
        {
            InitializeComponent();
            // Clave primaria del cliente indicado.
            Cliente_id = cliente_id;
        }

        private void FClientesModificar_Load(object sender, EventArgs e)
        {
            CClientesBD clientesBD = new CClientesBD();
            // Obtenemos todos los registros de la tabla
           // cbClientes.DataSource = clientesBD.Seleccionar();
            //Mostramos el valor del campo cliente
         //   cbClientes.DisplayMember = "cliente";
            //Indicamos que el valor selecciondo es la clave primaria
       //     cbClientes.ValueMember = "cliente_id";

            //Si me indican un cliente en concreto , es qu equeremos modificarlo
            if (Cliente_id != 0)
            {
                //Instanciamos la clase CClientesBD;
              //  CClientesBD clientesBD = new CClientesBD();
                //Buscmaos el cliente
                clientesBD.Seleccionar(Cliente_id);
                //Mostramos la clave primaria
                txtId.Text = Convert.ToString(clientesBD.Cliente_id);
                //codigo
                txtCodigo.Text = Convert.ToString(clientesBD.Codigo_id);
                //El nombre del cliente
                txtCliente.Text = Convert.ToString(clientesBD.Cliente);
                //CIF
                txtCif.Text = Convert.ToString(clientesBD.Cif);
                txtDireccion.Text = Convert.ToString(clientesBD.Direccion);
                txtCp.Text = Convert.ToString(clientesBD.Cp);
                txtPoblacion.Text = Convert.ToString(clientesBD.Poblacion);
                txtProvincia.Text = Convert.ToString(clientesBD.Provincia_id);
                txtTelefono.Text = Convert.ToString(clientesBD.Telefono);
                txtEmail.Text = Convert.ToString(clientesBD.Email);

                //Cambio a form modificacion
                Text = "Clientes : modificación";

            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            // Verificamos que todo es correcto antes de proseguir
            if (!Correcto())
                return;
            //Por ejemplo,indicamos que se pulse el boton OK
            DialogResult = DialogResult.OK;
            //Instanciamos la clase CCLientesBD
            CClientesBD clientesBD = new CClientesBD();
            // Le pasamos ac ada una de las propiedades los valores correspondientes.
            clientesBD.Codigo_id = (int)clientesBD.Codigo_id;
            clientesBD.Cliente = txtCliente.Text;
            clientesBD.Cif = txtCif.Text;
            clientesBD.Direccion = txtDireccion.Text;
            clientesBD.Cp = txtCp.Text;
            clientesBD.Poblacion = txtPoblacion.Text;
            clientesBD.Provincia_id = Convert.ToInt32(txtProvincia.Text);
            clientesBD.Telefono = txtTelefono.Text;
            clientesBD.Email = txtEmail.Text;
            //Si estamos insertando
            if( Cliente_id == 0)
            {
                //insertamos y verificamos que todo ha ido bien

                if (clientesBD.Insertar())
                {
                    Cliente_id = clientesBD.Cliente_id;
                }
                else
                {
                    MessageBox.Show("Al insertar el cliente,", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //Si no se ha podido insertar,devolvemos cancel
                    DialogResult = DialogResult.Cancel;
                }
            }
            else
            {
                // Y si no , estamos modificando
                //Indicamos el cliente a modificar
                clientesBD.Cliente_id = Cliente_id;
                // Verificamos que si ha havido un error
                if (!clientesBD.Editar())
                {
                    MessageBox.Show("Al modificar el cliente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //Si no se ha podido modificar , devolvemos cancel
                    DialogResult = DialogResult.Cancel;
                }
            }
        }
        private bool Correcto()
        {
            if (txtCliente.Text == "")
            {
                MessageBox.Show("Debe indicar el nombre del cliente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtCliente.Focus();
                return false;
            }
            return true;
        }
    }

}
