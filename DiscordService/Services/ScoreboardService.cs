using Helium.Core.Models;

namespace Helium.DiscordService.Services
{
    internal class ScoreboardService : IScoreboardService
    {
        private readonly HeliumDbContext _context;

        ScoreboardService(HeliumDbContext dbContext)
        {
            _context = dbContext;
        }

        public void SetScoreboards()
        {
        }
    }
}
