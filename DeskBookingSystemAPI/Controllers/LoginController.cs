using DataAccess;
using DeskBookingSystemAPI.TestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DeskBookingSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration Configuration { get; set; }
        public DataProcessor DataProcessor { get; set; }
        public LoginController(IConfiguration configuration)
        {
            DataProcessor = new();
            Configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserLogin userLogin)
        {
            var user = await Authenticate(userLogin);

            if (user is not null)
            {
                var token = GenerateToken(user);
                return Ok(token);
            }

            return NotFound("User couldn't be found");
        }

        //searching user 
        private async Task<DataAccess.Models.UserModel> Authenticate(UserLogin userLogin)
        {
            List<DataAccess.Models.UserModel> currentUserList = await DataProcessor.GetAllUsers();
            var currentUser = currentUserList.FirstOrDefault(user =>
                 user.Username.ToLower() == userLogin.Username.Trim().ToLower() && user.Password == userLogin.Password.Trim().ToLower());
            
            //var currentUser = UserConstants.Users.FirstOrDefault( user => 
            //    user.Username.ToLower()== userLogin.Username.ToLower() &&
            //    user.Password == userLogin.Password);
            
            if (currentUser is not null)
            {
                return currentUser;
            }
            // if we don't find user
            return currentUser;
        }

        //generating token
        private string GenerateToken(DataAccess.Models.UserModel user)
        {
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            //claiming users data
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Username),
                new Claim(ClaimTypes.SerialNumber, user.ID.ToString()),
                new Claim(ClaimTypes.Role, user.Role),
            };

            JwtSecurityToken token = new JwtSecurityToken(Configuration["Jwt:Issuer"], Configuration["Jwt:Audience"], claims, expires: DateTime.Now.AddMinutes(15), signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
