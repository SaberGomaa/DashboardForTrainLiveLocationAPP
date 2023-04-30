using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Test.Models
{
    public class Post
    {
        [Column("PostId")]
        public int Id { get; set; }
        public string Content { get; set; }
        public int TrainNumber { get; set; }
        public bool Critical { get; set; } = false;
        public string Img { get; set; }
        public DateTime date { get; set; } = DateTime.Now;
        public string ImgId { get; set; }
        public string UserName { get; set; }
        public string UserPhone { get; set; }

        public ICollection<Comment> comments { get; set; }
        public ICollection<Report> reports { get; set; }


        [ForeignKey(nameof(Admin))]
        public int? AdminId { get; set; }
        public virtual Admin Admin { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public virtual User User { get; set; }



    }
}
