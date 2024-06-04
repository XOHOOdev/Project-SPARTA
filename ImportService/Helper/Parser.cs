using Helium.Core.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text.RegularExpressions;

namespace Helium.ImportService.Helper
{
    internal static partial class Parser
    {
        public static void ParseGameState(ref HllGameState gameStateObj, string gamestate, string slots)
        {
            /**
              * Players: Allied: 1 - Axis: 1
              * Score: Allied: 2 - Axis: 2
              * Remaining Time: 1:13:30
              * Map: omahabeach_warfare
              * Next Map: carentan_warfare
              */
            string[] lines = gamestate.Split('\n');
            string[] players = lines[0].Split(' ');
            string[] scores = lines[1].Split(' ');
            string[] slotStrings = slots.Split('/');

            gameStateObj.AlliedPlayers = int.Parse(players[2]);
            gameStateObj.AxisPlayers = int.Parse(players[5]);
            gameStateObj.AlliedScore = int.Parse(scores[2]);
            gameStateObj.AxisScore = int.Parse(scores[5]);
            gameStateObj.RemainingTime = TimeSpan.Parse(lines[2].Remove(0, 16));
            gameStateObj.Map = lines[3].Remove(0, 5);
            gameStateObj.NextMap = lines[4].Remove(0, 10);
            gameStateObj.SlotsCurrent = int.Parse(slotStrings[0]);
            gameStateObj.SlotsTotal = int.Parse(slotStrings[1]);
        }

        public static List<HllPlayer> ParsePlayers(string players)
        {
            /**
             * {PlayerCount}[TAB(\t)]{PlayerName : SteamId}[TAB(\t)]{PlayerName : SteamId}...
             */
            List<HllPlayer> playerList = new();

            string[] playerStrings = players.Split("\t", StringSplitOptions.RemoveEmptyEntries);
            playerStrings = playerStrings.Skip(1).ToArray();

            foreach (string playerString in playerStrings)
            {
                string[] playerValues = playerString.Split(" : ");
                playerList.Add(new HllPlayer { Name = playerValues[0], SteamId = long.Parse(playerValues[1]), FirstLogon = DateTime.UtcNow });
            }

            return playerList;
        }

        public static void ParsePlayerDetails(ref HllPlayer player, ref HllGamePlayer gamePlayer, string playerInfo)
        {
            gamePlayer.CombatScore = int.Parse(CombatScore().Match(playerInfo).Value);
            gamePlayer.DefensiveScore = int.Parse(DefensiveScore().Match(playerInfo).Value);
            gamePlayer.Loadout = Loadout().Match(playerInfo).Value;
            gamePlayer.OffensiveScore = int.Parse(OffensiveScore().Match(playerInfo).Value);
            gamePlayer.Role = Role().Match(playerInfo).Value;
            gamePlayer.Unit = Unit().Match(playerInfo).Value;
            gamePlayer.SteamId = player.SteamId;
            gamePlayer.SupportScore = int.Parse(SupportScore().Match(playerInfo).Value);
            gamePlayer.Team = Team().Match(playerInfo).Value;

            player.Level = int.Parse(Level().Match(playerInfo).Value);
        }

        public static void ModifyPlayerSession(ref HllPlayerSession session, HllPlayerSession donorSession)
        {
            session.SteamId = donorSession.SteamId;
            session.ServerId = donorSession.ServerId;
            session.StartTime = donorSession.StartTime;
            session.EndTime = donorSession.EndTime;
        }

        public static void ModifyGamePlayer(ref HllGamePlayer player, HllGamePlayer donorGamePlayer)
        {
            player.CombatScore = donorGamePlayer.CombatScore;
            player.DefensiveScore = donorGamePlayer.DefensiveScore;
            player.Loadout = donorGamePlayer.Loadout;
            player.OffensiveScore = donorGamePlayer.OffensiveScore;
            player.Role = donorGamePlayer.Role;
            player.Unit = donorGamePlayer.Unit;
            player.SteamId = donorGamePlayer.SteamId;
            player.SupportScore = donorGamePlayer.SupportScore;
            player.Team = donorGamePlayer.Team;
        }

        public static void ModifyPlayer(ref HllPlayer player, HllPlayer donorPlayer)
        {
            player.SteamId = donorPlayer.SteamId;
            player.Level = donorPlayer.Level;
            player.Name = donorPlayer.Name;
            player.FirstLogon = donorPlayer.FirstLogon;
        }

        public static List<HllLog> ParseLogs(string logs, long serverId)
        {
            string pattern = @"(?=\[.+? \(\d+\)\])";

            string[] logLines = Regex.Split(logs, pattern);

            List<HllLog> logObjs = new();
            foreach (string logLine in logLines)
            {
                if (logLine.IsNullOrEmpty()) continue;
                if (logLine.Equals("EMPTY")) continue;
                logObjs.Add(ParseLog(logLine, serverId));
            }
            return logObjs;
        }

