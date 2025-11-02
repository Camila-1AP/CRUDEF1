using CrudORMentity.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrudORMentity
{
    public partial class FrmProductos : Form
    {
        private Actividad1Entities _context;

        public FrmProductos()
        {
            InitializeComponent();
        }
        private void cargarDatos()
        {
            var listaProductos = _context.Productos
                   .Select(p => new {
                       ID = p.ProductoID,
                       Nombre = p.NombreProducto,
                       DescripcíonProducto = p.Descripcion,
                       PrecioProducto = p.Precio,
                       StockProducto = p.Stock,
                       Categoria = p.Categorias.NombreCategoria,

                   }).ToList();

            dgv.DataSource = listaProductos;
        }

        private void cargarCmbCategorias()
        {
            var categorias = _context.Categorias.Select(c => new {
                ID = c.CategoriaID,
                Nombre = c.NombreCategoria
            }).ToList();

            cmbCategorias.DataSource = categorias;
            cmbCategorias.DisplayMember = "Nombre";
            cmbCategorias.ValueMember = "ID";

            cmbCategoriasUp.DataSource = categorias;
            cmbCategoriasUp.DisplayMember = "Nombre";
            cmbCategoriasUp.ValueMember = "ID";
        }


        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void FrmProductos_Load(object sender, EventArgs e)
        {
            _context = new Actividad1Entities();

            this.cargarCmbCategorias();
            this.cargarDatos();
        }

        private void Add_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbCategorias.SelectedValue.ToString()))
            {
                MessageBox.Show("El campo de categoría esta vacío o es incorrecto", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                return;
            }
            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                MessageBox.Show("El campo de nombre esta vacío o es incorrecto", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;

            }
            if (string.IsNullOrEmpty(txtDesc.Text))
            {
                MessageBox.Show("El campo de descripción esta vacío o es incorrecto", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;

            }
            if (string.IsNullOrEmpty(txtStock.Text))
            {
                MessageBox.Show("El campo de stock esta vacío o es incorrecto", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;

            }
            if (string.IsNullOrEmpty(txtPrecio.Text))
            {
                MessageBox.Show("El campo de precio esta vacío o es incorrecto", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;

            }


            Productos producto = new Productos()
            {
                NombreProducto = txtNombre.Text,
                Descripcion = txtDesc.Text,
                Precio = Convert.ToDecimal(txtPrecio.Text),
                Stock = Convert.ToInt32(txtStock.Text),
                CategoriaID = Convert.ToInt32(cmbCategorias.SelectedValue),
               


            };

            _context.Productos.Add(producto);

            int rowsAffected = _context.SaveChanges();
            if (rowsAffected > 0)
            {
                MessageBox.Show("Se ha insertado el producto en la base de datos.");
            }

            this.cargarDatos();
        }

        private void Delete_Click(object sender, EventArgs e)
        {


            if (string.IsNullOrEmpty(txtID.Text))
            {
                MessageBox.Show("Debe introducir un ID válido.");
                return;
            }

            if (MessageBox.Show("Recuerde al aceptar eliminar este producto, también se eliminarán los detalles de factura y de compra de dicho producto. Quizás quiera revisarlo antes.", "Advertencia de Eliminación de Producto", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
            {
                return;
            }
            else
            {
                if (string.IsNullOrEmpty(txtID.Text))
                {
                    MessageBox.Show("Debe introducir un ID válido.");
                    return;
                }

                int productoID = Convert.ToInt32(txtID.Text);

                Productos productos = _context.Productos.FirstOrDefault(q => q.ProductoID.Equals(productoID));
                if (productos == null)
                {
                    MessageBox.Show("Producto no existe.");
                    return;
                }

                _context.Productos.Remove(productos);
                int rowsAffected = _context.SaveChanges();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Se ha eliminado el producto en la base de datos.");
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
            if (string.IsNullOrEmpty(cmbCategoriasUp.SelectedValue.ToString()))
            {
                MessageBox.Show("El campo de categoría esta vacío o es incorrecto", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                return;
            }
            if (string.IsNullOrEmpty(txtNombreUp.Text))
            {
                MessageBox.Show("El campo de nombre esta vacío o es incorrecto", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;

            }
            if (string.IsNullOrEmpty(txtDescUp.Text))
            {
                MessageBox.Show("El campo de descripción esta vacío o es incorrecto", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;

            }
            if (string.IsNullOrEmpty(txtStockUp.Text))
            {
                MessageBox.Show("El campo de stock esta vacío o es incorrecto", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;

            }
            if (string.IsNullOrEmpty(txtPrecioUp.Text))
            {
                MessageBox.Show("El campo de precio esta vacío o es incorrecto", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;

            }

            int productoID = Convert.ToInt32(txtIDUp.Text);

            Productos productos = _context.Productos.FirstOrDefault(q => q.ProductoID.Equals(productoID));
            if (productos == null)
            {
                MessageBox.Show("Producto no existe.");
                return;
            }

            productos.NombreProducto = txtNombreUp.Text;
            productos.Descripcion = txtDescUp.Text;
            productos.Precio = Convert.ToDecimal(txtPrecioUp.Text);
            productos.Stock = Convert.ToInt32(txtStockUp.Text);
            productos.CategoriaID = Convert.ToInt32(cmbCategoriasUp.SelectedValue);
           

            int rowsAffected = _context.SaveChanges();
            if (rowsAffected > 0)
            {
                MessageBox.Show("Se ha actualizado el producto en la base de datos.");
            }

            this.cargarDatos();

        }

        private void View_Click(object sender, EventArgs e)
        {
            cargarDatos();
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

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void txtStock_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
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

        private void txtPrecioUp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != '\b')
            {
                e.Handled = true;
            }

        }

        private void txtStockUp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }
    }
}
