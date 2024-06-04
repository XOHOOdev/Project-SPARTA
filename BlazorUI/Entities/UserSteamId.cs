using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Helium.BlazorUI.Entities
{
    public class UserSteamId
    {
        [Required]
        [Key]
        [ForeignKey("AspNetUsers")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string UserId { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(HLLPlayer))]
        public long SteamId { get; set; }
    }
}
