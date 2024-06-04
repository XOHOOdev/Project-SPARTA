using System;
using System.Collections.Generic;

namespace Sparta.Core.Models;

public partial class HllGameState
{
    public int Id { get; set; }

    public int AlliedPlayers { get; set; }

    public int AxisPlayers { get; set; }

    public int AlliedScore { get; set; }

    public int AxisScore { get; set; }

    public TimeSpan RemainingTime { get; set; }

    public string Map { get; set; } = null!;

    public string NextMap { get; set; } = null!;

    public int SlotsCurrent { get; set; }

    public int SlotsTotal { get; set; }

    public long? HllgameserverId { get; set; }

    public virtual HllGameserver? Hllgameserver { get; set; }
}
