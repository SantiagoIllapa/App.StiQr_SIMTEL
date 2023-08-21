using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StiQr_SIMTEL.Server.Data;
using StiQr_SIMTEL.Server.Models;
using StiQr_SIMTEL.Shared.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StiQr_SIMTEL.Server.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        public UsersController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager , IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPost("CreateRole")]
        public async Task<IActionResult> CreateRole(CreateRoleDTO roleDTO)
        {

            var response = await _roleManager.CreateAsync(new IdentityRole
            {
                Name = roleDTO.RoleName
            });
            if (response.Succeeded)
            {
                return Ok("Rol Creado");
            }
            else
            {
                return BadRequest(response.Errors);
            }
        }

        [HttpPost("AssignRoleToUser")]
        public async Task<IActionResult> AssignRoleToUser(AssignRoleToUserDTO assignRoleToUserDTO)
        {
            var userDetails = await _userManager.FindByEmailAsync(assignRoleToUserDTO.Email);
            if (userDetails != null)
            {
               var userRoleAssignResponse =await _userManager.AddToRoleAsync(userDetails, assignRoleToUserDTO.RoleName);
                if (userRoleAssignResponse.Succeeded)
                {
                    return Ok("Rol asignado al usuario: " + assignRoleToUserDTO.RoleName);
                }
                else
                {
                    return BadRequest(userRoleAssignResponse.Errors);
                }
            }
            else
            {
                return BadRequest("Este usuario no existe");
            }
           
        }
        [AllowAnonymous]
        [HttpPost("AuthenticateUser")]
        public async Task<IActionResult> AuthenticateUser(AuthenticateUser authenticateUser)
        {
            var user = await _userManager.FindByNameAsync(authenticateUser.UserName);
            if (user == null) return Unauthorized();
            bool isVaidUser = await _userManager.CheckPasswordAsync(user, authenticateUser.Password);
            if (isVaidUser)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
#pragma warning disable CS8604 // Posible argumento de referencia nulo
                var keyDetail = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier,user.Id),
                    new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}")
                };
#pragma warning restore CS8604 // Posible argumento de referencia nulo
                var tokenDescription = new SecurityTokenDescriptor
                {
                    Audience = _configuration["JWT:Audience"],
                    Issuer = _configuration["JWT:Issuer"],
                    Expires = DateTime.UtcNow.AddDays(5),
                    Subject = new ClaimsIdentity(claims),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyDetail),SecurityAlgorithms.HmacSha256Signature),
                };
                var token = tokenHandler.CreateToken(tokenDescription);
                return Ok(tokenHandler.WriteToken(token));
            }
            else
            {
                return Unauthorized();
            }
        }


        [AllowAnonymous] 
        [HttpPost("RegisterUser")]
        public async Task<IActionResult> RegisterUser(RegisterUserDTO registerUserDTO)
        {
            var userToBeCreated = new User
            {
                FirstName = registerUserDTO.FirstName,
                LastName = registerUserDTO.LastName,
                UserName = registerUserDTO.Email,
                Email = registerUserDTO.Email,
                
            };
            var response = await _userManager.CreateAsync(userToBeCreated, registerUserDTO.Password);
            if (response.Succeeded)
            {
                return Ok("Usuario Creado");
            }
            else
            {
                return BadRequest(response.Errors);
            }
        }
        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser(UserEmailDTO userDetails)
        {

            var existingUser = await _userManager.FindByEmailAsync(userDetails.Email);
            if (existingUser != null)
            {
                var response = await _userManager.DeleteAsync(existingUser);
                if (response.Succeeded)
                {

                    return Ok("Usuario Eliminado");
                }
                else
                {
                    return BadRequest(response.Errors);
                }
            }
            else
            {
                return BadRequest("No se ha encontado el usuario");
            }

        }

    }
}
