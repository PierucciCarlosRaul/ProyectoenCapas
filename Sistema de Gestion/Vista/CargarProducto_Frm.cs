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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using CapaNegocio;
using System.Runtime.Remoting.Contexts;


namespace Sistema_de_Gestion.Vista
{
    public partial class CargarProducto_Frm : Form
    {
        private bool editando = false;
        Producto prod = new Producto();

        public CargarProducto_Frm()
        {
            InitializeComponent();
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

        private void CargarProducto_Frm_Load(object sender, EventArgs e)
        {
           
            try
            {
                InicializarFormulario();
                HabilitarCampos(false);
            }

            catch (Exception ex)
            {
                Mensajes.MensajeError(ex);
            }

        }


        private void InicializarFormulario()
        {
            try
            {
                txtCosto.Text = "0";
                txtprecioventa.Text = "0";
                txtstock.Text = "0";
                ListarProductos();
                SetGrillaProducto();
                CargarComboUsuarios();
                BtnNuevo.Enabled = true;
                btnGuardar.Enabled = false;
                btnEliminar.Enabled = false;
                BtnCancelar.Enabled = false;
            }

            catch (Exception ex)
            {
                Mensajes.MensajeError(ex);
            }
        }

        private void ListarProductos()
        {
            try
            {
                List<Producto> listaProductos = ProductoNegocio.listarProductos();
                dtListaProductos.DataSource = listaProductos;
                this.Refresh();
            }
          
              catch (Exception ex)
            {
                Mensajes.MensajeError(ex);
            }
        }

        private void Limpiar()
        {
            try
            {
                txtDescripcion.Text = "";
                txtCosto.Text = "";
                txtprecioventa.Text = "";
                txtstock.Text = "";
                cmbUsuario.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Mensajes.MensajeError(ex);
            }
        }

        private void SetGrillaProducto()
        {
            try
            {
                dtListaProductos.AutoGenerateColumns = false;
                dtListaProductos.Columns.Clear();
                dtListaProductos.ReadOnly = false;

                DataGridViewTextBoxColumn idProducto = new DataGridViewTextBoxColumn();
                idProducto.Name = "idProducto";
                idProducto.DataPropertyName = "idProducto";
                idProducto.HeaderText = "Cod.Producto";
                idProducto.Width = 100;
                idProducto.ReadOnly = true;
                idProducto.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                idProducto.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtListaProductos.Columns.Add(idProducto);

                DataGridViewTextBoxColumn descripcion = new DataGridViewTextBoxColumn();
                descripcion.Name = "descripcion";
                descripcion.DataPropertyName = "descripcion";
                descripcion.HeaderText = "Descripción";
                descripcion.Width = 253;
                descripcion.ReadOnly = true;
                descripcion.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                descripcion.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtListaProductos.Columns.Add(descripcion);

                DataGridViewTextBoxColumn costo = new DataGridViewTextBoxColumn();
                costo.Name = "costo";
                costo.DataPropertyName = "costo";
                costo.HeaderText = "Costo";
                costo.Width = 80;
                costo.ReadOnly = true;
                costo.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                costo.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
               
                dtListaProductos.Columns.Add(costo);

                DataGridViewTextBoxColumn precioVenta = new DataGridViewTextBoxColumn();
                precioVenta.Name = "precioVenta";
                precioVenta.DataPropertyName = "precioVenta";
                precioVenta.HeaderText = "Precio Venta";
                precioVenta.Width = 80;
                precioVenta.ReadOnly = true;
                precioVenta.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                precioVenta.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
              
                dtListaProductos.Columns.Add(precioVenta);

                DataGridViewTextBoxColumn stock = new DataGridViewTextBoxColumn();
                stock.Name = "stock";
                stock.DataPropertyName = "stock";
                stock.HeaderText = "Stock";
                stock.Width = 80;
                stock.ReadOnly = true;
                stock.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                stock.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtListaProductos.Columns.Add(stock);

                DataGridViewTextBoxColumn idUsuario = new DataGridViewTextBoxColumn();
                idUsuario.Name = "idUsuario";
                idUsuario.DataPropertyName = "idUsuario";
                idUsuario.HeaderText = "Nro Usuario";
                idUsuario.Width = 60;
                idUsuario.ReadOnly = true;
                idUsuario.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                idUsuario.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtListaProductos.Columns.Add(idUsuario);

                DataGridViewTextBoxColumn nombreUsuario = new DataGridViewTextBoxColumn();
                nombreUsuario.Name = "nombreUsuario";
                nombreUsuario.DataPropertyName = "nombreUsuario";
                nombreUsuario.HeaderText = "Usuario";
                nombreUsuario.Width = 120;
                nombreUsuario.ReadOnly = true;
                nombreUsuario.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                nombreUsuario.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtListaProductos.Columns.Add(nombreUsuario);

                dtListaProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            }
            catch (Exception ex)
            {
                Mensajes.MensajeError(ex);
            }
        }

        private void CargarDatos(Producto prod)
        {
            try
            {
                prod.Descripcion = txtDescripcion.Text;
                prod.Costo = Convert.ToDecimal(txtCosto.Text);
                prod.PrecioVenta = Convert.ToDecimal(txtprecioventa.Text);
                prod.Stock = Convert.ToInt32(txtstock.Text);
                prod.IdUsuario = Convert.ToInt32(cmbUsuario.SelectedValue);
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
                if (txtDescripcion.Text == "")
                {
                    Mensajes.MostrarMensaje("Falta ingresar una descripción", "Faltan Datos", MessageBoxIcon.Warning, MessageBoxButtons.OK);
                    correcto = false;
                    return correcto;
                    txtDescripcion.Focus();
                }

                if (txtCosto.Text == "")
                {
                    Mensajes.MostrarMensaje("Falta ingresar el costo", "Faltan Datos", MessageBoxIcon.Warning, MessageBoxButtons.OK);
                    correcto = false;
                    return correcto;
                    txtCosto.Focus();
                }

                if (txtprecioventa.Text == "")
                {
                    Mensajes.MostrarMensaje("No ingreso el precio de venta", "Faltan Datos", MessageBoxIcon.Warning, MessageBoxButtons.OK);
                    correcto = false;
                    return correcto;
                    txtprecioventa.Focus();
                }

                if (txtstock.Text == "")
                {
                    Mensajes.MostrarMensaje("No ingreso el stock del producto", "Faltan Datos", MessageBoxIcon.Warning, MessageBoxButtons.OK);
                    correcto = false;
                    return correcto;
                    txtstock.Focus();
                }

                if (cmbUsuario.SelectedIndex == 0)
                {
                    Mensajes.MostrarMensaje("No seleccionó un usuario", "Faltan Datos", MessageBoxIcon.Warning, MessageBoxButtons.OK);
                    correcto = false;
                    return correcto;
                    cmbUsuario.Focus();
                }

            }
            catch (Exception ex)
            {
                Mensajes.MensajeError(ex);
            }

            return correcto;
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

        private void btnGuardar_Click(object sender, EventArgs e)
        {
          
            try
            {
                if (validar() == true)
                {
                    CargarDatos(prod);
                    if (editando == false)
                    {
                        ProductoNegocio.InsertarProducto(prod);
                       Mensajes.MostrarMensaje("El producto se Registró Correctamente","Exito", MessageBoxIcon.Information, MessageBoxButtons.OK);
                    }
                    else
                    {
                        ProductoNegocio.ModificarProducto(prod);
                        Mensajes.MostrarMensaje("El producto se Modificó Correctamente", "Exito", MessageBoxIcon.Information, MessageBoxButtons.OK);
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

        private void txtCosto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != ',' && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void txtprecioventa_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != ',' && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void txtstock_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != ' ' && e.KeyChar != ' ' && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void dtListaProductos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                try
                {
                    DataGridViewRow selectedRow = dtListaProductos.Rows[e.RowIndex];

                    prod.IdProducto = Convert.ToInt32(selectedRow.Cells["idProducto"].Value.ToString());
                    prod.Descripcion = selectedRow.Cells["descripcion"].Value.ToString();
                    prod.Costo = Convert.ToDecimal(selectedRow.Cells["costo"].Value.ToString());
                    prod.PrecioVenta = Convert.ToDecimal(selectedRow.Cells["precioVenta"].Value.ToString());
                    prod.Stock = Convert.ToInt32(selectedRow.Cells["stock"].Value.ToString());
                    prod.IdUsuario = Convert.ToInt32(selectedRow.Cells["idUsuario"].Value);

                    txtDescripcion.Text = prod.Descripcion;
                    txtCosto.Text = Convert.ToInt32(prod.Costo).ToString("N2");
                    txtprecioventa.Text = Convert.ToDecimal(prod.PrecioVenta).ToString("N2");
                    txtstock.Text = Convert.ToInt32(prod.Stock).ToString("N2");
                    cmbUsuario.SelectedValue = Convert.ToInt32(prod.IdUsuario);
                    editando = true;
                }
                catch (Exception ex)
                {
                    Mensajes.MensajeError(ex);
                }
            
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtListaProductos.RowCount > 0)
                {
                    int _idproducto = 0;
                    if (dtListaProductos.CurrentRow != null)
                    {
                        _idproducto = ((Producto)dtListaProductos.CurrentRow.DataBoundItem).IdProducto;
                    }

                    Producto _prod = ProductoNegocio.ObtenerProductoxId (_idproducto);

                    if (_prod != null)
                    {
                        DialogResult resultado = MessageBox.Show("¿Confirma Eliminar el Producto Nro.: " + _prod.IdProducto + "?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    
                        if (resultado == DialogResult.Yes)
                        {
                            ProductoNegocio.EliminarProducto(_prod.IdProducto);
                            Mensajes.MostrarMensaje("El producto " + _prod.IdProducto + " se eliminó correctamente", "Exito", MessageBoxIcon.Information, MessageBoxButtons.OK);
                           
                            ListarProductos();
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
            }
            catch (Exception ex)
            {
              Mensajes.MensajeError(ex);
            }
        }

        private void CargarProducto_Frm_MouseMove(object sender, MouseEventArgs e)
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

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool HabilitarCampos(bool valor)
        {
            txtDescripcion.Enabled = valor;
            txtCosto.Enabled = valor;
            txtprecioventa.Enabled = valor;
            txtstock.Enabled = valor;
            cmbUsuario.Enabled = valor;
            dtListaProductos.Enabled = valor;
            return valor;
        }

        private void HabilitarBotones()
        {
            BtnNuevo.Enabled = false;
            btnGuardar.Enabled = true;
            btnEliminar.Enabled = true;
            BtnCancelar.Enabled = true; 
        }

        private void BtnNuevo_Click(object sender, EventArgs e)
        {
            HabilitarCampos(true);
            InicializarFormulario();
            HabilitarBotones();
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            Limpiar();
            InicializarFormulario();
            HabilitarCampos(false);
        }

        private void cmbUsuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void BtnInformacion_Click(object sender, EventArgs e)
        {
            Mensajes.MostrarMensaje("El botón Nuevo Desbloquea los campos, la grilla y los botones Guardar, Modificar, Eliminar y Cancelar. \n\n" +
                "Para modificar un producto se debe presionar doble clic en la grilla sobre la fila del producto en cuestión \n\n" +
                "Debe presionar el botón nuevo para cada acción que quiera realizar ( Guardar, Modificar, Eliminar o Cancelar )"
                , "Información", MessageBoxIcon.Information, MessageBoxButtons.OK);   
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string valor = "";
                valor = txtBuscar.Text;
                this.dtListaProductos.DataSource = ProductoNegocio.BuscarProducto(valor);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
