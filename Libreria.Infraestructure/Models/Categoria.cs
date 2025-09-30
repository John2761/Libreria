using System;
using System.Collections.Generic;

namespace Libreria.Infraestructure.Models;

public partial class Categoria
{
    public int IdCategoria { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Libro> IdLibro { get; set; } = new List<Libro>();
}
