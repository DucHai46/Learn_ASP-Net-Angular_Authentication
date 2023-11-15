using Microsoft.AspNetCore.Identity;

namespace API_identity.Entities.Models
{
    public class User : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
