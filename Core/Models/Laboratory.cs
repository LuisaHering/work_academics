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

        [ForeignKey("User")]
        public Guid IdUser {
            get; set;
        }

        public virtual User User {
            get; set;
        }
    }
}
