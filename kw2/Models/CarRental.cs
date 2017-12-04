using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace kw2.Models
{
    public class CarRental
    {
        [Key]
        public int RentId { get; set; }
        public int? CustomerId { get; set; }
        public int? ServiceId { get; set; }
        public int? EmployeeId { get; set; }
        public int? CarId { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateOfIssue { get; set; }
        public int RentalPeriod { get; set; }
        [DataType(DataType.Date)]
        public DateTime ReturnDate { get; set; }
        public int PriceRental { get; set; }
        public int PaymentNote { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual AdditionalService AdditionalService { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Car Car { get; set; }

    }
}
