using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sparta.Core.DataAccess.DatabaseAccess.Entities;

public class Configuration
{
    [Required]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual List<Configuration>? Children { get; set; }

    public virtual Configuration? Parent { get; set; }

    public string? Value { get; set; }
}