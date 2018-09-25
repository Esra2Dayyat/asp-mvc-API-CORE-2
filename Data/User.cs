using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace DontCoreApiAuthantaication.Data
{
    public class User
    { public int ID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }


    public  class UserContext : DbContext {
        public UserContext()
        {
        }

        public UserContext (DbContextOptions<UserContext> option ) : base (option) {
        } 

        public DbSet<User> Users { get; set;  }



    }
  
}
