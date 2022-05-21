using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
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
        public ActionResult ViewAll()
        {
            return View();
        }
        public ActionResult CarDetail(int? id)
        {
            var vehicle = db.VehicleImg.Where(d => d.VehicleId == id).ToList();
            return View(vehicle);
        }

       
    }
}