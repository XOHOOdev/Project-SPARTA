using Helium.Core.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Helium.Core.Models;

public partial class HeliumDbContext : DbContext
{
    //Scaffold-DbContext "Server=localhost,1433;Database=HeliumDb;User Id=SA;Password=A&VeryComplex123Password;MultipleActiveResultSets=true;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -force

    public HeliumDbContext()
    {
    }

    public HeliumDbContext(DbContextOptions<HeliumDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<CfConfiguration> CfConfigurations { get; set; }

    public virtual DbSet<DcAuthor> DcAuthors { get; set; }

    public virtual DbSet<DcChannel> DcChannels { get; set; }

    public virtual DbSet<DcEmbed> DcEmbeds { get; set; }

    public virtual DbSet<DcEmbedField> DcEmbedFields { get; set; }

    public virtual DbSet<DcGuild> DcGuilds { get; set; }

    public virtual DbSet<DcMessageComponent> DcMessageComponents { get; set; }

    public virtual DbSet<DcModal> DcModals { get; set; }

    public virtual DbSet<DcModalComponent> DcModalComponents { get; set; }

    public virtual DbSet<DcStatsChannel> DcStatsChannels { get; set; }

    public virtual DbSet<HllGame> HllGames { get; set; }

    public virtual DbSet<HllGamePlayer> HllGamePlayers { get; set; }

    public virtual DbSet<HllGameState> HllGameStates { get; set; }

    public virtual DbSet<HllGameserver> HllGameservers { get; set; }

    public virtual DbSet<HllLog> HllLogs { get; set; }

    public virtual DbSet<HllPlayer> HllPlayers { get; set; }

    public virtual DbSet<HllPlayerSession> HllPlayerSessions { get; set; }

    public virtual DbSet<UsPermission> UsPermissions { get; set; }

    public virtual DbSet<UsSteamId> UsSteamIds { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connectionString = ConfigLoader.Load().GetConnectionString("DefaultConnection") ?? string.Empty;
        optionsBuilder.UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedName] IS NOT NULL)");

            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");

            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("AspNetUserRoles");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            entity.Property(e => e.LoginProvider).HasMaxLength(128);
            entity.Property(e => e.ProviderKey).HasMaxLength(128);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.Property(e => e.LoginProvider).HasMaxLength(128);
            entity.Property(e => e.Name).HasMaxLength(128);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<CfConfiguration>(entity =>
        {
            entity.HasKey(e => new { e.Class, e.Property });

            entity.ToTable("CF_Configurations");
        });

        modelBuilder.Entity<DcAuthor>(entity =>
        {
            entity.ToTable("DC_Authors");
        });

        modelBuilder.Entity<DcChannel>(entity =>
        {
            entity.ToTable("DC_Channels");

            entity.HasIndex(e => e.DiscordGuildId, "IX_DC_Channels_DiscordGuildId");

            entity.Property(e => e.Id).HasColumnType("decimal(20, 0)");
            entity.Property(e => e.DiscordGuildId).HasColumnType("decimal(20, 0)");

            entity.HasOne(d => d.DiscordGuild).WithMany(p => p.DcChannels).HasForeignKey(d => d.DiscordGuildId);
        });

        modelBuilder.Entity<DcEmbed>(entity =>
        {
            entity.ToTable("DC_Embeds");

            entity.HasIndex(e => e.DiscordAuthorId, "IX_DC_Embeds_DiscordAuthorId");

            entity.HasIndex(e => e.DiscordChannelId, "IX_DC_Embeds_DiscordChannelId");

            entity.Property(e => e.DiscordChannelId).HasColumnType("decimal(20, 0)");

            entity.HasOne(d => d.DiscordAuthor).WithMany(p => p.DcEmbeds).HasForeignKey(d => d.DiscordAuthorId);

            entity.HasOne(d => d.DiscordChannel).WithMany(p => p.DcEmbeds).HasForeignKey(d => d.DiscordChannelId);

            entity.HasMany(d => d.MessageComponents).WithMany(p => p.Embeds)
                .UsingEntity<Dictionary<string, object>>(
                    "DiscordEmbedDiscordMessageComponent",
                    r => r.HasOne<DcMessageComponent>().WithMany().HasForeignKey("MessageComponentsId"),
                    l => l.HasOne<DcEmbed>().WithMany().HasForeignKey("EmbedsId"),
                    j =>
                    {
                        j.HasKey("EmbedsId", "MessageComponentsId");
                        j.ToTable("DiscordEmbedDiscordMessageComponent");
                        j.HasIndex(new[] { "MessageComponentsId" }, "IX_DiscordEmbedDiscordMessageComponent_MessageComponentsId");
                    });
        });

        modelBuilder.Entity<DcEmbedField>(entity =>
        {
            entity.ToTable("DC_EmbedFields");

            entity.HasIndex(e => e.DiscordEmbedId, "IX_DC_EmbedFields_DiscordEmbedId");

            entity.HasOne(d => d.DiscordEmbed).WithMany(p => p.DcEmbedFields).HasForeignKey(d => d.DiscordEmbedId);
        });

        modelBuilder.Entity<DcGuild>(entity =>
        {
            entity.ToTable("DC_Guilds");

            entity.Property(e => e.Id).HasColumnType("decimal(20, 0)");
        });

