using CO567WBL_Ticket_App.Data;
using CO567WBL_Ticket_App.Models;
using CO567WBL_Ticket_App.Models.ViewModels;
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
        private ApplicationDbContext _context;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<EventDetails> getMovieList = _context.EventDetails.ToList();
            return View(getMovieList);
        }

        public IActionResult BookNow(int Id)
        {
            BookNowViewModel ViewModel = new BookNowViewModel();
            EventDetails item = _context.EventDetails.Where(Event => Event.Id == Id).FirstOrDefault();
            ViewModel.Event_Name = item.Event_Name;
            ViewModel.Event_Date = item.DateAndTime;
            ViewModel.Event_Id = Id;
            return View(ViewModel);
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
