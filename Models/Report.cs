using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Test.Models
{
    public class Report
    {
        [Column("ReportId")]
        public int Id { get; set; }
        public string  Descreption { get; set; }
        public DateTime Date { get; set; }



        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        [ForeignKey(nameof(Post))]
        public int PostId { get; set; }
        public virtual Post Post{ get; set; }
    }
}
