using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Test.Models
{
    public class Admin
    {
        [Column("AdminId")]
        public int Id { get; set; }
        
        [Required(ErrorMessage ="Required")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Required")]
        public string  Email { get; set; }
        
        [Required(ErrorMessage ="Required")]
        public string Phone { get; set; }

        [Required(ErrorMessage ="Required")]
        public string Password { get; set; }
        
        [Required(ErrorMessage = "Required")]
        public string AdminDegree { get; set; }

        public ICollection<News> News { get; set; }

        public ICollection<Comment> comments { get; set; }
        public ICollection<Post> posts { get; set; }



    }
}
