﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AprajitaRetails.Data;
using AprajitaRetails.Models;
using AprajitaRetails.Ops.Triggers;
using System.Globalization;

namespace AprajitaRetails.Sales.Expenses.Controllers
{
    [Area ("Sales")]
    public class DailySalesController : Controller
    {
        private readonly AprajitaRetailsContext db;
        CultureInfo c = CultureInfo.GetCultureInfo("In");

        /// <summary>
        /// ProcessAccounts Handle Cash/Bank flow and update related tables
        /// </summary>
        /// <param name="dailySale"></param>
        private void ProcessAccounts(DailySale dailySale)
        {
            if (!dailySale.IsSaleReturn)
            {
                if (!dailySale.IsDue)
                {
                    if (dailySale.PayMode == PayModes.Cash && dailySale.CashAmount > 0)
                    {
                        Utils.UpDateCashInHand(db, dailySale.SaleDate, dailySale.CashAmount);

                    }
                    //TODO: in future make it more robust
                    if (dailySale.PayMode != PayModes.Cash && dailySale.PayMode != PayModes.Coupons && dailySale.PayMode != PayModes.Points)
                    {
                        Utils.UpDateCashInBank(db, dailySale.SaleDate, dailySale.Amount - dailySale.CashAmount);
                    }
                }
                else
                {
                    decimal dueAmt;
                    if (dailySale.Amount != dailySale.CashAmount)
                    {
                        dueAmt = dailySale.Amount - dailySale.CashAmount;
                    }
                    else
                        dueAmt = dailySale.Amount;

                    DuesList dl = new DuesList() { Amount = dueAmt, DailySale = dailySale, DailySaleId = dailySale.DailySaleId };
                    db.DuesLists.Add(dl);

                    if (dailySale.PayMode == PayModes.Cash && dailySale.CashAmount > 0)
                    {
                        Utils.UpDateCashInHand(db, dailySale.SaleDate, dailySale.CashAmount);

                    }
                    //TODO: in future make it more robust
                    if (dailySale.PayMode != PayModes.Cash && dailySale.PayMode != PayModes.Coupons && dailySale.PayMode != PayModes.Points)
                    {
                        Utils.UpDateCashInBank(db, dailySale.SaleDate, dailySale.Amount - dailySale.CashAmount);
                    }
                }
            }
            else
            {
                if (dailySale.PayMode == PayModes.Cash && dailySale.CashAmount > 0)
                {
                    Utils.UpDateCashOutHand(db, dailySale.SaleDate, dailySale.CashAmount);

                }
                //TODO: in future make it more robust
                if (dailySale.PayMode != PayModes.Cash && dailySale.PayMode != PayModes.Coupons && dailySale.PayMode != PayModes.Points)
                {
                    Utils.UpDateCashOutBank(db, dailySale.SaleDate, dailySale.Amount - dailySale.CashAmount);
                }
                dailySale.Amount = 0 - dailySale.Amount;

            }

        }

        private void ProcessAccountDelete(DailySale dailySale)
        {
            if (dailySale.PayMode == PayModes.Cash && dailySale.CashAmount > 0)
            {
                Utils.UpDateCashInHand(db, dailySale.SaleDate, 0 - dailySale.CashAmount);
            }
            else if (dailySale.PayMode != PayModes.Cash && dailySale.PayMode != PayModes.Coupons && dailySale.PayMode != PayModes.Points)
            {
                Utils.UpDateCashInBank(db, dailySale.SaleDate, 0 - dailySale.CashAmount);
            }
            else
            {
                //TODO: Add this option in Create and Edit also
                // Handle when payment is done by Coupons and Points. 
                //Need to create table to create Coupn and Royalty point.
                // Points will go in head for Direct Expenses 
                // Coupon Table will be colloum for TAS Coupon and Apajita Retails. 
                //TODO: Need to handle is. 
                // If payment is cash and cashamount is zero then need to handle this option also 
                // may be error entry , might be due.
                throw new Exception();
            }
        }
        public DailySalesController(AprajitaRetailsContext context)
        {
            db = context;
        }

