using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuanLySachThuVien.Models;

namespace QuanLySachThuVien.Areas.Admin.Controllers
{
    public class BaoTriDanhMucController : Controller
    {
        private QuanLySachThuVienContext db = new QuanLySachThuVienContext();

        // GET: Admin/BaoTriDanhMuc
        public ActionResult Index()
        {
            return View(db.DanhMucs.ToList());
        }

        // GET: Admin/BaoTriDanhMuc/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DanhMuc danhMuc = db.DanhMucs.Find(id);
            if (danhMuc == null)
            {
                return HttpNotFound();
            }
            return View(danhMuc);
        }
        public ActionResult DeleteList(List<String> selectedProducts)
        {
            if (selectedProducts == null || selectedProducts.Count < 1)
            {
                // If no products selected, you can redirect or return a message
                TempData["mes"] = "Not ok";
                return RedirectToAction("Index");

            }


            var list = db.DanhMucs.Where(s => selectedProducts.Contains(s.maDanhMuc)).ToList();
            //xóa ảnh

            TempData["SuccessMessage"] = "Xóa thành công " + list.Count + " danh mục!";
            db.DanhMucs.RemoveRange(list);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        // GET: Admin/BaoTriDanhMuc/Create
        public ActionResult Create()
        {
            ViewBag.maDanhMuc = new SelectList(db.DanhMucs, "maDanhMuc", "tenDanhMuc");
            return View();
        }
        // POST: Admin/BaoTriDanhMuc/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "maDanhMuc,tenDanhMuc,moTa")] DanhMuc danhMuc)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.DanhMucs.Add(danhMuc);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(danhMuc);
        //}

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


        [HttpPost]

        public ActionResult Create(DanhMuc danhMuc)
        {
            string err = "";
            try
            {
                danhMuc.maDanhMuc = "default";
                if (ModelState.IsValid)
                {
                    String newID = "DM00100";
                    if (db.DanhMucs.Any())
                    {
                        var danhmucMaxID = (from dm in db.DanhMucs
                                            orderby dm.maDanhMuc descending
                                            select dm).FirstOrDefault();
                        String maxID = danhmucMaxID.maDanhMuc;
                        newID = IncrementId(maxID);
                    }

                    danhMuc.maDanhMuc = newID;
                    db.DanhMucs.Add(danhMuc);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Đã thêm danh mục thành công." }, JsonRequestBehavior.AllowGet);
                    //return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                err = e.Message;
                //return View(sach);
            }
            return Json(new { success = false, message = "Thêm danh mục không thành công. Vui lòng kiểm tra lại thông tin." + err }, JsonRequestBehavior.AllowGet);
        }

        // GET: Admin/BaoTriDanhMuc/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DanhMuc danhMuc = db.DanhMucs.Find(id);
            if (danhMuc == null)
            {
                return HttpNotFound();
            }
            return View(danhMuc);
        }

        // POST: Admin/BaoTriDanhMuc/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "maDanhMuc,tenDanhMuc,moTa")] DanhMuc danhMuc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(danhMuc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(danhMuc);
        }

        // GET: Admin/BaoTriDanhMuc/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DanhMuc danhMuc = db.DanhMucs.Find(id);
            if (danhMuc == null)
            {
                return HttpNotFound();
            }
            return View(danhMuc);
        }

        // POST: Admin/BaoTriDanhMuc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            DanhMuc danhMuc = db.DanhMucs.Find(id);
            db.DanhMucs.Remove(danhMuc);
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
