using HouseRent.Data;
using HouseRent.Models;
using HouseRent.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRent.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
        public HomeController(ILogger<HomeController> logger,ApplicationDbContext db)
        {
            _db = db;

        }

        public IActionResult Index()
        {
            var obj=_db.DetailOfFlats.ToList<DetailOfFlat>();
            return View(obj);
        }

        public IActionResult Indexx()
        {
            var objD = _db.DetailOfDuplexs.ToList<DetailOfDuplex>();
            return View(objD);
        }
        
        public IActionResult FlatDetails(int? id)
        {
            var flat = _db.DetailOfFlats.Include(x => x.Flat).FirstOrDefault(x=>x.Id==id);
            return View(flat);
        }
        public IActionResult DuplexDetails(int? id)
        {
            var duplex = _db.DetailOfDuplexs.FirstOrDefault(x => x.Id == id);
            return View(duplex);
        }

        [HttpPost]
        public  IActionResult AddBookingSession(int id)
        {
            var flat = _db.DetailOfFlats.Include(x => x.Flat).FirstOrDefault(x => x.Id == id);
            List<DetailOfFlat> detailOfFlats = HttpContext.Session.Get<List<DetailOfFlat>>("flats");
            if (detailOfFlats==null) {
                detailOfFlats = new List<DetailOfFlat>();
            }
            detailOfFlats.Add(flat);
            HttpContext.Session.Set("flats", detailOfFlats);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult AddBookingSessionn(int id)
        {
            var flat = _db.DetailOfDuplexs.FirstOrDefault(x => x.Id == id);
            List<DetailOfDuplex> detailOfDuplexs = HttpContext.Session.Get<List<DetailOfDuplex>>("duplexs");
            if (detailOfDuplexs == null)
            {
                detailOfDuplexs = new List<DetailOfDuplex>();
            }
            detailOfDuplexs.Add(flat);
            HttpContext.Session.Set("duplexs", detailOfDuplexs);
            return RedirectToAction(nameof(Indexx));
        }

        public IActionResult BookingList()
        {
            List<DetailOfFlat> detailOfFlats = HttpContext.Session.Get<List<DetailOfFlat>>("flats");
            return View(detailOfFlats);
        }

        public IActionResult BookingListt()
        {
            List<DetailOfDuplex> detailOfDuplexs = HttpContext.Session.Get<List<DetailOfDuplex>>("duplexs");
            return View(detailOfDuplexs);
        }
        public ActionResult RemoveBooking(int id)
        {
            List<DetailOfFlat> detailOfFlats = HttpContext.Session.Get<List<DetailOfFlat>>("flats");
            DetailOfFlat detailOfFlat = null;
            detailOfFlat = detailOfFlats.FirstOrDefault(c => c.Id == id);
            detailOfFlats.Remove(detailOfFlat);
            HttpContext.Session.Set("flats", detailOfFlats);
            return RedirectToAction(nameof(Index));
        }
        
    }
}
