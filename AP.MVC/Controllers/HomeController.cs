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

        public ActionResult CreateTask()
        {

            ViewBag.Milestones =
                DropdownHelper.GetMilestones(
                    db.Milestones);


            ViewBag.StatusList =
                new SelectList(
                    EnumHelper.GetSelectList<TaskStatus>(),
                    "Value",
                    "Text");


            ViewBag.PriorityList =
                new SelectList(
                    EnumHelper.GetSelectList<TaskPriority>(),
                    "Value",
                    "Text");


            return PartialView(
                "_CreateTask",
                new Task());
        }


        [HttpPost]
        public ActionResult CreateTask(Task task)
        {

            if (ModelState.IsValid)
            {

                task.CreatedDate = System.DateTime.Now;

                task.ModifiedDate = System.DateTime.Now;


                db.Tasks.Add(task);

                db.SaveChanges();


                return RedirectToAction("Index");
            }


            ViewBag.Milestones =
                new SelectList(
                    db.Milestones,
                    "Id",
                    "Name");


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