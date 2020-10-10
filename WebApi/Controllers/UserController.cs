using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private IConfiguration _config;

        public UserController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            UserServices uss = new UserServices(_config);
            var list = uss.GetAll();
            return Ok(new ResponseResult(200, StatusMessage.Completed.ToString(), "", list));
        }

        [HttpGet("GetByUsername")]
        public IActionResult GetByUsername(string username)
        {
            UserServices uss = new UserServices(_config);
            var user = uss.GetByUsername(username);
            if (user == null)
                return Ok(new ResponseResult(200, StatusMessage.NotFound.ToString(), "Not Found", null));
            return Ok(new ResponseResult(200, StatusMessage.Completed.ToString(), "", user));
        }


        [HttpPost("Add")]
        public IActionResult Add(string username, string password, string email, string type)
        {
            User model = new User()
            {
                Username = username,
                Password = password,
                Email = email,
                Type = type,
                Hash = "",
                CreatedDate = DateTime.Now,
                CreatedBy = "web",
                UpdatedDate = DateTime.Now,
                UpdatedBy = "web"
            };

            UserServices uss = new UserServices(_config);
            var isDupUser = uss.GetByUsername(username);
            if (isDupUser != null)
                return Ok(new ResponseResult(200, StatusMessage.Error.ToString(), "Username is duplicate"));

            uss.Add(model);
            return Ok(new ResponseResult(200, StatusMessage.Completed.ToString(), "", model));
        }
    }
}
