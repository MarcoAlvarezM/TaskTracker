using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TaskTracker.Data;

namespace TaskTracker.Domain
{
    public class ProjectService
    {
        TaskTrackerEntities db = new TaskTrackerEntities();

        public List<Project> GetAll()
        {
            return db.Projects.ToList();
        }

        public Project GetById(int id)
        {
            return db.Projects.Find(id);
        }

        public void Add(Project project)
        {
            db.Projects.Add(project);
            db.SaveChanges();
        }

        public void Update(Project project)
        {
            db.Entry(project).State = EntityState.Modified;
            db.SaveChanges();
        }

        public bool HasMilestones(int projectId)
        {
            return db.Milestones.Any(m => m.ProjectId == projectId);
        }

        public bool Delete(int id, out string message)
        {
            Project project = db.Projects.Find(id);

            if (project == null)
            {
                message = "Project not found.";
                return false;
            }

            if (db.Milestones.Any(m => m.ProjectId == id))
            {
                message = "Cannot delete this project because it contains milestones. Delete all milestones first.";
                return false;
            }

            db.Projects.Remove(project);

            db.SaveChanges();

            message = string.Empty;
            return true;
        }
    }
}