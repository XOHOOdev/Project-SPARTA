using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sparta.BlazorUI.Entities;

namespace Sparta.BlazorUI.Data;

public class ApplicationDbContext<TUser, TRole, TKey>(
    DbContextOptions<ApplicationDbContext<TUser, TRole, TKey>> options)
    : IdentityDbContext<
        TUser, TRole, TKey, IdentityUserClaim<TKey>, IdentityUserRole<TKey>,
        IdentityUserLogin<TKey>, IdentityRoleClaim<TKey>, IdentityUserToken<TKey>>(options)
    where TUser : IdentityUser<TKey>
    where TRole : IdentityRole<TKey>
    where TKey : IEquatable<TKey>
{
    public DbSet<Permission> US_Permissions { get; set; }

    public DbSet<UserSteamId> US_SteamIds { get; set; }

    public DbSet<Configuration> CF_Configurations { get; set; }

    public DbSet<Module> MD_Modules { get; set; }

    public DbSet<ModuleParameter> MD_Parameters { get; set; }

    public DbSet<ModuleType> MD_ModuleType { get; set; }

    public DbSet<DiscordReceivedMessage> DC_ReceivedMessages { get; set; }
}