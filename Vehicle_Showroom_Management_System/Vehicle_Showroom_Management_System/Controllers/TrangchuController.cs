using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vehicle_Showroom_Management_System.Models;

namespace Vehicle_Showroom_Management_System.Controllers
{
    public class TrangchuController : Controller
    {
        // GET: Trangchu
        private ShowRoomDbContext db = new ShowRoomDbContext ();
        public ActionResult Index()
        {
            ViewBag.SoMauTin = db.Products.Count();
            return View();
        }
    }
}