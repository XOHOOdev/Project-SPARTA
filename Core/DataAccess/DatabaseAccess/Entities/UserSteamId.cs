﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sparta.Core.DataAccess.DatabaseAccess.Entities;

public class UserSteamId
{
    [Required]
    [Key]
    [ForeignKey("AspNetUsers")]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public string UserId { get; set; } = null!;

    [Required] public long SteamId { get; set; }
}