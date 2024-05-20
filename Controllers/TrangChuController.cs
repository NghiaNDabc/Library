using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuanLySachThuVien.Models;

namespace QuanLySachThuVien.Controllers
{
    public class TrangChuController : Controller
    {
        private QuanLySachThuVienContext db = new QuanLySachThuVienContext();

        // GET: Saches
        public ActionResult Index()
        {
            var saches = db.Saches.Include(s => s.DanhMuc);
            return View(saches.ToList());
        }

        // GET: Saches/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sach sach = db.Saches.Find(id);
            if (sach == null)
            {
                return HttpNotFound();
            }
            return View(sach);
        }

        // GET: Saches/Create


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}