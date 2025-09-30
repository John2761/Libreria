using Libreria.Application.DTOs;
using Libreria.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.Application.Services.Interfaces
{
    public interface IServiceLibro
    {
        Task<ICollection<LibroDTO>> FindByNameAsync(string nombre);
        Task<ICollection<LibroDTO>> ListAsync();
        Task<LibroDTO> FindByIdAsync(int id);
        Task<int> AddAsync(LibroDTO dto, string[] selectedCategorias);
        Task DeleteAsync(int id);
        Task UpdateAsync(int id, LibroDTO dto, string[] selectedCategorias);

        Task<ICollection<LibroDTO>> GetLibroByCategoria(int IdCategoria);
       
    }
}
