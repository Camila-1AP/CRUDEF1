using CrudORMentity.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrudORMentity
{
    public partial class FrmProveedores : Form
    {
        private Actividad1Entities _context;
        public FrmProveedores()
        {
            InitializeComponent();
        }
        private void cargarDatos()
        {
            var listaProveedores = _context.Proveedores
                   .Select(p => new {
                       ID = p.ProveedorID,
                       Nombre = p.NombreProveedor,
                       TelefonoProveedor = p.Telefono,
                       Eamil = p.CorreoElectronico,


                   }).ToList();

            dgv.DataSource = listaProveedores;
        }
        private void Add_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                MessageBox.Show("El campo de nombre esta vacío o es incorrecto", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (string.IsNullOrEmpty(maskTel.Text))
            {
                MessageBox.Show("El campo de teléfono esta vacío o es incorrecto", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (!maskTel.MaskFull)
            {
                MessageBox.Show("Por favor ingresa un número de teléfono completo.");
                maskTel.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                MessageBox.Show("El campo de correo electrónico esta vacío o es incorrecto", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (!Regex.IsMatch(txtEmail.Text, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            {
                MessageBox.Show("Correo electrónico inválido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEmail.Focus();
                return;
            }

            Proveedores proveedor = new Proveedores()
            {
                NombreProveedor = txtNombre.Text,
                Telefono = maskTel.Text,
                CorreoElectronico = txtEmail.Text,

            };

            _context.Proveedores.Add(proveedor);

            int rowsAffected = _context.SaveChanges();
            if (rowsAffected > 0)
            {
                MessageBox.Show("Se ha insertado el proveedor en la base de datos.");
            }

            this.cargarDatos();
        }

        private void FrmProveedores_Load(object sender, EventArgs e)
        {
            _context = new Actividad1Entities();

            // this.cargarCmbCategorias();
            this.cargarDatos();

        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtID.Text))
            {
                MessageBox.Show("Debe introducir un ID válido.");
                return;
            }
            if (MessageBox.Show("Recuerde al aceptar eliminar este proveedor, los detalles de compra, relacionados al proveedor se eliminarán. Quizás quiera revisarlos antes.", "Advertencia de Eliminación de categoría", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
            {
                return;
            }
            else
            {

                int proveedorID = Convert.ToInt32(txtID.Text);

                Proveedores proveedor = _context.Proveedores.FirstOrDefault(q => q.ProveedorID.Equals(proveedorID));
                if (proveedor == null)
                {
                    MessageBox.Show($"Proveedor {proveedor} no existe.");
                    return;
                }

                _context.Proveedores.Remove(proveedor);
                int rowsAffected = _context.SaveChanges();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Se ha eliminado el proveedor en la base de datos.");
                }

                this.cargarDatos();


            }
        }

        private void Update_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtIDUp.Text))
            {
                MessageBox.Show("El campo de ID esta vacío o es incorrecto", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (string.IsNullOrEmpty(txtNombreUp.Text))
            {
                MessageBox.Show("El campo de nombre esta vacío o es incorrecto", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (string.IsNullOrEmpty(maskTelUp.Text))
            {
                MessageBox.Show("El campo de teléfono esta vacío o es incorrecto", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (!maskTelUp.MaskFull)
            {
                MessageBox.Show("Por favor ingresa un número de teléfono completo.");
                maskTelUp.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtEmailUp.Text))
            {
                MessageBox.Show("El campo de correo electrónico esta vacío o es incorrecto", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (!Regex.IsMatch(txtEmailUp.Text, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            {
                MessageBox.Show("Correo electrónico inválido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEmailUp.Focus();
                return;
            }

            int proveedorID = Convert.ToInt32(txtIDUp.Text);

            Proveedores proveedor = _context.Proveedores.FirstOrDefault(q => q.ProveedorID.Equals(proveedorID));
            if (proveedor == null)
            {
                MessageBox.Show($"Proveedor {proveedor} no existe.");
                return;
            }

            proveedor.NombreProveedor = txtNombreUp.Text;
            proveedor.Telefono = maskTelUp.Text;
            proveedor.CorreoElectronico = txtEmailUp.Text;

            int rowsAffected = _context.SaveChanges();
            if (rowsAffected > 0)
            {
                MessageBox.Show("Se ha actualizado el proveedor en la base de datos.");
            }

            this.cargarDatos();
        }

        private void View_Click(object sender, EventArgs e)
        {
            this.cargarDatos();
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) || char.IsWhiteSpace(e.KeyChar) || e.KeyChar == (char)Keys.Back)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsWhiteSpace(e.KeyChar))
            {

                e.Handled = true;

            }
            else
            {
                e.Handled = false;

            }
        }

        private void txtID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtIDUp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void txtNombreUp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) || char.IsWhiteSpace(e.KeyChar) || e.KeyChar == (char)Keys.Back)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }

        }

        private void txtEmailUp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsWhiteSpace(e.KeyChar))
            {

                e.Handled = true;

            }
            else
            {
                e.Handled = false;

            }

        }
    }
}
