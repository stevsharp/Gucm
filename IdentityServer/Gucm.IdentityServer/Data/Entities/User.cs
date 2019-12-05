using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Gucm.IdentityServer.Data.Entities
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public int DepartmentId { get; set; }
        public string JobTitle { get; set; }
        public int UserGroupId { get; set; }
        public bool IsTrashed { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Updated { get; set; }
        public Guid Guid { get; set; }
        public string EmailConfirmationToken { get; set; }
        public string ResetPasswordToken { get; set; }
        public virtual ICollection<UserClaim> Claims { get; set; }
        public virtual ICollection<UserLogin> Logins { get; set; }
        public virtual ICollection<UserToken> Tokens { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}