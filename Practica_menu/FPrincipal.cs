using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Sql;

namespace Practica_menu
{
   

    public partial class FPrincipal : Form
    {
        public FPrincipal()
        {
            InitializeComponent();
        }        

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }       

        private void iVAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FIvasBD FrmIvas = new FIvasBD();
            FrmIvas.Show();
        }

        private void provinciasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FProvincias gestionProvincias = new FProvincias();
            gestionProvincias.ShowDialog();
        }

        private void cLientesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FClientes gestionClientes = new FClientes();
            gestionClientes.ShowDialog();
        }

        private void productosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FProductos gestionProductos = new FProductos();
            gestionProductos.ShowDialog();
        }

        private void albaranesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FAlbaranBD nuevoAlbaran = new FAlbaranBD();
            nuevoAlbaran.ShowDialog();
        }
    }
}
