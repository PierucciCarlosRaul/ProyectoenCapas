using Sistema_de_Gestion.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;


namespace CapaNegocio
{
    public static class ProductoNegocio
    {
        public static void InsertarProducto(Producto prod)
        {
            ProductosDatos.InsertarProducto(prod);
        }
        public static void ModificarProducto(Producto prod)
        {
            ProductosDatos.ModificarProducto(prod);
        }
        public static void EliminarProducto(int IdProducto)
        {
            ProductosDatos.EliminarProducto(IdProducto);
        }
        public static List<Producto> listarProductos()
        {
            return ProductosDatos.listarProductos();
        }
        public static List<Usuario>CargarComboUsuarios()
        {
            return ProductosDatos.CargarComboUsuarios();
        }

        public static Producto ObtenerProductoxId(int idproducto)
        {
            return ProductosDatos.ObtenerProductoxId(idproducto);
        }

        public static List<Producto> BuscarProducto(String valor)
        {
            return ProductosDatos.BuscarProducto(valor);
        }


    }
}
