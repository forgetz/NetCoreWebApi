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

        public void AddOrUpdate(Shop model)
        {
            using (var db = new NatFlutterContext(_config))
            {
                if (model.Id == 0)
                {
                    db.Shops.Add(model);
                    db.SaveChanges();
                }
                else
                {
                    db.Shops.Update(model);
                    db.SaveChanges();
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
    }
}
