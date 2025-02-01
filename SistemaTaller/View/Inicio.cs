using SistemaTaller.View.Clientes;
using SistemaTaller.View.Contratos;
using SistemaTaller.View.Pagos;
using SistemaTaller.View.Reporte;
using SistemaTaller.View.Vehiculos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaTaller
{
    public partial class Inicio : Form
    {
        public Inicio()
        {
            InitializeComponent();
        }

        private void Inicio_Load(object sender, EventArgs e)
        {

        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            new frmClientes().ShowDialog();
        }

        private void btnContratos_Click(object sender, EventArgs e)
        {
            new frmContratos().ShowDialog();
        }

        private void btnVehiculos_Click(object sender, EventArgs e)
        {
            new frmVehiculos().ShowDialog();
        }

        private void btnPagos_Click(object sender, EventArgs e)
        {
            new frmPagos().ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            new frmReporte().ShowDialog();
        }
    }
}
