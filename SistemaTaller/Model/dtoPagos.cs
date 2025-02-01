using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTaller.Model
{
    internal class dtoPagos
    {
        public int IdPago { get; set; }
        public int IdContrato { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaPago { get; set; }
        public string MetodoPago { get; set; }
    }
}
