using Sparta.Modules.Dto;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sparta.BlazorUI.Entities
{
    public class DiscordUser : IDiscordType
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] public ulong Id { get; set; }

        public string Name { get; set; } = null!;

        public virtual List<DiscordGuild> DiscordGuilds { get; set; } = [];

        public virtual List<DiscordRole> Roles { get; set; } = [];
    }
}
