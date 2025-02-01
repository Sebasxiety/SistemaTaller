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

namespace SistemaTaller.View.Contratos
{
    public partial class frmContratos : Form
    {
        private clsContratos controllerContratos;
        private clsClientes controllerClientes;
        private clsVehiculos controllerVehiculos;
        private List<dtoContratos> listaContratos;
        private List<dtoClientes> listaClientes;
        private List<dtoVehiculos> listaVehiculos;
        public frmContratos()
        {
            InitializeComponent();
        }

        private void frmContratos_Load(object sender, EventArgs e)
        {
            controllerContratos = new clsContratos();
            controllerClientes = new clsClientes();
            controllerVehiculos = new clsVehiculos();
            CargarCombos();
            CargarListaContratos();
        }
        private void CargarCombos()
        {
            listaClientes = controllerClientes.Listar();
            cmbClientes.DataSource = null;
            cmbClientes.DataSource = listaClientes;
            cmbClientes.DisplayMember = "Nombre"; // o una propiedad combinada como "NombreCompleto"
            cmbClientes.ValueMember = "IdCliente";
            cmbClientes.SelectedIndex = -1;

            listaVehiculos = controllerVehiculos.Listar();
            cmbVehiculos.DataSource = null;
            cmbVehiculos.DataSource = listaVehiculos;
            cmbVehiculos.DisplayMember = "Modelo"; 
            cmbVehiculos.ValueMember = "IdVehiculo";
            cmbVehiculos.SelectedIndex = -1;
        }

        private void CargarListaContratos()
        {
            listaContratos = controllerContratos.Listar();
            lstDatos.DataSource = null;
            lstDatos.DataSource = listaContratos;
            lstDatos.DisplayMember = "IdContrato"; 
            lstDatos.SelectedIndex = -1;
        }

        private void lstDatos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstDatos.SelectedIndex >= 0)
            {
                dtoContratos contrato = (dtoContratos)lstDatos.SelectedItem;
                cmbClientes.SelectedValue = contrato.IdCliente;
                cmbVehiculos.SelectedValue = contrato.IdVehiculo;
                dtpInicio.Value = contrato.FechaInicio;
                dtpFin.Value = contrato.FechaFin.HasValue ? contrato.FechaFin.Value : DateTime.Now;
                txtMonto.Text = contrato.MontoTotal.ToString();
            }
            else
            {
                LimpiarControles();
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (lstDatos.SelectedIndex >= 0)
            {
                dtoContratos contrato = (dtoContratos)lstDatos.SelectedItem;
                DialogResult r = MessageBox.Show("¿Eliminar contrato?", "Confirmar", MessageBoxButtons.YesNo);
                if (r == DialogResult.Yes)
                {
                    bool resultado = controllerContratos.Eliminar(contrato.IdContrato);
                    if (resultado)
                    {
                        MessageBox.Show("Contrato eliminado.");
                        CargarListaContratos();
                        LimpiarControles();
                    }
                    else
                    {
                        MessageBox.Show("Error al eliminar contrato.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Selecciona un contrato en la lista.");
            }
        }
        private void LimpiarControles()
        {
            cmbClientes.SelectedIndex = -1;
            cmbVehiculos.SelectedIndex = -1;
            dtpInicio.Value = DateTime.Now;
            dtpFin.Value = DateTime.Now;
            txtMonto.Clear();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (dtpFin.Value.Date < dtpInicio.Value.Date.AddDays(1))
            {
                MessageBox.Show("La fecha de fin debe ser al menos un día mayor que la fecha de inicio.",
                                "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            dtoContratos nuevo = new dtoContratos
            {
                IdCliente = (int)cmbClientes.SelectedValue,
                IdVehiculo = (int)cmbVehiculos.SelectedValue,
                FechaInicio = dtpInicio.Value,
                FechaFin = dtpFin.Value,
                MontoTotal = decimal.Parse(txtMonto.Text)
            };

            bool resultado = controllerContratos.Registrar(nuevo);
            if (resultado)
            {
                MessageBox.Show("Contrato registrado correctamente.");
                CargarListaContratos();
                LimpiarControles();
            }
            else
            {
                MessageBox.Show("Error al registrar el contrato.");
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (lstDatos.SelectedIndex < 0)
            {
                MessageBox.Show("Selecciona un contrato de la lista.");
                return;
            }

            // Valida que FechaFin sea al menos 1 día mayor que FechaInicio
            if (dtpFin.Value.Date < dtpInicio.Value.Date.AddDays(1))
            {
                MessageBox.Show("La fecha de fin debe ser al menos un día mayor que la fecha de inicio.",
                                "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            dtoContratos contrato = (dtoContratos)lstDatos.SelectedItem;
            contrato.IdCliente = (int)cmbClientes.SelectedValue;
            contrato.IdVehiculo = (int)cmbVehiculos.SelectedValue;
            contrato.FechaInicio = dtpInicio.Value;
            contrato.FechaFin = dtpFin.Value;
            contrato.MontoTotal = decimal.Parse(txtMonto.Text);

            bool resultado = controllerContratos.Actualizar(contrato);
            if (resultado)
            {
                MessageBox.Show("Contrato actualizado correctamente.");
                CargarListaContratos();
                LimpiarControles();
            }
            else
            {
                MessageBox.Show("Error al actualizar el contrato.");
            }
        }

        private void cmbClientes_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void cmbVehiculos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
