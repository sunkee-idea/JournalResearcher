using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JournalResearcher.Resources
{
    public class TokenResponseData
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public int ExpiresIn { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public string Role { get; set; }
        public string Key { get; set; }
    }
}
