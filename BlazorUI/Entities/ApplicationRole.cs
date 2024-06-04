using Microsoft.AspNetCore.Identity;

namespace Helium.BlazorUI.Entities
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole(string name)
        {
            Name = name;
            NormalizedName = name.ToUpper();
        }

        public virtual List<Permission> Permissions { get; } = new();
    }
}
