using Newtonsoft.Json;

namespace Sparta.Core.Dto.Rcon
{
    public class HllPlayerProfileName
    {
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string? Name { get; set; }

        [JsonProperty(PropertyName = "steam_id_64")]
        public string? SteamId { get; set; }

        [JsonProperty(PropertyName = "created")]
        public DateTimeOffset Created { get; set; }

        [JsonProperty(PropertyName = "last_seen")]
        public DateTimeOffset LastSeen { get; set; }
    }
}
