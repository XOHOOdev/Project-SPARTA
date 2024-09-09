using Sparta.Modules.Dto;
using Sparta.Modules.Interface.ModuleParameters;
using System.Reflection;

namespace Sparta.Modules.Interface
{
    public abstract class ModuleParametersBase : IModuleParameters
    {
        public IEnumerable<ParamInfo> AllParameters
        {
            get
            {
                return from prop in ((TypeInfo)GetType()).GetProperties().Where(p => !string.Equals(p.Name,
                        nameof(AllParameters),
                        StringComparison.Ordinal))
                       let par = (prop.GetValue(this) ?? Activator.CreateInstance(prop.PropertyType)) as ModuleParameterBase
                       select new ParamInfo { Name = prop.Name, Type = par?.GetParameterType() ?? ParameterType.Text, Content = par?.Content ?? string.Empty };
            }
            set
            {
                foreach (var paramInfo in value)
                {
                    var prop = ((TypeInfo)GetType()).GetProperty(paramInfo.Name);
                    if (prop == null || Activator.CreateInstance(prop.PropertyType) is not ModuleParameterBase property) continue;

                    property.Content = paramInfo.Content;

                    prop.SetValue(this, property);
                }
            }
        }
    }
}
