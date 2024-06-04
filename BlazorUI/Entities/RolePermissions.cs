using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Helium.BlazorUI.Entities
{
    [PrimaryKey(nameof(RoleId), nameof(Permission))]
    public class RolePermissions
    {
        [ForeignKey("AspNetRoles")]
        public string RoleId { get; set; } = null!;
        public Permission Permission { get; set; } = null!;
    }
}
