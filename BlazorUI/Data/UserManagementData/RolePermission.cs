using Helium.BlazorUI.Entities;

namespace Helium.BlazorUI.Data.UserManagementData
{
    public class RolePermission
    {
        public ApplicationRole ApplicationRole { get; set; } = null!;
        public IList<IPermissionModel> RolePermissions { get; set; } = null!;
    }

    public interface IPermissionModel { }

    public class BaseRolePermissionModel : IPermissionModel
    {
        public string BaseName { get; set; } = null!;
        public IList<IPermissionModel> RolePermissions { get; set; } = null!;
    }

    public class RolePermissionModel : IPermissionModel
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public bool Selected { get; set; }
    }
}
