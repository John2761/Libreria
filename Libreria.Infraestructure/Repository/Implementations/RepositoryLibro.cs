using Libreria.Infraestructure.Data;
using Libreria.Infraestructure.Models;
using Libreria.Infraestructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Libreria.Infraestructure.Repository.Implementations
{
    public class RepositoryLibro : IRepositoryLibro
    {
        private readonly LibreriaContext _context;
        public RepositoryLibro(LibreriaContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Libro>> ListAsync()
        {
            var collection = await _context.Set<Libro>()
                                            .Include(x => x.IdAutorNavigation)
                                            .OrderByDescending(x => x.IdAutor)
                                            .AsNoTracking()
                                            .ToListAsync();
            return collection;
        }
        public async Task<Libro> FindByIdAsync(int id)
        {
            //var @object = await _context.Set<Libro>().FindAsync(id);
            var @object = await _context.Set<Libro>()
                .Include(x => x.IdAutorNavigation)
                .Include(x => x.IdCategoria).Where(x => x.IdLibro == id).FirstAsync();

            return @object!;
        }
        public async Task<int> AddAsync(Libro entity, string[] selectedCategorias)
        {
            //Relación de muchos a muchos solo con llave primaria compuesta
            var categorias = await getCategorias(selectedCategorias);
            entity.IdCategoria=categorias;
            await _context.Set<Libro>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.IdLibro;
        }

        public async Task UpdateAsync(Libro entity, string[] selectedCategorias)
        {
            //Las relaciones a actualizar depende de la consulta utilizada en el servicio

            // Asegurar que la relación con Autor se mantenga
            var autor = await _context.Set<Autor>().FindAsync(entity.IdAutor);
            entity.IdAutorNavigation = autor!;

            //Relación de muchos a muchos solo con llave primaria compuesta
            var nuevasCategorias = await getCategorias(selectedCategorias);
            entity.IdCategoria.Clear();// Eliminar todas las categorías actuales
            //Asignar las categorias actualizadas
            entity.IdCategoria=nuevasCategorias;

            await _context.SaveChangesAsync();
        }

       
        private async Task<ICollection<Categoria>> getCategorias(string[] selectedCategorias)
        {
            // Buscar o crear categorías
            var categorias = await _context.Set<Categoria>()
                .Where(c => selectedCategorias.Contains(c.IdCategoria.ToString()))
                .ToListAsync();
            return categorias;

        }
        public async Task<ICollection<Libro>> FindByNameAsync(string nombre)
        {
            var collection = await _context
                                         .Set<Libro>()
                                         .Where(p => p.Nombre.Contains(nombre))
                                         .ToListAsync();
            return collection;
        }


        public async Task<ICollection<Libro>> GetLibroByCategoria(int idCategoria)
        {
            var collection = await _context.Set<Libro>()
                                    .Include(l => l.IdAutorNavigation) // Asegura que la relación se carga
                                    .Where(l => l.IdCategoria.Any(c => c.IdCategoria == idCategoria))
                                    .AsNoTracking()
                                    .ToListAsync();
            return collection;
        }

        public async Task DeleteAsync(int id)
        {
            // Raw Query
            //https://www.learnentityframeworkcore.com/raw-sql/execute-sql
            int rowAffected = _context.Database.ExecuteSql($"Delete Libro Where IdLibro = {id}");
            await Task.FromResult(1);
        }
       
    }
}
