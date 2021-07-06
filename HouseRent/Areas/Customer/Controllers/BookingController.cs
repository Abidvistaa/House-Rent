using HouseRent.Data;
using HouseRent.Models;
using HouseRent.Utility;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRent.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class BookingController : Controller
    {
        private readonly ApplicationDbContext _db;
        public BookingController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Checkout()
        {

            return View();
        }
        public IActionResult Checkout2()
        {

            return View();
        }

        [HttpPost]
        [HttpPost]
        public IActionResult Checkout(Booking booking)
        {
            List<DetailOfFlat> detailOfFlats = HttpContext.Session.Get<List<DetailOfFlat>>("flats");
            if (detailOfFlats != null)
            {
                foreach (var item in detailOfFlats)
                {
                    BookingDetail bookingDetail = new BookingDetail();
                    bookingDetail.DetailOfFlatId = item.Id;
                    booking.BookingDetail.Add(bookingDetail);
                }
            }
            booking.BookingNo = GetBookingNo();
            _db.Bookings.Add(booking);
            _db.SaveChanges();
            HttpContext.Session.Set("flats", new List<DetailOfFlat>());
            TempData["save"] = "Booking has been successfull";
            return RedirectToAction("Index", "Home", new { area = "Customer" });
        }

        [HttpPost]
        public IActionResult Checkout2(Booking booking)
        {
            List<DetailOfDuplex> detailOfDuplexs = HttpContext.Session.Get<List<DetailOfDuplex>>("duplexs");
            if (detailOfDuplexs != null)
            {
                foreach (var item in detailOfDuplexs)
                {
                    BookingDetail2 bookingDetail2 = new BookingDetail2();
                    bookingDetail2.DetailOfDuplexId = item.Id;
                    booking.BookingDetail2.Add(bookingDetail2);
                }
            }
            booking.BookingNo = GetBookingNo();
            _db.Bookings.Add(booking);
            _db.SaveChanges();
            HttpContext.Session.Set("duplexs", new List<DetailOfDuplex>());
            TempData["save"] = "Booking has been successfull";
            return RedirectToAction("Indexx", "Home", new { area = "Customer" });
        }
        public string GetBookingNo()
        {
            int rowCount = _db.Bookings.ToList().Count() + 1;
            return rowCount.ToString("000");
        }
    }
}
