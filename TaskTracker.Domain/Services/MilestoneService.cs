using System;
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

        public bool Delete(int id, out string message)
        {
            Milestone milestone = db.Milestones.Find(id);

            if (milestone == null)
            {
                message = "Milestone not found.";
                return false;
            }

            if (milestone.Tasks.Any())
            {
                message = "Cannot delete this milestone because it contains tasks. Delete or move the tasks first.";
                return false;
            }

            db.Milestones.Remove(milestone);
            db.SaveChanges();

            message = string.Empty;
            return true;
        }
    }
}