using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SN_WebApi.Models.Follow {
    public class DataToFollowing {

        public string IdSeguidor {
            get; set;
        }

        public string IdSeguido {
            get; set;
        }
    }
}