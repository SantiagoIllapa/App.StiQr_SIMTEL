﻿using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace StiQr_SIMTEL.Server.Data
{
    public class Agent
    {
        [Key]
        public int AgentID { get; set; }
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string FullName { get; set; } = null!;
        [MaxLength(100)]
        public string Description { get; set; } = null!;
    }
}
