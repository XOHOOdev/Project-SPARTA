using Sparta.Modules.Interface;

namespace Sparta.Modules.HllServerStatus
{
    public class HllServerStatusParameters : ModuleParameterBase
    {
        public long ServerId { get; set; }

        public ulong DiscordChannel { get; set; }

        public long BattleMetricsId { get; set; }

        internal ulong DiscordMessage { get; set; }
    }
}
