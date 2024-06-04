using System.ComponentModel.DataAnnotations;

namespace Sparta.BlazorUI.Entities;

public class HLLGameState
{
    /**
     * Players: Allied: 1 - Axis: 1
     * Score: Allied: 2 - Axis: 2
     * Remaining Time: 1:13:30
     * Map: omahabeach_warfare
     * Next Map: carentan_warfare
     */

    [Key]
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
}