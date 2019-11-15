using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models {
    public class Laboratory {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {
            get; set;
        }

        public string Descricao {
            get; set;
        }

        public virtual ICollection<User> Users {
            get; set;
        }

        public virtual ICollection<Project> Projects {
            get; set;
        }

        public Laboratory() {
            Users = new HashSet<User>();
            Projects = new HashSet<Project>();
        }

        public void Adiciona(User user) {            
            this.Users.Add(user);                
        }
    }
}
