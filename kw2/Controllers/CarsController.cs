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
    public class CarsController : Controller
    {
        private readonly CarsRentalContext _context;

        public CarsController(CarsRentalContext context)
        {
            _context = context;
        }


        [Authorize(Roles = "admin,user")]
        // GET: Cars
        public async Task<IActionResult> Index()
        {
            var carsRentalContext = _context.Cars.Include(c => c.Model);
            return View(await carsRentalContext.ToListAsync());
        }


        [Authorize(Roles = "admin,user")]
        // GET: Cars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .Include(c => c.Model)
                .SingleOrDefaultAsync(m => m.CarId == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }
        [Authorize(Roles = "admin")]
        // GET: Cars/Create
        public IActionResult Create()
        {
            ViewData["ModelId"] = new SelectList(_context.Models, "ModelId", "Name");
            return View();
        }


        [Authorize(Roles = "admin")]
        // POST: Cars/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CarId,ModelId,RegistrationNumber,CarNumber,EngineNumber,DateOfIssue,Mileage,DayRentalCar")] Car car)
        {
            if (ModelState.IsValid)
            {
                _context.Add(car);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ModelId"] = new SelectList(_context.Models, "ModelId", "ModelId", car.ModelId);
            return View(car);
        }

        [Authorize(Roles = "admin")]
        // GET: Cars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars.SingleOrDefaultAsync(m => m.CarId == id);
            if (car == null)
            {
                return NotFound();
            }
            ViewData["ModelId"] = new SelectList(_context.Models, "ModelId", "Name");
            return View(car);
        }

        [Authorize(Roles = "admin")]
        // POST: Cars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CarId,ModelId,RegistrationNumber,CarNumber,EngineNumber,DateOfIssue,Mileage,DayRentalCar")] Car car)
        {
            if (id != car.CarId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(car);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(car.CarId))
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
            ViewData["ModelId"] = new SelectList(_context.Models, "ModelId", "ModelId", car.ModelId);
            return View(car);
        }
        [Authorize(Roles = "admin")]
        // GET: Cars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Cars
                .Include(c => c.Model)
                .SingleOrDefaultAsync(m => m.CarId == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }
        [Authorize(Roles = "admin")]
        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var car = await _context.Cars.SingleOrDefaultAsync(m => m.CarId == id);
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarExists(int id)
        {
            return _context.Cars.Any(e => e.CarId == id);
        }
    }
}
