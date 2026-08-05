using System.Web.Mvc;
using TaskTracker.Data;
using TaskTracker.Domain;

namespace TaskTracker.MVC.Controllers
{
    public class MilestoneController : Controller
    {
        MilestoneService service = new MilestoneService();

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
            ViewBag.ProjectId = new SelectList(service.GetProjects(), "ProjectId", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Milestone milestone)
        {
            if (ModelState.IsValid)
            {
                service.Add(milestone);
                return RedirectToAction("Index");
            }

            ViewBag.ProjectId = new SelectList(service.GetProjects(), "ProjectId", "Name", milestone.ProjectId);
            return View(milestone);
        }

        public ActionResult Edit(int id)
        {
            Milestone milestone = service.GetById(id);

            ViewBag.ProjectId = new SelectList(service.GetProjects(), "ProjectId", "Name", milestone.ProjectId);

            return View(milestone);
        }

        [HttpPost]
        public ActionResult Edit(Milestone milestone)
        {
            if (ModelState.IsValid)
            {
                service.Update(milestone);
                return RedirectToAction("Index");
            }

            ViewBag.ProjectId = new SelectList(service.GetProjects(), "ProjectId", "Name", milestone.ProjectId);

            return View(milestone);
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