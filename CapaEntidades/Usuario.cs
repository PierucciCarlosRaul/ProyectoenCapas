using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sistema_de_Gestion.Modelo
{
    public class Usuario
    {
        private int idUsuario;
        private string nombre;
        private string apellido;
        private string nombreUsuario;
        private string password;
        private string mail;
        private string nombreCompleto;
        private string usuario;


        public Usuario(int idUsuario, string nombre, string apellido, string nombreUsuario, string contraseña, string mail, string nombreCompleto, string usuario)
        {
            this.idUsuario = idUsuario;
            this.nombre = nombre;
            this.apellido = apellido;
            this.nombreUsuario = nombreUsuario;
            this.password = contraseña;
            this.mail = mail;
            this.nombreCompleto = nombreCompleto;
            this.usuario = usuario;
        }
        public Usuario()
        {
            this.idUsuario = 0;
            this.nombre = "";
            this.apellido = "";
            this.nombreUsuario = "";
            this.password = "";
            this.mail = "";
            this.nombreCompleto = "";
            this.usuario = "";
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

        public string Nombre
        {
            get
            {
                return nombre;
            }

            set
            {
                nombre = value;
            }
        }

        public string Apellido
        {
            get
            {
                return apellido;
            }

            set
            {
                apellido = value;
            }
        }

        public string NombreUsuario
        {
            get
            {
                return (nombre + " " + apellido);
            }
        }

        public string Password
        {
            get
            {
                return password;
            }

            set
            {
                password = value;
            }
        }

        public string Mail
        {
            get
            {
                return mail;
            }

            set
            {
                mail = value;
            }
        }

        public string NombreCompleto
        {
            get
            {
                return nombreCompleto;
            }

            set
            {
                nombreCompleto = value;
            }
        }

        public string UsuarioLogin
        {
            get
            {
                return usuario;
            }

            set
            {
                usuario = value;
            }
        }

        public string toStringUsuario()
        {
            return " Cod.Usuario: " + idUsuario + " Nombre: " + nombre + " Apellido: " + apellido + " Nombre Completo Usuario " + nombreUsuario + " Contraseña: " + password + " Correo Electonico: " + mail;
        }

    }
}
