using StiQr_SIMTEL.Shared;
using StiQr_SIMTEL.Shared.Agents;

namespace StiQr_SIMTEL.Server.Services
{
    public interface IAgentService
    {
        Task<ResponseAPI<string>> AddAgent(CreateAgentDTO agentDTO);
    }
}
