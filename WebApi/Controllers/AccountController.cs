using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IConfiguration _config;
        public AccountController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet("Login")]
        public IActionResult Login(string username, string password)
        {
            LoginServices lgs = new LoginServices(_config);

            var user = lgs.AuthenticateUser(username, password);
            if (user == null)
                return Ok(new ResponseResult(500, StatusMessage.Error.ToString(), "Not Found"));

            var tokenString = lgs.GenerateJSONWebToken(user);
            IActionResult response = Ok(new ResponseResult(200, StatusMessage.Completed.ToString(), tokenString, user));
            return response;
        }

        //[Authorize]
        //[HttpPost("Post")]
        //public string Post()
        //{
        //    var identity = HttpContext.User.Identity as ClaimsIdentity;
        //    IList<Claim> claim = identity.Claims.ToList();

        //    var username = claim[0].Value;
        //    return "Welcome Back " + username;
        //}

        //[Authorize]
        //[HttpGet("GetValue")]
        //public ActionResult<IEnumerable<string>> Get()
        //{
        //    return new string[] { "Value1", "Value2", "Value3"};
        //}

    }

}
