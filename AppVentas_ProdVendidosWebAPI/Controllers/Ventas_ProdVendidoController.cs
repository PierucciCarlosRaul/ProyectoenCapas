using CapaNegocio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sistema_de_Gestion.Modelo;

namespace AppVentas_ProdVendidosWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Ventas_ProdVendidoController : ControllerBase
    {
        [HttpGet(Name = "GetVentasRealizadas")]
        public IEnumerable<VentasRealizadas> Get()
        {
            return VentaNegocio.listarVentas().ToArray();
        }

        [HttpDelete(Name = "EliminarVenta")]
        public void EliminarVenta([FromBody] int Idventa)
        {
            VentaNegocio.EliminarVenta(Idventa);
        }

        [HttpPut(Name = "ModificarVenta")]
        public void ModificarVenta([FromBody] Venta venta)
        {
            VentaNegocio.ModificarVenta(venta);
        }

        [HttpPost(Name = "RegistrarVenta")]
        public void InsertarVenta([FromBody] Venta ven)
        {
            VentaNegocio.InsertarVenta(ven);
        }
    }
}
