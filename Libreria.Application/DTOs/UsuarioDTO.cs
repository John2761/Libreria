using Libreria.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.Application.DTOs
{
    public record UsuarioDTO
    {
        public int IdUsuario { get; set; }

        public string Email { get; set; } = null!;

        public int IdRol { get; set; }

        public string Password { get; set; } = null!;

        public string Nombre { get; set; } = null!;

        public string Apellidos { get; set; } = null!;

        public bool Estado { get; set; }

        public virtual RolDTO IdRolNavigation { get; set; } = null!;

        public virtual List<OrdenDTO> Orden { get; set; } = null!;
    }
}
