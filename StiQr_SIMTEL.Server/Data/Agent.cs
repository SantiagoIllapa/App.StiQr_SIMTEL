using System.ComponentModel.DataAnnotations;

namespace StiQr_SIMTEL.Server.Data
{
    public class Agent
    {
        [Key]
        public int AgentID { get; set; }
        [MaxLength(50)]
        public string AgentName { get; set; } = null!;
        [MaxLength(100)]
        public string AgentDescription { get; set; } = null!;
    }
}
