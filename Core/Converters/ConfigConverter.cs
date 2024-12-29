using Newtonsoft.Json.Linq;
using Sparta.Core.DataAccess.DatabaseAccess.Entities;

namespace Sparta.Core.Converters
{
    public class ConfigConverter
    {
        public static Configuration[] Deserialize(string json)
        {
            var jsonObject = JObject.Parse(json);

            return GenerateConfig(jsonObject).ToArray();
        }

        private static List<Configuration> GenerateConfig(JToken token)
        {
            return token switch
            {
                JProperty prop =>
                [
                    new Configuration
                    {
                        Name = prop.Name,
                        Children = prop.Value is not JValue ? GenerateConfig(prop.Value) : null,
                        Value = prop.Value is JValue val ? val.Value?.ToString() : null,
                    }
                ],
                JObject obj => obj.Children().SelectMany(GenerateConfig).ToList(),
                _ => throw new ArgumentOutOfRangeException(nameof(token), token, null)
            };
        }

        public static string Serialize(Configuration[] dbConfigurations)
        {
            JObject jsonObject = new();

            AddToJsonObject(dbConfigurations.ToList(), ref jsonObject);

            return jsonObject.ToString();
        }

        private static void AddToJsonObject(List<Configuration>? configs, ref JObject jsonObject)
        {
            if (configs == null) return;
            foreach (var configuration in configs)
            {
                var prop = new JProperty(configuration.Name, configuration.Value);

                jsonObject.Add(prop);
                if (!(configuration.Children?.Count > 0)) continue;

                var obj = new JObject();
                prop.Value = obj;
                AddToJsonObject(configuration.Children, ref obj);
            }
        }
    }
}
