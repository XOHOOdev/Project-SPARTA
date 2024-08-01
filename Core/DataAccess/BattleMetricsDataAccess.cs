using Newtonsoft.Json;

namespace Sparta.Core.DataAccess
{
    public class BattleMetricsDataAccess
    {
        public long GetBattlemetricsRank(string baseAddress, long serverId)
        {
            using HttpClient httpClient = new HttpClient { BaseAddress = new Uri(baseAddress) };
            var response = httpClient.GetAsync($"servers/{serverId}").Result;
            response.EnsureSuccessStatusCode();

            var jsonString = response.Content.ReadAsStringAsync().Result;
            var dynamicObject = JsonConvert.DeserializeObject<dynamic>(jsonString);
            if (dynamicObject == null) return -1;
            return (long)dynamicObject.data.attributes.rank.Value;
        }
    }
}
