using Sparta.Modules.Dto;

namespace Sparta.Modules.Interface.ModuleParameters
{
    public class BoolParameter : ModuleParameterBase
    {
        public override ParameterType GetParameterType()
        {
            return ParameterType.Bool;
        }
    }
}
