using StiQr_SIMTEL.Shared;
using StiQr_SIMTEL.Shared.LabelQR;
using StiQr_SIMTEL.Shared.Transactions;

namespace StiQr_SIMTEL.Server.Services
{
    public interface ITransactionService
    {
        Task<ResponseAPI<string>> RegisterTransaction(RegisterTransactionDTO transactionDTO);
    }
}
