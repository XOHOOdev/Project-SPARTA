using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Sparta.BlazorUI.Entities;

[PrimaryKey(nameof(SteamId), nameof(StartTime))]
public class HLLPlayerSession
{
    [Required]
    [ForeignKey(nameof(HLLPlayer))]
    public long SteamId { get; set; }

    public long ServerId { get; set; }

    [Required] public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }
}