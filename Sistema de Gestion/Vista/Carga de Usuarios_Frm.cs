
using CapaNegocio;
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

namespace Sistema_de_Gestion.Vista
{
    public partial class Carga_de_Usuarios_Frm : Form
    {
        private bool editando = false;
        Usuario usu = new Usuario();

        public Carga_de_Usuarios_Frm()
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

        private void Carga_de_Usuarios_Frm_Load(object sender, EventArgs e)
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
                txtnombre.Text = "";
                txtApellido.Text = "";
                txtNombreUsuario.Text = "";
                txtcorreo.Text = "";
                txtcontraseña.Text = "";
                ListarUsuarios();
                SetGrillaUsuarios();
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

        private void ListarUsuarios()
        {
            try
            {
                List<Usuario> listaUsuarios = UsuarioNegocio.ListarUsuarios();
                dtListaUsuarios.DataSource = listaUsuarios;
                this.Refresh();
            }

            catch (Exception ex)
            {
                Mensajes.MensajeError(ex);
            }
        }

        private void SetGrillaUsuarios()
        {
            try
            {
                dtListaUsuarios.AutoGenerateColumns = false;
                dtListaUsuarios.Columns.Clear();
                dtListaUsuarios.ReadOnly = false;

                DataGridViewTextBoxColumn idusu = new DataGridViewTextBoxColumn();
                idusu.Name = "idUsuario";
                idusu.DataPropertyName = "idUsuario";
                idusu.HeaderText = "Cod.Usuario";
                idusu.Width = 70;
                idusu.ReadOnly = true;
                idusu.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                idusu.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtListaUsuarios.Columns.Add(idusu);

                DataGridViewTextBoxColumn usunombre = new DataGridViewTextBoxColumn();
                usunombre.Name = "nombre";
                usunombre.DataPropertyName = "nombre";
                usunombre.HeaderText = "Nombre";
                usunombre.Width = 127;
                usunombre.ReadOnly = true;
                usunombre.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                usunombre.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtListaUsuarios.Columns.Add(usunombre);

                DataGridViewTextBoxColumn usuapellido = new DataGridViewTextBoxColumn();
                usuapellido.Name = "apellido";
                usuapellido.DataPropertyName = "apellido";
                usuapellido.HeaderText = "Apellido";
                usuapellido.Width = 126;
                usuapellido.ReadOnly = true;
                usuapellido.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                usuapellido.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtListaUsuarios.Columns.Add(usuapellido);

                DataGridViewTextBoxColumn usunomcompleto = new DataGridViewTextBoxColumn();
                usunomcompleto.Name = "UsuarioLogin";
                usunomcompleto.DataPropertyName = "UsuarioLogin";
                usunomcompleto.HeaderText = "Nombre Usuario";
                usunomcompleto.Width = 150;
                usunomcompleto.ReadOnly = true;
                usunomcompleto.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                usunomcompleto.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtListaUsuarios.Columns.Add(usunomcompleto);

                DataGridViewTextBoxColumn pass = new DataGridViewTextBoxColumn();
                pass.Name = "password";
                pass.DataPropertyName = "password";
                pass.HeaderText = "Contraseña";
                pass.Width = 150;
                pass.ReadOnly = true;
                pass.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                pass.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtListaUsuarios.Columns.Add(pass);

                DataGridViewTextBoxColumn email = new DataGridViewTextBoxColumn();
                email.Name = "mail";
                email.DataPropertyName = "mail";
                email.HeaderText = "Email";
                email.Width = 150;
                email.ReadOnly = true;
                email.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                email.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtListaUsuarios.Columns.Add(email);

                dtListaUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            }
            catch (Exception ex)
            {
                Mensajes.MensajeError(ex);
            }
        }

