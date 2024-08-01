using Newtonsoft.Json;

namespace Sparta.Core.Dto
{
    public class HllPlayerProfile
    {
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }

        [JsonProperty(PropertyName = "steam_id_64")]
        public string? SteamId { get; set; }

        [JsonProperty(PropertyName = "created")]
        public DateTimeOffset Created { get; set; }

        [JsonProperty(PropertyName = "names")]
        public IList<HllPlayerProfileName>? Names { get; set; }

        [JsonProperty(PropertyName = "sessions")]
        public IList<HllPlayerSession>? Sessions { get; set; }

        [JsonProperty(PropertyName = "sessions_count")]
        public int SessionCount { get; set; }

        [JsonProperty(PropertyName = "total_playtime_seconds")]
        public int TotalPlaytime { get; set; }

        [JsonProperty(PropertyName = "current_playtime_seconds")]
        public int CurrentPlaytime { get; set; }

        [JsonProperty(PropertyName = "received_actions")]
        public IList<HllPlayerAction>? ReceivedActions { get; set; }

        [JsonProperty(PropertyName = "penalty_count")]
        public HllPlayerPenalties? PenaltyCount { get; set; }

        [JsonProperty(PropertyName = "blacklist")]
        public HllPlayerBlacklist? BlackList { get; set; }

        [JsonProperty(PropertyName = "flags")]
        public IList<HllPlayerFlag>? Flags { get; set; }

        [JsonProperty(PropertyName = "watchlist")]
        public HllPlayerWatchList? WatchList { get; set; }

        [JsonProperty(PropertyName = "steaminfo")]
        public HllPlayerSteamInfo? SteamInfo { get; set; }

        [JsonProperty(PropertyName = "vips")]
        public IList<HllPlayerVip>? Vips { get; set; }
    }
}
