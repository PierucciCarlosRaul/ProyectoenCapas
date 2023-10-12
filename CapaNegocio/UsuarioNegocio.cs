using CapaDatos;
using Sistema_de_Gestion.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class UsuarioNegocio
    {
        public static void InsertarUsuario(Usuario usu)
        {
           UsuariosDatos.InsertarUsuario(usu);
        }

        public static void ModificarUsuario(Usuario usu)
        {
           UsuariosDatos.ModificarUsuario(usu);
        }

        public static void EliminarUsuario(int IdUsuario)
        {
            UsuariosDatos.EliminarUsuario(IdUsuario);
        }

        public static List<Usuario> ListarUsuarios()
        {
            return UsuariosDatos.ListarUsuarios();
        }

        public static Usuario ObtenerUsuarioxId(int idusuario)
        {
            return UsuariosDatos.ObtenerUsuarioxId(idusuario);
        }
    }
}
