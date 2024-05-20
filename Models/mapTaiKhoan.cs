using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLySachThuVien.Models
{
    public class mapTaiKhoan
    {
        public TaiKhoan ChiTiet(string tenDangNhap)
        {
            try
            {
                QuanLySachThuVienContext db = new QuanLySachThuVienContext();
                var model = db.TaiKhoans.SingleOrDefault(m=>m.tenDangNhap == tenDangNhap);
                return model;
            }
            catch
            {
                return null;
            }
        }
    }
}