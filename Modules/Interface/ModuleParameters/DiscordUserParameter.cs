using Sparta.Modules.Dto;

namespace Sparta.Modules.Interface.ModuleParameters
{
    public class DiscordUserParameter : ModuleParameterBase
    {
        public override ParameterType GetParameterType()
        {
            return ParameterType.DiscordUser;
        }
    }
}
