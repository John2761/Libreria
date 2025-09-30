using Libreria.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Libreria.Application.DTOs
{
    public record LibroDTO
    {
        [Display(Name = "Identificador Libro")]
        [ValidateNever]
        public int IdLibro { get; set; }

        [Display(Name = "ISBN")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string Isbn { get; set; } = null!;

        [Display(Name = "Autor")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public int IdAutor { get; set; }

        [Display(Name = "Nombre Libro")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public string Nombre { get; set; } = null!;

        [Display(Name = "Precio")]
        //[Range(0, 999999999, ErrorMessage = "El valor mínimo es {0}")]
        [DisplayFormat( DataFormatString = "{0:C0}")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        public decimal Precio { get; set; }

        [Display(Name = "Cantidad")]
        [Required(ErrorMessage = "{0} es un dato requerido")]
        [RegularExpression(@"^\d+$", ErrorMessage = "{0} deber númerico")]
        public int Cantidad { get; set; }

        [Display(Name = "Imagen Libro")]
        public byte[] Imagen { get; set; } = null!;

        [Display(Name = "Autor")]
        [ValidateNever]
        public virtual AutorDTO IdAutorNavigation { get; set; } = null!;

        [ValidateNever]
        public virtual List<OrdenDetalleDTO> OrdenDetalle { get; set; } = null!;

        [Display(Name = "Categoría")]
        [ValidateNever]
        public virtual List<CategoriaDTO> IdCategoria { get; set; } = null!;
    }
}
