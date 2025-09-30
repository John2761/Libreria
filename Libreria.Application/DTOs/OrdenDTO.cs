using Libreria.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.Application.DTOs
{
    public record OrdenDTO
    {
        [Display(Name = "Orden #")]
        public int IdOrden { get; set; }

        [Display(Name = "Cliente")]
        public string IdCliente { get; set; } = null!;

        [Display(Name = "Usuario")]
        public int IdUsuario { get; set; }

        [Display(Name = "Fecha")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]

        public DateTime FechaOrden { get; set; }

        public decimal Total { get; set; }

        [Display(Name = "Cliente")]
        public virtual ClienteDTO IdClienteNavigation { get; set; } = null!;

        public virtual UsuarioDTO IdUsuarioNavigation { get; set; } = null!;

        public virtual List<OrdenDetalleDTO> OrdenDetalle { get; set; } = null!;
    }
}
