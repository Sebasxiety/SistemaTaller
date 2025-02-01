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
    internal class clsContratos
    {
        public bool Registrar(dtoContratos contrato)
        {
            try
            {
                clsConexion db = new clsConexion();
                using (SqlConnection con = db.obtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_InsertarContrato", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdCliente", contrato.IdCliente);
                        cmd.Parameters.AddWithValue("@IdVehiculo", contrato.IdVehiculo);
                        cmd.Parameters.AddWithValue("@FechaInicio", contrato.FechaInicio);
                        cmd.Parameters.AddWithValue("@FechaFin", (object)contrato.FechaFin ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@MontoTotal", contrato.MontoTotal);

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

        public bool Actualizar(dtoContratos contrato)
        {
            try
            {
                clsConexion db = new clsConexion();
                using (SqlConnection con = db.obtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ActualizarContrato", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdContrato", contrato.IdContrato);
                        cmd.Parameters.AddWithValue("@IdCliente", contrato.IdCliente);
                        cmd.Parameters.AddWithValue("@IdVehiculo", contrato.IdVehiculo);
                        cmd.Parameters.AddWithValue("@FechaInicio", contrato.FechaInicio);
                        cmd.Parameters.AddWithValue("@FechaFin", (object)contrato.FechaFin ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@MontoTotal", contrato.MontoTotal);

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

        public bool Eliminar(int idContrato)
        {
            try
            {
                clsConexion db = new clsConexion();
                using (SqlConnection con = db.obtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_EliminarContrato", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdContrato", idContrato);

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

        public List<dtoContratos> Listar()
        {
            List<dtoContratos> lista = new List<dtoContratos>();
            try
            {
                clsConexion db = new clsConexion();
                using (SqlConnection con = db.obtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ObtenerContratos", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdContrato", DBNull.Value);

                        con.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                dtoContratos c = new dtoContratos
                                {
                                    IdContrato = Convert.ToInt32(dr["IdContrato"]),
                                    IdCliente = Convert.ToInt32(dr["IdCliente"]),
                                    IdVehiculo = Convert.ToInt32(dr["IdVehiculo"]),
                                    FechaInicio = Convert.ToDateTime(dr["FechaInicio"]),
                                    FechaFin = dr["FechaFin"] == DBNull.Value
                                        ? (DateTime?)null
                                        : Convert.ToDateTime(dr["FechaFin"]),
                                    MontoTotal = Convert.ToDecimal(dr["MontoTotal"]),
                                    FechaCreacion = dr["FechaCreacion"] == DBNull.Value
                                        ? (DateTime?)null
                                        : Convert.ToDateTime(dr["FechaCreacion"])
                                };
                                lista.Add(c);
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
