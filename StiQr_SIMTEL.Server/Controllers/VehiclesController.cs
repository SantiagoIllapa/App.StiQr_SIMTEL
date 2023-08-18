using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StiQr_SIMTEL.Server.Models;
using StiQr_SIMTEL.Shared;
using System.Numerics;
using System.Security.Principal;

namespace StiQR_SIMTEL_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly StiqrDbContext _context;

        public VehiclesController(StiqrDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetVehicles()
        {
            var responseAPI = new ResponseAPI<List<VehicleDTO>>();
            var listVehicleDTO = new List<VehicleDTO>();

            try
            {
                foreach (var vehicle in await _context.Vehicles.ToListAsync())
                {
                    listVehicleDTO.Add(new VehicleDTO
                    {
                        Id = vehicle.Id,
                        Alias = vehicle.Alias,
                        Description = vehicle.Description,
                        Plate   = vehicle.Plate,
                        IdUser  = vehicle.IdUser,
                    });
                }
                responseAPI.IsSuccess = true;
                responseAPI.Content = listVehicleDTO;

            }
            catch (Exception ex)
            {
                responseAPI.IsSuccess = false;
                responseAPI.ErrorMessage = ex.Message;
            }
            return Ok(responseAPI);
        }

        [HttpGet]
        [Route("GetByID/{id}")]
        public async Task<IActionResult> GetVehicle(int id)
        {
            var responseAPI = new ResponseAPI<VehicleDTO>();
            var vehicleDTO = new VehicleDTO();

            try
            {
                var dbVehicle = await _context.Vehicles.FirstOrDefaultAsync(x => x.Id == id);
                if (dbVehicle != null)
                {
                    vehicleDTO.Id= dbVehicle.Id;
                    vehicleDTO.Alias= dbVehicle.Alias;
                    vehicleDTO.Description= dbVehicle.Description;
                    vehicleDTO.Plate = dbVehicle.Plate;
                    vehicleDTO.IdUser = dbVehicle.IdUser;

                    responseAPI.IsSuccess = true;
                    responseAPI.Content = vehicleDTO;
                }
                else
                {
                    responseAPI.IsSuccess = false;
                    responseAPI.ErrorMessage = "No encontrado";
                }



            }
            catch (Exception ex)
            {
                responseAPI.IsSuccess = false;
                responseAPI.ErrorMessage = ex.Message;
            }
            return Ok(responseAPI);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateVehicle(VehicleDTO vehicleDTO)
        {
            var responseAPI = new ResponseAPI<int>();
            try
            {
                var dbVehicle = new Vehicle
                {
                    Plate= vehicleDTO.Plate,
                    Alias       = vehicleDTO.Alias,
                    Description= vehicleDTO.Description,
                    IdUser= vehicleDTO.IdUser,
                               
                };
                _context.Vehicles.Add(dbVehicle);
                await _context.SaveChangesAsync();

                if (dbVehicle.Id != 0) {
                    responseAPI.IsSuccess = true;
                    responseAPI.Content = dbVehicle.Id;
                }
                else
                {
                    responseAPI.IsSuccess = false;
                    responseAPI.ErrorMessage = "No se pudo guardar el Vehiculo";
                }

            }
            catch (Exception ex)
            {
                responseAPI.IsSuccess = false;
                responseAPI.ErrorMessage = ex.Message;
            }
            return Ok(responseAPI);
        }

        [HttpPut]
        [Route("Edit/{id}")]
        public async Task<IActionResult> EditVehicle(VehicleDTO vehicileDTO, int id)
        {
            var responseAPI = new ResponseAPI<int>();
            try
            {
                var dbVehicle = await _context.Vehicles.FirstOrDefaultAsync(x => x.Id == id);
                if (dbVehicle != null)
                {
                    dbVehicle.Plate = vehicileDTO.Plate;
                    dbVehicle.Description = vehicileDTO.Description;
                    dbVehicle.Alias= vehicileDTO.Alias;
                    dbVehicle.IdUser= vehicileDTO.IdUser;

                    _context.Vehicles.Update(dbVehicle);
                    await _context.SaveChangesAsync();

                    responseAPI.IsSuccess = true;
                    responseAPI.Content = dbVehicle.Id;
                }
                else
                {
                    responseAPI.IsSuccess = false;
                    responseAPI.ErrorMessage = "Usuario no encontrado";
                }

            }
            catch (Exception ex)
            {
                responseAPI.IsSuccess = false;
                responseAPI.ErrorMessage = ex.Message;
            }
            return Ok(responseAPI);
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var responseAPI = new ResponseAPI<int>();
            try
            {
                var dbVehicle = await _context.Vehicles.FirstOrDefaultAsync(x => x.Id == id);
                if (dbVehicle != null)
                {
                    _context.Vehicles.Remove(dbVehicle);
                    await _context.SaveChangesAsync();
                    responseAPI.IsSuccess = true;
                }
                else
                {
                    responseAPI.IsSuccess = false;
                    responseAPI.ErrorMessage = "Usuario no encontrado";
                }

            }
            catch (Exception ex)
            {
                responseAPI.IsSuccess = false;
                responseAPI.ErrorMessage = ex.Message;
            }
            return Ok(responseAPI);
        } 
    

    }
}
