using Newtonsoft.Json;

namespace Sparta.Core.Dto.Rcon
{
    public class HllPlayerFlag
    {
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }

        [JsonProperty(PropertyName = "flag")]
        public string? Flag { get; set; }

        [JsonProperty(PropertyName = "comment")]
        public string? Comment { get; set; }

        [JsonProperty(PropertyName = "modified")]
        public DateTimeOffset Modified { get; set; }
    }
}
