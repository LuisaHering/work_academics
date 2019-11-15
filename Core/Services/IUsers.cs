using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services {
    public interface IUsers {

        Task<User> FindByEmail(string email);

        bool Create(User user);

        bool Update(User user);

        bool UpdateEF2(User user);

    }
}
