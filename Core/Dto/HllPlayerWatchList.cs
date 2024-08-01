using Newtonsoft.Json;

namespace Sparta.Core.Dto
{
    public class HllPlayerWatchList
    {
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }

        [JsonProperty(PropertyName = "modified")]
        public DateTimeOffset? Modified { get; set; }

        [JsonProperty(PropertyName = "steam_id_64")]
        public long SteamId { get; set; }

        [JsonProperty(PropertyName = "is_watched")]
        public bool IsWatched { get; set; }

        [JsonProperty(PropertyName = "reason")]
        public string? Reason { get; set; }

        [JsonProperty(PropertyName = "by")]
        public string? By { get; set; }

        [JsonProperty(PropertyName = "count")]
        public int Count { get; set; }
    }
}
