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
    public class RepositoryCliente:IRepositoryCliente
    {
        private readonly LibreriaContext _context;

        public RepositoryCliente(LibreriaContext context)
        {

            _context = context;
        }

        public async Task<string> AddAsync(Cliente entity)
        {
            await _context.Set<Cliente>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.IdCliente;
        }

        public async Task DeleteAsync(string id)
        {
            
            var @object = await FindByIdAsync(id);
            _context.Remove(@object);
            _context.SaveChanges();
        }

        public async Task<ICollection<Cliente>> FindByDescriptionAsync(string description)
        {
            description = description.Replace(' ', '%');
            description = "%" + description + "%";
            FormattableString sql = $@"select * from Cliente where Nombre+Apellido1+Apellido2 like  {description}  ";

            var collection = await _context.Cliente.FromSql(sql).AsNoTracking().ToListAsync();
            return collection;
        }

        public async Task<Cliente> FindByIdAsync(string id)
        {
            var @object = await _context.Set<Cliente>().FindAsync(id);

            return @object!;
        }

        public async Task<ICollection<Cliente>> ListAsync()
        {

            var collection = await _context.Set<Cliente>().AsNoTracking().ToListAsync();
            return collection;
        }

        public async Task UpdateAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
