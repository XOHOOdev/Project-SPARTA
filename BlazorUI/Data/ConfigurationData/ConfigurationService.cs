using Microsoft.AspNetCore.Identity;
using Sparta.BlazorUI.Authorization;
using Sparta.BlazorUI.Entities;
using Sparta.Core.Helpers;

namespace Sparta.BlazorUI.Data.ConfigurationData;

[HasPermission(Permissions.Permissions.Configuration.View)]
public class ConfigurationService
{
    private readonly ApplicationDbContext<IdentityUser, ApplicationRole, string> _context;

    public ConfigurationService(ApplicationDbContext<IdentityUser, ApplicationRole, string> context)
    {
        _context = context;
    }

    public ConfigurationCategory[] GetConfigurations()
    {
        var dbConfigs = _context.CF_Configurations.ToList();

        List<ConfigurationCategory> configurations = new();
        foreach (var dbconfig in dbConfigs)
        {
            var config = configurations.FirstOrDefault(x => x.Name == dbconfig.Class);
            if (config == null)
            {
                config = new ConfigurationCategory
                {
                    Name = dbconfig.Class,
                    ConfigurationElements = new List<ConfigurationElement>()
                };
                configurations.Add(config);
            }

            config.ConfigurationElements.Add(new ConfigurationElement
                { Value = dbconfig.Value, Name = dbconfig.Property });
        }

        return configurations.ToArray();
    }

    [HasPermission(Permissions.Permissions.Configuration.Edit)]
    public void SetConfiguration(ConfigurationCategory? category)
    {
        if (category == null) return;
        var affectedEntries = _context.CF_Configurations.Where(x => x.Class == category.Name);
        foreach (var entry in affectedEntries)
            entry.Value = category.ConfigurationElements.First(x => x.Name == entry.Property).Value;
        _context.SaveChanges();
    }

    [HasPermission(Permissions.Permissions.Configuration.Edit)]
    public void SaveConfiguration()
    {
        var jsonString = ConfigHelper.GetConfigAsJson();
        var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config", "default-config.json");
        File.WriteAllText(path, jsonString);
    }
}