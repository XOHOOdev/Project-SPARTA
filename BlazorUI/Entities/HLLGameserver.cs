using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Helium.BlazorUI.Entities
{
    public class HLLGameserver
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string Address { get; set; } = null!;

        public int Port { get; set; }

        public string Password { get; set; } = null!;

        public string Name { get; set; } = null!;

        public virtual List<HLLGameState> GameStates { get; } = new();

        public virtual List<HLLGame> Games { get; } = new();

        public virtual List<HLLLog> Logs { get; } = new();
    }
}
