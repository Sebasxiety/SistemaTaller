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

namespace SistemaTaller.View.Clientes
{
    public partial class frmClientes : Form
    {
        public frmClientes()
        {
            InitializeComponent(); controller = new clsClientes();
        }
        private clsClientes controller;
        private List<dtoClientes> listaClientes;
        private void frmClientes_Load(object sender, EventArgs e)
        {
            CargarListaClientes();
        }
        private void CargarListaClientes()
        {
            listaClientes = controller.Listar();
            lstDatos.DataSource = null;
            lstDatos.DataSource = listaClientes;
            //lstDatos.DisplayMember = "Nombre";
            lstDatos.SelectedIndex = -1;
        }

        private void lstDatos_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            if (lstDatos.SelectedIndex >= 0)
            {
                dtoClientes clienteSeleccionado = (dtoClientes)lstDatos.SelectedItem;
                txtNombre.Text = clienteSeleccionado.Nombre;
                txtApellido.Text = clienteSeleccionado.Apellido;
                txtTelefono.Text = clienteSeleccionado.Telefono;
                txtEmail.Text = clienteSeleccionado.Email;
            }
            else
            {
                LimpiarControles();
            }

        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            dtoClientes nuevoCliente = new dtoClientes
            {
                Nombre = txtNombre.Text,
                Apellido = txtApellido.Text,
                Telefono = txtTelefono.Text,
                Email = txtEmail.Text
            };
            bool resultado = controller.Registrar(nuevoCliente);
            if (resultado)
            {
                MessageBox.Show("Cliente registrado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarListaClientes();
                LimpiarControles();
            }
            else
            {
                MessageBox.Show("Error al registrar el cliente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (lstDatos.SelectedIndex >= 0)
            {
                dtoClientes clienteSeleccionado = (dtoClientes)lstDatos.SelectedItem;
                clienteSeleccionado.Nombre = txtNombre.Text;
                clienteSeleccionado.Apellido = txtApellido.Text;
                clienteSeleccionado.Telefono = txtTelefono.Text;
                clienteSeleccionado.Email = txtEmail.Text;
                bool resultado = controller.Actualizar(clienteSeleccionado);
                if (resultado)
                {
                    MessageBox.Show("Cliente actualizado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarListaClientes();
                    LimpiarControles();
                }
                else
                {
                    MessageBox.Show("Error al actualizar el cliente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Selecciona un cliente de la lista primero.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (lstDatos.SelectedIndex >= 0)
            {
                dtoClientes clienteSeleccionado = (dtoClientes)lstDatos.SelectedItem;
                DialogResult respuesta = MessageBox.Show("¿Estás seguro de eliminar este cliente?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (respuesta == DialogResult.Yes)
                {
                    bool resultado = controller.Eliminar(clienteSeleccionado.IdCliente);
                    if (resultado)
                    {
                        MessageBox.Show("Cliente eliminado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarListaClientes();
                        LimpiarControles();
                    }
                    else
                    {
                        MessageBox.Show("Error al eliminar el cliente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Selecciona un cliente de la lista primero.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void LimpiarControles()
        {
            txtNombre.Clear();
            txtApellido.Clear();
            txtTelefono.Clear();
            txtEmail.Clear();
        }
    }
}
