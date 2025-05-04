using Amazon.Util;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VirtualMachinesCore.Application.Interfaces;
using VirtualMachinesCore.Domain.Entities;

namespace VirtualMachinesCore.Application.Services
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;        

        public JwtMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;             
        }

        public async Task Invoke(HttpContext context, IUserMiddlewareService userServices)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                attachUserToContext(context, userServices, token);

            await _next(context);
        }

        private void attachUserToContext(HttpContext context, IUserMiddlewareService userServices, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]); ;
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time
                    // (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                //var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
                var userEmail = jwtToken.Claims.First(x => x.Type == "unique_name").Value;

                // attach user to context on successful jwt validation
                User user = userServices.GetUser(new User() { Username = userEmail}).Result;
                context.Items["User"] = user;

                List<Claim> claim = new List<Claim>() {
                    new Claim(ClaimTypes.Name, user.Username)
                };                
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claim, ClaimsIdentity.DefaultNameClaimType);
                ClaimsPrincipal principal = new ClaimsPrincipal(claimsIdentity);
                context.User = principal;
            }
            catch
            {
                // do nothing if jwt validation fails user is not attached to context so request
                // won't have access to secure routes
            }
        }
    }

}
