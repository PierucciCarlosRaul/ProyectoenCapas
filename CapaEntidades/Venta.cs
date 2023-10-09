using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_de_Gestion.Modelo
{
    public class Venta
    {
        private int idVenta;
        private string comentario;
        private int idUsuario;
        public List<ProductoVendido> listaDetalleProductoVendido { get; set; }

        public Venta(int idVenta, string comentario, int idUsuario)
        {
            this.idVenta = idVenta;
            this.comentario = comentario;
            this.idUsuario = idUsuario;
        }

        public Venta()
        {
            this.idVenta = 0;
            this.comentario = "";
            this.idUsuario = 0;
        }

        public int IdVenta
        {
            get
            {
                return idVenta;
            }

            set
            {
                idVenta = value;
            }
        }

        public string Comentario
        {
            get
            {
                return comentario;
            }

            set
            {
                comentario = value;
            }
        }

        public int IdUsuario
        {
            get
            {
                return idUsuario;
            }

            set
            {
                idUsuario = value;
            }
        }

        public string toStringVenta()
        {
            return " Nro.Venta: " + idVenta + " Observación: " + comentario + " Usuario: " + idUsuario;
        }


    }
}
