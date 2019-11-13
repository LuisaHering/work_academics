using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Picture
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public virtual User User { get; set; }
    }
}
