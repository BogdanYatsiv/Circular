using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface ISubprojectRepository
    {
        IEnumerable<Subproject> GetSubprojectsByProjectId(int projectId);

        void Create(Subproject item);
        void Update(Subproject item);
        void Delete(int id);
        Subproject getProjectById(int id);
        void Save();
    }
}
