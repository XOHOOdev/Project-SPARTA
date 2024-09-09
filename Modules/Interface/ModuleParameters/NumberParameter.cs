using Sparta.Modules.Dto;

namespace Sparta.Modules.Interface.ModuleParameters
{
    public class NumberParameter : ModuleParameterBase
    {
        public override ParameterType GetParameterType()
        {
            return ParameterType.Number;
        }
    }
}
