using Microsoft.AspNetCore.Identity;

namespace Gucm.IdentityServer.Data.Entities
{
    public class UserLogin : IdentityUserLogin<int>
    {
        public virtual User User { get; set; }
    }
}