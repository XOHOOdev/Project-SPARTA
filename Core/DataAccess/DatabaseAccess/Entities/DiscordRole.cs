using Sparta.Core.Dto.Modules;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sparta.Core.DataAccess.DatabaseAccess.Entities
{
    public class DiscordRole : IModuleParameterType
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] public ulong Id { get; set; }

        public string Name { get; set; } = null!;

        public virtual DiscordGuild Guild { get; set; } = null!;

        public virtual List<DiscordUser> Users { get; set; } = [];
    }
}
