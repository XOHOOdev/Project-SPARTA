using Microsoft.EntityFrameworkCore;

namespace Helium.BlazorUI.Entities
{
    [PrimaryKey(nameof(Class), nameof(Property))]
    public class Configuration
    {
        public string Class { get; set; } = null!;

        public string Property { get; set; } = null!;

        public string Value { get; set; } = null!;
    }
}
