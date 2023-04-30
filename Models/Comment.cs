using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Test.Models
{
    public class Comment
    {
        [Column("CommentId")]
        public int Id { get; set; }
        public string Content { get; set; }
        public string Img { get; set; }
        public DateTime? Date { get; set; } = DateTime.Now;

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public virtual User User { get; set; }


        [ForeignKey(nameof(Post))]
        public int PostId { get; set; }
        public virtual Post Post { get; set; }
            
        [ForeignKey(nameof(Admin))]
        public int? AdminId { get; set; }
        public virtual Admin Admin { get; set; }
    }
}
