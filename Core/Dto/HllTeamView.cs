using Newtonsoft.Json;

namespace Sparta.Core.Dto
{
    public class HllTeamView
    {
        [JsonProperty(PropertyName = "fail_count")]
        public int FailCount { get; set; }

        [JsonProperty(PropertyName = "none")]
        public HllTeam? None { get; set; }

        [JsonProperty(PropertyName = "allies")]
        public HllTeam? Allies { get; set; }

        [JsonProperty(PropertyName = "axis")]
        public HllTeam? Axis { get; set; }
    }
}
