using Core.Models;
using Core.Services;
using Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Services {
    public class AspNetUsersImpl : IAspNetUsers {

        private DatabaseContext databaseContext = new DatabaseContext();

        public AspNetUsers FindByEmail(string email) {
            try {
                //var obj = databaseContext.Users.Where(u => u.Email == email);
                var obj = databaseContext.Users.Find(email);
            } catch (Exception e) {
                Console.WriteLine(e.Message);
            }
            return null;
        }
    }
}
