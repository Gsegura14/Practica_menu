using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Practica_menu
{
    public partial class FProvinciasModificar : Form
    {
        public int Provincia_id { get; set; }
        public FProvinciasModificar(int provincia_id = 0)
        {
            InitializeComponent();
            // clave primaria de la provincia indicada
            Provincia_id = provincia_id;
        }

        private void FProvinciasModificar_Load(object sender, EventArgs e)
        {
            // instranciamos las classe de conexion cprvinciasbd
            CProvinciasBD provinciasBD = new CProvinciasBD();
            // Obtenemos todos los registros de la tabla

            // Mostramos el codigo de la provincia
            txtCodigo.Text = Convert.ToString(provinciasBD.Codigo);
            // Nombre de la provincia
            txtNProvincia.Text = provinciasBD.Provincia;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            // Verificamos que todo es correcto antes de proseguir
            if (!Correcto())
                return;
            // Por defecto , indicamos que se le pulsa el botón OK;
            DialogResult = DialogResult.OK;
            // Instanciamos la clase CprovinciasBD();
            CProvinciasBD provinciasBd = new CProvinciasBD();
            // Le pasamos a cada una de las propiedades los valores correspondientes
            provinciasBd.Codigo = Convert.ToInt32(txtCodigo.Text);
            provinciasBd.Provincia = txtNProvincia.Text;
            // Si estamos insertando...
            if ( Provincia_id == 0)
            {
                // Insertamo y verificamos que todo ha ido bien.
                if (provinciasBd.Insertar())
                {
                    Provincia_id = provinciasBd.Provincia_id;

                }
                else
                {
                    MessageBox.Show("Al insertar el producto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Si no se ha podido insertar , devolvemos cancel
                    DialogResult = DialogResult.Cancel;

                }
            }
            else
            {
                // y si no,estamos modificando
                // Indicamosla provincia a modificar
                provinciasBd.Provincia_id = Provincia_id;
                // Verificamos que si ha habido un error
                if (!provinciasBd.Editar())
                {
                    MessageBox.Show("Al modificar el producto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Si no se ha podido modificar , devolvemos cancel
                    DialogResult = DialogResult.Cancel;
                }
            }
        }
        private bool Correcto()
        {
            if (txtNProvincia.Text == "")
            {
                MessageBox.Show("Debe indicar el nombre de la provincia", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNProvincia.Focus();
                return false;
            }
            return true;
        }
    }
}
