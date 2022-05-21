using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Vehicle_ShowRoom_Manager_System.Models;

namespace Vehicle_ShowRoom_Manager_System.Controllers
{
    public class VehicleImgsController : Controller
    {
        private Vehicle_ShowRoom_Manager_System_DataEntities db = new Vehicle_ShowRoom_Manager_System_DataEntities();

        // GET: VehicleImgs
        [Authorize]
        public ActionResult Index(string currentFilter, string searchString, int? page)
        {
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }


            ViewBag.CurrentFilter = searchString;
            var vehicleImg = db.VehicleImg.Include(e => e.Vehicle.VehicleImg);

            vehicleImg = vehicleImg.OrderByDescending(vh => vh.ImgId);

            int pageSize = 6;
            int pageNumber = (page ?? 1);
            return View(vehicleImg.ToPagedList(pageNumber, pageSize));
        }
       


        // GET: VehicleImgs/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleImg vehicleImg = db.VehicleImg.Find(id);
            if (vehicleImg == null)
            {
                return HttpNotFound();
            }
            return View(vehicleImg);
        }

        // GET: VehicleImgs/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.VehicleId = new SelectList(db.Vehicle, "VehicleId", "VehicleName");
            return View();
        }

        // POST: VehicleImgs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "ImgId,VehicleId,ImgPath")] VehicleImg vehicleImg)
        {
            if (ModelState.IsValid)
            {
                db.VehicleImg.Add(vehicleImg);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.VehicleId = new SelectList(db.Vehicle, "VehicleId", "VehicleName", vehicleImg.VehicleId);
            return View(vehicleImg);
        }

        // GET: VehicleImgs/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleImg vehicleImg = db.VehicleImg.Find(id);
            if (vehicleImg == null)
            {
                return HttpNotFound();
            }
            ViewBag.VehicleId = new SelectList(db.Vehicle, "VehicleId", "VehicleName", vehicleImg.VehicleId);
            return View(vehicleImg);
        }

        // POST: VehicleImgs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ImgId,VehicleId,ImgPath")] VehicleImg vehicleImg)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vehicleImg).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.VehicleId = new SelectList(db.Vehicle, "VehicleId", "VehicleName", vehicleImg.VehicleId);
            return View(vehicleImg);
        }

        // GET: VehicleImgs/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VehicleImg vehicleImg = db.VehicleImg.Find(id);
            if (vehicleImg == null)
            {
                return HttpNotFound();
            }
            return View(vehicleImg);
        }

        // POST: VehicleImgs/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VehicleImg vehicleImg = db.VehicleImg.Find(id);
            db.VehicleImg.Remove(vehicleImg);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize]
        public JsonResult SaveFile(HttpPostedFileBase file)
        {
            string returnImgPath = string.Empty;
            if (file.ContentLength > 0)
            {
                string fileName, fileExtension, imgSavePath;
                fileName = Path.GetFileNameWithoutExtension(file.FileName);
                fileExtension = Path.GetExtension(file.FileName);
                imgSavePath = Server.MapPath("/uploadedImages/") + fileName + fileExtension;
                file.SaveAs(imgSavePath);

                returnImgPath = "/uploadedImages/" + fileName + fileExtension;
            }
            return Json(returnImgPath, JsonRequestBehavior.AllowGet);
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
