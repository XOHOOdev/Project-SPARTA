using Sparta.Modules.Interface;
using Sparta.Modules.Interface.ModuleParameters;

namespace Sparta.Modules.HllServerSeeding
{
    public class HllServerSeedingParameters : ModuleParametersBase
    {
        public ServerParameter Server { get; set; } = null!;

        public NumberParameter MaxPlayerCount { get; set; } = null!;

        public TextParameter MessageBy { get; set; } = null!;

        public LargeTextParameter Message { get; set; } = null!;

        internal bool IsSeeding { get; set; }
    }
}
