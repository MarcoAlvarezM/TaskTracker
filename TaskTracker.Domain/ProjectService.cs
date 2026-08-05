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

        public void Delete(int id)
        {
            var project = db.Projects.Find(id);

            if (project != null)
            {
                db.Projects.Remove(project);
                db.SaveChanges();
            }
        }
    }
}