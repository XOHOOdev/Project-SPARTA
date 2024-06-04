using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Helium.BlazorUI.Entities
{
    [PrimaryKey(nameof(SteamId), nameof(GameId))]
    public class HLLGamePlayer
    {
        /**
         * Name: Λ • XOHOO
         * steamID64: 76561198330434784
         * Team: Axis
         * Role: Rifleman
         * Loadout: Standard Issue
         * Kills: 0 - Deaths: 0
         * Score: C 0, O 0, D 0, S 0
         * Level: 145
         */

        [Required]
        [ForeignKey(nameof(HLLPlayer))]
        public long SteamId { get; set; }

        [Required]
        [ForeignKey(nameof(HLLGame))]
        public long GameId { get; set; }

        public string Team { get; set; } = null!;

        public string Role { get; set; } = null!;

        public string Unit { get; set; } = null!;

        public string Loadout { get; set; } = null!;

        public int CombatScore { get; set; }

        public int OffensiveScore { get; set; }

        public int DefensiveScore { get; set; }

        public int SupportScore { get; set; }
    }
}
