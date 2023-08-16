using Newtonsoft.Json;
using StiQr_SIMTEL.Shared;
using System.Diagnostics;

namespace StiQR_SIMTEL.Services
{

    public class UserService : IUserService
    {
        private string _baseURL = "https://localhost:7039";
        public async Task<List<UserDTO>> GetAllUsers()
        {
            var returnResponse = new List<UserDTO>();
            try
            {
                using (var client = new HttpClient())
                {
                    string url = $"{_baseURL}/api/Users/GetAllUsers";
                    var apiResponse = await client.GetAsync(url);
                    if (apiResponse.StatusCode == System.Net.HttpStatusCode.OK) { }
                    {
                        var response = await apiResponse.Content.ReadAsStringAsync();
                        var deserializeResponse = JsonConvert.DeserializeObject<ResponseAPI<List<UserDTO>>>(response);
                        if (deserializeResponse.IsCorrect)
                        {
                            returnResponse = deserializeResponse.Value;
                            Console.Write(returnResponse.ToString());
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
    }
}
