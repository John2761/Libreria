using Libreria.Infraestructure.Data;
using Libreria.Infraestructure.Models;
using Libreria.Infraestructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Libreria.Infraestructure.Repository.Implementations
{
    public class RepositoryOrden : IRepositoryOrden
    {
        private readonly LibreriaContext _context;
        public RepositoryOrden(LibreriaContext context)
        {
            _context = context;
        }
        public async Task<int> AddAsync(Orden entity)
        {
            try
            {
                // Begin Transaction
                await _context.Database.BeginTransactionAsync();
                await _context.Set<Orden>().AddAsync(entity);
                // Actualizar inventario
                foreach (var item in entity.OrdenDetalle)
                {
                    //Buscar libro
                    var libro = await _context.Set<Libro>().FindAsync(item.IdLibro);
                    //Actualizar cantidad en stock
                    libro!.Cantidad = libro.Cantidad - item.Cantidad;
                    //Actualizar libro
                    _context.Set<Libro>().Update(libro);
                }
                await _context.SaveChangesAsync();
                // Commit
                await _context.Database.CommitTransactionAsync();

                return entity.IdOrden;
            }
            catch (Exception ex)
            {
                Exception exception = ex;
                // Rollback 
                await _context.Database.RollbackTransactionAsync();
                throw new Exception(ex.Message);
            }
        }

        public async Task<Orden> FindByIdAsync(int id)
        {
            var @object = await _context.Set<Orden>()
                .Include(cliente => cliente.IdClienteNavigation)
                .Include(detalle => detalle.OrdenDetalle)
                .ThenInclude(libro=>libro.IdLibroNavigation)
                .Where(x => x.IdOrden == id).FirstOrDefaultAsync();

            return @object!;
        }
        public async Task<ICollection<Orden>> ListAsync()
        {
            var collection = await _context.Set<Orden>()
                                            .Include(x => x.IdClienteNavigation)
                                            .AsNoTracking()
                                            .ToListAsync();
            return collection;
        }
        public async Task<int> GetNextNumberOrden()
        {
            int current = 0;

            string sql = string.Format("SELECT IDENT_CURRENT ('Orden') AS Current_Identity;");

            System.Data.DataTable dataTable = new System.Data.DataTable();

            System.Data.Common.DbConnection connection = _context.Database.GetDbConnection();
            System.Data.Common.DbProviderFactory dbFactory = System.Data.Common.DbProviderFactories.GetFactory(connection!)!;
            using (var cmd = dbFactory!.CreateCommand())
            {
                cmd!.Connection = connection;
                cmd.CommandText = sql;
                using (System.Data.Common.DbDataAdapter adapter = dbFactory.CreateDataAdapter()!)
                {
                    adapter.SelectCommand = cmd;
                    adapter.Fill(dataTable);
                }
            }


            current = Convert.ToInt32(dataTable.Rows[0][0].ToString());
            return await Task.FromResult(current);
        }

        public async Task<Orden> FindByIdChangeAsync(int id)
        {
            var @object = await _context.Set<Orden>()
                .Where(x => x.IdOrden == id).FirstOrDefaultAsync();

            return @object!;
        }
        public async Task<ICollection<Orden>> OrdenByClientId(string id)
        {
            var response = await _context.Set<Orden>()
                           .Include(p => p.OrdenDetalle)
                           .Where(p => p.IdCliente == id.ToString()).ToListAsync();

            return response;

        }

    }
}
