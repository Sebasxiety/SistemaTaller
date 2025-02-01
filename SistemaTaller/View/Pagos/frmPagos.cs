using SistemaTaller.Controller;
using SistemaTaller.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaTaller.View.Pagos
{
    public partial class frmPagos : Form
    {
        private clsPagos controllerPagos;
        private clsContratos controllerContratos;
        private List<dtoPagos> listaPagos;
        private List<dtoContratos> listaContratos;
        public frmPagos()
        {
            InitializeComponent();
        }

        private void frmPagos_Load(object sender, EventArgs e)
        {

            controllerPagos = new clsPagos();
            controllerContratos = new clsContratos();
            CargarComboContratos();
            CargarListaPagos();
        }
        private void CargarComboContratos()
        {
            listaContratos = controllerContratos.Listar();
            cmbContratos.DataSource = null;
            cmbContratos.DataSource = listaContratos;
            cmbContratos.DisplayMember = "IdContrato";
            cmbContratos.ValueMember = "IdContrato";
            cmbContratos.SelectedIndex = -1;
        }

        private void CargarListaPagos()
        {
            listaPagos = controllerPagos.Listar();
            lstDatos.DataSource = null;
            lstDatos.DataSource = listaPagos;
            lstDatos.DisplayMember = "MetodoPago"; // Ajusta si deseas mostrar algo más
            lstDatos.SelectedIndex = -1;
        }

        private void lstDatos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstDatos.SelectedIndex >= 0)
            {
                dtoPagos pago = (dtoPagos)lstDatos.SelectedItem;
                cmbContratos.SelectedValue = pago.IdContrato;
                txtMonto.Text = pago.Monto.ToString();
                dtpFecha.Value = pago.FechaPago;
                cmbMetodoPago.Text = pago.MetodoPago;
            }
            else
            {
                LimpiarControles();
            }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {

            dtoPagos nuevo = new dtoPagos
            {
                IdContrato = (int)cmbContratos.SelectedValue,
                Monto = decimal.Parse(txtMonto.Text),
                FechaPago = dtpFecha.Value,
                MetodoPago = cmbMetodoPago.Text
            };
            bool resultado = controllerPagos.Registrar(nuevo);
            if (resultado)
            {
                MessageBox.Show("Pago registrado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarListaPagos();
                LimpiarControles();
            }
            else
            {
                MessageBox.Show("Error al registrar el pago.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (lstDatos.SelectedIndex >= 0)
            {
                dtoPagos pago = (dtoPagos)lstDatos.SelectedItem;
                pago.IdContrato = (int)cmbContratos.SelectedValue;
                pago.Monto = decimal.Parse(txtMonto.Text);
                pago.FechaPago = dtpFecha.Value;
                pago.MetodoPago = cmbMetodoPago.Text;
                bool resultado = controllerPagos.Actualizar(pago);
                if (resultado)
                {
                    MessageBox.Show("Pago actualizado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarListaPagos();
                    LimpiarControles();
                }
                else
                {
                    MessageBox.Show("Error al actualizar el pago.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Selecciona un pago de la lista.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (lstDatos.SelectedIndex >= 0)
            {
                dtoPagos pago = (dtoPagos)lstDatos.SelectedItem;
                DialogResult r = MessageBox.Show("¿Eliminar pago?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (r == DialogResult.Yes)
                {
                    bool resultado = controllerPagos.Eliminar(pago.IdPago);
                    if (resultado)
                    {
                        MessageBox.Show("Pago eliminado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarListaPagos();
                        LimpiarControles();
                    }
                    else
                    {
                        MessageBox.Show("Error al eliminar el pago.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Selecciona un pago de la lista.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void LimpiarControles()
        {
            cmbContratos.SelectedIndex = -1;
            txtMonto.Clear();
            dtpFecha.Value = DateTime.Now;
            cmbMetodoPago.SelectedIndex = -1;
        }
    }
}
