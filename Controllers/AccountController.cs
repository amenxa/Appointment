using Apoint_pro.Data;
using Apoint_pro.Data.DTOS;
using Apoint_pro.Data.Helpers;
using Apoint_pro.Data.models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Asn1.Ocsp;
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
                if(Db.Users.Any(u => u.Email== user.Email)) return BadRequest(new { field = "Email", message = "This Email already exists" });
                string hashedpass =HashPasswordhelper.HashPassword(user.Password);
                user.Password = hashedpass;

                User user1 = new User ()
                {
                    Email = user.Email,
                    Name = user.Name,
                    password = user.Password
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
                        //var token = _tokenService.GenerateToken(user.Email);
                      //  return Ok(new { Token = token });
                          var token =  HelperMethods.createToken(userDb,configuration);
                          return Ok(new { token =token});
                    }
                    else return BadRequest( new { field = "Password", message = "Invalid password" });
                }
                else return BadRequest(new { field = "Email", message = $"User with email {user.Email} Not found " });
            }
            return BadRequest(new { field = "Model", message = $"ModelState Not VAlid ! " });
        }
            

        
    }
}
