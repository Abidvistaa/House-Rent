using HouseRent.Areas.Customer.Models;
using HouseRent.Data;
using HouseRent.Models;
using HouseRent.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRent.Areas.Customer.Controllers
{
    [Authorize]
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

        public IActionResult ShowBookingToAdmin()
        {

            var result = from bd in _db.BookingDetails
                         join bk in _db.Bookings on bd.BookingId equals bk.Id
                         join df in _db.DetailOfFlats on bd.DetailOfFlatId equals df.Id
                         select new BookingListMappingVm()
                         {
                             BookingNo = bk.BookingNo,
                             Name = bk.Name,
                             PhoneNO= bk.PhoneNo,
                             Email=bk.Email,
                             Address=bk.Address,
                             FlatName=df.FlatName,
                             Photo=df.Photo
                         };
            ViewBag.Show = result;
            return View();
        }

        [Authorize(Roles ="Admin")]
        public IActionResult ShowBookingToAdmin2()
        {

            var result = from bd in _db.bookingDetail2s
                         join bk in _db.Bookings on bd.BookingId equals bk.Id
                         join dd in _db.DetailOfDuplexs on bd.DetailOfDuplexId equals dd.Id
                         select new BookingListMappingVm()
                         {
                             BookingNo = bk.BookingNo,
                             Name = bk.Name,
                             PhoneNO = bk.PhoneNo,
                             Email = bk.Email,
                             Address = bk.Address,
                             DuplexName = dd.DuplexName,
                             Photo = dd.Photo
                         };
            ViewBag.Show = result;
            return View();
        }
    }
}
