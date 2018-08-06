using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project5EMDAStaffManagement.Data;
using Project5EMDAStaffManagement.Models;

namespace Project5EMDAStaffManagement.Controllers
{
    public class SignOutsController : Controller
    {
        private readonly StaffDbContext _context;

        public SignOutsController(StaffDbContext context)
        {
            _context = context;
        }

        // GET: SignOuts
        public async Task<IActionResult> Index()
        {
            return View(await _context.SignOuts.ToListAsync());
        }

        // GET: SignOuts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var signOuts = await _context.SignOuts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (signOuts == null)
            {
                return NotFound();
            }

            return View(signOuts);
        }

        // GET: SignOuts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SignOuts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Day,TimeOut,HoursIn")] SignOuts signOuts)
        {
            if (ModelState.IsValid)
            {
                _context.Add(signOuts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(signOuts);
        }

        // GET: SignOuts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var signOuts = await _context.SignOuts.FindAsync(id);
            if (signOuts == null)
            {
                return NotFound();
            }
            return View(signOuts);
        }

        // POST: SignOuts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Day,TimeOut,HoursIn")] SignOuts signOuts)
        {
            if (id != signOuts.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(signOuts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SignOutsExists(signOuts.Id))
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
            return View(signOuts);
        }

        // GET: SignOuts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var signOuts = await _context.SignOuts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (signOuts == null)
            {
                return NotFound();
            }

            return View(signOuts);
        }

        // POST: SignOuts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var signOuts = await _context.SignOuts.FindAsync(id);
            _context.SignOuts.Remove(signOuts);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SignOutsExists(int id)
        {
            return _context.SignOuts.Any(e => e.Id == id);
        }
    }
}
