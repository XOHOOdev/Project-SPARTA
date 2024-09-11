using Newtonsoft.Json;

namespace Sparta.Core.Dto.Rcon
{
    public class HllTeam
    {
        [JsonProperty(PropertyName = "squads")]
        public Dictionary<string, HllSquad>? Squads { get; set; }

        [JsonProperty(PropertyName = "commander")]
        public HllPlayer? Commander { get; set; }

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

        [JsonProperty(PropertyName = "count")]
        public int Count { get; set; }

        public List<HllPlayer> GetPlayers()
        {
            var players = Squads?.Select(s => s.Value).SelectMany(p => p.Players ?? []).ToList();
            if (Commander != null) players?.Add(Commander);

            return players ?? [];
        }

        public int GetAverageLevel()
        {
            var avgLevel = GetPlayers().Average(p => p.Level);

            return (int)Math.Round(avgLevel);
        }

        public int GetVips()
        {
            return GetPlayers().Count(p => p.IsVip);
        }

        public int GetTaurusPlayers()
        {
            return GetPlayers().Count(p => p.Name?.StartsWith("Λ") ?? false);
        }

        public int GetPlayersInTeam(List<long> steamIds)
        {
            return GetPlayers().Count(p => long.TryParse(p.SteamId, out var id) && steamIds.Contains(id));
        }
    }
}
