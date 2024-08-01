using Newtonsoft.Json;

namespace Sparta.Core.Dto
{
    public class HllPlayerPenalties
    {
        [JsonProperty(PropertyName = "KICK")]
        public int Kick { get; set; }

        [JsonProperty(PropertyName = "PUNISH")]
        public int Punish { get; set; }

        [JsonProperty(PropertyName = "TEMPBAN")]
        public int TempBan { get; set; }

        [JsonProperty(PropertyName = "PERMABAN")]
        public int PermanentBan { get; set; }
    }
}
