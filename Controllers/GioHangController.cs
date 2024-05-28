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
    public class GioHangController : Controller
    {
        private QuanLySachThuVienContext db = new QuanLySachThuVienContext();

        public ActionResult XemGioHang()
        {

            return View(LayGioHang());
        }
        public List<ChiTietGioHang> LayGioHang()
        {

            TaiKhoan tk = (TaiKhoan)Session["taikhoan"];
            GioHang gioHang = db.GioHangs.FirstOrDefault(g => g.maNguoiDung == tk.maNguoiDung);
            if (gioHang == null)
            {
                gioHang = new GioHang()
                {
                    maGH = tk.maNguoiDung,
                    maNguoiDung = tk.maNguoiDung,
                };
                return new List<ChiTietGioHang>();
            }

            var li = db.GioHangs.FirstOrDefault(m => m.maNguoiDung == tk.maNguoiDung).ChiTietGioHangs.ToList();
            return li;
        }
        public ActionResult ThemVaoGioHang(string maSach)
        {

            TaiKhoan tk = (TaiKhoan)Session["taikhoan"];

            if (tk == null)
            {
                return Redirect("/DangNhap/DangNhap");
            }
            var gh = db.GioHangs.Where(m => m.maNguoiDung == tk.maNguoiDung).FirstOrDefault();
            if (gh == null)
            {
                gh = new GioHang()
                {
                    maNguoiDung = tk.maNguoiDung,
                    maGH = tk.maNguoiDung
                };
                db.GioHangs.Add(gh);
                db.SaveChanges();
            }

            List<ChiTietGioHang> li = db.ChiTietGioHangs.Where(m => m.maGH == gh.maGH).ToList();
            if (li == null)
            {
                li = new List<ChiTietGioHang>();
                ChiTietGioHang ct = new ChiTietGioHang
                {
                    maGH = tk.maNguoiDung,
                    maSach = maSach,
                    soLuong = 1
                };
                db.ChiTietGioHangs.Add(ct);
                db.SaveChanges();
                li.Add(ct);
            }
            else
            {
                var chiTiet = li.FirstOrDefault(c => c.maSach == maSach);
                if (chiTiet != null)
                {
                    chiTiet.soLuong++;
                    db.SaveChanges();
                }
                else
                {
                    // Nếu sách chưa tồn tại, thêm mới sách vào giỏ hàng
                    ChiTietGioHang ct = new ChiTietGioHang
                    {
                        maGH = tk.maNguoiDung,
                        maSach = maSach,
                        soLuong = 1
                    };
                    db.ChiTietGioHangs.Add(ct);
                    db.SaveChanges();
                    li.Add(ct);
                }
            }
            db.SaveChanges();
            return View("XemGioHang", LayGioHang());
        }
        public ActionResult CapNhatGioHang(FormCollection form)
        {

            List<ChiTietGioHang> li = LayGioHang();
            TaiKhoan tk = (TaiKhoan)Session["taikhoan"];
            GioHang gh = db.GioHangs.FirstOrDefault(g => g.maNguoiDung == tk.maNguoiDung);
            if (tk == null)
            {
                return Redirect("/DangNhap/DangNhap");
            }

            if (form["soluong"] == null)
            {
                ViewBag.Error = "Số lượng không hợp lệ.";
                return View("XemGioHang", LayGioHang());
            }


            try
            {

                foreach (var key in form.AllKeys)
                {
                    if (key.StartsWith("soluong_"))
                    {
                        string maSach = key.Substring(8);
                        int soluong = Convert.ToInt32(form[key]);
                        if (soluong > db.Saches.FirstOrDefault(s => s.maSach == maSach).soLuong)
                        {
                            ViewBag.Error = "Số lượng không đủ.";
                            return View("XemGioHang", LayGioHang());
                        }
                        var ct = db.ChiTietGioHangs.FirstOrDefault(m => m.maSach == maSach && m.maGH == gh.maGH);
                        if (ct != null)
                        {
                            db.ChiTietGioHangs.FirstOrDefault(m => m.maSach == maSach && m.maGH == gh.maGH).soLuong = soluong;
                            db.SaveChanges();
                            return View("XemGioHang", LayGioHang());
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("XemGioHang", LayGioHang());
            }

            return RedirectToAction("XemGioHang");
        }

        public ActionResult XoaSanPham(string maSach)
        {
            List<ChiTietGioHang> li = LayGioHang();
            if (li != null)
            {
                var chiTiet = li.FirstOrDefault(c => c.maSach == maSach);
                if (chiTiet != null)
                {
                    db.ChiTietGioHangs.Remove(chiTiet);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("XemGioHang", LayGioHang());
        }

        public ActionResult MuonSach()
        {
            List<ChiTietGioHang> li = (List<ChiTietGioHang>)Session["giohang"];
            Session.Remove("giohang");
            return View(li);
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
