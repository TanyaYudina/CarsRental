using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace kw2.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public System.DateTime DateOfBirth { get; set; }
        public string Adress { get; set; }
        public string PhoneNumber { get; set; }
        public int PassportData { get; set; }
        public virtual ICollection<CarRental> CarsRental { get; set; }
       
    }
}
