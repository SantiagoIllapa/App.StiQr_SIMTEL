using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StiQr_SIMTEL.Server.Services;
using StiQr_SIMTEL.Shared;
using StiQr_SIMTEL.Shared.LabelQR;
using StiQr_SIMTEL.Shared.LabelsQR;

namespace StiQr_SIMTEL.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelsQrController : ControllerBase
    {
        private readonly ILabelQrService _labelQrService;
        public LabelsQrController(ILabelQrService labelQrService)
        {
            _labelQrService = labelQrService;
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
        [HttpPut("CheckHourLabelQr/{id}")]
        public async Task<IActionResult> CheckHourLabelQR([FromBody] CheckHourLabelQrDTO checkHourLabel,int id)
        {
            var response = await _labelQrService.CheckHourLabelQR(checkHourLabel,id);
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
