using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StiQr_SIMTEL.Server.Services;
using StiQr_SIMTEL.Shared;
using StiQr_SIMTEL.Shared.LabelQR;
using StiQr_SIMTEL.Shared.LabelsQR;
using StiQr_SIMTEL.Shared.Transactions;

namespace StiQr_SIMTEL.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelsQrController : ControllerBase
    {
        private readonly ILabelQrService _labelQrService;
        private readonly ITransactionService _transactionService;

        public LabelsQrController(ILabelQrService labelQrService, ITransactionService transactionService)
        {
            _labelQrService = labelQrService;
            _transactionService = transactionService;

        }


        [HttpPost("RegisterLabelQr")]
        public async Task<IActionResult> RegisterLabelQr([FromBody] CreateLabelQrDTO labelInf)
        {
            var response = await _labelQrService.RegisterLabelQR(labelInf);
            return HandleAPIResponse(response);
        }
        [HttpGet("GetLabelsQr")]
        public async Task<IActionResult> GetLabelQrAll()
        {
            var response = await _labelQrService.GetLabelQrAll();
            return HandleAPIResponse(response);
        }
 
        [HttpGet("GetLabelsQrById/{id}")]
        public async Task<IActionResult> GetLabelQrByID(int id)
        {
            var response = await _labelQrService.GetLabelQrById(id);
            return HandleAPIResponse(response);
        }
        [HttpGet("GetLabelsQrByPlate/{plate}")]
        public async Task<IActionResult> GetLabelQrByPlate(string plate)
        {
            var response = await _labelQrService.GetLabelQrByPlate(plate);
            return HandleAPIResponse(response);
        }
        [HttpPut("UpdateLabelQr/{id}")]
        public async Task<IActionResult> UpdateLabelQr([FromBody] CreateLabelQrDTO label, int id)
        {
            var response = await _labelQrService.UpdateLabelQr(label,id);
            return HandleAPIResponse(response);
        }
        [HttpDelete("DeleteLabelQr/{id}")]
        public async Task<IActionResult> DeleteLabelQr( int id)
        {
            var response = await _labelQrService.DeleteLabelQr(id);
            return HandleAPIResponse(response);
        }
        [HttpPut("CheckHourLabelQr")]
        public async Task<IActionResult> CheckHourLabelQr([FromBody] CheckHourDTO checkHour)
        {
            var response = await _labelQrService.CheckHourLabelQR(checkHour);
            if (response.IsSuccess)
            {
                var transactionResponse = await _transactionService.RegisterTransaction(new RegisterTransactionDTO
                {
                    IdUserTransmiter = checkHour.IdUserRecharger,
                    IdLabelQr = checkHour.IdLabelQr,
                    Amount = 0.25m,
                    Type = 1,
                    DateTransacction = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("America/Guayaquil")),
                });
                if (!transactionResponse.IsSuccess)
                {
                    return HandleAPIResponse(transactionResponse);
                }

            }
            return HandleAPIResponse(response);
        }
        [HttpPut("RechargeCash")]
        public async Task<IActionResult> RechargeCash([FromBody] RechargeCashDTO rechargeDTO)
        {
            var response = await _labelQrService.RechargeCash(rechargeDTO);
            if(response.IsSuccess)
            {
                var transactionResponse = await _transactionService.RegisterTransaction(new RegisterTransactionDTO
                {
                    IdUserTransmiter= rechargeDTO.IdUserRecharger,
                    IdLabelQr= rechargeDTO.IdLabelQr,
                    Amount=rechargeDTO.CashAmount,
                    Type=0,
                    DateTransacction= TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("America/Guayaquil")),
                });
                if (!transactionResponse.IsSuccess)
                {
                    return HandleAPIResponse(transactionResponse);
                }

            }
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
