using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTaller.Config
{
    internal class clsConexion
    {
        private readonly string cadenaConexion =
            "Server=(local);database=BBDDTaller;uid=sa;pwd=123456";

        public SqlConnection obtenerConexion()
        {
            return new SqlConnection(cadenaConexion);
        }
    }
}
