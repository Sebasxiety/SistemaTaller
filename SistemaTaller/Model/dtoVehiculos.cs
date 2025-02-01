using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTaller.Model
{
    internal class dtoVehiculos
    {
        public int IdVehiculo { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Anio { get; set; }
        public string Color { get; set; }
        public string Estado { get; set; }
    }
}
