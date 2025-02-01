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
    internal class clsClientes
    {
        //create
        public bool Registrar(dtoClientes cliente)
        {
            try
            {
                clsConexion db = new clsConexion();
                using (SqlConnection con = db.obtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_InsertarCliente", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                        cmd.Parameters.AddWithValue("@Apellido", cliente.Apellido);
                        cmd.Parameters.AddWithValue("@Telefono", cliente.Telefono);
                        cmd.Parameters.AddWithValue("@Email", cliente.Email);

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
               
                return false;
            }
        }

        // UPDATE
        public bool Actualizar(dtoClientes cliente)
        {
            try
            {
                clsConexion db = new clsConexion();
                using (SqlConnection con = db.obtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ActualizarCliente", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdCliente", cliente.IdCliente);
                        cmd.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                        cmd.Parameters.AddWithValue("@Apellido", cliente.Apellido);
                        cmd.Parameters.AddWithValue("@Telefono", cliente.Telefono);
                        cmd.Parameters.AddWithValue("@Email", cliente.Email);

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

        // DELETE
        public bool Eliminar(int idCliente)
        {
            try
            {
                clsConexion db = new clsConexion();
                using (SqlConnection con = db.obtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_EliminarCliente", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdCliente", idCliente);

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

        // READ (LISTAR)
        public List<dtoClientes> Listar()
        {
            List<dtoClientes> lista = new List<dtoClientes>();
            try
            {
                clsConexion db = new clsConexion();
                using (SqlConnection con = db.obtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ObtenerClientes", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        // Asignamos NULL a @IdCliente para traer todos
                        cmd.Parameters.AddWithValue("@IdCliente", DBNull.Value);

                        con.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                dtoClientes c = new dtoClientes
                                {
                                    IdCliente = Convert.ToInt32(dr["IdCliente"]),
                                    Nombre = dr["Nombre"].ToString(),
                                    Apellido = dr["Apellido"].ToString(),
                                    Telefono = dr["Telefono"].ToString(),
                                    Email = dr["Email"].ToString(),
                                    FechaRegistro = dr["FechaRegistro"] == DBNull.Value
                                        ? (DateTime?)null
                                        : Convert.ToDateTime(dr["FechaRegistro"])
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
