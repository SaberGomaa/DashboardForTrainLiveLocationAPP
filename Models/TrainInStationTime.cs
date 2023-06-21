using System.ComponentModel.DataAnnotations.Schema;

namespace Entites.Models
{
    public class TrainInStationTime
    {
        public int StationId { get; set; }
        public int TrainId { get; set; }
        public double TrainTime { get; set; }

    }
}
