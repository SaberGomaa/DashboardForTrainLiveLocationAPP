using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Test.Models;

namespace Test.Models
{
    public class Station
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Required")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Required")]
        public string? NextStation { get; set; }
        [Required(ErrorMessage = "Required")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Required")]
        public int Position { get; set; }
        [Required(ErrorMessage = "Required")]
        public double Longitude { get; set; }
        [Required(ErrorMessage = "Required")]
        public double Latitude { get; set; }
        [Required(ErrorMessage = "Required")]
        public int? NextStationPostion { get; set; }
        [Required]
        public int? RailwayId { get; set; }
        [Required]
        public bool MainStation { get; set; } = false;
        public ICollection<Ticket>? Tickets { get; set; }

    }
}
