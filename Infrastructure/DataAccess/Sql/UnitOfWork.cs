namespace Infrastructure.DataAccess.Sql
{
    using System;
    using System.Threading.Tasks;
    using Application.Services;
    using Domain.Exceptions;
    using Infrastructure.DataAccess.Sql.SqlServer;

    public sealed class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DatabaseContext _context;
        private bool _disposed;

        public UnitOfWork(DatabaseContext context) => _context = context;

        public void Dispose() => Dispose(true);

        public async Task<int> SaveAsync()
        {
            try
            {
                int affectedRows = await _context.SaveChangesAsync();
                return affectedRows;
            }
            catch (Exception e)
            {
                throw new RepositoryException("Erro ao persistir entidades", e);
            }
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
                _context.Dispose();

            _disposed = true;
        }
    }
}