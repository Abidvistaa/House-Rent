using HouseRent.Data;
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
    public class DetailOfDuplexController : Controller
    {
        private ApplicationDbContext _db;
        private IWebHostEnvironment _he;
        public DetailOfDuplexController(ApplicationDbContext db, IWebHostEnvironment he)
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
            var duplex = _db.DetailOfDuplexs.ToList();
            return Json(new { data = duplex });
        }

        public IActionResult Upsert(int? id)
        {
            DetailOfDuplex detailOfDuplex = new DetailOfDuplex();
            if (id == null)
            {
                //This is for create
                return View(detailOfDuplex);
            }
            else
            {
                //This is for edit
                var obj = _db.DetailOfDuplexs.FirstOrDefault(x => x.Id == id);
                return View(obj);
            }
            
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(DetailOfDuplex detailOfDuplex,int id)
        {

            if (ModelState.IsValid)
            {
                string webRootPath = _he.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    var uploads = Path.Combine(webRootPath, @"images");
                    string filename = Guid.NewGuid().ToString();
                    var extension = Path.GetExtension(files[0].FileName);

                    if (detailOfDuplex.Photo != null)
                    {
                        //this is an edit and we need to remove old image

                        var imagePath = Path.Combine(webRootPath, detailOfDuplex.Photo.TrimStart('\\'));
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }
                    using (var fileStreams = new FileStream(Path.Combine(uploads, filename + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStreams);
                    }
                    detailOfDuplex.Photo = @"\images\" + filename + extension;
                }
                else
                {
                    //Update when they do not change the image
                    if (detailOfDuplex.Id != 0)
                    {
                        var objFromDB = _db.DetailOfDuplexs.Where(x => x.Id == id).FirstOrDefault<DetailOfDuplex>();
                        detailOfDuplex.Photo = objFromDB.Photo;
                    }
                }


                if (detailOfDuplex.Id == 0)
                {
                    _db.DetailOfDuplexs.Add(detailOfDuplex);
                    TempData["save"] = "Saved successfully";
                }
                else
                {
                    _db.DetailOfDuplexs.Update(detailOfDuplex);
                    TempData["edit"] = "Updated successfully";
                }
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(detailOfDuplex);
        }

       

        [HttpDelete]

        public ActionResult Delete(int id)
        {
            var objFromDB = _db.DetailOfDuplexs.Where(x => x.Id == id).FirstOrDefault<DetailOfDuplex>();
            string webRootPath = _he.WebRootPath;
            var imagePath = Path.Combine(webRootPath, objFromDB.Photo.TrimStart('\\'));
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
            _db.DetailOfDuplexs.Remove(objFromDB);
            _db.SaveChanges();
            return Json(new { success = true, message = "Deleted Successfully" });
        }
    }
}
