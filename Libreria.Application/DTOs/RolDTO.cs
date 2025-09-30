using Libreria.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.Application.DTOs
{
    public record RolDTO
    {
        public int IdRol { get; set; }

        public string Descripcion { get; set; } = null!;

        public virtual List<UsuarioDTO> Usuario { get; set; } = null!;
    }
}
