using System.Linq;
using TaskTracker.Data;

namespace TaskTracker.Domain
{
    public class UserService
    {
        private TaskTrackerEntities db = new TaskTrackerEntities();


        public IQueryable<User> GetAll()
        {
            return db.Users;
        }


        public User GetById(int id)
        {
            return db.Users
                .FirstOrDefault(u => u.UserId == id);
        }


        public IQueryable<User> GetActiveUsers()
        {
            return db.Users
                .Where(u => u.IsActive);
        }


        public void Add(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();
        }


        public void Update(User user)
        {
            User existing =
                db.Users.Find(user.UserId);

            if (existing == null)
                return;


            existing.Name = user.Name;
            existing.Email = user.Email;
            existing.IsActive = user.IsActive;


            db.SaveChanges();
        }

        public bool Deactivate(int id, out string message)
        {
            User user =
                db.Users.Find(id);


            if (user == null)
            {
                message = "User not found.";
                return false;
            }


            if (!user.IsActive)
            {
                message = "User is already inactive.";
                return false;
            }


            user.IsActive = false;

            db.SaveChanges();


            message =
                "User deactivated successfully.";

            return true;
        }


        public bool Delete(int id, out string message)
        {
            User user = db.Users.Find(id);


            if (user == null)
            {
                message = "User not found.";
                return false;
            }

            message =
                "Users should be deactivated instead of deleted.";

            return false;
        }
    }
}
