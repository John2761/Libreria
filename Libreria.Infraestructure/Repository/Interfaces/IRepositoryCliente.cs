using Libreria.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.Infraestructure.Repository.Interfaces
{
    public interface IRepositoryCliente
    {
        Task<ICollection<Cliente>> FindByDescriptionAsync(string description);
        Task<ICollection<Cliente>> ListAsync();
        Task<Cliente> FindByIdAsync(string id);
        Task<string> AddAsync(Cliente entity);
        Task DeleteAsync(string id);
        Task UpdateAsync();
    }
}
