using System;
using System.Web.Mvc;
using TaskTracker.Data;
using TaskTracker.Domain;
using TaskTracker.Domain.Enums;
using TaskTracker.Domain.Helpers;

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

        private void LoadDropDowns(int? milestoneId = null, int? status = null, int? priority = null)
        {
            ViewBag.Milestones =
                DropdownHelper.GetMilestones(
                    service.GetMilestones(),
                    milestoneId);

            ViewBag.StatusList = new SelectList(
                EnumHelper.GetSelectList<TaskStatus>(),
                "Value",
                "Text",
                status);

            ViewBag.PriorityList = new SelectList(
                EnumHelper.GetSelectList<TaskPriority>(),
                "Value",
                "Text",
                priority);
        }

        public ActionResult Create()
        {
            LoadDropDowns();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Task task)
        {
            if (ModelState.IsValid)
            {
                task.CreatedDate = DateTime.Now;
                task.ModifiedDate = DateTime.Now;
                service.Add(task);
                return RedirectToAction("Index");
            }

            LoadDropDowns(task.MilestoneId, task.Status, task.Priority);
            return View(task);
        }

        public ActionResult Edit(int id)
        {
            Task task = service.GetById(id);

            LoadDropDowns(task.MilestoneId, task.Status, task.Priority);

            return View(task);
        }

        [HttpPost]
        public ActionResult Edit(Task task)
        {
            if (ModelState.IsValid)
            {
                task.ModifiedDate = DateTime.Now;
                service.Update(task);
                return RedirectToAction("Index");
            }

            LoadDropDowns(task.MilestoneId, task.Status, task.Priority);

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