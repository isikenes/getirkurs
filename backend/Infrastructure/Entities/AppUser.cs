using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Entities
{
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }
    }
}
