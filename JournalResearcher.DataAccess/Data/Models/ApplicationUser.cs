using Microsoft.AspNetCore.Identity;

namespace JournalResearcher.DataAccess.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
    }
}
