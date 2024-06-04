using Microsoft.AspNetCore.Identity;

namespace Helium.BlazorUI.Data.UserManagementData
{
    public class UserRoleModel
    {
        public IdentityUser User { get; set; } = null!;

        public IEnumerable<UserRoleEntryModel> Roles { get; set; } = null!;
    }

    public class UserRoleEntryModel
    {
        public bool Selected { get; set; }
        public string Name { get; set; } = null!;
        public string Id { get; set; } = null!;
    }
}
