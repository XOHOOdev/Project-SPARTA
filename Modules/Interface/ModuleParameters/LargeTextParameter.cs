using Sparta.Modules.Dto;

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
