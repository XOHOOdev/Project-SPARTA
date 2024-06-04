using Microsoft.AspNetCore.Authorization;

namespace Sparta.BlazorUI.Authorization;

public class HasPermissionAttribute : AuthorizeAttribute
{
    public HasPermissionAttribute(string permission) : base(permission)
    {
    }
}