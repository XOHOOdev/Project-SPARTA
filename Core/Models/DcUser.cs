using System;
using System.Collections.Generic;

namespace Sparta.Core.Models;

public partial class DcUser
{
    public decimal Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<DcGuild> DiscordGuilds { get; set; } = new List<DcGuild>();

    public virtual ICollection<DcRole> Roles { get; set; } = new List<DcRole>();
}
