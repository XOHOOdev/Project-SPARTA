using Newtonsoft.Json;

namespace Sparta.Core.Dto
{
    public class HllPlayerVip
    {
        [JsonProperty(PropertyName = "server_number")]
        public int ServerNumber { get; set; }

        [JsonProperty(PropertyName = "expiration")]
        public DateTimeOffset Expiration { get; set; }
    }
}
