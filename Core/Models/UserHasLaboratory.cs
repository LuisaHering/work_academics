using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models {
    public class UserHasLaboratory {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {
            get; set;
        }

        public int UserId {
            get; set;
        }

        public virtual User User {
            get; set;
        }

        public int LaboratoryId {
            get; set;
        }


        public virtual Laboratory Laboratory {
            get; set;
        }
    }
}
