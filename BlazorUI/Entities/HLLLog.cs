using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sparta.BlazorUI.Entities;

public class HLLLog
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    public DateTime LogTime { get; set; }

    public HLLLogType LogType { get; set; }

    [ForeignKey(nameof(HLLPlayer))] public long ParticipantId1 { get; set; }

    [ForeignKey(nameof(HLLPlayer))] public long ParticipantId2 { get; set; }

    public string? Arguments { get; set; } = null!;
}