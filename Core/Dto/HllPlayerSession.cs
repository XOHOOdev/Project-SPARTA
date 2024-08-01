using Newtonsoft.Json;

namespace Sparta.Core.Dto
{
    public class HllPlayerSession
    {
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }

        [JsonProperty(PropertyName = "steam_id_64")]
        public string? SteamId { get; set; }

        [JsonProperty(PropertyName = "start")]
        public DateTimeOffset Start { get; set; }

        [JsonProperty(PropertyName = "end")]
        public DateTimeOffset? End { get; set; }

        [JsonProperty(PropertyName = "created")]
        public DateTimeOffset Created { get; set; }
    }
}
