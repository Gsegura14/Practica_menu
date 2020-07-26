using System;
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
            //Instanciamos las clases CCprovinciasBD
            CProvinciasBD cProvinciasBD = new CProvinciasBD();
            //obtenemos todos los valore de la tabla
            cbProvincias.DataSource = cProvinciasBD.Seleccionar();
            //Mostramos el valor del campo de la categoria
            cbProvincias.DisplayMember = "provincia";
            //Indicamos que el valor seleccionado es clave primaria
            cbProvincias.ValueMember = "provincia_id";

            

          
            if (Cliente_id != 0)
            {
                //Instanciamos la clase CClientesBD;
                 CClientesBD clientesBD = new CClientesBD();
                //Buscmaos el cliente
                clientesBD.Seleccionar(Cliente_id);
                //Mostramos la clave primaria
                txtId.Text = Convert.ToString(clientesBD.Cliente_id);
                //codigo
                txtCodigo.Text = Convert.ToString(clientesBD.Codigo);
                //El nombre del cliente
                txtCliente.Text = clientesBD.Cliente;
                //CIF
                txtCif.Text =clientesBD.Cif;
                txtDireccion.Text = clientesBD.Direccion;
                txtCp.Text = Convert.ToString(clientesBD.Cp);
                txtPoblacion.Text = clientesBD.Poblacion;
                // Buscamos en el comboBox el indice de la provincia
                //cbProvincias.SelectedIndex = cbProvincias.FindStringExact(clientesBD.Provincia);
                cbProvincias.SelectedValue = clientesBD.Provincia_id;
                txtTelefono.Text = clientesBD.Telefono;
                txtEmail.Text =clientesBD.Email;

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
            clientesBD.Codigo = Convert.ToInt32(txtCodigo.Text);
            clientesBD.Cliente = txtCliente.Text;
            clientesBD.Cif = txtCif.Text;
            clientesBD.Direccion = txtDireccion.Text;
            clientesBD.Cp = (txtCp.Text);
            clientesBD.Poblacion = txtPoblacion.Text;
            clientesBD.Provincia_id =(int)cbProvincias.SelectedValue;
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
