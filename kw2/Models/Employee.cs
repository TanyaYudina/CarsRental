using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace kw2.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        public String Name { get; set; }
        public System.DateTime StartWork { get; set; }

        public virtual ICollection<CarRental> CarsRental { get; set; }
    }
}
