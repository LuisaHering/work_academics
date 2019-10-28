using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SN_WebApi.Models
{
    public class DatabaseContext : DbContext
    {
        public DbSet<ApplicationUser> applicationUser
        {
            get; set;
        }
    }
}