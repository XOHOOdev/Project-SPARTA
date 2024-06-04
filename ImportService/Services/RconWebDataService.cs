using Helium.Core.Helpers;
using Helium.Core.Models;
using Helium.ImportService.Connections;
using Helium.ImportService.Helper;

namespace Helium.ImportService.Services
{
    public class RconWebDataService
    {
        private readonly HeliumDbContext _context;

        #region Constructor

        public RconWebDataService(HeliumDbContext dbContext)
        {
            _context = dbContext;

            EstablishConnections();
        }
        #endregion Constructor

        #region EstablishConnections
        private void EstablishConnections()
        {
            EstablishConnection("RCON");

            int counter = 1;
            while (EstablishConnection($"RCON{counter}"))
            {
                counter++;
            }
        }

        private bool EstablishConnection(string configClass)
        {
            string? address = ConfigHelper.GetConfig(configClass, "Address");
            int port = int.Parse(ConfigHelper.GetConfig(configClass, "Port") ?? "0");
            string? password = ConfigHelper.GetConfig(configClass, "Password");

            if (address == null || port == 0 || password == null)
            {
                return false;
            }

            string serverName;

            using (RconWebConnection connection = new())
            {
                if (!connection.Connect(address, port, password))
                {
                    return false;
                }
                serverName = connection.Request("Get Name");
            }

            HllGameserver hllGameserver = _context.HllGameservers.FirstOrDefault(row => row.Address == address && row.Port == port && row.Password == password) ?? new HllGameserver { Address = address, Port = port, Password = password, Name = serverName };
            _context.Update(hllGameserver);
            _context.SaveChanges();

            return true;
        }

        private string? RunCommand(long id, string command)
        {
            if (_context.Find(typeof(HllGameserver), id) is not HllGameserver gameserver) return null;

            using RconWebConnection connection = new();
            if (!connection.Connect(gameserver))
            {
                return null;
            }
            return connection.Request(command);
        }
        #endregion EstablishConnections

        #region Interface Implementations
        public void GetGameState(long id)
        {
            lock (_context)
            {
                string? gameState = RunCommand(id, "Get Gamestate");
                string? slots = RunCommand(id, "Get Slots");
                if (gameState == null || slots == null) return;
                HllGameState? gameStateServ = _context.Find(typeof(HllGameState), id) as HllGameState;
                bool exists = gameStateServ != null;
                gameStateServ ??= new HllGameState { HllgameserverId = id };

                Parser.ParseGameState(ref gameStateServ, gameState, slots);

                if (exists)
                {
                    _context.Update(gameStateServ);
                }
                else
                {
                    _context.Add(gameStateServ);
                }

                _context.SaveChanges();
            }
        }

        public void GetGame(long id)
        {
            lock (_context)
            {
                HllGame? currentGame = _context.HllGames.OrderBy(x => x.GameId).LastOrDefault(x => x.HllgameserverId == id && x.Duration == TimeSpan.Zero);
                if (currentGame == null)
                {
                    HllLog? gameStart = _context.HllLogs.OrderBy(x => x.Id).LastOrDefault(x => x.HllgameserverId == id && x.LogType == 12);
                    HllGame? lastGame = _context.HllGames.OrderBy(x => x.GameId).LastOrDefault(x => x.HllgameserverId == id);
                    if (lastGame != null && lastGame.StartTime == gameStart?.LogTime) return;

                    currentGame = new HllGame
                    {
                        HllgameserverId = id,
                        StartTime = gameStart?.LogTime ?? DateTime.Now,
                        Map = _context.HllGameStates.FirstOrDefault(x => x.HllgameserverId == id)?.Map ?? "",
                        Duration = TimeSpan.Zero
                    };
                }
                var gameEnd = _context.HllLogs.OrderBy(x => x.Id).LastOrDefault(x => x.HllgameserverId == id && (x.LogType == 11 || x.LogType == 12) && x.LogTime > currentGame.StartTime);
                if (gameEnd != null)
                {
                    currentGame.Duration = gameEnd.LogTime.Subtract(currentGame.StartTime);
                }
                _context.Update(currentGame);
                _context.SaveChanges();
            }
        }

