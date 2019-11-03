using Core.Models;
using Core.Services;
using Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Services {
    public class UsersImpl : IUsers {

        private DatabaseContext database = new DatabaseContext();

        public bool Create(User user) {

            try {
                database.Users.Add(user);
                database.SaveChangesAsync();
                return true;
            } catch(Exception e) {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
            return false;
        }

        public User FindByEmail(string email) {         
            return database.Users.Where(x => x.Email == email).FirstOrDefault();
        }
    }
}
