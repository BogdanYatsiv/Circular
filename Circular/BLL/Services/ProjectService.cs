using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    class ProjectService: IProjectService
    {
        private UnitOfWork _repository;
        public ProjectService(UnitOfWork repository)
        {
            _repository = repository;
        }
        public async Task CreateProject(ProjectDTO testDto)
        {
            await Task.Run(() => _repository.Projects.Create(new Project { githubLink = testDto.githubLink }));
        }
        public async Task DeleteProject(int projectId)
        {
            await Task.Run(() => _repository.Projects.Delete(projectId));
        }
        public async Task<IEnumerable<Project>> GetProjectsByUserId(string userId)
        {
            return await Task.Run(() =>
                _repository.Projects.GetProjectsByUserId(userId)
            );
        }
    }
}
