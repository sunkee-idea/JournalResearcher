using JournalResearcher.DataAccess.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace JournalResearcher.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public void InsertNew(RefreshToken token)
        {
            var tokenModel = RefreshTokens.SingleOrDefault(i => i.UserId == token.UserId);
            if (tokenModel != null)
            {
                RefreshTokens.Remove(tokenModel);
                SaveChanges();
            }
            RefreshTokens.Add(token);
            SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Journal> Journals { get; set; }
        public DbSet<RejectJournal> RejectJournals { get; set; }

    }
}
