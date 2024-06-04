using System;
using System.Collections.Generic;

namespace Helium.Core.Models;

public partial class HllGameserver
{
    public long Id { get; set; }

    public string Address { get; set; } = null!;

    public int Port { get; set; }

    public string Password { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual ICollection<HllGameState> HllGameStates { get; set; } = new List<HllGameState>();

    public virtual ICollection<HllGame> HllGames { get; set; } = new List<HllGame>();

    public virtual ICollection<HllLog> HllLogs { get; set; } = new List<HllLog>();
}
