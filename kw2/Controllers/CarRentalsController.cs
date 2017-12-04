using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using kw2.Models;
using Microsoft.AspNetCore.Authorization;

namespace kw2.Controllers
{
    public class CarRentalsController : Controller
    {
        private readonly CarsRentalContext _context;

        public CarRentalsController(CarsRentalContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "admin,user")]
        // GET: CarRentals
        public async Task<IActionResult> Index()
        {
            var carsRentalContext = _context.CarsRental.Include(c => c.AdditionalService).Include(c => c.Car).Include(c => c.Customer).Include(c => c.Employee);
            return View(await carsRentalContext.ToListAsync());
        }



        [Authorize(Roles = "admin,user")]
        // GET: CarRentals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carRental = await _context.CarsRental
                .Include(c => c.AdditionalService)
                .Include(c => c.Car)
                .Include(c => c.Customer)
                .Include(c => c.Employee)
                .SingleOrDefaultAsync(m => m.RentId == id);
            if (carRental == null)
            {
                return NotFound();
            }

            return View(carRental);
        }

        [Authorize(Roles = "admin")]
        // GET: CarRentals/Create
        public IActionResult Create()
        {
            ViewData["ServiceId"] = new SelectList(_context.AdditionalServices, "ServiceId", "Name");
            ViewData["CarId"] = new SelectList(_context.Cars, "CarId", "CarNumber");
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Name");
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "Name");
            return View();
        }


        [Authorize(Roles = "admin")]
        // POST: CarRentals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RentId,CustomerId,ServiceId,EmployeeId,CarId,DateOfIssue,RentalPeriod,ReturnDate,PriceRental,PaymentNote")] CarRental carRental)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carRental);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ServiceId"] = new SelectList(_context.AdditionalServices, "ServiceId", "ServiceId", carRental.ServiceId);
            ViewData["CarId"] = new SelectList(_context.Cars, "CarId", "CarId", carRental.CarId);
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", carRental.CustomerId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", carRental.EmployeeId);
            return View(carRental);
        }


        [Authorize(Roles = "admin")]
        // GET: CarRentals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carRental = await _context.CarsRental.SingleOrDefaultAsync(m => m.RentId == id);
            if (carRental == null)
            {
                return NotFound();
            }
            ViewData["ServiceId"] = new SelectList(_context.AdditionalServices, "ServiceId", "Name");
            ViewData["CarId"] = new SelectList(_context.Cars, "CarId", "CarNumber");

            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Name");
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "Name");
            return View(carRental);
        }
        [Authorize(Roles = "admin")]
        // POST: CarRentals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RentId,CustomerId,ServiceId,EmployeeId,CarId,DateOfIssue,RentalPeriod,ReturnDate,PriceRental,PaymentNote")] CarRental carRental)
        {
            if (id != carRental.RentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carRental);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarRentalExists(carRental.RentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ServiceId"] = new SelectList(_context.AdditionalServices, "ServiceId", "ServiceId", carRental.ServiceId);
            ViewData["CarId"] = new SelectList(_context.Cars, "CarId", "CarId", carRental.CarId);
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", carRental.CustomerId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", carRental.EmployeeId);
            return View(carRental);
        }
        [Authorize(Roles = "admin")]
        // GET: CarRentals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carRental = await _context.CarsRental
                .Include(c => c.AdditionalService)
                .Include(c => c.Car)
                .Include(c => c.Customer)
                .Include(c => c.Employee)
                .SingleOrDefaultAsync(m => m.RentId == id);
            if (carRental == null)
            {
                return NotFound();
            }

            return View(carRental);
        }
        [Authorize(Roles = "admin")]
        // POST: CarRentals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carRental = await _context.CarsRental.SingleOrDefaultAsync(m => m.RentId == id);
            _context.CarsRental.Remove(carRental);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarRentalExists(int id)
        {
            return _context.CarsRental.Any(e => e.RentId == id);
        }
    }
}
