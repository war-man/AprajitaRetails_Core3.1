﻿using Microsoft.AspNetCore.Authorization;    using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AprajitaRetails.Data;
using AprajitaRetails.Data.Reports;
using AprajitaRetails.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AprajitaRetails.Controllers
{
    public class IncomeExpensesReportController : Controller
    {
        private readonly AprajitaRetailsContext _context;

        public IncomeExpensesReportController(AprajitaRetailsContext context)
        {
            _context = context;
        }

        public IActionResult Index(int? id, DateTime? ondate)
        {
            DateTime onDate = DateTime.Today;
            if (ondate != null)
            {
                onDate = ondate??DateTime.Today;
            }

            IncomeExpensesReport ierData;
            IEReport dM = new IEReport();
            if (id == 1)
            {

                ierData =dM.GetDailyReport(_context, onDate);
            }else if (id == 7)
            {
                ierData =dM.GetWeeklyReport(_context);
            }
            else if (id == 30)
            {
                ierData =dM.GetMonthlyReport(_context, onDate);
            }
            else if (id == 365)
            {
                ierData =dM.GetYearlyReport(_context, onDate);
            }
            else if (id == 600)
            {
                ierData = dM.GetYearlyReport(_context, onDate.AddDays(-365));
            }
            else
            {
                ierData = dM.GetDailyReport(_context, onDate);
            }

           return View(ierData);
        }

        public IActionResult IEReport()
        {
            IEReport dM = new IEReport();
            IERVM data = new IERVM { 

                CurrentWeek=dM.GetWeeklyReport(_context),
                Monthly=dM.GetMonthlyReport(_context,DateTime.Today), 
                Today=dM.GetDailyReport(_context,DateTime.Today),
                Yearly=dM.GetYearlyReport(_context, DateTime.Today)
            };

           return View(data);
        }

    }
}