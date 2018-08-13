using JournalResearcher.DataAccess.Data;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace JournalResearcher.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly ApplicationDbContext Context;

        public UnitOfWork(ApplicationDbContext context)
        {
            Context = context;
        }

        public int SaveChanges()
        {
            return Context.SaveChanges();
        }


        public Task<int> SaveChangesAsync()
        {
            return Context.SaveChangesAsync();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return Context.SaveChangesAsync(cancellationToken);
        }


        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}