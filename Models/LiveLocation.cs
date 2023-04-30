using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Test.Models
{
    public class LiveLocation
    {
        public int Id { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public DateTime DateOfShareLoaction { get; set; }

        [ForeignKey(nameof(Train))]
        public int TrainId { get; set; }
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }

        public Train Train { get; set; }
        public User User { get; set; }

    }
}
