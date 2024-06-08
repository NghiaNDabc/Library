using QuanLySachThuVien.Models;
using System.Data.SqlClient;
using System;
using System.Linq;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;

namespace QuanLySachThuVien.Areas.Admin.Controllers
{
    public class DangNhapController : Controller
    {
        QuanLySachThuVienContext db = new QuanLySachThuVienContext();
        public ActionResult DangXuat()
        {
            Session["NguoiDung"] = null;
            return Redirect("/TrangChu/Index");
        }
        public ActionResult DangKy()
        {
            return View();
        }
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpGet]
        public ActionResult TaiKhoanCuaToi()
        {
            var model = Session["NguoiDung"];
            return View(model);
        }

        //Tạo ID mới
        public string IncrementId(string currentID)
        {
            // Tìm vị trí đầu tiên của chữ số trong chuỗi
            int firstNumIndex = -1;
            for (int i = 0; i < currentID.Length; i++)
            {
                if (char.IsDigit(currentID[i]))
                {
                    firstNumIndex = i;
                    break;
                }
            }

            // Nếu không tìm thấy chữ số nào, trả về chuỗi gốc
            if (firstNumIndex == -1)
            {
                throw new ArgumentException("Input string does not contain a numeric part.");
            }

            // Tách phần chữ và phần số
            string letterPart = currentID.Substring(0, firstNumIndex);
            string numberPart = currentID.Substring(firstNumIndex);

            // Tăng phần số lên 1
            int number = int.Parse(numberPart);
            number++;

            // Ghép phần chữ và phần số đã tăng
            string newId = letterPart + number.ToString(new string('0', numberPart.Length));

            return newId;
        }
        [HttpPost]
        public ActionResult DangNhap(string tenDangNhap, string matKhau)
        {
            //Tìm tài khoản theo tên đăng nhập trong database        
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

            // Lưu tài khoản hiện tại vào Session
            var nguoiDung = db.NguoiDungs.FirstOrDefault(m => m.maNguoiDung == taiKhoan.maNguoiDung);
            Session["NguoiDung"] = nguoiDung;

            //Kiểm tra quyền và chuyển hướng sang trang admin/user
            if (taiKhoan.quyen == "admin")
            {
                return Redirect("/Admin/BaoTriSach/Index");
            }
            else
            {
                return Redirect("/TrangChu/Index");
            }
        }

        [HttpPost]
        public ActionResult DangKy(string email, string hoTen, string ngaySinh, string queQuan, string sdt, string tenDangNhap, string matKhau, string matKhauNL)
        {
            //Kiểm tra tài khoản đã tồn tại chưa
            var taiKhoan = db.TaiKhoans.FirstOrDefault(m => m.tenDangNhap == tenDangNhap);
            if (taiKhoan != null)
            {
                ViewBag.thongBao = "Tài khoản đã tồn tại !";
                return View();
            }

            //Kiểm tra mật khẩu nhập lại có chính xác không 
            if (matKhau.Equals(matKhauNL) != true)
            {
                ViewBag.thongBao = "Mật khẩu nhập lại không chính xác !";
                return View();
            }

            //Chuyển string ngaySinh sang dateTime
            DateTime dateTime = DateTime.Parse(ngaySinh);

            //Tạo mã người dùng mới 
            string maNguoiDung = "ND001";

            if (db.NguoiDungs.Any())
            {
                var nguoiDungMaxID = (from nguoidung in db.NguoiDungs
                                      orderby nguoidung.maNguoiDung descending
                                      select nguoidung).FirstOrDefault();
                String maxIDNow = nguoiDungMaxID.maNguoiDung;
                maNguoiDung = IncrementId(maxIDNow);
            }

            //Thêm vào csdl
            string connectionString = WebConfigurationManager.ConnectionStrings["QuanLySachThuVienContext"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query1 = "INSERT INTO NguoiDung VALUES (@HoTen, @NgaySinh, @QueQuan, @Email, @SDT, @MaNguoiDung)";
                SqlCommand cmd1 = new SqlCommand(query1, connection);
                cmd1.Parameters.AddWithValue("@HoTen", hoTen);
                cmd1.Parameters.AddWithValue("@NgaySinh", dateTime);
                cmd1.Parameters.AddWithValue("@QueQuan", queQuan);
                cmd1.Parameters.AddWithValue("@Email", email);
                cmd1.Parameters.AddWithValue("@SDT", sdt);
                cmd1.Parameters.AddWithValue("@MaNguoiDung", maNguoiDung);

                string query2 = "INSERT INTO TaiKhoan VALUES(@TenDangNhap, @MatKhau, @Quyen, @MaNguoiDung)";
                SqlCommand cmd2 = new SqlCommand(query2, connection);
                cmd2.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                cmd2.Parameters.AddWithValue("@MatKhau", matKhau);
                cmd2.Parameters.AddWithValue("@Quyen", "user");
                cmd2.Parameters.AddWithValue("@MaNguoiDung", maNguoiDung);

                connection.Open();
                cmd1.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();
            }

            var nguoiDung = db.NguoiDungs.SingleOrDefault(m => m.maNguoiDung == maNguoiDung);
            Session["NguoiDung"] = nguoiDung;
            return Redirect("/TrangChu/Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TaiKhoanCuaToi([Bind(Include = "maNguoiDung, email, hoTen, ngaySinh, queQuan, sdt")] NguoiDung nguoiDung)
        {
            DateTime dateTime = DateTime.Parse((string)Request.Form["ngaySinh"]);
            nguoiDung.ngaySinh = dateTime;
            if (ModelState.IsValid)
            {
                db.Entry(nguoiDung).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.thongBao = "Cập nhật thông tin thành công !";
                Session["NguoiDung"] = nguoiDung;
                return View(nguoiDung);
            }
            ViewBag.thongBao = "Chua duoc cap nhat";
            return View(nguoiDung);
        }
    }

}