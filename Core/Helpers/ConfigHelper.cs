using Microsoft.AspNetCore.Identity;
using Sparta.Core.Converters;
using Sparta.Core.DataAccess.DatabaseAccess;
using Sparta.Core.DataAccess.DatabaseAccess.Entities;

namespace Sparta.Core.Helpers
{
    public class ConfigHelper(ApplicationDbContext<IdentityUser, ApplicationRole, string> context)
    {
        public void SetConfig(string json)
        {
            if (context.CF_Configurations.Any()) return;

            context.CF_Configurations.AddRange(ConfigConverter.Deserialize(json));

            context.SaveChanges();
        }

        public string? GetConfig(List<string> path)
        {
            return GetConfig(
                context.CF_Configurations.First(c => c.Parent == null && c.Name == path.First()),
                path.Skip(1).ToList());
        }

        private static string? GetConfig(Configuration config, List<string> path)
        {
            return config.Children == null ?
                config.Value :
                GetConfig(config.Children.First(c => c.Parent == null && c.Name == path.First()), path.Skip(1).ToList());
        }

        public string GetConfigAsJson()
        {
            return ConfigConverter.Serialize(context.CF_Configurations.Where(c => c.Parent == null).ToArray());
        }
    }
}