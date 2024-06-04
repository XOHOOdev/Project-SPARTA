using System;
using System.Collections.Generic;

namespace Sparta.Core.Models;

public partial class DcEmbed
{
    public long Id { get; set; }

    public bool Updated { get; set; }

    public long MessageId { get; set; }

    public int ColorArgb { get; set; }

    public string? Description { get; set; }

    public string? FooterIconUrl { get; set; }

    public string? FooterText { get; set; }

    public string? ImageUrl { get; set; }

    public string? ThumbnailUrl { get; set; }

    public string? Title { get; set; }

    public string? Url { get; set; }

    public long? DiscordAuthorId { get; set; }

    public decimal? DiscordChannelId { get; set; }

    public virtual ICollection<DcEmbedField> DcEmbedFields { get; set; } = new List<DcEmbedField>();

    public virtual DcAuthor? DiscordAuthor { get; set; }

    public virtual DcChannel? DiscordChannel { get; set; }

    public virtual ICollection<DcMessageComponent> MessageComponents { get; set; } = new List<DcMessageComponent>();
}
