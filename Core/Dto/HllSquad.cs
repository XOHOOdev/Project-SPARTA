using Newtonsoft.Json;

namespace Sparta.Core.Dto
{
    public class HllSquad
    {
        [JsonProperty(PropertyName = "players")]
        public IList<HllPlayer>? Players { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string? Type { get; set; }

        [JsonProperty(PropertyName = "has_leader")]
        public bool HasLeader { get; set; }

        [JsonProperty(PropertyName = "combat")]
        public int Combat { get; set; }

        [JsonProperty(PropertyName = "offense")]
        public int Offense { get; set; }

        [JsonProperty(PropertyName = "defense")]
        public int Defense { get; set; }

        [JsonProperty(PropertyName = "support")]
        public int Support { get; set; }

        [JsonProperty(PropertyName = "kills")]
        public int Kills { get; set; }

        [JsonProperty(PropertyName = "deaths")]
        public int Deaths { get; set; }
    }
}
