using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sparta.BlazorUI.Entities;

public class DiscordGuild
{
    [Required]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public ulong Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual IList<DiscordChannel> Channels { get; set; } = null!;
}