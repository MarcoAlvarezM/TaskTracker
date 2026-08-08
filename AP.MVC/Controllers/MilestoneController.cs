using System.Linq;
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


        private void LoadDropDowns(int? projectId = null,int? status = null)
        {
            ViewBag.ProjectId =
                new SelectList(
                    service.GetProjects(),
                    "ProjectId",
                    "Name",
                    projectId);


            ViewBag.StatusList =
                new SelectList(
                    EnumHelper.GetSelectList<MilestoneStatus>(),
                    "Value",
                    "Text",
                    status);
        }


        // GET: Milestone/Create
        public ActionResult Create(bool partial = false)
        {
            LoadDropDowns();


            if (partial)
            {
                return PartialView(
                    "_CreateMilestone",
                    new Milestone());
            }


            return View();
        }


        // POST: Milestone/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Milestone milestone,bool fromHome = false)
        {
            if (ModelState.IsValid)
            {
                service.Add(milestone);


                if (fromHome)
                {
                    return RedirectToAction(
                        "Index",
                        "Home");
                }


                return RedirectToAction("Index");
            }


            LoadDropDowns(
                milestone.ProjectId,
                milestone.Status);


            if (fromHome)
            {
                return PartialView(
                    "_CreateMilestone",
                    milestone);
            }


            return View(milestone);
        }


        public ActionResult Edit(int id)
        {
            Milestone milestone =
                service.GetById(id);


            LoadDropDowns(
                milestone.ProjectId,
                milestone.Status);


            return View(milestone);
        }


        [HttpPost]
        public ActionResult Edit(
            Milestone milestone)
        {
            if (ModelState.IsValid)
            {
                service.Update(milestone);

                return RedirectToAction("Index");
            }


            LoadDropDowns(
                milestone.ProjectId,
                milestone.Status);


            return View(milestone);
        }


        public ActionResult Delete(int id)
        {
            Milestone milestone =
                service.GetById(id);


            ViewBag.HasTasks =
                milestone.Tasks.Any();


            return View(milestone);
        }


        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            string message;


            if (service.Delete(id, out message))
            {
                TempData["Success"] =
                    "Milestone deleted successfully.";

                return RedirectToAction("Index");
            }


            TempData["Error"] = message;


            return RedirectToAction("Delete",new { id });
        }
    }
}
