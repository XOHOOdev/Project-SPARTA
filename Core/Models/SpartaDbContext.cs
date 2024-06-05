using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Sparta.Core.Helpers;

namespace Sparta.Core.Models;

public partial class SpartaDbContext : DbContext
{
    //Scaffold-DbContext "Server=localhost,1433;Database=SpartaDb;User Id=SA;Password=A&VeryComplex123Password;MultipleActiveResultSets=true;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -force

    public SpartaDbContext()
    {
    }

    public SpartaDbContext(DbContextOptions<SpartaDbContext> options)
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

    public virtual DbSet<DcReceivedMessage> DcReceivedMessages { get; set; }

    public virtual DbSet<MdModule> MdModules { get; set; }

    public virtual DbSet<MdParameter> MdParameters { get; set; }

    public virtual DbSet<ModuleType> ModuleTypes { get; set; }

    public virtual DbSet<UsPermission> UsPermissions { get; set; }

    public virtual DbSet<UsSteamId> UsSteamIds { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = ConfigLoader.Load().GetConnectionString("DefaultConnection") ?? string.Empty;
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

        modelBuilder.Entity<DcReceivedMessage>(entity =>
        {
            entity.ToTable("DC_ReceivedMessages");

            entity.Property(e => e.Id).HasColumnType("decimal(20, 0)");
            entity.Property(e => e.Reference).HasColumnType("decimal(20, 0)");
        });

        modelBuilder.Entity<MdModule>(entity =>
        {
            entity.ToTable("MD_Modules");

            entity.HasIndex(e => e.TypeId, "IX_MD_Modules_TypeId");

            entity.HasOne(d => d.Type).WithMany(p => p.MdModules).HasForeignKey(d => d.TypeId);
        });

        modelBuilder.Entity<MdParameter>(entity =>
        {
            entity.ToTable("MD_Parameters");

            entity.HasIndex(e => e.ModuleId, "IX_MD_Parameters_ModuleId");

            entity.HasOne(d => d.Module).WithMany(p => p.MdParameters).HasForeignKey(d => d.ModuleId);
        });

        modelBuilder.Entity<ModuleType>(entity =>
        {
            entity.ToTable("ModuleType");
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
