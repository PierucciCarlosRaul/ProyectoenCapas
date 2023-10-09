using CapaNegocio;
using Sistema_de_Gestion;
using Sistema_de_Gestion.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_de_Gestion.Vista
{
    public partial class ConsultaVentas_Frm : Form
    {
        private BindingSource _binding_ventas;

        private int idventa;

        public int Idventa
        {
            get
            {
                return idventa;
            }

            set
            {
                idventa = value;
            }
        }

        public ConsultaVentas_Frm()
        {
            InitializeComponent();

            _binding_ventas = new BindingSource();
            dtListadoVentas.DataSource = _binding_ventas;
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

        private void ConsultaVentas_Frm_MouseMove(object sender, MouseEventArgs e)
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

        private void ListarVentas()
        {
            try
            {
                List<VentasRealizadas> listaVentas = VentaNegocio.listarVentas();
                dtListadoVentas.DataSource = listaVentas;
                this.Refresh();
            }

            catch (Exception ex)
            {
                Mensajes.MensajeError(ex);
            }
        }

        private void ConsultaVentas_Frm_Load(object sender, EventArgs e)
        {
            SetGrillaProdVendidos();
            ListarVentas();
        }

        private void SetGrillaProdVendidos()
        {
            try
            {
                dtListadoVentas.AutoGenerateColumns = false;
                dtListadoVentas.Columns.Clear();
                dtListadoVentas.ReadOnly = false;

                DataGridViewTextBoxColumn idventa = new DataGridViewTextBoxColumn();
                idventa.Name = "Id_Venta";
                idventa.DataPropertyName = "Id_Venta";
                idventa.HeaderText = "Cod.Venta";
                idventa.Width = 70;
                idventa.ReadOnly = false;
                idventa.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                idventa.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtListadoVentas.Columns.Add(idventa);

                DataGridViewTextBoxColumn codprods = new DataGridViewTextBoxColumn();
                codprods.Name = "Id_Producto";
                codprods.DataPropertyName = "Id_Producto";
                codprods.HeaderText = "Cod. Producto";
                codprods.Width = 70;
                codprods.ReadOnly = false;
                codprods.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                codprods.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtListadoVentas.Columns.Add(codprods);

                DataGridViewTextBoxColumn descripcion = new DataGridViewTextBoxColumn();
                descripcion.Name = "Descripciones";
                descripcion.DataPropertyName = "Descripciones";
                descripcion.HeaderText = "Descripción";
                descripcion.Width = 223;
                descripcion.ReadOnly = true;
                descripcion.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                descripcion.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtListadoVentas.Columns.Add(descripcion);

                DataGridViewTextBoxColumn cantidad = new DataGridViewTextBoxColumn();
                cantidad.Name = "Stock";
                cantidad.DataPropertyName = "Stock";
                cantidad.HeaderText = "Cantidad";
                cantidad.Width = 70;
                cantidad.ReadOnly = false;
                cantidad.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                cantidad.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtListadoVentas.Columns.Add(cantidad);

                DataGridViewTextBoxColumn precioVenta = new DataGridViewTextBoxColumn();
                precioVenta.Name = "PrecioVenta";
                precioVenta.DataPropertyName = "PrecioVenta";
                precioVenta.HeaderText = "Precio Venta";
                precioVenta.Width = 100;
                precioVenta.ReadOnly = false;
                precioVenta.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                precioVenta.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtListadoVentas.Columns.Add(precioVenta);

                DataGridViewTextBoxColumn comenta = new DataGridViewTextBoxColumn();
                comenta.Name = "Comentario";
                comenta.DataPropertyName = "Comentario";
                comenta.HeaderText = "Comentario";
                comenta.Width = 120;
                comenta.ReadOnly = false;
                comenta.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                comenta.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtListadoVentas.Columns.Add(comenta);

                DataGridViewTextBoxColumn nomusu = new DataGridViewTextBoxColumn();
                nomusu.Name = "nombreCompleto";
                nomusu.DataPropertyName = "nombreCompleto";
                nomusu.HeaderText = "Usuario";
                nomusu.Width = 120;
                nomusu.ReadOnly = false;
                nomusu.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                nomusu.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dtListadoVentas.Columns.Add(nomusu);


                dtListadoVentas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            }
            catch (Exception ex)
            {
                Mensajes.MensajeError(ex);
            }
        }

        private void dtListadoVentas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                try
                {
                    DataGridViewRow selectedRow = dtListadoVentas.Rows[e.RowIndex];

                    Idventa = Convert.ToInt32(selectedRow.Cells["Id_Venta"].Value);

                    if (idventa > 0)
                    {
                        DialogResult = DialogResult.OK;
                    }

                }
                catch (Exception ex)
                {
                    Mensajes.MensajeError(ex);
                }

            }

        }
    }
}
