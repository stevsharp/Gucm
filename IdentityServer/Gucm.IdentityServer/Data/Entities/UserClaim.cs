using Microsoft.AspNetCore.Identity;

namespace Gucm.IdentityServer.Data.Entities
{
    public class UserClaim : IdentityUserClaim<int>
    {
        public virtual User User { get; set; }
    }
}