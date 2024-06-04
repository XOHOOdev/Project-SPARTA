using Helium.BlazorUI.Data;
using Helium.BlazorUI.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Helium.BlazorUI.Authorization
{
    public class PermissionService : IPermissionService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public PermissionService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public HashSet<string> GetPermissionAsync(string memberId)
        {
            using var scope = _serviceScopeFactory.CreateScope();

            ApplicationDbContext<IdentityUser, ApplicationRole, string> context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext<IdentityUser, ApplicationRole, string>>();

            var roleIds = context.UserRoles.Where(x => x.UserId == memberId).Select(x => x.RoleId);
            return context.US_Permissions.Where(x => x.Roles.Any(y => roleIds.Contains(y.Id))).Select(x => x.Name).ToHashSet();
        }
    }
}
