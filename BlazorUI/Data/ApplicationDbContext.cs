using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sparta.BlazorUI.Entities;

namespace Sparta.BlazorUI.Data;

public class ApplicationDbContext<TUser, TRole, TKey> : IdentityDbContext<
    TUser, TRole, TKey, IdentityUserClaim<TKey>, IdentityUserRole<TKey>,
    IdentityUserLogin<TKey>, IdentityRoleClaim<TKey>, IdentityUserToken<TKey>>
    where TUser : IdentityUser<TKey>
    where TRole : IdentityRole<TKey>
    where TKey : IEquatable<TKey>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext<TUser, TRole, TKey>> options)
        : base(options)
    {
    }

    public DbSet<Permission> US_Permissions { get; set; }

    public DbSet<UserSteamId> US_SteamIds { get; set; }

    public DbSet<Configuration> CF_Configurations { get; set; }

    public DbSet<DiscordGuild> DC_Guilds { get; set; }

    public DbSet<DiscordChannel> DC_Channels { get; set; }

    public DbSet<DiscordStatsMessage> DC_StatsChannels { get; set; }

    public DbSet<DiscordEmbed> DC_Embeds { get; set; }

    public DbSet<DiscordEmbedFields> DC_EmbedFields { get; set; }

    public DbSet<DiscordAuthor> DC_Authors { get; set; }

    public DbSet<DiscordMessageComponent> DC_MessageComponents { get; set; }

    public DbSet<DiscordModal> DC_Modals { get; set; }

    public DbSet<DiscordModalComponent> DC_ModalComponents { get; set; }
}