        public void GetSessions(long gameServerId, DateTime? startTime = null)
        {
            startTime ??= DateTime.UtcNow.Subtract(TimeSpan.FromSeconds(int.Parse(ConfigHelper.GetConfig("DataImport", "LogImportInterval") ?? "60") + 10));
            lock (_context)
            {
                List<HllLog> sessionStartEnd = _context.HllLogs.Where(x => x.HllgameserverId == gameServerId && x.LogTime >= startTime && (x.LogType == 1 || x.LogType == 2)).OrderBy(x => x.LogTime).ToList();
                HllPlayerSession currentSession = new();
                foreach (HllLog session in sessionStartEnd)
                {
                    switch (session.LogType)
                    {
                        case 1:
                            currentSession = new HllPlayerSession { SteamId = session.ParticipantId1, StartTime = session.LogTime, ServerId = gameServerId };
                            break;
                        case 2:
                            currentSession = _context.HllPlayerSessions.OrderBy(x => x.StartTime).LastOrDefault(x => x.SteamId == session.ParticipantId1 && x.ServerId == gameServerId)
                                ?? new HllPlayerSession { SteamId = session.ParticipantId1, StartTime = session.LogTime, ServerId = gameServerId };
                            currentSession.EndTime = session.LogTime;
                            break;
                    }
                    if (_context.Find(typeof(HllPlayerSession), currentSession.SteamId, currentSession.StartTime) is HllPlayerSession dbSession)
                    {
                        Parser.ModifyPlayerSession(ref dbSession, currentSession);
                    }
                    else
                    {
                        _context.Add(currentSession);
                    }
                }
                _context.SaveChanges();
            }
        }

        public void GetPlayers(long id)
        {
            lock (_context)
            {
                string? players = RunCommand(id, "Get PlayerIds");
                if (players == null) return;
                var currentGame = _context.HllGames.OrderBy(x => x.StartTime).LastOrDefault(x => x.HllgameserverId == id);

                List<HllPlayer> playerObjs = Parser.ParsePlayers(players);
                List<HllPlayer> gamePlayers = new();

                foreach (HllPlayer player in playerObjs)
                {
                    HllPlayer playerCopy = player;
                    HllGamePlayer gamePlayer = new();

                    string? playerInfo = RunCommand(id, $"PlayerInfo {player.Name}");
                    if (playerInfo == null) continue;
                    Parser.ParsePlayerDetails(ref playerCopy, ref gamePlayer, playerInfo);
                    gamePlayer.GameId = currentGame?.GameId ?? -1;

                    if (_context.Find(typeof(HllGamePlayer), gamePlayer.SteamId, gamePlayer.GameId) is not HllGamePlayer dbGamePlayer)
                    {
                        _context.Add(gamePlayer);
                    }
                    else
                    {
                        Parser.ModifyGamePlayer(ref dbGamePlayer, gamePlayer);
                    }

                    if (_context.Find(typeof(HllPlayer), player.SteamId) is not HllPlayer dbPlayer)
                    {
                        _context.Add(playerCopy);
                    }
                    else
                    {
                        Parser.ModifyPlayer(ref dbPlayer, playerCopy);
                    }
                }
                _context.SaveChanges();
            }
        }

        public void GetLogs(long id, int? seconds = null)
        {
            lock (_context)
            {
                List<HllLog> logObjs = new();
                TimeSpan logDelay = TimeSpan.FromSeconds(seconds ?? int.Parse(ConfigHelper.GetConfig("DataImport", "LogImportInterval") ?? "60"));

                string? logs = RunCommand(id, $"ShowLog {Math.Ceiling(Math.Ceiling(logDelay.TotalMinutes))}");
                if (logs == null) return;
                logObjs.AddRange(Parser.ParseLogs(logs, id));

                HllLog? lastLogLine = _context.HllLogs.OrderBy(x => x.Id).LastOrDefault(x => x.HllgameserverId == id);
                if (lastLogLine != null)
                {
                    logObjs.RemoveAll(x => x.LogTime <= lastLogLine.LogTime);
                }
                _context.AddRange(logObjs);
                _context.SaveChanges();
            }
        }
        #endregion Interface Implementations
    }
}
