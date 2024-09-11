using Newtonsoft.Json;
using Sparta.Core.DataAccess.DatabaseAccess.Entities;
using Sparta.Core.Dto.Rcon;
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

        private T PostToAPI<T>(string url, int port, string username, string password, string requestUri, JsonObject json) where T : new()
        {
            HttpClient client = new() { BaseAddress = new Uri($"{url}:{port}/api/") };
            Login(client, username, password);

            var response = client.PostAsync(requestUri, new StringContent(System.Text.Json.JsonSerializer.Serialize(json))).Result;

            Logout(client);

            return new T();
        }

        public HllServerInfo GetServerInfo(Server server) => GetFromApi<HllServerInfo>(server.Url, server.Port, server.Username, server.Password, "public_info");

        public HllTeamView GetTeamView(Server server) => GetFromApi<HllTeamView>(server.Url, server.Port, server.Username, server.Password, "get_team_view");

        public List<HllMod> GetInGameMods(Server server) => GetFromApi<List<HllMod>>(server.Url, server.Port, server.Username, server.Password, "get_ingame_mods");

        public void SendMessage(Server server, string steamId, string message, string by = "")
        {
            JsonObject jsonObject = new()
            {
                {"steam_id_64",steamId},
                {"message",message},
                {"by",by}
            };

            PostToAPI<HllTeamView>(server.Url, server.Port, server.Username, server.Password, "do_message_player", jsonObject);
        }
    }
}
