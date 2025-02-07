﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaTaller.Model
{
    internal class dtoClientes
    {
        public int IdCliente { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public override string ToString()
        {
            return Nombre + " " + Apellido;
        }
    }
}
