using System.Reflection;

namespace Sparta.Modules
{
    public static class Modules
    {
        private static readonly string[] StringsToExclude = ["Sparta", "Modules", "DataAccess", "Dto", "Interface", "Templates"];

        public static IEnumerable<string> GetModules()
        {
            return Assembly
                .GetAssembly(typeof(Modules))?
                .GetTypes()
                .SelectMany(t => t.Namespace?.Split('.') ?? [])
                .GroupBy(n => n)
                .Distinct()
                .Select(g => g.Key)
                .Where(x => !StringsToExclude.Contains(x)) ?? [];
        }
    }
}
