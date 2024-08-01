using Newtonsoft.Json;

namespace Sparta.Core.Dto
{
    public class HllPlayerBlacklist
    {
        [JsonProperty(PropertyName = "steam_id_64")]
        public long SteamId { get; set; }

        [JsonProperty(PropertyName = "is_blacklisted")]
        public bool IsBlacklisted { get; set; }

        [JsonProperty(PropertyName = "reason")]
        public string? Reason { get; set; }

        [JsonProperty(PropertyName = "by")]
        public string? By { get; set; }
    }
}
