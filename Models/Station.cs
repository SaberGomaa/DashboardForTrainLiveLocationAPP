using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Test.Models
{
    public class Station
    {
        [Column("StationId")]
        public int Id { get; set; }
        public string Name { get; set; }
        public string NextStation { get; set; }
        public string Description { get; set; }


        [ForeignKey(nameof(Train))]
        public int TrainId { get; set; }
        public virtual Train Train { get; set; }
    }
}
