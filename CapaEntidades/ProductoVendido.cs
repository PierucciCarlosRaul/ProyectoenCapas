using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_de_Gestion.Modelo
{
    public class ProductoVendido : Producto
    {
        private int idProdVendido;
        private int idVenta;

        public ProductoVendido(int idProducto, string descripcion, decimal costo, decimal precioVenta, int stock, int idUsuario, string nombreUsuario, int idProdVendido, int idVenta)
        : base(idProducto, descripcion, costo, precioVenta, stock, idUsuario, nombreUsuario)
        {
            this.idProdVendido = idProdVendido;
            this.idVenta = idVenta;
        }

        public ProductoVendido()
        { 
        
        }

        public int IdProdVendido
        {
            get
            {
                return idProdVendido;
            }

            set
            {
                idProdVendido = value;
            }
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

        public string toStringProductoVendido()
        {
            return " Cod.Prod.Vendido: " + idProdVendido + " Cod.Producto: " + idProducto + " Stock: " + stock + " Nro.Venta: " + idVenta;
        }
    }
}
