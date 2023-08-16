using StiQr_SIMTEL.Shared;

namespace StiQR_SIMTEL.Services
{
    internal interface IUserService
    {

        public Task<List<UserDTO>> GetAllUsers();


    }
}
