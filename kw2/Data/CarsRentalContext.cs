using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace kw2.Models
{
    public class CarsRentalContext : IdentityDbContext<User>
    {
        public CarsRentalContext(DbContextOptions<CarsRentalContext> options) : base(options)
        {
        }

        public DbSet<AdditionalService> AdditionalServices { get; set; }
        public DbSet<CarRental> CarsRental { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<User> User { get; set; }
    }
}
