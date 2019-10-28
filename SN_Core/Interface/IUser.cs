using SN_WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SN_Core.Interface
{
    public interface IUser
    {
        ApplicationUser findUserByEmail(string email); 
    }
}
