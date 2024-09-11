using Newtonsoft.Json;

namespace Sparta.Core.Dto.Rcon
{
    public class HllSteamBan
    {
        public long SteamId { get; set; }

        [JsonProperty(PropertyName = "VACBanned")]
        public bool VacBanned { get; set; }

        public string? EconomyBan { get; set; }

        public bool CommunityBanned { get; set; }

        [JsonProperty(PropertyName = "NumberOfVACBans")]
        public int NumberOfVacBans { get; set; }

        public int DaysSinceLastBan { get; set; }

        public int NumberOfGameBans { get; set; }
    }
}
