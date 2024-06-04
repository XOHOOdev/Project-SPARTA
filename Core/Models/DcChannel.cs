using System;
using System.Collections.Generic;

namespace Sparta.Core.Models;

public partial class DcChannel
{
    public decimal Id { get; set; }

    public string Name { get; set; } = null!;

    public string Type { get; set; } = null!;

    public decimal? DiscordGuildId { get; set; }

    public virtual ICollection<DcEmbed> DcEmbeds { get; set; } = new List<DcEmbed>();

    public virtual ICollection<DcMessageComponent> DcMessageComponents { get; set; } = new List<DcMessageComponent>();

    public virtual DcGuild? DiscordGuild { get; set; }
}
