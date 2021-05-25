using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IProjectRepository
    {
        public IEnumerable<Project> GetProjectsByUserId(string userId);
        void Create(Project item);
        void Update(Project item);
        void Delete(int id);
        void Save();
    }
}
