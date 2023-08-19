using StiQr_SIMTEL.Server.Context;
using StiQr_SIMTEL.Shared;

namespace StiQr_SIMTEL.Server.Services
{
    public class AgentService : IAgentService
    {
        private readonly StiQrDbContext _dbContext;
        public AgentService(StiQrDbContext context)
        {
            _dbContext = context;
        }
        public async Task<ResponseAPI<string>> AddAgent(AgentDTO agentDTO)
        {
            var response = new ResponseAPI<string>();
            try
            {
               
                await _dbContext.AddAsync(new AgentDTO
                {
                    AgentName = agentDTO.AgentName,
                    AgentDescription = agentDTO.AgentDescription,
                });
                await _dbContext.SaveChangesAsync();
                response.IsSuccess = true;
                response.Content = "Agente añadido";
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
