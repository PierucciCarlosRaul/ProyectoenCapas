using Sistema_de_Gestion;
using Sistema_de_Gestion.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaNegocio;

namespace Sistema_de_Gestion.Vista
{
    public partial class VentasAbm_Frm : Form
    {
        private BindingSource _binding_prod;
        private bool editando = false;

        private Venta venta;

        private const int idProd = 0;
        private const int cantidad = 2;
        private const int precioVenta = 3;

        public VentasAbm_Frm()
        {
            InitializeComponent();

            _binding_prod = new BindingSource();
            dtListaVentas.DataSource = _binding_prod;
        }

        public class WinAPI
        {
            [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
            public static extern IntPtr CreateRoundRectRgn(int leftRect, int topRect, int rightRect, int bottomRect, int widthEllipse, int heightEllipse);

            [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
            public static extern void ReleaseCapture();

            [DllImport("user32.DLL", EntryPoint = "SendMessage")]
            public static extern void SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        }


        private void SetGrillaProdVendidos()
        {
            try
            {
                dtListaVentas.AutoGenerateColumns = false;
                dtListaVentas.Columns.Clear();
                dtListaVentas.ReadOnly = false;

                DataGridViewTextBoxColumn idProd = new DataGridViewTextBoxColumn();
                idProd.Name = "idProducto";
                idProd.DataPropertyName = "idProducto";
                idProd.HeaderText = "Cod.Producto";
                idProd.Width = 100;
                idProd.ReadOnly = false;
                idProd.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                idProd.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtListaVentas.Columns.Add(idProd);

                DataGridViewTextBoxColumn descripcion = new DataGridViewTextBoxColumn();
                descripcion.Name = "descripcion";
                descripcion.DataPropertyName = "descripcion";
                descripcion.HeaderText = "Descripción";
                descripcion.Width = 253;
                descripcion.ReadOnly = true;
                descripcion.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                descripcion.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtListaVentas.Columns.Add(descripcion);

                DataGridViewTextBoxColumn cantidad = new DataGridViewTextBoxColumn();
                cantidad.Name = "stock";
                cantidad.DataPropertyName = "stock";
                cantidad.HeaderText = "Cantidad";
                cantidad.Width = 80;
                cantidad.ReadOnly = false;
                cantidad.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                cantidad.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtListaVentas.Columns.Add(cantidad);

                DataGridViewTextBoxColumn precioVenta = new DataGridViewTextBoxColumn();
                precioVenta.Name = "precioVenta";
                precioVenta.DataPropertyName = "precioVenta";
                precioVenta.HeaderText = "Precio Venta";
                precioVenta.Width = 80;
                precioVenta.ReadOnly = true;
                precioVenta.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                precioVenta.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtListaVentas.Columns.Add(precioVenta);

                dtListaVentas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            }
            catch (Exception ex)
            {
                Mensajes.MensajeError(ex);
            }
        }

        private void Limpiar()
        {
            txtidventa.Text = "0";
            txtcomentario.Text = "";
            _binding_prod.DataSource = null;
            txtTotal.Text = "0";
            dtListaVentas.Rows.Clear();
            cmbUsuario.SelectedIndex = 0;
        }

        private void InicializarFormulario()
        {
            try
            {
                SetGrillaProdVendidos();
                CargarComboUsuarios();
                HabilitarBotones(false);
                HabilitarCampos(false);
                Limpiar();
            }

            catch (Exception ex)
            {
                Mensajes.MensajeError(ex);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CargarComboUsuarios()
        {
            try
            {
                List<Usuario> table = ProductoNegocio.CargarComboUsuarios();
                Usuario usu = new Usuario();
                usu.IdUsuario = 0;
                usu.NombreCompleto = "";
                table.Insert(0, usu);
                cmbUsuario.DisplayMember = "NombreCompleto";
                cmbUsuario.ValueMember = "IdUsuario";
                cmbUsuario.DataSource = table;
            }
            catch (Exception ex)
            {
                Mensajes.MensajeError(ex);
            }
        }

        private void cmbUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void VentasAbm_Frm_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                WinAPI.ReleaseCapture();
                WinAPI.SendMessage(this.Handle, 0x112, 0xF012, 0);
            }
            catch (Exception ex)
            {
                Mensajes.MensajeError(ex);
            }
        }

        private void pnlSuperior_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                WinAPI.ReleaseCapture();
                WinAPI.SendMessage(this.Handle, 0x112, 0xF012, 0);
            }
            catch (Exception ex)
            {
                Mensajes.MensajeError(ex);
            }
        }

        private void VentasAbm_Frm_Load(object sender, EventArgs e)
        {
            InicializarFormulario();
        }

