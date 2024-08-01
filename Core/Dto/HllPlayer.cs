using Newtonsoft.Json;

namespace Sparta.Core.Dto
{
    public class HllPlayer
    {
        [JsonProperty(PropertyName = "name")]
        public string? Name { get; set; }

        [JsonProperty(PropertyName = "steam_id_64")]
        public string? SteamId { get; set; }

        [JsonProperty(PropertyName = "country")]
        public string? Country { get; set; }

        [JsonProperty(PropertyName = "steam_bans")]
        public HllSteamBan? SteamBan { get; set; }

        [JsonProperty(PropertyName = "profile")]
        public HllPlayerProfile? Profile { get; set; }

        [JsonProperty(PropertyName = "is_vip")]
        public bool IsVip { get; set; }

        [JsonProperty(PropertyName = "unit_id")]
        public string? UnitId { get; set; }

        [JsonProperty(PropertyName = "unit_name")]
        public string? UnitName { get; set; }

        [JsonProperty(PropertyName = "loadout")]
        public string? LoadOut { get; set; }

        [JsonProperty(PropertyName = "team")]
        public string? Team { get; set; }

        [JsonProperty(PropertyName = "role")]
        public string? Role { get; set; }

        [JsonProperty(PropertyName = "kills")]
        public int Kills { get; set; }

        [JsonProperty(PropertyName = "deaths")]
        public int Deaths { get; set; }

        [JsonProperty(PropertyName = "combat")]
        public int Combat { get; set; }

        [JsonProperty(PropertyName = "offense")]
        public int Offense { get; set; }

        [JsonProperty(PropertyName = "defense")]
        public int Defense { get; set; }

        [JsonProperty(PropertyName = "support")]
        public int Support { get; set; }

        [JsonProperty(PropertyName = "level")]
        public int Level { get; set; }
    }
}
