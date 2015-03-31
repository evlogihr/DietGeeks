namespace DietGeeks.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    using Microsoft.AspNet.Identity.EntityFramework;

    using DietGeeks.Data.Models;
    using DietGeeks.Data.Contracts;

    public class DietGeeksDbContext : IdentityDbContext<AppUser>, IDietGeeksDbContext
    {
        public DietGeeksDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        { }

        public DietGeeksDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        { }

        public static DietGeeksDbContext Create()
        {
            return new DietGeeksDbContext();
        }

        public new DbContext DbContext
        {
            get { throw new NotImplementedException(); }
        }

        public void ClearDatabase()
        {
            throw new NotImplementedException();
        }

        public new IDbSet<T> Set<T>() where T : class
        {
            throw new NotImplementedException();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        private void ApplyAuditInfoRules()
        {
            // Approach via @julielerman: http://bit.ly/123661P
            foreach (var entry in
                this.ChangeTracker.Entries()
                    .Where(
                        e =>
                        e.Entity is IAuditInfo && ((e.State == EntityState.Added) || (e.State == EntityState.Modified))))
            {
                var entity = (IAuditInfo)entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    if (!entity.PreserveCreatedOn)
                    {
                        entity.CreatedOn = DateTime.Now;
                    }
                }
                else
                {
                    entity.ModifiedOn = DateTime.Now;
                }
            }
        }

        private void ApplyDeletableEntityRules()
        {
            // Approach via @julielerman: http://bit.ly/123661P
            foreach (
                var entry in
                    this.ChangeTracker.Entries()
                        .Where(e => e.Entity is IDeletableEntity && (e.State == EntityState.Deleted)))
            {
                var entity = (IDeletableEntity)entry.Entity;

                entity.DeletedOn = DateTime.Now;
                entity.IsDeleted = true;
                entry.State = EntityState.Modified;
            }
        }
    }
}
