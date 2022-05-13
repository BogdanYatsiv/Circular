using DAL.Data;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        private bool _disposed = false;

        public IUserRepository Users { get; }

        public IProjectRepository Projects { get; }
        public ISubprojectRepository Subprojects { get; }

        public UnitOfWork(
            ApplicationDbContext dbContext,
            IUserRepository userRepository,
            IProjectRepository projectRepository,
            ISubprojectRepository subprojectRepository
            )
        {
            _dbContext = dbContext;
            Users = userRepository;
            Projects = projectRepository;
            Subprojects = subprojectRepository;
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
                _dbContext.Dispose();
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