        private static HllLog ParseLog(string logLine, long serverId)
        {
            /**
             * UNKNOWN,
             * CONNECTED,
             * DISCONNECTED,
             * KILL,
             * CHAT,
             * MESSAGE,
             * KICK,
             * BAN,
             * TEAMSWITCH,
             * MATCH_ENDED,
             * MATCH_STARTED
             */
            HllLog logObj = new()
            {
                LogTime = UnixTimeStampToDateTime(double.Parse(logLine.Substring(logLine.IndexOf('(') + 1, 10))),
                HllgameserverId = serverId
            };
            string log = logLine.Remove(0, logLine.IndexOf(']') + 2).Trim();

            try
            {
                string firstWord = log[..log.IndexOfAny(new[] { ' ', '[', ':' })];
                MatchCollection participants;

                switch (firstWord.ToUpper())
                {
                    case "CONNECTED":
                        logObj.LogType = 1;
                        logObj.ParticipantId1 = long.Parse(FirstParticipantId().Match(log).Value);
                        break;
                    case "DISCONNECTED":
                        logObj.LogType = 2;
                        logObj.ParticipantId1 = long.Parse(FirstParticipantId().Match(log).Value);
                        break;
                    case "KILL":
                        logObj.LogType = 3;
                        participants = Participants().Matches(log);
                        string[] killPart1 = participants.First().Value.Split('/');
                        string[] killPart2 = participants.Last().Value.Split('/');
                        logObj.ParticipantId1 = long.Parse(killPart1[1]);
                        logObj.ParticipantId2 = long.Parse(killPart2[1]);
                        logObj.Arguments = string.Join(";", new[] { killPart1[0], killPart2[0], log[(log.LastIndexOf("with") + 5)..].Trim() });
                        break;
                    case "TEAM":
                        logObj.LogType = 4;
                        participants = Participants().Matches(log);
                        string[] tkpart1 = participants.First().Value.Split('/');
                        string[] tkpart2 = participants.Last().Value.Split('/');
                        logObj.ParticipantId1 = long.Parse(tkpart1[1]);
                        logObj.ParticipantId2 = long.Parse(tkpart2[1]);
                        logObj.Arguments = string.Join(";", new[] { tkpart1[0], tkpart2[0], log[(log.LastIndexOf("with") + 5)..].Trim() });
                        break;
                    case "CHAT":
                        logObj.LogType = 5;
                        participants = Participants().Matches(log);
                        string[] chatParts = participants.First().Value.Split('/');
                        logObj.ParticipantId1 = long.Parse(chatParts[1]);
                        logObj.Arguments = string.Join(';', new[] { Regex.Match(log, @"(?<=\[)\w*(?=\])").Value, chatParts[0], Regex.Match(log, @"(?<=\[.+?\(\w+?\/\d+?\)\]: ).+").Value });
                        break;
                    case "PLAYER":
                        logObj.LogType = 9;
                        logObj.ParticipantId1 = long.Parse(FirstParticipantId().Match(log).Value);
                        logObj.Arguments = Regex.Match(log, @"(?<=\[.+?\(\d*\)\] )\w+").Value;
                        break;
                    case "KICK":
                        logObj.LogType = 7;
                        participants = Regex.Matches(log, @"(?<=\[).+?(?=(\]|\n|$))");
                        logObj.Arguments = participants.First().Value + ";" + participants.Last().Value;
                        break;
                    case "BAN":
                        logObj.LogType = 8;
                        participants = Regex.Matches(log, @"(?<=\[).+?(?=(\]|\n|$))");
                        logObj.Arguments = participants.First().Value + ";" + participants.Last().Value;
                        break;
                    case "MESSAGE":
                        logObj.LogType = 6;
                        logObj.ParticipantId1 = long.Parse(FirstParticipantId().Match(log).Value);
                        logObj.Arguments = Regex.Match(log, @"(?<=content \[).+").Value;
                        break;
                    case "TEAMSWITCH":
                        logObj.LogType = 10;
                        string part = Regex.Match(log, @"(?<=TEAMSWITCH ).*(?= \()").Value;
                        string[] teams = Regex.Match(log, @"(?<=\()\w+? > \w+?(?=\))").Value.Split(" > ");
                        logObj.Arguments = string.Join(";", new[] { part, teams[0], teams[1] });
                        break;
                    case "MATCH":
                        switch (Regex.Match(log, @"(?<=MATCH )\w+").Value)
                        {
                            case "START":
                                logObj.LogType = 12;
                                logObj.Arguments = Regex.Match(log, @"(?<=MATCH START ).+").Value;
                                break;
                            case "ENDED":
                                logObj.LogType = 11;
                                logObj.Arguments = Regex.Match(log, @"(?<=MATCH ENDED `).+(?=`)").Value;
                                break;
                        }
                        break;
                }
                return logObj;
            }
            catch (Exception)
            {
                logObj.LogType = 0;
                logObj.Arguments = log;
                return logObj;
            }
        }

        private static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            DateTime dateTime = new(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }

        [GeneratedRegex("(?<=Score:(.+?)C )\\d+(?=,)")]
        private static partial Regex CombatScore();
        [GeneratedRegex("(?<=Score:(.+?)D )\\d+(?=,)")]
        private static partial Regex DefensiveScore();
        [GeneratedRegex("(?<=Loadout: ).+")]
        private static partial Regex Loadout();
        [GeneratedRegex("(?<=Score:(.+?)O )\\d+(?=,)")]
        private static partial Regex OffensiveScore();
        [GeneratedRegex("(?<=Role: ).+")]
        private static partial Regex Role();
        [GeneratedRegex("(?<=Unit: ).+")]
        private static partial Regex Unit();
        [GeneratedRegex("(?<=Score:(.+?)S )\\d+(?=(,|\\n))")]
        private static partial Regex SupportScore();
        [GeneratedRegex("(?<=Team: ).+")]
        private static partial Regex Team();
        [GeneratedRegex("(?<=Level: ).+")]
        private static partial Regex Level();
        [GeneratedRegex("(?<=\\()\\d+(?=\\))")]
        private static partial Regex FirstParticipantId();
        [GeneratedRegex("(?<=\\()\\w+\\/\\d*(?=\\))")]
        private static partial Regex Participants();
    }
}
