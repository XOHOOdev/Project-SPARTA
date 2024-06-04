﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Sparta.BlazorUI.Data;
using Sparta.BlazorUI.Entities;

namespace Sparta.BlazorUI.Authorization;

public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public PermissionAuthorizationHandler(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        if (context.User.Identity == null) return;

        var dbContext = scope.ServiceProvider
            .GetRequiredService<ApplicationDbContext<IdentityUser, ApplicationRole, string>>();
        var user = dbContext.Users.FirstOrDefault(x => x.UserName == context.User.Identity.Name);
        var userId = user?.Id;

        if (userId == null) return;

        var permissionService = scope.ServiceProvider.GetRequiredService<IPermissionService>();

        var permissions = permissionService.GetPermissionAsync(userId);

        if (permissions.Contains(requirement.Permission)) context.Succeed(requirement);
    }
}