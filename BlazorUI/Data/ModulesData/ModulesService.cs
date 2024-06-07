using Microsoft.AspNetCore.Identity;
using Sparta.BlazorUI.Authorization;
using Sparta.BlazorUI.Entities;
using Sparta.Modules.Interface;

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
                Modules = context.MD_Modules.Where(m => m.Type.Name == x)
            });
        }

        public ModuleParameterBase? GetModuleParameters(string moduleName)
        {
            var type = typeof(Modules.Modules).Assembly
                .GetTypes()
                .First(t =>
                    t.Namespace != null && t.Namespace.Contains(moduleName) && t.FullName != null &&
                    t.FullName.EndsWith("Parameters"));

            return Activator.CreateInstance(type) as ModuleParameterBase;
        }

        public void CreateModule(ModuleParameterBase parameters, ModuleCategory category)
        {
            var moduleType = context.MD_ModuleType.FirstOrDefault(t => t.Name == category.Name) ?? context.Add(new ModuleType { Name = category.Name }).Entity;

            context.Add(new Module
            {
                Name = "New Module",
                Enabled = false,
                Parameters = parameters.AllParameters.Select(p => new ModuleParameter
                {
                    Name = p.Name,
                    Value = p.Content
                }).ToList(),
                Type = moduleType
            });

            context.SaveChanges();
        }
    }
}
