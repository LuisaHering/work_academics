using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SN_WebApi.Models
{
    public class UpdateBindingModel
    {
        public string Email { get; set; }
        public string Nome { get; set; }
        public string Biografia { get; set; }
        public string Universidade { get; set; }
        public string Curso { get; set; }
    }
}