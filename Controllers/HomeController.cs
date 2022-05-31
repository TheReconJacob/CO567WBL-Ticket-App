using CO567WBL_Ticket_App.Data;
using CO567WBL_Ticket_App.Models;
using CO567WBL_Ticket_App.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CO567WBL_Ticket_App.Controllers
{
    public class HomeController : Controller
    {
        int count = 1;
        bool Flag = true;
        private UserManager<IdentityUser> _userManager;
        private ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            List<EventDetails> getMovieList = _context.EventDetails.ToList();
            return View(getMovieList);
        }

        [HttpGet]
        public IActionResult BookNow(int Id)
        {
            BookNowViewModel ViewModel = new BookNowViewModel();
            EventDetails item = _context.EventDetails.Where(Event => Event.Id == Id).FirstOrDefault();
            ViewModel.Event_Name = item.Event_Name;
            ViewModel.Event_Date = item.DateAndTime;
            ViewModel.Event_Id = Id;
            return View(ViewModel);
        }
        [HttpPost]
        public IActionResult BookNow(BookNowViewModel ViewModel)
        {
            List<BookingTable> Bookings = new List<BookingTable>();
            List<Cart> Carts = new List<Cart>();
            string Seat_No = ViewModel.Seat_No.ToString();
            int EventId = ViewModel.Event_Id;
            string[] Seat_No_Array = Seat_No.Split(',');
            count = Seat_No_Array.Length;
            if(CheckSeat(Seat_No, EventId) == false)
            {
                foreach(string item in Seat_No_Array)
                {
                    Carts.Add(new Cart { Amount = 150, EventId = ViewModel.Event_Id, UserId = _userManager.GetUserId(HttpContext.User), Date = ViewModel.Event_Date, Seat_No = item });
                };
                foreach (var item in Carts)
                {
                    _context.Cart.Add(item);
                    _context.SaveChanges();
                }
                TempData["Success"]="Seat has been Booked, Check Your Cart";
            }
            else
            {
                TempData["SeatNoMsg"] = "Seat is no longer available, please choose another";
            }
            return RedirectToAction("BookNow");
        }

        private bool CheckSeat(string seat_No, int event_Id)
        {
            string[] Seat_Reserve = seat_No.Split(',');
            List<BookingTable> Seat_No_List = _context.BookingTable.Where(Event => Event.EventDetailsId == event_Id).ToList();
            foreach (BookingTable item in Seat_No_List)
            {
                string AlreadyBooked = item.Seat_No;
                foreach (var Seat in Seat_Reserve)
                {
                    if(Seat == AlreadyBooked)
                    {
                        Flag = false;
                        break;
                    }
                }
            }
            if (Flag == false)
                return true;
            else
                return false;
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
