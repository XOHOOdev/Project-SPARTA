﻿// <auto-generated />
using System;
using Sparta.BlazorUI.Data;
using Sparta.BlazorUI.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Sparta.BlazorUI.Data.Model
{
    [DbContext(typeof(ApplicationDbContext<IdentityUser, ApplicationRole, string>))]
    [Migration("20231205184957_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ApplicationRolePermission", b =>
                {
                    b.Property<string>("PermissionsId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RolesId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("PermissionsId", "RolesId");

                    b.HasIndex("RolesId");

                    b.ToTable("ApplicationRolePermission");
                });

            modelBuilder.Entity("DiscordEmbedDiscordMessageComponent", b =>
                {
                    b.Property<long>("EmbedsId")
                        .HasColumnType("bigint");

                    b.Property<string>("MessageComponentsId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("EmbedsId", "MessageComponentsId");

                    b.HasIndex("MessageComponentsId");

                    b.ToTable("DiscordEmbedDiscordMessageComponent");
                });

            modelBuilder.Entity("DiscordModalDiscordModalComponent", b =>
                {
                    b.Property<string>("ComponentsId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DiscordModalsId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ComponentsId", "DiscordModalsId");

                    b.HasIndex("DiscordModalsId");

                    b.ToTable("DiscordModalDiscordModalComponent");
                });

            modelBuilder.Entity("Sparta.BlazorUI.Entities.ApplicationRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Sparta.BlazorUI.Entities.Configuration", b =>
                {
                    b.Property<string>("Class")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Property")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Class", "Property");

                    b.ToTable("CF_Configurations");
                });

            modelBuilder.Entity("Sparta.BlazorUI.Entities.DiscordAuthor", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("IconUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("DC_Authors");
                });

            modelBuilder.Entity("Sparta.BlazorUI.Entities.DiscordChannel", b =>
                {
                    b.Property<decimal>("Id")
                        .HasColumnType("decimal(20,0)");

                    b.Property<decimal?>("DiscordGuildId")
                        .HasColumnType("decimal(20,0)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DiscordGuildId");

                    b.ToTable("DC_Channels");
                });

            modelBuilder.Entity("Sparta.BlazorUI.Entities.DiscordEmbed", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<int>("ColorArgb")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("DiscordAuthorId")
                        .HasColumnType("bigint");

                    b.Property<decimal?>("DiscordChannelId")
                        .HasColumnType("decimal(20,0)");

                    b.Property<string>("FooterIconUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FooterText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("MessageId")
                        .HasColumnType("bigint");

                    b.Property<string>("ThumbnailUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Updated")
                        .HasColumnType("bit");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DiscordAuthorId");

                    b.HasIndex("DiscordChannelId");

                    b.ToTable("DC_Embeds");
                });

            modelBuilder.Entity("Sparta.BlazorUI.Entities.DiscordEmbedFields", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("DiscordEmbedId")
                        .HasColumnType("bigint");

                    b.Property<bool>("Inline")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DiscordEmbedId");

                    b.ToTable("DC_EmbedFields");
                });

            modelBuilder.Entity("Sparta.BlazorUI.Entities.DiscordGuild", b =>
                {
                    b.Property<decimal>("Id")
                        .HasColumnType("decimal(20,0)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("DC_Guilds");
                });

            modelBuilder.Entity("Sparta.BlazorUI.Entities.DiscordMessageComponent", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Action")
                        .HasColumnType("int");

                    b.Property<decimal?>("ActionChannelId")
                        .HasColumnType("decimal(20,0)");

                    b.Property<string>("ActionModalId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("ButtonStyle")
                        .HasColumnType("int");

                    b.Property<string>("Emote")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDisbled")
                        .HasColumnType("bit");

                    b.Property<string>("Label")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ActionChannelId");

                    b.HasIndex("ActionModalId");

                    b.ToTable("DC_MessageComponents");
                });

            modelBuilder.Entity("Sparta.BlazorUI.Entities.DiscordModal", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("DC_Modals");
                });

            modelBuilder.Entity("Sparta.BlazorUI.Entities.DiscordModalComponent", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DefaultValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("InputStyle")
                        .HasColumnType("int");

                    b.Property<string>("Label")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MaxLength")
                        .HasColumnType("int");

                    b.Property<int?>("MinLength")
                        .HasColumnType("int");

                    b.Property<string>("Placeholder")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("Required")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("DC_ModalComponents");
                });

            modelBuilder.Entity("Sparta.BlazorUI.Entities.DiscordStatsMessage", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("MessageId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("DC_StatsChannels");
                });

            modelBuilder.Entity("Sparta.BlazorUI.Entities.HLLGame", b =>
                {
                    b.Property<long>("GameId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("GameId"));

                    b.Property<TimeSpan?>("Duration")
                        .HasColumnType("time");

                    b.Property<long?>("HLLGameserverId")
                        .HasColumnType("bigint");

                    b.Property<string>("Map")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.HasKey("GameId");

                    b.HasIndex("HLLGameserverId");

                    b.ToTable("HLL_Games");
                });

            modelBuilder.Entity("Sparta.BlazorUI.Entities.HLLGamePlayer", b =>
                {
                    b.Property<long>("SteamId")
                        .HasColumnType("bigint");

                    b.Property<long>("GameId")
                        .HasColumnType("bigint");

                    b.Property<int>("CombatScore")
                        .HasColumnType("int");

                    b.Property<int>("DefensiveScore")
                        .HasColumnType("int");

                    b.Property<string>("Loadout")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OffensiveScore")
                        .HasColumnType("int");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SupportScore")
                        .HasColumnType("int");

                    b.Property<string>("Team")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SteamId", "GameId");

                    b.HasIndex("GameId");

                    b.ToTable("HLL_GamePlayers");
                });

            modelBuilder.Entity("Sparta.BlazorUI.Entities.HLLGameState", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AlliedPlayers")
                        .HasColumnType("int");

                    b.Property<int>("AlliedScore")
                        .HasColumnType("int");

                    b.Property<int>("AxisPlayers")
                        .HasColumnType("int");

                    b.Property<int>("AxisScore")
                        .HasColumnType("int");

                    b.Property<long?>("HLLGameserverId")
                        .HasColumnType("bigint");

                    b.Property<string>("Map")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NextMap")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("RemainingTime")
                        .HasColumnType("time");

                    b.Property<int>("SlotsCurrent")
                        .HasColumnType("int");

                    b.Property<int>("SlotsTotal")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("HLLGameserverId");

                    b.ToTable("HLL_GameStates");
                });

            modelBuilder.Entity("Sparta.BlazorUI.Entities.HLLGameserver", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Port")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("HLL_Gameservers");
                });

            modelBuilder.Entity("Sparta.BlazorUI.Entities.HLLLog", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Arguments")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("HLLGameserverId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("LogTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("LogType")
                        .HasColumnType("int");

                    b.Property<long>("ParticipantId1")
                        .HasColumnType("bigint");

                    b.Property<long>("ParticipantId2")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("HLLGameserverId");

                    b.ToTable("HLL_Logs");
                });

            modelBuilder.Entity("Sparta.BlazorUI.Entities.HLLPlayer", b =>
                {
                    b.Property<long>("SteamId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("FirstLogon")
                        .HasColumnType("datetime2");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SteamId");

                    b.ToTable("HLL_Players");
                });

            modelBuilder.Entity("Sparta.BlazorUI.Entities.HLLPlayerSession", b =>
                {
                    b.Property<long>("SteamId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<long>("ServerId")
                        .HasColumnType("bigint");

                    b.HasKey("SteamId", "StartTime");

                    b.ToTable("HLL_PlayerSessions");
                });

            modelBuilder.Entity("Sparta.BlazorUI.Entities.Permission", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("US_Permissions");
                });

            modelBuilder.Entity("Sparta.BlazorUI.Entities.UserSteamId", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<long>("SteamId")
                        .HasColumnType("bigint");

                    b.HasKey("UserId");

                    b.ToTable("US_SteamIds");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("ApplicationRolePermission", b =>
                {
                    b.HasOne("Sparta.BlazorUI.Entities.Permission", null)
                        .WithMany()
                        .HasForeignKey("PermissionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sparta.BlazorUI.Entities.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DiscordEmbedDiscordMessageComponent", b =>
                {
                    b.HasOne("Sparta.BlazorUI.Entities.DiscordEmbed", null)
                        .WithMany()
                        .HasForeignKey("EmbedsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sparta.BlazorUI.Entities.DiscordMessageComponent", null)
                        .WithMany()
                        .HasForeignKey("MessageComponentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DiscordModalDiscordModalComponent", b =>
                {
                    b.HasOne("Sparta.BlazorUI.Entities.DiscordModalComponent", null)
                        .WithMany()
                        .HasForeignKey("ComponentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sparta.BlazorUI.Entities.DiscordModal", null)
                        .WithMany()
                        .HasForeignKey("DiscordModalsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Sparta.BlazorUI.Entities.DiscordChannel", b =>
                {
                    b.HasOne("Sparta.BlazorUI.Entities.DiscordGuild", null)
                        .WithMany("Channels")
                        .HasForeignKey("DiscordGuildId");
                });

            modelBuilder.Entity("Sparta.BlazorUI.Entities.DiscordEmbed", b =>
                {
                    b.HasOne("Sparta.BlazorUI.Entities.DiscordAuthor", null)
                        .WithMany("Embeds")
                        .HasForeignKey("DiscordAuthorId");

                    b.HasOne("Sparta.BlazorUI.Entities.DiscordChannel", null)
                        .WithMany("Embeds")
                        .HasForeignKey("DiscordChannelId");
                });

            modelBuilder.Entity("Sparta.BlazorUI.Entities.DiscordEmbedFields", b =>
                {
                    b.HasOne("Sparta.BlazorUI.Entities.DiscordEmbed", "DiscordEmbed")
                        .WithMany("Fields")
                        .HasForeignKey("DiscordEmbedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DiscordEmbed");
                });

            modelBuilder.Entity("Sparta.BlazorUI.Entities.DiscordMessageComponent", b =>
                {
                    b.HasOne("Sparta.BlazorUI.Entities.DiscordChannel", "ActionChannel")
                        .WithMany()
                        .HasForeignKey("ActionChannelId");

                    b.HasOne("Sparta.BlazorUI.Entities.DiscordModal", "ActionModal")
                        .WithMany("MessageComponents")
                        .HasForeignKey("ActionModalId");

                    b.Navigation("ActionChannel");

                    b.Navigation("ActionModal");
                });

            modelBuilder.Entity("Sparta.BlazorUI.Entities.HLLGame", b =>
                {
                    b.HasOne("Sparta.BlazorUI.Entities.HLLGameserver", null)
                        .WithMany("Games")
                        .HasForeignKey("HLLGameserverId");
                });

            modelBuilder.Entity("Sparta.BlazorUI.Entities.HLLGamePlayer", b =>
                {
                    b.HasOne("Sparta.BlazorUI.Entities.HLLGame", null)
                        .WithMany("Players")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sparta.BlazorUI.Entities.HLLPlayer", null)
                        .WithMany("GamePlayers")
                        .HasForeignKey("SteamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Sparta.BlazorUI.Entities.HLLGameState", b =>
                {
                    b.HasOne("Sparta.BlazorUI.Entities.HLLGameserver", null)
                        .WithMany("GameStates")
                        .HasForeignKey("HLLGameserverId");
                });

            modelBuilder.Entity("Sparta.BlazorUI.Entities.HLLLog", b =>
                {
                    b.HasOne("Sparta.BlazorUI.Entities.HLLGameserver", null)
                        .WithMany("Logs")
                        .HasForeignKey("HLLGameserverId");
                });

            modelBuilder.Entity("Sparta.BlazorUI.Entities.HLLPlayerSession", b =>
                {
                    b.HasOne("Sparta.BlazorUI.Entities.HLLPlayer", null)
                        .WithMany("Sessions")
                        .HasForeignKey("SteamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Sparta.BlazorUI.Entities.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Sparta.BlazorUI.Entities.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Sparta.BlazorUI.Entities.DiscordAuthor", b =>
                {
                    b.Navigation("Embeds");
                });

            modelBuilder.Entity("Sparta.BlazorUI.Entities.DiscordChannel", b =>
                {
                    b.Navigation("Embeds");
                });

            modelBuilder.Entity("Sparta.BlazorUI.Entities.DiscordEmbed", b =>
                {
                    b.Navigation("Fields");
                });

            modelBuilder.Entity("Sparta.BlazorUI.Entities.DiscordGuild", b =>
                {
                    b.Navigation("Channels");
                });

            modelBuilder.Entity("Sparta.BlazorUI.Entities.DiscordModal", b =>
                {
                    b.Navigation("MessageComponents");
                });

            modelBuilder.Entity("Sparta.BlazorUI.Entities.HLLGame", b =>
                {
                    b.Navigation("Players");
                });

            modelBuilder.Entity("Sparta.BlazorUI.Entities.HLLGameserver", b =>
                {
                    b.Navigation("GameStates");

                    b.Navigation("Games");

                    b.Navigation("Logs");
                });

            modelBuilder.Entity("Sparta.BlazorUI.Entities.HLLPlayer", b =>
                {
                    b.Navigation("GamePlayers");

                    b.Navigation("Sessions");
                });
#pragma warning restore 612, 618
        }
    }
}
