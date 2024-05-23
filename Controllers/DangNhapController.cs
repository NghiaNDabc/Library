using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLySachThuVien.Models;

namespace QuanLySachThuVien.Areas.Admin.Controllers
{
    public class DangNhapController : Controller
    {
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(string tenDangNhap, string matKhau)
        {
            //Kiểm tra xem đã điền đủ tài khoản và mật khẩu chưa
            if (string.IsNullOrEmpty(tenDangNhap) || string.IsNullOrEmpty(matKhau))
            {
                ViewBag.thongBao = "Vui long nhap day du thong tin !";
                return View();
            }

            //Tìm tài khoản theo tên đăng nhập trong database
            var taiKhoan = new mapTaiKhoan().ChiTiet(tenDangNhap);

            //Kiểm tra tài khoản tồn tại không
            if (taiKhoan == null)
            {
                ViewBag.thongBao = "Tài khoản hoặc mật khẩu không chính xác !";
                ViewBag.tenDangNhap = tenDangNhap;
                return View();
            }

            //Kiểm tra mật khẩu có đúng không
            if (taiKhoan.matKhau != matKhau)
            {
                ViewBag.thongBao = "Tài khoản hoặc mật khẩu không chính xác !";
                ViewBag.tenDangNhap = tenDangNhap;
                return View();
            }

            //Kiểm tra quyền
            if (taiKhoan.quyen == "NV")
            {
                return Redirect("/Admin/BaoTriSach/Index");
            }
            else return Redirect("/TrangChu/Index");
        }
    }
}