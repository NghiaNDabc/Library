using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using System.Web.Services.Description;
using QuanLySachThuVien.Models;

namespace QuanLySachThuVien.Controllers
{
    public class TrangChuController : Controller
    {
        private QuanLySachThuVienContext db = new QuanLySachThuVienContext();

        // GET: Saches
        public ActionResult Index(string key)
        {
            TaiKhoan tk = (TaiKhoan)Session["taikhoan"];
            if (tk == null)
            {
                return Redirect("/DangNhap/DangNhap");
            }
            var saches = db.Saches.Include(s => s.DanhMuc);

            return View(saches.ToList());

        }
        public ActionResult XemALLSach()
        {
            var saches = db.Saches;
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

        // GET: Saches/Create\

        public ActionResult XemSachTheoDM(string madm)
        {
            var saches = db.Saches.Where(s => s.maDanhMuc == madm).ToList();
            return View(saches);
        }
        public ActionResult TimKiem(string key)
        {
            if (!String.IsNullOrEmpty(key.ToLower()))
            {
                var saches = db.Saches.Where(s => s.tenSach.ToLower().Contains(key)
                || s.tenTacGia.ToLower().Contains(key) || s.namXuatBan.ToString().Equals(key)
                || s.nhaXuatBan.Contains(key) || s.DanhMuc.tenDanhMuc.ToLower().Contains(key)).ToList();

                if (!saches.Any())
                {
                    ViewBag.msg = "Không tìm thấy sách có thông tin liên quan!";
                }
                else
                {
                    return View(saches);

                }
            }
            return View();
        }
      
        public ActionResult QuyDinh()
        {
            return View();
        }
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
