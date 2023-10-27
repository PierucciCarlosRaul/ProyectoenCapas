using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sistema_de_Gestion.Modelo;
using CapaNegocio;

namespace AppInicioSesionWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InicioSesionController : ControllerBase
    {

        [HttpGet(Name = "GetInicioSesion")]
        public IActionResult TraerUsuarioxLogin(string usuario, string password)
        {
            var usuarioEncontrado = UsuarioNegocio.TraerUsuarioxLogin(usuario, password);

            if (usuarioEncontrado.NombreCompleto != "")
            {
                return Ok("Usuario: " + usuarioEncontrado.NombreCompleto);
            }
            else
            {
                return NotFound(new { message = "Usuario no encontrado. Usuario o Contraseña Inválido" });
            }
        }

    }
}
