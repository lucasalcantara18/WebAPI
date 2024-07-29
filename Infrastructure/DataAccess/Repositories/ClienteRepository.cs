using Domain.Interfaces.Repositories;
using Domain.Models;
using Infrastructure.DataAccess.Sql;
using Infrastructure.DataAccess.Sql.Bases;
using Infrastructure.DataAccess.Sql.SqlServer;

namespace Infrastructure.DataAccess.Repositories.Entities
{
    public class ClienteRepository : BaseRepository<Cliente>, IClienteRepository
    {
        public ClienteRepository(DatabaseContext context) : base(context)
        {
        }
    }
}