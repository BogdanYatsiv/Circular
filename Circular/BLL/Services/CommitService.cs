using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CommitService : ICommitService
    {
        private IUnitOfWork _repository;
        public CommitService(IUnitOfWork repository)
        {
            _repository = repository;
        }
        public async Task CreateCommit(CommitDTO commitDto)
        {
            await Task.Run(() =>
            {
                _repository.Commits.Create(new Commit { ownerName = commitDto.ownerName,
                                                    description = commitDto.description,
                                                    createTime = commitDto.createTime
                });
                _repository.Save();
            });
        }

        public async Task<IEnumerable<Commit>> GetCommitsBySubprojectId(int subprojectId)
        {
            return await Task.Run(() =>
                _repository.Commits.GetCommitsBySubprojectId(subprojectId)
            );
        }

        public async Task<Commit> FindCommit(string ownername, string descr, DateTime dateTime, int id)
        {
            return await Task.Run(() => _repository.Commits.GetCommit(ownername, descr, dateTime, id));
        }

        public async Task<IEnumerable<Commit>> GetCommitsByOwnerName(string ownerName, int subprojectId)
        {
            return await Task.Run(() =>
                _repository.Commits.GetCommitsByOwnerName(ownerName, subprojectId).ToList()
            ) ;
        }

    }
}
