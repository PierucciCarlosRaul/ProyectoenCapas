using Sistema_de_Gestion.Modelo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CapaDatos
{
    public static class ProductosDatos
    {
        
        private static readonly string connectionsting = @"Server=DESKTOP-QL4OJD8\SQLEXPRESS;DataBase=SistemaGestion;Trusted_Connection=True";
        
        public static void InsertarProducto(Producto prod)
        {
       
            string _sente = " INSERT INTO Producto (Descripciones, Costo, PrecioVenta, stock, IdUsuario) " +
                            " VALUES (@Descripciones, @Costo, @PrecioVenta, @stock, @IdUsuario) ";
            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionsting))
                {
                    conexion.Open();
                    using (SqlCommand command = new SqlCommand(_sente, conexion))
                    {
                        command.Parameters.Add(new SqlParameter("Descripciones", SqlDbType.VarChar) { Value = prod.Descripcion });
                        command.Parameters.Add(new SqlParameter("Costo", SqlDbType.Decimal) { Value = prod.Costo });
                        command.Parameters.Add(new SqlParameter("PrecioVenta", SqlDbType.Decimal) { Value = prod.PrecioVenta });
                        command.Parameters.Add(new SqlParameter("stock", SqlDbType.Int) { Value = prod.Stock });
                        command.Parameters.Add(new SqlParameter("IdUsuario", SqlDbType.BigInt) { Value = prod.IdUsuario });
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

        public static void ModificarProducto(Producto prod)
        {
            
            string _sente = " UPDATE Producto SET " +
                            " Descripciones = @Descripciones, " +
                            " Costo = @Costo, " +
                            " PrecioVenta = @PrecioVenta, " +
                            " stock = @stock, " +
                            " IdUsuario = @IdUsuario " +
                            " WHERE Id = @Id ";
            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionsting))
                {
                    conexion.Open();
                    using (SqlCommand command = new SqlCommand(_sente, conexion))
                    {
                        command.Parameters.Add(new SqlParameter("Id", SqlDbType.Int) { Value = prod.IdProducto });
                        command.Parameters.Add(new SqlParameter("Descripciones", SqlDbType.VarChar) { Value = prod.Descripcion });
                        command.Parameters.Add(new SqlParameter("Costo", SqlDbType.Decimal) { Value = prod.Costo });
                        command.Parameters.Add(new SqlParameter("PrecioVenta", SqlDbType.Decimal) { Value = prod.PrecioVenta });
                        command.Parameters.Add(new SqlParameter("stock", SqlDbType.Int) { Value = prod.Stock });
                        command.Parameters.Add(new SqlParameter("IdUsuario", SqlDbType.BigInt) { Value = prod.IdUsuario });
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

        public static void EliminarProducto(int IdProducto)
        {
           
            string _sente = " DELETE FROM Producto WHERE Id = @Id ";
            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionsting))
                {
                    conexion.Open();
                    using (SqlCommand command = new SqlCommand(_sente, conexion))
                    {
                        command.Parameters.Add(new SqlParameter("Id", SqlDbType.Int) { Value = IdProducto });
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

        public static List<Producto> listarProductos()
        {
            List<Producto> _lista = new List<Producto>();
           
            string _sente = " SELECT p.Id, p.Descripciones, p.Costo, p.PrecioVenta, p.stock, p.IdUsuario ,u.Nombre + ' ' + u.Apellido 'Usuario' " +
                            " FROM Producto p JOIN Usuario u ON p.IdUsuario = u.IdUsuario ";
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
                                    var producto = new Producto();
                                    producto.IdProducto = dr["Id"] is DBNull ? 0 : Convert.ToInt32(dr["Id"]);
                                    producto.Descripcion = dr["Descripciones"] is DBNull ? "" : dr["Descripciones"].ToString();
                                    producto.Costo = dr["Costo"] is DBNull ? 0.0m : Convert.ToDecimal(dr["Costo"]);
                                    producto.PrecioVenta = dr["PrecioVenta"] is DBNull ? 0.0m : Convert.ToDecimal(dr["PrecioVenta"]);
                                    producto.Stock = dr["stock"] is DBNull ? 0 : Convert.ToInt32(dr["stock"]);
                                    producto.IdUsuario = dr["IdUsuario"] is DBNull ? 0 : Convert.ToInt32(dr["IdUsuario"]);
                                    producto.NombreUsuario = dr["Usuario"] is DBNull ? "" : dr["Usuario"].ToString();
                                    _lista.Add(producto);
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

        public static List<Usuario> CargarComboUsuarios()
        {
            List<Usuario> _lista = new List<Usuario>();

            string _sente = " SELECT  u.IdUsuario, u.Nombre + ' ' + u.Apellido NombreCompleto FROM Usuario u ";
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
                                    usu.NombreCompleto = dr["NombreCompleto"] is DBNull ? "" : dr["NombreCompleto"].ToString();
                                   
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

        public static Producto ObtenerProductoxId(int idproducto)
        {
            Producto producto = new Producto();
           
            string _sente = " SELECT  Id,Descripciones,Costo,PrecioVenta,stock,IdUsuario FROM Producto WHERE Id = @Id ";
            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionsting))
                {
                    conexion.Open();
                    using (SqlCommand command = new SqlCommand(_sente, conexion))
                    {
                        command.Parameters.Add(new SqlParameter("Id", SqlDbType.Int) { Value = idproducto });

                        using (SqlDataReader dr = command.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                if (dr.Read())
                                {
                                    producto.IdProducto = dr["Id"] is DBNull ? 0 : Convert.ToInt32(dr["Id"]);
                                    producto.Descripcion = dr["Descripciones"] is DBNull ? "" : dr["Descripciones"].ToString();
                                    producto.Costo = dr["Costo"] is DBNull ? 0.0m : Convert.ToDecimal(dr["Costo"]);
                                    producto.PrecioVenta = dr["PrecioVenta"] is DBNull ? 0.0m : Convert.ToDecimal(dr["PrecioVenta"]);
                                    producto.Stock = dr["stock"] is DBNull ? 0 : Convert.ToInt32(dr["stock"]);
                                    producto.IdUsuario = dr["IdUsuario"] is DBNull ? 0 : Convert.ToInt32(dr["IdUsuario"]);
                                  
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
            return producto;
        }

        public static List<Producto> BuscarProducto(String valor)
        {
            List<Producto> _lista = new List<Producto>();

            string _sente = " SELECT p.Id, p.Descripciones, p.Costo, p.PrecioVenta, p.stock, p.IdUsuario ,u.Nombre + ' ' + u.Apellido 'Usuario' " +
                            " FROM Producto p JOIN Usuario u ON p.IdUsuario = u.IdUsuario " +
                            " WHERE p.Id LIKE '%" + valor + "%' " +
                            " or upper(p.Descripciones) LIKE upper('%" + valor + "%') " +
                            " or upper(p.PrecioVenta) LIKE upper('%" + valor + "%') " +
                            " order by 1 ";
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
                                    var producto = new Producto();
                                    producto.IdProducto = dr["Id"] is DBNull ? 0 : Convert.ToInt32(dr["Id"]);
                                    producto.Descripcion = dr["Descripciones"] is DBNull ? "" : dr["Descripciones"].ToString();
                                    producto.Costo = dr["Costo"] is DBNull ? 0.0m : Convert.ToDecimal(dr["Costo"]);
                                    producto.PrecioVenta = dr["PrecioVenta"] is DBNull ? 0.0m : Convert.ToDecimal(dr["PrecioVenta"]);
                                    producto.Stock = dr["stock"] is DBNull ? 0 : Convert.ToInt32(dr["stock"]);
                                    producto.IdUsuario = dr["IdUsuario"] is DBNull ? 0 : Convert.ToInt32(dr["IdUsuario"]);
                                    producto.NombreUsuario = dr["Usuario"] is DBNull ? "" : dr["Usuario"].ToString();
                                    _lista.Add(producto);
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



    }
}
