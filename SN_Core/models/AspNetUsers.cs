using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SN_Core
{
    class AspNetUsers
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string University { get; set; }
        public string Biography { get; set; }
        public DateTime StartDate { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public int PhoneNumber { get; set; }
        public int PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public bool LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public bool AccessFailedCount { get; set; }
        public string UserName { get; set; }
    }
}
