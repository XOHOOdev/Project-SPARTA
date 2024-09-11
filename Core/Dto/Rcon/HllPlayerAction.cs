using Newtonsoft.Json;

namespace Sparta.Core.Dto.Rcon
{
    public class HllPlayerAction
    {
        [JsonProperty(PropertyName = "action_type")]
        public string? Type { get; set; }

        [JsonProperty(PropertyName = "reason")]
        public string? Reason { get; set; }

        [JsonProperty(PropertyName = "by")]
        public string? By { get; set; }

        [JsonProperty(PropertyName = "time")]
        public DateTimeOffset Time { get; set; }
    }
}
