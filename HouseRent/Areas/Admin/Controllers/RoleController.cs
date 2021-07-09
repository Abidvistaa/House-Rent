﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRent.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class RoleController : Controller
    { 
        RoleManager<IdentityRole> _rm;
        public RoleController(RoleManager<IdentityRole> rm)
        {
            _rm = rm;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetAll()
        {
            var role = _rm.Roles.ToList();
            return Json(new { data = role });
        }
        public IActionResult Create()
        {
            return View();
        }
        public async Task<IActionResult> Edit(string id)
        {
            var role = await _rm.FindByIdAsync(id);
            ViewBag.name = role.Name;
            return View(role);
        }

        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole();
                role.Name = name;
                var isExist = await _rm.RoleExistsAsync(role.Name);
                if (isExist == true)
                {
                    ViewBag.mgs = "This Role Already Exist";
                    return View();
                }
                var result = await _rm.CreateAsync(role);
                if (result.Succeeded)
                {

                    TempData["save"] = "Role has been created Successfully";
                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string id, string name)
        {
            var role = await _rm.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            role.Name = name;
            var isExist = await _rm.RoleExistsAsync(role.Name);
            if (isExist == true)
            {
                ViewBag.mgs = "This Role Already Exist";
                return View();
            }
            var result = await _rm.UpdateAsync(role);
            if (result.Succeeded)
            {

                TempData["edit"] = "Role has been updated Successfully";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            var role = await _rm.FindByIdAsync(id);
            await _rm.DeleteAsync(role);
            return Json(new { success = true, message = "Deleted Successfully" });
        }


    }
}

