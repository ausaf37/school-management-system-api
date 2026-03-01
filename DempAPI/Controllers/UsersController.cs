using DempAPI.Data;
using DempAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DempAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private ApplicationDbContext _db;

        private string _SecretKey;

        public UsersController(ApplicationDbContext db , IConfiguration configuration)
        {
            _db = db;
            _SecretKey = configuration.GetValue<string>("ApiServicekey:Secret");
        }

        [HttpPost("UserLogin")]
        public async Task<UserLoginResponce> Login(UserLoginRequest logindetails)
        {
            var user = _db.LocalUsers.Where(u => u.UserName == logindetails.UserName && u.Password == logindetails.Password).FirstOrDefault();
            if (user != null)
            {
                //creating JWT Token and returning to client
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_SecretKey);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Name, user.Id.ToString()),
                        new Claim(ClaimTypes.Role, user.Role)
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                UserLoginResponce loginResponce = new UserLoginResponce
                {
                    UserDetails = user,
                    Token = tokenHandler.WriteToken(token)
                };
                return loginResponce;
            }
            else
            {
                return null;
            }

        }
    }
}
