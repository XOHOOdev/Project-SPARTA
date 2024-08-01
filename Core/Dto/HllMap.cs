using Newtonsoft.Json;

namespace Sparta.Core.Dto
{
    public class HllMap
    {
        [JsonProperty(PropertyName = "just_name")]
        public string? SimpleName { get; set; }

        [JsonProperty(PropertyName = "human_name")]
        public string? Name { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string? Tag { get; set; }

        [JsonProperty(PropertyName = "start")]
        public long? Start { get; set; }

        [JsonIgnore]
        public DateTimeOffset StartTime
        {
            get
            {
                if (Start == null) return DateTimeOffset.Now;

                DateTime dateTime = new(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                return dateTime.AddSeconds(Start.Value).ToLocalTime();
            }
        }
    }
}
