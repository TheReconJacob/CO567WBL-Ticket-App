using CO567WBL_Ticket_App.Data;
using CO567WBL_Ticket_App.Models;
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
            List<CO567WBL_Ticket_App.Models.EventDetails> getMovieList = _context.EventDetails.ToList();
            return View(getMovieList);
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
