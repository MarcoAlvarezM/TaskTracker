using System.Web.Mvc;
using TaskTracker.Data;
using TaskTracker.Domain;

namespace TaskTracker.MVC.Controllers
{
    public class ProjectController : Controller
    {
        ProjectService service = new ProjectService();


        public ActionResult Index()
        {
            return View(service.GetAll());
        }


        public ActionResult Details(int id)
        {
            return View(service.GetById(id));
        }


        // GET: Project/Create
        public ActionResult Create(bool partial = false)
        {
            if (partial)
            {
                return PartialView("_CreateProject", new Project());
            }

            return View();
        }


        // POST: Project/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Project project, bool fromHome = false)
        {
            if (ModelState.IsValid)
            {
                service.Add(project);


                if (fromHome)
                {
                    return RedirectToAction("Index", "Home");
                }


                return RedirectToAction("Index");
            }


            if (fromHome)
            {
                return PartialView("_CreateProject", project);
            }


            return View(project);
        }


        public ActionResult Edit(int id)
        {
            return View(service.GetById(id));
        }


        [HttpPost]
        public ActionResult Edit(Project project)
        {
            if (ModelState.IsValid)
            {
                service.Update(project);

                return RedirectToAction("Index");
            }


            return View(project);
        }


        public ActionResult Delete(int id)
        {
            Project project =
                service.GetById(id);


            ViewBag.HasMilestones = service.HasMilestones(id);


            return View(project);
        }


        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            string message;


            if (service.Delete(
                id,
                out message))
            {
                TempData["Success"] = "Project deleted successfully.";

                return RedirectToAction("Index");
            }


            TempData["Error"] = message;


            return RedirectToAction("Delete", new { id });
        }
    }
}