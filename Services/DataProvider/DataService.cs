namespace Services.DataProvider
{
    using Contracts;
    using MeTube.Models;
    using MeTube.Data;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;

    public class DataService : IDataService
    {
        private readonly MeTubeDbContext db;

        public DataService()
        {
            this.db = new MeTubeDbContext();
        }

 

        public void CreateTube(Tube tube)
        {
            this.db.Tubes.Add(tube);
            this.db.SaveChanges();
        }

        public IEnumerable<Tube> GetAllTubes()
        {
            return this.db.Tubes.ToList();
        }

        public Tube GetTubeById(int id)
        {
            var tube = this.db.Tubes.Include(t => t.User).Where(t => t.Id == id).FirstOrDefault();

            return tube;
        }

        public User GetUserByName(string username)
        {
            var user = this.db.Users.Include(u => u.Tubes)
                .Where(u => u.Username == username)
                .FirstOrDefault();
            

            return user;
        }

        public  void IncreaseVies(Tube tube)
        {
            tube.Views++;
            db.SaveChanges();
        }

        public bool IsUserExist(string username)
        {
            var user = this.db
                .Users
                .Where(u => u.Username == username)
                .FirstOrDefault();

            if (user == null)
            {
                return false;
            }

            return true;
        }

        public void RegisterUser(User user)
        {
            this.db.Users.Add(user);
            this.db.SaveChanges();
        }
    }
}
