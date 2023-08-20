using StiQr_SIMTEL.Client.Models;
using StiQr_SIMTEL.Shared.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StiQr_SIMTEL.Client.Services
{
    internal interface IAppService
    {
        public Task<string> AuthenticateUser(LogInUserDTO loginModel);
        public Task<(bool IsSuccess, string ErrorMessage)> RegisterUser(RegisterUserDTO registrationModel);
    }
}
