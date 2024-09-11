using Sparta.Core.Dto.Modules;

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
