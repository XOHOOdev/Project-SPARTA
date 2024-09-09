using Sparta.Modules.Dto;

namespace Sparta.Modules.Interface.ModuleParameters
{
    public abstract class ModuleParameterBase
    {
        public string Content = null!;

        public virtual ParameterType GetParameterType()
        {
            return 0;
        }
    }
}
