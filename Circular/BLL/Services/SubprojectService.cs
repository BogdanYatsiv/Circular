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
    public class SubprojectService : ISubprojectService
    { 
        private IUnitOfWork _repository;
        public SubprojectService(IUnitOfWork repository)
        {
            _repository = repository;
        }
        public async Task CreateProject(SubprojectDTO subprojectDto)
        {
            await Task.Run(() =>
            {
                _repository.Subprojects.Create(new Subproject { githubLink = subprojectDto.githubLink });
                _repository.Save();
            });
        }
        public async Task DeleteProject(int subprojectId)
        {
            await Task.Run(() =>
            {
                _repository.Subprojects.Delete(subprojectId);
                _repository.Save();
            });
        }

        public async Task<Subproject> FindProject(int subprojectId)
        {
            return await Task.Run(() => _repository.Subprojects.getProjectById(subprojectId));
        }

        public async Task<IEnumerable<Subproject>> GetSubprojectsByProjectId(int projectId)
        {
            return await Task.Run(() =>
                _repository.Subprojects.GetSubprojectsByProjectId(projectId)
            );
        }
    }
}
