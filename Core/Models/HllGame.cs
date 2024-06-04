using System;
using System.Collections.Generic;

namespace Sparta.Core.Models;

public partial class HllGame
{
    public long GameId { get; set; }

    public DateTime StartTime { get; set; }

    public TimeSpan? Duration { get; set; }

    public string Map { get; set; } = null!;

    public long? HllgameserverId { get; set; }

    public virtual ICollection<HllGamePlayer> HllGamePlayers { get; set; } = new List<HllGamePlayer>();

    public virtual HllGameserver? Hllgameserver { get; set; }
}
