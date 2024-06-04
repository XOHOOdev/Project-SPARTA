using System;
using System.Collections.Generic;

namespace Helium.Core.Models;

public partial class DcMessageComponent
{
    public string Id { get; set; } = null!;

    public string Label { get; set; } = null!;

    public bool IsDisbled { get; set; }

    public int? ButtonStyle { get; set; }

    public string? Emote { get; set; }

    public string? Url { get; set; }

    public int Action { get; set; }

    public decimal? ActionChannelId { get; set; }

    public string? ActionModalId { get; set; }

    public virtual DcChannel? ActionChannel { get; set; }

    public virtual DcModal? ActionModal { get; set; }

    public virtual ICollection<DcEmbed> Embeds { get; set; } = new List<DcEmbed>();
}
