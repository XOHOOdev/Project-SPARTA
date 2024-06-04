﻿using Helium.BlazorUI.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Helium.BlazorUI.Data
{
    public class ApplicationDbContext<TUser, TRole, TKey> : IdentityDbContext<
    TUser, TRole, TKey, IdentityUserClaim<TKey>, IdentityUserRole<TKey>,
    IdentityUserLogin<TKey>, IdentityRoleClaim<TKey>, IdentityUserToken<TKey>>
        where TUser : IdentityUser<TKey>
        where TRole : IdentityRole<TKey>
        where TKey : IEquatable<TKey>
    {
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

        public DbSet<HLLGameserver> HLL_Gameservers { get; set; }

        public DbSet<HLLGameState> HLL_GameStates { get; set; }

        public DbSet<HLLGame> HLL_Games { get; set; }

        public DbSet<HLLPlayer> HLL_Players { get; set; }

        public DbSet<HLLGamePlayer> HLL_GamePlayers { get; set; }

        public DbSet<HLLLog> HLL_Logs { get; set; }

        public DbSet<HLLPlayerSession> HLL_PlayerSessions { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext<TUser, TRole, TKey>> options)
            : base(options)
        {
        }
    }
}