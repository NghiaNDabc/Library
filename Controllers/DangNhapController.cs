using QuanLySachThuVien.Models;
using System.Linq;
using System.Web.Mvc;

namespace QuanLySachThuVien.Areas.Admin.Controllers
{
    public class DangNhapController : Controller
    {
        public ActionResult DangXuat()
        {
            Session["taikhoan"] = null;
            return RedirectToAction("DangNhap");
        }
        public ActionResult TaiKhoanCuaToi()
        {
            return View();
        }
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
                ViewBag.thongBao = "Vui lòng nhập đầy đủ thông tin !";
                return View();
            }

            //Tìm tài khoản theo tên đăng nhập trong database
            QuanLySachThuVienContext db = new QuanLySachThuVienContext();
            var taiKhoan = db.TaiKhoans.SingleOrDefault(m => m.tenDangNhap == tenDangNhap);

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

            //Kiểm tra quyền và chuyển hướng sang trang admin/user
            if (taiKhoan.quyen == "admin")
            {
                return Redirect("/Admin/BaoTriSach/Index");
            }
            else
            {
                Session["taikhoan"] = taiKhoan;
                return Redirect("/TrangChu/Index");
            }
        }
        public ActionResult DangKy(string email, string hoten, string ngaySinh, string queQuan, string sdt, string tenDangNhap, string matKhau, string matKhaunl)
        {
            //Kiểm tra xem đã điền đủ thông tin
            if (string.IsNullOrEmpty(tenDangNhap) || string.IsNullOrEmpty(matKhau)||string.IsNullOrEmpty(email)||
                string.IsNullOrEmpty(hoten)||string.IsNullOrEmpty(queQuan)||string.IsNullOrEmpty(sdt)||string.IsNullOrEmpty(ngaySinh))
            {
                ViewBag.thongBao = "Vui lòng nhập đầy đủ thông tin !";
                return View();
            }

            //Tìm tài khoản theo tên đăng nhập trong database
            QuanLySachThuVienContext db = new QuanLySachThuVienContext();
            var taiKhoan = db.TaiKhoans.SingleOrDefault(m => m.tenDangNhap == tenDangNhap);

            //Kiểm tra tài khoản tồn tại không
            if (taiKhoan == null)
            {
                ViewBag.thongBao = "Tài khoản hoặc mật khẩu không chính xác !";
                //code Minh
                if (matKhau != matKhaunl)
                {
                    ViewBag.thongBao = "Tài khoản hoặc mật khẩu không chính xác !";
                    ViewBag.tenDangNhap = tenDangNhap;
                    return View();
                }
                // xử lý để khi đăng ký xong thì chuyển qua đăng nhập
            }
            else
            {
                ViewBag.thongBao = "Tên đăng nhập đã tồn tại!";
                return View();
            }
            return View();
        }

    }

}