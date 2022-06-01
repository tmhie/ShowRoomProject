
using Microsoft.AspNet.Identity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Vehicle_ShowRoom_Manager_System.Models;


namespace Vehicle_ShowRoom_Manager_System.Controllers
{
    public class AdminsController : Controller
    {
        private Vehicle_ShowRoom_Manager_System_DataEntities db = new Vehicle_ShowRoom_Manager_System_DataEntities();

 
        public ActionResult LoginAdmin()
        {
            return View();
        }

        // GET: Admins
        [Authorize]
        public ActionResult Index(string currentFilter,string searchString , int? page)
        {
            if (IsSuperAdminAsync())
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
                var admin = db.Admin.Include(e => e.Vehicle);
                if (!String.IsNullOrEmpty(searchString))
                {
                    admin = admin.Where(admin1 => admin1.AdminName.ToLower().Contains(searchString.ToLower()));
                }
                admin = admin.OrderByDescending(admin1 => admin1.AdminName);

                int pageSize = 3;
                int pageNumber = (page ?? 1);
                return View(admin.ToPagedList(pageNumber, pageSize));
            }
            return RedirectToAction("Index", "Vehicles");
        }

        // GET: Admins/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admin.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // GET: Admins/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "AdminId,AdminName,Email,Password,Status")] Admin admin)
        {
            if (ModelState.IsValid && IsSuperAdminAsync())
            {
                admin.Password = EncodePassword(admin.Password);
                db.Admin.Add(admin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "you don't have permission to do that");
            }
            return View(admin);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LoginAdmin(LoginAdmin admin)
        {
            string query = "select * from Admin where Email = @p0 and Password = @p1";
            Admin admin1 = await db.Admin.SqlQuery(query, admin.Email, EncodePassword(admin.Password)).SingleOrDefaultAsync(); 
            if (admin1!= null && ModelState.IsValid)
            {
                FormsAuthentication.SetAuthCookie(admin1.AdminName, admin.RememberMe);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "login faill");
            }
            return View(admin);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }

        // GET: Admins/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admin admin = db.Admin.Find(id);
            if (admin == null)
            {
                return HttpNotFound();
            }
            return View(admin);
        }

        // POST: Admins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "AdminId,AdminName,Email,Password,Status")] Admin admin)
        {
            if (ModelState.IsValid && IsSuperAdminAsync())
            {
                admin.Password = EncodePassword(admin.Password);
                db.Entry(admin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "you dont have permission to do that");
            }
            return View(admin);
        }

        // GET: Admins/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (IsSuperAdminAsync())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Admin admin = db.Admin.Find(id);
                if (admin == null)
                {
                    return HttpNotFound();
                }
                return View(admin);
            }
            return View();
        }

        // POST: Admins/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Admin admin = db.Admin.Find(id);
            db.Admin.Remove(admin);
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
        public bool IsSuperAdminAsync()
        {
            string admiName = User.Identity.GetUserName();
            var admin = db.Admin.Where(w => w.AdminName == admiName && w.Status == 2);
            if (admin.Count() > 0) 
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static string EncodePassword(string password)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            Byte[] originalBytes = ASCIIEncoding.Default.GetBytes(password);
            Byte[] endcodeBytes = md5.ComputeHash(originalBytes);
            return BitConverter.ToString(endcodeBytes).Replace("-", "").ToLower();
        }
    }
}
