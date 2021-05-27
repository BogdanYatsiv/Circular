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
    public class ProjectService: IProjectService
    {
        private IUnitOfWork _repository;
        public ProjectService(IUnitOfWork repository)
        {
            _repository = repository;
        }
        public async Task CreateProject(ProjectDTO projectDto)
        {
            await Task.Run(() =>
            {
                _repository.Projects.Create(new Project { githubLink = projectDto.githubLink });
                _repository.Save();
            });
        }
        public async Task DeleteProject(int projectId)
        {
            await Task.Run(() =>
            {
                _repository.Projects.Delete(projectId);
                _repository.Save();
            });
        }
        public async Task<Project> FindProject(int projectId)
        {
            return await Task.Run(() => _repository.Projects.getProjectById(projectId));
        }

        public async Task<IEnumerable<Project>> GetProjectsByUserId(string userId)
        {
            return await Task.Run(() =>
                _repository.Projects.GetProjectsByUserId(userId)
            );
        }
    }
}
