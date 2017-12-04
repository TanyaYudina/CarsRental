using kw2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kw2.Data
{
    public static class DbInitializer
    {

        public static void Initialize(CarsRentalContext db)
        {
            db.Database.EnsureCreated();

            if (db.CarsRental.Any())
            {
                return;
            }
            Random rnd = new Random();

            for (int i = 1; i < 10; i++)
                db.AdditionalServices.Add(new AdditionalService { Name = "Имя " + rnd.Next(999), Description = "Описание " + rnd.Next(999), Price = rnd.Next(999) });
            db.SaveChanges();

            for (int i = 1; i < 10; i++)
                db.Models.Add(new Model { Name = "Название " + rnd.Next(999), Specification = "Спецификация " + rnd.Next(999), Description = "Описание " + rnd.Next(999) });
            db.SaveChanges();

            for (int i = 1; i < 10; i++)
                db.Cars.Add(new Car { ModelId = i, RegistrationNumber = rnd.Next(100000, 999999), CarNumber = rnd.Next(100000, 999999), EngineNumber = rnd.Next(100000, 999999), DateOfIssue = new DateTime(2000, 10, 10), Mileage = rnd.Next(999), DayRentalCar = rnd.Next(99) });
            db.SaveChanges();

            for (int i = 1; i < 10; i++)
                db.Customers.Add(new Customer { Name = "Имя " + rnd.Next(999), Adress = "Адрес " + rnd.Next(999), PhoneNumber = "Паспорт " + rnd.Next(999), PassportData = rnd.Next(1000000, 9999999), DateOfBirth = new DateTime(1990,i,i) });
            db.SaveChanges();

            for (int i = 1; i < 10; i++)
                db.Employees.Add(new Employee { Name = "Имя " + rnd.Next(999), StartWork = new DateTime(2010, 03, 03) });
            db.SaveChanges();

            for (int i = 1; i < 10; i++)
                db.CarsRental.Add(new CarRental { CustomerId = i, ServiceId = i, EmployeeId = i, CarId = i, DateOfIssue = new DateTime(2015, i, i), RentalPeriod = rnd.Next(99), ReturnDate = new DateTime(2015, i + 1, i + 1), PriceRental = rnd.Next(99), PaymentNote = rnd.Next(99) });
            db.SaveChanges();
        }
    }
}