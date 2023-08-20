using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace StiQr_SIMTEL.Server.Data
{
    public class User : IdentityUser
    {
        [MaxLength(50)]
        public string FirstName { get; set; } = null!;
        [MaxLength(50)]
        public string LastName { get; set; } = null!;
        [MaxLength(50)]
        public string  Gender { get; set; } = null!;
        [MaxLength(10)]
        [MinLength(10)]
        public string? IdCard { get; set; }
    
        public string? Address { get; set; }

    }
}
