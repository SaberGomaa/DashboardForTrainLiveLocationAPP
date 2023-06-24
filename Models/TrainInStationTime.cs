using Microsoft.Build.Framework;

namespace Test.Models
{
    public class TrainInStationTime
    {
        [Required]
        public int StationId { get; set; }
        [Required]
        public int TrainId { get; set; }
        [Required]
        public double TrainTime { get; set; }

    }
}
