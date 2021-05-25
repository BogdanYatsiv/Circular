using BLL.DTO;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    interface IProjectService
    {
        Task CreateProject(ProjectDTO testDto);
        Task DeleteProject(int projectId);
        Task<IEnumerable<Project>> GetProjectsByUserId(string userId);
    }
}
