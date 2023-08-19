using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StiQr_SIMTEL.Server.Services;
using StiQr_SIMTEL.Shared;

namespace StiQr_SIMTEL.Server.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private readonly IAgentService _agentService;

        public AgentsController(IAgentService agentService)
        {
            _agentService = agentService;
        }


        [HttpPost("AddAgent")]
        public async Task<IActionResult> AddAgent([FromBody] AgentDTO agentInf)
        {
            try
            {
                var response = await _agentService.AddAgent(agentInf);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}
