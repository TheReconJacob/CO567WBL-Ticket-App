using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using CO567WBL_Ticket_App.Models;
using Microsoft.AspNetCore.Authorization;

namespace CO567WBL_Ticket_App.Controllers
{
    [Authorize(Roles = "Agent,Venue Manager")]
    public class AssignRoleController : Controller
    {
        private UserManager<IdentityUser> userManager;

        public AssignRoleController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProjectRole Role)
        {
            var Assignee = await userManager.FindByEmailAsync(Role.Email);
            await userManager.AddToRoleAsync(Assignee, Role.RoleName);
            return View();
        }
    }
}
