using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_de_Gestion.Modelo
{
    public class Producto
    {
        protected int idProducto;
        protected string descripcion;
        protected decimal costo;
        protected decimal precioVenta;
        protected int stock;
        protected int idUsuario;
        protected string nombreUsuario;

        public Producto(int idProducto, string descripcion, decimal costo, decimal precioVenta, int stock, int idUsuario, string nombreUsuario)
        {
            this.idProducto = idProducto;
            this.descripcion = descripcion;
            this.costo = costo;
            this.precioVenta = precioVenta;
            this.stock = stock;
            this.idUsuario = idUsuario;
            this.nombreUsuario = nombreUsuario;
          
        }

        public Producto()
        {
            idProducto = 0;
            descripcion = "";
            costo = 0;
            precioVenta = 0;
            stock = 0;
            idUsuario = 0;
            nombreUsuario = "";
            
        }


        public int IdProducto
        {
            get
            {
                return idProducto;
            }

            set
            {
                idProducto = value;
            }
        }

        public string Descripcion
        {
            get
            {
                return descripcion;
            }

            set
            {
                descripcion = value;
            }
        }

        public decimal Costo
        {
            get
            {
                return costo;
            }

            set
            {
                costo = value;
            }
        }

        public decimal PrecioVenta
        {
            get
            {
                return precioVenta;
            }

            set
            {
                precioVenta = value;
            }
        }

        public int Stock
        {
            get
            {
                return stock;
            }

            set
            {
                stock = value;
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

        public string NombreUsuario
        {
            get
            {
                return nombreUsuario;
            }

            set
            {
                nombreUsuario = value;
            }
        }

        public string toStringProducto()
        {
            return " Cod.Producto: " + idProducto + " Descripcion: " + descripcion + " Costo: " + costo + " Precio Venta:" + precioVenta + " Stock: " + stock + " Usuario: " + idUsuario;
        }

    }
}
