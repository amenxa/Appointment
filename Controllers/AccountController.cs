using Apoint_pro.Data;
using Apoint_pro.Data.DTOS;
using Apoint_pro.Data.Helpers;
using Apoint_pro.Data.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Apoint_pro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AppDbContext Db;
        private readonly IConfiguration configuration;
        public AccountController(AppDbContext Db, IConfiguration configuration)
        {
            this.configuration = configuration;
            this.Db = Db;
        }
        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] UserDTO user)
        {
            if (ModelState.IsValid)
            {
               string hashedpass =HashPasswordhelper.HashPassword(user.Password);
                user.Password = hashedpass;
                User user1 = new User ()
                {
                    Email = user.Email,
                    Name = user.Name,
                    password = user.Password,
                    phone = user.Phone
                };
                await Db.Users.AddAsync(user1);
                await Db.SaveChangesAsync();
                return Ok(user);
            }
            return BadRequest("Invalid register");
        }

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginDto user)
        {
            if (ModelState.IsValid)
            {
                var userDb = await Db.Users.FirstOrDefaultAsync(u => u.Email.Equals(user.Email));
                if (userDb != null)
                {
                    if (HashPasswordhelper.VerifyPassword(user.Password, userDb.password))
                    {
                        List<Claim> claims = new List<Claim>();
                        Console.WriteLine(userDb.userType==null);
                        claims.Add(new Claim(ClaimTypes.Role, userDb.userType.descripton));
                        claims.Add(new Claim(ClaimTypes.NameIdentifier, userDb.Id.ToString()));
                        claims.Add(new Claim(ClaimTypes.Email, userDb.Email));
                        claims.Add(new Claim(ClaimTypes.Name, userDb.Name));
                        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]));
                        var sc = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        var token = new JwtSecurityToken(
                        claims: claims,
                        issuer: configuration["JWT:issuer"],
                        audience: configuration["JWT:audience"],
                        expires: DateTime.Now.AddMinutes(1),
                        signingCredentials: sc
                        );
                        var ftoken = new {token = new JwtSecurityTokenHandler().WriteToken(token) , expiration = token.ValidTo };

                        return Ok(ftoken);
                    }
                    else ModelState.AddModelError("Password", "Invalid password");
                }
                else ModelState.AddModelError("Email", $"User with email {user.Email} Not found ");
            }
            return BadRequest(ModelState);
        }
            

        
    }
}
