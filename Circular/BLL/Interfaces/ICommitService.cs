using BLL.DTO;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICommitService
    {
        Task CreateCommit(CommitDTO commitDto);

        Task<IEnumerable<Commit>> GetCommitsBySubprojectId(int subprojectId);

        Task<IEnumerable<Commit>> GetCommitsByOwnerName(string ownerName, int subprojectId);
        Task<Commit> FindCommit(string ownername, string descr, DateTime dateTime, int id);

    }
}
