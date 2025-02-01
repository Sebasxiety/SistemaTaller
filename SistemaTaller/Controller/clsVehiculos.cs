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
    internal class clsVehiculos
    {
        public bool Registrar(dtoVehiculos vehiculo)
        {
            try
            {
                clsConexion db = new clsConexion();
                using (SqlConnection con = db.obtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_InsertarVehiculo", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Marca", vehiculo.Marca);
                        cmd.Parameters.AddWithValue("@Modelo", vehiculo.Modelo);
                        cmd.Parameters.AddWithValue("@Anio", vehiculo.Anio);
                        cmd.Parameters.AddWithValue("@Color", vehiculo.Color);
                        cmd.Parameters.AddWithValue("@Estado", vehiculo.Estado);

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

        public bool Actualizar(dtoVehiculos vehiculo)
        {
            try
            {
                clsConexion db = new clsConexion();
                using (SqlConnection con = db.obtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ActualizarVehiculo", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdVehiculo", vehiculo.IdVehiculo);
                        cmd.Parameters.AddWithValue("@Marca", vehiculo.Marca);
                        cmd.Parameters.AddWithValue("@Modelo", vehiculo.Modelo);
                        cmd.Parameters.AddWithValue("@Anio", vehiculo.Anio);
                        cmd.Parameters.AddWithValue("@Color", vehiculo.Color);
                        cmd.Parameters.AddWithValue("@Estado", vehiculo.Estado);

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

        public bool Eliminar(int idVehiculo)
        {
            try
            {
                clsConexion db = new clsConexion();
                using (SqlConnection con = db.obtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_EliminarVehiculo", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdVehiculo", idVehiculo);

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

        public List<dtoVehiculos> Listar()
        {
            List<dtoVehiculos> lista = new List<dtoVehiculos>();
            try
            {
                clsConexion db = new clsConexion();
                using (SqlConnection con = db.obtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ObtenerVehiculos", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdVehiculo", DBNull.Value);

                        con.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                dtoVehiculos v = new dtoVehiculos
                                {
                                    IdVehiculo = Convert.ToInt32(dr["IdVehiculo"]),
                                    Marca = dr["Marca"].ToString(),
                                    Modelo = dr["Modelo"].ToString(),
                                    Anio = Convert.ToInt32(dr["Anio"]),
                                    Color = dr["Color"].ToString(),
                                    Estado = dr["Estado"].ToString()
                                };
                                lista.Add(v);
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
