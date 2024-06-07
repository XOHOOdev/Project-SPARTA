using Sparta.Modules.Dto;
using System.Reflection;

namespace Sparta.Modules.Interface
{
    public abstract class ModuleParameterBase : IModuleParameters
    {
        public IEnumerable<ParamInfo> AllParameters
        {
            get
            {
                return ((TypeInfo)GetType()).GetProperties().Where(p => !string.Equals(p.Name, nameof(AllParameters),
                    StringComparison.Ordinal)).Select(p => new ParamInfo
                    {
                        Name = p.Name,
                        Type = p.PropertyType,
                        Content = p.GetValue(this)?.ToString() ?? string.Empty
                    });
            }
            set
            {
                foreach (var paramInfo in value)
                {
                    var prop = ((TypeInfo)GetType()).GetProperty(paramInfo.Name);
                    if (prop == null) return;
                    prop.SetValue(this, Convert.ChangeType(paramInfo.Content, prop.PropertyType));
                }
            }
        }
    }
}
