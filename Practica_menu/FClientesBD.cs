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
    public partial class FClientes : Form
    {
        public FClientes()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            //Instanciamos la clase FClientesModificicar para introducir los datos
            FClientesModificar fClientesModificar = new FClientesModificar();

            // Mostramos el cuadro de dialogo
            fClientesModificar.ShowDialog();

            // Si se ha pulsado el boton aceptar
            if (fClientesModificar.DialogResult == DialogResult.OK)
            {
                //Recargamos la tabla
                Recargar();

                //Obtenemos la clave primaria del cliente insertado.
                int cliente_id = fClientesModificar.Cliente_id;

                //Buscamos la fila del cliente insertado

                int rowIndex = dataGridView1.Rows
                    .Cast<DataGridViewRow>()
                    .Where(r => r.Cells[0].Value.Equals(cliente_id))
                    .First()
                    .Index;
                // Nos posicionamos en ella
                dataGridView1.CurrentCell = dataGridView1[1, rowIndex];

            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            // Si tenemos registros en la tabla
            if (dataGridView1.RowCount > 0)
            {
                //Obtenemos la clave primaria del cliente
                int cliente_id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                //Instanciamos la clase FClientesModificar para modificar los datos.
                //Observar que le pasamos en el constructor la clave primaria
                FClientesModificar fClientesModificar = new FClientesModificar(cliente_id);
                //Mostramos el cuadro de dialosgo

                fClientesModificar.ShowDialog();
                //Si se ha pulsado en el boton aceptar
                if (fClientesModificar.DialogResult == DialogResult.OK)
                {
                    //Recargamos la tabla
                    Recargar();
                    // Buscamos la fila del cliente editado

                    int rowIndex = dataGridView1.Rows
                        .Cast<DataGridViewRow>()
                        .Where(r => r.Cells[0].Value.Equals(cliente_id))
                        .First()
                        .Index;
                    //Nos posicionamos en ella
                    dataGridView1.CurrentCell = dataGridView1[1, rowIndex];

                }
            }
        }
        private void btnRecargar_Click(object sender, EventArgs e)
        {
            //Miramos en que fila nos encontramos
            //Si no tenemos filas,nos posicionamos en la primera(0)
            //En caso contrario,en la fila actual del DataGRidView
            //Observar la utilidad, en este caso, del operador ternario.Más limpio que utilizar un if.

            int rowIndex = (dataGridView1.RowCount == 0) ? 0 : dataGridView1.CurrentRow.Index;

            Recargar(rowIndex);
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            //Si tenemos regitros en la tabla y...
            //EL usuario me condirma que erealmente quiere vorrar el registro...
            if ((dataGridView1.RowCount > 0) && (MessageBox.Show("¿Realmente quiere borrar el producto seleccionado?", "Confirmación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes))
            {
                //Creamos una instancia de la clase CCLientesBD
                CClientesBD clientesBD = new CClientesBD();
                //Obtenemos la clave princiapl del cliente a borrar
                clientesBD.Cliente_id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                //Si el cliente se borra correctamente

                if (clientesBD.Borrar())
                {
                    //Obtenemos la fila actual
                    int rowIndex = dataGridView1.CurrentCell.RowIndex;
                    //Recargamos y vamos al afila actual, que correspondera al siguiente cliente.
                    Recargar(rowIndex);

                }
                else//Si no se ha podido borrar,mensaje de error.
                    MessageBox.Show("Al borrar el cliente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }


                
        }

       
        private void Recargar (int rowIndex = 0)
        {
            //Instanciamos la clase CClientesBD
            CClientesBD clientesBD = new CClientesBD();
            //Recargamos el datagridview asociando el datasource con los datos devuletos.
            dataGridView1.DataSource = clientesBD.Seleccionar();
            //Si tenemos datos...
            if (dataGridView1.RowCount > 0)
            {
                //Comprobamos que la fila que nos indica no es superior a la cantidad d e filas
                // Si es asi, nos posicionamos en la ultima fila...
                if (rowIndex >= dataGridView1.RowCount)
                    rowIndex = dataGridView1.RowCount - 1;

                //Si nos indican una fila negativa,nos posiocnamos en la primera.
                if (rowIndex < 0)
                    rowIndex = 0;
                // Ocultamos las columnas que nos interese como la clave primaria y la id de la provincia
                dataGridView1.Columns["provincia_id"].Visible = false;
                dataGridView1.Columns["id"].Visible = false;
                //Nos posiconamos en la filaindicada.
                dataGridView1.CurrentCell = dataGridView1[1, rowIndex];
            }
        }

        private void FClientes_Load(object sender, EventArgs e)
        {
            // CArgamos la tabla de productos.
            Recargar();
            // No permitimos que nos inserten dilas a través del DataGRidview
            dataGridView1.AllowUserToAddRows = false;
            
            //Las tablas de la cabecera las ponemos centradas.
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            // Si hay algun valor null, lo mostraremos con tres guiones...
            dataGridView1.DefaultCellStyle.NullValue = "---";
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            // Si el usuario realment quiere salir,ceramos la app

            if (MessageBox.Show("¿Salir de Clientes?","Confirmación",MessageBoxButtons.YesNo,MessageBoxIcon.Question)== DialogResult.Yes)
            {
                Close();
            }
        }

        private void btnPrimero_Click(object sender, EventArgs e)
        {
            // Nos posicionamos en la primera fila del datagridview
            dataGridView1.CurrentCell = dataGridView1[1, 0];

        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            // Buscamos la fila anterior
          
            int rowIndex = dataGridView1.CurrentRow.Index - 1;
            // Si es negativa es poruqe ya estabamos en la primera fila
            if (rowIndex < 0) 
                rowIndex = 0;

            // Nos posicionamos en la fila del dataGridView
            dataGridView1.CurrentCell = dataGridView1[1, rowIndex];

        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            // Buscamos la fila siguiente.
            int rowIndex = dataGridView1.CurrentRow.Index + 1;
            // Si es mayor que la cantidad de filas que hay en el DataGridView, entonces nos vamos a la última fila.
            if (rowIndex >= dataGridView1.RowCount) 
                rowIndex = dataGridView1.RowCount - 1;
            // Nos posicionamos en la fila del DataGridView.
            dataGridView1.CurrentCell = dataGridView1[1, rowIndex];
        }

        private void btnUltimo_Click(object sender, EventArgs e)
        {
            // Buscamos la ultima fila
            int rowIndex = dataGridView1.RowCount - 1;
            // Si no había filas en el DataGridView, entonces la fila será la primera.
            if (rowIndex < 0)
                rowIndex = 0;
            // Nos posicionamos en la fila del DAtaGridView
            dataGridView1.CurrentCell = dataGridView1[1, rowIndex];
        }
    }
}
