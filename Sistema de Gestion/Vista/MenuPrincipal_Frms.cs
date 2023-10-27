using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_de_Gestion.Vista
{
    public partial class MenuPrincipal_Frms : Form
    {
       

        public MenuPrincipal_Frms()
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

        private void BtnCargarProducto_Click(object sender, EventArgs e)
        {
            CargarProducto_Frm frmCargaProducto = new CargarProducto_Frm();
            frmCargaProducto.Show();
            frmCargaProducto.BringToFront();
        }

        private void btnCargarUsuario_Click(object sender, EventArgs e)
        {
            Carga_de_Usuarios_Frm frmCargaUsuarios = new Carga_de_Usuarios_Frm();
            frmCargaUsuarios.Show();
            frmCargaUsuarios.BringToFront();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void BtnCerrarSistema_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MenuPrincipal_Frms_Load(object sender, EventArgs e)
        {
            
        }

        private void btnMaximizar_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
            }
        }

        private void pnlSuperior_MouseMove(object sender, MouseEventArgs e)
        {
            WinAPI.ReleaseCapture();
            WinAPI.SendMessage(this.Handle, 0x112, 0xF012, 0);
        }

        private void picLogo_MouseMove(object sender, MouseEventArgs e)
        {
            WinAPI.ReleaseCapture();
            WinAPI.SendMessage(this.Handle, 0x112, 0xF012, 0);
        }

        private void lbltitulo_MouseMove(object sender, MouseEventArgs e)
        {
            WinAPI.ReleaseCapture();
            WinAPI.SendMessage(this.Handle, 0x112, 0xF012, 0);
        }

        private void pnlOpciones_MouseMove(object sender, MouseEventArgs e)
        {
            WinAPI.ReleaseCapture();
            WinAPI.SendMessage(this.Handle, 0x112, 0xF012, 0);
        }

        private void btnCargarVenta_Click(object sender, EventArgs e)
        {
            VentasAbm_Frm frmventas = new VentasAbm_Frm();
            frmventas.Show();
            frmventas.BringToFront();
        }
    }
}
