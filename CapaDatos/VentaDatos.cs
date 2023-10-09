using Sistema_de_Gestion.Modelo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;


namespace CapaDatos
{
    public static class VentaDatos
    {
        private static readonly string connectionsting = @"Server=DESKTOP-QL4OJD8\SQLEXPRESS;DataBase=SistemaGestion;Trusted_Connection=True";
        public static void InsertarVenta(Venta ven)
        {
            int Idventa = 0;
       

            string _sente = "";

             _sente = " INSERT INTO Ventas (Comentario, IdUsuario ) " +
                            " VALUES (@Comentario, @IdUsuario) ";
            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionsting))
                {
                    conexion.Open();

                    using (SqlCommand command = new SqlCommand(_sente, conexion))
                    {
                        command.Parameters.Add(new SqlParameter("Comentario", SqlDbType.VarChar) { Value = ven.Comentario });
                        command.Parameters.Add(new SqlParameter("IdUsuario", SqlDbType.BigInt) { Value = ven.IdUsuario });

                        command.ExecuteNonQuery();

                         _sente = "SELECT max(v.Id_Venta) Id_Venta FROM ventas v";


                        using (SqlCommand commando = new SqlCommand(_sente, conexion))
                        {
                            Idventa = Convert.ToInt32(commando.ExecuteScalar());
                        }

                        foreach (var _det in ven.listaDetalleProductoVendido)
                        {
                            ProductoVendido prodv = new ProductoVendido();

                            prodv.IdProducto = _det.IdProducto;
                            prodv.IdVenta = Idventa;
                            prodv.Stock = _det.Stock;
                            InsertarDetalle(prodv);
                        }

                    }
                    conexion.Close();
                }
            }

            catch (Exception ex)
            {

                Mensajes.MensajeError(ex);
            }
        }

        public static void InsertarDetalle(ProductoVendido prodv)
        {

       

            string _sente = " INSERT INTO Producto_Vendido (Id, Stock, IdVenta ) " +
                            " VALUES (@Id, @Stock, @IdVenta) ";
            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionsting))
                {
                    conexion.Open();
                    using (SqlCommand command = new SqlCommand(_sente, conexion))
                    {
                        command.Parameters.Add(new SqlParameter("Id", SqlDbType.Int) { Value = prodv.IdProducto });
                        command.Parameters.Add(new SqlParameter("Stock", SqlDbType.Int) { Value = prodv.Stock });
                        command.Parameters.Add(new SqlParameter("IdVenta", SqlDbType.Int) { Value = prodv.IdVenta });
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

        public static void ModificarVenta(Venta ven)
        {
         

            string _sente = " UPDATE Ventas SET " +
                            " Comentario = @Comentario, " +
                            " IdUsuario = @IdUsuario " +
                            " WHERE Id_Venta = @Id_Venta ";
            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionsting))
                {
                    conexion.Open();
                    using (SqlCommand command = new SqlCommand(_sente, conexion))
                    {
                        command.Parameters.Add(new SqlParameter("Comentario", SqlDbType.VarChar) { Value = ven.Comentario });
                        command.Parameters.Add(new SqlParameter("IdUsuario", SqlDbType.BigInt) { Value = ven.IdUsuario });
                        command.Parameters.Add(new SqlParameter("Id_Venta", SqlDbType.Int) { Value = ven.IdVenta });

                        EliminarTodosLosDetalles(ven.IdVenta);

                        foreach (var _det in ven.listaDetalleProductoVendido)
                        {
                            ProductoVendido prodv = new ProductoVendido();
                       
                            prodv.IdProducto = _det.IdProducto;
                            prodv.Stock = _det.Stock;
                            prodv.IdVenta = ven.IdVenta;
                            InsertarDetalle(prodv);
                        }

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

        public static void EliminarTodosLosDetalles(int id_venta)
        {
        

            string _sente = " DELETE FROM Producto_Vendido WHERE IdVenta = @IdVenta ";
            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionsting))
                {
                    conexion.Open();
                    using (SqlCommand command = new SqlCommand(_sente, conexion))
                    {
                        command.Parameters.Add(new SqlParameter("IdVenta", SqlDbType.Int) { Value = id_venta });
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

        public static void EliminarVenta(int id_venta)
        {
            EliminarTodosLosDetalles(id_venta);

       

            string _sente = " DELETE FROM Ventas WHERE Id_Venta = @Id_Venta ";
            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionsting))
                {
                    conexion.Open();
                    using (SqlCommand command = new SqlCommand(_sente, conexion))
                    {
                        command.Parameters.Add(new SqlParameter("Id_Venta", SqlDbType.Int) { Value = id_venta });
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

        public static Venta ObtenerVentaxId(int idventa)
        {
            Venta ven = new Venta();
         

            string _sente = "SELECT v.Id_Venta, v.Comentario, v.IdUsuario FROM Ventas v WHERE v.Id_Venta = @Id_Venta ";
            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionsting))
                {
                    conexion.Open();
                    using (SqlCommand command = new SqlCommand(_sente, conexion))
                    {
                        command.Parameters.Add(new SqlParameter("Id_Venta", SqlDbType.Int) { Value = idventa });

                        using (SqlDataReader dr = command.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                if (dr.Read())
                                {
                                    ven.IdVenta = dr["Id_Venta"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Venta"]);
                                    ven.Comentario = dr["Comentario"] is DBNull ? "" : dr["Comentario"].ToString();
                                    ven.IdUsuario = dr["IdUsuario"] is DBNull ? 0 : Convert.ToInt32(dr["IdUsuario"]);
                                    ven.listaDetalleProductoVendido = listarProductoVendidos(idventa);


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
            return ven;
        }

        public static List<ProductoVendido> listarProductoVendidos(int idventa)
        {
            List<ProductoVendido> _lista = new List<ProductoVendido>();
       

            string _sente = "SELECT p.IdProd_Vendido, p.IdVenta, p.Id, pr.Descripciones, p.Stock, pr.PrecioVenta FROM Producto_Vendido p LEFT JOIN Producto pr ON " +
                            "p.Id = pr.Id WHERE p.IdVenta = @IdVenta";
            try
            {
                using (SqlConnection conexion = new SqlConnection(connectionsting))
                {
                    conexion.Open();
                    using (SqlCommand command = new SqlCommand(_sente, conexion))
                    {

                        command.Parameters.Add(new SqlParameter("IdVenta", SqlDbType.Int) { Value = idventa });

                        using (SqlDataReader dr = command.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    var productoVendido = new ProductoVendido();
                                    productoVendido.IdProdVendido = dr["IdProd_Vendido"] is DBNull ? 0 : Convert.ToInt32(dr["IdProd_Vendido"]);
                                    productoVendido.IdProducto = dr["Id"] is DBNull ? 0 : Convert.ToInt32(dr["Id"]);
                                    productoVendido.IdVenta = dr["IdVenta"] is DBNull ? 0 : Convert.ToInt32(dr["IdVenta"]);
                                    productoVendido.Descripcion = dr["Descripciones"] is DBNull ? "" : dr["Descripciones"].ToString();
                                    productoVendido.Stock = dr["Stock"] is DBNull ? 0 : Convert.ToInt32(dr["Stock"]);
                                    productoVendido.PrecioVenta = dr["PrecioVenta"] is DBNull ? 0 : Convert.ToDecimal(dr["PrecioVenta"]);
                                    _lista.Add(productoVendido);
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

        public static List<VentasRealizadas> listarVentas()
        {
            List<VentasRealizadas> _lista = new List<VentasRealizadas>();
         

            string _sente = " SELECT v.Id_Venta, p.Id, pr.Descripciones, p.Stock, pr.PrecioVenta, v.Comentario, u.Nombre +' '+ u.Apellido nombreCompleto " +
                               " FROM Ventas v LEFT JOIN Producto_Vendido p ON " +
                               " v.Id_Venta = p.IdVenta " +
                               " LEFT JOIN Producto pr ON pr.Id = p.Id " +
                               " LEFT JOIN Usuario u ON v.IdUsuario = u.IdUsuario ";
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
                                    var vrealizada = new VentasRealizadas();
                                    vrealizada.Id_Venta = dr["Id_Venta"] is DBNull ? 0 : Convert.ToInt32(dr["Id_Venta"]);
                                    vrealizada.Id_Producto = dr["Id"] is DBNull ? 0 : Convert.ToInt32(dr["Id"]);
                                    vrealizada.Descripciones = dr["Descripciones"] is DBNull ? "" : dr["Descripciones"].ToString();
                                    vrealizada.Stock = dr["Stock"] is DBNull ? 0 : Convert.ToInt32(dr["Stock"]);
                                    vrealizada.PrecioVenta = dr["PrecioVenta"] is DBNull ? 0 : Convert.ToDecimal(dr["PrecioVenta"]);
                                    vrealizada.Comentario = dr["Comentario"] is DBNull ? "" : dr["Comentario"].ToString();
                                    vrealizada.nombreCompleto = dr["nombreCompleto"] is DBNull ? "" : dr["nombreCompleto"].ToString();
                                    _lista.Add(vrealizada);
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

