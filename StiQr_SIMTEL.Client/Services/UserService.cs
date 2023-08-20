using Newtonsoft.Json;
using StiQr_SIMTEL.Shared;
using System.Diagnostics;
using System.Text;
using Windows.System;

namespace StiQR_SIMTEL.Services
{

    public class UserService : IUserService
    {
        private readonly string _baseURL = "https://localhost:7039";
        public async Task<List<UserDTO>> GetAllUsers()
        {
            var returnResponse = new List<UserDTO>();
            try
            {
                using (var client = new HttpClient())
                {
                    string url = $"{_baseURL}/api/Users/GetAll";
                    var apiResponse = await client.GetAsync(url);
                    if (apiResponse.StatusCode == System.Net.HttpStatusCode.OK) { }
                    {
                        var response = await apiResponse.Content.ReadAsStringAsync();
                        var deserializeResponse = JsonConvert.DeserializeObject<ResponseAPI<List<UserDTO>>>(response);
                        if( deserializeResponse != null )
                        {
                            if (deserializeResponse.IsSuccess)
                            {
                                returnResponse = deserializeResponse.Content;

                            }
                        }
                        
                    }
                }
            }
            catch (Exception ex)
            {

                string mess = ex.Message;
                
                Debug.WriteLine(mess);
            }

            return returnResponse;
        }

        public async Task<ResponseAPI<bool>> CreateUser(UserDTO userRequest)
        {
            var returnResponse = new ResponseAPI<bool>();
            try
            {
                using (var client = new HttpClient())
                {
                    string url = $"{_baseURL}/api/Users/Create";
                    var serializeContent = JsonConvert.SerializeObject(userRequest);
                    var apiResponse = await client.PostAsync(url, new StringContent(serializeContent,Encoding.UTF8,"application/json"));
                    
                    if (apiResponse.StatusCode == System.Net.HttpStatusCode.OK) { }
                    {
                        var response = await apiResponse.Content.ReadAsStringAsync();
                        returnResponse = JsonConvert.DeserializeObject<ResponseAPI<bool>>(response);
                    }
                }
            }
            catch (Exception ex)
            {

                string mess = ex.Message;
                returnResponse.ErrorMessage = ex.Message;
                Debug.WriteLine(mess);
            }

            return returnResponse;
        }
    }
}
