using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.Web.Models;
using Capstone.Web.DAL;
using System.Data.SqlClient;


namespace Capstone.Web.Controllers
{
    public class AdminController : Controller
    {
        IAddHotspotDAL dal;
        public AdminController(IAddHotspotDAL dal)
        {
            this.dal = dal;
        }
    
        // GET: User
        public ActionResult Index()
        {
            return View("AddHotspot");
        }

        [HttpPost]
        public ActionResult AddHotspot(AddHotspot addHotspot)
        {
            bool success = dal.SaveNewHotspot(addHotspot);
            return RedirectToAction("AddHotspotResult", success);
        }
    }
}