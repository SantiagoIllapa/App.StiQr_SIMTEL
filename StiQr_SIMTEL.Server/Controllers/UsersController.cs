using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StiQr_SIMTEL.Server.Models;
using StiQr_SIMTEL.Shared;

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
        [Route("GetAllUsers")]
        public async Task<IActionResult> GetUsers()
        {
            var responseAPI = new ResponseAPI<List<UserDTO>>();
            var listUserDTO= new List<UserDTO>();

            try
            {
                foreach( var user in await _context.Users.ToListAsync())
                {
                    listUserDTO.Add(new UserDTO
                    {
                        Id = user.Id,
                        Name= user.Name,
                        LastName = user.LastName,
                        Cidentity = user.Cidentity,
                        Email= user.Email,
                        Phone= user.Phone,
                    });
                }
                responseAPI.IsCorrect = true;
                responseAPI.Value=listUserDTO;

            }
            catch (Exception ex) 
            {
                responseAPI.IsCorrect = false;
                responseAPI.Message = ex.Message;
            }
            return Ok(responseAPI);
        }

        [HttpGet]
        [Route("GetUseByID/{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var responseAPI = new ResponseAPI<UserDTO>();
            var userDTO = new UserDTO();

            try
            {
                var dbUser = await _context.Users.FirstOrDefaultAsync(x=> x.Id==id);
                if (dbUser != null)
                {
                    userDTO.Id = dbUser.Id;
                    userDTO.Name = dbUser.Name;
                    userDTO.LastName = dbUser.LastName;
                    userDTO.Cidentity = dbUser.Cidentity;
                    userDTO.Email = dbUser.Email;
                    userDTO.Phone = dbUser.Phone;
                    responseAPI.IsCorrect = true;
                    responseAPI.Value = userDTO;
                }
                else
                {
                    responseAPI.IsCorrect = false;
                    responseAPI.Message = "No encontrado";
                }
              
                

            }
            catch (Exception ex)
            {
                responseAPI.IsCorrect = false;
                responseAPI.Message = ex.Message;
            }
            return Ok(responseAPI);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
          if (_context.Users == null)
          {
              return Problem("Entity set 'StiqrDbContext.Users'  is null.");
          }
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
