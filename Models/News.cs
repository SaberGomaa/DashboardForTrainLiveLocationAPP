using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Test.Models
{
    public class News
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Image is Required")]
        public string Img { get; set; }
        public DateTime? date { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Content is Required")]
        public string ContentOfPost { get; set; }


        [ForeignKey(nameof(Admin))]
        public int? AdminId { get; set; }
        public virtual Admin Admin { get; set; }


    }
}