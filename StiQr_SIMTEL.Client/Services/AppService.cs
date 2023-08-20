using Newtonsoft.Json;
using StiQr_SIMTEL.Client.Models;
using StiQr_SIMTEL.Shared.Users;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StiQr_SIMTEL.Client.Services
{
    internal class AppService : IAppService
    {
        public string _baseUrl = "https://localhost:7039";
        public async Task<string> AuthenticateUser(LogInUserDTO loginModel)
        {
            string returnStr = string.Empty;
            using (var client = new HttpClient())
            {
                var url = $"{_baseUrl}{APIs.AuthenticateUser}";
                var serializeStr = JsonConvert.SerializeObject(loginModel);
                var response = await client.PostAsync(url, new StringContent(serializeStr, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    returnStr = await response.Content.ReadAsStringAsync();
                }
            }
            return returnStr;
        }

        public async Task<(bool IsSuccess, string ErrorMessage)> RegisterUser(RegisterUserDTO registrationModel)
        {
            string errorMessage = string.Empty;
            bool isSuccess=false;
            using (var client = new HttpClient())
            {
                var url = $"{_baseUrl}{APIs.RegisterUser}";
                var serializeStr = JsonConvert.SerializeObject(registrationModel);
                var response = await client.PostAsync(url, new StringContent(serializeStr, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    isSuccess = true;
                }
                else
                {
                    errorMessage = await response.Content.ReadAsStringAsync();
                }
            }
            return (isSuccess, errorMessage);
        }
    }
}
