using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WebApi.Helpers;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ILogger<ShopController> _logger;

        public ShopController(IConfiguration config, ILogger<ShopController> logger)
        {
            _config = config;
            _logger = logger;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            _logger.LogInformation("Shop/GetAll");
            ShopServices shs = new ShopServices(_config, _logger);
            var list = shs.GetAll();
            return Ok(new ResponseResult(200, StatusMessage.Completed.ToString(), "", list));
        }

        [HttpGet("GetById")]
        public IActionResult GetById(int userid)
        {
            ShopServices shs = new ShopServices(_config, _logger);
            var model = shs.GetById(userid);
            return Ok(new ResponseResult(200, StatusMessage.Completed.ToString(), "", model));
        }

        [HttpGet("GetByUserId")]
        public IActionResult GetByUserId(int id)
        {
            ShopServices shs = new ShopServices(_config, _logger);
            var model = shs.GetByUserId(id);
            return Ok(new ResponseResult(200, StatusMessage.Completed.ToString(), "", model));
        }

        [HttpPost("AddOrUpdate")]
        public async Task<IActionResult> AddOrUpdate(int? id, string name, string address, string phoneNo, string latitude, string longitude, bool? isActive, string updatedBy, IFormFile file)
        {
            _logger.LogInformation("Shop/AddOrUpdate");
            _logger.LogInformation("id: {0} name: {1} file: {2} type: {3} len: {4}", id, name, file.FileName, file.ContentType, file.Length);

            int userId = !id.HasValue ? 0 : id.Value;

            string imageUrl = "";
            ShopServices shs = new ShopServices(_config, _logger);
            string lat = latitude;
            string lng = longitude;
            string createdBy = updatedBy;
            DateTime? createdDate = DateTime.Now;

            Shop local = shs.GetByUserId(userId);
            if (local != null)
            {
                _logger.LogInformation("local.userid=" + local.UserId);
                imageUrl = await FileHelpers.FileUpload(userId, file) ?? local.ImageUrl;
                lat = !string.IsNullOrEmpty(latitude) ? latitude : local.Latitude;
                lng = !string.IsNullOrEmpty(longitude) ? longitude : local.Longitude;
                createdBy = local.CreatedBy;
                createdDate = local.CreatedDate;
            }
            else
            {
                _logger.LogInformation("New");
                imageUrl = await FileHelpers.FileUpload(userId, file);
            }
            
            Shop model = new Shop()
            {
                UserId = userId,
                Name = name,
                Address = address,
                PhoneNo = phoneNo,
                ImageUrl = imageUrl,
                Latitude = lat,
                Longitude = lng,
                IsActive = isActive == null ? true : isActive,
                IsDelete = false,
                UpdatedDate = DateTime.Now,
                UpdatedBy = updatedBy,
                CreatedDate = createdDate,
                CreatedBy = createdBy
            };
            
            shs.AddOrUpdate(model);

            return Ok(new ResponseResult(200, StatusMessage.Completed.ToString(), "", model));
        }


    }
}
