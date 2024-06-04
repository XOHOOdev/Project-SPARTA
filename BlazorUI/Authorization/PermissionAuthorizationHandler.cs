using Helium.BlazorUI.Data;
using Helium.BlazorUI.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Helium.BlazorUI.Authorization
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public PermissionAuthorizationHandler(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            using IServiceScope scope = _serviceScopeFactory.CreateScope();
            if (context.User.Identity == null) return;

            ApplicationDbContext<IdentityUser, ApplicationRole, string> dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext<IdentityUser, ApplicationRole, string>>();
            IdentityUser? user = dbContext.Users.FirstOrDefault(x => x.UserName == context.User.Identity.Name);
            string? userId = user?.Id;

            if (userId == null) return;

            IPermissionService permissionService = scope.ServiceProvider.GetRequiredService<IPermissionService>();

            var permissions = permissionService.GetPermissionAsync(userId);

            if (permissions.Contains(requirement.Permission))
            {
                context.Succeed(requirement);
            }
        }
    }
}
