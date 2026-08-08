using System;
using System.Linq;
using System.Web.Mvc;
using TaskTracker.Data;
using TaskTracker.Domain.Enums;
using TaskTracker.Domain.Helpers;

namespace TaskTracker.MVC.Controllers
{
    public class HomeController : Controller
    {

        private TaskTrackerEntities db = new TaskTrackerEntities();


        public ActionResult Index()
        {
            var projects = db.Projects
                .Include("Milestones.Tasks")
                .ToList();

            return View(projects);
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