using Helium.BlazorUI.Authorization;
using Helium.BlazorUI.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Helium.BlazorUI.Data.UserManagementData
{
    [HasPermission(Permissions.Permissions.UserManagement.View)]
    public class UserManagementService
    {
        private readonly ApplicationDbContext<IdentityUser, ApplicationRole, string> _context;

        public UserManagementService(ApplicationDbContext<IdentityUser, ApplicationRole, string> context)
        {
            _context = context;
        }

        public IEnumerable<IdentityUser> GetUsers() => _context.Users;

        public IEnumerable<ApplicationRole> GetRoles() => _context.Roles;

        public async Task AddNewRoleAsync(string? name)
        {
            if (name != null && (_context.Roles.FirstOrDefault(x => x.Name == name)) == null)
            {
                await _context.AddAsync(new ApplicationRole(name.Trim()));
            }
            _context.SaveChanges();
        }

        public void DeleteRole(ApplicationRole role)
        {
            if (role != null)
            {
                _context.Roles.Remove(role);
            }
            _context.SaveChanges();
        }

        public void DeleteUser(IdentityUser user)
        {
            if (user != null)
            {
                _context.Users.Remove(user);
            }
            _context.SaveChanges();
        }

        public RolePermission GetPermissions(ApplicationRole role)
        {
            return new RolePermission { ApplicationRole = role, RolePermissions = GetPermissions(typeof(Permissions.Permissions).GetNestedTypes(), role) };
        }

        private List<IPermissionModel> GetPermissions(IEnumerable<Type> typePermissions, ApplicationRole role)
        {
            List<IPermissionModel> permissions = new();

            foreach (var permission in typePermissions)
            {
                var subPermissions = new List<IPermissionModel>();

                subPermissions.AddRange(permission.GetFields().Select(x =>
                {
                    var dbPermission = _context.US_Permissions.First(y => y.Name == ((x.GetValue(null) as string) ?? ""));
                    return new RolePermissionModel
                    {
                        Id = dbPermission.Id,
                        Name = x.Name,
                        Selected = role.Permissions.Any(x => x.Id == dbPermission.Id)
                    };
                }));

                subPermissions.AddRange(GetPermissions(permission.GetNestedTypes(), role));

                permissions.Add(new BaseRolePermissionModel
                {
                    BaseName = permission.Name,
                    RolePermissions = subPermissions
                });
            }

            return permissions;
        }

        public void SavePermissions(RolePermission rolePermissionModel)
        {
            ApplicationRole? role = _context.Roles.FirstOrDefault(x => x.Id == rolePermissionModel.ApplicationRole.Id);
            if (role == null) return;
            SavePermissions(rolePermissionModel.RolePermissions, role);
            _context.SaveChanges();
        }

        private void SavePermissions(IList<IPermissionModel> permissions, ApplicationRole role)
        {
            foreach (IPermissionModel permission in permissions)
            {
                if (permission is BaseRolePermissionModel basePermission)
                {
                    SavePermissions(basePermission.RolePermissions, role);
                    continue;
                }
                if (permission is not RolePermissionModel rolePermission) continue;

                bool roleHasPermission = role.Permissions.Any(x => x.Id == rolePermission.Id);
                if (rolePermission.Selected && !roleHasPermission)
                {
                    role.Permissions.Add(_context.US_Permissions.First(x => x.Id == rolePermission.Id));
                }
                else if (!rolePermission.Selected && roleHasPermission)
                {
                    var permissionsToRemove = role.Permissions.FirstOrDefault(x => x.Id == rolePermission.Id);
                    if (permissionsToRemove != null)
                    {
                        role.Permissions.Remove(permissionsToRemove);
                    }
                }
            }
        }

        public UserRoleModel GetUserRoles(IdentityUser user)
        {
            var roleModels = new List<UserRoleEntryModel>();
            foreach (var role in _context.Roles)
            {
                if (role == null) continue;
                var userRolesViewModel = new UserRoleEntryModel
                {
                    Name = role.Name ?? "",
                    Selected = _context.UserRoles.Any(x => x.UserId == user.Id && x.RoleId == role.Id),
                    Id = role.Id
                };
                roleModels.Add(userRolesViewModel);
            }
            return new UserRoleModel()
            {
                User = user,
                Roles = roleModels
            };
        }

        public void SaveUserRoles(UserRoleModel userRoleModel)
        {
            _context.UserRoles.Where(x => x.UserId == userRoleModel.User.Id).ExecuteDelete();
            foreach (var role in userRoleModel.Roles)
            {
                if (role.Selected)
                {
                    _context.UserRoles.Add(new IdentityUserRole<string> { RoleId = role.Id, UserId = userRoleModel.User.Id });
                }
            }
            _context.SaveChanges();
        }

        public UserSteamId GetSteamId(IdentityUser user)
        {
            return new UserSteamId
            {
                User = user,
                SteamId = _context.US_SteamIds.FirstOrDefault(x => x.UserId == user.Id)?.SteamId ?? 0
            };
        }

        public void SaveUserSteamId(UserSteamId userSteam)
        {
            var userSteamID = _context.US_SteamIds.FirstOrDefault(x => x.UserId == userSteam.User.Id);
            if (userSteamID == null)
            {
                userSteamID = new Entities.UserSteamId { UserId = userSteam.User.Id };
                _context.US_SteamIds.Add(userSteamID);
            }
            userSteamID.SteamId = userSteam.SteamId;
            _context.SaveChanges();
        }
    }
}
