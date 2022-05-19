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
    public class SubprojectRepository : ISubprojectRepository
    {
        private readonly ApplicationDbContext dbContext;

        public SubprojectRepository(ApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
        }
        public Subproject getSubprojectById(int id)
        {
            return dbContext.Subprojects.Where(x => x.Id == id).FirstOrDefault();
        }

        public Subproject FindSubproject(string url)
        {
            return dbContext.Subprojects.Where(x => x.githubLink == url).FirstOrDefault();
        }
        public void Create(Subproject item)
        {
            dbContext.Subprojects.Add(item);
        }
        public void Update(Subproject item)
        {
            dbContext.Subprojects.Update(item);
        }
        public void Delete(int id)
        {
            Subproject item = dbContext.Subprojects.Find(id);
            if (item != null)
                dbContext.Subprojects.Remove(item);
        }
        public void Save()
        {
            dbContext.SaveChanges();
        }

        public IEnumerable<Subproject> GetSubprojectsByProjectId(int projectId)
        {
            return dbContext.Subprojects.Where(x => x.ProjectId == projectId).ToList();
        }

    }
}
