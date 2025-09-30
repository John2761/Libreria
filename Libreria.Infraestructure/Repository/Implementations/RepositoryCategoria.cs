using Libreria.Infraestructure.Data;
using Libreria.Infraestructure.Models;
using Libreria.Infraestructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.Infraestructure.Repository.Implementations
{
    
    public class RepositoryCategoria : IRepositoryCategoria
    {
        private readonly LibreriaContext _context;
        //Alt+Enter
        public RepositoryCategoria(LibreriaContext context)
        {
            _context = context;
        }

        public async Task<Categoria> FindByIdAsync(int id)
        {
            var @object = await _context.Set<Categoria>().FindAsync(id);

            return @object!;
        }

        public async Task<ICollection<Categoria>> ListAsync()
        {
            var collection = await _context.Set<Categoria>().AsNoTracking().ToListAsync();
            return collection;
        }
    }
}
