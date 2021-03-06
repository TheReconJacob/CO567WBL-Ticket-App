using CO567WBL_Ticket_App.Data;
using CO567WBL_Ticket_App.Models;
using CO567WBL_Ticket_App.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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
        private SignInManager<IdentityUser> _signInManager;
        private ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;

        public HomeController(ApplicationDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var RoleExist = await _roleManager.RoleExistsAsync("Venue Manager");
            string PreviousPage = Request.Headers["Referer"].ToString();
            if (!RoleExist && _signInManager.IsSignedIn(User))
            {
                await _roleManager.CreateAsync(new IdentityRole("Venue Manager"));
                await _roleManager.CreateAsync(new IdentityRole("Agent"));
                await _userManager.AddToRoleAsync(_userManager.GetUserAsync(User).Result, "Venue Manager");
            }
            if (PreviousPage.EndsWith("Register"))
            {
                foreach (var cookie in Request.Cookies.Keys)
                {
                    Response.Cookies.Delete(cookie);
                }
                return RedirectToAction("AccountCreated", "Home");
            }
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
            List<Cart> Carts = new List<Cart>();
            string Seat_No = ViewModel.Seat_No.ToString();
            int EventId = ViewModel.Event_Id;
            DateTime EventDate = _context.EventDetails.Where(Event => Event.Id == ViewModel.Event_Id).FirstOrDefault().DateAndTime;
            Debug.WriteLine("EVENTDATE: " + EventDate);
            string[] Seat_No_Array = Seat_No.Split(',');
            count = Seat_No_Array.Length;
            if(CheckSeat(Seat_No, EventId) == false)
            {
                foreach(string item in Seat_No_Array)
                {
                    Carts.Add(new Cart { Amount = 150, EventId = ViewModel.Event_Id, UserId = _userManager.GetUserId(HttpContext.User), Date = EventDate, Seat_No = item });
                }
                foreach (var item in Carts)
                {
                    _context.Cart.Add(item);
                    _context.SaveChanges();
                }
                TempData["Success"]="Seat has added to your Cart";
            }
            else
            {
                TempData["Success"] = "Seat is no longer available, please choose another";
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
        [HttpPost]
        public IActionResult CheckSeat(DateTime Event_Date, BookNowViewModel BookNow)
        {
            string Seat_No = string.Empty;
            List<BookingTable> Event_List = _context.BookingTable.Where(Table => Table.DateToPresent == Event_Date).ToList();
            if(Event_List != null)
            {
                List<BookingTable> Get_Seat_No = Event_List.Where(Table => Table.EventDetailsId == BookNow.Event_Id).ToList();
                if(Get_Seat_No != null)
                {
                    foreach(BookingTable item in Get_Seat_No)
                    {
                        Seat_No = Seat_No + " " + item.Seat_No.ToString();
                    }
                    TempData["SNO"] = "Seat " + Seat_No + "has already been booked";
                }
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult AccountCreated()
        {
            string PreviousPage = Request.Headers["Referer"].ToString();
            if (PreviousPage.EndsWith("Register") && !_signInManager.IsSignedIn(User))
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult YourBookings()
        {
            var GetBookingTable = _context.BookingTable.ToList();
            return View(GetBookingTable);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
