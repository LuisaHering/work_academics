using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models {
    public class Post {

        public Guid Id {
            get; set;
        }

        public string Mensagem {
            get; set;
        }

        public User Autor {
            get; set;
        }

        public string UrlDocumento {
            get; set;
        }

        public virtual List<Project> Project {
            get; set;
        }

        public virtual Laboratory Laboratory {
            get; set;
        }

        public DateTime DataPublicacao {
            get; set;
        }
    }
}
