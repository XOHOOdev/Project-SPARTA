using Sparta.Core.Converters;
using Sparta.Core.Models;

/**
 * Update Migration
 * Scaffold-DbContext "Server=localhost,1434;Database=SalesDb;User Id=SA;Password=A&VeryComplex123Password;MultipleActiveResultSets=true;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -force
 */

namespace Sparta.Core.Helpers
{
    public class ConfigHelper
    {
        public static void SetConfig(string json)
        {
            using SpartaDbContext dbContext = new();

            if (dbContext.CfConfigurations.Any()) return;

            foreach (var config in ConfigConverter.Deserialize(json))
            {
                var reqObj = dbContext.Find(typeof(CfConfiguration), [config.Class, config.Property]);
                if (reqObj != null)
                {
                    ((CfConfiguration)reqObj).Value = config.Value;
                    continue;
                }
                dbContext.Add(config);
            }
            dbContext.SaveChanges();
        }

        public static string? GetConfig(string className, string config)
        {
            using SpartaDbContext dbContext = new();
            return (dbContext.Find(typeof(CfConfiguration), [className, config]) as CfConfiguration)?.Value;
        }

        public static string GetConfigAsJson()
        {
            using SpartaDbContext dbContext = new();
            return ConfigConverter.Serialize(dbContext.CfConfigurations.ToArray());
        }
    }
}