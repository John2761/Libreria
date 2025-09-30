using Libreria.Application.DTOs;
using Libreria.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.Application.Services.Interfaces
{
    public interface IServiceAutor
    {
        Task<ICollection<AutorDTO>> ListAsync();
        Task<AutorDTO> FindByIdAsync(int id);
    }
}
