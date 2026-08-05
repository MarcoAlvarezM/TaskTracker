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

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Project project)
        {
            if (ModelState.IsValid)
            {
                service.Add(project);
                return RedirectToAction("Index");
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
            return View(service.GetById(id));
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            service.Delete(id);
            return RedirectToAction("Index");
        }
    }
}