using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StiQr_SIMTEL.Server.Context;
using StiQr_SIMTEL.Server.Data;
using StiQr_SIMTEL.Shared;
using StiQr_SIMTEL.Shared.LabelsQR;
using StiQr_SIMTEL.Shared.Transactions;
using StiQr_SIMTEL.Shared.Users;

namespace StiQr_SIMTEL.Server.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly StiQrDbContext _dbContext;
        private readonly UserManager<User> _userManager;
        public TransactionService(StiQrDbContext context, UserManager<User> userManager)
        {
            _dbContext = context;
            _userManager = userManager;
        }
        public async Task<ResponseAPI<string>> RegisterTransaction(RegisterTransactionDTO transactionDTO)
        {
            var response = new ResponseAPI<string>();

            var existingUser = await _userManager.FindByIdAsync(transactionDTO.IdUserTransmiter);
            if (existingUser != null)
            {

                var existingLabel = await _dbContext.LabelsQr.FirstOrDefaultAsync(l => l.Id == transactionDTO.IdLabelQr);
                if (existingLabel != null)
                {
                    try
                    {
                        await _dbContext.AddAsync(new Transaction
                        {
                            IdUserTransmiter = transactionDTO.IdUserTransmiter,
                            IdLabelQr = transactionDTO.IdLabelQr,
                            Type = transactionDTO.Type,
                            DateTransacction = transactionDTO.DateTransacction,
                            Amount=transactionDTO.Amount,
                            Observations = transactionDTO.Observations,
                        }); ;
                        await _dbContext.SaveChangesAsync();
                        response.IsSuccess = true;
                        response.Content = "Transaccion creada correctamente";
                    }
                    catch (Exception ex)
                    {
                        response.ErrorMessage = ex.Message;
                        response.IsSuccess = false;
                    }
                }
                else
                {
                    response.IsSuccess = false;
                    response.ErrorMessage = "Esta etiqueteQR no existe";
                }
            }
            else
            {
                response.IsSuccess = false;
                response.ErrorMessage = ("Este usuario no existe");
            }

            return response;
        }
        public async Task<ResponseAPI<List<GetTransactionDTO>>> GetTransactionAll()
        {
            var response = new ResponseAPI<List<GetTransactionDTO>>();
            var TransactionList = new List<GetTransactionDTO>();
            try
            {
                var dbTransactions = await _dbContext.Transactions.ToListAsync();

                foreach (Transaction transaction in dbTransactions)
                {
                    TransactionList.Add(new GetTransactionDTO
                    {
                        Id = transaction.Id,
                        IdUserTransmiter=transaction.IdUserTransmiter,
                        DateTransacction=transaction.DateTransacction,
                        IdLabelQr=transaction.IdLabelQr,
                        Type = transaction.Type,
                        Observations = transaction.Observations,
                    });
                }
                response.IsSuccess = true;
                response.Content = TransactionList;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
                response.IsSuccess = false;
            }
            return response;
        }
    }
}
