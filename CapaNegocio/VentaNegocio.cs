using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using Sistema_de_Gestion.Modelo;

namespace CapaNegocio
{
    public static class VentaNegocio
    {
        public static void InsertarVenta(Venta ven)
        {
           VentaDatos.InsertarVenta(ven);
        }

        public static void ModificarVenta(Venta ven)
        {
            VentaDatos.ModificarVenta(ven);
        }

        public static void EliminarVenta(int ven)
        {
            VentaDatos.EliminarVenta(ven);
        }

        public static Venta ObtenerVentaxId(int idventa)
        {
            return VentaDatos.ObtenerVentaxId(idventa);
        }

        public static List<ProductoVendido> listarProductoVendidos(int idventa)
        {
            return VentaDatos.listarProductoVendidos(idventa);
        }

        public static List<VentasRealizadas> listarVentas()
        {
            return VentaDatos.listarVentas();
        }



    }
}
