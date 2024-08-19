using Sparta.Modules.Dto;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sparta.BlazorUI.Entities
{
    public class DiscordChannel : IDiscordType
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] public ulong Id { get; set; }

        public string Name { get; set; } = null!;

        public virtual DiscordGuild DiscordGuild { get; set; } = null!;
    }
}
