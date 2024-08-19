using System;
using System.Collections.Generic;

namespace Sparta.Core.Models;

public partial class DcChannel
{
    public decimal Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal DiscordGuildId { get; set; }

    public virtual DcGuild DiscordGuild { get; set; } = null!;
}
