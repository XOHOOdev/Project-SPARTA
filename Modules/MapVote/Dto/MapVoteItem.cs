namespace Sparta.Modules.MapVote.Dto
{
    internal class MapVoteItem
    {
        internal string MapName { get; set; } = null!;

        internal MapVoteState State { get; set; }
    }

    internal enum MapVoteState
    {
        Unknown = 0,
        Team1 = 1,
        Team2 = 2
    }
}
