using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models {
    public class Role {

        [Key]
        public int Id {
            get; set;
        }

        public string Descricao {
            get; set;
        }

    }
}
