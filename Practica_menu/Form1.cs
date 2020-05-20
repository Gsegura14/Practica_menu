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

namespace Practica_menu
{
   

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }        

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }       

        private void iVAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Fivas FrmIvas = new Fivas();
            FrmIvas.Show();
        }

        private void provinciasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FProvincias gestionProvincias = new FProvincias();
            gestionProvincias.ShowDialog();
        }

       
    }
}
