using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Services
{
    public class LoginServices
    {
        private IConfiguration _config;

        public LoginServices(IConfiguration config)
        {
            _config = config;
        }


        public User AuthenticateUser(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            if (username == "user" && password == "password")
            {
                return new User()
                {
                    Id = 1,
                    Username = username,
                    Password = password,
                    Email = username + "@gmail.com"
                };
            }

            UserServices uss = new UserServices(_config);
            var user = uss.GetLogin(username, password);
            return user;
        }

        public string GenerateJSONWebToken(User login)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, login.Username),
                new Claim(JwtRegisteredClaimNames.Email, login.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials
                );

            var encodedToken = new JwtSecurityTokenHandler().WriteToken(token);
            return encodedToken;
        }
    }
}
