using Microsoft.EntityFrameworkCore;
using Sparta.Core.Models;
using System.Diagnostics;

namespace Sparta.Core.Helpers
{
    public class ModuleHelper
    {
        public static void SetParameters(string json)
        {
            using SpartaDbContext dbContext = new();
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

        public static IEnumerable<string> GetParameters(string? id = null)
        {
            var className = new StackTrace().GetFrame(1)?.GetMethod()?.DeclaringType?.Name;

            using SpartaDbContext dbContext = new();

            return dbContext.MdModules.Include(mdModule => mdModule.MdParameters).FirstOrDefault(x => x.Name == className)?.MdParameters.Select(z => z.Value) ?? [];
        }
    }
}
