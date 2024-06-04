using Microsoft.AspNetCore.Authorization;

namespace Helium.BlazorUI.Authorization
{
    public class HasPermissionAttribute : AuthorizeAttribute
    {
        public HasPermissionAttribute(string permission) : base(policy: permission) { }
    }
}
