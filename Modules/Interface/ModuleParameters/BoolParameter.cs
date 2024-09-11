using Sparta.Core.Dto.Modules;

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
