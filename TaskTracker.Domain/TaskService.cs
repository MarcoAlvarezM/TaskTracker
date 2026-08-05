using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TaskTracker.Data;

namespace TaskTracker.Domain
{
    public class TaskService
    {
        TaskTrackerEntities db = new TaskTrackerEntities();

        public List<Task> GetAll()
        {
            return db.Tasks.ToList();
        }

        public Task GetById(int id)
        {
            return db.Tasks.Find(id);
        }

        public List<Milestone> GetMilestones()
        {
            return db.Milestones.ToList();
        }

        public void Add(Task task)
        {
            db.Tasks.Add(task);
            db.SaveChanges();
        }

        public void Update(Task task)
        {
            db.Entry(task).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var task = db.Tasks.Find(id);

            if (task != null)
            {
                db.Tasks.Remove(task);
                db.SaveChanges();
            }
        }
    }
}