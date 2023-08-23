using Microsoft.AspNetCore.Identity;
using StiQr_SIMTEL.Server.Context;
using StiQr_SIMTEL.Server.Data;
using StiQr_SIMTEL.Shared;
using StiQr_SIMTEL.Shared.Agents;

namespace StiQr_SIMTEL.Server.Services
{
    public class AgentService : IAgentService
    {
        private readonly StiQrDbContext _dbContext;
        private readonly UserManager<User> _userManager;
        public AgentService(StiQrDbContext context, UserManager<User> userManager)
        {
            _dbContext = context;
            _userManager = userManager;
        }
        public async Task<ResponseAPI<string>> AddAgent(CreateAgentDTO agentDTO)
        {
            var response = new ResponseAPI<string>();
            var existingUser = await _userManager.FindByEmailAsync(agentDTO.Email);
            if (existingUser != null)
            {
                try
                {
                    await _dbContext.AddAsync(new Agent
                    {
                        Email = agentDTO.Email,
                        FullName = agentDTO.FullName,
                        Description = agentDTO.Description,
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
            }else
            {
                response.IsSuccess = false;
                response.ErrorMessage = "No existe este usuario para ser asignado como agente";
            }
            return response;
        }
    }
}
