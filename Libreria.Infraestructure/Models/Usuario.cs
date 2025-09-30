using System;
using System.Collections.Generic;

namespace Libreria.Infraestructure.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string Email { get; set; } = null!;

    public int IdRol { get; set; }

    public string Password { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public bool Estado { get; set; }

    public virtual Rol IdRolNavigation { get; set; } = null!;

    public virtual ICollection<Orden> Orden { get; set; } = new List<Orden>();
}
