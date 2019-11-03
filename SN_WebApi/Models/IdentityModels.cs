using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace SN_WebApi.Models {

    public class ApplicationUser : IdentityUser {

        public string Name {
            get; set;
        }

        public string University {
            get; set;
        }

        public string Biography {
            get; set;
        }

        public DateTime StartDate {
            get; set;
        }

        [Key]
        override
        public string Email {
            get; set;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]        
        public int Key{
            get; set;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType) {
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            return userIdentity;
        }

        public static implicit operator IdentityResult(ApplicationUser v) {
            throw new NotImplementedException();
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser> {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false) {
        }

        public static ApplicationDbContext Create() {
            return new ApplicationDbContext();
        }
    }
}