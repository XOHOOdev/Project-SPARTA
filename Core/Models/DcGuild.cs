using System;
using System.Collections.Generic;

namespace Sparta.Core.Models;

public partial class DcGuild
{
    public decimal Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<DcChannel> DcChannels { get; set; } = new List<DcChannel>();
}
