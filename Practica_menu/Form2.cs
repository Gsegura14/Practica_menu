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
    public partial class Fivas : Form
    {
        public Fivas()
        {
            InitializeComponent();
        }

        private void btnCerrarF2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            FivasModificar frmModificarIvas = new FivasModificar();
            frmModificarIvas.ShowDialog();
        }

        private void Fivas_Load(object sender, EventArgs e)
        {

        }
    }
}
