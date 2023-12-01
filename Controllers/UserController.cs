using API_CSharp.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API_CSharp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IConfiguration Configuration { get; set; }

        public UserController(IConfiguration configuration) {
            Configuration = configuration;
        }

        [HttpPost]
        public dynamic SignIn([FromBody] Object optData)
        {
            var data = JsonConvert.DeserializeObject<dynamic>(optData.ToString());
            string username = data.username.ToString();
            string password = data.password.ToString();

            API_CSharp.Model.User? userFounded = API_CSharp.Model.User.DB().Where(user => user.UserName == username && user.Password == password).FirstOrDefault();

            if (userFounded == null) {
                return new
                {
                    success = false,
                    message = "User doesn't exist in DB"
                };
            }

            var jwt = Configuration.GetSection("Jwt").Get<Jwt>();
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("id", userFounded.Id.ToString()),
                new Claim("username", userFounded.UserName),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                jwt.Issuer,
                jwt.Audience,
                claims,
                signingCredentials: signIn,
                expires: DateTime.Now.AddMinutes(4)
            );

            return new
            {
                success = true,
                message = "Request successfully",
                result = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }
    }
}
