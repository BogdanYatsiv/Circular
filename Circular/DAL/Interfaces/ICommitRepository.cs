using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface ICommitRepository
    {
        void Create(Commit item);

        IEnumerable<Commit> GetCommitsBySubprojectId(int subprojectId);

        IEnumerable<Commit> GetCommitsByOwnerName(string ownerName, int subprojectId);

        Commit GetCommit(string ownername, string descr, DateTime dateTime, int id);
    }
}
