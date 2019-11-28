using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models {
    public class Project {

        public Project() {
            Posts = new List<Post>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {
            get; set;
        }
        public string Titulo {
            get; set;
        }
        public string Descricao {
            get; set;
        }
        public DateTime DataCriacao {
            get; set;
        }
        public DateTime? DataFinalizacao {
            get; set;
        }

        public virtual Laboratory Laboratory {
            get; set;
        }

        public virtual List<Post> Posts {
            get; set;
        }
    }
}
