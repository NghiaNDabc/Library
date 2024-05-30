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
    public class PhieuMuonsController : Controller
    {
        private QuanLySachThuVienContext db = new QuanLySachThuVienContext();

        public JsonResult GoiYSach(String query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return Json(new List<string>(), JsonRequestBehavior.AllowGet);
            }
            var goiYSach = db.Saches
                        .Where(s => s.tenSach.Contains(query)) 
                        .Select(s => new {maSach= s.maSach,tenSach= s.tenSach,hinhAnh= s.hinhAnh }) 
                        .ToList();
            return Json(goiYSach, JsonRequestBehavior.AllowGet);
        }
       
        public JsonResult GoiYMsv(String query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return Json(new List<string>(), JsonRequestBehavior.AllowGet);
            }
            var goiYSv = db.NguoiDungs
                        .Where(s => s.maNguoiDung.Contains(query))
                        .Select(s => new { maNguoiDung = s.maNguoiDung, hoTen = s.hoTen })
                        .ToList();
            return Json(goiYSv, JsonRequestBehavior.AllowGet);
        }

        // GET: Admin/PhieuMuons
        public ActionResult Index()
        {
            var phieuMuons = db.PhieuMuons.Include(p => p.NguoiDung);
           
            return View(phieuMuons.ToList());
        }

        // GET: Admin/PhieuMuons/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuMuon phieuMuon = db.PhieuMuons.Find(id);
            if (phieuMuon == null)
            {
                return HttpNotFound();
            }
            return View(phieuMuon);
        }

        // GET: Admin/PhieuMuons/Create
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
        public ActionResult Create()
        {
            ViewBag.maNguoiDung = new SelectList(db.NguoiDungs, "maNguoiDung", "hoTen");
            return View();
        }

        // POST: Admin/PhieuMuons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        public ActionResult Create([Bind(Include = "maPhieuMuon,maNguoiDung,ngayMuon,ngayTra,tinhTrang")] PhieuMuon phieuMuon,  List<string> maSach,List<int> soLuong )
        {
            phieuMuon.maPhieuMuon = "default";
            if (ModelState.IsValid)
            {
                String newID = "SA0001";
                if (db.Saches.Any())
                {
                    var bookWithMaxId = (from pm in db.PhieuMuons
                                         orderby pm.maPhieuMuon descending
                                         select pm).FirstOrDefault();
                    String maxID = bookWithMaxId.maPhieuMuon;
                    newID = IncrementId(maxID);
                }

                phieuMuon.maPhieuMuon = newID;
                db.PhieuMuons.Add(phieuMuon);
                db.SaveChanges();
                //foreach (Sach s in sach)
                //{
                //    ChiTietPhieuMuon ctpm = new ChiTietPhieuMuon();
                //    ctpm.maPhieuMuon = phieuMuon.maPhieuMuon;
                //    ctpm.maSach = s.maSach;
                //    ctpm.soLuong = s.soLuong;
                //    db.ChiTietPhieuMuons.Add(ctpm);
                //}
                for (int i = 0; i < maSach.Count; i++)
                {
                    ChiTietPhieuMuon ctpm = new ChiTietPhieuMuon();
                    ctpm.maPhieuMuon = phieuMuon.maPhieuMuon;
                    ctpm.maSach = maSach[i];
                    ctpm.soLuong = soLuong[i];
                    db.ChiTietPhieuMuons.Add(ctpm);
                }
                ViewBag.soluong = maSach.Count;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.maNguoiDung = new SelectList(db.NguoiDungs, "maNguoiDung", "hoTen", phieuMuon.maNguoiDung);
            return View(phieuMuon);
        }

        // GET: Admin/PhieuMuons/Edit/5
        public class SachViewModel
        {
            public string MaSach { get; set; }
            public string TenSach { get; set; }
            public string HinhAnh { get; set; }
            public int? SoLuongMuon { get; set; }
        }
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuMuon phieuMuon = db.PhieuMuons.Find(id);
            if (phieuMuon == null)
            {
                return HttpNotFound();
            }
            ViewBag.maNguoiDung = new SelectList(db.NguoiDungs, "maNguoiDung", "hoTen", phieuMuon.maNguoiDung);

            var Saches = from ctpm in db.ChiTietPhieuMuons
                         join sach in db.Saches
                         on ctpm.maSach equals sach.maSach
                         where ctpm.maPhieuMuon.Equals(id)
                         select new SachViewModel
                         {
                             MaSach = sach.maSach,
                             TenSach = sach.tenSach,
                           HinhAnh= sach.hinhAnh,
                           SoLuongMuon= ctpm.soLuong        
                         };
            ViewBag.Saches = Saches.ToList();


            return View(phieuMuon);
        }

        // POST: Admin/PhieuMuons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "maPhieuMuon,maNguoiDung,ngayMuon,ngayTra,tinhTrang")] PhieuMuon phieuMuon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phieuMuon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.maNguoiDung = new SelectList(db.NguoiDungs, "maNguoiDung", "hoTen", phieuMuon.maNguoiDung);
            return View(phieuMuon);
        }

        // GET: Admin/PhieuMuons/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuMuon phieuMuon = db.PhieuMuons.Find(id);
            if (phieuMuon == null)
            {
                return HttpNotFound();
            }
            return View(phieuMuon);
        }

        // POST: Admin/PhieuMuons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            PhieuMuon phieuMuon = db.PhieuMuons.Find(id);
            db.PhieuMuons.Remove(phieuMuon);
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
