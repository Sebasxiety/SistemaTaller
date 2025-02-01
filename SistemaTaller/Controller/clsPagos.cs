using SistemaTaller.Config;
using SistemaTaller.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTaller.Controller
{
    internal class clsPagos
    {
        public bool Registrar(dtoPagos pago)
        {
            try
            {
                clsConexion db = new clsConexion();
                using (SqlConnection con = db.obtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_InsertarPago", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdContrato", pago.IdContrato);
                        cmd.Parameters.AddWithValue("@Monto", pago.Monto);
                        cmd.Parameters.AddWithValue("@FechaPago", pago.FechaPago);
                        cmd.Parameters.AddWithValue("@MetodoPago", pago.MetodoPago);

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Actualizar(dtoPagos pago)
        {
            try
            {
                clsConexion db = new clsConexion();
                using (SqlConnection con = db.obtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ActualizarPago", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdPago", pago.IdPago);
                        cmd.Parameters.AddWithValue("@IdContrato", pago.IdContrato);
                        cmd.Parameters.AddWithValue("@Monto", pago.Monto);
                        cmd.Parameters.AddWithValue("@FechaPago", pago.FechaPago);
                        cmd.Parameters.AddWithValue("@MetodoPago", pago.MetodoPago);

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Eliminar(int idPago)
        {
            try
            {
                clsConexion db = new clsConexion();
                using (SqlConnection con = db.obtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_EliminarPago", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdPago", idPago);

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<dtoPagos> Listar()
        {
            List<dtoPagos> lista = new List<dtoPagos>();
            try
            {
                clsConexion db = new clsConexion();
                using (SqlConnection con = db.obtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ObtenerPagos", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdPago", DBNull.Value);

                        con.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                dtoPagos p = new dtoPagos
                                {
                                    IdPago = Convert.ToInt32(dr["IdPago"]),
                                    IdContrato = Convert.ToInt32(dr["IdContrato"]),
                                    Monto = Convert.ToDecimal(dr["Monto"]),
                                    FechaPago = Convert.ToDateTime(dr["FechaPago"]),
                                    MetodoPago = dr["MetodoPago"].ToString()
                                };
                                lista.Add(p);
                            }
                        }
                    }
                }
            }
            catch
            {
                
            }
            return lista;
        }
    }
}
