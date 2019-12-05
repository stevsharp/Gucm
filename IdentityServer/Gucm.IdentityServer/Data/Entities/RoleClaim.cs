using Microsoft.AspNetCore.Identity;

namespace Gucm.IdentityServer.Data.Entities
{
    public class RoleClaim : IdentityRoleClaim<int>
    {
        public virtual Role Role { get; set; }
    }
}