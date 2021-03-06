﻿using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AprajitaRetails.Data;
using AprajitaRetails.Models;
using AprajitaRetails.Ops.Utility;
using Microsoft.AspNetCore.Identity;

namespace AprajitaRetails.Areas.PayRoll.Controllers
{
    [Area ("PayRoll")]
    [Authorize]
    public class EmployeesController : Controller
    {
        private readonly AprajitaRetailsContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public EmployeesController(AprajitaRetailsContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }

        // GET: PayRoll/Employees
        public async Task<IActionResult> Index(string currentFilter, string searchString, int? pageNumber)
        {
            if ( searchString != null )
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData ["CurrentFilter"] = searchString;
            int pageSize = 10;
            return View (await PaginatedList<Employee>.CreateAsync (_context.Employees.AsNoTracking (), pageNumber ?? 1, pageSize));
            //return View(await _context.Employees.ToListAsync());
        }

        // GET: PayRoll/Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if ( id == null )
            {
                return NotFound ();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync (m => m.EmployeeId == id);
            if ( employee == null )
            {
                return NotFound ();
            }

            return PartialView (employee);
        }

        // GET: PayRoll/Employees/Create
        public IActionResult Create()
        {

            return PartialView ();
        }

        // POST: PayRoll/Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind ("EmployeeId,StaffName,MobileNo,JoiningDate,LeavingDate,IsWorking,Category")] Employee employee)
        {
            if ( ModelState.IsValid )
            {
                _context.Add (employee);
                await _context.SaveChangesAsync ();
                if ( employee.Category == EmpType.StoreManager )
                    await UserAdmin.AddUserAsync (_userManager, employee.StaffName, true);
                else
                    await UserAdmin.AddUserAsync (_userManager, employee.StaffName, false);
                //TODO: Implement add employee level security and permissions 

                return RedirectToAction (nameof (Index));
            }
            return PartialView (employee);
        }

        // GET: PayRoll/Employees/Edit/5
        [Authorize (Roles = "Admin,PowerUser")]
        public async Task<IActionResult> Edit(int? id)
        {
            if ( id == null )
            {
                return NotFound ();
            }

            var employee = await _context.Employees.FindAsync (id);
            if ( employee == null )
            {
                return NotFound ();
            }
            return PartialView (employee);
        }

        // POST: PayRoll/Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize (Roles = "Admin,PowerUser")]
        public async Task<IActionResult> Edit(int id, [Bind ("EmployeeId,StaffName,MobileNo,JoiningDate,LeavingDate,IsWorking,Category")] Employee employee)
        {
            if ( id != employee.EmployeeId )
            {
                return NotFound ();
            }

            if ( ModelState.IsValid )
            {
                try
                {
                    _context.Update (employee);
                    await _context.SaveChangesAsync ();
                }
                catch ( DbUpdateConcurrencyException )
                {
                    if ( !EmployeeExists (employee.EmployeeId) )
                    {
                        return NotFound ();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction (nameof (Index));
            }
            return PartialView (employee);
        }

        // GET: PayRoll/Employees/Delete/5
        [Authorize (Roles = "Admin,PowerUser")]
        public async Task<IActionResult> Delete(int? id)
        {
            if ( id == null )
            {
                return NotFound ();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync (m => m.EmployeeId == id);
            if ( employee == null )
            {
                return NotFound ();
            }

            return PartialView (employee);
        }

        // POST: PayRoll/Employees/Delete/5
        [HttpPost, ActionName ("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize (Roles = "Admin,PowerUser")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employees.FindAsync (id);
            _context.Employees.Remove (employee);
            await _context.SaveChangesAsync ();
            return RedirectToAction (nameof (Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any (e => e.EmployeeId == id);
        }
    }
}
