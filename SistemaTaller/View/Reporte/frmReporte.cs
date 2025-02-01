using Microsoft.Reporting.WinForms;
using SistemaTaller.Controller;
using SistemaTaller.Model.DataSets;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaTaller.View.Reporte
{
    public partial class frmReporte : Form
    {
        public frmReporte()
        {
            InitializeComponent();
        }
        private void CargarClientes()
        {
            clsClientes ctrl = new clsClientes();
            var lista = ctrl.Listar();
            cmbClientes.DataSource = lista;
            cmbClientes.DisplayMember = "Apellido";  
            cmbClientes.ValueMember = "IdCliente";
            cmbClientes.SelectedIndex = -1;
        }
        private void frmReporte_Load(object sender, EventArgs e)
        {
            CargarClientes();
            this.reportViewer1.RefreshReport();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int? idCliente = null;
            if (cmbClientes.SelectedIndex >= 0)
            {
                idCliente = (int)cmbClientes.SelectedValue;
            }

            dsReporte ds = new dsReporte();
            var adapter = new Model.DataSets.dsReporteTableAdapters.sp_ReporteContratosPorClienteTableAdapter();
            adapter.Fill(ds.sp_ReporteContratosPorCliente, idCliente);

            reportViewer1.LocalReport.DataSources.Clear();

           
            var dt = ds.sp_ReporteContratosPorCliente as System.Data.DataTable;

           if (dt != null)
            {
                reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("DSInforme", dt));
            }

            reportViewer1.RefreshReport();
        }
    }
}
