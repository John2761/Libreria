using Libreria.Application.DTOs;
using Libreria.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.Application.Services.Interfaces
{
    public interface IServiceOrden
    {
        Task<ICollection<OrdenDTO>> ListAsync();
        Task<OrdenDTO> FindByIdAsync(int id);
        Task<OrdenDTO> FindByIdChangeAsync(int id);
        Task<int> AddAsync(OrdenDTO dto);
        Task<int> GetNextNumberOrden();
    }
}
