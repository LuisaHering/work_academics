using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SN_WebApi.Models
{
    public class PictureBindingModel
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public virtual User User { get; set; }
    }
}