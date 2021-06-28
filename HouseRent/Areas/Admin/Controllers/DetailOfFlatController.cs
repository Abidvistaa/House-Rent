﻿using HouseRent.Data;
using HouseRent.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRent.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DetailOfFlatController : Controller
    {
        private ApplicationDbContext _db;
        private IWebHostEnvironment _he;
        public DetailOfFlatController(ApplicationDbContext db, IWebHostEnvironment he)
        {
            _db = db;
            _he = he;
        }
        public IActionResult Index()
        {
            
            return View();
        }

        public IActionResult GetAll()
        {
            var flat = _db.DetailOfFlats.Include(x => x.Flat).ToList<DetailOfFlat>();
            return Json(new { data = flat });
        }

        public ActionResult Create()
        {
            ViewData["floor"] = new SelectList(_db.Flats.ToList(),"Id","Floor");
            return View();
        }

        public ActionResult Edit(int id)
        {
            ViewData["floor"] = new SelectList(_db.Flats.ToList(), "Id", "Floor");
            var type = _db.DetailOfFlats.FirstOrDefault(x => x.Id == id);
            return View(type);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DetailOfFlat detailOfFlat)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    string webRootPath = _he.WebRootPath;
                    var uploads = Path.Combine(webRootPath, @"images");
                    string filename = Guid.NewGuid().ToString();
                    var extension = Path.GetExtension(files[0].FileName);

                    using (var fileStreams = new FileStream(Path.Combine(uploads, filename + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStreams);
                    }
                    detailOfFlat.Photo = @"\images\" + filename + extension;
                    _db.DetailOfFlats.Add(detailOfFlat);
                    _db.SaveChanges();
                    TempData["save"] = "Created successfully";
                    return RedirectToAction(nameof(Index));
                }
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(DetailOfFlat detailOfFlat,int id)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    string webRootPath = _he.WebRootPath;
                    var uploads = Path.Combine(webRootPath, @"images");
                    string filename = Guid.NewGuid().ToString();
                    var extension = Path.GetExtension(files[0].FileName);

                    if (detailOfFlat.Photo != null)
                    {
                        //this is an edit and we need to remove old image
                        var imagePath = Path.Combine(webRootPath, detailOfFlat.Photo.TrimStart('\\'));
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }
                    using (var fileStreams = new FileStream(Path.Combine(uploads, filename + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStreams);
                    }
                    detailOfFlat.Photo = @"\images\" + filename + extension;
                    _db.DetailOfFlats.Update(detailOfFlat);
                    //_db.Entry(detailOfFlat).State = EntityState.Modified;
                    _db.SaveChanges();
                    TempData["edit"] = "Updated successfully";
                    return RedirectToAction(nameof(Index));
                }
                //else
                //{
                //    //Update when they do not change the image
                //    DetailOfFlat objFromDB = _db.DetailOfFlats.Where(x => x.Id == id).FirstOrDefault<DetailOfFlat>();
                //    detailOfFlat.Photo = objFromDB.Photo;
                //}

                
            }
            return View();
        }

        [HttpPost]

        public ActionResult Delete(int id)
        {
            var objFromDB = _db.DetailOfFlats.Where(x => x.Id == id).FirstOrDefault<DetailOfFlat>();
            string webRootPath = _he.WebRootPath;
            var imagePath = Path.Combine(webRootPath, objFromDB.Photo.TrimStart('\\'));
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
            _db.DetailOfFlats.Remove(objFromDB);
            //_db.Remove(objFromDB);
            _db.SaveChanges();
            return Json(new { success = true, message = "Deleted Successfully" });
        }
    }
}
