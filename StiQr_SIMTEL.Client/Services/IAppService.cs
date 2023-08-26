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
using StiQr_SIMTEL.Shared.Transactions;

namespace StiQr_SIMTEL.Client.Services
{
    internal interface IAppService
    {
        public Task<string> AuthenticateUser(LogInUserDTO loginModel);
        public Task<(bool IsSuccess, string ErrorMessage)> RegisterUser(RegisterUserDTO registrationModel);
        public Task<(string ConfirmMessage, string ErrorMessage)> CreateLabelQr(CreateLabelQrDTO labelModel);
        public Task<(string ConfirmMessage, string ErrorMessage)> UpdateLabelQr(CreateLabelQrDTO labelModel, int id);
        public Task<(string ConfirmMessage, string ErrorMessage)> DeleteLabelQr(int id);
        public Task<(List<GetLabelQrDTO> LabelsList, string ErrorMessage)> GetLabelsQr();
        public Task<(GetLabelQrDTO LabelQr, string ErrorMessage)> GetLabelQrById(int id);
        public Task<(GetLabelQrDTO LabelQr, string ErrorMessage)> GetLabelQrByPlate(string plate);
        public Task<(GetLabelQrDTO LabelQr, string ErrorMessage)> GetLabelQrByUrl(string url);
        public Task<(string ConfirmMessage, string ErrorMessage)> RechargeLabelQr(RechargeCashDTO rechargeCashDTO);
        public Task<(string ConfirmMessage, string ErrorMessage)> CheckHourLabelQr(CheckHourDTO checkHourLabelQr);
        public Task<(List<GetTransactionDTO> TransactionList, string ErrorMessage)> GetTransactions();
    }
}
