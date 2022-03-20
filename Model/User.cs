using Microsoft.AspNetCore.Identity;

namespace Tryneboka.Models {
    public class User : IdentityUser {
        public ICollection<Post> Posts { get; set; }
    }
}