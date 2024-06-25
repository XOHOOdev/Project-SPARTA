using Newtonsoft.Json.Linq;
using Sparta.Core.Models;

namespace Sparta.Core.Converters
{
    public class ConfigConverter
    {
        public static CfConfiguration[] Deserialize(string json)
        {
            List<CfConfiguration> configs = [];

            var jsonObject = JObject.Parse(json);
            configs.AddRange(from item in jsonObject.Children() from item2 in item.First().Children() let prop = item2 as JProperty select new CfConfiguration { Class = item.Path, Property = prop?.Name ?? "", Value = prop?.Value.ToString() ?? "" });

            return configs.ToArray();
        }

        public static string Serialize(CfConfiguration[] dbConfigurations)
        {
            JObject jsonObject = new();
            foreach (var config in dbConfigurations)
            {
                if (jsonObject.Children().FirstOrDefault(x => x.Path == config.Class) is not JProperty item)
                {
                    item = new JProperty(config.Class, new JObject());
                    jsonObject.Add(item);
                }
                ((JObject)item.Value).Add(new JProperty(config.Property, config.Value));
            }
            return jsonObject.ToString();
        }
    }
}
