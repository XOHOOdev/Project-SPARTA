using System;
using System.Collections.Generic;

namespace Helium.Core.Models;

public partial class DcStatsChannel
{
    public long Id { get; set; }

    public string MessageId { get; set; } = null!;
}
