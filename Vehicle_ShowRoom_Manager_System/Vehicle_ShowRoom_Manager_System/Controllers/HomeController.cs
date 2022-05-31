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
using PagedList;
using PagedList.Mvc;
using Vehicle_ShowRoom_Manager_System.Models;

namespace Project3.Controllers
{
    public class HomeController : Controller
    {
        private Vehicle_ShowRoom_Manager_System_DataEntities db = new Vehicle_ShowRoom_Manager_System_DataEntities();

        public ActionResult Index()
        {
            var vehicle = db.Vehicle.Include(v => v.Admin).Include(v => v.VehicleImg);           
            return View(vehicle.ToList());
        }
        public ActionResult search(string search, int? page)
        {
            var vehicle = db.Vehicle.Include(v => v.Admin).Include(v => v.VehicleImg).Where(s => s.VehicleName.Contains(search));
            vehicle = vehicle.OrderByDescending(d => d.CreateDate);
            int pageNumber = (page ?? 1);
            int pageSize = 9;
            return View("ViewAll", vehicle.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult ViewAll(int? page)
        {
            var vehicle = db.Vehicle.Include(d => d.VehicleImg);
            int pageNumber = (page ?? 1);
            int pageSize = 9;
            vehicle = vehicle.OrderByDescending(d => d.CreateDate);
            return View(vehicle.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult CarDetail(int? id)
        {
            var vehicleImg = db.VehicleImg.Where(d => d.VehicleId == id).Include(d => d.Vehicle);
            return View(vehicleImg);
        }

        public ActionResult Login()
        {
            return View();
        } 

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginUser user)
        {
                     
            if ( ModelState.IsValid )
            {
                var user1 = db.Customer.Where(i => i.CustomerName == user.UserName);
                if (user1.FirstOrDefault().Password == EncodePassword(user.Password) && user1 != null)
                {
                    Session["UserID"] = user1.FirstOrDefault().CustomerName.ToString();
                    Session["UserName"] = user1.FirstOrDefault().CustomerName.ToString();
                    return RedirectToAction("ViewAll", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "wrong user name or password");
                }
               

            }
            else
            {
                ModelState.AddModelError("", "login faill");
            }
            return View(user);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register ([Bind(Include = "CustomerId, CustomerName, Email, Password, Address, Gender, Status")]UserRegister user)
        {
            if (user.Password.Equals(user.ConfirmPassword))
            {
                if (ModelState.IsValid) 
                {
                    Customer user1 = null;
                    user1.CustomerName = user.UserName;
                    user1.Email = user.Email;
                    user1.Password = EncodePassword(user.Password);
                    user1.Gender = user.Gender;
                    user1.Address = user.Address;
                    if(db.Customer.Where(i => i.CustomerName == user1.Password) == null)
                    {
                        db.Customer.Add(user1);
                        db.SaveChanges();
                        ModelState.AddModelError("", "Register Successful");
                        return View(user);
                    }
                    else
                    {
                        ModelState.AddModelError("", "this user name already exist");
                    }
                    
                }
            }
            else
            {
                ModelState.AddModelError("", "confirm password not match");
            }
            return View(user);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return View("Index");
        }

        public static string EncodePassword(string password)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            Byte[] originalBytes = ASCIIEncoding.Default.GetBytes(password);
            Byte[] endcodeBytes = md5.ComputeHash(originalBytes);
            return BitConverter.ToString(endcodeBytes);
        }

    }
}