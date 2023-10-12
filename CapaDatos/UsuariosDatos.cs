using Sistema_de_Gestion.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public static class UsuariosDatos
    {
        private static readonly string connectionsting = @"Server=DESKTOP-QL4OJD8\SQLEXPRESS;DataBase=SistemaGestion;Trusted_Connection=True";
        public static void InsertarUsuario(Usuario usu)
        {
           
            string _sente = " INSERT INTO Usuario (Nombre, Apellido, NombreUsuario, Contraseña, Mail) " +
                            " VALUES (@Nombre, @Apellido, @NombreUsuario, @Contraseña, @Mail) ";
            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionsting))
                {
                    conexion.Open();
                    using (SqlCommand command = new SqlCommand(_sente, conexion))
                    {
                        command.Parameters.Add(new SqlParameter("Nombre", SqlDbType.VarChar) { Value = usu.Nombre });
                        command.Parameters.Add(new SqlParameter("Apellido", SqlDbType.VarChar) { Value = usu.Apellido });
                        command.Parameters.Add(new SqlParameter("NombreUsuario", SqlDbType.VarChar) { Value = usu.UsuarioLogin });
                        command.Parameters.Add(new SqlParameter("Contraseña", SqlDbType.VarChar) { Value = usu.Password });
                        command.Parameters.Add(new SqlParameter("Mail", SqlDbType.VarChar) { Value = usu.Mail });
                        command.ExecuteNonQuery();
                    }
                    conexion.Close();
                }
            }
            catch (Exception ex)
            {
                Mensajes.MensajeError(ex);
            }

        }

        public static void ModificarUsuario(Usuario usu)
        {
           
            string _sente = " UPDATE Usuario SET " +
                            " Nombre = @Nombre, " +
                            " Apellido = @Apellido, " +
                            " NombreUsuario = @NombreUsuario, " +
                            " Contraseña = @Contraseña, " +
                            " Mail = @Mail " +
                            " WHERE IdUsuario = @IdUsuario ";
            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionsting))
                {
                    conexion.Open();
                    using (SqlCommand command = new SqlCommand(_sente, conexion))
                    {
                        command.Parameters.Add(new SqlParameter("Nombre", SqlDbType.VarChar) { Value = usu.Nombre });
                        command.Parameters.Add(new SqlParameter("Apellido", SqlDbType.VarChar) { Value = usu.Apellido });
                        command.Parameters.Add(new SqlParameter("NombreUsuario", SqlDbType.VarChar) { Value = usu.UsuarioLogin });
                        command.Parameters.Add(new SqlParameter("Contraseña", SqlDbType.VarChar) { Value = usu.Password });
                        command.Parameters.Add(new SqlParameter("Mail", SqlDbType.VarChar) { Value = usu.Mail });
                        command.Parameters.Add(new SqlParameter("IdUsuario", SqlDbType.BigInt) { Value = usu.IdUsuario });
                        command.ExecuteNonQuery();
                    }
                    conexion.Close();
                }
            }
            catch (Exception ex)
            {
                Mensajes.MensajeError(ex);
            }
        }

        public static void EliminarUsuario(int IdUsuario)
        {
            
            string _sente = " DELETE FROM Usuario WHERE IdUsuario = @IdUsuario ";
            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionsting))
                {
                    conexion.Open();
                    using (SqlCommand command = new SqlCommand(_sente, conexion))
                    {
                        command.Parameters.Add(new SqlParameter("IdUsuario", SqlDbType.Int) { Value = IdUsuario });
                        command.ExecuteNonQuery();
                    }
                    conexion.Close();
                }
            }
            catch (Exception ex)
            {
                Mensajes.MensajeError(ex);
            }
        }

        public static List<Usuario> ListarUsuarios()
        {
            List<Usuario> _lista = new List<Usuario>();
           
            string _sente = "SELECT IdUsuario, Nombre, Apellido, NombreUsuario, Contraseña, Mail FROM Usuario ";
            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionsting))
                {
                    conexion.Open();
                    using (SqlCommand command = new SqlCommand(_sente, conexion))
                    {
                        using (SqlDataReader dr = command.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    var usu = new Usuario();
                                    usu.IdUsuario = dr["IdUsuario"] is DBNull ? 0 : Convert.ToInt32(dr["IdUsuario"]);
                                    usu.Nombre = dr["Nombre"] is DBNull ? "" : dr["Nombre"].ToString();
                                    usu.Apellido = dr["Apellido"] is DBNull ? "" : dr["Apellido"].ToString();
                                    usu.UsuarioLogin = dr["NombreUsuario"] is DBNull ? "" : dr["NombreUsuario"].ToString();
                                    usu.Password = dr["Contraseña"] is DBNull ? "" : dr["Contraseña"].ToString();
                                    usu.Mail = dr["Mail"] is DBNull ? "" : dr["Mail"].ToString();
                                  
                                    _lista.Add(usu);
                                }
                            }
                            dr.Close();
                        }
                    }
                    conexion.Close();
      
                }

            }
            catch (Exception ex)
            {
                Mensajes.MensajeError(ex);
            }
            return _lista;
        }

        public static Usuario ObtenerUsuarioxId(int idusuario)
        {
            Usuario usu = new Usuario();
           
            string _sente = "SELECT IdUsuario, Nombre, Apellido, NombreUsuario, Contraseña, Mail FROM Usuario WHERE IdUsuario = @IdUsuario ";
            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionsting))
                {
                    conexion.Open();
                    using (SqlCommand command = new SqlCommand(_sente, conexion))
                    {
                        command.Parameters.Add(new SqlParameter("IdUsuario", SqlDbType.Int) { Value = idusuario });

                        using (SqlDataReader dr = command.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                if (dr.Read())
                                {
                                    usu.IdUsuario = dr["IdUsuario"] is DBNull ? 0 : Convert.ToInt32(dr["IdUsuario"]);
                                    usu.Nombre = dr["Nombre"] is DBNull ? "" : dr["Nombre"].ToString();
                                    usu.Apellido = dr["Apellido"] is DBNull ? "" : dr["Apellido"].ToString();
                                    usu.UsuarioLogin = dr["NombreUsuario"] is DBNull ? "" : dr["NombreUsuario"].ToString();
                                    usu.Password = dr["Contraseña"] is DBNull ? "" : dr["Contraseña"].ToString();
                                    usu.Mail = dr["Mail"] is DBNull ? "" : dr["Mail"].ToString();

                                }
                            }
                            dr.Close();
                        }
                    }
                    conexion.Close();
                }

            }
            catch (Exception ex)
            {
                Mensajes.MensajeError(ex);
            }
            return usu;
        }

    }
}

