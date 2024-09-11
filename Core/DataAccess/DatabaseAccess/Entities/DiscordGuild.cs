using Sparta.Core.Dto.Modules;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sparta.Core.DataAccess.DatabaseAccess.Entities
{
    public class DiscordGuild : IModuleParameterType
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] public ulong Id { get; set; }

        public string Name { get; set; } = null!;

        public virtual List<DiscordChannel> Channels { get; set; } = [];

        public virtual List<DiscordUser> Users { get; set; } = [];

        public virtual List<DiscordRole> Roles { get; set; } = [];

        public virtual List<ApplicationRole> ApplicationRoles { get; set; } = [];
    }
}
