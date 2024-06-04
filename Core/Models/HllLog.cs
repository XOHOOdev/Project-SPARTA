using System;
using System.Collections.Generic;

namespace Helium.Core.Models;

public partial class HllLog
{
    public long Id { get; set; }

    public DateTime LogTime { get; set; }

    public int LogType { get; set; }

    public long ParticipantId1 { get; set; }

    public long ParticipantId2 { get; set; }

    public string? Arguments { get; set; }

    public long? HllgameserverId { get; set; }

    public virtual HllGameserver? Hllgameserver { get; set; }
}
