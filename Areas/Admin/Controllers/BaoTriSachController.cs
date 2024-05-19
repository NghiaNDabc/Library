using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuanLySachThuVien.Models;
using PagedList;
namespace QuanLySachThuVien.Areas.Admin.Controllers
{
    public class BaoTriSachController : Controller
    {
        private QuanLySachThuVienContext db = new QuanLySachThuVienContext();

        // GET: Admin/Saches0.
        public ActionResult Index(int? page, string searchString)
        {
            var saches = db.Saches.Include(s => s.DanhMuc);
            if (!String.IsNullOrEmpty(searchString))
            {
                saches = saches.Where(s => s.tenSach.ToLower().Contains(searchString)
|| s.tenTacGia.ToLower().Contains(searchString) || s.namXuatBan.ToString().Equals(searchString)
|| s.nhaXuatBan.Contains(searchString) || s.DanhMuc.tenDanhMuc.ToLower().Contains(searchString));
            }
            saches = saches.OrderByDescending(s => s.maSach);
            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View(saches.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/Saches/Details/5
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
        public ActionResult DeleteList(List<String> selectedProducts)
        {
            if (selectedProducts == null || selectedProducts.Count < 1)
            {
                // If no products selected, you can redirect or return a message
                TempData["mes"] = "Not ok";
                return RedirectToAction("Index");

            }


            var list = db.Saches.Where(s => selectedProducts.Contains(s.maSach)).ToList();
            //xóa ảnh

            TempData["SuccessMessage"] = "Xóa thành công " + list.Count + " cuốn sách!";
            db.Saches.RemoveRange(list);
            foreach (var sach in list)
            {
                string path = Server.MapPath("~/Content/Images/" + sach.hinhAnh);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Admin/Saches/Create
        public ActionResult Create()
        {
            ViewBag.maDanhMuc = new SelectList(db.DanhMucs, "maDanhMuc", "tenDanhMuc");
            return View();
        }
        public string IncrementId(string currentId)
        {
            // Tìm vị trí đầu tiên của chữ số trong chuỗi
            int numberStartIndex = -1;
            for (int i = 0; i < currentId.Length; i++)
            {
                if (char.IsDigit(currentId[i]))
                {
                    numberStartIndex = i;
                    break;
                }
            }

            // Nếu không tìm thấy chữ số nào, trả về chuỗi gốc
            if (numberStartIndex == -1)
            {
                throw new ArgumentException("Input string does not contain a numeric part.");
            }

            // Tách phần chữ và phần số
            string prefix = currentId.Substring(0, numberStartIndex);
            string numberPart = currentId.Substring(numberStartIndex);

            // Tăng phần số lên 1
            int number = int.Parse(numberPart);
            number += 1;

            // Ghép phần chữ và phần số đã tăng
            string newId = prefix + number.ToString(new string('0', numberPart.Length));

            return newId;
        }

        // POST: Admin/Saches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        //[ValidateAntiForgeryToken]

        //public ActionResult Create([Bind(Include = "maSach,maDanhMuc,tenSach,hinhAnh,soTrang,moTa,soLuong,namXuatBan,nhaXuatBan,giaTien,tenTacGia")] Sach sach)
        //{
        //    sach.maSach = "default";
        //    if (ModelState.IsValid)
        //    {
        //        var bookWithMaxId = db.Saches.OrderByDescending(b => b.maSach).FirstOrDefault();
        //        String maxID = bookWithMaxId?.maSach ?? "0";
        //        String newID = IncrementId(maxID);
        //        sach.maSach = newID;
        //        sach.hinhAnh = "";

        //        // Handle file upload
        //        var f = Request.Files["imgfile"];
        //        if (f != null && f.ContentLength > 0)
        //        {
        //            string fileName = System.IO.Path.GetFileName(f.FileName);
        //            string uploadPath = Server.MapPath("~/Content/Images/" + fileName);
        //            f.SaveAs(uploadPath);
        //            sach.hinhAnh = fileName;
        //            Console.WriteLine("file " + fileName + uploadPath);
        //        }

        //        db.Saches.Add(sach);
        //        db.SaveChanges();
        //        return Json(new { success = true, message = "Đã thêm sách thành công." }, JsonRequestBehavior.AllowGet);
        //    }

        //    // If ModelState is not valid, return error message
        //    return Json(new { success = false, message = "Thêm sách không thành công. Vui lòng kiểm tra lại thông tin." });
        //}
        [HttpPost]
        public ActionResult Create([Bind(Include = "maSach,maDanhMuc,tenSach,hinhAnh,soTrang,moTa,soLuong,namXuatBan,nhaXuatBan,giaTien,tenTacGia")] Sach sach)
        {
            string err = "";
            try
            {
                sach.maSach = "default";
                if (ModelState.IsValid)
                {


                    String newID = "SA0001";
                    if (db.Saches.Any())
                    {
                        var bookWithMaxId = (from book in db.Saches
                                             orderby book.maSach descending
                                             select book).FirstOrDefault();
                        String maxID = bookWithMaxId.maSach;
                        newID = IncrementId(maxID);
                    }

                    sach.maSach = newID;
                    sach.hinhAnh = "";
                    var f = Request.Files["imgfile"];
                    if (f != null && f.ContentLength > 0)
                    {
                        string fileName = System.IO.Path.GetFileName(f.FileName);
                        string uploadPath = Server.MapPath("~/Content/Images/" + fileName);
                        f.SaveAs(uploadPath);
                        sach.hinhAnh = fileName;

                    }
                    db.Saches.Add(sach);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Đã thêm sách thành công." }, JsonRequestBehavior.AllowGet);
                    //return RedirectToAction("Index");
                }
            }
            catch(Exception e)
            {
                err = e.Message;
                //return View(sach);
            }
            ViewBag.maDanhMuc = new SelectList(db.DanhMucs, "maDanhMuc", "tenDanhMuc", sach.maDanhMuc);
            return Json(new { success = false, message = "Thêm sách không thành công. Vui lòng kiểm tra lại thông tin." +err}, JsonRequestBehavior.AllowGet);


        }

        // GET: Admin/Saches/Edit/5
        public ActionResult Edit(string id)
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
            ViewBag.maDanhMuc = new SelectList(db.DanhMucs, "maDanhMuc", "tenDanhMuc", sach.maDanhMuc);
            return View(sach);
        }

        // POST: Admin/Saches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "maSach,maDanhMuc,tenSach,hinhAnh,soTrang,moTa,soLuong,namXuatBan,nhaXuatBan,giaTien,tenTacGia")] Sach sach)
        {
            //sach.hinhAnh = "";
            if (ModelState.IsValid)
            {
                var f = Request.Files["imgfile"];
                if (f != null && f.ContentLength > 0)
                {
                    string fileName = System.IO.Path.GetFileName(f.FileName);
                    string uploadPath = Server.MapPath("~/Content/Images/" + fileName);
                    f.SaveAs(uploadPath);
                    sach.hinhAnh = fileName;
                 
                }
                db.Entry(sach).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.maDanhMuc = new SelectList(db.DanhMucs, "maDanhMuc", "tenDanhMuc", sach.maDanhMuc);
            return View(sach);
        }

        // GET: Admin/Saches/Delete/5
        public ActionResult Delete(string id)
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

        // POST: Admin/Saches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Sach sach = db.Saches.Find(id);
            db.Saches.Remove(sach);
            db.SaveChanges();
            return RedirectToAction("Index");
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
