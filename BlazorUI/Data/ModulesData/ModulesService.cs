using Microsoft.AspNetCore.Identity;
using Sparta.BlazorUI.Authorization;
using Sparta.BlazorUI.Entities;
using Sparta.Modules.Interface;
using System.Reflection;
using Module = Sparta.BlazorUI.Entities.Module;

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

        public ModuleParameterBase? GetModuleParameters(Module module)
        {
            var type = typeof(Modules.Modules).Assembly
                .GetTypes()
            .First(t =>
                    t.Namespace != null && t.Namespace.Contains(module.Type.Name) && t.FullName != null &&
                    t.FullName.EndsWith("Parameters"));

            if (Activator.CreateInstance(type) is not ModuleParameterBase parameters) return null;

            foreach (var paramInfo in module.Parameters)
            {
                var prop = ((TypeInfo)parameters.GetType()).GetProperty(paramInfo.Name);
                if (prop == null) return null;
                prop.SetValue(parameters, Convert.ChangeType(paramInfo.Value, prop.PropertyType));
            }

            return parameters;
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

            SaveChanges();
        }

        public void SetModuleParameters(Module module, ModuleParameterBase parameters)
        {
            module.Parameters.ForEach(p => p.Value = parameters.AllParameters.First(x => x.Name == p.Name).Content);

            SaveChanges();
        }

        public void DeleteModule(Module module)
        {
            context.Remove(module);

            SaveChanges();
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
