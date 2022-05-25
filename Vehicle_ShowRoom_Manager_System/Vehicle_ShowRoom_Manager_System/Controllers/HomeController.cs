using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
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
        public ActionResult ViewAll(int? page)
        {
            var vehicle = db.Vehicle.Include(d => d.VehicleImg);
            int pageNumber = (page ?? 1);
            int pageSize = 10;
            vehicle = vehicle.OrderByDescending(d => d.CreateDate);
            return View(vehicle.ToPagedList(pageNumber , pageSize));
        }
        public ActionResult CarDetail(int? id)
        {
            var vehicleImg = db.VehicleImg.Where(d => d.VehicleId == id).Include(d => d.Vehicle);
            return View(vehicleImg);
        }

       
    }
}