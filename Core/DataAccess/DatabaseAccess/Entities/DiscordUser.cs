using Sparta.Core.Dto.Modules;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sparta.Core.DataAccess.DatabaseAccess.Entities
{
    public class DiscordUser : IModuleParameterType
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] public ulong Id { get; set; }

        public string Name { get; set; } = null!;

        public virtual List<DiscordGuild> DiscordGuilds { get; set; } = [];

        public virtual List<DiscordRole> Roles { get; set; } = [];

        public override bool Equals(object? obj)
        {
            if (obj is not DiscordUser user) return false;
            return Id == user.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
