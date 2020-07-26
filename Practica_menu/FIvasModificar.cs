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
    public partial class FIvasModificar : Form
    {
        public int Iva_id { get; set; }

        public FIvasModificar(int iva_id = 0)
        {
            InitializeComponent();
            Iva_id = iva_id;
        }

        private void FIvasModificar_Load(object sender, EventArgs e)
        {
            if (Iva_id != 0)
            {
                CivasBD ivasBD = new CivasBD();
                ivasBD.Seleccionar(Iva_id);
                txtIvaid.Text = Convert.ToString(ivasBD.Iva_id);
                txtIva.Text = Convert.ToString(ivasBD.Iva);
                txtRe.Text = Convert.ToString(ivasBD.Re);
                Text = "Ivas :: Modificar";
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (!Correcto())
                return;
            DialogResult = DialogResult.OK;
            CivasBD ivasBD = new CivasBD();
            ivasBD.Iva = Convert.ToDouble(txtIva.Text);
            ivasBD.Re = Convert.ToDouble(txtRe.Text);
            if (Iva_id == 0)
            {
                if (ivasBD.Insertar())
                {
                    Iva_id = ivasBD.Iva_id;
                }
                else
                {
                    MessageBox.Show("Al insertar el nuevo iva", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DialogResult = DialogResult.Cancel;
                }
            }
            else
            {
                ivasBD.Iva_id = Iva_id;
                if (!ivasBD.Editar())
                {
                    MessageBox.Show("Al modificar el iva.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DialogResult = DialogResult.Cancel;
                }
            }
        }
        private bool Correcto()
        {
            if (txtIva.Text == "")
            {
                MessageBox.Show("Debe indicar la cantidad de IVA", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        

    }


}
