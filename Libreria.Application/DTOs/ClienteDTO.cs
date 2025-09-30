using Libreria.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.Application.DTOs
{
    public record ClienteDTO
    {
        public string IdCliente { get; set; } = null!;

        public string Nombre { get; set; } = null!;

        public string Apellido1 { get; set; } = null!;

        public string? Apellido2 { get; set; }

        public string Sexo { get; set; } = null!;

        public DateTime FechaNacimiento { get; set; }

        public virtual List<OrdenDTO> Orden { get; set; } = null!;
    }
}
