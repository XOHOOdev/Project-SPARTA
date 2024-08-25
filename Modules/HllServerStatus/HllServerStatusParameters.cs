using Sparta.Modules.Interface;
using Sparta.Modules.Interface.ModuleParameters;

namespace Sparta.Modules.HllServerStatus
{
    public class HllServerStatusParameters : ModuleParametersBase
    {
        public StringParameter ServerId { get; set; } = null!;

        public DiscordChannelParameter DiscordChannel { get; set; } = null!;

        public StringParameter BattleMetricsId { get; set; } = null!;

        internal ulong DiscordMessage { get; set; }
    }
}
