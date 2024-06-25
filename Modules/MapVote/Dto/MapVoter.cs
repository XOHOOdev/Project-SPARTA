namespace Sparta.Modules.MapVote.Dto
{
    internal class MapVoter
    {
        internal ulong UserId { get; set; }

        internal ulong RoleId { get; set; }

        internal string RoleMention { get; set; } = null!;

        internal MapVoteState State { get; set; }
    }
}
