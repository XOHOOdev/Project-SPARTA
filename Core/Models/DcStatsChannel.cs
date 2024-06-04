using System;
using System.Collections.Generic;

namespace Sparta.Core.Models;

public partial class DcStatsChannel
{
    public long Id { get; set; }

    public string MessageId { get; set; } = null!;
}
