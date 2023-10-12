using CapaNegocio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sistema_de_Gestion.Modelo;

namespace AppProductosWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        [HttpGet(Name = "GetProductos")]
        public IEnumerable<Producto> Get()
        {
            return ProductoNegocio.listarProductos().ToArray();
        }

        [HttpDelete(Name = "EliminarProductos")]
        public void EliminarProductos([FromBody] int IdProducto)
        {
            ProductoNegocio.EliminarProducto(IdProducto);
        }

        [HttpPut(Name = "ModificarProductos")]
        public void ModificarProducto([FromBody] Producto prod)
        {
            ProductoNegocio.ModificarProducto(prod);
        }

        [HttpPost(Name = "RegistrarProducto")]
        public void InsertarProducto([FromBody] Producto prod)
        {
            ProductoNegocio.InsertarProducto(prod);
        }
    }
}
