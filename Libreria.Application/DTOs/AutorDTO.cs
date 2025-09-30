using Libreria.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.Application.DTOs
{
    public record AutorDTO
    {
        [Display(Name = "Id")]
        public int IdAutor { get; set; }

        [Display(Name = "Nombre Autor")]
        public string Nombre { get; set; } = null!;

        public virtual List<LibroDTO> Libro { get; set; } = null!;

    }
}
