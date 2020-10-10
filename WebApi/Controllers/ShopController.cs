using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApi.Helpers;
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

        [HttpGet("GetById")]
        public IActionResult GetById(int id)
        {
            ShopServices shs = new ShopServices(_config);
            var model = shs.GetById(id);
            return Ok(new ResponseResult(200, StatusMessage.Completed.ToString(), "", model));
        }

        [HttpPost("AddOrUpdate")]
        public async Task<IActionResult> AddOrUpdate(int? id, string name, string address, string phoneNo, string latitude, string longitude, bool? isActive, string updatedBy, IFormFile file)
        {
            int shopId = !id.HasValue ? 0 : id.Value;

            string imageUrl = "";
            ShopServices shs = new ShopServices(_config);
            string lat = latitude;
            string lng = longitude;
            string createdBy = updatedBy;
            DateTime? createdDate = DateTime.Now;

            if (shopId > 0)
            {
                Shop local = shs.GetById(shopId);
                if (local == null)
                {
                    return Ok(new ResponseResult(200, StatusMessage.NotFound.ToString(), "", null));
                }

                imageUrl = await FileHelpers.FileUpload(shopId, file) ?? local.ImageUrl;
                lat = !string.IsNullOrEmpty(latitude) ? latitude : local.Latitude;
                lng = !string.IsNullOrEmpty(longitude) ? longitude : local.Longitude;
                createdBy = local.CreatedBy;
                createdDate = local.CreatedDate;
            }
            
            Shop model = new Shop()
            {
                Id = shopId,
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

            if (shopId == 0 && file.Length > 0)
            {
                imageUrl = await FileHelpers.FileUpload(model.Id, file);
                model.ImageUrl = imageUrl;
                shs.AddOrUpdate(model);
            }



            return Ok(new ResponseResult(200, StatusMessage.Completed.ToString(), "", model));
        }


    }
}
