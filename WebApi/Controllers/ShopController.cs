using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private IConfiguration _config;

        public ShopController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            ShopServices shs = new ShopServices(_config);
            var list = shs.GetAll();
            return Ok(new ResponseResult(200, StatusMessage.Completed.ToString(), "", list));
        }

        [HttpPost("Add")]
        public IActionResult Add(string name, string address, string phoneNo)
        { 
            Shop model = new Shop()
            {
                Name = name,
                Address = address,
                PhoneNo = phoneNo,

            };

            ShopServices shs = new ShopServices(_config);
            shs.Add(model);

            return Ok(new ResponseResult(200, StatusMessage.Completed.ToString(), "", model));
        }

    }
}
