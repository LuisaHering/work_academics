using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SN_WebMVC.Models
{
    public class PictureViewModel
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public virtual User User { get; set; }
        public bool Ativa { get; set; }
    }
}