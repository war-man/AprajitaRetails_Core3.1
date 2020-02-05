﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AprajitaRetails.Areas.Chat.Models;
using AprajitaRetails.Data;
using AprajitaRetails.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AprajitaRetails.Areas.Chat.Controllers
{
    [Authorize]
    [Area ("Chat")]
    public class ARChatController : Controller
    {
        public readonly ApplicationDbContext _context;
        public readonly AprajitaRetailsContext aprajitaRetailsContext;
        public readonly UserManager<IdentityUser> _userManager;

        public ARChatController(AprajitaRetailsContext arCon, ApplicationDbContext context, UserManager<IdentityUser> userManger)
        {
            _context = context;
            aprajitaRetailsContext = arCon;
            _userManager = userManger;
        }
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync (User);
            if ( User.Identity.IsAuthenticated )
            {
                ViewBag.CurrentUserName = currentUser.UserName;
            }
            var messages = await aprajitaRetailsContext.Messages.ToListAsync ();
            return View (messages);
        }
        public async Task<IActionResult> Create(Message message)
        {
            if ( ModelState.IsValid )
            {
                message.UserName = User.Identity.Name;
               var sender = await _userManager.GetUserAsync (User);
                message.UserID = sender.Id;
                await aprajitaRetailsContext.Messages.AddAsync (message);
                await aprajitaRetailsContext.SaveChangesAsync ();
                return Ok ();
            }
            return Error ();
        }
        
        [ResponseCache (Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View (new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



       
       
        
    }
}