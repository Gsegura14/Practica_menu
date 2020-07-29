using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text.xml;
using System.Reflection;

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
           
            int numero = 0;
            CargarNumeroAlbaran(ref numero);
            txtId.Text = Convert.ToString(numero);
            DateTime fecha = DateTime.Today;
            txtFecha.Text = fecha.ToString("d");
        }

        private void buscaProducto()
        {
            MaestroArticulos maestroProductos = new MaestroArticulos();
            maestroProductos.ShowDialog();
            //Autocompletar al seleccionar un articulo del maestro de articulos
            txtCodigo.Text = Selecciona_Datos.codigo;
            txtProducto.Text = Selecciona_Datos.producto;
            txtPrecio.Text = Selecciona_Datos.precio;
            txtPrecio.Text.Replace(",", ".");
        }


        private void btnBuscaProducto_Click(object sender, EventArgs e)
        {

            buscaProducto();


        }



        private void txtCantidad_Leave(object sender, EventArgs e)
        {
            try {
                double precio = 0;
                double cantidad = 0;
                double total = 0;
                precio = Convert.ToDouble(txtPrecio.Text);
                cantidad = Convert.ToDouble(txtCantidad.Text);
                total = cantidad * precio;
                txtTotal.Text = total.ToString();

            }
            catch
            {
                MessageBox.Show("La cantidad no puede ser 0", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
       private void buscaClientes()
        {
            
            MaestroClientes maestroClientes = new MaestroClientes();
            maestroClientes.ShowDialog();
            lblNombreCliente.Text = SDClientes.nombreCliente;
            lblDireccionCliente.Text = SDClientes.direccionCliente;
            lblTelefonoCliente.Text = SDClientes.telefonoCliente;
            txtIdCliente.Text = SDClientes.codigoCliente;
        }



        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            buscaClientes();

        }




        private void btnAceptarCliente_Click(object sender, EventArgs e)
        {
            try
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
            catch
            {
                MessageBox.Show("El cliente no puede ser vacio. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ConexionAlb.Close();
            }
        }

        static void CargarIdCliente(ref int IdParam)
        {
            // Función para cargar el id de cliente ya que funciona con el código pero la tabla con el ID

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
        static void CargarNumeroAlbaran(ref int NumParam)
        {
            // Función que sirve como contador para atribuir un número al albrán(Este número es distinto al Id de albarán en la base de datos.
            CAlbaranesBD cAlbaranesBD = new CAlbaranesBD();
            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader sqlDataReader;
            DataTable dataTable = new DataTable();
            cAlbaranesBD.Abrir();
            sqlCommand.Connection = cAlbaranesBD.Connection;
            sqlCommand.CommandType = CommandType.Text;
            // Seleccionamos el número más alto en la columna numero de la tabla albarnes y sumamos 1.
            sqlCommand.CommandText = "SELECT MAX(numero) as numero FROM albaranes";
            sqlDataReader = sqlCommand.ExecuteReader();
            dataTable.Load(sqlDataReader);
            DataRow[] rows = dataTable.Select();
            NumParam = (Convert.ToInt32(rows[0]["numero"].ToString())) + 1;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            
            int n = dataGridViewlineas.Rows.Add();
           
            int NumAlbaran = Convert.ToInt32(txtId.Text);
            cargarIdAlbaran(ref NumAlbaran);
            string IdProducto =(txtCodigo.Text);
            cargarIdProducto(ref IdProducto);
            double iva, re;
            iva = 0;
            re = 0;
            int idpr = Convert.ToInt32(IdProducto);
            // Cargamos los valores del Iva del producto
            cargarIvas(ref iva, ref re , idpr);

            double subtotal = (Convert.ToDouble(txtPrecio.Text) * Convert.ToDouble(txtCantidad.Text));
            double impuesto = iva + re;
            double total_importe = subtotal + ((subtotal * impuesto)/100);
            double bruto = Convert.ToDouble(txtSubtotal.Text); 
            // Añadimos el valor de la linea a la pantalla del subtotal
            sumaPantalla(ref total_importe , bruto);            
            bruto = total_importe + bruto;
            txtSubtotal.Text = Convert.ToString(bruto);

            // Introducimos los valores al GRID
            dataGridViewlineas.Rows[n].Cells[0].Value = NumAlbaran;
            dataGridViewlineas.Rows[n].Cells[1].Value = txtCantidad.Text;
            dataGridViewlineas.Rows[n].Cells[2].Value = IdProducto;
            dataGridViewlineas.Rows[n].Cells[3].Value = txtPrecio.Text.Replace(".",",");
            dataGridViewlineas.Rows[n].Cells[4].Value = iva;
            dataGridViewlineas.Rows[n].Cells[5].Value = re;
            dataGridViewlineas.Rows[n].Cells[6].Value = Convert.ToString(total_importe).Replace(".",".");
            // Reseteamos los campos de introducción de productos
            txtCodigo.Text = "";
            txtProducto.Text = "";
            txtCantidad.Text = "";
            txtTotal.Text = "";
            txtPrecio.Text = "";

           

        
            
        }

       

        private void btnTerminar_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand guardar = new SqlCommand("INSERT INTO albaraneslineas (albaran_id,cantidad,producto_id,precio,iva,re,importe) VALUES (@albaranid,@cantidad,@productoid,@precio,@iva,@re,@importe)", ConexionAlb);
                ConexionAlb.Open();

                // En primer lugar guarda todos las lineas del Grid en la tabla albaraneslineas
                foreach (DataGridViewRow row in dataGridViewlineas.Rows)
                {
                    guardar.Parameters.Clear();
                    guardar.Parameters.AddWithValue("@albaranid", Convert.ToInt32(row.Cells["Albaran_id"].Value));
                    guardar.Parameters.AddWithValue("@cantidad", Convert.ToInt32(row.Cells["Cantidad"].Value));
                    guardar.Parameters.AddWithValue("@productoid", Convert.ToInt32(row.Cells["Producto_id"].Value));
                    guardar.Parameters.AddWithValue("@precio", Convert.ToDouble(row.Cells["Precio"].Value));
                    guardar.Parameters.AddWithValue("@iva", Convert.ToDouble(row.Cells["Iva"].Value));
                    guardar.Parameters.AddWithValue("@re", Convert.ToDouble(row.Cells["Re"].Value));
                    guardar.Parameters.AddWithValue("@importe", Convert.ToDouble(row.Cells["Importe"].Value));
                    guardar.ExecuteNonQuery();
                }

                // Posteriormente Imprime el albarán
                To_pdf();
            }
            finally
            {
                // Y finalmente reestablece el TPV para poder realizar un nuevo albarán.
                reloadAlbaran();             
            }
            
            



        }

        // Función para cargar el Id del Albarán
        static void cargarIdAlbaran(ref int NumParam)
        {
            
                CAlbaranesBD cAlbaranesBD = new CAlbaranesBD();
                SqlCommand sqlCommand = new SqlCommand();
                SqlDataReader sqlDataReader;
                DataTable dataTable = new DataTable();
                cAlbaranesBD.Abrir();
                sqlCommand.Connection = cAlbaranesBD.Connection;
                sqlCommand.CommandType = CommandType.Text;

                sqlCommand.CommandText = "SELECT albaran_id FROM albaranes WHERE numero =" + NumParam;
                sqlDataReader = sqlCommand.ExecuteReader();
                dataTable.Load(sqlDataReader);
                DataRow[] rows = dataTable.Select();
                NumParam = Convert.ToInt32(rows[0]["albaran_id"].ToString());
            
           
         }

        // Función para cargar el Id del producto

        static void cargarIdProducto(ref string NumParam)
        {
            CAlbaranesBD cAlbaranesBD = new CAlbaranesBD();
            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader sqlDataReader;
            DataTable dataTable = new DataTable();
            cAlbaranesBD.Abrir();
            sqlCommand.Connection = cAlbaranesBD.Connection;
            sqlCommand.CommandType = CommandType.Text;

            sqlCommand.CommandText = "SELECT producto_id FROM productos WHERE codigo =" + NumParam;
            sqlDataReader = sqlCommand.ExecuteReader();
            dataTable.Load(sqlDataReader);
            DataRow[] rows = dataTable.Select();
            NumParam = rows[0]["producto_id"].ToString();


        }
        // Función para cargar las dos cantidades de Iva+Recargo de la tabla Ivas

        static void cargarIvas(ref double ParamIva, ref double ParamRe , int idPr)
        {
            CProductosBD cProductos = new CProductosBD();
            SqlCommand sqlCommand = new SqlCommand();
            SqlDataReader sqlDataReader;
            DataTable dataTable = new DataTable();
            cProductos.Abrir();
            sqlCommand.Connection = cProductos.Connection;
            sqlCommand.CommandType = CommandType.Text;

            sqlCommand.CommandText = "SELECT iva,re FROM ivas WHERE iva_id = (SELECT iva_id FROM productos WHERE producto_id = " + idPr + ")";
            sqlDataReader = sqlCommand.ExecuteReader();
            dataTable.Load(sqlDataReader);
            DataRow[] rows = dataTable.Select();
            ParamIva =Convert.ToDouble(rows[0]["iva"].ToString());
            ParamRe = Convert.ToDouble(rows[0]["re"].ToString());
            
        }


        // Función para la pantalla de subtotal
        static void sumaPantalla (ref double subtotal, double br)
        {
            subtotal = br + subtotal;
        }


        // Crear PDF
        private void To_pdf()
        {
            string filename = "";
            Document doc = new Document(PageSize.A4, 10, 10, 10, 10);
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = @"C:\Users\USER-PC\Documents";
            saveFileDialog1.Title = "Guardar Albarán";
            saveFileDialog1.DefaultExt = "pdf";
            saveFileDialog1.Filter = "pdf Files (*.pdf)|*.pdf| All Files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;
            string nombre_archivo =txtId.Text + "_" + txtIdCliente.Text + "_" + lblNombreCliente.Text;
            saveFileDialog1.FileName = nombre_archivo;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filename = saveFileDialog1.FileName;
            }

            if (filename.Trim() != "")
            {
                FileStream file = new FileStream(filename,
                FileMode.OpenOrCreate,
                FileAccess.ReadWrite,
                FileShare.ReadWrite);
                PdfWriter.GetInstance(doc, file);
                doc.Open();
                string num_albaran = "Nº Albarán       :" + txtId.Text;
                string codigo_cliente = "Cod.Cliente   :" + txtIdCliente.Text;
                string nombre_cliente = "Cliente       :" + lblNombreCliente.Text;
                string domicilio_cliente = "Domicilio  :" + lblDireccionCliente.Text;
                string telefono_cliente = "Teléfono    :" + lblTelefonoCliente.Text;
                string total_importe = "TOTAL : " + txtSubtotal.Text;
                Chunk chunk = new Chunk("ALBARÁN", FontFactory.GetFont("ARIAL", 20, iTextSharp.text.Font.BOLD | iTextSharp.text.Font.UNDERLINE));
                doc.Add(new Paragraph(chunk));
                doc.Add(new Paragraph("                       "));
                doc.Add(new Paragraph("                       "));
                doc.Add(new Paragraph("------------------------------------------------------------------------------------------"));
                doc.Add(new Paragraph(num_albaran));
                doc.Add(new Paragraph(codigo_cliente));
                doc.Add(new Paragraph(nombre_cliente));
                doc.Add(new Paragraph(domicilio_cliente));
                doc.Add(new Paragraph(telefono_cliente));
                doc.Add(new Paragraph("------------------------------------------------------------------------------------------"));
                doc.Add(new Paragraph("                       "));
                doc.Add(new Paragraph("                       "));
                doc.Add(new Paragraph("                       "));
                GenerarDocumento(doc);
                doc.AddCreationDate();
                doc.Add(new Paragraph("______________________________________________", FontFactory.GetFont("ARIAL", 20, iTextSharp.text.Font.BOLD)));                
                doc.Add(new Paragraph(total_importe, FontFactory.GetFont("ARIAL", 20, iTextSharp.text.Font.BOLD | Element.ALIGN_RIGHT )));                
                doc.Add(new Paragraph("Firma", FontFactory.GetFont("ARIAL", 20, iTextSharp.text.Font.BOLD)));
                doc.Close();
               // Process.Start(filename);//Esta parte se puede omitir, si solo se desea guardar el archivo, y que este no se ejecute al instante
            }

        }
        // CRear PDF
        public void GenerarDocumento(Document document)
        {
            int i, j;
            PdfPTable datatable = new PdfPTable(dataGridViewlineas.ColumnCount);
            datatable.DefaultCell.Padding = 3;
            float[] headerwidths = GetTamañoColumnas(dataGridViewlineas);
            datatable.SetWidths(headerwidths);
            datatable.WidthPercentage = 100;
            datatable.DefaultCell.BorderWidth = 2;
            datatable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            for (i = 0; i < dataGridViewlineas.ColumnCount; i++)
            {
                datatable.AddCell(dataGridViewlineas.Columns[i].HeaderText);
            }
            datatable.HeaderRows = 1;
            datatable.DefaultCell.BorderWidth = 1;
            for (i = 0; i < dataGridViewlineas.Rows.Count; i++)
            {
                for (j = 0; j < dataGridViewlineas.Columns.Count; j++)
                {
                    if (dataGridViewlineas[j, i].Value != null)
                    {
                        datatable.AddCell(new Phrase(dataGridViewlineas[j, i].Value.ToString()));//En esta parte, se esta agregando un renglon por cada registro en el datagrid
                    }
                }
                datatable.CompleteRow();
            }
            document.Add(datatable);
        }
        public float[] GetTamañoColumnas(DataGridView dg)
        {
            float[] values = new float[dg.ColumnCount];
            for (int i = 0; i < dg.ColumnCount; i++)
            {
                values[i] = (float)dg.Columns[i].Width;
            }
            return values;

        }
        
        
        
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            cancelaAlbaran();
        }
        // Función para reestablecer la pantalla del TPV
        private void reloadAlbaran()
        {
            this.Controls.Clear();
            this.InitializeComponent();
            ConexionAlb.Close();
            DateTime fecha = DateTime.Today;
            txtFecha.Text = fecha.ToString("d");
        }

        //Función para que el usuario pueda cancelar un Albarán
        private void cancelaAlbaran()
        {
            string mensaje = "¿Estas seguro que quieres cancelar el albarán?";
            string titulo = "Confirmación";
            var respuesta = MessageBox.Show(mensaje, titulo, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
           
            if (respuesta == DialogResult.Yes)
            {
                reloadAlbaran();
            }
        }


        // Atajos de teclado
        
        private void FAlbaranBD_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                buscaClientes();
            }
            if ( e.KeyCode == Keys.F3) 
            {   
                if (txtCodigo.Enabled == true)
                buscaProducto();
                else
                {
                    MessageBox.Show("Debes ingresar un clientes y después p ulsar el botón ACEPTAR", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            
            if (e.KeyCode == Keys.F5)
            {
                To_pdf();
            }
            if (e.KeyCode == Keys.Escape)
            {
                cancelaAlbaran();
            }
        }
    }
    }











