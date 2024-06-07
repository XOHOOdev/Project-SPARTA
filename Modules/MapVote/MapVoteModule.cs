using Sparta.Modules.Interface;

namespace Sparta.Modules.MapVote
{
    public class MapVoteModule : IModule
    {
        public Type GetModuleParameters()
        {
            return typeof(MapVoteParameters);
        }
    }
}