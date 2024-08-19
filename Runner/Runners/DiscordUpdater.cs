using Sparta.Core.DataAccess;
using Sparta.Core.Models;

namespace Sparta.Runner.Runners
{
    public class DiscordUpdater(SpartaDbContext context, DiscordAccess discord)
    {
        public void Update()
        {
            var guilds = discord.GetGuilds();

            context.DcGuilds.UpdateRange(guilds.Where(g => context.DcGuilds.Any(dg => g.Id == dg.Id)));
            context.DcGuilds.AddRange(guilds.Where(g => !context.DcGuilds.Any(dg => g.Id == dg.Id)));

            var channels = discord.GetChannels();

            context.DcChannels.UpdateRange(channels.Where(c => context.DcChannels.Any(dc => c.Id == dc.Id)));
            context.DcChannels.AddRange(channels.Where(c => !context.DcChannels.Any(dc => c.Id == dc.Id)));

            var users = discord.GetUsers();

            context.DcUsers.UpdateRange(users.Where(u => context.DcUsers.Any(du => du.Id == u.Id)));
            context.DcUsers.AddRange(users.Where(u => !context.DcUsers.Any(du => du.Id == u.Id)));

            var roles = discord.GetRoles();

            context.DcRoles.UpdateRange(roles.Where(r => context.DcRoles.Any(dr => dr.Id == r.Id)));
            context.DcRoles.AddRange(roles.Where(r => !context.DcRoles.Any(dr => dr.Id == r.Id)));

            context.SaveChanges();
        }
    }
}
