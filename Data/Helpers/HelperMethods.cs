using Apoint_pro.Data.models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Apoint_pro.Data.Helpers
{
    public class HelperMethods
    {
        //private readonly IConfiguration configuration;
        private  readonly HttpContext httpContext;
        public HelperMethods(HttpContext httpContext)
        {
            this.httpContext = httpContext;
            //this.configuration = configuration;
        }
        public static Object createToken(User userDb , IConfiguration configuration)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim("Role", userDb.userType.descripton));
            claims.Add(new Claim("id", userDb.Id.ToString()));
            claims.Add(new Claim("Email", userDb.Email));
            claims.Add(new Claim("Name", userDb.Name));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"]));
            
            var sc = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
            claims: claims,
            issuer: configuration["JWT:issuer"],
            audience: configuration["JWT:audience"],
            expires: DateTime.Now.AddHours(5),
            signingCredentials: sc
            );
            var ftoken = new { token = new JwtSecurityTokenHandler().WriteToken(token), expiration = token.ValidTo };
            return ftoken;
        }

        public  int GetIdFromToken()
        {
            var token = httpContext.Request.Headers["Authorization"]
                      .ToString().Replace("Bearer ", "");

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var id = jwtToken.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            return int.Parse(id);
        }

        public  string GetRoleFromToken()
        {
            var token = httpContext.Request.Headers["Authorization"]
                     .ToString().Replace("Bearer ", "");

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var role = jwtToken.Claims.FirstOrDefault(c => c.Type == "Role")?.Value;
            return role;
        }

    }
}
