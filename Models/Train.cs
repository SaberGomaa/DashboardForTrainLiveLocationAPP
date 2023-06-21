using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.Models
{
    public class Train
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Required")]

        public string? Degree { get; set; }
        [Required(ErrorMessage ="Required")]
        public string? TrainNumber { get; set; }
        [Required(ErrorMessage ="Required")]
        public int NumOfSeat { get; set; }
        [Required(ErrorMessage ="Required")]
        public double TrainTime { get; set; }
        [NotMapped]
        public int TrainTimeH { get; set; }
        [NotMapped]
        public int TrainTimeM { get; set; }
        [Required(ErrorMessage ="Required")]
        public int NumOfTrainCars { get; set; }
        //////
        public string? Conductor { get; set; }
        public string? Driver { get; set; }
        ///////
        public string? CurrentLocation { get; set; }
        public int? RailwayId { get; set; }

        public ICollection<LiveLocation> liveLocations { get; set; }

        public ICollection<User>? users { get; set; }
        public ICollection<Station>? stations { get; set; }
        public ICollection<Ticket>? tickets { get; set; }


    }
}
