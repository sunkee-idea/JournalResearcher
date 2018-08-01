using System.Threading;
using System.Threading.Tasks;

namespace JournalResearcher.DataAccess.Repository
{
    public interface IUnitOfWork
    {
        int SaveChanges();
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}