using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gucm.IdentityServer.Data.Entities
{
    public class Role : IdentityRole<int>
    {
        public bool IsTrashed { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Updated { get; set; }
        public bool IsSuperAdmin { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<RoleClaim> RoleClaims { get; set; }
    }
}
