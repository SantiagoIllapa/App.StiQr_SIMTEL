using Newtonsoft.Json;
using StiQr_SIMTEL.Client.Models;
using StiQr_SIMTEL.Shared;
using StiQr_SIMTEL.Shared.LabelsQR;
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

        //Users
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
            bool isSuccess = false;
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
        //LabelsQr

        public async Task<(List<GetLabelQrDTO> LabelsList, string ErrorMessage)> GetLabelsQr()
        {
            var labelsList = new List<GetLabelQrDTO>();
            string errorMessage = string.Empty;
            try
            {
                using (var client = new HttpClient())
                {
                    var url = $"{_baseUrl}{APIs.GetLabelsQr}";
                    var apiResponse = await client.GetAsync(url);
                    if (apiResponse.IsSuccessStatusCode)
                    {

                        var response = await apiResponse.Content.ReadAsStringAsync();
                        var deserializeResponse = JsonConvert.DeserializeObject<ResponseAPI<List<GetLabelQrDTO>>>(response);
                        if (deserializeResponse.IsSuccess)
                        {
                            labelsList = deserializeResponse.Content;
                        }
                        else
                        {
                            errorMessage = deserializeResponse.ErrorMessage;
                        }
                    }
                    else
                    {
                        errorMessage = "No se ha podido conectar con el servidor";
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            return (labelsList, errorMessage);

        }
        public async Task <(GetLabelQrDTO LabelQr,string ErrorMessage)> GetLabelQrById(int id)
        {
            var labelQr = new GetLabelQrDTO();
            string errorMessage = string.Empty;
            try
            {
                using (var client = new HttpClient())
                {
                    var url = $"{_baseUrl}{APIs.GetLabelsQrById}/{id}";
                    var apiResponse = await client.GetAsync(url);
                    if (apiResponse.IsSuccessStatusCode)
                    {

                        var response = await apiResponse.Content.ReadAsStringAsync();
                        var deserializeResponse = JsonConvert.DeserializeObject<ResponseAPI<GetLabelQrDTO>>(response);
                        if (deserializeResponse.IsSuccess)
                        {
                            labelQr = deserializeResponse.Content;
                        }
                        else
                        {
                            errorMessage = deserializeResponse.ErrorMessage;
                        }
                    }
                    else
                    {
                        errorMessage = "No se ha podido conectar con el servidor";
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            return (labelQr, errorMessage);
        }
    }
}
