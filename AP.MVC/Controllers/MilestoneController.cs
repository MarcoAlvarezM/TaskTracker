using System.Web.Mvc;
using TaskTracker.Data;
using TaskTracker.Domain;
using TaskTracker.Domain.Enums;
using TaskTracker.Domain.Helpers;

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

        private void LoadDropDowns(int? projectId = null, int? status = null)
        {
            ViewBag.ProjectId = new SelectList(
                service.GetProjects(),
                "ProjectId",
                "Name",
                projectId);

            ViewBag.StatusList = new SelectList(
                EnumHelper.GetSelectList<MilestoneStatus>(),
                "Value",
                "Text",
                status);
        }

        public ActionResult Create()
        {
            LoadDropDowns();
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

            LoadDropDowns(milestone.ProjectId, milestone.Status);

            return View(milestone);
        }

        public ActionResult Edit(int id)
        {
            Milestone milestone = service.GetById(id);

            LoadDropDowns(milestone.ProjectId, milestone.Status);

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

            LoadDropDowns(milestone.ProjectId, milestone.Status);

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