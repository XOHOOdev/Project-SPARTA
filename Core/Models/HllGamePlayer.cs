using System;
using System.Collections.Generic;

namespace Sparta.Core.Models;

public partial class HllGamePlayer
{
    public long SteamId { get; set; }

    public long GameId { get; set; }

    public string Team { get; set; } = null!;

    public string Role { get; set; } = null!;

    public string Unit { get; set; } = null!;

    public string Loadout { get; set; } = null!;

    public int CombatScore { get; set; }

    public int OffensiveScore { get; set; }

    public int DefensiveScore { get; set; }

    public int SupportScore { get; set; }

    public virtual HllGame Game { get; set; } = null!;

    public virtual HllPlayer Steam { get; set; } = null!;
}
