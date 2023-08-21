using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StiQr_SIMTEL.Server.Services;
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
            try
            {
                var response = await _transactionService.RegisterTransaction(transactionDetail);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
