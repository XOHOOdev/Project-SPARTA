﻿using Sparta.Core.Dto.Modules;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sparta.Core.DataAccess.DatabaseAccess.Entities
{
    public class DiscordChannel : IModuleParameterType
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] public ulong Id { get; set; }

        public string Name { get; set; } = null!;

        public virtual DiscordGuild DiscordGuild { get; set; } = null!;

        public override bool Equals(object? obj)
        {
            if (obj is not DiscordChannel channel) return false;
            return Id == channel.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
