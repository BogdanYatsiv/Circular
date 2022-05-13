using BLL.DTO;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IProjectService
    {
        Task CreateProject(ProjectDTO projectDto);
        Task DeleteProject(int projectId);
        Task<Project> FindProjectById(int projectId);
        Task<Project> FindProjectByName(string projectName);

        Task<IEnumerable<Project>> GetProjectsByUserId(string userId);
    }
}
