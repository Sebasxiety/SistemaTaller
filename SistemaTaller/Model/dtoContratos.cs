using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTaller.Model
{
    internal class dtoContratos
    {
        public int IdContrato { get; set; }
        public int IdCliente { get; set; }
        public int IdVehiculo { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public decimal MontoTotal { get; set; }
        public DateTime? FechaCreacion { get; set; }
    }
}
