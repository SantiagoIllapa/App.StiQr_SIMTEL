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
    public class UsersController : ControllerBase
    {
        private readonly StiqrDbContext _context;

        public UsersController(StiqrDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetUsers()
        {
            var responseAPI = new ResponseAPI<List<UserDTO>>();
            var listUserDTO = new List<UserDTO>();

            try
            {
                foreach (var user in await _context.Users.ToListAsync())
                {
                    listUserDTO.Add(new UserDTO
                    {
                        Id = user.Id,
                        Name = user.Name,
                        LastName = user.LastName,
                        Cidentity = user.Cidentity,
                        Email = user.Email,
                        Phone = user.Phone,
                        Password = user.Password,
                    });
                }
                responseAPI.IsSuccess = true;
                responseAPI.Content = listUserDTO;

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
        public async Task<IActionResult> GetUser(int id)
        {
            var responseAPI = new ResponseAPI<UserDTO>();
            var userDTO = new UserDTO();

            try
            {
                var dbUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
                if (dbUser != null)
                {
                    userDTO.Id = dbUser.Id;
                    userDTO.Name = dbUser.Name;
                    userDTO.LastName = dbUser.LastName;
                    userDTO.Cidentity = dbUser.Cidentity;
                    userDTO.Email = dbUser.Email;
                    userDTO.Phone = dbUser.Phone;
                    responseAPI.IsSuccess = true;
                    responseAPI.Content = userDTO;
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
        public async Task<IActionResult> CreateUser(UserDTO userDTO)
        {
            var responseAPI = new ResponseAPI<int>();
            try
            {
                var dbUser = new User
                {
                    Name = userDTO.Name,
                    LastName = userDTO.LastName,
                    Cidentity = userDTO.Cidentity,
                    Email = userDTO.Email,
                    Phone = userDTO.Phone,
                    Password = userDTO.Password,
                };
                _context.Users.Add(dbUser);
                await _context.SaveChangesAsync();
                if (dbUser.Id != 0) {
                    responseAPI.IsSuccess = true;
                    responseAPI.Content = dbUser.Id;
                }
                else
                {
                    responseAPI.IsSuccess = false;
                    responseAPI.ErrorMessage = "No se pudo guardar el usuario";
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
        public async Task<IActionResult> EditUser(UserDTO userDTO, int id)
        {
            var responseAPI = new ResponseAPI<int>();
            try
            {
                var dbUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
                if (dbUser != null)
                {
                    dbUser.Name = userDTO.Name;
                    dbUser.LastName = userDTO.LastName;
                    dbUser.Cidentity = userDTO.Cidentity;
                    dbUser.Email = userDTO.Email;
                    dbUser.Phone = userDTO.Phone;
                    dbUser.Password = userDTO.Password;

                    _context.Users.Update(dbUser);
                    await _context.SaveChangesAsync();

                    responseAPI.IsSuccess = true;
                    responseAPI.Content = dbUser.Id;
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
        public async Task<IActionResult> DeleteUser(int id)
        {
            var responseAPI = new ResponseAPI<int>();
            try
            {
                var dbUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
                if (dbUser != null)
                {
                    _context.Users.Remove(dbUser);
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
