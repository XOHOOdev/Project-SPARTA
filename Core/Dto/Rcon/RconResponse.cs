using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Sparta.Core.Dto.Rcon
{
    public class RconResponse<T>
    {
        [JsonProperty(PropertyName = "result")]
        public T? Result { get; set; }

        [JsonProperty(PropertyName = "command")]
        public string? Command { get; set; }

        [JsonProperty(PropertyName = "arguments")]
        public JObject? Arguments { get; set; }

        [JsonProperty(PropertyName = "failed")]
        public bool Failed { get; set; }

        [JsonProperty(PropertyName = "error")]
        public string? Error { get; set; }

        [JsonProperty(PropertyName = "forwards_results")]
        public string? ForwardResults { get; set; }

        [JsonProperty(PropertyName = "version")]
        public string? Version { get; set; }
    }
}
