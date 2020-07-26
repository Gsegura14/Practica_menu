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
    public partial class FIvasBD : Form
    {
        public FIvasBD()
        {
            InitializeComponent();
        }      

        

      

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            FIvasModificar fIvasModificar = new FIvasModificar();
            fIvasModificar.ShowDialog();
            if (fIvasModificar.DialogResult == DialogResult.OK)
            {
                Recargar();
                int iva_id = fIvasModificar.Iva_id;

                int rowIndex = dataGridViewIvas.Rows
                    .Cast<DataGridViewRow>()
                    .Where(r => r.Cells[0].Value.Equals(iva_id))
                    .First()
                    .Index;
                dataGridViewIvas.CurrentCell = dataGridViewIvas[1, rowIndex];
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dataGridViewIvas.Rows.Count > 0)
            {
                int iva_id = Convert.ToInt32(dataGridViewIvas.CurrentRow.Cells[0].Value);
                FIvasModificar fIvasModificar = new FIvasModificar(iva_id);
                fIvasModificar.ShowDialog();
                if(fIvasModificar.DialogResult == DialogResult.OK)
                {
                    Recargar();
                    int rowIndex = dataGridViewIvas.Rows
                        .Cast<DataGridViewRow>()
                        .Where(r => r.Cells[0].Value.Equals(iva_id))
                        .First()
                        .Index;
                    dataGridViewIvas.CurrentCell = dataGridViewIvas[1, rowIndex];
                }
            }
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            if ((dataGridViewIvas.RowCount > 0) &&
                (MessageBox.Show("¿Realmente quiere borrar el producto seleccionado?",
                "Confirmación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes))
            {
                CivasBD ivasBD = new CivasBD();
                ivasBD.Iva_id = Convert.ToInt32(dataGridViewIvas.CurrentRow.Cells[0].Value);

                if (ivasBD.Borrar())
                {
                    int rowIndex = dataGridViewIvas.CurrentCell.RowIndex;
                    Recargar(rowIndex);
                }
                else
                    MessageBox.Show("Al Borrar el producto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btnRecargar_Click(object sender, EventArgs e)
        {
            int rowIndex = (dataGridViewIvas.RowCount == 0) ? 0 : dataGridViewIvas.CurrentRow.Index;
            Recargar(rowIndex);
        }
        private void Recargar (int rowIndex = 0)
        {
            CivasBD ivasBD = new CivasBD();
            dataGridViewIvas.DataSource = ivasBD.Seleccionar();

            if (dataGridViewIvas.RowCount > 0)
            {
                if (rowIndex >= dataGridViewIvas.RowCount)
                    rowIndex = dataGridViewIvas.RowCount - 1;
                if (rowIndex < 0)
                    rowIndex = 0;

                dataGridViewIvas.CurrentCell = dataGridViewIvas[1, rowIndex];
            }
        }
        private void Fivas_Load(object sender, EventArgs e)
        {
            Recargar();
            dataGridViewIvas.AllowUserToAddRows = false;
            dataGridViewIvas.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
           // dataGridViewIvas.Columns["iva_id"].Visible = false;
            dataGridViewIvas.DefaultCellStyle.NullValue = "---";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Realmente quiere salir de Ivas",
                "Confirmación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Close();
            }
        }
        


    }
}
