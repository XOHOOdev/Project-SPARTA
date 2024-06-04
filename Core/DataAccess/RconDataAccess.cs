using Sparta.Core.Helpers;
using System.Text.Json.Nodes;

namespace Sparta.Core.DataAccess
{
    public class RconDataAccess
    {
        private readonly HttpClient _httpClient = new() { BaseAddress = new Uri($"{ConfigHelper.GetConfig("RCON", "Address")}:{ConfigHelper.GetConfig("RCON", "Port")}/api/") };

        public void Login(string username, string password)
        {
            JsonObject json = new()
            {
                { "username", username },
                { "password", password }
            };

            var result = _httpClient.PostAsync("login", new StringContent(System.Text.Json.JsonSerializer.Serialize(json))).Result;
            result.EnsureSuccessStatusCode();
        }
    }
}
