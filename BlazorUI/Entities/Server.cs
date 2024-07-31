using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sparta.BlazorUI.Entities
{
    public class Server
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string Name { get; set; } = null!;

        public string Url { get; set; } = null!;

        public int Port { get; set; }

        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

        public ServerType Type { get; set; }

        public virtual List<Module> Modules { get; set; } = [];
    }
}
