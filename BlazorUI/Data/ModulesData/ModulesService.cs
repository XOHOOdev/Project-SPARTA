using Microsoft.AspNetCore.Identity;
using Sparta.BlazorUI.Authorization;
using Sparta.BlazorUI.Entities;
using Sparta.Modules.Dto;
using Sparta.Modules.Interface;
using Sparta.Modules.Interface.ModuleParameters;
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

        public ModuleParametersBase? GetModuleParameters(string moduleName)
        {
            var type = typeof(Modules.Modules).Assembly
                .GetTypes()
                .First(t =>
                    t.Namespace != null && t.Namespace.Contains(moduleName) && t.FullName != null &&
                    t.FullName.EndsWith("Parameters"));

            return Activator.CreateInstance(type) as ModuleParametersBase;
        }

        public ModuleParametersBase? GetModuleParameters(Module module)
        {
            var type = typeof(Modules.Modules).Assembly
                .GetTypes()
            .First(t =>
                    t.Namespace != null && t.Namespace.Contains(module.Type.Name) && t.FullName != null &&
                    t.FullName.EndsWith("Parameters"));

            if (Activator.CreateInstance(type) is not ModuleParametersBase parameters) return null;

            foreach (var paramInfo in module.Parameters)
            {
                var prop = ((TypeInfo)parameters.GetType()).GetProperty(paramInfo.Name);
                if (prop == null || Activator.CreateInstance(prop.PropertyType) is not ModuleParameterBase property) continue;

                property.Content = paramInfo.Value;

                prop.SetValue(parameters, property);
            }

            return parameters;
        }

        public void CreateModule(ModuleParametersBase parameters, ModuleCategory category)
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

        public void SetModuleParameters(Module module, ModuleParametersBase parameters)
        {
            module.Parameters.ForEach(p => p.Value = parameters.AllParameters.First(x => x.Name == p.Name).Content);

            SaveChanges();
        }

        public void SetOptions(ref List<ParamInfo> parameters)
        {
            if (parameters.Any(p => p.Type > 0)) parameters.Insert(0, new ParamInfo { Name = "DiscordGuild", Type = (int)ParameterType.DiscordGuild });

            foreach (var parameter in parameters)
            {
                switch ((ParameterType)parameter.Type)
                {
                    case ParameterType.String:
                        break;
                    case ParameterType.DiscordChannel:
                        parameter.Options = context.DC_Channels.ToArray();
                        break;
                    case ParameterType.DiscordUser:
                        parameter.Options = context.DC_Users.ToArray();
                        break;
                    case ParameterType.DiscordRole:
                        parameter.Options = context.DC_Roles.ToArray();
                        break;
                    case ParameterType.DiscordGuild:
                        parameter.Options = context.DC_Guilds.ToArray();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
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
