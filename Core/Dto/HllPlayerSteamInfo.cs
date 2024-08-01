using Newtonsoft.Json;

namespace Sparta.Core.Dto
{
    public class HllPlayerSteamInfo
    {
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }

        [JsonProperty(PropertyName = "created")]
        public DateTimeOffset Created { get; set; }

        [JsonProperty(PropertyName = "updated")]
        public DateTimeOffset? Updated { get; set; }

        [JsonProperty(PropertyName = "profile")]
        public HllPlayerSteamProfile? Profile { get; set; }

        [JsonProperty(PropertyName = "country")]
        public string? Country { get; set; }

        [JsonProperty(PropertyName = "bans")]
        public HllSteamBan? Bans { get; set; }

        [JsonProperty(PropertyName = "has_bans")]
        public bool HasBans { get; set; }
    }
}