        modelBuilder.Entity<DcMessageComponent>(entity =>
        {
            entity.ToTable("DC_MessageComponents");

            entity.HasIndex(e => e.ActionChannelId, "IX_DC_MessageComponents_ActionChannelId");

            entity.HasIndex(e => e.ActionModalId, "IX_DC_MessageComponents_ActionModalId");

            entity.Property(e => e.ActionChannelId).HasColumnType("decimal(20, 0)");

            entity.HasOne(d => d.ActionChannel).WithMany(p => p.DcMessageComponents).HasForeignKey(d => d.ActionChannelId);

            entity.HasOne(d => d.ActionModal).WithMany(p => p.DcMessageComponents).HasForeignKey(d => d.ActionModalId);
        });

        modelBuilder.Entity<DcModal>(entity =>
        {
            entity.ToTable("DC_Modals");
        });

        modelBuilder.Entity<DcModalComponent>(entity =>
        {
            entity.ToTable("DC_ModalComponents");

            entity.HasMany(d => d.DiscordModals).WithMany(p => p.Components)
                .UsingEntity<Dictionary<string, object>>(
                    "DiscordModalDiscordModalComponent",
                    r => r.HasOne<DcModal>().WithMany().HasForeignKey("DiscordModalsId"),
                    l => l.HasOne<DcModalComponent>().WithMany().HasForeignKey("ComponentsId"),
                    j =>
                    {
                        j.HasKey("ComponentsId", "DiscordModalsId");
                        j.ToTable("DiscordModalDiscordModalComponent");
                        j.HasIndex(new[] { "DiscordModalsId" }, "IX_DiscordModalDiscordModalComponent_DiscordModalsId");
                    });
        });

        modelBuilder.Entity<DcStatsChannel>(entity =>
        {
            entity.ToTable("DC_StatsChannels");
        });

        modelBuilder.Entity<HllGame>(entity =>
        {
            entity.HasKey(e => e.GameId);

            entity.ToTable("HLL_Games");

            entity.HasIndex(e => e.HllgameserverId, "IX_HLL_Games_HLLGameserverId");

            entity.Property(e => e.HllgameserverId).HasColumnName("HLLGameserverId");

            entity.HasOne(d => d.Hllgameserver).WithMany(p => p.HllGames).HasForeignKey(d => d.HllgameserverId);
        });

        modelBuilder.Entity<HllGamePlayer>(entity =>
        {
            entity.HasKey(e => new { e.SteamId, e.GameId });

            entity.ToTable("HLL_GamePlayers");

            entity.HasIndex(e => e.GameId, "IX_HLL_GamePlayers_GameId");

            entity.HasOne(d => d.Game).WithMany(p => p.HllGamePlayers).HasForeignKey(d => d.GameId);

            entity.HasOne(d => d.Steam).WithMany(p => p.HllGamePlayers).HasForeignKey(d => d.SteamId);
        });

        modelBuilder.Entity<HllGameState>(entity =>
        {
            entity.ToTable("HLL_GameStates");

            entity.HasIndex(e => e.HllgameserverId, "IX_HLL_GameStates_HLLGameserverId");

            entity.Property(e => e.HllgameserverId).HasColumnName("HLLGameserverId");

            entity.HasOne(d => d.Hllgameserver).WithMany(p => p.HllGameStates).HasForeignKey(d => d.HllgameserverId);
        });

        modelBuilder.Entity<HllGameserver>(entity =>
        {
            entity.ToTable("HLL_Gameservers");
        });

        modelBuilder.Entity<HllLog>(entity =>
        {
            entity.ToTable("HLL_Logs");

            entity.HasIndex(e => e.HllgameserverId, "IX_HLL_Logs_HLLGameserverId");

            entity.Property(e => e.HllgameserverId).HasColumnName("HLLGameserverId");

            entity.HasOne(d => d.Hllgameserver).WithMany(p => p.HllLogs).HasForeignKey(d => d.HllgameserverId);
        });

        modelBuilder.Entity<HllPlayer>(entity =>
        {
            entity.HasKey(e => e.SteamId);

            entity.ToTable("HLL_Players");

            entity.Property(e => e.SteamId).ValueGeneratedNever();
        });

        modelBuilder.Entity<HllPlayerSession>(entity =>
        {
            entity.HasKey(e => new { e.SteamId, e.StartTime });

            entity.ToTable("HLL_PlayerSessions");

            entity.HasOne(d => d.Steam).WithMany(p => p.HllPlayerSessions).HasForeignKey(d => d.SteamId);
        });

        modelBuilder.Entity<UsPermission>(entity =>
        {
            entity.ToTable("US_Permissions");

            entity.HasMany(d => d.Roles).WithMany(p => p.Permissions)
                .UsingEntity<Dictionary<string, object>>(
                    "ApplicationRolePermission",
                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RolesId"),
                    l => l.HasOne<UsPermission>().WithMany().HasForeignKey("PermissionsId"),
                    j =>
                    {
                        j.HasKey("PermissionsId", "RolesId");
                        j.ToTable("ApplicationRolePermission");
                        j.HasIndex(new[] { "RolesId" }, "IX_ApplicationRolePermission_RolesId");
                    });
        });

        modelBuilder.Entity<UsSteamId>(entity =>
        {
            entity.HasKey(e => e.UserId);

            entity.ToTable("US_SteamIds");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
