using HouseRent.Data;
using HouseRent.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRent.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PropertyTypeController : Controller
    {
        private readonly ApplicationDbContext _db;
        public PropertyTypeController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            
            return View();
        }

        public IActionResult GetAll()
        {
            var propertyTypes = _db.PropertyTypes.ToList();
            return Json(new { data = propertyTypes });
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit(int id)
        {
            var type=_db.PropertyTypes.Find(id);
            return View(type);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PropertyType propertyType)
        {
            if (ModelState.IsValid)
            {
                _db.PropertyTypes.Add(propertyType);
                await _db.SaveChangesAsync();
            }
            TempData["save"] = "Created successfully";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PropertyType propertyType)
        {
            if (ModelState.IsValid)
            {
                _db.PropertyTypes.Update(propertyType);
                await _db.SaveChangesAsync();
            }
            TempData["edit"] = "Updated successfully";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]

        public ActionResult Delete(int id)
        {
            var type = _db.PropertyTypes.Where(x => x.Id == id).FirstOrDefault<PropertyType>();
            _db.Remove(type);
            _db.SaveChanges();
            return Json(new { success=true,message="Deleted Successfully"});
        }
    }
}
