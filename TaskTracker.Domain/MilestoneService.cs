using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TaskTracker.Data;

namespace TaskTracker.Domain
{
    public class MilestoneService
    {
        TaskTrackerEntities db = new TaskTrackerEntities();

        public List<Milestone> GetAll()
        {
            return db.Milestones.ToList();
        }

        public Milestone GetById(int id)
        {
            return db.Milestones.Find(id);
        }

        public List<Project> GetProjects()
        {
            return db.Projects.ToList();
        }

        public void Add(Milestone milestone)
        {
            db.Milestones.Add(milestone);
            db.SaveChanges();
        }

        public void Update(Milestone milestone)
        {
            db.Entry(milestone).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var milestone = db.Milestones.Find(id);

            if (milestone != null)
            {
                db.Milestones.Remove(milestone);
                db.SaveChanges();
            }
        }
    }
}