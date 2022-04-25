using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Vehicle_Showroom_Management_System.Models;

namespace Vehicle_Showroom_Management_System.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        private ShowRoomDbContext db = new ShowRoomDbContext();

        // GET: Admin/Category
        public ActionResult Index()
        {
            var list = db.Categorys.Where(m=>m.Status != 0).OrderByDescending(m=>m.Created_At).ToList();
            return View("Index",list);
        }

        public ActionResult Trash()
        {
            var list = db.Categorys.Where(m => m.Status == 0).OrderByDescending(m => m.Created_At).ToList();
            return View("Trash", list);
        }

        // GET: Admin/Category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categorys.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Admin/Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,ParentId,Orders,Describe,Created_By,Created_At,Update_By,Update_At,Status")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categorys.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }

        // GET: Admin/Category/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categorys.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Admin/Category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,ParentId,Orders,Describe,Created_By,Created_At,Update_By,Update_At,Status")] Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Admin/Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categorys.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Admin/Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Categorys.Find(id);
            db.Categorys.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Trash","Category");
        }

        //thay doi trang thai 1=>2 , 2=>1
        public ActionResult Status(int id)
        {
            Category category = db.Categorys.Find(id);
            int status = (category.Status == 1) ? 2 : 1;
            category.Status = status;
            category.Update_By = 1;
            category.Update_At = DateTime.Now;
            db.Entry(category).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //Xoa vao thung rac Status = 0;
        public ActionResult DelTrash(int id)
        {
            Category category = db.Categorys.Find(id);
            category.Status = 0;
            category.Update_By = 1;
            category.Update_At = DateTime.Now;
            db.Entry(category).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index" , "Category");
        }

        //Khoi phuc từ thùng rác status = 2;
        public ActionResult Restore(int id)
        {
            Category category = db.Categorys.Find(id);
            category.Status = 2;
            category.Update_By = 1;
            category.Update_At = DateTime.Now;
            db.Entry(category).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Trash","Category");
        }
    }
}
