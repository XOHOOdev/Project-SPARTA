using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sparta.Core.DataAccess.DatabaseAccess.Entities
{
    public class RunnerStatus
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public DateTime LastActive { get; set; }
    }
}
