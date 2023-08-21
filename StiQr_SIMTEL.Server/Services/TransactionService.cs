using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StiQr_SIMTEL.Server.Context;
using StiQr_SIMTEL.Server.Data;
using StiQr_SIMTEL.Shared;
using StiQr_SIMTEL.Shared.Transactions;
using StiQr_SIMTEL.Shared.Users;

namespace StiQr_SIMTEL.Server.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly StiQrDbContext _dbContext;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public TransactionService(StiQrDbContext context, RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _dbContext = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task<ResponseAPI<string>> RegisterTransaction(RegisterTransactionDTO transactionDTO)
        {
            var response = new ResponseAPI<string>();

            var existingUser = await _dbContext.Agents.FirstOrDefaultAsync(l => l.AgentID == transactionDTO.IdAgent);
            if (existingUser != null)
            {

                var existingLabel = await _dbContext.LabelsQr.FirstOrDefaultAsync(l => l.Id == transactionDTO.IdLabelQr);
                if (existingLabel != null)
                {
                    try
                    {
                        await _dbContext.AddAsync(new Transaction
                        {
                            IdAgent = transactionDTO.IdAgent,
                            IdLabelQr = transactionDTO.IdLabelQr,
                            DateMark = transactionDTO.DateMark,
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
                response.ErrorMessage = ("Este agente no existe");
            }

            return response;
        }
    }
}
