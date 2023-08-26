using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StiQr_SIMTEL.Server.Context;
using StiQr_SIMTEL.Server.Data;
using StiQr_SIMTEL.Shared;
using StiQr_SIMTEL.Shared.LabelQR;
using StiQr_SIMTEL.Shared.LabelsQR;
using StiQr_SIMTEL.Shared.Users;
using System;
using System.Diagnostics;

namespace StiQr_SIMTEL.Server.Services
{
    public class LabelQrService : ILabelQrService
    {
        private readonly StiQrDbContext _dbContext;
        private readonly UserManager<User> _userManager;
        public LabelQrService(StiQrDbContext context, UserManager<User> userManager)
        {
            _dbContext = context;
            _userManager = userManager;
        }
        public async Task<ResponseAPI<string>> RegisterLabelQR(CreateLabelQrDTO LabelQrDTO)
        {
            var response = new ResponseAPI<string>();

            var userDetails = await _userManager.FindByEmailAsync(LabelQrDTO.UserEmail);
            try
            {
                if (userDetails != null)
                {
                    var existingLabel = await _dbContext.LabelsQr.FirstOrDefaultAsync(l => l.Plate == LabelQrDTO.Plate);
                    if (existingLabel == null)
                    {

                        await _dbContext.AddAsync(new LabelQr
                        {
                            UserEmail = LabelQrDTO.UserEmail,
                            Plate = LabelQrDTO.Plate,
                            LastMark = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("America/Guayaquil")).AddDays(-1),
                            Amount = 0
                        });
                        await _dbContext.SaveChangesAsync();
                        response.IsSuccess = true;
                        response.Content = "Registro de EtiquetaQr correcto";

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
                    response.ErrorMessage = ("Este usuario no existe");
                }
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
                response.IsSuccess = false;
            }

            return response;
        }
        public async Task<ResponseAPI<List<GetLabelQrDTO>>> GetLabelQrAll()
        {
            var response = new ResponseAPI<List<GetLabelQrDTO>>();
            var LabelsQrList = new List<GetLabelQrDTO>();
            try
            {
                var dbLabelsQr = await _dbContext.LabelsQr.ToListAsync();

                foreach (LabelQr label in dbLabelsQr)
                {
                    LabelsQrList.Add(new GetLabelQrDTO
                    {
                        Id = label.Id,
                        LastMark = label.LastMark,
                        UserEmail = label.UserEmail,
                        Plate = label.Plate,
                        Amount= label.Amount,
                        ExpiredDateMark = label.LastMark.AddHours(1)
                    });
                }
                response.Content = LabelsQrList;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
                response.IsSuccess = false;
            }
            return response;

        }
        public async Task<ResponseAPI<GetLabelQrDTO>> GetLabelQrById(int LabelId)
        {
            var response = new ResponseAPI<GetLabelQrDTO>();
            try
            {
                var dbLabelQr = await _dbContext.LabelsQr.FirstOrDefaultAsync(l => l.Id == LabelId);
                if (dbLabelQr != null)
                {
                    response.Content = new GetLabelQrDTO
                    {
                        Id = dbLabelQr.Id,
                        Plate = dbLabelQr.Plate,
                        UserEmail = dbLabelQr.UserEmail,
                        LastMark = dbLabelQr.LastMark,
                        Amount= dbLabelQr.Amount,
                        ExpiredDateMark = dbLabelQr.LastMark.AddHours(1)
                    };
                    response.IsSuccess = true;
                }
                else
                {
                    response.IsSuccess = false;
                    response.ErrorMessage = "No existe una EtiquetaQr con este Id";
                }

            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
                response.IsSuccess = false;
            }
            return response;

        }
        public async Task<ResponseAPI<GetLabelQrDTO>> GetLabelQrByPlate(string LabelPlate)
        {
            var response = new ResponseAPI<GetLabelQrDTO>();
            try
            {
                var dbLabelQr = await _dbContext.LabelsQr.FirstOrDefaultAsync(l => l.Plate == LabelPlate);
                if (dbLabelQr != null)
                {
                    response.Content = new GetLabelQrDTO
                    {
                        Id = dbLabelQr.Id,
                        Plate = dbLabelQr.Plate,
                        UserEmail = dbLabelQr.UserEmail,
                        LastMark = dbLabelQr.LastMark,
                        Amount= dbLabelQr.Amount,
                        ExpiredDateMark = dbLabelQr.LastMark.AddHours(1)
                    };
                    response.IsSuccess = true;
                }
                else
                {
                    response.IsSuccess = false;
                    response.ErrorMessage = "No existe una EtiquetaQr con este Placa";
                }

            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
                response.IsSuccess = false;
            }
            return response;

        }
        public async Task<ResponseAPI<string>> UpdateLabelQr(CreateLabelQrDTO labelDto, int id)
        {
            var response = new ResponseAPI<string>();
            try
            {
                var dbLabelQr = await _dbContext.LabelsQr.FirstOrDefaultAsync(l => l.Id == id);
                if (dbLabelQr != null)
                {
                    dbLabelQr.UserEmail = labelDto.UserEmail;
                    dbLabelQr.Plate = labelDto.Plate;
                    await _dbContext.SaveChangesAsync();
                    response.IsSuccess = true;
                    response.Content = "Etiqueta actualizada correctamente";
                }
                else
                {
                    response.IsSuccess = false;
                    response.ErrorMessage = "No existe una EtiquetaQr con este id";
                }

            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
                response.IsSuccess = false;
            }
            return response;
        }
        public async Task<ResponseAPI<string>> DeleteLabelQr(int id)
        {
            var response = new ResponseAPI<string>();
            try
            {
                var dbLabelQr = await _dbContext.LabelsQr.FirstOrDefaultAsync(l => l.Id == id);
                if (dbLabelQr != null)
                {
                    
                     _dbContext.Remove(dbLabelQr);
                    await _dbContext.SaveChangesAsync();
                    response.IsSuccess = true;
                    response.Content = "Etiqueta eliminada correctamente";
                }
                else
                {
                    response.IsSuccess = false;
                    response.ErrorMessage = "No existe una EtiquetaQr con este id";
                }

            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
                response.IsSuccess = false;
            }
            return response;
        }
        public async Task<ResponseAPI<string>> RechargeCash(RechargeCashDTO rechargeDTO)
        {
            var response = new ResponseAPI<string>();
            try
            {
                var existingLabel = await _dbContext.LabelsQr.FirstOrDefaultAsync(l => l.Id == rechargeDTO.IdLabelQr);
                if (existingLabel != null)
                {
                    existingLabel.Amount += rechargeDTO.CashAmount;
                    await _dbContext.SaveChangesAsync();

                    response.IsSuccess = true;
                    response.Content = "Se ha realizado la recarga de $" + rechargeDTO.CashAmount + " a la etiqueta con placa: " + existingLabel.Plate + " Correctamente.";

                }
                else
                {
                    response.IsSuccess = false;
                    response.ErrorMessage = "No se ha encontrado una etiqueta con este id";
                }
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
                response.IsSuccess = false;
            }

            return response;
        }
        public async Task<ResponseAPI<string>> CheckHourLabelQR(CheckHourDTO ChecklabelQrDTO)
        {
            var response = new ResponseAPI<string>();
            try
            {
                var existingLabel = await _dbContext.LabelsQr.FirstOrDefaultAsync(l => l.Id == ChecklabelQrDTO.IdLabelQr);
                if (existingLabel != null)
                {
                    if (existingLabel.Amount - 0.25m < 0)
                    {
                        response.IsSuccess = false;
                        response.ErrorMessage = "Esta tarjeta no tiene fondos suficientes";
                    }
                    else
                    {
                        existingLabel.LastMark = ChecklabelQrDTO.LastMark;
                        existingLabel.Amount -= 0.25m;
                        await _dbContext.SaveChangesAsync();
                        response.IsSuccess = true;
                        response.Content = "Check Hora confirmado";
                    }
                }
                else
                {
                    response.IsSuccess = false;
                    response.ErrorMessage = "No se ha encontrado una etiqueta con este id";
                }
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
