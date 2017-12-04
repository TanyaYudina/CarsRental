using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace kw2.Models
{
    public class AdditionalService
    {
        [Key]
        public int ServiceId { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public int Price { get; set; }

        public virtual ICollection<CarRental> CarsRental { get; set; }
    }
}
