using Sparta.Modules.Interface;

namespace Sparta.Modules.MapVote
{
    public class MapVoteParameters : ModuleParameterBase
    {
        public ulong DiscordChannel { get; set; }

        public ulong Rep1 { get; set; }

        public ulong Rep2 { get; set; }

        public ulong Team1 { get; set; }

        public ulong Team2 { get; set; }

        internal ulong DiscordMessage { get; set; }

        internal bool? Sides { get; set; } = null;

        internal string Votes { get; set; } = string.Empty;
    }
}
