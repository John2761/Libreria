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
    
    public class RepositoryAutor : IRepositoryAutor
    {
        private readonly LibreriaContext _context;
        //Alt+Enter
        public RepositoryAutor(LibreriaContext context)
        {
            _context = context;
        }

        public async Task<Autor> FindByIdAsync(int id)
        {
            var @object = await _context.Set<Autor>().FindAsync(id);

            return @object!;
        }

        public async Task<ICollection<Autor>> ListAsync()
        {
            var collection = await _context.Set<Autor>().AsNoTracking().ToListAsync();
            return collection;
        }
    }
}
