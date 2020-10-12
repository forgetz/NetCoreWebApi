using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Services
{
    public class ShopServices
    {
        private readonly IConfiguration _config;
        private readonly ILogger<Controllers.ShopController> _logger;

        public ShopServices(IConfiguration config, ILogger<Controllers.ShopController> logger)
        {
            _config = config;
            _logger = logger;
        }

        public void AddOrUpdate(Shop model)
        {
            _logger.LogInformation("AddOrUpdate");
            using (var db = new NatFlutterContext(_config))
            {
                var local = db.Shops.Where(s => s.UserId == model.UserId).FirstOrDefault();
                if (local == null)
                {
                    _logger.LogInformation("Add: " + model.UserId);
                    db.Shops.Add(model);
                    db.SaveChanges();
                    return;
                }
                else
                {
                    _logger.LogInformation("Update: " + model.UserId);
                    model.Id = local.Id;
                    db.Entry(local).State = EntityState.Detached;
                    db.Entry(model).State = EntityState.Modified;
                    db.SaveChanges();
                    return;
                }

            }
        }

        public List<Shop> GetAll()
        {
            using (var db = new NatFlutterContext(_config))
            {
                return db.Shops.ToList();
            }
        }

        public Shop GetById(int id)
        {
            using (var db = new NatFlutterContext(_config))
            {
                return db.Shops.Where(s => s.Id == id).FirstOrDefault();
            }
        }

        public Shop GetByUserId(int userId)
        {
            using (var db = new NatFlutterContext(_config))
            {
                return db.Shops.Where(s => s.UserId == userId).FirstOrDefault();
            }
        }
    }
}
