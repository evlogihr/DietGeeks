namespace DietGeeks.Data
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public interface IDietGeeksDbContext : IDisposable
    {
        DbContext DbContext { get; }

        int SaveChanges();

        void ClearDatabase();

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        IDbSet<T> Set<T>() where T : class;
    }
}
