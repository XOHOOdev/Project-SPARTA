using Newtonsoft.Json;

namespace Sparta.Core.Dto.Rcon
{
    public class HllServerInfo
    {
        [JsonProperty(PropertyName = "current_map")]
        public HllMap? CurrentMap { get; set; }

        [JsonProperty(PropertyName = "next_map")]
        public HllMap? NextMap { get; set; }

        [JsonProperty(PropertyName = "player_count")]
        public int PlayerCount { get; set; }

        [JsonProperty(PropertyName = "max_player_count")]
        public int PlayersMax { get; set; }

        [JsonProperty(PropertyName = "players")]
        public Dictionary<string, int>? Players { get; set; }

        [JsonProperty(PropertyName = "score")]
        public Dictionary<string, int>? Score { get; set; }

        [JsonProperty(PropertyName = "raw_time_remaining")]
        public string? TimeRemaining { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string? Title { get; set; }

        [JsonProperty(PropertyName = "short_name")]
        public string? ShortTitle { get; set; }

        [JsonProperty(PropertyName = "public_stats_port")]
        public string? StatsPort { get; set; }

        [JsonProperty(PropertyName = "public_stats_port_https")]
        public string? SslStatsPort { get; set; }
    }
}
