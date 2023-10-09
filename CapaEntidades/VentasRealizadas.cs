using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_de_Gestion.Modelo
{
    public class VentasRealizadas
    {
        public int Id_Venta { get; set; }
        public int Id_Producto { get; set; }
        public string Descripciones { get; set; }
        public int Stock { get; set; }
        public decimal PrecioVenta { get; set; }
        public string Comentario { get; set; }
        public string nombreCompleto { get; set; }

    }
}
