namespace Sparta.BlazorUI.Entities
{
    public class Module
    {
        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

        public virtual List<ModuleParameter> Parameters { get; } = [];
    }
}
