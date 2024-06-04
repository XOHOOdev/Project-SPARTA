using System;
using System.Collections.Generic;

namespace Sparta.Core.Models;

public partial class DcAuthor
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string? IconUrl { get; set; }

    public string? Url { get; set; }

    public virtual ICollection<DcEmbed> DcEmbeds { get; set; } = new List<DcEmbed>();
}
