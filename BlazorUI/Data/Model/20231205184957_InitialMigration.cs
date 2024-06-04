using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Helium.BlazorUI.Data.Model
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CF_Configurations",
                columns: table => new
                {
                    Class = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Property = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CF_Configurations", x => new { x.Class, x.Property });
                });

            migrationBuilder.CreateTable(
                name: "DC_Authors",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IconUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DC_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DC_Guilds",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DC_Guilds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DC_ModalComponents",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InputStyle = table.Column<int>(type: "int", nullable: false),
                    Placeholder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MinLength = table.Column<int>(type: "int", nullable: true),
                    MaxLength = table.Column<int>(type: "int", nullable: true),
                    Required = table.Column<bool>(type: "bit", nullable: true),
                    DefaultValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DC_ModalComponents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DC_Modals",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DC_Modals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DC_StatsChannels",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MessageId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DC_StatsChannels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HLL_Gameservers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Port = table.Column<int>(type: "int", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HLL_Gameservers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HLL_Players",
                columns: table => new
                {
                    SteamId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstLogon = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HLL_Players", x => x.SteamId);
                });

            migrationBuilder.CreateTable(
                name: "US_Permissions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NormalizedName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_US_Permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "US_SteamIds",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SteamId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_US_SteamIds", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DC_Channels",
                columns: table => new
                {
                    Id = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiscordGuildId = table.Column<decimal>(type: "decimal(20,0)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DC_Channels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DC_Channels_DC_Guilds_DiscordGuildId",
                        column: x => x.DiscordGuildId,
                        principalTable: "DC_Guilds",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DiscordModalDiscordModalComponent",
                columns: table => new
                {
                    ComponentsId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DiscordModalsId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscordModalDiscordModalComponent", x => new { x.ComponentsId, x.DiscordModalsId });
                    table.ForeignKey(
                        name: "FK_DiscordModalDiscordModalComponent_DC_ModalComponents_ComponentsId",
                        column: x => x.ComponentsId,
                        principalTable: "DC_ModalComponents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiscordModalDiscordModalComponent_DC_Modals_DiscordModalsId",
                        column: x => x.DiscordModalsId,
                        principalTable: "DC_Modals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HLL_Games",
                columns: table => new
                {
                    GameId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: true),
                    Map = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HLLGameserverId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HLL_Games", x => x.GameId);
                    table.ForeignKey(
                        name: "FK_HLL_Games_HLL_Gameservers_HLLGameserverId",
                        column: x => x.HLLGameserverId,
                        principalTable: "HLL_Gameservers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HLL_GameStates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AlliedPlayers = table.Column<int>(type: "int", nullable: false),
                    AxisPlayers = table.Column<int>(type: "int", nullable: false),
                    AlliedScore = table.Column<int>(type: "int", nullable: false),
                    AxisScore = table.Column<int>(type: "int", nullable: false),
                    RemainingTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    Map = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NextMap = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SlotsCurrent = table.Column<int>(type: "int", nullable: false),
                    SlotsTotal = table.Column<int>(type: "int", nullable: false),
                    HLLGameserverId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HLL_GameStates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HLL_GameStates_HLL_Gameservers_HLLGameserverId",
                        column: x => x.HLLGameserverId,
                        principalTable: "HLL_Gameservers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HLL_Logs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LogTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LogType = table.Column<int>(type: "int", nullable: false),
                    ParticipantId1 = table.Column<long>(type: "bigint", nullable: false),
                    ParticipantId2 = table.Column<long>(type: "bigint", nullable: false),
                    Arguments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HLLGameserverId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HLL_Logs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HLL_Logs_HLL_Gameservers_HLLGameserverId",
                        column: x => x.HLLGameserverId,
                        principalTable: "HLL_Gameservers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HLL_PlayerSessions",
                columns: table => new
                {
                    SteamId = table.Column<long>(type: "bigint", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ServerId = table.Column<long>(type: "bigint", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HLL_PlayerSessions", x => new { x.SteamId, x.StartTime });
                    table.ForeignKey(
                        name: "FK_HLL_PlayerSessions_HLL_Players_SteamId",
                        column: x => x.SteamId,
                        principalTable: "HLL_Players",
                        principalColumn: "SteamId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationRolePermission",
                columns: table => new
                {
                    PermissionsId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RolesId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationRolePermission", x => new { x.PermissionsId, x.RolesId });
                    table.ForeignKey(
                        name: "FK_ApplicationRolePermission_AspNetRoles_RolesId",
                        column: x => x.RolesId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationRolePermission_US_Permissions_PermissionsId",
                        column: x => x.PermissionsId,
                        principalTable: "US_Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DC_Embeds",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Updated = table.Column<bool>(type: "bit", nullable: false),
                    MessageId = table.Column<long>(type: "bigint", nullable: false),
                    ColorArgb = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FooterIconUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FooterText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThumbnailUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiscordAuthorId = table.Column<long>(type: "bigint", nullable: true),
                    DiscordChannelId = table.Column<decimal>(type: "decimal(20,0)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DC_Embeds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DC_Embeds_DC_Authors_DiscordAuthorId",
                        column: x => x.DiscordAuthorId,
                        principalTable: "DC_Authors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DC_Embeds_DC_Channels_DiscordChannelId",
                        column: x => x.DiscordChannelId,
                        principalTable: "DC_Channels",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DC_MessageComponents",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDisbled = table.Column<bool>(type: "bit", nullable: false),
                    ButtonStyle = table.Column<int>(type: "int", nullable: true),
                    Emote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Action = table.Column<int>(type: "int", nullable: false),
                    ActionChannelId = table.Column<decimal>(type: "decimal(20,0)", nullable: true),
                    ActionModalId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DC_MessageComponents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DC_MessageComponents_DC_Channels_ActionChannelId",
                        column: x => x.ActionChannelId,
                        principalTable: "DC_Channels",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DC_MessageComponents_DC_Modals_ActionModalId",
                        column: x => x.ActionModalId,
                        principalTable: "DC_Modals",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HLL_GamePlayers",
                columns: table => new
                {
                    SteamId = table.Column<long>(type: "bigint", nullable: false),
                    GameId = table.Column<long>(type: "bigint", nullable: false),
                    Team = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Loadout = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CombatScore = table.Column<int>(type: "int", nullable: false),
                    OffensiveScore = table.Column<int>(type: "int", nullable: false),
                    DefensiveScore = table.Column<int>(type: "int", nullable: false),
                    SupportScore = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HLL_GamePlayers", x => new { x.SteamId, x.GameId });
                    table.ForeignKey(
                        name: "FK_HLL_GamePlayers_HLL_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "HLL_Games",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HLL_GamePlayers_HLL_Players_SteamId",
                        column: x => x.SteamId,
                        principalTable: "HLL_Players",
                        principalColumn: "SteamId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DC_EmbedFields",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Inline = table.Column<bool>(type: "bit", nullable: false),
                    DiscordEmbedId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DC_EmbedFields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DC_EmbedFields_DC_Embeds_DiscordEmbedId",
                        column: x => x.DiscordEmbedId,
                        principalTable: "DC_Embeds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiscordEmbedDiscordMessageComponent",
                columns: table => new
                {
                    EmbedsId = table.Column<long>(type: "bigint", nullable: false),
                    MessageComponentsId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscordEmbedDiscordMessageComponent", x => new { x.EmbedsId, x.MessageComponentsId });
                    table.ForeignKey(
                        name: "FK_DiscordEmbedDiscordMessageComponent_DC_Embeds_EmbedsId",
                        column: x => x.EmbedsId,
                        principalTable: "DC_Embeds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiscordEmbedDiscordMessageComponent_DC_MessageComponents_MessageComponentsId",
                        column: x => x.MessageComponentsId,
                        principalTable: "DC_MessageComponents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationRolePermission_RolesId",
                table: "ApplicationRolePermission",
                column: "RolesId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DC_Channels_DiscordGuildId",
                table: "DC_Channels",
                column: "DiscordGuildId");

            migrationBuilder.CreateIndex(
                name: "IX_DC_EmbedFields_DiscordEmbedId",
                table: "DC_EmbedFields",
                column: "DiscordEmbedId");

            migrationBuilder.CreateIndex(
                name: "IX_DC_Embeds_DiscordAuthorId",
                table: "DC_Embeds",
                column: "DiscordAuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_DC_Embeds_DiscordChannelId",
                table: "DC_Embeds",
                column: "DiscordChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_DC_MessageComponents_ActionChannelId",
                table: "DC_MessageComponents",
                column: "ActionChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_DC_MessageComponents_ActionModalId",
                table: "DC_MessageComponents",
                column: "ActionModalId");

            migrationBuilder.CreateIndex(
                name: "IX_DiscordEmbedDiscordMessageComponent_MessageComponentsId",
                table: "DiscordEmbedDiscordMessageComponent",
                column: "MessageComponentsId");

            migrationBuilder.CreateIndex(
                name: "IX_DiscordModalDiscordModalComponent_DiscordModalsId",
                table: "DiscordModalDiscordModalComponent",
                column: "DiscordModalsId");

            migrationBuilder.CreateIndex(
                name: "IX_HLL_GamePlayers_GameId",
                table: "HLL_GamePlayers",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_HLL_Games_HLLGameserverId",
                table: "HLL_Games",
                column: "HLLGameserverId");

            migrationBuilder.CreateIndex(
                name: "IX_HLL_GameStates_HLLGameserverId",
                table: "HLL_GameStates",
                column: "HLLGameserverId");

            migrationBuilder.CreateIndex(
                name: "IX_HLL_Logs_HLLGameserverId",
                table: "HLL_Logs",
                column: "HLLGameserverId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationRolePermission");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CF_Configurations");

            migrationBuilder.DropTable(
                name: "DC_EmbedFields");

            migrationBuilder.DropTable(
                name: "DC_StatsChannels");

            migrationBuilder.DropTable(
                name: "DiscordEmbedDiscordMessageComponent");

            migrationBuilder.DropTable(
                name: "DiscordModalDiscordModalComponent");

            migrationBuilder.DropTable(
                name: "HLL_GamePlayers");

            migrationBuilder.DropTable(
                name: "HLL_GameStates");

            migrationBuilder.DropTable(
                name: "HLL_Logs");

            migrationBuilder.DropTable(
                name: "HLL_PlayerSessions");

            migrationBuilder.DropTable(
                name: "US_SteamIds");

            migrationBuilder.DropTable(
                name: "US_Permissions");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "DC_Embeds");

            migrationBuilder.DropTable(
                name: "DC_MessageComponents");

            migrationBuilder.DropTable(
                name: "DC_ModalComponents");

            migrationBuilder.DropTable(
                name: "HLL_Games");

            migrationBuilder.DropTable(
                name: "HLL_Players");

            migrationBuilder.DropTable(
                name: "DC_Authors");

            migrationBuilder.DropTable(
                name: "DC_Channels");

            migrationBuilder.DropTable(
                name: "DC_Modals");

            migrationBuilder.DropTable(
                name: "HLL_Gameservers");

            migrationBuilder.DropTable(
                name: "DC_Guilds");
        }
    }
}
