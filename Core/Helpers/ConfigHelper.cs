﻿using Helium.Core.Models;

/**
 * Update Migration
 * Scaffold-DbContext "Server=localhost,1433;Database=SalesDb;User Id=SA;Password=A&VeryComplex123Password;MultipleActiveResultSets=true;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -force
 */

namespace Helium.Core.Helpers
{
    public class ConfigHelper
    {
        public static void SetConfig(string json)
        {
            using HeliumDbContext dbContext = new();
            foreach (var config in ConfigConverter.Deserialize(json))
            {
                var reqObj = dbContext.Find(typeof(CfConfiguration), new[] { config.Class, config.Property });
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
            using HeliumDbContext dbContext = new();
            return (dbContext.Find(typeof(CfConfiguration), new[] { className, config }) as CfConfiguration)?.Value;
        }

        public static string GetConfigAsJson()
        {
            using HeliumDbContext dbContext = new();
            return ConfigConverter.Serialize(dbContext.CfConfigurations.ToArray());
        }
    }
}