        private void txtidventa_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void btnAgregarfila_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtListaVentas.Enabled)
                {
                    AddItem();
                }
            }
            catch (Exception ex)
            {
                Mensajes.MensajeError(ex);
            }
        }

        private void AddItem()
        {
            try
            {
                ProductoVendido bd = new ProductoVendido();
                _binding_prod.Add(bd);
                dtListaVentas.CurrentCell = dtListaVentas[0, dtListaVentas.Rows.Count - 1];
            }
            catch (Exception ex)
            {
                Mensajes.MensajeError(ex);
            }

        }

        private void Btnquitarfila_Click(object sender, EventArgs e)
        {
            try
            {
                if (_binding_prod.Count > -1)
                {
                    _binding_prod.RemoveCurrent();
                    CalcularTotales();
                }
            }
            catch (Exception ex)
            {
                Mensajes.MensajeError(ex);
            }
        }

        private void dtListaVentas_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (_binding_prod.Count > 0)
                {
                    ProductoVendido _item = (ProductoVendido)_binding_prod.Current;
                    Producto _prod = ProductoNegocio.ObtenerProductoxId(_item.IdProducto);

                    if (e.ColumnIndex == idProd)
                    {
                        if (_prod.IdProducto != 0)
                        {
                            _item.Descripcion = _prod.Descripcion;
                            _item.PrecioVenta = _prod.PrecioVenta;
                            _item.Stock = 0;
                        }
                        else
                        {
                           // _item = new ProductoVendido();
                            Mensajes.MostrarMensaje("No existe ese producto","Error", MessageBoxIcon.Error, MessageBoxButtons.OK);
                        }
                    }

                    if (e.ColumnIndex == cantidad)
                    {
                        CalcularTotales();
                        if (_prod.Stock < _item.Stock)
                        {
                            Mensajes.MostrarMensaje("La cantidad ingresada es mayor al stock actual del producto \n\n" +
                            "El stock actual en Almacén es: " + _prod.Stock + " ", "Error", MessageBoxIcon.Error, MessageBoxButtons.OK);
                        }
                    }

                    _binding_prod.ResetBindings(false);
                    dtListaVentas.Refresh();
                }
                dtListaVentas.EndEdit();

            }
            catch (Exception ex)
            {
                Mensajes.MensajeError(ex);
            }
        }

        private bool HabilitarCampos(bool valor)
        {
            txtcomentario.Enabled = valor;
            dtListaVentas.Enabled = valor;
            return valor;
        }

        private void BtnNuevo_Click(object sender, EventArgs e)
        {
            HabilitarBotones(true);
            HabilitarCampos(true);
        }

        private void HabilitarBotones(bool valor)
        {
            BtnNuevo.Enabled = !valor;
            btnGuardar.Enabled = valor;
            btnEliminar.Enabled = valor;
            BtnCancelar.Enabled = valor;
            btnAgregarfila.Enabled = valor;
            Btnquitarfila.Enabled = valor;
        }

        private void dtListaVentas_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (_binding_prod.Count > 0)
                {
                    e.SuppressKeyPress = true;
                    int _col = dtListaVentas.CurrentCell.ColumnIndex;
                    int _row = dtListaVentas.CurrentCell.RowIndex;

                    if (_row < 0)
                        _row = 0;

                    int _totCols = dtListaVentas.Columns.Count;
                    int _totRows = dtListaVentas.RowCount;

                    if (e.KeyCode == Keys.Tab)
                    {
                        if (_col < (_totCols - 1))
                        {
                            dtListaVentas.CurrentCell = dtListaVentas[_col + 1, _row];
                        }
                        else if (_row == (_totRows - 1))
                        {
                            AddItem();
                            dtListaVentas.CurrentCell = dtListaVentas[0, _row + 1];
                        }
                        else if (_col == (dtListaVentas.Columns.Count - 1))
                        {
                            dtListaVentas.CurrentCell = dtListaVentas[0, _row + 1];
                        }
                    }
                    else if (e.KeyCode == Keys.Up)
                    {
                        if (_row > 0)
                        {
                            dtListaVentas.CurrentCell = dtListaVentas[_col, _row - 1];
                        }
                    }
                    else if (e.KeyCode == Keys.Down)
                    {
                        if (_row < dtListaVentas.RowCount - 1)
                        {
                            dtListaVentas.CurrentCell = dtListaVentas[_col, _row + 1];
                        }
                    }
                    else if (e.KeyCode == Keys.Left)
                    {
                        if (_col > 0)
                        {
                            dtListaVentas.CurrentCell = dtListaVentas[_col - 1, _row];
                        }
                    }
                    else if (e.KeyCode == Keys.Right)
                    {
                        if (_col < dtListaVentas.ColumnCount - 1)
                        {
                            dtListaVentas.CurrentCell = dtListaVentas[_col + 1, _row];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Mensajes.MensajeError(ex);
            }
        }

        private void CargarDatos()
        {
            try
            {
                venta.IdUsuario = Convert.ToInt32(cmbUsuario.SelectedValue);
                venta.Comentario = txtcomentario.Text;

                if (_binding_prod.Count > 0)
                {
                    venta.listaDetalleProductoVendido = new List<ProductoVendido>();

                    foreach (ProductoVendido _it in _binding_prod.List)
                    {
                        ProductoVendido item = new ProductoVendido();
                        item.IdProducto = _it.IdProducto;
                        item.Stock = _it.Stock;
                        venta.listaDetalleProductoVendido.Add(_it);
                    }
                }
            }
            catch (Exception ex)
            {
                Mensajes.MensajeError(ex);
            }
        }

        private bool validar()
        {
            bool correcto = true;
            try
            {
                if (_binding_prod.Count <= 0)
                {
                    Mensajes.MostrarMensaje("No ingresó un item en la grilla", "Faltan Datos", MessageBoxIcon.Warning, MessageBoxButtons.OK);
                    correcto = false;
                    return correcto;

                }

                if (cmbUsuario.SelectedIndex <= 0)
                {
                    Mensajes.MostrarMensaje("No seleccionó un usuario", "Faltan Datos", MessageBoxIcon.Warning, MessageBoxButtons.OK);
                    correcto = false;
                    return correcto;

                }

            }
            catch (Exception ex)
            {
                Mensajes.MensajeError(ex);
            }

            return correcto;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            int idventa;

            idventa = Convert.ToInt32(txtidventa.Text);

            if (idventa != 0)
            {
                editando = true;
            }
            else 
            {
                venta = new Venta();
            }

            try
            {
                if (validar() == true)
                {
                    CargarDatos();
                    if (editando == false)
                    {
                        VentaNegocio.InsertarVenta(venta);
                        Mensajes.MostrarMensaje("La Venta se Registró Correctamente", "Exito", MessageBoxIcon.Information, MessageBoxButtons.OK);
                    }
                    else
                    {
                        VentaNegocio.ModificarVenta(venta);
                        Mensajes.MostrarMensaje("La Venta Nro: " + venta.IdVenta + " se Modificó Correctamente", "Exito", MessageBoxIcon.Information, MessageBoxButtons.OK);
                    }
                    Limpiar();
                    InicializarFormulario();
                    editando = false;
                    HabilitarCampos(false);

                }
            }
            catch (Exception ex)
            {
                Mensajes.MensajeError(ex);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {

                int _idproducto = 0;

                _idproducto = Convert.ToInt32(txtidventa.Text);


                if (_idproducto != 0)
                {
                    DialogResult resultado = MessageBox.Show("¿Confirma Eliminar la Venta Nro. : " + _idproducto + "?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (resultado == DialogResult.Yes)
                    {
                        VentaNegocio.EliminarVenta(_idproducto);
                        Mensajes.MostrarMensaje("La Venta " + _idproducto + " se eliminó correctamente", "Exito", MessageBoxIcon.Information, MessageBoxButtons.OK);


                        Limpiar();
                        InicializarFormulario();
                        editando = false;
                        HabilitarCampos(false);
                    }
                }
                else
                {
                    MessageBox.Show("El producto no se eliminó");
                }

            }
            catch (Exception ex)
            {
                Mensajes.MensajeError(ex);
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            Limpiar();
            InicializarFormulario();
            HabilitarCampos(false);
        }

       

        private void BtnInformacion_Click(object sender, EventArgs e)
        {
            Mensajes.MostrarMensaje("El botón Nuevo Desbloquea los campos, la grilla y los botones Guardar, Modificar, Eliminar, Cancelar Agregar y Quitar fila. \n\n" +
                "Debe presionar el botón nuevo para cada acción que quiera realizar ( Guardar, Eliminar o Cancelar ).\n\n" +
                "Para modificar una Venta puede buscar ventas realizadas presionando el botón con la imagen de la lupa y presionando doble click en la grilla de consulta de ventas. \n\n" +
                "Para agregar a la grilla un producto se debe presionar en el botón con la imagen del signo +, y en la grilla ingresar un Cod.producto en dicha columna. \n\n" +
                "Se puede editar y asignar un numero de producto así también como su cantidad. \n\n" +
                "Para eliminar una venta primero se debe buscar una venta por la lupa y traer la venta al formulario \n" +
                "luego presionar en eliminar.", "Información", MessageBoxIcon.Information, MessageBoxButtons.OK);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                ConsultaVentas_Frm _frm = new ConsultaVentas_Frm();
                _frm.ShowDialog();

                if (_frm.DialogResult == DialogResult.OK)
                {
                 
                    HabilitarCampos(true);
                    HabilitarBotones(true);

                    int idventa = 0;

                    idventa = _frm.Idventa;

                    venta = VentaNegocio.ObtenerVentaxId(idventa);

                    if (venta.IdVenta != 0)
                    {
                        txtidventa.Text = venta.IdVenta.ToString();
                        txtcomentario.Text = venta.Comentario.ToString();
                        cmbUsuario.SelectedValue = venta.IdUsuario;
                        _binding_prod.DataSource = venta.listaDetalleProductoVendido;
                        CalcularTotales();
                    }
                    else
                    {
                        txtidventa.Text = "0";
                    }
                }
            }
            catch (Exception ex)
            {
                Mensajes.MensajeError(ex);
            }
        }

        private decimal CalcularTotales() 
        { 
            decimal total = 0;

            foreach (ProductoVendido _item in _binding_prod)
            {
                total += _item.PrecioVenta * _item.Stock;
            }
            txtTotal.Text = total.ToString("N2");

            return total;
        }

    }
}
