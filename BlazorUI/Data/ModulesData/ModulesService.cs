using Microsoft.AspNetCore.Identity;
using Sparta.BlazorUI.Authorization;
using Sparta.BlazorUI.Entities;

namespace Sparta.BlazorUI.Data.ModulesData
{
    [HasPermission(Permissions.Permissions.Modules.View)]
    public class ModulesService(ApplicationDbContext<IdentityUser, ApplicationRole, string> context)
    {
        public IEnumerable<ModuleCategory> GetModuleCategories()
        {
            return Modules.Modules.GetModules().Select(x => new ModuleCategory
            {
                Name = x,
                Modules = context.MD_Modules.Where(m => m.Name == x)
            });
        }
    }
}
