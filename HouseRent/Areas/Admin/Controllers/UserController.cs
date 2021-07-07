using HouseRent.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRent.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        UserManager<IdentityUser>_userManager;
        public UserController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                var result = await _userManager.CreateAsync(applicationUser);
                TempData["save"] = "User has been created Successfully";
                return RedirectToAction("Index", "Home", new { area = "Customer" });  
            }
            return View();
        }
    }
}
