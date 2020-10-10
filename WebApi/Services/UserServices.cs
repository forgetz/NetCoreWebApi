using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Services
{
    public class UserServices
    {
        private IConfiguration _config;

        public UserServices(IConfiguration config)
        {
            _config = config;
        }

        public void Add(User model)
        {
            using (var db = new NatFlutterContext(_config))
            {
                db.Users.Add(model);
                db.SaveChanges();
            }
        }

        public List<User> GetAll()
        {
            using (var db = new NatFlutterContext(_config))
            {
                return db.Users.ToList();
            }
        }

        public User GetLogin(string username, string password)
        {
            using (var db = new NatFlutterContext(_config))
            {
                return db.Users
                    .Where(s => s.Username == username)
                    .Where(s => s.Password == password)
                    .FirstOrDefault();
            }
        }

        public User GetByUsername(string username)
        {
            using (var db = new NatFlutterContext(_config))
            {
                return db.Users
                    .Where(s => s.Username == username)
                    .FirstOrDefault();
            }
        }



    }
}
