﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AprajitaRetails.Data;
using AprajitaRetails.Models;

namespace AprajitaRetails.Controllers
{
    public class TalioringBookingsController : Controller
    {
        private readonly AprajitaRetailsContext _context;

        public TalioringBookingsController(AprajitaRetailsContext context)
        {
            _context = context;
        }

        // GET: TalioringBookings
        public async Task<IActionResult> Index()
        {
            return View(await _context.Bookings.ToListAsync());
        }

        // GET: TalioringBookings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var talioringBooking = await _context.Bookings
                .FirstOrDefaultAsync(m => m.TalioringBookingId == id);
            if (talioringBooking == null)
            {
                return NotFound();
            }

            return View(talioringBooking);
        }

        // GET: TalioringBookings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TalioringBookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TalioringBookingId,BookingDate,CustName,DeliveryDate,TryDate,BookingSlipNo,TotalAmount,TotalQty,ShirtQty,ShirtPrice,PantQty,PantPrice,CoatQty,CoatPrice,KurtaQty,KurtaPrice,BundiQty,BundiPrice,Others,OthersPrice,IsDelivered")] TalioringBooking talioringBooking)
        {
            if (ModelState.IsValid)
            {
                _context.Add(talioringBooking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(talioringBooking);
        }

        // GET: TalioringBookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var talioringBooking = await _context.Bookings.FindAsync(id);
            if (talioringBooking == null)
            {
                return NotFound();
            }
            return View(talioringBooking);
        }

        // POST: TalioringBookings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TalioringBookingId,BookingDate,CustName,DeliveryDate,TryDate,BookingSlipNo,TotalAmount,TotalQty,ShirtQty,ShirtPrice,PantQty,PantPrice,CoatQty,CoatPrice,KurtaQty,KurtaPrice,BundiQty,BundiPrice,Others,OthersPrice,IsDelivered")] TalioringBooking talioringBooking)
        {
            if (id != talioringBooking.TalioringBookingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(talioringBooking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TalioringBookingExists(talioringBooking.TalioringBookingId))
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
            return View(talioringBooking);
        }

        // GET: TalioringBookings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var talioringBooking = await _context.Bookings
                .FirstOrDefaultAsync(m => m.TalioringBookingId == id);
            if (talioringBooking == null)
            {
                return NotFound();
            }

            return View(talioringBooking);
        }

        // POST: TalioringBookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var talioringBooking = await _context.Bookings.FindAsync(id);
            _context.Bookings.Remove(talioringBooking);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TalioringBookingExists(int id)
        {
            return _context.Bookings.Any(e => e.TalioringBookingId == id);
        }
    }
}