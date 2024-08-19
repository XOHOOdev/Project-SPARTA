using Sparta.Modules.Interface.ModuleParameters;

namespace Sparta.Modules.MapVote
{
    public class MapVoteParameters : Interface.ModuleParametersBase
    {
        public required DiscordChannelParameter DiscordChannel { get; set; }

        public required DiscordUserParameter Rep1 { get; set; }

        public required DiscordUserParameter Rep2 { get; set; }

        public required DiscordRoleParameter Team1 { get; set; }

        public required DiscordRoleParameter Team2 { get; set; }

        internal ulong DiscordMessage { get; set; }

        internal bool? Sides { get; set; } = null;

        internal string Votes { get; set; } = string.Empty;
    }
}
