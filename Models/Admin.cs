using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Test.Models
{
    public class Admin
    {
        [Column("AdminId")]
        public int Id { get; set; }
        public string Name { get; set; }
        public string  Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string AdminDegree { get; set; }

        public ICollection<News> News { get; set; }

        public ICollection<Comment> comments { get; set; }
        public ICollection<Post> posts { get; set; }



    }
}
