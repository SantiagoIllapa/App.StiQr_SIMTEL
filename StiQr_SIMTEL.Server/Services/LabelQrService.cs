﻿using Microsoft.AspNetCore.Identity;
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
                            LastMark = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("America/Guayaquil")),
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
        public async Task<ResponseAPI<string>> CheckHourLabelQR(CheckHourLabelQrDTO ChecklabelQrDTO, int id)
        {
            var response = new ResponseAPI<string>();
            try
            {
                var existingLabel = await _dbContext.LabelsQr.FirstOrDefaultAsync(l => l.Id == id);
                if (existingLabel != null)
                {
                    existingLabel.LastMark = ChecklabelQrDTO.LastMark;
                    await _dbContext.SaveChangesAsync();
                    response.IsSuccess = true;
                    response.Content = "Check Hora confirmado";

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