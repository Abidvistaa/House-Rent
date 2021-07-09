using HouseRent.Data;
using HouseRent.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRent.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class FlatController : Controller
    {
        private readonly ApplicationDbContext _db;
        public FlatController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            
            return View();
        }

        public IActionResult GetAll()
        {
            var flat = _db.Flats.ToList();
            return Json(new { data = flat });
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit(int id)
        {
            var type=_db.Flats.Find(id);
            return View(type);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Flat flat)
        {
            if (ModelState.IsValid)
            {
                _db.Flats.Add(flat);
                await _db.SaveChangesAsync();
            }
            TempData["save"] = "Created successfully";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Flat flat)
        {
            if (ModelState.IsValid)
            {
                _db.Flats.Update(flat);
                await _db.SaveChangesAsync();
            }
            TempData["edit"] = "Updated successfully";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]

        public ActionResult Delete(int id)
        {
            var flat = _db.Flats.Where(x => x.Id == id).FirstOrDefault<Flat>();
            _db.Remove(flat);
            _db.SaveChanges();
            return Json(new { success=true,message="Deleted Successfully"});
        }
    }
}
