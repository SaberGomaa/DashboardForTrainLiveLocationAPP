using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Test.Models
{
    public class Payment
    {
        [Column("PaymentId")]
        public int Id { get; set; }
        public string BankName { get; set; }
        public int CardNumber { get; set; }
        public double Cost { get; set; }

        public DateTime?  Date { get; set; } = DateTime.Now;

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
