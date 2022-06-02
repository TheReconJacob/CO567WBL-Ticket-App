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
    public class AddRoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public AddRoleController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
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
            var RoleExist = await roleManager.RoleExistsAsync(Role.RoleName);
            if (!RoleExist)
            {
                var result = await roleManager.CreateAsync(new IdentityRole(Role.RoleName));
            }
            return View();
        }
    }
}
