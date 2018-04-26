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
        IAdminNewHotspotDAL dal;
        public AdminController(IAdminNewHotspotDAL dal)
        {
            this.dal = dal;
        }

        // GET: User
        //public ActionResult Index()
        //{
        //    return View("AddHotspot");
        //}

        [HttpGet]
        public ActionResult AddHotspot()
        {
            return View("AddHotspot");   
        }

        [HttpPost]
        public ActionResult AddHotspot(AdminNewHotspotModel addHotspot)
        {
            bool success = dal.SaveNewHotspot(addHotspot);
            return RedirectToAction("AddHotspotResult", success);
        }

        [HttpGet]
        public ActionResult AddHotspotResult(AdminNewHotspotModel addHotspot)
        {
            return View("AddHotspotResult");
        }

    }
}