using Sparta.Core.Models;
using Newtonsoft.Json.Linq;

namespace Sparta.Core.Helpers
{
    public class ConfigConverter
    {
        public static CfConfiguration[] Deserialize(string json)
        {
            List<CfConfiguration> configs = new();

            JObject jsonObject = JObject.Parse(json);
            foreach (JToken item in jsonObject.Children())
            {
                foreach (JToken item2 in item.First().Children())
                {
                    JProperty? prop = item2 as JProperty;
                    configs.Add(new CfConfiguration { Class = item.Path, Property = prop?.Name ?? "", Value = prop?.Value.ToString() ?? "" });
                }
            }

            return configs.ToArray();
        }

        public static string Serialize(CfConfiguration[] dbConfigurations)
        {
            JObject jsonObject = new();
            foreach (CfConfiguration dbconfig in dbConfigurations)
            {
                if (jsonObject.Children().FirstOrDefault(x => x.Path == dbconfig.Class) is not JProperty item)
                {
                    item = new JProperty(dbconfig.Class, new JObject());
                    jsonObject.Add(item);
                }
                ((JObject)item.Value).Add(new JProperty(dbconfig.Property, dbconfig.Value));
            }
            return jsonObject.ToString();
        }
    }
}
