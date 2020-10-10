using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Services
{
    public class ShopServices
    {
        private IConfiguration _config;

        public ShopServices(IConfiguration config)
        {
            _config = config;
        }

        public void Add(Shop model)
        {
            using (var db = new NatFlutterContext(_config))
            {
                db.Shops.Add(model);
                db.SaveChanges();
            }
        }

        public List<Shop> GetAll()
        {
            using (var db = new NatFlutterContext(_config))
            {
                return db.Shops.ToList();
            }
        }
    }
}
