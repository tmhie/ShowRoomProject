﻿using PagedList;
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
    public class SalesController : Controller
    {
        private Vehicle_ShowRoom_Manager_System_DataEntities db = new Vehicle_ShowRoom_Manager_System_DataEntities();

        // GET: Sales
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
            var sales = db.Sale.Include(s1 => s1.Admin).Include(s2 => s2.Customer).Include(s3 => s3.Vehicle);
            if (!String.IsNullOrEmpty(searchString))
            {
                sales = sales.Where(sale =>
                    sale.Vehicle.VehicleName.ToLower().Contains(searchString.ToLower()));
            }
            
            sales = sales.OrderByDescending(sale1 => sale1.Vehicle.VehicleName);

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(sales.ToPagedList(pageNumber, pageSize));

            
        }

        // GET: Sales/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sale sale = db.Sale.Find(id);
            if (sale == null)
            {
                return HttpNotFound();
            }
            return View(sale);
        }

        // GET: Sales/Create
        public ActionResult Create()
        {
            ViewBag.AdminId = new SelectList(db.Admin, "AdminId", "AdminName");
            ViewBag.CustomerId = new SelectList(db.Customer, "CustomerId", "CustomerName");
            ViewBag.VehicleId = new SelectList(db.Vehicle, "VehicleId", "VehicleName");
            return View();
        }

        // POST: Sales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SaleId,VehicleId,RoomName,CustomerId,AdminId,Price,OrderDate,DaliveryDate,Status")] Sale sale)
        {
            if (ModelState.IsValid)
            {
                db.Sale.Add(sale);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AdminId = new SelectList(db.Admin, "AdminId", "AdminName", sale.AdminId);
            ViewBag.CustomerId = new SelectList(db.Customer, "CustomerId", "CustomerName", sale.CustomerId);
            ViewBag.VehicleId = new SelectList(db.Vehicle, "VehicleId", "VehicleName", sale.VehicleId);
            return View(sale);
        }

        // GET: Sales/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sale sale = db.Sale.Find(id);
            if (sale == null)
            {
                return HttpNotFound();
            }
            ViewBag.AdminId = new SelectList(db.Admin, "AdminId", "AdminName", sale.AdminId);
            ViewBag.CustomerId = new SelectList(db.Customer, "CustomerId", "CustomerName", sale.CustomerId);
            ViewBag.VehicleId = new SelectList(db.Vehicle, "VehicleId", "VehicleName", sale.VehicleId);
            return View(sale);
        }

        // POST: Sales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SaleId,VehicleId,RoomName,CustomerId,AdminId,Price,OrderDate,DaliveryDate,Status")] Sale sale)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sale).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AdminId = new SelectList(db.Admin, "AdminId", "AdminName", sale.AdminId);
            ViewBag.CustomerId = new SelectList(db.Customer, "CustomerId", "CustomerName", sale.CustomerId);
            ViewBag.VehicleId = new SelectList(db.Vehicle, "VehicleId", "VehicleName", sale.VehicleId);
            return View(sale);
        }

        // GET: Sales/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sale sale = db.Sale.Find(id);
            if (sale == null)
            {
                return HttpNotFound();
            }
            return View(sale);
        }

        // POST: Sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sale sale = db.Sale.Find(id);
            db.Sale.Remove(sale);
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
