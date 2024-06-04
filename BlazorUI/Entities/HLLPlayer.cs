using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Helium.BlazorUI.Entities
{
    public class HLLPlayer
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SteamId { get; set; }

        public string Name { get; set; } = null!;

        public DateTime FirstLogon { get; set; }

        public int Level { get; set; }

        public virtual List<HLLPlayerSession> Sessions { get; } = new()!;

        public virtual List<HLLGamePlayer> GamePlayers { get; } = new();
    }
}
