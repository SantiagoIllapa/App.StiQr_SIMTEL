using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StiQr_SIMTEL.Shared.Agents
{
    public class CreateAgentDTO
    {
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string FullName { get; set; } = null!;
        [Required]
        public string Description { get; set; } = null!;

    }
}
