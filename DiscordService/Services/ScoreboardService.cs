using Sparta.Core.Models;

namespace Sparta.DiscordService.Services
{
    internal class ScoreboardService : IScoreboardService
    {
        private readonly SpartaDbContext _context;

        ScoreboardService(SpartaDbContext dbContext)
        {
            _context = dbContext;
        }

        public void SetScoreboards()
        {
        }
    }
}
