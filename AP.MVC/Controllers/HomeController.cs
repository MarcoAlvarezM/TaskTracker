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


            ViewBag.Milestones =
                new SelectList(
                    db.Milestones,
                    "Id",
                    "Name");


            return View(projects);
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