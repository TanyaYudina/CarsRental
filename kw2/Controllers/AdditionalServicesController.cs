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
    public class AdditionalServicesController : Controller
    {
        private readonly CarsRentalContext _context;

        public AdditionalServicesController(CarsRentalContext context)
        {
            _context = context;
        }


        [Authorize(Roles = "admin,user")]
        // GET: AdditionalServices
        public async Task<IActionResult> Index()
        {
            return View(await _context.AdditionalServices.ToListAsync());
        }

        [Authorize(Roles = "admin,user")]

        // GET: AdditionalServices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var additionalService = await _context.AdditionalServices
                .SingleOrDefaultAsync(m => m.ServiceId == id);
            if (additionalService == null)
            {
                return NotFound();
            }

            return View(additionalService);
        }


        [Authorize(Roles = "admin")]
        // GET: AdditionalServices/Create
        public IActionResult Create()
        {
            return View();
        }


        [Authorize(Roles = "admin")]
        // POST: AdditionalServices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ServiceId,Name,Description,Price")] AdditionalService additionalService)
        {
            if (ModelState.IsValid)
            {
                _context.Add(additionalService);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(additionalService);
        }
        [Authorize(Roles = "admin")]
        // GET: AdditionalServices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var additionalService = await _context.AdditionalServices.SingleOrDefaultAsync(m => m.ServiceId == id);
            if (additionalService == null)
            {
                return NotFound();
            }



            return View(additionalService);
        }


        [Authorize(Roles = "admin")]
        // POST: AdditionalServices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ServiceId,Name,Description,Price")] AdditionalService additionalService)
        {
            if (id != additionalService.ServiceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(additionalService);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdditionalServiceExists(additionalService.ServiceId))
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
            return View(additionalService);
        }


        [Authorize(Roles = "admin")]
        // GET: AdditionalServices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var additionalService = await _context.AdditionalServices
                .SingleOrDefaultAsync(m => m.ServiceId == id);
            if (additionalService == null)
            {
                return NotFound();
            }

            return View(additionalService);
        }

        [Authorize(Roles = "admin")]
        // POST: AdditionalServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var additionalService = await _context.AdditionalServices.SingleOrDefaultAsync(m => m.ServiceId == id);
            _context.AdditionalServices.Remove(additionalService);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdditionalServiceExists(int id)
        {
            return _context.AdditionalServices.Any(e => e.ServiceId == id);
        }
    }
}
