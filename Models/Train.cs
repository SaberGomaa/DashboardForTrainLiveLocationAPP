using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Test.Models
{
    public class Train
    {
        [Column("TrainId")]
        public int Id { get; set; }
        public string Degree { get; set; }
        public int NumOfSeat { get; set; }
        public string TrainNumber { get; set; }

        public int NumOfTrainCars { get; set; }
        //////
        public string Conductor { get; set; }
        public string Driver { get; set; }
        ///////
        public string CurrentLocation { get; set; }

        public ICollection<LiveLocation> liveLocations { get; set; }

        public ICollection<User> users { get; set; }
        public ICollection<Station> stations { get; set; }
    }
}
