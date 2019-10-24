using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SN_WebMVC.Models
{
    public class RegisterViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}