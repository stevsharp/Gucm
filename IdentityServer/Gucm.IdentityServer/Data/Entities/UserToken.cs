using Microsoft.AspNetCore.Identity;

namespace Gucm.IdentityServer.Data.Entities
{
    public class UserToken : IdentityUserToken<int>
    {
        public virtual User User { get; set; }
    }
}