using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project5EMDAStaffManagement.Data;
using Project5EMDAStaffManagement.Models;
using Project5EMDAStaffManagement.ViewModels;

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
            var today = DateTime.Today;
            List<SignOuts> StaffOut = new List<SignOuts>();
            StaffOut.AddRange(_context.SignOuts
                .Include(s => s.Staff)
                .Include(r => r.Reason)
                .OrderBy(s => s.Staff)
                .Where(s => s.Day.Date == today)
                .ToList());

            ViewData["StaffOut"] = StaffOut;
            return View();
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
            ViewData["Staff"] = _context.Staff.Distinct()
                .OrderBy(n => n.FirstName)
                .Select(n => new SelectListItem
                {
                    Value = n.Id.ToString(),
                    Text = n.FirstName + " " + n.LastName
                }).ToList();

            ViewData["StaffIn"] = _context.Staff.Distinct()
                .OrderBy(n => n.Id)
                .Select(n => new SelectListItem()
                {
                    Value = n.Id.ToString(),
                    Text = n.In.ToString()
                }).ToList();

            ViewData["Reasons"] = _context.Reasons.Distinct()
                .OrderByDescending(n => n.ReasonCount)
                .Select(n => new SelectListItem()
                {
                    Value = n.Id.ToString(),
                    Text = n.Reason
                }).ToList();

            return View();
        }

        // POST: SignOuts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Day,TimeOut,HoursIn,Reason,Staff,StaffIn")] CreateSignOutVM createSignOutVM)
        {
            if (ModelState.IsValid)
            {
                
                // staff is signing in
                if (createSignOutVM.StaffIn == true)
                {
                    // update sign outs table
                    SignOuts signOuts = new SignOuts();
                    signOuts.Day = createSignOutVM.Day;
                    signOuts.TimeOut = createSignOutVM.TimeOut;
                    signOuts.HoursIn = 8;
                    signOuts.Reason = createSignOutVM.Reason;
                    signOuts.Staff = createSignOutVM.Staff;
                    _context.Add(signOuts);

                    await _context.SaveChangesAsync();
                    return Redirect("~/Home/Index");
                }
                // staff is signing out
                else
                {
                    createSignOutVM.TimeOut = DateTime.Now;
                    createSignOutVM.Day = DateTime.Now;

                    SignOuts signOuts = new SignOuts();
                    signOuts.Day = createSignOutVM.Day;
                    signOuts.TimeOut = createSignOutVM.TimeOut;
                    signOuts.HoursIn = 8;
                    signOuts.Reason = createSignOutVM.Reason;
                    signOuts.Staff = createSignOutVM.Staff;

                    // update sign outs table
                    _context.Add(signOuts);

                    await _context.SaveChangesAsync();
                    return Redirect("~/Home/Index");
                }
            }
            return NotFound();
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
