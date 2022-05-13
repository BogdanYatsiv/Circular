using BLL.DTO;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ISubprojectService
    {
        Task CreateProject(SubprojectDTO subprojectDto);
        Task DeleteProject(int subprojectId);

        Task<Subproject> FindProject(int subprojectId);

        Task<IEnumerable<Subproject>> GetSubprojectsByProjectId(int projectId);
    }
}
