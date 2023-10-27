using Sistema_de_Gestion.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaNegocio;
using System.Runtime.InteropServices;

namespace Sistema_de_Gestion.Vista
{
    public partial class Login : Form
    {
        Usuario usu = new Usuario();
        public Login()
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

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private bool validar()
        {
            bool correcto = true;
            try
            {
                if (txtusuario.Text == "")
                {

                    Mensajes.MostrarMensaje("Debe ingresar su usario", "Faltan Datos", MessageBoxIcon.Warning, MessageBoxButtons.OK);
                    correcto = false;
                    return correcto;
                   
                }

                if (txtcontrasena.Text == "")
                {
                    Mensajes.MostrarMensaje("Debe ingresar su contraseña", "Faltan Datos", MessageBoxIcon.Warning, MessageBoxButtons.OK);
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
            string usuario = "";
            string password = "";

            usuario = txtusuario.Text;
            password = txtcontrasena.Text;

            if (validar())
            {
                usu = UsuarioNegocio.Login(usuario, password);
                if (usu.IdUsuario != 0)
                {
                    MenuPrincipal_Frms menu = new MenuPrincipal_Frms();
                    menu.Show();
                    this.Hide();
                    
                }
                else
                {
                    Mensajes.MostrarMensaje("Usuario o contraseña invalidos","Error", MessageBoxIcon.Error,MessageBoxButtons.OK );
                }
            }
        }

        private void InicializarFormulario()
        {
            try
            {
                txtusuario.Text = "";
                txtcontrasena.Text = "";              
            }

            catch (Exception ex)
            {
                Mensajes.MensajeError(ex);
            }

        }

        private void Login_Load(object sender, EventArgs e)
        {
            try
            {
                InicializarFormulario();
                
            }

            catch (Exception ex)
            {
                Mensajes.MensajeError(ex);
            }
        }

        private void Login_MouseMove(object sender, MouseEventArgs e)
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
    }
}
