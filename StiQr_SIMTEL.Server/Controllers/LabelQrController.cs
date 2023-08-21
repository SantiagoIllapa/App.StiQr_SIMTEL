using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StiQr_SIMTEL.Server.Services;
using StiQr_SIMTEL.Shared;
using StiQr_SIMTEL.Shared.LabelQR;

namespace StiQr_SIMTEL.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelQrController : ControllerBase
    {
        private readonly ILabelQrService _labelQrService;
        public LabelQrController(ILabelQrService labelQrService)
        {
            _labelQrService = labelQrService;
        }


        [HttpPost("RegisterLabelQr")]
        public async Task<IActionResult> RegisterLabelQr([FromBody] CreateLabelQrDTO labelInf)
        {
            try
            {
                var response = await _labelQrService.RegisterLabelQR(labelInf);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
