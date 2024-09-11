using Newtonsoft.Json;

namespace Sparta.Core.Dto.Rcon
{
    public class HllMod
    {
        [JsonProperty(PropertyName = "username")]
        public string? Username { get; set; }

        [JsonProperty(PropertyName = "steam_id_64")]
        public string? SteamId { get; set; }
    }
}
