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
    public class CommitRepository : ICommitRepository
    {
        private readonly ApplicationDbContext dbContext;

        public CommitRepository(ApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public void Create(Commit item)
        {
            dbContext.Commits.Add(item);
        }

        public IEnumerable<Commit> GetCommitsBySubprojectId(int subprojectId)
        {
            return dbContext.Commits.Where(x => x.SubProjectId == subprojectId).ToList();
        }

        public Commit GetCommit(string ownername, string descr, DateTime dateTime, int id)
        {
            return dbContext.Commits.Where(x => x.ownerName == ownername && x.description == descr && x.createTime == dateTime && x.SubProjectId == id).FirstOrDefault();
        }

        public IEnumerable<Commit> GetCommitsByOwnerName(string ownerName, int subprojectId)
        {
            return dbContext.Commits.Where(x => x.ownerName == ownerName && x.SubProjectId == subprojectId).ToList();
        }
    }
}
