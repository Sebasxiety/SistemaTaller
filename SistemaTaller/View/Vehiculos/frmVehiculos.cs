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

namespace SistemaTaller.View.Vehiculos
{
    public partial class frmVehiculos : Form
    {
        private clsVehiculos controller;
        private List<dtoVehiculos> listaVehiculos;
        public frmVehiculos()
        {
            InitializeComponent();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
        private void LimpiarControles()
        {
            txtMarca.Clear();
            txtModelo.Clear();
            txtAnio.Clear();
            cmbColor.SelectedIndex = -1;
            cmbEstado.SelectedIndex = -1;
        }
        private void frmVehiculos_Load(object sender, EventArgs e)
        {
            controller = new clsVehiculos();
            CargarOpcionesColor();
            CargarOpcionesEstado();
            CargarListaVehiculos();
        }
        private void CargarOpcionesColor()
        {
            cmbColor.Items.Clear();
            cmbColor.Items.Add("Blanco");
            cmbColor.Items.Add("Negro");
            cmbColor.Items.Add("Rojo");
            cmbColor.Items.Add("Azul");
            cmbColor.Items.Add("Gris");
            cmbColor.SelectedIndex = -1;
        }

        private void CargarOpcionesEstado()
        {
            cmbEstado.Items.Clear();
            cmbEstado.Items.Add("Disponible");
            cmbEstado.Items.Add("Ocupado");
            cmbEstado.Items.Add("Mantenimiento");
            cmbEstado.SelectedIndex = -1;
        }

        private void CargarListaVehiculos()
        {
            listaVehiculos = controller.Listar();
            lstDatos.DataSource = null;
            lstDatos.DataSource = listaVehiculos;
            lstDatos.DisplayMember = "Modelo";
            lstDatos.SelectedIndex = -1;
        }

        private void lstDatos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstDatos.SelectedIndex >= 0)
            {
                dtoVehiculos veh = (dtoVehiculos)lstDatos.SelectedItem;
                txtMarca.Text = veh.Marca;
                txtModelo.Text = veh.Modelo;
                txtAnio.Text = veh.Anio.ToString();
                cmbColor.Text = veh.Color;
                cmbEstado.Text = veh.Estado;
            }
            else
            {
                LimpiarControles();
            }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            dtoVehiculos nuevo = new dtoVehiculos
            {
                Marca = txtMarca.Text,
                Modelo = txtModelo.Text,
                Anio = int.Parse(txtAnio.Text),
                Color = cmbColor.Text,
                Estado = cmbEstado.Text
            };
            bool resultado = controller.Registrar(nuevo);
            if (resultado)
            {
                MessageBox.Show("Vehículo registrado.");
                CargarListaVehiculos();
                LimpiarControles();
            }
            else
            {
                MessageBox.Show("Error al registrar.");
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (lstDatos.SelectedIndex >= 0)
            {
                dtoVehiculos veh = (dtoVehiculos)lstDatos.SelectedItem;
                veh.Marca = txtMarca.Text;
                veh.Modelo = txtModelo.Text;
                veh.Anio = int.Parse(txtAnio.Text);
                veh.Color = cmbColor.Text;
                veh.Estado = cmbEstado.Text;
                bool resultado = controller.Actualizar(veh);
                if (resultado)
                {
                    MessageBox.Show("Vehículo actualizado.");
                    CargarListaVehiculos();
                    LimpiarControles();
                }
                else
                {
                    MessageBox.Show("Error al actualizar.");
                }
            }
            else
            {
                MessageBox.Show("Selecciona un vehículo en la lista.");
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (lstDatos.SelectedIndex >= 0)
            {
                dtoVehiculos veh = (dtoVehiculos)lstDatos.SelectedItem;
                DialogResult r = MessageBox.Show("¿Eliminar vehículo?", "Confirmar", MessageBoxButtons.YesNo);
                if (r == DialogResult.Yes)
                {
                    bool resultado = controller.Eliminar(veh.IdVehiculo);
                    if (resultado)
                    {
                        MessageBox.Show("Vehículo eliminado.");
                        CargarListaVehiculos();
                        LimpiarControles();
                    }
                    else
                    {
                        MessageBox.Show("Error al eliminar.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Selecciona un vehículo en la lista.");
            }
        }
    }
}
