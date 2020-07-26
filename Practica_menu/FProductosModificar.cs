using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Practica_menu
{
    public partial class FProductosModificar : Form
    {
        public int Producto_id { get; set; }
        public FProductosModificar(int producto_id = 0)
        {
            InitializeComponent();
            Producto_id = producto_id;
        }

        private void FProductosModificar_Load(object sender, EventArgs e)
        {
            CivasBD ivasBD = new CivasBD();
            cbIvas.DataSource = ivasBD.Seleccionar();
            cbIvas.DisplayMember = "iva";
            cbIvas.ValueMember = "iva_id";
           
            if (Producto_id != 0)
            {
                CProductosBD productosBD = new CProductosBD();
                
                productosBD.Seleccionar(Producto_id);
                txtId.Text = Convert.ToString(productosBD.Producto_id);
                txtCodigo.Text = productosBD.Codigo;
                txtProducto.Text = productosBD.Producto;
                txtPrecio.Text = Convert.ToString(productosBD.Precio);
                cbIvas.SelectedItem = productosBD.Iva;
                txtImporte.Text = Convert.ToString(productosBD.Importe);
                Text = "Productos :: Modificación";
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (!Correcto())
                return;
            DialogResult = DialogResult.OK;
            CProductosBD productosBD = new CProductosBD();
            productosBD.Codigo = txtCodigo.Text;
            productosBD.Producto = txtProducto.Text;
            productosBD.Precio = Convert.ToDouble(txtPrecio.Text);
            productosBD.Iva_id = (int)cbIvas.SelectedValue;
            productosBD.Importe = Convert.ToDouble(txtImporte.Text);
            if (Producto_id == 0)
            {
                if (productosBD.Insertar())
                {
                    Producto_id = productosBD.Producto_id;
                }
                else
                {
                    MessageBox.Show("Al insertar el producto.", "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    DialogResult = DialogResult.Cancel;
                }
            }
            else
            {
                productosBD.Producto_id = Producto_id;
                if (!productosBD.Editar())
                {
                    MessageBox.Show("Al modificar el producto", "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    DialogResult = DialogResult.Cancel;
                }
            }

            

        }
        
        private bool Correcto()
        {
            if (txtProducto.Text == "")
            {
                MessageBox.Show("Debe indicar el nombre del producto", "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                txtProducto.Focus();
                return false;
            }
            return true;
        }
    }

}
