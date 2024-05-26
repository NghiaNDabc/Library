using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using QuanLySachThuVien.Models;

namespace QuanLySachThuVien.Controllers
{
    public class TrangChuController : Controller
    {
        private QuanLySachThuVienContext db = new QuanLySachThuVienContext();

        // GET: Saches
        public ActionResult Index(string key)
        {
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
        public ActionResult ThemVaoGioHang(string maSach)
        {

            ViewBag.ma = maSach;
            List<ChiTietGioHang> li = (List<ChiTietGioHang>)Session["giohang"];
            if (li == null)
            {
                li = new List<ChiTietGioHang>();
                ChiTietGioHang ct = new ChiTietGioHang
                {
                    maSach = maSach,
                    soLuong = 1
                };
                li.Add(ct);
            }

            else
            {
                var chiTiet = li.FirstOrDefault(c => c.maSach == maSach);
                if (chiTiet != null)
                {
                    chiTiet.soLuong++;
                }
                else
                {
                    // Nếu sách chưa tồn tại, thêm mới sách vào giỏ hàng
                    ChiTietGioHang ct = new ChiTietGioHang
                    {
                        maSach = maSach,
                        soLuong = 1
                    };
                    li.Add(ct);
                }
            }
            
            Session["giohang"]=li;
            return View();
        }
        public ActionResult XoaSanPham(string maSach)
        {
            List<ChiTietGioHang> li = (List<ChiTietGioHang>)Session["giohang"];
            if (li != null)
            {
                var chiTiet = li.FirstOrDefault(c => c.maSach == maSach);
                if (chiTiet != null)
                {
                    li.Remove(chiTiet);
                    Session["giohang"] = li;
                }
            }
            return RedirectToAction("XemGioHang");
        }
        public ActionResult XemGioHang()
        {
            List<ChiTietGioHang> li = Session["giohang"] as List<ChiTietGioHang> ?? new List<ChiTietGioHang>();
            return View(li);
        }
        public ActionResult MuonSach()
        {
            List<ChiTietGioHang> li = (List<ChiTietGioHang>)Session["giohang"];
            Session.Remove("giohang");
            return View(li);
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
