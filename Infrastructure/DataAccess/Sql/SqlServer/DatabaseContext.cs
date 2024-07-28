namespace Infrastructure.DataAccess.Sql.SqlServer
{
    using System;
    using Microsoft.EntityFrameworkCore;

    /// <inheritdoc />
    public sealed class DatabaseContext : DbContext
    {
        /// <summary>
        /// </summary>
        /// <param name="options"></param>

        public DatabaseContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder is null)
                throw new ArgumentNullException(nameof(modelBuilder));

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);
        }
    }
}