        // GET: DailySales
        public async Task<IActionResult> Index(int? id, string salesmanId, string searchString, DateTime? SaleDate)
        {
            var dailySales = db.DailySales.Include(d => d.Salesman).Where(c => c.SaleDate == DateTime.Today).OrderByDescending(c => c.SaleDate).ThenByDescending(c => c.DailySaleId);
            if (id != null && id == 101)
            {
                dailySales = db.DailySales.Include(d => d.Salesman).OrderByDescending(c => c.SaleDate).ThenByDescending(c => c.DailySaleId);
            }
            
            //Fixed Query
            var totalSale = dailySales.Where(c => c.IsManualBill == false).Sum(c => (decimal?)c.Amount) ?? 0;
            var totalManualSale = dailySales.Where(c => c.IsManualBill == true).Sum(c => (decimal?)c.Amount) ?? 0;
            var totalMonthlySale = db.DailySales.Where(c => c.SaleDate.Month == DateTime.Today.Month).Sum(c => (decimal?)c.Amount) ?? 0;
            var duesamt = db.DuesLists.Where(c => c.IsRecovered == false).Sum(c => (decimal?)c.Amount) ?? 0;

            var cashinhand = (decimal)0.00;
            try
            {
                cashinhand = db.CashInHands.Where(c =>c.CIHDate ==DateTime.Today).FirstOrDefault().InHand;
            }
            catch (Exception)
            {
               // Utils.ProcessOpenningClosingBalance(db, DateTime.Today, false, true);
                new CashWork().Process_OpenningBalance(db, DateTime.Today, true);
                cashinhand = (decimal)0.00;
                //Log.Error("Cash In Hand is null");
            }


            // Fixed UI
            ViewBag.TodaySale = totalSale;
            ViewBag.ManualSale = totalManualSale;
            ViewBag.MonthlySale = totalMonthlySale;
            ViewBag.DuesAmount = duesamt;
            ViewBag.CashInHand = cashinhand;

            // By Salesman
            var salesmanList = new List<string>();
            var smQry = from d in db.Salesmen
                        orderby d.SalesmanName
                        select d.SalesmanName;
            salesmanList.AddRange(smQry.Distinct());
            ViewBag.salesmanId = new SelectList(salesmanList);

            //By Date

            var dateList = new List<DateTime>();
            var opdQry = from d in db.DailySales
                         orderby d.SaleDate
                         select d.SaleDate;
            dateList.AddRange(opdQry.Distinct());
            ViewBag.dateID = new SelectList(dateList);

            //By Invoice No Search

            if (!String.IsNullOrEmpty(searchString))
            {
                var dls = db.DailySales.Include(d => d.Salesman).Where(c => c.InvNo == searchString);
                return View(dls.ToListAsync());

            }
            else if (!String.IsNullOrEmpty(salesmanId) || SaleDate != null)
            {
                IEnumerable<DailySale> DailySales;

                if (SaleDate != null)
                {
                    DailySales = db.DailySales.Include(d => d.Salesman).Where(c => (c.SaleDate) == (SaleDate)).OrderByDescending(c => c.DailySaleId);
                }
                else
                {
                    DailySales = db.DailySales.Include(d => d.Salesman).Where(c => (c.SaleDate) == (DateTime.Today)).OrderByDescending(c => c.SaleDate).ThenByDescending(c => c.DailySaleId);
                }

                if (!String.IsNullOrEmpty(salesmanId))
                {
                    DailySales = DailySales.Where(c => c.Salesman.SalesmanName == salesmanId);
                }

                return View(DailySales);

            }



            return View(dailySales.ToListAsync());


            //OrignalCode
            var aprajitaRetailsContext = db.DailySales.Include(d => d.Salesman);
            return View(await aprajitaRetailsContext.ToListAsync());
        }

        // GET: DailySales/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dailySale = await db.DailySales
                .Include(d => d.Salesman)
                .FirstOrDefaultAsync(m => m.DailySaleId == id);
            if (dailySale == null)
            {
                return NotFound();
            }

            return PartialView(dailySale);
        }

        // GET: DailySales/Create
        public IActionResult Create()
        {
            ViewData["SalesmanId"] = new SelectList(db.Salesmen, "SalesmanId", "SalesmanId");
            return PartialView();
        }

        // POST: DailySales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DailySaleId,SaleDate,InvNo,Amount,PayMode,CashAmount,SalesmanId,IsDue,IsManualBill,IsTailoringBill,IsSaleReturn,Remarks")] DailySale dailySale)
        {
            if (ModelState.IsValid)
            {
                db.Add(dailySale);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SalesmanId"] = new SelectList(db.Salesmen, "SalesmanId", "SalesmanId", dailySale.SalesmanId);
            return PartialView(dailySale);
        }

        // GET: DailySales/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dailySale = await db.DailySales.FindAsync(id);
            if (dailySale == null)
            {
                return NotFound();
            }
            ViewData["SalesmanId"] = new SelectList(db.Salesmen, "SalesmanId", "SalesmanId", dailySale.SalesmanId);
            return PartialView(dailySale);
        }

        // POST: DailySales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DailySaleId,SaleDate,InvNo,Amount,PayMode,CashAmount,SalesmanId,IsDue,IsManualBill,IsTailoringBill,IsSaleReturn,Remarks")] DailySale dailySale)
        {
            if (id != dailySale.DailySaleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(dailySale);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DailySaleExists(dailySale.DailySaleId))
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
            ViewData["SalesmanId"] = new SelectList(db.Salesmen, "SalesmanId", "SalesmanId", dailySale.SalesmanId);
            return PartialView(dailySale);
        }

        // GET: DailySales/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dailySale = await db.DailySales
                .Include(d => d.Salesman)
                .FirstOrDefaultAsync(m => m.DailySaleId == id);
            if (dailySale == null)
            {
                return NotFound();
            }

            return PartialView(dailySale);
        }

        // POST: DailySales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dailySale = await db.DailySales.FindAsync(id);
            db.DailySales.Remove(dailySale);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DailySaleExists(int id)
        {
            return db.DailySales.Any(e => e.DailySaleId == id);
        }
    }
}