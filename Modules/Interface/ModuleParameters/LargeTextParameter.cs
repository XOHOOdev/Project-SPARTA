using Sparta.Core.Dto.Modules;

namespace Sparta.Modules.Interface.ModuleParameters
{
    public class LargeTextParameter : ModuleParameterBase
    {
        public override ParameterType GetParameterType()
        {
            return ParameterType.LargeText;
        }
    }
}
