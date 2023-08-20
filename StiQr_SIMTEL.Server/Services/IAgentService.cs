using StiQr_SIMTEL.Shared;

namespace StiQr_SIMTEL.Server.Services
{
    public interface IAgentService
    {
        Task<ResponseAPI<string>> AddAgent(AgentDTO agentDTO);
    }
}
