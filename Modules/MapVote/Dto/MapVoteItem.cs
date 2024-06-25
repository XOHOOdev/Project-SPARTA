using Newtonsoft.Json;

namespace Sparta.Modules.MapVote.Dto
{
    internal class MapVoteItem
    {
        [JsonProperty]
        internal string MapName { get; set; } = null!;

        [JsonProperty]
        internal MapVoteState State { get; set; }
    }

    internal enum MapVoteState
    {
        Unknown = 0,
        Team1 = 1,
        Team2 = 2
    }
}
