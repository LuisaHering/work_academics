﻿using Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Context {
    public class DatabaseContext : DbContext {
        public DbSet<AspNetUsers> Users {
            get; set;
        }
    }
}
