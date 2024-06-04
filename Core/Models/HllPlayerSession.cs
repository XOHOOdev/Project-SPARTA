using System;
using System.Collections.Generic;

namespace Sparta.Core.Models;

public partial class HllPlayerSession
{
    public long SteamId { get; set; }

    public DateTime StartTime { get; set; }

    public long ServerId { get; set; }

    public DateTime EndTime { get; set; }

    public virtual HllPlayer Steam { get; set; } = null!;
}
