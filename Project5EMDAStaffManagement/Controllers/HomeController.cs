using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project5EMDAStaffManagement.Data;
using Project5EMDAStaffManagement.Models;

namespace Project5EMDAStaffManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly StaffDbContext _context;

        public HomeController(StaffDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewData["Staff"] = _context.Staff.Distinct()
                    .OrderBy(n => n.FirstName)
                    .Select(n => new SelectListItem
                    {
                        Value = n.Id.ToString(),
                        Text = n.FirstName + " " + n.LastName
                    }).ToList();
            
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            ViewData["Staff"] = _context.Staff.Distinct()
                    .OrderBy(n => n.FirstName)
                    .Select(n => new SelectListItem
                    {
                        Value = n.Id.ToString(),
                        Text = n.FirstName + " " + n.LastName
                    }).ToList();

            return View();
        }

        //[HttpPost]
        //public ActionResult Submit(FormCollection formcollection)
        //{
        //    TempData["Message"] = "Fruit Name: " + formcollection["Text"];
        //    TempData["Message"] += "\\nFruit Id: " + formcollection["Value"]; ;
        //    return RedirectToAction("About");
        //}

        //private void FillStaff(string staff="")
        //{
        //    SelectListItem selectedItem = (from i in Staff
        //                                   where i.Value == staff
        //                                   select i).SingleOrDefault();

        //    if (selectedItem != null)
        //    {
        //        selectedItem.Selected = true;
        //    }

        //    ViewBag.Staff = staff;
        //}



        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
