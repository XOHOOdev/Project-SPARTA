using System;
using System.Collections.Generic;

namespace Helium.Core.Models;

public partial class DcEmbedField
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string Value { get; set; } = null!;

    public bool Inline { get; set; }

    public long DiscordEmbedId { get; set; }

    public virtual DcEmbed DiscordEmbed { get; set; } = null!;
}
