using Sparta.Modules.Interface;
using Sparta.Modules.Interface.ModuleParameters;

namespace Sparta.Modules.HllServerStatus
{
    public class HllServerStatusParameters : ModuleParametersBase
    {
        public ModuleParametersBase ServerId { get; set; } = null!;

        public DiscordChannelParameter DiscordChannel { get; set; } = null!;

        public ModuleParametersBase BattleMetricsId { get; set; } = null!;

        internal ulong DiscordMessage { get; set; }
    }
}
