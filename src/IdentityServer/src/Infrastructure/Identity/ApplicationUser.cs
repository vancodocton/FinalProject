using Microsoft.AspNetCore.Identity;

namespace DuongTruong.IdentityServer.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser() : base()
        {
        }

        public ApplicationUser(string userName) : base(userName)
        {
        }
    }
}