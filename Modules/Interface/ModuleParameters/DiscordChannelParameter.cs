using Sparta.Modules.Dto;

namespace Sparta.Modules.Interface.ModuleParameters
{
    public class DiscordChannelParameter : ModuleParameterBase
    {
        public override ParameterType GetParameterType()
        {
            return ParameterType.DiscordChannel;
        }
    }
}
