using Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Context {
    public class DatabaseContext : DbContext {
        public DbSet<User> Users {
            get; set;
        }

        public DbSet<Laboratory> Laboratories {
            get; set;
        }

        public DbSet<Role> Role {
            get; set;
        }

        public DatabaseContext() : base("DefaultConnection") {
            Configuration.LazyLoadingEnabled = true;
            Configuration.ProxyCreationEnabled = true;            
        }

        public static DatabaseContext Create() {
            return new DatabaseContext();
        }
        
    }
}
