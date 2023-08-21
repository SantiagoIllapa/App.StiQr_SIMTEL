using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StiQr_SIMTEL.Server.Context;
using StiQr_SIMTEL.Server.Data;
using StiQr_SIMTEL.Shared;
using StiQr_SIMTEL.Shared.LabelQR;
using StiQr_SIMTEL.Shared.Users;
using System;
using System.Diagnostics;

namespace StiQr_SIMTEL.Server.Services
{
    public class LabelQrService : ILabelQrService
    {
        private readonly StiQrDbContext _dbContext;
        private readonly UserManager<User> _userManager;
        public LabelQrService(StiQrDbContext context , UserManager<User> userManager)
        {
            _dbContext = context;
            _userManager = userManager;
        }
        public async Task<ResponseAPI<string>> RegisterLabelQR(CreateLabelQrDTO LabelQrDTO)
        {
            var response = new ResponseAPI<string>();

            var userDetails = await _userManager.FindByEmailAsync(LabelQrDTO.UserEmail);
            if (userDetails != null)
            {
               
                var existingLabel = await _dbContext.LabelsQr.FirstOrDefaultAsync(l => l.Plate == LabelQrDTO.Plate);
                if (existingLabel == null)
                {
                    try
                    {
                        await _dbContext.AddAsync(new LabelQr
                        {
                            UserEmail = LabelQrDTO.UserEmail,
                            Plate = LabelQrDTO.Plate,
                            LastMark = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("America/Guayaquil")),
                            Amount = 0

                        });
                        await _dbContext.SaveChangesAsync();
                        response.IsSuccess = true;
                        response.Content = "Registro de EtiquetaQr correcto";
                    }
                    catch (Exception ex)
                    {
                        response.ErrorMessage = ex.Message;
                        response.IsSuccess = false;
                    }
                }
                else
                {
                    response.IsSuccess = false;
                    response.ErrorMessage = "Esta placa ya se encuentra registrada";
                }
            }
            else
            {
                response.IsSuccess = false;
                response.ErrorMessage=("Este usuario no existe");
            }
            
            return response;
        }
    }
}
