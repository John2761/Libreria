using Libreria.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.Application.DTOs
{
    public record OrdenDetalleDTO
    {
        public int IdOrden { get; set; }
        [Display(Name = "Código")]
        public int IdLibro { get; set; }

        [Display(Name = "Libro")]
        public string NombreLibro { get; set; } = default!;

        [Display(Name = "Cantidad")]
        public int Cantidad { get; set; }

        [DisplayFormat(DataFormatString = "{0:n2}")]
        [Display(Name = "Precio")]
        public decimal Precio { get; set; }

        [DisplayFormat(DataFormatString = "{0:n2}")]
        [Display(Name = "Subtotal")]
        public decimal Subtotal { get; set; }

        public virtual LibroDTO IdLibroNavigation { get; set; } = null!;

        public virtual OrdenDTO IdOrdenNavigation { get; set; } = null!;
    }
}
