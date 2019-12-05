using System.Threading.Tasks;
using System.Linq;
using System.Security.Claims;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Extensions;
using Gucm.IdentityServer.Data.Entities;

namespace Gucm.IdentityServer.Services
{
    public class ProfileService : IProfileService
    {
        private readonly UserManager<User> userManager;

        private readonly RoleManager<Role> roleManager;

        public ProfileService(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            this.userManager = userManager;

            this.roleManager = roleManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var subjectId = context.Subject.GetSubjectId();
            var user = await userManager.FindByEmailAsync(subjectId);
            var userClaims = await userManager.GetClaimsAsync(user);

            if (userClaims != null && userClaims.Count > 0)
            {
                var role = await GetRole(user);
                var userGroupId = role != null ? role.Id : 0;

                var claims = new List<Claim>()
                {
                    new Claim(JwtRegisteredClaimNames.UniqueName, user.Email),
                    new Claim("Id", user.Id.ToString()),
                    new Claim("Guid", user.Guid.ToString()),
                    new Claim("FirstName", user.FirstName),
                    new Claim("LastName", user.LastName),
                    new Claim("Email", user.Email),
                    new Claim("UserGroupId", userGroupId.ToString()),
                    new Claim("role", role.Name)
                };

                claims.AddRange(userClaims);
                context.IssuedClaims.AddRange(claims);
            }
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var subjectId = context.Subject.GetSubjectId();
            var user = await userManager.FindByEmailAsync(subjectId);
            context.IsActive = (user != null && !user.IsDeleted);
        }

        private async Task<Role> GetRole(User user)
        {
            Role role = null;
            var roleNames = await userManager.GetRolesAsync(user);
            if (roleNames != null && roleNames.Count > 0)
                role = await roleManager.Roles.FirstOrDefaultAsync(x => x.Name == roleNames.FirstOrDefault());
            return role;
        }
    }
}
