using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Helium.BlazorUI.Entities
{
    public class HLLGame
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long GameId { get; set; }

        public DateTime StartTime { get; set; }

        public TimeSpan? Duration { get; set; }

        public string Map { get; set; } = null!;

        public virtual List<HLLGamePlayer> Players { get; } = new();
    }
}
