using CO567WBL_Ticket_App.Data;
using CO567WBL_Ticket_App.Models;
using CO567WBL_Ticket_App.Models.ViewModels;
using FileUploadControl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CO567WBL_Ticket_App.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext _context;
        private UploadInterface _upload;

        public AdminController(ApplicationDbContext context, UploadInterface upload)
        {
            _upload = upload;
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(IList<IFormFile> Files, EventDetailViewModel ViewModel, EventDetails Event)
        {
            Event.Event_Name = ViewModel.Name;
            Event.Event_Description = ViewModel.Description;
            Event.DateAndTime = ViewModel.DateOfMovie;
            foreach(IFormFile File in Files)
            {
                Event.EventPicture = "~/uploads/" + File.FileName.Trim();
            }
            _upload.UploadFileMultiple(Files);
            _context.EventDetails.Add(Event);
            _context.SaveChanges();
            TempData["Success"] = "Your Event has been saved!";
            return RedirectToAction("Create","Admin");
        }
        [HttpGet]
        public IActionResult CheckBookSeat()
        {
            var GetBookingTable = _context.BookingTable.ToList().OrderByDescending(a => a.DateToPresent);
            return View(GetBookingTable);
        }
        [HttpGet]
        public IActionResult GetUserDetails()
        {
            var GetUserTable = _context.Users.ToList();
            return View(GetUserTable);
        }
    }
}
