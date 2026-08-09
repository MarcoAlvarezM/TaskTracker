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


        private void LoadDropDowns(
            int? milestoneId = null,
            int? status = null,
            int? priority = null,
            int? responsibleId = null,
            int? assigneeId = null)
        {
            ViewBag.MilestoneId =
                DropdownHelper.GetMilestones(
                    service.GetMilestones(),
                    milestoneId);


            ViewBag.StatusList =
                new SelectList(
                    EnumHelper.GetSelectList<TaskStatus>(),
                    "Value",
                    "Text",
                    status);


            ViewBag.PriorityList =
                new SelectList(
                    EnumHelper.GetSelectList<TaskPriority>(),
                    "Value",
                    "Text",
                    priority);

            ViewBag.ResponsibleList =
                new SelectList(
                    service.GetActiveUsers(),
                    "UserId",
                    "Name",
                    responsibleId);

            ViewBag.AssigneeList =
                new SelectList(
                    service.GetActiveUsers(),
                    "UserId",
                    "Name",
                    assigneeId);
        }


        // GET: Task/Create
        public ActionResult Create(bool partial = false)
        {
            LoadDropDowns();

            if (partial)
            {
                return PartialView("_CreateTask", new Task());
            }

            return View();
        }


        // POST: Task/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Task task,bool fromHome = false)
        {
            if (ModelState.IsValid)
            {
                task.CreatedDate = DateTime.Now;
                task.ModifiedDate = DateTime.Now;

                service.Add(task);


                if (fromHome)
                {
                    return RedirectToAction("Index", "Home");
                }


                return RedirectToAction("Index");
            }


            LoadDropDowns(
                task.MilestoneId,
                task.Status,
                task.Priority);


            if (fromHome)
            {
                return PartialView("_CreateTask", task);
            }


            return View(task);
        }


        public ActionResult Edit(int id)
        {
            Task task = service.GetById(id);

            LoadDropDowns(
                task.MilestoneId,
                task.Status,
                task.Priority);

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


            LoadDropDowns(
                task.MilestoneId,
                task.Status,
                task.Priority);

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
