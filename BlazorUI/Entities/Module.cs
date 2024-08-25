using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sparta.BlazorUI.Entities
{
    public class Module
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string Name { get; set; } = null!;

        public bool Enabled { get; set; } = false;

        public virtual ModuleType Type { get; set; } = null!;

        public virtual List<ModuleParameter> Parameters { get; set; } = [];
    }
}
