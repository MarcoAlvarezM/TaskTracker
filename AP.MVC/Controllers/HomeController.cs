using System;
using System.Linq;
using System.Web.Mvc;
using TaskTracker.Data;

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
                new SelectList(
                    db.Milestones,
                    "Id",
                    "Name");


            ViewBag.StatusList =
                new SelectList(
                    Enum.GetValues(typeof(TaskTracker.Domain.Enums.TaskStatus))
                    .Cast<TaskTracker.Domain.Enums.TaskStatus>()
                    .Select(x => new
                    {
                        Value = (int)x,
                        Text = x.ToString()
                    }),
                    "Value",
                    "Text");


            ViewBag.PriorityList =
                new SelectList(
                    Enum.GetValues(typeof(TaskTracker.Domain.Enums.TaskPriority))
                    .Cast<TaskTracker.Domain.Enums.TaskPriority>()
                    .Select(x => new
                    {
                        Value = (int)x,
                        Text = x.ToString()
                    }),
                    "Value",
                    "Text");


            return PartialView("_CreateTask",
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