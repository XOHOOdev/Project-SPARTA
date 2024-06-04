using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Helium.BlazorUI.Entities
{
    public class DiscordChannel
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public ulong Id { get; set; }

        public string Name { get; set; } = null!;

        public string Type { get; set; } = null!;

        public virtual List<DiscordEmbed> Embeds { get; } = new();

        public virtual DiscordGuild DiscordGuild { get; } = new();
    }
}
