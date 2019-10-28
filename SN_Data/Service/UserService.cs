using SN_Core.Interface;
using SN_WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SN_Data.Service
{
    class UserService : IUser
    {
        public ApplicationUser findUserByEmail(string email)
        {
            ApplicationUser a = new ApplicationUser();
            a.Email = "teste@teste.com";
            a.Id = "1";
            a.Name = "Gabi gol";
            a.University = "Infnet";
            a.Biography = "Eu sou porra louca";
            a.StartDate = DateTime.Now;
            return a;
        }
    }
}
