using DAL.Data;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ApplicationDbContext dbContext;

        public ProjectRepository(ApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
        }
        public IEnumerable<Project> GetProjectsByUserId(string userId)
        {
            return dbContext.Projects.Where(x => x.UserId == userId).ToList();
        }
        public void Create(Project item)
        {
            dbContext.Projects.Add(item);
        }
        public void Update(Project item)
        {
            dbContext.Projects.Update(item);
        }
        public void Delete(int id)
        {
            Project item = dbContext.Projects.Find(id);
            if (item != null)
                dbContext.Projects.Remove(item);
        }
        public void Save()
        {
            dbContext.SaveChanges();
        }

    }
}
