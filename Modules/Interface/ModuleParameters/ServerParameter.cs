using Sparta.Modules.Dto;

namespace Sparta.Modules.Interface.ModuleParameters
{
    public class ServerParameter : ModuleParameterBase
    {
        public override ParameterType GetParameterType()
        {
            return ParameterType.HllServer;
        }
    }
}
