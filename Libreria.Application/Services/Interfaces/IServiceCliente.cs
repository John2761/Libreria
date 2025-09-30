using Libreria.Application.DTOs;
using Libreria.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.Application.Services.Interfaces
{
    public interface IServiceCliente
    {
        Task<ICollection<ClienteDTO>> FindByDescriptionAsync(string description);
        Task<ICollection<ClienteDTO>> ListAsync();
        Task<ClienteDTO> FindByIdAsync(string id);
        Task<string> AddAsync(ClienteDTO dto);
        Task DeleteAsync(string id);
        Task UpdateAsync();
    }
}
