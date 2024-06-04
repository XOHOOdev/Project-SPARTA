#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Sparta.BlazorUI.Data.Model;

/// <inheritdoc />
public partial class InitialMigration : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            "AspNetRoles",
            table => new
            {
                Id = table.Column<string>("nvarchar(450)", nullable: false),
                Name = table.Column<string>("nvarchar(256)", maxLength: 256, nullable: true),
                NormalizedName = table.Column<string>("nvarchar(256)", maxLength: 256, nullable: true),
                ConcurrencyStamp = table.Column<string>("nvarchar(max)", nullable: true)
            },
            constraints: table => { table.PrimaryKey("PK_AspNetRoles", x => x.Id); });

        migrationBuilder.CreateTable(
            "AspNetUsers",
            table => new
            {
                Id = table.Column<string>("nvarchar(450)", nullable: false),
                UserName = table.Column<string>("nvarchar(256)", maxLength: 256, nullable: true),
                NormalizedUserName = table.Column<string>("nvarchar(256)", maxLength: 256, nullable: true),
                Email = table.Column<string>("nvarchar(256)", maxLength: 256, nullable: true),
                NormalizedEmail = table.Column<string>("nvarchar(256)", maxLength: 256, nullable: true),
                EmailConfirmed = table.Column<bool>("bit", nullable: false),
                PasswordHash = table.Column<string>("nvarchar(max)", nullable: true),
                SecurityStamp = table.Column<string>("nvarchar(max)", nullable: true),
                ConcurrencyStamp = table.Column<string>("nvarchar(max)", nullable: true),
                PhoneNumber = table.Column<string>("nvarchar(max)", nullable: true),
                PhoneNumberConfirmed = table.Column<bool>("bit", nullable: false),
                TwoFactorEnabled = table.Column<bool>("bit", nullable: false),
                LockoutEnd = table.Column<DateTimeOffset>("datetimeoffset", nullable: true),
                LockoutEnabled = table.Column<bool>("bit", nullable: false),
                AccessFailedCount = table.Column<int>("int", nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_AspNetUsers", x => x.Id); });

        migrationBuilder.CreateTable(
            "CF_Configurations",
            table => new
            {
                Class = table.Column<string>("nvarchar(450)", nullable: false),
                Property = table.Column<string>("nvarchar(450)", nullable: false),
                Value = table.Column<string>("nvarchar(max)", nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_CF_Configurations", x => new { x.Class, x.Property }); });

        migrationBuilder.CreateTable(
            "DC_Authors",
            table => new
            {
                Id = table.Column<long>("bigint", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>("nvarchar(max)", nullable: false),
                IconUrl = table.Column<string>("nvarchar(max)", nullable: true),
                Url = table.Column<string>("nvarchar(max)", nullable: true)
            },
            constraints: table => { table.PrimaryKey("PK_DC_Authors", x => x.Id); });

        migrationBuilder.CreateTable(
            "DC_Guilds",
            table => new
            {
                Id = table.Column<decimal>("decimal(20,0)", nullable: false),
                Name = table.Column<string>("nvarchar(max)", nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_DC_Guilds", x => x.Id); });

        migrationBuilder.CreateTable(
            "DC_ModalComponents",
            table => new
            {
                Id = table.Column<string>("nvarchar(450)", nullable: false),
                Label = table.Column<string>("nvarchar(max)", nullable: false),
                InputStyle = table.Column<int>("int", nullable: false),
                Placeholder = table.Column<string>("nvarchar(max)", nullable: true),
                MinLength = table.Column<int>("int", nullable: true),
                MaxLength = table.Column<int>("int", nullable: true),
                Required = table.Column<bool>("bit", nullable: true),
                DefaultValue = table.Column<string>("nvarchar(max)", nullable: true)
            },
            constraints: table => { table.PrimaryKey("PK_DC_ModalComponents", x => x.Id); });

        migrationBuilder.CreateTable(
            "DC_Modals",
            table => new
            {
                Id = table.Column<string>("nvarchar(450)", nullable: false),
                Title = table.Column<string>("nvarchar(max)", nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_DC_Modals", x => x.Id); });

        migrationBuilder.CreateTable(
            "DC_StatsChannels",
            table => new
            {
                Id = table.Column<long>("bigint", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                MessageId = table.Column<string>("nvarchar(max)", nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_DC_StatsChannels", x => x.Id); });

        migrationBuilder.CreateTable(
            "HLL_Gameservers",
            table => new
            {
                Id = table.Column<long>("bigint", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Address = table.Column<string>("nvarchar(max)", nullable: false),
                Port = table.Column<int>("int", nullable: false),
                Password = table.Column<string>("nvarchar(max)", nullable: false),
                Name = table.Column<string>("nvarchar(max)", nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_HLL_Gameservers", x => x.Id); });

        migrationBuilder.CreateTable(
            "HLL_Players",
            table => new
            {
                SteamId = table.Column<long>("bigint", nullable: false),
                Name = table.Column<string>("nvarchar(max)", nullable: false),
                FirstLogon = table.Column<DateTime>("datetime2", nullable: false),
                Level = table.Column<int>("int", nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_HLL_Players", x => x.SteamId); });

        migrationBuilder.CreateTable(
            "US_Permissions",
            table => new
            {
                Id = table.Column<string>("nvarchar(450)", nullable: false),
                Name = table.Column<string>("nvarchar(max)", nullable: false),
                NormalizedName = table.Column<string>("nvarchar(max)", nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_US_Permissions", x => x.Id); });

        migrationBuilder.CreateTable(
            "US_SteamIds",
            table => new
            {
                UserId = table.Column<string>("nvarchar(450)", nullable: false),
                SteamId = table.Column<long>("bigint", nullable: false)
            },
            constraints: table => { table.PrimaryKey("PK_US_SteamIds", x => x.UserId); });

        migrationBuilder.CreateTable(
            "AspNetRoleClaims",
            table => new
            {
                Id = table.Column<int>("int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                RoleId = table.Column<string>("nvarchar(450)", nullable: false),
                ClaimType = table.Column<string>("nvarchar(max)", nullable: true),
                ClaimValue = table.Column<string>("nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                table.ForeignKey(
                    "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                    x => x.RoleId,
                    "AspNetRoles",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "AspNetUserClaims",
            table => new
            {
                Id = table.Column<int>("int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                UserId = table.Column<string>("nvarchar(450)", nullable: false),
                ClaimType = table.Column<string>("nvarchar(max)", nullable: true),
                ClaimValue = table.Column<string>("nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                table.ForeignKey(
                    "FK_AspNetUserClaims_AspNetUsers_UserId",
                    x => x.UserId,
                    "AspNetUsers",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "AspNetUserLogins",
            table => new
            {
                LoginProvider = table.Column<string>("nvarchar(128)", maxLength: 128, nullable: false),
                ProviderKey = table.Column<string>("nvarchar(128)", maxLength: 128, nullable: false),
                ProviderDisplayName = table.Column<string>("nvarchar(max)", nullable: true),
                UserId = table.Column<string>("nvarchar(450)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                table.ForeignKey(
                    "FK_AspNetUserLogins_AspNetUsers_UserId",
                    x => x.UserId,
                    "AspNetUsers",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "AspNetUserRoles",
            table => new
            {
                UserId = table.Column<string>("nvarchar(450)", nullable: false),
                RoleId = table.Column<string>("nvarchar(450)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                table.ForeignKey(
                    "FK_AspNetUserRoles_AspNetRoles_RoleId",
                    x => x.RoleId,
                    "AspNetRoles",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    "FK_AspNetUserRoles_AspNetUsers_UserId",
                    x => x.UserId,
                    "AspNetUsers",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "AspNetUserTokens",
            table => new
            {
                UserId = table.Column<string>("nvarchar(450)", nullable: false),
                LoginProvider = table.Column<string>("nvarchar(128)", maxLength: 128, nullable: false),
                Name = table.Column<string>("nvarchar(128)", maxLength: 128, nullable: false),
                Value = table.Column<string>("nvarchar(max)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                table.ForeignKey(
                    "FK_AspNetUserTokens_AspNetUsers_UserId",
                    x => x.UserId,
                    "AspNetUsers",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "DC_Channels",
            table => new
            {
                Id = table.Column<decimal>("decimal(20,0)", nullable: false),
                Name = table.Column<string>("nvarchar(max)", nullable: false),
                Type = table.Column<string>("nvarchar(max)", nullable: false),
                DiscordGuildId = table.Column<decimal>("decimal(20,0)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_DC_Channels", x => x.Id);
                table.ForeignKey(
                    "FK_DC_Channels_DC_Guilds_DiscordGuildId",
                    x => x.DiscordGuildId,
                    "DC_Guilds",
                    "Id");
            });

        migrationBuilder.CreateTable(
            "DiscordModalDiscordModalComponent",
            table => new
            {
                ComponentsId = table.Column<string>("nvarchar(450)", nullable: false),
                DiscordModalsId = table.Column<string>("nvarchar(450)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_DiscordModalDiscordModalComponent",
                    x => new { x.ComponentsId, x.DiscordModalsId });
                table.ForeignKey(
                    "FK_DiscordModalDiscordModalComponent_DC_ModalComponents_ComponentsId",
                    x => x.ComponentsId,
                    "DC_ModalComponents",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    "FK_DiscordModalDiscordModalComponent_DC_Modals_DiscordModalsId",
                    x => x.DiscordModalsId,
                    "DC_Modals",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "HLL_Games",
            table => new
            {
                GameId = table.Column<long>("bigint", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                StartTime = table.Column<DateTime>("datetime2", nullable: false),
                Duration = table.Column<TimeSpan>("time", nullable: true),
                Map = table.Column<string>("nvarchar(max)", nullable: false),
                HLLGameserverId = table.Column<long>("bigint", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_HLL_Games", x => x.GameId);
                table.ForeignKey(
                    "FK_HLL_Games_HLL_Gameservers_HLLGameserverId",
                    x => x.HLLGameserverId,
                    "HLL_Gameservers",
                    "Id");
            });

        migrationBuilder.CreateTable(
            "HLL_GameStates",
            table => new
            {
                Id = table.Column<int>("int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                AlliedPlayers = table.Column<int>("int", nullable: false),
                AxisPlayers = table.Column<int>("int", nullable: false),
                AlliedScore = table.Column<int>("int", nullable: false),
                AxisScore = table.Column<int>("int", nullable: false),
                RemainingTime = table.Column<TimeSpan>("time", nullable: false),
                Map = table.Column<string>("nvarchar(max)", nullable: false),
                NextMap = table.Column<string>("nvarchar(max)", nullable: false),
                SlotsCurrent = table.Column<int>("int", nullable: false),
                SlotsTotal = table.Column<int>("int", nullable: false),
                HLLGameserverId = table.Column<long>("bigint", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_HLL_GameStates", x => x.Id);
                table.ForeignKey(
                    "FK_HLL_GameStates_HLL_Gameservers_HLLGameserverId",
                    x => x.HLLGameserverId,
                    "HLL_Gameservers",
                    "Id");
            });

        migrationBuilder.CreateTable(
            "HLL_Logs",
            table => new
            {
                Id = table.Column<long>("bigint", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                LogTime = table.Column<DateTime>("datetime2", nullable: false),
                LogType = table.Column<int>("int", nullable: false),
                ParticipantId1 = table.Column<long>("bigint", nullable: false),
                ParticipantId2 = table.Column<long>("bigint", nullable: false),
                Arguments = table.Column<string>("nvarchar(max)", nullable: true),
                HLLGameserverId = table.Column<long>("bigint", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_HLL_Logs", x => x.Id);
                table.ForeignKey(
                    "FK_HLL_Logs_HLL_Gameservers_HLLGameserverId",
                    x => x.HLLGameserverId,
                    "HLL_Gameservers",
                    "Id");
            });

        migrationBuilder.CreateTable(
            "HLL_PlayerSessions",
            table => new
            {
                SteamId = table.Column<long>("bigint", nullable: false),
                StartTime = table.Column<DateTime>("datetime2", nullable: false),
                ServerId = table.Column<long>("bigint", nullable: false),
                EndTime = table.Column<DateTime>("datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_HLL_PlayerSessions", x => new { x.SteamId, x.StartTime });
                table.ForeignKey(
                    "FK_HLL_PlayerSessions_HLL_Players_SteamId",
                    x => x.SteamId,
                    "HLL_Players",
                    "SteamId",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "ApplicationRolePermission",
            table => new
            {
                PermissionsId = table.Column<string>("nvarchar(450)", nullable: false),
                RolesId = table.Column<string>("nvarchar(450)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ApplicationRolePermission", x => new { x.PermissionsId, x.RolesId });
                table.ForeignKey(
                    "FK_ApplicationRolePermission_AspNetRoles_RolesId",
                    x => x.RolesId,
                    "AspNetRoles",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    "FK_ApplicationRolePermission_US_Permissions_PermissionsId",
                    x => x.PermissionsId,
                    "US_Permissions",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "DC_Embeds",
            table => new
            {
                Id = table.Column<long>("bigint", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Updated = table.Column<bool>("bit", nullable: false),
                MessageId = table.Column<long>("bigint", nullable: false),
                ColorArgb = table.Column<int>("int", nullable: false),
                Description = table.Column<string>("nvarchar(max)", nullable: true),
                FooterIconUrl = table.Column<string>("nvarchar(max)", nullable: true),
                FooterText = table.Column<string>("nvarchar(max)", nullable: true),
                ImageUrl = table.Column<string>("nvarchar(max)", nullable: true),
                ThumbnailUrl = table.Column<string>("nvarchar(max)", nullable: true),
                Title = table.Column<string>("nvarchar(max)", nullable: true),
                Url = table.Column<string>("nvarchar(max)", nullable: true),
                DiscordAuthorId = table.Column<long>("bigint", nullable: true),
                DiscordChannelId = table.Column<decimal>("decimal(20,0)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_DC_Embeds", x => x.Id);
                table.ForeignKey(
                    "FK_DC_Embeds_DC_Authors_DiscordAuthorId",
                    x => x.DiscordAuthorId,
                    "DC_Authors",
                    "Id");
                table.ForeignKey(
                    "FK_DC_Embeds_DC_Channels_DiscordChannelId",
                    x => x.DiscordChannelId,
                    "DC_Channels",
                    "Id");
            });

        migrationBuilder.CreateTable(
            "DC_MessageComponents",
            table => new
            {
                Id = table.Column<string>("nvarchar(450)", nullable: false),
                Label = table.Column<string>("nvarchar(max)", nullable: false),
                IsDisbled = table.Column<bool>("bit", nullable: false),
                ButtonStyle = table.Column<int>("int", nullable: true),
                Emote = table.Column<string>("nvarchar(max)", nullable: true),
                Url = table.Column<string>("nvarchar(max)", nullable: true),
                Action = table.Column<int>("int", nullable: false),
                ActionChannelId = table.Column<decimal>("decimal(20,0)", nullable: true),
                ActionModalId = table.Column<string>("nvarchar(450)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_DC_MessageComponents", x => x.Id);
                table.ForeignKey(
                    "FK_DC_MessageComponents_DC_Channels_ActionChannelId",
                    x => x.ActionChannelId,
                    "DC_Channels",
                    "Id");
                table.ForeignKey(
                    "FK_DC_MessageComponents_DC_Modals_ActionModalId",
                    x => x.ActionModalId,
                    "DC_Modals",
                    "Id");
            });

        migrationBuilder.CreateTable(
            "HLL_GamePlayers",
            table => new
            {
                SteamId = table.Column<long>("bigint", nullable: false),
                GameId = table.Column<long>("bigint", nullable: false),
                Team = table.Column<string>("nvarchar(max)", nullable: false),
                Role = table.Column<string>("nvarchar(max)", nullable: false),
                Unit = table.Column<string>("nvarchar(max)", nullable: false),
                Loadout = table.Column<string>("nvarchar(max)", nullable: false),
                CombatScore = table.Column<int>("int", nullable: false),
                OffensiveScore = table.Column<int>("int", nullable: false),
                DefensiveScore = table.Column<int>("int", nullable: false),
                SupportScore = table.Column<int>("int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_HLL_GamePlayers", x => new { x.SteamId, x.GameId });
                table.ForeignKey(
                    "FK_HLL_GamePlayers_HLL_Games_GameId",
                    x => x.GameId,
                    "HLL_Games",
                    "GameId",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    "FK_HLL_GamePlayers_HLL_Players_SteamId",
                    x => x.SteamId,
                    "HLL_Players",
                    "SteamId",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "DC_EmbedFields",
            table => new
            {
                Id = table.Column<long>("bigint", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>("nvarchar(max)", nullable: false),
                Value = table.Column<string>("nvarchar(max)", nullable: false),
                Inline = table.Column<bool>("bit", nullable: false),
                DiscordEmbedId = table.Column<long>("bigint", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_DC_EmbedFields", x => x.Id);
                table.ForeignKey(
                    "FK_DC_EmbedFields_DC_Embeds_DiscordEmbedId",
                    x => x.DiscordEmbedId,
                    "DC_Embeds",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "DiscordEmbedDiscordMessageComponent",
            table => new
            {
                EmbedsId = table.Column<long>("bigint", nullable: false),
                MessageComponentsId = table.Column<string>("nvarchar(450)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_DiscordEmbedDiscordMessageComponent",
                    x => new { x.EmbedsId, x.MessageComponentsId });
                table.ForeignKey(
                    "FK_DiscordEmbedDiscordMessageComponent_DC_Embeds_EmbedsId",
                    x => x.EmbedsId,
                    "DC_Embeds",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    "FK_DiscordEmbedDiscordMessageComponent_DC_MessageComponents_MessageComponentsId",
                    x => x.MessageComponentsId,
                    "DC_MessageComponents",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            "IX_ApplicationRolePermission_RolesId",
            "ApplicationRolePermission",
            "RolesId");

        migrationBuilder.CreateIndex(
            "IX_AspNetRoleClaims_RoleId",
            "AspNetRoleClaims",
            "RoleId");

        migrationBuilder.CreateIndex(
            "RoleNameIndex",
            "AspNetRoles",
            "NormalizedName",
            unique: true,
            filter: "[NormalizedName] IS NOT NULL");

        migrationBuilder.CreateIndex(
            "IX_AspNetUserClaims_UserId",
            "AspNetUserClaims",
            "UserId");

        migrationBuilder.CreateIndex(
            "IX_AspNetUserLogins_UserId",
            "AspNetUserLogins",
            "UserId");

        migrationBuilder.CreateIndex(
            "IX_AspNetUserRoles_RoleId",
            "AspNetUserRoles",
            "RoleId");

        migrationBuilder.CreateIndex(
            "EmailIndex",
            "AspNetUsers",
            "NormalizedEmail");

        migrationBuilder.CreateIndex(
            "UserNameIndex",
            "AspNetUsers",
            "NormalizedUserName",
            unique: true,
            filter: "[NormalizedUserName] IS NOT NULL");

        migrationBuilder.CreateIndex(
            "IX_DC_Channels_DiscordGuildId",
            "DC_Channels",
            "DiscordGuildId");

        migrationBuilder.CreateIndex(
            "IX_DC_EmbedFields_DiscordEmbedId",
            "DC_EmbedFields",
            "DiscordEmbedId");

        migrationBuilder.CreateIndex(
            "IX_DC_Embeds_DiscordAuthorId",
            "DC_Embeds",
            "DiscordAuthorId");

        migrationBuilder.CreateIndex(
            "IX_DC_Embeds_DiscordChannelId",
            "DC_Embeds",
            "DiscordChannelId");

        migrationBuilder.CreateIndex(
            "IX_DC_MessageComponents_ActionChannelId",
            "DC_MessageComponents",
            "ActionChannelId");

        migrationBuilder.CreateIndex(
            "IX_DC_MessageComponents_ActionModalId",
            "DC_MessageComponents",
            "ActionModalId");

        migrationBuilder.CreateIndex(
            "IX_DiscordEmbedDiscordMessageComponent_MessageComponentsId",
            "DiscordEmbedDiscordMessageComponent",
            "MessageComponentsId");

        migrationBuilder.CreateIndex(
            "IX_DiscordModalDiscordModalComponent_DiscordModalsId",
            "DiscordModalDiscordModalComponent",
            "DiscordModalsId");

        migrationBuilder.CreateIndex(
            "IX_HLL_GamePlayers_GameId",
            "HLL_GamePlayers",
            "GameId");

        migrationBuilder.CreateIndex(
            "IX_HLL_Games_HLLGameserverId",
            "HLL_Games",
            "HLLGameserverId");

        migrationBuilder.CreateIndex(
            "IX_HLL_GameStates_HLLGameserverId",
            "HLL_GameStates",
            "HLLGameserverId");

        migrationBuilder.CreateIndex(
            "IX_HLL_Logs_HLLGameserverId",
            "HLL_Logs",
            "HLLGameserverId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            "ApplicationRolePermission");

        migrationBuilder.DropTable(
            "AspNetRoleClaims");

        migrationBuilder.DropTable(
            "AspNetUserClaims");

        migrationBuilder.DropTable(
            "AspNetUserLogins");

        migrationBuilder.DropTable(
            "AspNetUserRoles");

        migrationBuilder.DropTable(
            "AspNetUserTokens");

        migrationBuilder.DropTable(
            "CF_Configurations");

        migrationBuilder.DropTable(
            "DC_EmbedFields");

        migrationBuilder.DropTable(
            "DC_StatsChannels");

        migrationBuilder.DropTable(
            "DiscordEmbedDiscordMessageComponent");

        migrationBuilder.DropTable(
            "DiscordModalDiscordModalComponent");

        migrationBuilder.DropTable(
            "HLL_GamePlayers");

        migrationBuilder.DropTable(
            "HLL_GameStates");

        migrationBuilder.DropTable(
            "HLL_Logs");

        migrationBuilder.DropTable(
            "HLL_PlayerSessions");

        migrationBuilder.DropTable(
            "US_SteamIds");

        migrationBuilder.DropTable(
            "US_Permissions");

        migrationBuilder.DropTable(
            "AspNetRoles");

        migrationBuilder.DropTable(
            "AspNetUsers");

        migrationBuilder.DropTable(
            "DC_Embeds");

        migrationBuilder.DropTable(
            "DC_MessageComponents");

        migrationBuilder.DropTable(
            "DC_ModalComponents");

        migrationBuilder.DropTable(
            "HLL_Games");

        migrationBuilder.DropTable(
            "HLL_Players");

        migrationBuilder.DropTable(
            "DC_Authors");

        migrationBuilder.DropTable(
            "DC_Channels");

        migrationBuilder.DropTable(
            "DC_Modals");

        migrationBuilder.DropTable(
            "HLL_Gameservers");

        migrationBuilder.DropTable(
            "DC_Guilds");
    }
}