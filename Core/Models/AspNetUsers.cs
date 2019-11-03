using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models {
    public class AspNetUsers {

        public string Id {
            get; set;
        }

        public string Name {
            get; set;
        }

        public string University {
            get; set;
        }

        public int Biography {
            get; set;
        }

        public DateTime StartDate {
            get; set;
        }

        [Key]
        public string Email {
            get; set;
        }

        public string EmailConfirmed {
            get; set;
        }

        public string PasswordHash {
            get; set;
        }

        public string SecurityStamp {
            get; set;
        }

        public string PhoneNumber {
            get; set;
        }

        public bool PhoneNumberConfirmed {
            get; set;
        }

        public bool TwoFactorEnabled {
            get; set;
        }

        public DateTime LockoutEndDateUtc {
            get; set;
        }

        public bool LockoutEnabled {
            get; set;
        }

        public int AccessFailedCount {
            get; set;
        }

        public string UserName {
            get; set;
        }

        public AspNetUsers() {

        }
    }
}
