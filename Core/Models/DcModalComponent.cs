using System;
using System.Collections.Generic;

namespace Sparta.Core.Models;

public partial class DcModalComponent
{
    public string Id { get; set; } = null!;

    public string Label { get; set; } = null!;

    public int InputStyle { get; set; }

    public string? Placeholder { get; set; }

    public int? MinLength { get; set; }

    public int? MaxLength { get; set; }

    public bool? Required { get; set; }

    public string? DefaultValue { get; set; }

    public virtual ICollection<DcModal> DiscordModals { get; set; } = new List<DcModal>();
}
