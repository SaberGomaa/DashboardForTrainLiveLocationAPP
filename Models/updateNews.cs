using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dashboard.Models
{
    public class updateNews
    {
        public int Id { get; set; }
        public string Img { get; set; }
        public DateTime? date { get; set; } = DateTime.Now;
        public string ContentOfPost { get; set; }
    }
}