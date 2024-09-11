using Microsoft.AspNetCore.Identity;

namespace Sparta.Core.DataAccess.DatabaseAccess.Entities;

public class ApplicationRole : IdentityRole
{
    public ApplicationRole(string name)
    {
        Name = name;
        NormalizedName = name.ToUpper();
    }

    public virtual List<Permission> Permissions { get; } = [];

    public virtual List<DiscordGuild> DiscordGuilds { get; } = [];
}