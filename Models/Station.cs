using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.Models
{
    public class Station
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Required")]
        public string? Name { get; set; }
        [Required(ErrorMessage ="Required")]
        public string? NextStation { get; set; }
        [Required(ErrorMessage ="Required")]
        public string? Description { get; set; }
        [Required(ErrorMessage ="Required")]
        public int Position { get; set; }
        [Required(ErrorMessage ="Required")]
        public double Longitude { get; set; }
        [Required(ErrorMessage ="Required")]
        public double Latitude { get; set; }

        [ForeignKey(nameof(Train))]
        public int TrainId { get; set; }
        public virtual Train? Train { get; set; }
        public ICollection<Ticket>? Tickets { get; set; }

        //public ICollection<User>? Users { get; set; }
    }}
