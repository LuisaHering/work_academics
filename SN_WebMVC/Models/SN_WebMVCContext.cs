using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SN_WebMVC.Models
{
    public class SN_WebMVCContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public SN_WebMVCContext() : base("name=SN_WebMVCContext")
        {
        }

        public System.Data.Entity.DbSet<SN_WebMVC.Models.ProjectViewModel> ProjectViewModels {
            get; set;
        }

        public System.Data.Entity.DbSet<SN_WebMVC.Models.ProfileViewModel> ProfileViewModels {
            get; set;
        }

        public System.Data.Entity.DbSet<SN_WebMVC.Models.PostViewModel> PostViewModels {
            get; set;
        }
    }
}
