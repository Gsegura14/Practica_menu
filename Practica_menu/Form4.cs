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
    public partial class FProvincias : Form
    {
        public FProvincias()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            FProvinciasModificar fProvinciasmodificar = new FProvinciasModificar();
            fProvinciasmodificar.ShowDialog();

            // si se pulsa el boton aceptar... 
            if (fProvinciasmodificar.DialogResult == DialogResult.OK)
            {
                //recargamos tabla
                Recargar();
                //obtenemos la clave primaria de la provincia insertada
                int provincia_id = fProvinciasmodificar.Provincia_id;
                // buscamos la fila de la provincia insertada
                int rowIndex = dataGridView.Rows
                    .Cast<DataGridViewRow>()
                    .Where(r => r.Cells[0].Value.Equals(provincia_id))
                    .First()
                    .Index;
                // nos posicionamos en ella
                dataGridView.CurrentCell = dataGridView[1, rowIndex];
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            // Si tenemos registros en la tabla.
            if (dataGridView.RowCount > 0)
            {
                // Obtenemos la clave primaria del producto
                int provincia_id = Convert.ToInt32(dataGridView.CurrentRow.Cells[0].Value);
                // Instanciamos la clase FprovinciasModificar para modificar los datos.

                // Observar que los pasamos en el constructor de la clave primaria
                FProvinciasModificar fProvinciasModificar = new FProvinciasModificar(provincia_id);

                //Mostramos el cuadro de dialogo
                fProvinciasModificar.ShowDialog();

                //Si se ha pulsado el boton aceptar
                if (fProvinciasModificar.DialogResult == DialogResult.OK) ;
                {
                    // Recargamos la tabla.
                    Recargar();

                    // Buscamos la fila del producto editado
                    int rowIndex = dataGridView.Rows
                        .Cast<DataGridViewRow>()
                        .Where(r => r.Cells[0].Value.Equals(provincia_id))
                        .First()
                        .Index;

                    // Nos posicionamos en ella

                    dataGridView.CurrentCell = dataGridView[1, rowIndex];

                }
            }
        }

       

        private void btnRecargar_Click(object sender, EventArgs e)
        {
            // miramos en que fila nos encontramos
            //si no tenemos filas, nos posicionamos en la primera(0)
            //En caso contrario , ne la fila actual del DATAGRidview
            //Observar la utilidad, en este caso,del operador ternario.Mas limpio que utilizar un if.
            int rowIndex = (dataGridView.RowCount == 0) ? 0 : dataGridView.CurrentRow.Index;
            // otra dorma de mirar en que fila nos enconramos seria con un if
            // int rowIndex = 0 ;
            //if (dataGRidView.RowCount > 0)
            // rowIndex = dataGridView.CurrenteRow.Index;
            //Llamamos al procedimiento recargar y nos posicionamos en la fila actual
            Recargar(rowIndex);
        }
        private void btnBorrar_Click(object sender, EventArgs e)
        {
            // Si tenemos registros en la tabla y..
            // el usuario me condirma que realmente quiere borrar el registro

            if ((dataGridView.RowCount > 0)&&(MessageBox.Show("¿Realmente quiere borrar la provincia seleccionada?","Confirmación",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes))
            {
                // Creamos una instancia de la clase CProvinciasBD
                CProvinciasBD provinciasBD = new CProvinciasBD();

                // Obtenemos la clave principal del producto a borrar

                provinciasBD.Provincia_id = Convert.ToInt32(dataGridView.CurrentRow.Cells[0].Value);

                //Si la provincia se borra correctamente
                if (provinciasBD.Borrar())
                {
                    // Obtenemos la fila actual.
                    int rowIndex = dataGridView.CurrentCell.RowIndex;

                    //Recargamos y vamos a la fila actual, que corresponderá la siguiente provincia
                    Recargar(rowIndex);

                }
                else
                    // Si no se ha podido borrar,mensaje de error.
                    MessageBox.Show("Al borrar la provincia.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        private void Recargar(int rowIndex = 0)
        {
            // instanciamos la clase CprovinciasBD

            CProvinciasBD provinciasBD = new CProvinciasBD();

            // Recargamos el datagridview asociado al dataSoirce con los datos devueltos.

            dataGridView.DataSource = provinciasBD.Seleccionar();

            // Si tenemos datos...
            if (dataGridView.RowCount > 0)
            {
                // Comprobamos que la dila qu enos indican no e susperior a la cantiad de filas que tenmos.
                // y si es asi, nos posiconamos en la ultima fila.

                if (rowIndex >= dataGridView.RowCount)
                    rowIndex = dataGridView.RowCount - 1;
                   // si nos indican una dila negativa , nos posicionamos en la primera

            if (rowIndex < 0)
                {
                    rowIndex = 0;
                    // Nos posicionamos en la fila indicada.
                    dataGridView.CurrentCell = dataGridView[1, rowIndex];

                }
            }

        }

        private void FProvincias_Load(object sender, EventArgs e)
        {
            // cargamos la tabla de productos
            Recargar();
            //No permitimos qu enos inserten filas a traves del datagriview

            dataGridView.AllowUserToAddRows = false;

            // Las filas de la cabecera las ponemos centradas.

            dataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Si hay algun valor null , lo mostraremos con tres guiones

            dataGridView.DefaultCellStyle.NullValue = "---";
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            // Si el usuario realemnte quiere salir, cerramos el formulario
            if (MessageBox.Show("¿Cerrar?","Confirmación",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
