using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StiQr_SIMTEL.Server.Services;
using StiQr_SIMTEL.Shared;
using StiQr_SIMTEL.Shared.LabelQR;
using StiQr_SIMTEL.Shared.Transactions;

namespace StiQr_SIMTEL.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }


        [HttpPost("RegisterTransaction")]
        public async Task<IActionResult> RegisterTransaction([FromBody] RegisterTransactionDTO transactionDetail)
        {

            var response = await _transactionService.RegisterTransaction(transactionDetail);
            return HandleAPIResponse(response);

        }
        
        [HttpGet("GetTransactions")]
        public async Task<IActionResult> GetTransactions()
        {

            var response = await _transactionService.GetTransactionAll();
            return HandleAPIResponse(response);

        }
        private IActionResult HandleAPIResponse<T>(ResponseAPI<T> response)
        {
            try
            {
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
