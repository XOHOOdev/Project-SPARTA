using System;
using System.Collections.Generic;

namespace Helium.Core.Models;

public partial class HllPlayer
{
    public long SteamId { get; set; }

    public string Name { get; set; } = null!;

    public DateTime FirstLogon { get; set; }

    public int Level { get; set; }

    public virtual ICollection<HllGamePlayer> HllGamePlayers { get; set; } = new List<HllGamePlayer>();

    public virtual ICollection<HllPlayerSession> HllPlayerSessions { get; set; } = new List<HllPlayerSession>();
}
