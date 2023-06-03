using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.Models
{
    public class Admin
    {
        [Column("AdminId")]
        public int Id { get; set; }
        
        [Required(ErrorMessage ="Required")]
        [MinLength(3, ErrorMessage = "Name Length at least 3 Characters")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Required")]
        [RegularExpression("^([a-zA-Z0-9_\\-\\.]+)@([a-zA-Z0-9_\\-\\.]+)\\.([a-zA-Z]{2,5})$", ErrorMessage = "Not Vaild Email")]
        public string  Email { get; set; }
        
        [Required(ErrorMessage ="Required")]
        [RegularExpression("^01[0125][0-9]{8}$", ErrorMessage = "Not Vaild Phone Number")]

        public string Phone { get; set; }

        [Required(ErrorMessage ="Required")]
        public string Password { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Required")]
        [Compare("Password", ErrorMessage ="Not Matched Passwored")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Required")]
        public string AdminDegree { get; set; }

        public bool FirstTime { get; set; }

        public ICollection<News> News { get; set; }

        public ICollection<Comment> comments { get; set; }
        public ICollection<Post> posts { get; set; }



    }
}
