using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Vehicle_ShowRoom_Manager_System.Models;

namespace Vehicle_ShowRoom_Manager_System.Controllers
{
    public class ShowRoomsController : Controller
    {
        private Vehicle_ShowRoom_Manager_System_DataEntities db = new Vehicle_ShowRoom_Manager_System_DataEntities();

        // GET: ShowRooms
        [Authorize]

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }


            ViewBag.CurrentFilter = searchString;
            var showRoom = db.ShowRoom.Include(s => s.Admin).Include(s => s.Customer).Include(s => s.Vehicle);

            if (!String.IsNullOrEmpty(searchString))
            {
                showRoom = showRoom.Where(v =>
                    v.Vehicle.VehicleName.ToLower().Contains(searchString.ToLower()));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    showRoom = showRoom.OrderByDescending(v => v.Vehicle.VehicleName);
                    break;
                default:
                    showRoom = showRoom.OrderByDescending(v => v.Vehicle.VehicleName);
                    break;
            }

            int pageSize = 6;
            int pageNumber = (page ?? 1);
            return View(showRoom.ToPagedList(pageNumber, pageSize));


        }

        // GET: ShowRooms/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShowRoom showRoom = db.ShowRoom.Find(id);
            if (showRoom == null)
            {
                return HttpNotFound();
            }
            return View(showRoom);
        }
        [Authorize]
        // GET: ShowRooms/Create
        public ActionResult Create()
        {
            ViewBag.AdminId = new SelectList(db.Admin, "AdminId", "AdminName");
            ViewBag.CustomerId = new SelectList(db.Customer, "CustomerId", "CustomerName");
            ViewBag.VehicleId = new SelectList(db.Vehicle, "VehicleId", "VehicleName");
            return View();
        }

        // POST: ShowRooms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RoomId,RoomAddress,RoomName,AdminId,CustomerId,VehicleId,Status")] ShowRoom showRoom)
        {
            if (ModelState.IsValid)
            {
                db.ShowRoom.Add(showRoom);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AdminId = new SelectList(db.Admin, "AdminId", "AdminName", showRoom.AdminId);
            ViewBag.CustomerId = new SelectList(db.Customer, "CustomerId", "CustomerName", showRoom.CustomerId);
            ViewBag.VehicleId = new SelectList(db.Vehicle, "VehicleId", "VehicleName", showRoom.VehicleId);
            return View(showRoom);
        }

        // GET: ShowRooms/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShowRoom showRoom = db.ShowRoom.Find(id);
            if (showRoom == null)
            {
                return HttpNotFound();
            }
            ViewBag.AdminId = new SelectList(db.Admin, "AdminId", "AdminName", showRoom.AdminId);
            ViewBag.CustomerId = new SelectList(db.Customer, "CustomerId", "CustomerName", showRoom.CustomerId);
            ViewBag.VehicleId = new SelectList(db.Vehicle, "VehicleId", "VehicleName", showRoom.VehicleId);
            return View(showRoom);
        }

        // POST: ShowRooms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "RoomId,RoomAddress,RoomName,AdminId,CustomerId,VehicleId,Status")] ShowRoom showRoom)
        {
            if (ModelState.IsValid)
            {
                db.Entry(showRoom).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AdminId = new SelectList(db.Admin, "AdminId", "AdminName", showRoom.AdminId);
            ViewBag.CustomerId = new SelectList(db.Customer, "CustomerId", "CustomerName", showRoom.CustomerId);
            ViewBag.VehicleId = new SelectList(db.Vehicle, "VehicleId", "VehicleName", showRoom.VehicleId);
            return View(showRoom);
        }

        // GET: ShowRooms/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShowRoom showRoom = db.ShowRoom.Find(id);
            if (showRoom == null)
            {
                return HttpNotFound();
            }
            return View(showRoom);
        }

        // POST: ShowRooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ShowRoom showRoom = db.ShowRoom.Find(id);
            db.ShowRoom.Remove(showRoom);
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
