using System.Web.Mvc;
using TaskTracker.Data;
using TaskTracker.Domain;

namespace TaskTracker.MVC.Controllers
{
    public class TaskController : Controller
    {
        TaskService service = new TaskService();

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
            ViewBag.MilestoneId = new SelectList(service.GetMilestones(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Task task)
        {
            if (ModelState.IsValid)
            {
                service.Add(task);
                return RedirectToAction("Index");
            }

            ViewBag.MilestoneId = new SelectList(service.GetMilestones(), "Id", "Name", task.MilestoneId);
            return View(task);
        }

        public ActionResult Edit(int id)
        {
            Task task = service.GetById(id);

            ViewBag.MilestoneId = new SelectList(service.GetMilestones(), "Id", "Name", task.MilestoneId);

            return View(task);
        }

        [HttpPost]
        public ActionResult Edit(Task task)
        {
            if (ModelState.IsValid)
            {
                service.Update(task);
                return RedirectToAction("Index");
            }

            ViewBag.MilestoneId = new SelectList(service.GetMilestones(), "Id", "Name", task.MilestoneId);

            return View(task);
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