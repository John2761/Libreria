using Libreria.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.Infraestructure.Repository.Interfaces
{
    public interface IRepositoryLibro
    {
        Task<ICollection<Libro>> FindByNameAsync(string nombre);

        Task<ICollection<Libro>> ListAsync();

        Task<Libro> FindByIdAsync(int id);

        Task<int> AddAsync(Libro entity, string[] selectedCategorias);

        Task DeleteAsync(int id);

        Task UpdateAsync(Libro entity, string[] selectedCategorias);

        Task <ICollection<Libro>> GetLibroByCategoria(int idCategoria);

    }
}
