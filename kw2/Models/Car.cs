using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace kw2.Models
{
    public class Car
    {
        [Key]
        public int CarId { get; set; }
        public int? ModelId { get; set; }
        public int RegistrationNumber { get; set; }
        public int CarNumber { get; set; }
        public int EngineNumber { get; set; }
        public System.DateTime DateOfIssue { get; set; }
        public int Mileage { get; set; }
        public int DayRentalCar { get; set; }

        public virtual ICollection<CarRental> CarsRental { get; set; }
        public virtual Model Model { get; set; }


    }
}
