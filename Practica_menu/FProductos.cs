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
    public partial class FProductos : Form
    {
        public FProductos()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            FProductosModificar fProductosModificar = new FProductosModificar();
            fProductosModificar.ShowDialog();
            if (fProductosModificar.DialogResult == DialogResult.OK)
            {
                Recargar();

                int producto_id = fProductosModificar.Producto_id;
                int rowIndex = dataGridViewP.Rows
                    .Cast<DataGridViewRow>()
                    .Where(r => r.Cells[0].Value.Equals(producto_id))
                    .First()
                    .Index;
                dataGridViewP.CurrentCell = dataGridViewP[1, rowIndex];
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dataGridViewP.RowCount > 0)
            {
                int producto_id = Convert.ToInt32(dataGridViewP.CurrentRow.Cells[0].Value);
                FProductosModificar fProductosModificar = new FProductosModificar(producto_id);
                fProductosModificar.ShowDialog();
                if (fProductosModificar.DialogResult == DialogResult.OK)
                {
                    Recargar();
                    int rowIndex = dataGridViewP.Rows
                        .Cast<DataGridViewRow>()
                        .Where(r => r.Cells[0].Value.Equals(producto_id))
                        .First()
                        .Index;
                    dataGridViewP.CurrentCell = dataGridViewP[1, rowIndex];
                }
            }
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            if ((dataGridViewP.RowCount > 0)&&
                (MessageBox.Show("¿Realmente quiere borrar el producto seleccionado?",
                "Confirmación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes))
            {
                CProductosBD productosBD = new CProductosBD();
                productosBD.Producto_id = Convert.ToInt32(dataGridViewP.CurrentRow.Cells[0].Value);

                if (productosBD.Borrar())
                {
                    int rowIndex = dataGridViewP.CurrentCell.RowIndex;

                    Recargar(rowIndex);
                }
                else
                {
                    MessageBox.Show("Al borrar el producto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }

        }

        private void btnRecargar_Click(object sender, EventArgs e)
        {
            int rowIndex = (dataGridViewP.RowCount == 0) ? 0 : dataGridViewP.CurrentRow.Index;
            Recargar(rowIndex);

        }
        private void Recargar(int rowIndex = 0)
        {
            CProductosBD productosBD = new CProductosBD();
            dataGridViewP.DataSource = productosBD.Seleccionar();

            if (dataGridViewP.RowCount > 0)
            {
                if (rowIndex >= dataGridViewP.RowCount)
                    rowIndex = dataGridViewP.RowCount - 1;
                if (rowIndex < 0)
                {
                    rowIndex = 0;
                    dataGridViewP.CurrentCell = dataGridViewP[1, rowIndex];
                }
            }

        }

        private void FProductos_Load(object sender, EventArgs e)
        {
            Recargar();
            dataGridViewP.AllowUserToAddRows = false;
            dataGridViewP.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewP.Columns["ID"].Visible = false;
            dataGridViewP.Columns["precio"].DefaultCellStyle.Format = "c";
            dataGridViewP.Columns["precio"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewP.DefaultCellStyle.NullValue = "---";
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Realmente quiere salir de gestión de productos?",
                "Confirmación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question)== DialogResult.Yes)
            {
                Close();
            }
        }

        private void btnPrimero_Click(object sender, EventArgs e)
        {
            dataGridViewP.CurrentCell = dataGridViewP[1, 0];
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            int rowIndex = dataGridViewP.CurrentRow.Index - 1;
            if (rowIndex < 0)
                rowIndex = 0;
            dataGridViewP.CurrentCell = dataGridViewP[1, rowIndex];
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            int rowIndex = dataGridViewP.CurrentRow.Index + 1;
            if (rowIndex >= dataGridViewP.RowCount)
                rowIndex = dataGridViewP.RowCount - 1;
            dataGridViewP.CurrentCell = dataGridViewP[1, rowIndex];
        }

        private void btnUltimo_Click(object sender, EventArgs e)
        {
            int rowIndex = dataGridViewP.RowCount - 1;
            if (rowIndex < 0)
                dataGridViewP.CurrentCell = dataGridViewP[1, rowIndex];
        }

     
    }
}
