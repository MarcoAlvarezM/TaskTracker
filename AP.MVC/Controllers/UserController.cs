using System.Web.Mvc;
using TaskTracker.Data;
using TaskTracker.Domain;

namespace TaskTracker.MVC.Controllers
{
    public class UserController : Controller
    {
        private UserService service = new UserService();


        public ActionResult Index()
        {
            return View(service.GetAll());
        }


        public ActionResult Details(int id)
        {
            User user = service.GetById(id);

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }


        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                service.Add(user);

                TempData["Success"] = "User created successfully.";

                return RedirectToAction("Index");
            }

            return View(user);
        }


        public ActionResult Edit(int id)
        {
            User user = service.GetById(id);

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                service.Update(user);

                TempData["Success"] = "User updated successfully.";

                return RedirectToAction("Index");
            }

            return View(user);
        }

        public ActionResult Delete(int id)
        {
            User user = service.GetById(id);

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            string message;


            if (service.Deactivate(id, out message))
            {
                TempData["Success"] = message;

                return RedirectToAction("Index");
            }


            TempData["Error"] = message;

            return RedirectToAction("Delete", new { id });
        }
    }
}
