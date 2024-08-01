using Newtonsoft.Json;
using Sparta.Core.Dto;
using System.Text.Json.Nodes;

namespace Sparta.Core.DataAccess
{
    public class RconDataAccess
    {
        private void Login(HttpClient client, string username, string password)
        {
            JsonObject json = new()
            {
                { "username", username },
                { "password", password }
            };

            var result = client.PostAsync("login", new StringContent(System.Text.Json.JsonSerializer.Serialize(json))).Result;
            result.EnsureSuccessStatusCode();
        }

        private void Logout(HttpClient client)
        {
            var result = client.GetAsync("logout").Result;
            result.EnsureSuccessStatusCode();
        }

        private T GetFromApi<T>(string url, int port, string username, string password, string requestUri) where T : new()
        {
            HttpClient client = new() { BaseAddress = new Uri($"{url}:{port}/api/") };
            Login(client, username, password);

            var response = client.GetAsync(requestUri).Result;
            var jsonString = response.Content.ReadAsStringAsync().Result;

            var deserializedResponse = JsonConvert.DeserializeObject<RconResponse<T>>(jsonString);
            if (deserializedResponse == null || deserializedResponse.Result == null) return new T();

            Logout(client);

            return deserializedResponse.Result;
        }

        public HllServerInfo GetServerInfo(string url, int port, string username, string password) => GetFromApi<HllServerInfo>(url, port, username, password, "public_info");

        public HllTeamView GetTeamView(string url, int port, string username, string password) => GetFromApi<HllTeamView>(url, port, username, password, "get_team_view");

        public List<HllMod> GetIngameMods(string url, int port, string username, string password) => GetFromApi<List<HllMod>>(url, port, username, password, "get_ingame_mods");
    }
}
