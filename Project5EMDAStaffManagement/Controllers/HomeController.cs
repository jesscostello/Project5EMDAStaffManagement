﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Internal;
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

            var today = DateTime.Today;
            List<SignOuts> StaffOut = new List<SignOuts>();
            StaffOut.AddRange(_context.SignOuts
                .OrderBy(s => s.Staff)
                .Where(s => s.Day.Date == today)
                .ToList());

            //StaffOut.AddRange(_context.SignOuts.Join(_context.Staff, s => s.StaffName, n => n.Id, (s,n) => new {}));

            //StaffOut.AddRange(_context.SignOuts.Join(_context.Staff,
                    //SignOuts => new {Id = SignOuts.StaffNameId},
                  //  Staff => new {Staff.Id},
                   // (SignOuts, Staff) => new {StaffName = Staff})
               // .ToList());

            ViewData["StaffOut"] = StaffOut;

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            //ViewData["Staff"] = _context.Staff.Distinct()
            //        .OrderBy(n => n.FirstName)
            //        .Select(n => new SelectListItem
            //        {
            //            Value = n.Id.ToString(),
            //            Text = n.FirstName + " " + n.LastName
            //        }).ToList();

            return View();
        }

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
