using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StiQr_SIMTEL.Shared.Users
{
    public class RegisterUserDTO
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}
