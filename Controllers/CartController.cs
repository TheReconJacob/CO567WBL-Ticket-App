using CO567WBL_Ticket_App.Data;
using CO567WBL_Ticket_App.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CO567WBL_Ticket_App.Controllers
{
    public class CartController : Controller
    {
        private UserManager<IdentityUser> _userManager;
        private ApplicationDbContext _context;

        public CartController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            List<Cart> CartList = _context.Cart.Where(cart => cart.UserId == _userManager.GetUserId(HttpContext.User)).ToList();
            return View(CartList);
        }

        public IActionResult CartEmpty()
        {
            TempData["cartempty"] = "Your Cart is empty.";
            return View();
        }

        [HttpGet]
        public IActionResult Proceed(Cart cart)
        {
            List<Cart> CartList = _context.Cart.Where(cart => cart.UserId == _userManager.GetUserId(HttpContext.User)).ToList();
            if(CartList.Count == 0)
            {
                return RedirectToAction("cartEmpty", "Cart");
            }
            else
            {
                return View(CartList);
            }
        }
    }
}
