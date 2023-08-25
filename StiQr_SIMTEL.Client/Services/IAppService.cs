using StiQr_SIMTEL.Shared.Users;
using StiQr_SIMTEL.Shared.LabelQR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StiQr_SIMTEL.Client.Data;
using StiQr_SIMTEL.Shared;
using StiQr_SIMTEL.Shared.LabelsQR;

namespace StiQr_SIMTEL.Client.Services
{
    internal interface IAppService
    {
        public Task<string> AuthenticateUser(LogInUserDTO loginModel);
        public Task<(bool IsSuccess, string ErrorMessage)> RegisterUser(RegisterUserDTO registrationModel);
        public Task<(List<GetLabelQrDTO> LabelsList, string ErrorMessage)> GetLabelsQr();
        public Task<(GetLabelQrDTO LabelQr, string ErrorMessage)> GetLabelQrById(int id);
        public Task<(string ConfirmMessage, string ErrorMessage)> CheckHourLabelQr(CheckHourDTO checkHourLabelQr);
    }
}
