﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sparta.Core.DataAccess.DatabaseAccess.Entities
{
    public class DiscordReceivedMessage
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public ulong Id { get; set; }

        public ulong Reference { get; set; }

        public DiscordMessageType MessageType { get; set; }

        public ulong UserId { get; set; }

        public string Content { get; set; } = null!;
    }
}
