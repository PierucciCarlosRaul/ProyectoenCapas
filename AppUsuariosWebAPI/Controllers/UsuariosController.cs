using CapaNegocio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sistema_de_Gestion.Modelo;



namespace AppUsuariosWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        [HttpGet(Name = "GetUsuarios")]
        public IEnumerable<Usuario> Get()
        {
            return UsuarioNegocio.ListarUsuarios().ToArray();
        }

        [HttpDelete(Name = "EliminarUsuarios")]
        public void EliminarUsuario([FromBody] int IdUsuario) 
        {
           UsuarioNegocio.EliminarUsuario(IdUsuario);
        }

        [HttpPut(Name = "ModificarUsuarios")]
        public void ModificarUsuario([FromBody] Usuario usu)
        {
            UsuarioNegocio.ModificarUsuario(usu);
        }

        [HttpPost(Name = "RegistrarUsuarios")]
        public void InsertarUsuario([FromBody] Usuario usu)
        {
            UsuarioNegocio.InsertarUsuario(usu);
        }



    }

}
