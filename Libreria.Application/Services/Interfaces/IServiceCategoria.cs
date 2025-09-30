using Libreria.Application.DTOs;
using Libreria.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.Application.Services.Interfaces
{
    public interface IServiceCategoria
    {
        Task<ICollection<CategoriaDTO>> ListAsync();
        Task<CategoriaDTO> FindByIdAsync(int id);
    }
}
