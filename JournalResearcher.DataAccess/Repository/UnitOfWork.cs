using System.Threading;
using System.Threading.Tasks;
using JournalResearcher.DataAccess.Data;

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
    }
}