        private void CargarDatos(Usuario usu)
        {
            try
            {
                usu.Nombre = txtnombre.Text;
                usu.Apellido = txtApellido.Text;
                usu.UsuarioLogin = txtNombreUsuario.Text;
                usu.Password = txtcontraseña.Text;
                usu.Mail = txtcorreo.Text;
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
                if (txtnombre.Text == "")
                {

                    Mensajes.MostrarMensaje("Debe ingresar un nombre", "Faltan Datos", MessageBoxIcon.Warning, MessageBoxButtons.OK);
                    correcto = false;
                    return correcto;
                    txtnombre.Focus();
                }

                if (txtApellido.Text == "")
                {
                    Mensajes.MostrarMensaje("Debe ingresar su apellido", "Faltan Datos", MessageBoxIcon.Warning, MessageBoxButtons.OK);
                    correcto = false;
                    return correcto;
                    txtApellido.Focus();
                }

                if (txtNombreUsuario.Text == "")
                {
                    Mensajes.MostrarMensaje("Debe ingresar su usuario de login", "Faltan Datos", MessageBoxIcon.Warning, MessageBoxButtons.OK);
                    correcto = false;
                    return correcto;
                    txtNombreUsuario.Focus();
                }

                if (txtcontraseña.Text == "")
                {

                    Mensajes.MostrarMensaje("Debe ingresar su contraseña", "Faltan Datos", MessageBoxIcon.Warning, MessageBoxButtons.OK);
                    correcto = false;
                    return correcto;
                    txtcontraseña.Focus();
                }

                if (txtcorreo.Text == "")
                {
                    Mensajes.MostrarMensaje("No ingreso su correo electrónico", "Faltan Datos", MessageBoxIcon.Warning, MessageBoxButtons.OK);
                    correcto = false;
                    return correcto;
                    txtcorreo.Focus();
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
            try
            {
                if (validar() == true)
                {
                    CargarDatos(usu);
                    if (editando == false)
                    {
                        UsuarioNegocio.InsertarUsuario(usu);
                        Mensajes.MostrarMensaje("El usuario se Registró Correctamente", "Exito", MessageBoxIcon.Information, MessageBoxButtons.OK);
                    }
                    else
                    {
                        UsuarioNegocio.ModificarUsuario(usu);
                        Mensajes.MostrarMensaje("El usuario se Modificó Correctamente", "Exito", MessageBoxIcon.Information, MessageBoxButtons.OK);
                    }

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

        private void dtListaUsuarios_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                try
                {
                    DataGridViewRow selectedRow = dtListaUsuarios.Rows[e.RowIndex];

                    usu.IdUsuario = Convert.ToInt32(selectedRow.Cells["idUsuario"].Value);
                    usu.Nombre = selectedRow.Cells["nombre"].Value.ToString();
                    usu.Apellido = selectedRow.Cells["apellido"].Value.ToString();
                    usu.UsuarioLogin = selectedRow.Cells["UsuarioLogin"].Value.ToString();
                    usu.Password = selectedRow.Cells["password"].Value.ToString();
                    usu.Mail = selectedRow.Cells["mail"].Value.ToString();

                    txtnombre.Text = usu.Nombre;
                    txtApellido.Text = usu.Apellido;
                    txtNombreUsuario.Text = usu.UsuarioLogin;
                    txtcontraseña.Text = usu.Password;
                    txtcorreo.Text = usu.Mail;
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
                if (dtListaUsuarios.RowCount > 0)
                {
                    int _idusuario = 0;
                    if (dtListaUsuarios.CurrentRow != null)
                    {
                        _idusuario = ((Usuario)dtListaUsuarios.CurrentRow.DataBoundItem).IdUsuario;
                    }

                    Usuario _usu = UsuarioNegocio.ObtenerUsuarioxId(_idusuario);

                    if (usu != null)
                    {
                        DialogResult resultado = MessageBox.Show("¿Confirma Eliminar el Usuario Nro.: " + _usu.IdUsuario + "?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (resultado == DialogResult.Yes)
                        {
                            UsuarioNegocio.EliminarUsuario(_usu);
                            Mensajes.MostrarMensaje("El Usuario " + _usu.IdUsuario + " se eliminó correctamente", "Exito", MessageBoxIcon.Information, MessageBoxButtons.OK);
                            
                            ListarUsuarios();
                            InicializarFormulario();
                            editando = false;
                            HabilitarCampos(false);
                        }
                    }
                    else
                    {
                        MessageBox.Show("El Usuario no se eliminó");
                    }
                }
            }
            catch (Exception ex)
            {
                Mensajes.MensajeError(ex);
            }
        }
      
        private void Carga_de_Usuarios_Frm_MouseMove(object sender, MouseEventArgs e)
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
            txtnombre.Enabled = valor;
            txtApellido.Enabled = valor;
            txtNombreUsuario.Enabled = valor;
            txtcontraseña.Enabled = valor;
            txtcorreo.Enabled = valor;
            dtListaUsuarios.Enabled = valor;
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
            InicializarFormulario();
            HabilitarCampos(false);
        }

        private void BtnInformacion_Click(object sender, EventArgs e)
        {
            Mensajes.MostrarMensaje("El botón Nuevo Desbloquea los campos, la grilla y los botones Guardar, Modificar, Eliminar y Cancelar. \n\n" +
              "Para modificar un usuario se debe presionar doble clic en la grilla sobre la fila del usuario en cuestión \n\n" +
              "Debe presionar el botón nuevo para cada acción que quiera realizar ( Guardar, Modificar, Eliminar o Cancelar )"
              , "Información", MessageBoxIcon.Information, MessageBoxButtons.OK);
        }


    }
}
